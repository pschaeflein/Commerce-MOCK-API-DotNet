using System;
using System.Web.Http;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Orders;
using System.Collections.Generic;

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

		//string.Format("https://api.cp.microsoft.com/{0}/orders?recipient_customer_id={1}", resellerCid, customerCid)
		[HttpGet]
		[Route("{resellercid}/orders/")]
		public List<Order> GetOrders(Guid resellercid, Guid recipient_customer_id)
		{
			return _repository.GetOrders();
		}

	}
}
