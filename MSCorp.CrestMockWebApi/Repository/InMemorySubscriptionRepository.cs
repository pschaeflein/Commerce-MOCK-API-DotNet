/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.Collections.Generic;
using MSCorp.CrestMockWebApi.Constants;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Subscription;
using MSCorp.CrestMockWebApi.Services;

namespace MSCorp.CrestMockWebApi.Repository
{
	public class InMemorySubscriptionRepository : ISubscriptionRepository
	{

		private readonly List<SingleSubscriptionResponseData> _testDataList;

		public InMemorySubscriptionRepository()
		{
			_testDataList = GetExampleSubscriptionDataList();
		}

		public List<SingleSubscriptionResponseData> GetMultipleSubscriptionResponseData()
		{
			return GetExampleSubscriptionDataList();
		}

		public SingleSubscriptionResponseData GetSingleSubscriptionResponseData(SingleSubscriptionRequestData singleSubscriptionRequestData)
		{
			return _testDataList.Find(i => i.id == singleSubscriptionRequestData.SubscriptionId.ToString());
		}

		private static List<SingleSubscriptionResponseData> GetExampleSubscriptionDataList()
		{
			return JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<SingleSubscriptionResponseData>
					(ResourcePathConstant.GetSubscriptionResponseDataPath);
		}
	}
}