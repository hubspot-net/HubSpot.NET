namespace HubSpot.NET.Api.Properties
{
    using HubSpot.NET.Api.Properties.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System.Threading;
    using System.Threading.Tasks;

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

        public Task<PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>> GetAllAsync(CancellationToken cancellationToken = default)
            => _client.ExecuteAsync<PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>>(GetRoute<CompanyPropertyHubSpotModel>(), cancellationToken: cancellationToken);

        public CompanyPropertyHubSpotModel Create(CompanyPropertyHubSpotModel property) 
            => _client.Execute<CompanyPropertyHubSpotModel, CompanyPropertyHubSpotModel>(GetRoute<CompanyPropertyHubSpotModel>(), property, Method.POST);

        public Task<CompanyPropertyHubSpotModel> CreateAsync(CompanyPropertyHubSpotModel property, CancellationToken cancellationToken = default) 
            => _client.ExecuteAsync<CompanyPropertyHubSpotModel, CompanyPropertyHubSpotModel>(GetRoute<CompanyPropertyHubSpotModel>(), property, Method.POST, cancellationToken: cancellationToken);
    }
}