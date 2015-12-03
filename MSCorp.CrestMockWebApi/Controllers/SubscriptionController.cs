/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using System.Web.Http;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Subscription;

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
        [Route("{resellercid}/subscriptions/{subscriptionid}")]
        public SingleSubscriptionResponseData GetSubscription(Guid resellerCid, Guid subscriptionId) 
        {
            return _repository.GetSingleSubscriptionResponseData(new SingleSubscriptionRequestData()
            {
                SubscriptionId = subscriptionId
            });
        }

    }
}
