/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MSCorp.CrestMockWebApi.Constants;

namespace MSCorp.CrestMockWebApi.Validators
{
	public class CountryValidatorAttribute : ValidationAttribute
	{

		public override bool IsValid(object value)
		{

			string strValue = (string)value;
			if (!string.IsNullOrEmpty(strValue))
			{
				return ValidationConstant.CountriesList.Any(i => String.Equals(i.code, strValue, StringComparison.CurrentCultureIgnoreCase));
			}
			return false;
		}
	}
}