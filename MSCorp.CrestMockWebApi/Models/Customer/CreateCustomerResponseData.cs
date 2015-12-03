/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using MSCorp.CrestMockWebApi.Models.Profiles;
using Newtonsoft.Json;

namespace MSCorp.CrestMockWebApi.Models.Customer
{
	public class CreateCustomerResponseData
	{
		public string domain_prefix { get; set; }
		public string user_name { get; set; }
		public string password { get; set; }
		public Customer customer { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Profile profile { get; set; }
	}
}