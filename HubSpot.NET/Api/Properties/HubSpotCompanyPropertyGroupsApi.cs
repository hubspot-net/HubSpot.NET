using System.Collections.Generic;
using HubSpot.NET.Api.Properties.Dto;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Properties
{
    public class HubSpotCompanyPropertyGroupsApi : ApiRoutable, IHubSpotCompanyPropertyGroupsApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/properties/v1";

        public HubSpotCompanyPropertyGroupsApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<CompanyPropertyGroupHubSpotModel>("/companies/groups");
        }

        public IEnumerable<CompanyPropertyGroupHubSpotModel> GetAll()
            => _client.Execute<List<CompanyPropertyGroupHubSpotModel>>(GetRoute<CompanyPropertyGroupHubSpotModel>());

        public CompanyPropertyGroupHubSpotModel Create(CompanyPropertyGroupHubSpotModel property)
            => _client.Execute<CompanyPropertyGroupHubSpotModel, CompanyPropertyGroupHubSpotModel>(GetRoute<CompanyPropertyGroupHubSpotModel>(), property, Method.POST);
    }
}