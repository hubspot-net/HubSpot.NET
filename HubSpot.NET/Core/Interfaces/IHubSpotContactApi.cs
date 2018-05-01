using System.Collections.Generic;
using HubSpot.NET.Api.Contact;
using HubSpot.NET.Api.Contact.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotContactApi
    {
        T Create<T>(T entity) where T : ContactHubSpotModel, new();
        void Delete(long contactId);
        void Batch<T>(List<T> entities) where T : ContactHubSpotModel, new();
        T GetByEmail<T>(string email) where T : ContactHubSpotModel, new();
        T GetById<T>(long contactId) where T : ContactHubSpotModel, new();
        ContactListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T : ContactHubSpotModel, new();
        void Update<T>(T contact) where T : ContactHubSpotModel, new();
        ContactListHubSpotModel<T> RecentlyCreated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel, new();
        ContactListHubSpotModel<T> RecentlyUpdated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel, new();
    }
}