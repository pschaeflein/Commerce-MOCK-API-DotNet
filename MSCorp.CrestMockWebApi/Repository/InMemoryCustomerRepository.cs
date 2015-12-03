using System.Collections.Generic;
using MSCorp.CrestMockWebApi.Builders;
using MSCorp.CrestMockWebApi.Constants;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Customer;
using MSCorp.CrestMockWebApi.Services;

namespace MSCorp.CrestMockWebApi.Repository
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {

        private readonly CreateCustomerResponseData _createCustomerResponseTestData;
        private readonly List<Customer> _customerTestData; 

        public InMemoryCustomerRepository()
        {
            _createCustomerResponseTestData = GetExampleCreateCustomerResponseData();
            _customerTestData = GetExampleCustomerData();
        }

        public CreateCustomerResponseData CreateCustomer(CreateCustomerRequestData createCustomerRequestData)
        {
            return CreateCustomerResponseBuilder.CreateCustomerResponseData(createCustomerRequestData, _createCustomerResponseTestData);
        }

        public Customer GetCustomerByIdentity(GetByIdentityRequest getByIdentityRequest)
        {
           return _customerTestData.Find(i => i.id == getByIdentityRequest.Id.ToString());
        }

        private static List<Customer> GetExampleCustomerData()
        {
           return JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<Customer>(
                ResourcePathConstant.GetCustomerResponseDataPath);
        }

        private static CreateCustomerResponseData GetExampleCreateCustomerResponseData()
        {
            return JsonObjectExtractorService.ExtractJsonObjectFromFile<CreateCustomerResponseData>(
                ResourcePathConstant.CreateCustomerResponseDataPath);         
        }


    }
}