using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NgrokAspNetCore
{
	public class AddressMonitor : IDisposable
	{
		private readonly IServer _server;
		private Timer _addressPollTimer;
		public Func<string[], Task> AddressResolved;

		public AddressMonitor(IServer server)
		{
			_server = server;
		}

		private void Start()
		{
			_addressPollTimer = new Timer(PollAddresses, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
		}

		private void Stop()
		{
			_addressPollTimer?.Change(Timeout.Infinite, 0);
		}

		private void PollAddresses(object state)
		{
			var addresses = _server?.Features.Get<IServerAddressesFeature>().Addresses.ToArray();

			if (addresses.Any())
			{
				Stop();
				AddressResolved(addresses);
			}
		}

		public void Dispose()
		{
			_addressPollTimer?.Dispose();
		}
	}
}
