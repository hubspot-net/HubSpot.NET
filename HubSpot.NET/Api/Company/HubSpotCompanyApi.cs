using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Company
{
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
        public T Create<T>(T entity) where T : CompanyHubSpotModel
            => _client.Execute($"{GetRoute<T>()}/companies", entity, Method.POST);

        /// <summary>
        /// Gets a specific company by it's ID
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="companyId">The ID</param>
        /// <returns>The company entity</returns>
        public T GetById<T>(long companyId) where T : CompanyHubSpotModel
            => _client.Execute<T>($"{GetRoute<T>()}/companies/{companyId}");

        /// <summary>
        /// Gets a company by domain name
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="domain">Domain name to search for</param>
        /// <param name="options">Set of search options</param>
        /// <returns>The company entity</returns>
        public CompanySearchResultModel<T> GetByDomain<T>(string domain, CompanySearchByDomain opts = null) where T : CompanyHubSpotModel
        {
            opts = opts ?? new CompanySearchByDomain();

            var path = $"{GetRoute<T>()}/domains/{domain}/companies";

            return _client.ExecuteList<CompanySearchResultModel<T>>(path, opts, Method.POST);
        }

        public CompanyListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T : CompanyHubSpotModel
        {
            opts = opts ?? new ListRequestOptions();

            var path = $"{GetRoute<T>()}/companies/paged".SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())
                path.SetQueryParam("properties", opts.PropertiesToInclude);

            if (opts.Offset.HasValue)
                path = path.SetQueryParam("offset", opts.Offset);

            return _client.ExecuteList<CompanyListHubSpotModel<T>>(path, opts);
        }

        /// <summary>
        /// Updates a given company entity, any changed properties are updated
        /// </summary>
        /// <typeparam name="T">Implementation of CompanyHubSpotModel</typeparam>
        /// <param name="entity">The company entity</param>
        /// <returns>The updated company entity</returns>
        public T Update<T>(T entity) where T : CompanyHubSpotModel
        {
            if (entity.Id < 1)
                throw new ArgumentException("Company entity must have an id set!");

            return _client.Execute($"{GetRoute<T>()}/companies/{entity.Id}", entity, Method.PUT);
        }

        /// <summary>
        /// Deletes the given company
        /// </summary>
        /// <param name="companyId">ID of the company</param>
        public void Delete(long companyId)
            => _client.Execute($"{GetRoute<CompanyHubSpotModel>()}/companies/{companyId}", method: Method.DELETE);
    }
}