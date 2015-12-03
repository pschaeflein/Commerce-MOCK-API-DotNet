using System;
using System.Web.Http;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Customer;

namespace MSCorp.CrestMockWebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("{resellercid}/customers/create-reseller-customer")]
        public CreateCustomerResponseData CreateResellerCustomer(Guid resellerCid, [FromBody] CreateCustomerRequestData createCustomerRequestData)
        {
            return _repository.CreateCustomer(createCustomerRequestData);
        }

        [HttpGet]
        [Route("customers/get-by-identity/")]
        public Customer GetByIdentity([FromUri]string provider, [FromUri]string type, [FromUri]Guid tid)
        {
            return _repository.GetCustomerByIdentity(new GetByIdentityRequest()
            {
                Id = tid
            });
        }

    }
}
