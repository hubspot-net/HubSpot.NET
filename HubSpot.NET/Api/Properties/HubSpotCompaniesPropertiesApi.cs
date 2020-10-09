namespace HubSpot.NET.Api.Properties
{
    using HubSpot.NET.Api.Properties.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

    public class HubSpotCompaniesPropertiesApi : ApiRoutable, IHubSpotCompanyPropertiesApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/properties/v1";

        public HubSpotCompaniesPropertiesApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<CompanyPropertyHubSpotModel>("/companies/properties");
        }

        public PropertiesListHubSpotModel<CompanyPropertyHubSpotModel> GetAll() 
            => _client.Execute<PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>>(GetRoute<CompanyPropertyHubSpotModel>());

        public CompanyPropertyHubSpotModel Create(CompanyPropertyHubSpotModel property) 
            => _client.Execute<CompanyPropertyHubSpotModel, CompanyPropertyHubSpotModel>(GetRoute<CompanyPropertyHubSpotModel>(), property, Method.POST);
    }
}