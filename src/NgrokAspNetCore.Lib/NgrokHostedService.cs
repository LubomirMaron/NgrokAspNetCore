using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NgrokAspNetCore
{
	public class NgrokHostedService : IHostedService
	{
		private readonly ILogger<NgrokHostedService> _logger;
		private readonly NgrokOptions _options;
		private readonly NgrokLocalApiClient _ngrokClient;
		private readonly IServer _server;
		private readonly IApplicationLifetime _applicationLifetime;

		private Timer _addressPollTimer;

		public NgrokHostedService(
			NgrokOptions options, 
			NgrokLocalApiClient ngrokClient, 
			IServer server,
			ILogger<NgrokHostedService> logger,
			IApplicationLifetime applicationLifetime)
		{
			_options = options;
			_ngrokClient = ngrokClient;
			_server = server;
			_logger = logger;
			_applicationLifetime = applicationLifetime;
			_applicationLifetime.ApplicationStarted.Register(async () => await OnApplicationStarted());
		}

		public async Task OnApplicationStarted()
		{
			_logger.LogInformation("App started triggered");
			await Task.Delay(10);
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Starting NgrokHostedService");
			_addressPollTimer = new Timer(PollAddresses, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

			// Start Ngrok download if opted in

			// Start task to wait/poll for Addresses to be populated
			var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			cts.CancelAfter(TimeSpan.FromSeconds(30));
			// var addresses = await PollAddresses(TimeSpan.FromSeconds(5), cts.Token);
			Task.Run(() => PollAddresses(TimeSpan.FromSeconds(5), cts.Token));

			// Await download & address poll tasks

			// Start tunnel

			// Watch tunnel and restart as needed

			//throw new NotImplementedException();
		}

		private async Task<string[]> PollAddresses(TimeSpan delay, CancellationToken cancellationToken = default)
		{
			if (cancellationToken == default)
			{
				var cts = new CancellationTokenSource(delay);
				cancellationToken = cts.Token;
			}

			int iterationCount = 0;

			while (!cancellationToken.IsCancellationRequested)
			{
				_logger.LogInformation("Waiting for local addresses. Iteration #{iteration}", iterationCount);
				var addresses = _server?.Features.Get<IServerAddressesFeature>().Addresses.ToArray();
				if (addresses != null && addresses.Any())
				{
					return addresses;
				}
				await Task.Delay(Convert.ToInt32(delay.TotalMilliseconds), cancellationToken);
				iterationCount++;
			}
			cancellationToken.ThrowIfCancellationRequested();
			return null;
		}

		private void PollAddresses(object state)
		{
			var addresses = _server?.Features.Get<IServerAddressesFeature>().Addresses.ToArray();

			if (addresses.Any())
			{

			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
