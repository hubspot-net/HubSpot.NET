using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HubSpot.NET.Api.Properties.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyPropertyGroupsApi
    {
        IEnumerable<CompanyPropertyGroupHubSpotModel> GetAll();
        Task<IEnumerable<CompanyPropertyGroupHubSpotModel>> GetAllAsync(CancellationToken cancellationToken = default);
        CompanyPropertyGroupHubSpotModel Create(CompanyPropertyGroupHubSpotModel property);
        Task<CompanyPropertyGroupHubSpotModel> CreateAsync(CompanyPropertyGroupHubSpotModel property, CancellationToken cancellationToken = default);
    }
}