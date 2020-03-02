using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Ngrok.ApiClient.IntegrationTests
{
	[Order(1)]
	public class CreateGetListDelete_PositiveCase
	{
		private INgrokApiClient _client;

		public CreateGetListDelete_PositiveCase()
		{
			_client = new NgrokHttpClient(new HttpClient());
		}

		[Fact, Order(2)]
		public async Task ListTunnelsAsync_Null()
		{
			var tunnels = await _client.ListTunnelsAsync();
			Assert.True(true);
		}

		[Fact, Order(3)]
		public async Task StartTunnel_Http()
		{
			// Setup - Create Tunnel
			var tunnelRequest = new StartTunnelRequest()
			{
				Name = "Test_StartTunnel_Http",
				Protocol = "http",
				Address = "1234"
			};
			var response = await _client.StartTunnelAsync(tunnelRequest);

			Assert.NotNull(response);
		}

		[Fact, Order(4)]
		public async Task GetTunnel()
		{
			var tunnels = await _client.GetTunnelAsync("Test_StartTunnel_Http");
			Assert.True(true);
		}

		[Fact, Order(5)]
		public async Task ListTunnelsAsync_Single()
		{
			var tunnels = await _client.ListTunnelsAsync();

			Assert.NotNull(tunnels);
			Assert.NotEmpty(tunnels);
		}

		[Fact, Order(6)]
		public async Task StopTunnel_Https()
		{
			await _client.StopTunnelAsync("Test_StartTunnel_Http");
			Assert.True(true);
		}

		[Fact, Order(7)]
		public async Task StopTunnel_Http()
		{
			await _client.StopTunnelAsync("Test_StartTunnel_Http (http)");
			Assert.True(true);
		}

		[Fact, Order(8)]
		public async Task ListTunnelsAsync_Null2()
		{
			var tunnels = await _client.ListTunnelsAsync();
			Assert.True(true);
		}
	}
}