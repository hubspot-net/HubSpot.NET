using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HubSpot.NET.Api.Contact.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotContactApi : IHubSpotContactApi<ContactHubSpotModel>
    { }

    public interface IHubSpotContactApi<T> : ICRUDable<T>
        where T : IHubSpotModel
    {
        ContactListHubSpotModel<T> List(ListRequestOptions opts = null);
        Task<ContactListHubSpotModel<T>> ListAsync(ListRequestOptions opts = null, CancellationToken cancellationToken = default);
        ContactListHubSpotModel<T> RecentlyCreated(ListRecentRequestOptions opts = null);
        Task<ContactListHubSpotModel<T>> RecentlyCreatedAsync(ListRecentRequestOptions opts = null, CancellationToken cancellationToken = default);
        ContactListHubSpotModel<T> RecentlyUpdated(ListRecentRequestOptions opts = null);
        Task<ContactListHubSpotModel<T>> RecentlyUpdatedAsync(ListRecentRequestOptions opts = null, CancellationToken cancellationToken = default);
        ContactSearchHubSpotModel<T> Search(ContactSearchRequestOptions opts = null);
        Task<ContactSearchHubSpotModel<T>> SearchAsync(ContactSearchRequestOptions opts = null, CancellationToken cancellationToken = default);
        T GetByEmail(string email, bool IncludeHistory = true);
        Task<T> GetByEmailAsync(string email, bool IncludeHistory = true, CancellationToken cancellationToken = default);
        T GetByUserToken(string userToken, bool includeHistory = true);
        Task<T> GetByUserTokenAsync(string userToken, bool includeHistory = true, CancellationToken cancellationToken = default);
        T GetById(long contactId, bool includeHistory = true);
        Task<T> GetByIdAsync(long contactId, bool includeHistory = true, CancellationToken cancellationToken = default);
        T CreateOrUpdate(T entity);
        Task<T> CreateOrUpdateAsync(T entity, CancellationToken cancellationToken = default);
        T CreateOrUpdate(string originalEmail, T entity);
        Task<T> CreateOrUpdateAsync(string originalEmail, T entity, CancellationToken cancellationToken = default);
        void Batch(List<T> entities);
        Task BatchAsync(List<T> entities, CancellationToken cancellationToken = default);
        ContactListHubSpotModel<T> GetList(long listId, ListRequestOptions opts = null);
        Task<ContactListHubSpotModel<T>> GetListAsync(long listId, ListRequestOptions opts = null, CancellationToken cancellationToken = default);
        void Delete(long contactId);
        Task DeleteAsync(long contactId, CancellationToken cancellationToken = default);
    }
}
