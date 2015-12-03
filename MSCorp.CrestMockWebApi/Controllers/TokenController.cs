using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Tokens;

namespace MSCorp.CrestMockWebApi.Controllers
{
    public class TokenController : ApiController
    {
        private readonly ITokenRepository _repository;

        public TokenController(ITokenRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("my-org/tokens")]
        public TokenReponseData GetSaleAgentToken()
        {
            var ADtoken = Request.Headers.Authorization.Parameter;
            return _repository.GetToken(ADtoken);
        }


    }
}
