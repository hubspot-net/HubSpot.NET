namespace HubSpot.NET.Api.Properties
{
    using HubSpot.NET.Api.Properties.Dto;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

    public class HubSpotCompaniesPropertiesApi : IHubSpotCompanyPropertiesApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotCompaniesPropertiesApi(IHubSpotClient client)
        {
            _client = client;
        }

        public PropertiesListHubSpotModel<CompanyPropertyHubSpotModel> GetAll()
        {
            var path = $"{new PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>().RouteBasePath}";

            return _client.ExecuteList<PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>>(path, convertToPropertiesSchema: false);
        }

        public CompanyPropertyHubSpotModel Create(CompanyPropertyHubSpotModel property)
        {
            var path = $"{new PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>().RouteBasePath}";

            return _client.Execute<CompanyPropertyHubSpotModel>(path, property, Method.POST, convertToPropertiesSchema: false);
        }

        public CompanyPropertyHubSpotModel Update(CompanyPropertyHubSpotModel property)
        {
            var path = $"{new PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>().RouteBasePath}/named/{property.Name}";

            return _client.Execute<CompanyPropertyHubSpotModel>(path, property, Method.PUT, convertToPropertiesSchema: false);
        }

        public void Delete(string propertyName)
        {
            var path = $"{new PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>().RouteBasePath}/named/{propertyName}";

            _client.Execute(path, method: Method.DELETE, convertToPropertiesSchema: true);
        }
    }
}