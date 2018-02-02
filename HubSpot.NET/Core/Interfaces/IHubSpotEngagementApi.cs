using HubSpot.NET.Api.Engagement;
using HubSpot.NET.Api.Engagement.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotEngagementApi
    {
        void Associate(long engagementId, string objectType, long objectId);
        EngagementHubSpotModel Create(EngagementHubSpotModel entity);
        void Delete(long engagementId);
        EngagementHubSpotModel GetById(long engagementId);
        EngagementListHubSpotModel List(EngagementListRequestOptions opts = null);
        EngagementListHubSpotModel ListAssociated(long objectId, string objectType, EngagementListRequestOptions opts = null);
        EngagementListHubSpotModel ListRecent(EngagementListRequestOptions opts = null);
        void Update(EngagementHubSpotModel entity);
    }
}