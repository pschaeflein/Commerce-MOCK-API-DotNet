/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.Collections.Generic;
using MSCorp.CrestMockWebApi.Models.Common;

namespace MSCorp.CrestMockWebApi.Models.Orders
{
	public class PlaceOrderResponseData
	{
		public string id { get; set; }
		public string recipient_customer_id { get; set; }
		public string etag { get; set; }
		public string creation_date { get; set; }
		public List<LineItem> line_items { get; set; }
		public string object_type { get; set; }
		public string contract_version { get; set; }
		public Links links { get; set; }
	}
}