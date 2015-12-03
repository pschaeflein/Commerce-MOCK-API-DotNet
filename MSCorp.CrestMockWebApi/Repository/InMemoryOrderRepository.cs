using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Query.SemanticAst;
using MSCorp.CrestMockWebApi.Constants;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Orders;
using MSCorp.CrestMockWebApi.Services;

namespace MSCorp.CrestMockWebApi.Repository
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly PlaceOrderResponseData _testData;
        private readonly List<Order> _orderTestData; 

        public InMemoryOrderRepository()
        {
            _testData = GetExamplePlaceOrderResponseData();
            _orderTestData = GetExampleOrderData();
        }

        public PlaceOrderResponseData PlaceOrder()
        {
            return _testData;
        }

        public Order GetOrderById(Guid id)
        {
            return _orderTestData.Find(i => i.id == id.ToString());
        }

        private static PlaceOrderResponseData GetExamplePlaceOrderResponseData()
        {
            return JsonObjectExtractorService.ExtractJsonObjectFromFile<PlaceOrderResponseData>
                (ResourcePathConstant.PlaceOrderResponseDataPath);

        }

        private static List<Order> GetExampleOrderData()
        {
            return JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<Order>(
                 ResourcePathConstant.GetOrderResponseDataPath);
        }
    }
}