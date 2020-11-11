namespace HubSpot.NET.Core.Interfaces
{
    using HubSpot.NET.Api.Contact.Dto;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IHubSpotContactPropertyApi
    {
        ContactPropertyModel CreateProperty(ContactPropertyModel entity);
        Task<ContactPropertyModel> CreatePropertyAsync(ContactPropertyModel entity, CancellationToken cancellationToken = default);
        List<ContactPropertyModel> GetProperties();
        Task<List<ContactPropertyModel>> GetPropertiesAsync(CancellationToken cancellationToken = default);
        ContactPropertyModel GetProperty(string propertyName);
        Task<ContactPropertyModel> GetPropertyAsync(string propertyName, CancellationToken cancellationToken = default);
        ContactPropertyModel UpdateProperty(ContactPropertyModel model);
        Task<ContactPropertyModel> UpdatePropertyAsync(ContactPropertyModel model, CancellationToken cancellationToken = default);
    }
}
