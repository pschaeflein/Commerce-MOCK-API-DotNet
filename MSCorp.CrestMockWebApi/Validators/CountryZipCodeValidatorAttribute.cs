using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MSCorp.CrestMockWebApi.Constants;
using MSCorp.CrestMockWebApi.Models.Customer;

namespace MSCorp.CrestMockWebApi.Validators
{
    public class CountryZipCodeValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var requestData = validationContext.ObjectInstance as DefaultAddress;
            if (requestData == null || requestData.country != "US") return ValidationResult.Success;

            string strValue = (string)value;
            if (!string.IsNullOrEmpty(strValue))
            {
                bool exists = ValidationConstant.UsaZipCodesList.Any(i => String.Equals(i, strValue, StringComparison.CurrentCultureIgnoreCase));
                return exists ? ValidationResult.Success : new ValidationResult("US Zip doesn't Exist");
            }
            return new ValidationResult("US Zip doesn't exist");
        }
    }
}