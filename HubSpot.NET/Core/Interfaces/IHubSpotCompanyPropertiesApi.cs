using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubSpot.NET.Api.Properties.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyPropertiesApi
    {
        PropertiesListHubSpotModel<CompanyPropertyHubSpotModel> GetAll();
        CompanyPropertyHubSpotModel Create(CompanyPropertyHubSpotModel property);
    }
}
