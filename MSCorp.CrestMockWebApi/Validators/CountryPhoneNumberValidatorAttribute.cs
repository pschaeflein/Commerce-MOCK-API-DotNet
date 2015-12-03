/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MSCorp.CrestMockWebApi.Models.Customer;

namespace MSCorp.CrestMockWebApi.Validators
{
	public class CountryPhoneNumberValidatorAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var requestData = validationContext.ObjectInstance as DefaultAddress;
			if (requestData == null || requestData.country != "US") return ValidationResult.Success;

			string strValue = (string)value;
			if (!string.IsNullOrEmpty(strValue))
			{

				Regex regex = new Regex(@"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$");
				Match match = regex.Match(strValue);
				if (match.Success)
				{
					return ValidationResult.Success;
				}
			}
			return new ValidationResult("Incorrect US Format");
		}
	}
}

