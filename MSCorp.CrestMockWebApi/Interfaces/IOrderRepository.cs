using System;
using MSCorp.CrestMockWebApi.Models.Orders;

namespace MSCorp.CrestMockWebApi.Interfaces
{
    public interface IOrderRepository
    {
        PlaceOrderResponseData PlaceOrder();
        Order GetOrderById(Guid id);
    }
}