namespace HubSpot.NET.Api.Company
{
    using HubSpot.NET.Api.Company.Dto;
    using HubSpot.NET.Core;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System;
    using System.Linq;
    using System.Net;

    public class HubSpotCompanyApi : ApiRoutable, IHubSpotCompanyApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/companies/v2";        

        public HubSpotCompanyApi(IHubSpotClient client)
        {
            _client = client;            
        }

        /// <summary>
        /// Creates a Company entity
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        /// <exception cref="NotImplementedException"></exception>
        public CompanyHubSpotModel Create(CompanyHubSpotModel entity)
            => _client.Execute<CompanyHubSpotModel,CompanyHubSpotModel>(GetRoute<CompanyHubSpotModel>("companies"), entity, Method.POST);

        /// <summary>
        /// Gets a specific company by it's ID
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="companyId">The ID</param>
        /// <returns>The company entity or null if the company does not exist.</returns>
        public CompanyHubSpotModel GetById(long companyId)
        {
            try
            {
                return _client.Execute<CompanyHubSpotModel>(GetRoute<CompanyHubSpotModel>("companies", companyId.ToString()));
            }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        /// <summary>
        /// Gets a company by domain name
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="domain">Domain name to search for</param>
        /// <param name="options">Set of search options</param>
        /// <returns>The company entity or null if the company does not exist.</returns>
        public CompanySearchResultModel<CompanyHubSpotModel> GetByDomain(string domain, CompanySearchByDomain opts = null)
        {
            opts = opts ?? new CompanySearchByDomain();

            var path = GetRoute<CompanyHubSpotModel>("domains", domain, "companies");

            try
            {
                return _client.Execute<CompanySearchResultModel<CompanyHubSpotModel>, CompanySearchByDomain>(path, opts, Method.POST);
            }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        public CompanyListHubSpotModel<CompanyHubSpotModel> List(ListRequestOptions opts = null)
        {
            opts = opts ?? new ListRequestOptions();

            string path = GetRoute<CompanyHubSpotModel>("companies", "paged");

            path += $"{QueryParams.COUNT}={opts.Limit}";

            if (opts.PropertiesToInclude.Any())
                path += $"{QueryParams.PROPERTIES}={opts.PropertiesToInclude}";

            if (opts.Offset.HasValue)
                path += $"{QueryParams.OFFSET}={opts.Offset}";

            return _client.Execute<CompanyListHubSpotModel<CompanyHubSpotModel>, ListRequestOptions>(path, opts);
        }

        /// <summary>
        /// Updates a given company entity, any changed properties are updated
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="entity">The company entity</param>
        /// <returns>The updated company entity</returns>
        public CompanyHubSpotModel Update(CompanyHubSpotModel entity)
        {
            if (entity.Id < 1)
                throw new ArgumentException("Company entity must have an id set!");

            return _client.Execute<CompanyHubSpotModel, CompanyHubSpotModel>(GetRoute<CompanyHubSpotModel>("companies", entity.Id.ToString()), entity, Method.PUT);
        }

        /// <summary>
        /// Deletes the given company
        /// </summary>
        /// <param name="companyId">ID of the company</param>
        public void Delete(long companyId)
            => _client.ExecuteOnly(GetRoute<CompanyHubSpotModel>("companies", companyId.ToString()), method: Method.DELETE);
    }
}