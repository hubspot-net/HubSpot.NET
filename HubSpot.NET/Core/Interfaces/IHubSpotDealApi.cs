using System.Collections.Generic;
using HubSpot.NET.Api.Deal.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotDealApi<T> : ICRUDable<T>
        where T : IHubSpotModel   
    {
        DealListHubSpotModel<T> List(bool includeAssociations, ListRequestOptions opts = null);     
        DealListHubSpotModel<T> ListAssociated(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "contact");
        DealRecentListHubSpotModel<T> RecentlyCreated(DealRecentRequestOptions opts = null);
        DealRecentListHubSpotModel<T> RecentlyUpdated(DealRecentRequestOptions opts = null);
    }

    public interface IHubSpotDealApi : IHubSpotDealApi<DealHubSpotModel> { }
}