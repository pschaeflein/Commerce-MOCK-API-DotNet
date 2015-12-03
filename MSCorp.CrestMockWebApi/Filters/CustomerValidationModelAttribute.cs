using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MSCorp.CrestMockWebApi.Builders;

namespace MSCorp.CrestMockWebApi.Filters
{
    public class CustomerValidationModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {

                var stateList = (from state in modelState
                         where state.Value.Errors.Count != 0
                         select state.Key.Split('.').Last()).ToList();


                if (stateList.Contains("resellerCid")) return;
                             
                if(stateList.Contains("domain_prefix"))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.BadRequest, InvalidInputErrorResponseBuilder.BuildErrorResponse(modelState));

                } 
                else if (stateList.Contains("phone_number"))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.BadRequest, InvalidPropertyErrorResponseBuilder.BuildErrorResponse(modelState));

                } 
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                         HttpStatusCode.BadRequest, MissingPropertyErrorResponseBuilder.BuildErrorResponse(modelState));
                }


            }
        }
    }
}
