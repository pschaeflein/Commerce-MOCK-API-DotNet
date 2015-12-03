/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

namespace MSCorp.CrestMockWebApi.Models.Customer
{
	public class Identity
	{
		public string provider { get; set; }
		public string type { get; set; }
		public Data data { get; set; }
	}
}