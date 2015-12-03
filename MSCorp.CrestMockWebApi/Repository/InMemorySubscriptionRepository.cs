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
            _testDataList = GetExampleSubsriptionDataList();
        }

        public SingleSubscriptionResponseData GetSingleSubscriptionResponseData(SingleSubscriptionRequestData singleSubscriptionRequestData)
        {
            return _testDataList.Find(i => i.id == singleSubscriptionRequestData.SubscriptionId.ToString()); 
        }

        private static List<SingleSubscriptionResponseData> GetExampleSubsriptionDataList()
        {
            return JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<SingleSubscriptionResponseData>
                (ResourcePathConstant.GetSubscriptionResponseDataPath);
        }
    }
}