using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyApi : IHubSpotCompanyApi<CompanyHubSpotModel>
    { }

    public interface IHubSpotCompanyApi<T> : ICRUDable<T>
        where T : IHubSpotModel
    {
        CompanySearchResultModel<T> GetByDomain(string domain, CompanySearchByDomain options = null);
        CompanyListHubSpotModel<T> List(ListRequestOptions opts = null);
    }
}