using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.ModelBinding;
using MSCorp.CrestMockWebApi.Models.Errors;

namespace MSCorp.CrestMockWebApi.Builders
{
    public static class MissingPropertyErrorResponseBuilder
    {
        public static MissingProperty BuildErrorResponse(ModelStateDictionary modelStateDictionary)
        {
            return new MissingProperty()
            {
                error_code = "MissingProperty",
                message = "A required property is missing in the payload.",
                object_type = "Error",
                parameters = new Parameter()
                {
                   property_name = GetErrorParams(modelStateDictionary)
                }
            };
        }

        private static string GetErrorParams(IEnumerable<KeyValuePair<string, ModelState>> modelStateDictionary)
        {
            return  (from state in modelStateDictionary
                    where state.Value.Errors.Count != 0
                    select state.Key.Split('.').Last()).First();
        }
    }
}