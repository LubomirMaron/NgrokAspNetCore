﻿// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.
// Copyright (c) 2019 Kevin Gysberg

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ngrok.AspNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Ngrok.ApiClient;

namespace Ngrok.AspNetCore
{
	public static class NgrokAspNetCoreExtensions
	{
		public static void AddNgrok(this IServiceCollection services, NgrokOptions options = null)
		{
			services.TryAddSingleton<NgrokProcessMgr>();

			services.AddHttpClient<NgrokDownloader>();
			services.AddHttpClient<INgrokApiClient, NgrokHttpClient>();


			services.TryAddSingleton(options ?? new NgrokOptions());

			services.AddLogging();

			services.AddSingleton<NgrokHostedService>();
			services.AddSingleton<INgrokHostedService>(p => p.GetRequiredService<NgrokHostedService>());
			services.AddSingleton<IHostedService>(p => p.GetRequiredService<NgrokHostedService>());
		}
	}
}