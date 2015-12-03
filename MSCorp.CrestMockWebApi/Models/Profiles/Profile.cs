using MSCorp.CrestMockWebApi.Models.Common;
using MSCorp.CrestMockWebApi.Models.Customer;

namespace MSCorp.CrestMockWebApi.Models.Profiles
{
    public class Profile
    {
        public string email { get; set; }
        public string company_name { get; set; }
        public string language { get; set; }
        public string customer_id { get; set; }
        public string id { get; set; }
        public string snapshot_id { get; set; }
        public string type { get; set; }
        public DefaultAddress default_address { get; set; }
        public string culture { get; set; }
        public string etag { get; set; }
        public Links links { get; set; }
        public string object_type { get; set; }
    }
}