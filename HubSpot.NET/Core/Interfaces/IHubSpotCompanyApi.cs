using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyApi
    {
        T Create<T>(T entity) where T : CompanyHubSpotModel, new();
        void Delete(long companyId);
        T GetByDomain<T>(string domain, CompanySearchByDomain options = null) where T : CompanySearchResultModel, new();
        T GetById<T>(long companyId) where T : CompanyHubSpotModel, new();
        T Update<T>(T entity) where T : CompanyHubSpotModel, new();
    }
}