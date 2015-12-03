using System.Collections.Generic;

namespace MSCorp.CrestMockWebApi.Models.Orders
{
    public class PlaceOrderRequestData
    {
        public List<LineItem> line_items { get; set; }
        public string recipient_customer_id { get; set; }
    }
}