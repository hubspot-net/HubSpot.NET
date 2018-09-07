using System.Collections.Generic;
using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Contact;
using HubSpot.NET.Api.Contact.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotContactApi
    {
        T Create<T>(T entity) where T : ContactHubSpotModel;
        T CreateOrUpdate<T>(T entity) where T : ContactHubSpotModel;
        void Delete(long contactId);
        void Batch<T>(List<T> entities) where T : ContactHubSpotModel;
        T GetByEmail<T>(string email) where T : ContactHubSpotModel;
        T GetById<T>(long contactId) where T : ContactHubSpotModel;
        T GetByUserToken<T>(string userToken) where T : ContactHubSpotModel;
        ContactListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T : ContactHubSpotModel;
        void Update<T>(T contact) where T : ContactHubSpotModel;
        ContactListHubSpotModel<T> RecentlyCreated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel;
        ContactListHubSpotModel<T> RecentlyUpdated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel;
        ContactSearchHubSpotModel<T> Search<T>(ContactSearchRequestOptions opts = null) where T : ContactHubSpotModel;
    }
}
