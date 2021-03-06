﻿/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

namespace MSCorp.CrestMockWebApi.Models.Errors
{
	public class InvalidProperty
	{
		public Parameter parameters { get; set; }
		public string code { get; set; }
		public string error_code { get; set; }
		public string message { get; set; }
		public string object_type { get; set; }
	}
}