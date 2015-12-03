/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

namespace MSCorp.CrestMockWebApi.Models.Orders
{
	public class LineItem
	{
		public int line_item_number { get; set; }
		public string offer_uri { get; set; }
		public int quantity { get; set; }
		public string resulting_subscription_uri { get; set; }
	}
}