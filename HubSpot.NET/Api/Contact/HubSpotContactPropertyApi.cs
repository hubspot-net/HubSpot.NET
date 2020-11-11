using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpot.NET.Api.Contact
{
    public class HubSpotContactPropertyApi : ApiRoutable, IHubSpotContactPropertyApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotContactPropertyApi(IHubSpotClient client)
        {
             MidRoute = "/properties/v1/contacts";
            _client = client;

            AddRoute<ContactPropertyModel>("properties");
        }

        public ContactPropertyModel CreateProperty(ContactPropertyModel entity)
        {
            string path = GetRoute<ContactPropertyModel>();
            return _client.Execute<ContactPropertyModel, ContactPropertyModel>(path, entity, RestSharp.Method.GET);
        }

        public Task<ContactPropertyModel> CreatePropertyAsync(ContactPropertyModel entity, CancellationToken cancellationToken = default)
        {
            string path = GetRoute<ContactPropertyModel>();
            return _client.ExecuteAsync<ContactPropertyModel, ContactPropertyModel>(path, entity, RestSharp.Method.GET, cancellationToken);
        }

        public List<ContactPropertyModel> GetProperties()
        {
            return _client.Execute<List<ContactPropertyModel>>(GetRoute<ContactPropertyModel>());
        }

        public Task<List<ContactPropertyModel>> GetPropertiesAsync(CancellationToken cancellationToken = default)
        {
            return _client.ExecuteAsync<List<ContactPropertyModel>>(GetRoute<ContactPropertyModel>(), cancellationToken: cancellationToken);
        }

        public ContactPropertyModel GetProperty(string propertyName)
        {
            string path = GetRoute<ContactPropertyModel>("named", propertyName);
            return _client.Execute<ContactPropertyModel>(path);
        }

        public Task<ContactPropertyModel> GetPropertyAsync(string propertyName, CancellationToken cancellationToken = default)
        {
            string path = GetRoute<ContactPropertyModel>("named", propertyName);
            return _client.ExecuteAsync<ContactPropertyModel>(path, cancellationToken: cancellationToken);
        }

        public ContactPropertyModel UpdateProperty(ContactPropertyModel model)
        {
            string path = GetRoute<ContactPropertyModel>("named", model.Name);
            return _client.Execute<ContactPropertyModel, ContactPropertyModel>(path, model, RestSharp.Method.PUT);
        }

        public Task<ContactPropertyModel> UpdatePropertyAsync(ContactPropertyModel model, CancellationToken cancellationToken = default)
        {
            string path = GetRoute<ContactPropertyModel>("named", model.Name);
            return _client.ExecuteAsync<ContactPropertyModel, ContactPropertyModel>(path, model, RestSharp.Method.PUT, cancellationToken);
        }
    }
}
