using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NgrokAspNetCore
{
	class NgrokServer : IServer
	{
		public IFeatureCollection Features => throw new NotImplementedException();

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
