/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

namespace Microsoft.Partner.CSP.Api.V1.Samples
{
	using System;
	using System.IO;
	using System.Net;
	using System.Web;
	using System.Web.Helpers;

	public static class Reseller
	{
		/// <summary>
		/// This method returns the CID of the reseller given the Microsoft ID and the sales agent token
		/// </summary>
		/// <param name="microsoftId">Microsoft ID of the reseller</param>
		/// <param name="sa_Token">sales agent token of the reseller</param>
		/// <returns>CID of the reseller</returns>
		public static string GetCid(string microsoftId, string sa_Token)
		{
			return GetResellerCid(microsoftId, sa_Token);
		}

		/// <summary>
		/// Get the latest sales agent token given the AD Authorization Token
		/// </summary>
		/// <param name="adAuthorizationToken">AD Authorization Token</param>
		/// <param name="saAuthorizationToken">Sales agent authorization token, can be null</param>
		/// <returns>Latest sales agent token</returns>
		public static AuthorizationToken GetSA_Token(AuthorizationToken adAuthorizationToken, AuthorizationToken saAuthorizationToken = null)
		{
			if (saAuthorizationToken == null || (saAuthorizationToken != null && saAuthorizationToken.IsNearExpiry()))
			{
				//// Refresh the token on one of two conditions
				//// 1. If the token has never been retrieved
				//// 2. If the token is near expiry

				var saToken = GetSA_Token(adAuthorizationToken.AccessToken);
				saAuthorizationToken = new AuthorizationToken(saToken.access_token, Convert.ToInt64(saToken.expires_in));
			}

			return saAuthorizationToken;
		}

