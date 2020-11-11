using System.Threading;
using System.Threading.Tasks;
using HubSpot.NET.Api.Properties.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyPropertiesApi
    {
        PropertiesListHubSpotModel<CompanyPropertyHubSpotModel> GetAll();
        Task<PropertiesListHubSpotModel<CompanyPropertyHubSpotModel>> GetAllAsync(CancellationToken cancellationToken = default);
        CompanyPropertyHubSpotModel Create(CompanyPropertyHubSpotModel property);
        Task<CompanyPropertyHubSpotModel> CreateAsync(CompanyPropertyHubSpotModel property, CancellationToken cancellationToken = default);
    }
}
