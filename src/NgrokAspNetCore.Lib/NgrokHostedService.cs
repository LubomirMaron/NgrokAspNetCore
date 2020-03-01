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

			applicationLifetime.ApplicationStarted.Register(async () => await OnApplicationStarted());
		}

		public async Task OnApplicationStarted()
		{
			_logger.LogInformation("App started triggered");
			await Task.Delay(10);
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Starting NgrokHostedService");

			// TODO - Download Ngrok if opted in

			// TODO - Start tunnels

			// TODO - Watch tunnel and restart as needed
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await StopNgrokAsync(cancellationToken);
		}

		private async Task StopNgrokAsync(CancellationToken cancellationToken)
		{
			// TODO
		}
	}
}
