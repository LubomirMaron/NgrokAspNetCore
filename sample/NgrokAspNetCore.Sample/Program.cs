using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NgrokAspNetCore.Sample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
			var exitEvent = new ManualResetEvent(false);
			var host = CreateHostBuilder(args).Build();

            

            //host.Run();

			await host.StartAsync();

			await Task.Delay(10000);

			// NgrokAspNetCore - Only change needed to this file
			var tunnels = await host.StartNgrokAsync();



			exitEvent.WaitOne();

			await host.StopAsync();

		}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
