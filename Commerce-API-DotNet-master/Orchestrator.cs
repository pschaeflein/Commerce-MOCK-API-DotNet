/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;

namespace Microsoft.Partner.CSP.Api.V1.Samples
{
	internal class Orchestrator
	{
		/// <summary>
		/// Create a customer
		/// </summary>
		/// <param name="defaultDomain">default domain of the reseller</param>
		/// <param name="appId">appid that is registered for this application in Partner Center</param>
		/// <param name="key">Key for this application in Partner Center</param>
		/// <param name="resellerMicrosoftId">Microsoft Id of the reseller</param>
		public static void CreateCustomer(string defaultDomain, string appId, string key, string resellerMicrosoftId)
		{
			// Get Active Directory token first
			AuthorizationToken adAuthorizationToken = Reseller.GetAD_Token(defaultDomain, appId, key);

			// Using the ADToken get the sales agent token
			AuthorizationToken saAuthorizationToken = Reseller.GetSA_Token(adAuthorizationToken);

			// Get the Reseller Cid, you can cache this value
			string resellerCid = Reseller.GetCid(resellerMicrosoftId, saAuthorizationToken.AccessToken);

			// Get input from the console application for creating a new customer
			var customer = Customer.PopulateCustomerFromConsole();

			// This is the created customer object that contains the cid, the microsoft tenant id etc
			var createdCustomer = Customer.CreateCustomer(customer, resellerCid, saAuthorizationToken.AccessToken);
		}

		/// <summary>
		/// Get all subscriptions placed by the reseller for the customer
		/// </summary>
		/// <param name="defaultDomain">default domain of the reseller</param>
		/// <param name="appId">appid that is registered for this application in Partner Center</param>
		/// <param name="key">Key for this application in Partner Center</param>
		/// <param name="customerMicrosoftId">Microsoft Id of the customer</param>
		/// <param name="resellerMicrosoftId">Microsoft Id of the reseller</param>
		/// <returns>object that contains all of the subscriptions</returns>
		public static dynamic GetSubscriptions(string defaultDomain, string appId, string key,
				string customerMicrosoftId, string resellerMicrosoftId)
		{
			// Get Active Directory token first
			AuthorizationToken adAuthorizationToken = Reseller.GetAD_Token(defaultDomain, appId, key);

			// Using the ADToken get the sales agent token
			AuthorizationToken saAuthorizationToken = Reseller.GetSA_Token(adAuthorizationToken);

			// Get the Reseller Cid, you can cache this value
			string resellerCid = Reseller.GetCid(resellerMicrosoftId, saAuthorizationToken.AccessToken);

			// You can cache this value too
			var customerCid = Customer.GetCustomerCid(customerMicrosoftId, resellerMicrosoftId,
					saAuthorizationToken.AccessToken);

			return Subscription.GetSubscriptions(customerCid, resellerCid, saAuthorizationToken.AccessToken);
		}

		/// <summary>
		/// Get all orders placed by the reseller for this customer
		/// </summary>
		/// <param name="defaultDomain">default domain of the reseller</param>
		/// <param name="appId">appid that is registered for this application in Partner Center</param>
		/// <param name="key">Key for this application in Partner Center</param>
		/// <param name="customerMicrosoftId">Microsoft Id of the customer</param>
		/// <param name="resellerMicrosoftId">Microsoft Id of the reseller</param>
		/// <returns>object that contains orders</returns>
		public static dynamic GetOrders(string defaultDomain, string appId, string key, string customerMicrosoftId,
				string resellerMicrosoftId)
		{
			// Get Active Directory token first
			AuthorizationToken adAuthorizationToken = Reseller.GetAD_Token(defaultDomain, appId, key);

			// Using the ADToken get the sales agent token
			AuthorizationToken saAuthorizationToken = Reseller.GetSA_Token(adAuthorizationToken);

			// Get the Reseller Cid, you can cache this value
			string resellerCid = Reseller.GetCid(resellerMicrosoftId, saAuthorizationToken.AccessToken);

			// You can cache this value too
			var customerCid = Customer.GetCustomerCid(customerMicrosoftId, resellerMicrosoftId,
					saAuthorizationToken.AccessToken);

			return Order.GetOrders(customerCid, resellerCid, saAuthorizationToken.AccessToken);
		}

		/// <summary>
		/// Create an Order
		/// </summary>
		/// <param name="defaultDomain">default domain of the reseller</param>
		/// <param name="appId">appid that is registered for this application in Partner Center</param>
		/// <param name="key">Key for this application in Partner Center</param>
		/// <param name="customerMicrosoftId">Microsoft Id of the customer</param>
		/// <param name="resellerMicrosoftId">Microsoft Id of the reseller</param>
		public static void CreateOrder(string defaultDomain, string appId, string key, string customerMicrosoftId,
				string resellerMicrosoftId)
		{
			// Get Active Directory token first
			AuthorizationToken adAuthorizationToken = Reseller.GetAD_Token(defaultDomain, appId, key);

			// Using the ADToken get the sales agent token
			AuthorizationToken saAuthorizationToken = Reseller.GetSA_Token(adAuthorizationToken);

			// Get the Reseller Cid, you can cache this value
			string resellerCid = Reseller.GetCid(resellerMicrosoftId, saAuthorizationToken.AccessToken);

			// You can cache this value too
			var customerCid = Customer.GetCustomerCid(customerMicrosoftId, resellerMicrosoftId,
					saAuthorizationToken.AccessToken);

			// Populate a multi line item order
			var existingCustomerOrder = Order.PopulateOrderFromConsole(customerCid);
			// Place the order and subscription uri and entitlement uri are returned per each line item
			var existingCustomerPlacedOrder = Order.PlaceOrder(existingCustomerOrder, resellerCid,
					saAuthorizationToken.AccessToken);
			foreach (var line_Item in existingCustomerPlacedOrder.line_items)
			{
				Console.WriteLine("Subscription: {0}", line_Item.resulting_subscription_uri);
			}
		}
	}
}