using MSCorp.CrestMockWebApi.Models.Subscription;

namespace MSCorp.CrestMockWebApi.Interfaces
{
    public interface ISubscriptionRepository
    {
        SingleSubscriptionResponseData GetSingleSubscriptionResponseData(SingleSubscriptionRequestData data);
    }
}
