using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyApi
    {
        T Create<T>(T entity) where T : CompanyHubSpotModel;
        void Delete(long companyId);
        CompanySearchResultModel<T> GetByDomain<T>(string domain, CompanySearchByDomain options = null) where T : CompanyHubSpotModel;
        CompanyListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T : CompanyHubSpotModel;
        T GetById<T>(long companyId) where T : CompanyHubSpotModel;
        T Update<T>(T entity) where T : CompanyHubSpotModel;
    }
}