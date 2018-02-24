using System.Collections.Generic;
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
    }
}