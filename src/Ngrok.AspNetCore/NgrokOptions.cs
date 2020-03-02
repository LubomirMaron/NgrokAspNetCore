﻿// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.
// Copyright (c) 2019 Kevin Gysberg

namespace Ngrok.AspNetCore
{
	public class NgrokOptions
	{
		/// <summary>
		/// Path to Ngrok executable. If not set, the execution directory and Windows PATH (if on Windows) will be searched
		/// </summary>
		public string NgrokPath { get; set; }

		/// <summary>
		/// Set to Ngrok profile specified in Ngrok config. Ngrok config file must be present to use this option
		/// See <see href="https://Ngrok.com/docs#config">https://Ngrok.com/docs#config</see> for details
		/// </summary>
		public string NgrokConfigProfile { get; set; }

		/// <summary>
		/// Sets the local URL Ngrok will proxy to. Must be http (not https) at this time. If not filled in, it will be populated automatically at runtime via the IWebHost features
		/// </summary>
		public string ApplicationHttpUrl { get; set; }

		/// <summary>
		/// Download Ngrok if not found in local directory or PATH. Defaults to true
		/// </summary>
		public bool DownloadNgrok { get; set; } = true;

		public int NgrokProcessStartTimeoutMs { get; set; } = 10000;
	}
}