using System.Collections.Generic;
using MSCorp.CrestMockWebApi.Models.Common;

namespace MSCorp.CrestMockWebApi.Models.Subscription
{
    public class SingleSubscriptionResponseData
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string creation_date { get; set; }
        public string effective_start_date { get; set; }
        public string commitment_end_date { get; set; }
        public int quantity { get; set; }
        public string state { get; set; }
        public string etag { get; set; }
        public List<object> suspension_reasons { get; set; }
        public string offer_uri { get; set; }
        public string object_type { get; set; }
        public string contract_version { get; set; }
        public Links links { get; set; }
    }
}