using System.Collections.Generic;
using HubSpot.NET.Api.Deal.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotDealApi<T> where T : IHubSpotModel   
    {
        T Create(T entity);        
        T GetById(long dealId);
        void Delete(long dealId);
        T Update(T entity);

        DealListHubSpotModel<T> List(bool includeAssociations, ListRequestOptions opts = null);     
        DealListHubSpotModel<T> ListAssociated(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "contact");
        DealRecentListHubSpotModel<T> RecentlyCreated(DealRecentRequestOptions opts = null);
        DealRecentListHubSpotModel<T> RecentlyUpdated(DealRecentRequestOptions opts = null);
    }
}