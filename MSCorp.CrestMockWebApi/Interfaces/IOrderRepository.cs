/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using MSCorp.CrestMockWebApi.Models.Orders;
using System.Collections.Generic;

namespace MSCorp.CrestMockWebApi.Interfaces
{
	public interface IOrderRepository
	{
		PlaceOrderResponseData PlaceOrder();
		Order GetOrderById(Guid id);
		List<Order> GetOrders();
	}
}