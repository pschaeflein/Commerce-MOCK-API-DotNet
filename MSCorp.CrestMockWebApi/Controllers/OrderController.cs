/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using System.Web.Http;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Orders;

namespace MSCorp.CrestMockWebApi.Controllers
{
	public class OrderController : ApiController
	{
		private readonly IOrderRepository _repository;

		public OrderController(IOrderRepository repository)
		{
			_repository = repository;
		}

		[HttpPost]
		[Route("{resellercid}/orders")]
		public PlaceOrderResponseData PlaceOrder(Guid resellerCid, [FromBody] PlaceOrderRequestData placeOrderRequestData)
		{
			return _repository.PlaceOrder();
		}

		[HttpGet]
		[Route("{resellercid}/orders/{id}")]
		public Order GetOrderById(Guid resellercid, Guid id)
		{
			return _repository.GetOrderById(id);
		}

	}
}
