using HubSpot.NET.Api.Engagement;
using HubSpot.NET.Api.Engagement.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotEngagementApi
    {
        void Associate(long engagementId, string objectType, long objectId);
        Task AssociateAsync(long engagementId, string objectType, long objectId, CancellationToken cancellationToken = default);
        EngagementHubSpotModel Create(EngagementHubSpotModel entity);
        Task<EngagementHubSpotModel> CreateAsync(EngagementHubSpotModel entity, CancellationToken cancellationToken = default);
        void Delete(long engagementId);
        Task DeleteAsync(long engagementId, CancellationToken cancellationToken = default);
        EngagementHubSpotModel GetById(long engagementId);
        Task<EngagementHubSpotModel> GetByIdAsync(long engagementId, CancellationToken cancellationToken = default);
        EngagementListHubSpotModel<T> List<T>(EngagementListRequestOptions opts = null) where T : EngagementHubSpotModel;
        Task<EngagementListHubSpotModel<T>> ListAsync<T>(EngagementListRequestOptions opts = null, CancellationToken cancellationToken = default) where T : EngagementHubSpotModel;
        EngagementListHubSpotModel<T> ListAssociated<T>(long objectId, string objectType, EngagementListRequestOptions opts = null) where T : EngagementHubSpotModel;
        Task<EngagementListHubSpotModel<T>> ListAssociatedAsync<T>(long objectId, string objectType, EngagementListRequestOptions opts = null, CancellationToken cancellationToken = default) where T : EngagementHubSpotModel;
        EngagementListHubSpotModel<T> ListRecent<T>(EngagementListRequestOptions opts = null) where T : EngagementHubSpotModel;
        Task<EngagementListHubSpotModel<T>> ListRecentAsync<T>(EngagementListRequestOptions opts = null, CancellationToken cancellationToken = default) where T : EngagementHubSpotModel;
        void Update(EngagementHubSpotModel entity);
        Task UpdateAsync(EngagementHubSpotModel entity, CancellationToken cancellationToken = default);
    }
}