		/// <summary>
		/// Get the latest AD token given the reseller domain and client credentials
		/// </summary>
		/// <param name="domain">domain of the reseller</param>
		/// <param name="clientId">clientID of the application</param>
		/// <param name="clientSecret">client secret of the application, also refered to as key</param>
		/// <param name="adAuthorizationToken">ad authorization token, can be null</param>
		/// <returns>Latest AD Authorization token</returns>
		public static AuthorizationToken GetAD_Token(string domain, string clientId, string clientSecret, AuthorizationToken adAuthorizationToken = null)
		{
			if (adAuthorizationToken == null || (adAuthorizationToken != null && adAuthorizationToken.IsNearExpiry()))
			{
				//// Refresh the token on one of two conditions
				//// 1. If the token has never been retrieved
				//// 2. If the token is near expiry
				//var adToken = GetADToken(domain, clientId, clientSecret);
				//adAuthorizationToken = new AuthorizationToken(adToken.access_token, Convert.ToInt64(adToken.expires_in));

				// MOCK API simulates tokens
				var adToken = Json.Decode("{\"access_token\":\"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ijd3YzlFTnVyWGZaRWt5VkxfZkM5cTZpeHNMRSJ9.eyJpc3MiOiJ1cm46Y3BzdHMiLCJhdWQiOiJ1cm46Y3BzdmM6Y2E6ODZmZDM1YjktMzhhMi00MTNjLWE5YjEtMzNiYjc1ZjNiZDRmIiwibmJmIjoxNDM4MzY5OTIyLCJleHAiOjE0MzgzNzA4MjIsIm5hbWVpZCI6IjkwNDg1QTVFLUQyRTMtNDU5RC04NzFFLUM1OTVBQzhBMEUzNiIsInVuaXF1ZV9uYW1lIjoiVGhpcmRQYXJ0eUFwcCIsIklkZW50aWZpZXJJZCI6Ijg2ZmQzNWI5LTM4YTItNDEzYy1hOWIxLTMzYmI3NWYzYmQ0ZiIsImFwcGlkIjoiNGIzZGFkOWMtZTUxNi00NzZmLTlmOTItMmQ3OWRhOWZjYmNmIiwiQ2FpZCI6Ijg2ZmQzNWI5LTM4YTItNDEzYy1hOWIxLTMzYmI3NWYzYmQ0ZiIsIklzVGVzdCI6IlRydWUiLCJyb2xlIjoiU2FsZXNBZ2VudCIsImFjdG9ydCI6ImV5SjBlWEFpT2lKS1YxUWlMQ0poYkdjaU9pSnViMjVsSW4wLmV5SmhkV1FpT2lKb2RIUndjem92TDJkeVlYQm9MbmRwYm1SdmQzTXVibVYwSWl3aWFYTnpJam9pYUhSMGNITTZMeTl6ZEhNdWQybHVaRzkzY3k1dVpYUXZPRFptWkRNMVlqa3RNemhoTWkwME1UTmpMV0U1WWpFdE16TmlZamMxWmpOaVpEUm1MeUlzSW1saGRDSTZJakUwTXpnek5qazJNakFpTENKdVltWWlPaUl4TkRNNE16WTVOakl3SWl3aVpYaHdJam9pTVRRek9ETTNNelV5TUNJc0luWmxjaUk2SWpFdU1DSXNJblJwWkNJNklqZzJabVF6TldJNUxUTTRZVEl0TkRFell5MWhPV0l4TFRNelltSTNOV1l6WW1RMFppSXNJbTlwWkNJNklqTm1ZekUzT1Rsa0xXWTNZalV0TkRsaFl5MDVZekl3TFdGbU1qVTVNRGt4WkRnNU9DSXNJbWxrY0NJNkltaDBkSEJ6T2k4dmMzUnpMbmRwYm1SdmQzTXVibVYwTHpnMlptUXpOV0k1TFRNNFlUSXROREV6WXkxaE9XSXhMVE16WW1JM05XWXpZbVEwWmk4aUxDSmhjSEJwWkdGamNpSTZJakVpTENKdVlXMWxhV1FpT2lJNU1EUTROVUUxUlMxRU1rVXpMVFExT1VRdE9EY3hSUzFETlRrMVFVTTRRVEJGTXpZaUxDSjFibWx4ZFdWZmJtRnRaU0k2SWxSb2FYSmtVR0Z5ZEhsQmNIQWlMQ0poY0hCcFpDSTZJalJpTTJSaFpEbGpMV1UxTVRZdE5EYzJaaTA1WmpreUxUSmtOemxrWVRsbVkySmpaaUlzSWtsa1pXNTBhV1pwWlhKSlpDSTZJamcyWm1Rek5XSTVMVE00WVRJdE5ERXpZeTFoT1dJeExUTXpZbUkzTldZelltUTBaaUlzSW5KdmJHVWlPaUpCWTNSUGJsSmxjMlZzYkdWeVEyaGhibTVsYkNKOS4ifQ.GHg0RLBo-rdX4CINR6N54rL5m741df4QWh_aRyQW2gIbtqpOHZlJ-3CYBHtgy-rcfMFXia2N1BppBFZ4rGLuqRtyyStUDbWMoMVyMU2LwXyUyNVtiEg5K7YY9KaZLqzEgMxe5VM9HLwC0evM6yyjXiFABr7mTJe4OOpJcPXDAvh2DKjA5reqf4y9HODth1GLpN0bsNNTcGzeOP2rsEsEwMIaUeP0mBE60fVQ3lR5yt1_gZlUObi_CIC_nAnW3l5Je1JtWsYzq4AlLhnGrfsywi5V2TQLp6Wo6w0x6btZNBEExOryS691flzL6Cyr_PUXKSH0_-rgEu_s3zWvSZJoqw\",\"token_type\":\"bearer\",\"expires_in\":900}");
				adAuthorizationToken = new AuthorizationToken(adToken.access_token, Convert.ToInt64(adToken.expires_in));
			}

			return adAuthorizationToken;
		}

		/// <summary>
		/// Given the reseller domain, clientid and clientsecret of the app, this method helps to retrieve the AD token
		/// </summary>
		/// <param name="resellerDomain">domain of the reseller including .onmicrosoft.com</param>
		/// <param name="clientId">AppId from the azure portal registered for this app</param>
		/// <param name="clientSecret">Secret from the azure portal registered for this app</param>
		/// <returns>this is the authentication token object that contains access_token, expiration time, can be used to get the authorization token from a resource</returns>
		private static dynamic GetADToken(string resellerDomain, string clientId, string clientSecret)
		{
			var request = WebRequest.Create(string.Format("https://login.microsoftonline.com/{0}/oauth2/token", resellerDomain));

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			string content = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}", clientId, HttpUtility.UrlEncode(clientSecret), HttpUtility.UrlEncode("https://graph.windows.net"));

