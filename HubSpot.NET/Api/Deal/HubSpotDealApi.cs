using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Api.Shared;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Deal
{
    public class HubSpotDealApi : ApiRoutable, IHubSpotDealApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/deals/v1";

        public HubSpotDealApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<DealHubSpotModel>("/deal");
        }

        /// <summary>
        /// Creates a deal entity
        /// </summary>
        /// <typeparam name="T">Implementation of DealHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        public DealHubSpotModel Create(DealHubSpotModel entity)
        {
            NameTransportModel<DealHubSpotModel> model = new NameTransportModel<DealHubSpotModel>();
            model.ToPropertyTransportModel(entity);

            return _client.Execute<DealHubSpotModel,NameTransportModel<DealHubSpotModel>>(GetRoute<DealHubSpotModel>(), model, Method.POST);
        }

        /// <summary>
        /// Gets a single deal by ID
        /// </summary>
        /// <param name="dealId">ID of the deal</param>
        /// <typeparam name="T">Implementation of DealHubSpotModel</typeparam>
        /// <returns>The deal entity</returns>
        public DealHubSpotModel GetById(long dealId) 
            => _client.Execute<DealHubSpotModel>(GetRoute<DealHubSpotModel>(dealId.ToString()));

        /// <summary>
        /// Updates a given deal
        /// </summary>
        /// <typeparam name="T">Implementation of DealHubSpotModel</typeparam>
        /// <param name="entity">The deal entity</param>
        /// <returns>The updated deal entity</returns>
        public DealHubSpotModel Update(DealHubSpotModel entity)
        {
            if (entity.Id < 1)            
                throw new ArgumentException("Deal entity must have an id set!");

            return _client.Execute<DealHubSpotModel, DealHubSpotModel>(GetRoute<DealHubSpotModel>(entity.Id.ToString()), entity, method: Method.PUT);            
        }

        /// <summary>
        /// Gets a list of deals
        /// </summary>
        /// <typeparam name="T">Implementation of DealListHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of deals</returns>
        public DealListHubSpotModel<DealHubSpotModel> List(bool includeAssociations, ListRequestOptions opts = null)
        {
            opts = opts ?? new ListRequestOptions(250);         

            Url path = GetRoute<DealListHubSpotModel<DealHubSpotModel>>("deal", "paged").SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("offset", opts.Offset);            

            if (includeAssociations)            
                path = path.SetQueryParam("includeAssociations", "true");            

            if (opts.PropertiesToInclude.Any())            
                path = path.SetQueryParam("properties", opts.PropertiesToInclude);           

            return _client.Execute<DealListHubSpotModel<DealHubSpotModel>, ListRequestOptions>(path, opts);
        }

        /// <summary>
        /// Gets a list of deals associated to a hubSpot Object
        /// </summary>
        /// <typeparam name="T">Implementation of DealListHubSpotModel</typeparam>
        /// <param name="includeAssociations">Bool include associated Ids</param>
        /// <param name="hubId">Long Id of Hubspot object related to deals</param>
        /// <param name="objectName">String name of Hubspot object related to deals (contact\account)</param>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of deals</returns>
        public DealListHubSpotModel<DealHubSpotModel> ListAssociated(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "contact")
        {
            opts = opts ?? new ListRequestOptions();            

            Url path = $"{GetRoute<DealListHubSpotModel<DealHubSpotModel>>()}/deal/associated/{objectName}/{hubId}/paged"
                .SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("offset", opts.Offset);            

            if (includeAssociations)            
                path = path.SetQueryParam("includeAssociations", "true");            

            if (opts.PropertiesToInclude.Any())            
                path = path.SetQueryParam("properties", opts.PropertiesToInclude);            

            return _client.Execute<DealListHubSpotModel<DealHubSpotModel>, ListRequestOptions>(path, opts);
        }

        /// <summary>
        /// Deletes a given deal (by ID)
        /// </summary>
        /// <param name="dealId">ID of the deal</param>
        public void Delete(long dealId) 
            => _client.ExecuteOnly(GetRoute<DealHubSpotModel>(dealId.ToString()), method: Method.DELETE);

        /// <summary>
        /// Gets a list of recently created deals
        /// </summary>
        /// <typeparam name="T">Implementation of DealListHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of deals</returns>
        public DealRecentListHubSpotModel<DealHubSpotModel> RecentlyCreated(DealRecentRequestOptions opts = null)
        {
            opts = opts ?? new DealRecentRequestOptions();            

            Url path = $"{GetRoute<DealRecentListHubSpotModel<DealHubSpotModel>>()}/deal/recent/created"
                            .SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("offset", opts.Offset);            

            if (opts.IncludePropertyVersion)            
                path = path.SetQueryParam("includePropertyVersions", "true");            

            if (!string.IsNullOrEmpty(opts.Since))            
                path = path.SetQueryParam("since", opts.Since);            

            return _client.Execute<DealRecentListHubSpotModel<DealHubSpotModel>, DealRecentRequestOptions>(path, opts);            
        }

        /// <summary>
        /// Gets a list of recently modified deals
        /// </summary>
        /// <typeparam name="T">Implementation of DealListHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of deals</returns>
        public DealRecentListHubSpotModel<DealHubSpotModel> RecentlyUpdated(DealRecentRequestOptions opts = null)
        {
            opts = opts ?? new DealRecentRequestOptions();            

            var path = $"{GetRoute<DealRecentListHubSpotModel<DealHubSpotModel>>()}/deal/recent/modified"
                .SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("offset", opts.Offset);

            if (opts.IncludePropertyVersion)
                path = path.SetQueryParam("includePropertyVersions", "true");

            if (!string.IsNullOrEmpty(opts.Since))             
                path = path.SetQueryParam("since", opts.Since);

            return _client.Execute<DealRecentListHubSpotModel<DealHubSpotModel>, DealRecentRequestOptions>(path, opts);
        }
    }
}
