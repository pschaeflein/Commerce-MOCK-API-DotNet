using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using MSCorp.CrestMockWebApi.Models.Errors;

namespace MSCorp.CrestMockWebApi.Builders
{
    public class InvalidPropertyErrorResponseBuilder
    {
        public static InvalidProperty BuildErrorResponse(ModelStateDictionary modelStateDictionary)
        {
            return new InvalidProperty()
            {
                error_code = "InvalidProperty",
                message = "Phone Number doesn't match the pattern ^(1[ \\-\\/\\.]?)?(\\((\\d{3})\\)|(\\d{3}))[ \\-\\/\\.]?(\\d{3})[ \\-\\/\\.]?(\\d{4})$.",
                object_type = "Error",
                code = "4004",
                parameters = new Parameter()
                {
                    property_name = "phone_number"
                }
               
            };
        }
    }
}