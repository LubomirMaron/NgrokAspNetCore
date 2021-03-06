﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Ngrok.ApiClient;

namespace Ngrok.AspNetCore
{
	public interface INgrokHostedService : IHostedService
	{
		Task<IEnumerable<Tunnel>> GetTunnelsAsync();

		event Action<IEnumerable<Tunnel>> Ready;
	}
}