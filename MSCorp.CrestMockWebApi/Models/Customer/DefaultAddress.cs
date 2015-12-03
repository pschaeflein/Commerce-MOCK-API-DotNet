using System.ComponentModel.DataAnnotations;
using MSCorp.CrestMockWebApi.Models.Common;
using MSCorp.CrestMockWebApi.Validators;

namespace MSCorp.CrestMockWebApi.Models.Customer
{
    public class DefaultAddress
    {
        public string id { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        [CountryCityValidator]
        public string city { get; set; }
        [CountryStateValidator]
        public string region { get; set; }
        [CountryZipCodeValidator]
        public string postal_code { get; set; }
        [CountryValidator]
        public string country { get; set; }
        [CountryPhoneNumberValidatorAttribute]
        public string phone_number { get; set; }
        public Links links { get; set; }
        public string object_type { get; set; }
        public string contract_version { get; set; }
    }
}