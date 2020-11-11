using System.Threading;
using System.Threading.Tasks;
using HubSpot.NET.Api.Deal.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotDealApi<T> : ICRUDable<T>
        where T : IHubSpotModel   
    {
        DealListHubSpotModel<T> List(bool includeAssociations, ListRequestOptions opts = null);     
        Task<DealListHubSpotModel<T>> ListAsync(bool includeAssociations, ListRequestOptions opts = null, CancellationToken cancellationToken = default);     
        DealListHubSpotModel<T> ListAssociated(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "contact");
        Task<DealListHubSpotModel<T>> ListAssociatedAsync(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "contact", CancellationToken cancellationToken = default);
        DealRecentListHubSpotModel<T> RecentlyCreated(DealRecentRequestOptions opts = null);
        Task<DealRecentListHubSpotModel<T>> RecentlyCreatedAsync(DealRecentRequestOptions opts = null, CancellationToken cancellationToken = default);
        DealRecentListHubSpotModel<T> RecentlyUpdated(DealRecentRequestOptions opts = null);
        Task<DealRecentListHubSpotModel<T>> RecentlyUpdatedAsync(DealRecentRequestOptions opts = null, CancellationToken cancellationToken = default);
    }

    public interface IHubSpotDealApi : IHubSpotDealApi<DealHubSpotModel> { }
}