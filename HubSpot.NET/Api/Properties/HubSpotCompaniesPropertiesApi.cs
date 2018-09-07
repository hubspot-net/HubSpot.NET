using HubSpot.NET.Api.Properties.Dto;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Properties
{
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
            => _client.ExecuteList<PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>>(GetRoute<CompanyPropertyHubSpotModel>(), 
                                                                                            convertToPropertiesSchema: false);

        public CompanyPropertyHubSpotModel Create(CompanyPropertyHubSpotModel property) 
            => _client.Execute(GetRoute<CompanyPropertyHubSpotModel>(), property, Method.POST, convertToPropertiesSchema: false);
    }
}