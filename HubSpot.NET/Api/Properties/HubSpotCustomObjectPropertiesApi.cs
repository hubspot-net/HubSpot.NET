namespace HubSpot.NET.Api.Properties
{
    using Dto;
    using Core.Interfaces;
    using RestSharp;

    public class HubSpotCustomObjectPropertiesApi : IHubSpotCustomObjectPropertiesApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotCustomObjectPropertiesApi(IHubSpotClient client)
        {
            _client = client;
        }

        public CustomObjectPropertyHubSpotModel GetProperty<T>(string customObjectName, string customObjectProperty) where T : CustomObjectPropertyHubSpotModel, new()
        {
            var path = $"{new PropertiesListHubSpotModel<CustomObjectPropertyHubSpotModel>().RouteBasePath}/{customObjectName}/{customObjectProperty}";
            var result = _client.Execute<CustomObjectPropertyHubSpotModel>(path, null, Method.GET, convertToPropertiesSchema: false);
            return result;
        }

        public CustomObjectPropertyHubSpotModel UpdateProperty<T>(string customObjectName, string customObjectProperty,
            CustomObjectPropertyHubSpotModel property) where T : CustomObjectPropertyHubSpotModel, new()
        {
            var path = $"{new PropertiesListHubSpotModel<CustomObjectPropertyHubSpotModel>().RouteBasePath}/{customObjectName}/{customObjectProperty}";
            return _client.Execute<CustomObjectPropertyHubSpotModel>(path, property, Method.PATCH, convertToPropertiesSchema: false);
        }
    }
}