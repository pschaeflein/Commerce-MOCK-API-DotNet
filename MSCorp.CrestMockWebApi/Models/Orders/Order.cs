using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSCorp.CrestMockWebApi.Models.Common;

namespace MSCorp.CrestMockWebApi.Models.Orders
{
    public class Order
    {
        public string id { get; set; }
        public string recipient_customer_id { get; set; }
        public string etag { get; set; }
        public string creation_date { get; set; }
        public List<LineItem> line_items { get; set; }
        public string object_type { get; set; }
        public string contract_version { get; set; }
        public Links links { get; set; }
    }
}