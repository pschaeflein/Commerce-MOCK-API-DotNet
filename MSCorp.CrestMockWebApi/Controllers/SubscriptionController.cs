/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using System.Web.Http;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Subscription;
using System.Collections.Generic;

namespace MSCorp.CrestMockWebApi.Controllers
{
	public class SubscriptionController : ApiController
	{
		private readonly ISubscriptionRepository _repository;

		public SubscriptionController(ISubscriptionRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("{resellercid}/subscriptions/")]
		public List<SingleSubscriptionResponseData> GetSubscriptions(Guid resellerCid, Guid recipient_customer_id)
		{
			return _repository.GetMultipleSubscriptionResponseData();
		}

		[HttpGet]
		[Route("{resellercid}/subscriptions/{subscriptionid}")]
		public SingleSubscriptionResponseData GetSubscriptionById(Guid resellerCid, Guid subscriptionId)
		{
			return _repository.GetSingleSubscriptionResponseData(new SingleSubscriptionRequestData()
			{
				SubscriptionId = subscriptionId
			});
		}

	}
}
