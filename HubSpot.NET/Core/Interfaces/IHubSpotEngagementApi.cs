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
        EngagementListHubSpotModel<T> List<T>(EngagementListRequestOptions opts = null) where T : EngagementHubSpotModel;
        EngagementListHubSpotModel<T> ListAssociated<T>(long objectId, string objectType, EngagementListRequestOptions opts = null) where T : EngagementHubSpotModel;
        EngagementListHubSpotModel<T> ListRecent<T>(EngagementListRequestOptions opts = null) where T : EngagementHubSpotModel;
        void Update(EngagementHubSpotModel entity);
    }
}