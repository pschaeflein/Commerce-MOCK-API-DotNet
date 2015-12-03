/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.Web.Http.ModelBinding;
using MSCorp.CrestMockWebApi.Models.Errors;

namespace MSCorp.CrestMockWebApi.Builders
{
	public class InvalidInputErrorResponseBuilder
	{
		public static InvalidInput BuildErrorResponse(ModelStateDictionary modelStateDictionary)
		{
			return new InvalidInput()
			{
				code = "10008",
				error_code = "InvalidInput",
				message =
							"1 failed (out of 15 evaluations)\n  Failed:\n    Reseller Customer Identities request must have domain prefix.\n  Passed:\n",
				object_type = "Error"
			};

		}
	}
}