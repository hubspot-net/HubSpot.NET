using HubSpot.NET.Api.Properties.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyPropertiesApi
    {
        PropertiesListHubSpotModel<CompanyPropertyHubSpotModel> GetAll();

        CompanyPropertyHubSpotModel Create(CompanyPropertyHubSpotModel property);

        CompanyPropertyHubSpotModel Update(CompanyPropertyHubSpotModel property);

        void Delete(string propertyName);
    }
}
