using System.Collections.Generic;
using System.Linq;
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
			// Return first customer, regardless of the id that was sent.
			var customer = _customerTestData.First();
			// set value of id to what was sent
			customer.id = getByIdentityRequest.Id.ToString();
			return customer;
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