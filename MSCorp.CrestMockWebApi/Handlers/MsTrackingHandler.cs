/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using MSCorp.CrestMockWebApi.Services;

namespace MSCorp.CrestMockWebApi.Handlers
{
	public class MsTrackingHandler : DelegatingHandler
	{
		private const string TRACKING_HEADER = "x-ms-tracking-id";
		private const string CORRELATION_HEADER = "x-ms-correlation-id";
		private const string SESSION_HEADER = "x-ms-session-id";

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			string trackingId = "";
			string correlationId = "";
			string sessionId = Guid.NewGuid().ToString();

			if (request.Headers.Contains(CORRELATION_HEADER))
				correlationId = request.Headers.GetValues(CORRELATION_HEADER).First();

			if (request.Headers.Contains(TRACKING_HEADER))
				trackingId = request.Headers.GetValues(TRACKING_HEADER).First();

			// spin off on its own thread. We don't need to wait for this to execute
			//todo: change to local storage. 
			if (Convert.ToBoolean(WebConfigurationManager.AppSettings["AllowLogging"]))
			{
				TrackRequest(request, trackingId, correlationId, sessionId);
			}

			var response = await base.SendAsync(request, cancellationToken);

			response.Headers.Add(CORRELATION_HEADER, correlationId);
			response.Headers.Add(SESSION_HEADER, sessionId);

			return response;
		}

		private static void TrackRequest(HttpRequestMessage request, string trackingId, string correlationId, string sessionId)
		{
			FileLoggingService.LogInformational(request, trackingId, correlationId, sessionId);
		}
	}
}