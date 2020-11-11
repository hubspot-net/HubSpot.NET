using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<CompanyPropertyGroupHubSpotModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _client.ExecuteAsync<List<CompanyPropertyGroupHubSpotModel>>(GetRoute<CompanyPropertyGroupHubSpotModel>(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public CompanyPropertyGroupHubSpotModel Create(CompanyPropertyGroupHubSpotModel property)
            => _client.Execute<CompanyPropertyGroupHubSpotModel, CompanyPropertyGroupHubSpotModel>(GetRoute<CompanyPropertyGroupHubSpotModel>(), property, Method.POST);

        public Task<CompanyPropertyGroupHubSpotModel> CreateAsync(CompanyPropertyGroupHubSpotModel property, CancellationToken cancellationToken = default)
            => _client.ExecuteAsync<CompanyPropertyGroupHubSpotModel, CompanyPropertyGroupHubSpotModel>(GetRoute<CompanyPropertyGroupHubSpotModel>(), property, Method.POST, cancellationToken: cancellationToken);
    }
}