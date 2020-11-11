using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCompanyApi : IHubSpotCompanyApi<CompanyHubSpotModel>
    { }

    public interface IHubSpotCompanyApi<T> : ICRUDable<T>
        where T : IHubSpotModel
    {
        CompanySearchResultModel<T> GetByDomain(string domain, CompanySearchByDomain options = null);
        Task<CompanySearchResultModel<T>> GetByDomainAsync(string domain, CompanySearchByDomain options = null, CancellationToken cancellationToken = default);
        CompanyListHubSpotModel<T> List(ListRequestOptions opts = null);
        Task<CompanyListHubSpotModel<T>> ListAsync(ListRequestOptions opts = null, CancellationToken cancellationToken = default);
    }
}