using HubSpot.NET.Api;
using HubSpot.NET.Api.Deal.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotDealApi
    {
        T Create<T>(T entity) where T : DealHubSpotModel, new();
        void Delete(long dealId);
        T GetById<T>(long dealId) where T : DealHubSpotModel, new();
        T Update<T>(T entity) where T : DealHubSpotModel, new();

        DealListHubSpotModel<T> List<T>(bool includeAssociations, ListRequestOptions opts = null)
            where T : DealHubSpotModel, new();

        DealRecentListHubSpotModel<T> RecentlyCreated<T>(DealRecentRequestOptions opts = null)
            where T : DealHubSpotModel, new();

        DealRecentListHubSpotModel<T> RecentlyUpdated<T>(DealRecentRequestOptions opts = null)
            where T : DealHubSpotModel, new();
        DealListHubSpotModel<T> ListAssociated<T>(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "contact") where T :DealHubSpotModel, new();

        SearchHubSpotModel<T> Search<T>(SearchRequestOptions opts = null)
            where T : DealHubSpotModel, new();

        T AssociateToCompany<T>(T entity, long companyId)
            where T : DealHubSpotModel, new();

        T AssociateToContact<T>(T entity, long contactId)
            where T : DealHubSpotModel, new();

        T GetAssociations<T>(T entity)
            where T : DealHubSpotModel, new();
    }
}