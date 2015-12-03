/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

namespace Microsoft.Partner.CSP.Api.V1.Samples
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Web.Helpers;

	public static class Order
	{
		/// <summary>
		/// This method returns an order object populated from the console for a given customer
		/// </summary>
		/// <param name="customerCid">cid of the customer</param>
		/// <returns>order object</returns>
		public static dynamic PopulateOrderFromConsole(string customerCid)
		{
			var offerTypes = Enum.GetValues(typeof(OfferType));

			bool InvalidInput = false;
			Console.Clear();
			do
			{
				Console.WriteLine("Select Offer Group");
				Console.WriteLine("1.Azure");
				Console.WriteLine("2.IntuneAndOffice");

				Console.Write("Enter index [1...{0}]:", offerTypes.Length);
				string input = Console.ReadLine().Trim();
				switch (input)
				{
					case "1":
						return PopulateOrderFromConsoleForOfferType(OfferType.Azure, customerCid);
					case "2":
						return PopulateOrderFromConsoleForOfferType(OfferType.IntuneAndOffice, customerCid);
					default:
						Console.WriteLine("Invalid Input, Try Again");
						InvalidInput = true;
						break;
				}
			}
			while (InvalidInput);

			return null;
		}

		/// <summary>
		/// This method populates Order info by offer type from the console
		/// </summary>
		/// <param name="offerType">Offer type: Azure or IntuneAndOffice</param>
		/// <param name="customerCid">cid of the customer</param>
		/// <returns>order object</returns>
		private static dynamic PopulateOrderFromConsoleForOfferType(OfferType offerType, string customerCid)
		{
			GroupedOffers selectedGroupedOffers = OfferCatalog.Instance.GroupedOffersCollection.First(groupedOffers => groupedOffers.OfferType == offerType);
			dynamic order = new
			{
				line_items = new List<dynamic>(),
				recipient_customer_id = customerCid
			};

			int nrOfLineItems = 0;

			bool done = false;
			Console.Clear();
			do
			{
				Console.WriteLine("OfferType: {0}", offerType);
				foreach (var item in selectedGroupedOffers.Offers.Select((offer, index) => new { Offer = offer, Index = index }))
				{
					Console.WriteLine("{0}. {1}", item.Index + 1, item.Offer.Name);
				}

				Console.Write("\nSelect Offer (by index): ");
				string input = Console.ReadLine().Trim();
				int selectedIndex = -1;
				if (!int.TryParse(input, out selectedIndex))
				{
					done = false;
				}

				var selectedOffer = selectedGroupedOffers.Offers.ElementAt(selectedIndex - 1);

				bool validQuantity = false;

				do
				{
					Console.Write("\nQuantity {0} to {1}: ", selectedOffer.MinimumQuantity, selectedOffer.MaximumQuantity);
					input = Console.ReadLine().Trim();
					int quantity = 1;
					if (!int.TryParse(input, out quantity))
					{
						done = false;
					}

					if (quantity >= selectedOffer.MinimumQuantity && quantity <= selectedOffer.MaximumQuantity)
					{
						validQuantity = true;
					}

					Console.Write("\nFriendly Name (or hit Enter for none): ");
					input = Console.ReadLine().Trim();
					if (!string.IsNullOrWhiteSpace(input))
					{
						order.line_items.Add(new
						{
							//// has to be a unique number for each line item
							//// recommendation is to start with 0
							line_item_number = nrOfLineItems,

							//// this is the offer uri for the offer that is being purchased, refer to the excel sheet for this
							offer_uri = selectedOffer.Uri,

							//// This is the quantity for this offer
							quantity = quantity,

							//// This is friendly name
							friendlyname = input
						});
					}
					else
					{
						order.line_items.Add(new
						{
							//// has to be a unique number for each line item
							//// recommendation is to start with 0
							line_item_number = nrOfLineItems,

							//// this is the offer uri for the offer that is being purchased, refer to the excel sheet for this
							offer_uri = selectedOffer.Uri,

							//// This is the quantity for this offer
							quantity = quantity,
						});
					}
				}
				while (!validQuantity);

				Console.Write("\nDo you want to add another line item (y/n)? ");
				input = Console.ReadLine().Trim();

				switch (input)
				{
					case "y":
					case "Y":
						nrOfLineItems++;
						done = false;
						break;
					default:
						done = true;
						break;
				}
			}
			while (!done);

			return order;
		}

		/// <summary>
		/// This method retrieves all the orders placed for a customer by a reseller
		/// </summary>
		/// <param name="customerCid">cid of the customer</param>
		/// <param name="resellerCid">cid of the reseller</param>
		/// <param name="sa_Token">sales agent token</param>
		/// <returns>object that contains orders</returns>
		public static dynamic GetOrders(string customerCid, string resellerCid, string sa_Token)
		{
			//var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://api.cp.microsoft.com/{0}/orders?recipient_customer_id={1}", resellerCid, customerCid));
			var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://localhost:59080/{0}/orders?recipient_customer_id={1}", resellerCid, customerCid));

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
					var ordersResponse = Json.Decode(responseContent);

					foreach (var order in ordersResponse.items)
					{
						PrintOrder(order);
					}

					return ordersResponse;
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
		/// This method is used to place order on behalf of a customer by a reseller
		/// </summary>
		/// <param name="resellerCid">the cid of the reseller</param>
		/// <param name="order">new Order that can contain multiple line items</param>
		/// <param name="sa_Token">unexpired access token to call the partner apis</param>
		/// <returns>order information that has references to the subscription uris and entitlement uri for the line items</returns>
		public static dynamic PlaceOrder(dynamic order, string resellerCid, string sa_Token)
		{
			//var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://api.cp.microsoft.com/{0}/orders", resellerCid));
			var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://localhost:59080/{0}/orders", resellerCid));

			request.Method = "POST";
			request.ContentType = "application/json";
			request.Accept = "application/json";

			request.Headers.Add("api-version", "2015-03-31");
			request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
			request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
			request.Headers.Add("Authorization", "Bearer " + sa_Token);
			string content = Json.Encode(order);

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
					Utilities.PrintWebResponse((HttpWebResponse)response, responseContent);
					var placedOrder = Json.Decode(responseContent);
					PrintOrder(placedOrder);

					return placedOrder;
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
		/// This method prints the information in an order
		/// </summary>
		/// <param name="order">Order that was placed</param>
		public static void PrintOrder(dynamic order)
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("=========================================");
			Console.WriteLine("Order Information");
			Console.WriteLine("=========================================");
			Console.WriteLine("Id\t\t: {0}", order.id);
			Console.WriteLine("CustomerId\t: {0}", order.recipient_customer_id);
			Console.WriteLine("Etag\t\t: {0}", order.etag);
			Console.WriteLine("Created Date\t: {0}\n", order.creation_date);

			foreach (var line_item in order.line_items)
			{
				Console.WriteLine("LineItem {0}", line_item.line_item_number);
				Console.WriteLine("\tOfferUri\t\t: {0}", line_item.offer_uri);
				Console.WriteLine("\tQuantity\t\t: {0}", line_item.quantity);
				Console.WriteLine("\tSubscriptionUri\t\t: {0}", line_item.resulting_subscription_uri);
			}

			Console.WriteLine("=========================================");
			Console.ResetColor();
		}
	}
}