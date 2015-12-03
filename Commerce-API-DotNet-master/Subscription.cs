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
	using System.Web.Helpers;

	public static class Subscription
	{
		/// <summary>
		/// This method is to retrieve the subscriptions of a customer bought from the reseller
		/// </summary>
		/// <param name="customerCid">cid of the customer</param>
		/// <param name="resellerCid">cir of the reseller</param>
		/// <param name="sa_Token">sales agent token</param>
		/// <returns>object that contains all of the subscriptions</returns>
		public static dynamic GetSubscriptions(string customerCid, string resellerCid, string sa_Token)
		{
			//var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://api.cp.microsoft.com/{0}/subscriptions?recipient_customer_id={1}", resellerCid, customerCid));
			var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://localhost:59080/{0}/subscriptions?recipient_customer_id={1}", resellerCid, customerCid));

			request.Method = "GET";
			request.Accept = "application/json";

			request.Headers.Add("api-version", "2015-03-31");
			request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
			request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
			request.Headers.Add("Authorization", "Bearer " + sa_Token);

			try
			{
				Utilities.PrintWebRequest(request, string.Empty);

				var response = request.GetResponse();
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					var responseContent = reader.ReadToEnd();
					Utilities.PrintWebResponse((HttpWebResponse)response, responseContent);
					var subscriptionsResponse = Json.Decode(responseContent);

					foreach (var subscription in subscriptionsResponse.items)
					{
						PrintSubscription(subscription);
					}

					return subscriptionsResponse;
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
		/// This method returns the subscription given the subscription id
		/// </summary>
		/// <param name="subscriptionId">subscription id</param>
		/// <param name="resellerCid">cid of the reseller</param>
		/// <param name="sa_Token">sales agent token</param>
		/// <returns>subscription object</returns>
		public static dynamic GetSubscriptionById(string subscriptionId, string resellerCid, string sa_Token)
		{
			//var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://api.cp.microsoft.com/{0}/subscriptions/{1}", resellerCid, subscriptionId));
			var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://localhost:59080/{0}/subscriptions/{1}", resellerCid, subscriptionId));

			request.Method = "GET";
			request.Accept = "application/json";

			request.Headers.Add("api-version", "2015-03-31");
			request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
			request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
			request.Headers.Add("Authorization", "Bearer " + sa_Token);

			try
			{
				Utilities.PrintWebRequest(request, string.Empty);

				var response = request.GetResponse();
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					var responseContent = reader.ReadToEnd();
					Utilities.PrintWebResponse((HttpWebResponse)response, responseContent);
					var subscription = Json.Decode(responseContent);
					PrintSubscription(subscription);

					return subscription;
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
		/// This method is to print the subscription information
		/// </summary>
		/// <param name="subscription">subscription object</param>
		private static void PrintSubscription(dynamic subscription)
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("=========================================");
			Console.WriteLine("Subscription Information");
			Console.WriteLine("=========================================");
			Console.WriteLine("Id\t\t: {0}", subscription.id);
			Console.WriteLine("Order Id\t: {0}", subscription.order_id);
			Console.WriteLine("Etag\t\t: {0}", subscription.etag);
			Console.WriteLine("Offer Uri\t:{0}", subscription.offer_uri);
			Console.WriteLine("Friendly Name\t: {0}", subscription.friendly_name);

			Console.WriteLine("\nImportant Dates");
			Console.WriteLine("\tCreated Date\t\t: {0}", subscription.creation_date);
			Console.WriteLine("\tEffective Start Date\t: {0}", subscription.effective_start_date);
			Console.WriteLine("\tCommitment End Date\t: {0}", subscription.commitment_end_date);

			Console.WriteLine("\nLife Cycle");
			Console.WriteLine("\tState\t\t\t: {0}", subscription.state);
			foreach (var suspensionReason in subscription.suspension_reasons)
			{
				Console.WriteLine("\tSuspended Reason\t: {0}", suspensionReason);
			}

			Console.WriteLine("=========================================");
			Console.ResetColor();

		}
	}
}
