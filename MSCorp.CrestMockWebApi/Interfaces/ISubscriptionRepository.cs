using MSCorp.CrestMockWebApi.Models.Subscription;
using System.Collections.Generic;

namespace MSCorp.CrestMockWebApi.Interfaces
{
	public interface ISubscriptionRepository
	{
		List<SingleSubscriptionResponseData> GetMultipleSubscriptionResponseData();
		SingleSubscriptionResponseData GetSingleSubscriptionResponseData(SingleSubscriptionRequestData data);
	}
}
