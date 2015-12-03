/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.ComponentModel.DataAnnotations;
using MSCorp.CrestMockWebApi.Models.Profiles;

namespace MSCorp.CrestMockWebApi.Models.Customer
{
	public class CreateCustomerRequestData
	{
		[Required]
		public string domain_prefix { get; set; }
		[Required]
		public string user_name { get; set; }
		[Required]
		public string password { get; set; }
		[Required]
		public Profile profile { get; set; }
	}
}