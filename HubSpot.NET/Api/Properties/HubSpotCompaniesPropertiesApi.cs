using HubSpot.NET.Api.Properties.Dto;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Properties
{
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

    }
}