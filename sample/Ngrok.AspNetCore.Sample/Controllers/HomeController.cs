﻿using System.Diagnostics;
using System.Threading.Tasks;
using Ngrok.AspNetCore.Sample.Models;
using Ngrok.AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ngrok.AspNetCore;

namespace Ngrok.AspNetCore.Sample.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly INgrokHostedService _ngrokService;

		public HomeController(ILogger<HomeController> logger, INgrokHostedService ngrokService)
		{
			_logger = logger;
			_ngrokService = ngrokService;
		}

		public async Task<IActionResult> Index()
		{
			var tunnels = await _ngrokService.GetTunnelsAsync();
			return View(tunnels);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}