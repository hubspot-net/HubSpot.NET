using HubSpot.NET.Api.EmailSubscriptions.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotEmailSubscriptionsApi
    {
        SubscriptionTypeListHubSpotModel GetEmailSubscriptionTypes();
        SubscriptionStatusHubSpotModel GetStatus(string email);
        void UnsubscribeAll(string email);
        void UnsubscribeFrom(string email, long id);
    }
}