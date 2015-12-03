using MSCorp.CrestMockWebApi.Models.Customer;

namespace MSCorp.CrestMockWebApi.Builders
{
    public static class CreateCustomerResponseBuilder
    {
        public static CreateCustomerResponseData CreateCustomerResponseData(CreateCustomerRequestData createCustomerRequestData,
                                                                                CreateCustomerResponseData createCustomerResponseData)
        {
            return MapInputValuesToGenericCustomerResponse(createCustomerRequestData, createCustomerResponseData);
        }

        private static CreateCustomerResponseData MapInputValuesToGenericCustomerResponse(CreateCustomerRequestData createCustomerRequestData,
            CreateCustomerResponseData createCustomerResponseData)
        {
            createCustomerResponseData.profile.company_name = createCustomerRequestData.profile.company_name;
            createCustomerResponseData.profile.default_address.first_name = createCustomerRequestData.profile.default_address.first_name;
            createCustomerResponseData.profile.default_address.last_name =  createCustomerRequestData.profile.default_address.last_name;
            createCustomerResponseData.profile.email = createCustomerRequestData.profile.email;
            createCustomerResponseData.profile.default_address.address_line1 =createCustomerRequestData.profile.default_address.address_line1;
            createCustomerResponseData.profile.default_address.address_line2 = createCustomerRequestData.profile.default_address.address_line2;
            createCustomerResponseData.profile.default_address.city = createCustomerRequestData.profile.default_address.city;
            createCustomerResponseData.profile.default_address.region = createCustomerRequestData.profile.default_address.region;
            createCustomerResponseData.profile.default_address.postal_code = createCustomerRequestData.profile.default_address.postal_code;
            createCustomerResponseData.profile.default_address.phone_number = createCustomerRequestData.profile.default_address.postal_code;
            createCustomerResponseData.profile.default_address.country = createCustomerRequestData.profile.default_address.country;
            return createCustomerResponseData;
        }
    }
}