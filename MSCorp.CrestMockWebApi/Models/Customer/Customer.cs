/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using MSCorp.CrestMockWebApi.Models.Common;
using Newtonsoft.Json;

namespace MSCorp.CrestMockWebApi.Models.Customer
{
	public class Customer
	{
		public string id { get; set; }
		public Identity identity { get; set; }
		public bool is_test { get; set; }
		public Links links { get; set; }
		public string object_type { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string contract_version { get; set; }
	}
}