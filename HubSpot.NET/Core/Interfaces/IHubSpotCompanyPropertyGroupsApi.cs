using System.Collections.Generic;
using HubSpot.NET.Api.Properties.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyPropertyGroupsApi
    {
        IEnumerable<CompanyPropertyGroupHubSpotModel> GetAll();
        CompanyPropertyGroupHubSpotModel Create(CompanyPropertyGroupHubSpotModel property);
    }
}