			using (var writer = new StreamWriter(request.GetRequestStream()))
			{
				writer.Write(content);
			}

			try
			{
				Utilities.PrintWebRequest((HttpWebRequest)request, "<snipped>");

				var response = request.GetResponse();
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					var responseContent = reader.ReadToEnd();
					var adResponse = Json.Decode(responseContent);
					Utilities.PrintWebResponse((HttpWebResponse)response, "<snipped>");
					return adResponse;
				}

			}
			catch (WebException webException)
			{
				if (webException.Response != null)
				{
					using (var reader = new StreamReader(webException.Response.GetResponseStream()))
					{
						var responseContent = reader.ReadToEnd();
						Utilities.PrintErrorResponse((HttpWebResponse)webException.Response, responseContent);
					}
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// Given the ad token this method retrieves the sales agent token for accessing any of the partner apis
		/// </summary>
		/// <param name="adToken">this is the access_token we get from AD</param>
		/// <returns>the sales agent token object which contains access_token, expiration duration</returns>
		private static dynamic GetSA_Token(string adToken)
		{
			//var request = (HttpWebRequest)WebRequest.Create("https://api.cp.microsoft.com/my-org/tokens");
			var request = (HttpWebRequest)WebRequest.Create("http://localhost:59080/my-org/tokens");

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.Accept = "application/json";

			request.Headers.Add("api-version", "2015-03-31");
			request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
			request.Headers.Add("Authorization", "Bearer " + adToken);
			string content = "grant_type=client_credentials";

			using (var writer = new StreamWriter(request.GetRequestStream()))
			{
				writer.Write(content);
			}

			try
			{
				Utilities.PrintWebRequest(request, content);

				var response = request.GetResponse();
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					var responseContent = reader.ReadToEnd();
					Utilities.PrintWebResponse((HttpWebResponse)response, "<snipped>");
					return Json.Decode(responseContent);
				}
			}
			catch (WebException webException)
			{
				using (var reader = new StreamReader(webException.Response.GetResponseStream()))
				{
					var responseContent = reader.ReadToEnd();
					Utilities.PrintErrorResponse((HttpWebResponse)webException.Response, responseContent);
				}
			}
			return string.Empty;
		}

		/// <summary>
		/// This method is used to retrieve the reseller cid given the reseller microsoft id, and is used to perform any transactions by the reseller
		/// </summary>
		/// <param name="resellerMicrosoftId">Microsoft ID of the reseller</param>
		/// <param name="accessToken">unexpired access token to call the partner apis</param>
		/// <returns>Reseller cid that is required to use the partner apis</returns>
		private static string GetResellerCid(string resellerMicrosoftId, string accessToken)
		{
			//var request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.cp.microsoft.com/customers/get-by-identity?provider=AAD&type=tenant&tid={0}", resellerMicrosoftId));
			var request = (HttpWebRequest)WebRequest.Create(string.Format("http://localhost:59080/customers/get-by-identity?provider=AAD&type=tenant&tid={0}", resellerMicrosoftId));

			request.Method = "GET";
			request.Accept = "application/json";

			request.Headers.Add("api-version", "2015-03-31");
			request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
			request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
			request.Headers.Add("Authorization", "Bearer " + accessToken);

			try
			{
				Utilities.PrintWebRequest(request, string.Empty);

				var response = request.GetResponse();
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					var responseContent = reader.ReadToEnd();
					var crestResponse = Json.Decode(responseContent);
					Utilities.PrintWebResponse((HttpWebResponse)response, responseContent);
					return crestResponse.id;
				}
			}
			catch (WebException webException)
			{
				using (var reader = new StreamReader(webException.Response.GetResponseStream()))
				{
					var responseContent = reader.ReadToEnd();
					Utilities.PrintErrorResponse((HttpWebResponse)webException.Response, responseContent);
				}
			}
			return string.Empty;
		}
	}
}
