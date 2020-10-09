namespace HubSpot.NET.Api.Engagement

{
    using HubSpot.NET.Api.Engagement.Dto;
    using HubSpot.NET.Core;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System;
    using System.Net;

    public class HubSpotEngagementApi : ApiRoutable, IHubSpotEngagementApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/engagements/v1";

        public HubSpotEngagementApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<EngagementHubSpotModel>("engagements/");
        }

        /// <summary>
        /// Creates an engagement
        /// </summary>
        /// <param name="entity">The engagement to create</param>
        /// <returns>The created engagement (with ID set)</returns>
        public EngagementHubSpotModel Create(EngagementHubSpotModel entity) 
            => _client.Execute<EngagementHubSpotModel, EngagementHubSpotModel>(GetRoute<EngagementHubSpotModel>(), entity, Method.POST);

        /// <summary>
        /// Updates a given engagement
        /// </summary>
        /// <param name="entity">The updated engagement</param>
        public void Update(EngagementHubSpotModel entity)
        {
            if (entity.Engagement.Id < 1)
                throw new ArgumentException("Engagement entity must have an id set!");

            _client.ExecuteOnly(GetRoute<EngagementHubSpotModel>(entity.Engagement.Id.ToString()), entity, Method.PATCH);
        }

        /// <summary>
        /// Gets a given engagement (by ID)
        /// </summary>
        /// <param name="engagementId">The ID of the engagement</param>
        /// <returns>The engagement or null if the engagement does not exist.</returns>
        public EngagementHubSpotModel GetById(long engagementId)
        {
            try
            {
                return _client.Execute<EngagementHubSpotModel>(GetRoute<EngagementHubSpotModel>(engagementId.ToString()));
            }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        /// <summary>
        /// Retrieves a paginated list of engagements
        /// </summary>
        /// <param name="opts">Options for querying</param>
        /// <returns>List of engagements, with additional metadata, e.g. total</returns>
        public EngagementListHubSpotModel<T> List<T>(EngagementListRequestOptions opts = null) where T: EngagementHubSpotModel
        {
            opts = opts ?? new EngagementListRequestOptions();

            var path = $"{GetRoute<T>("paged")}?{QueryParams.LIMIT}={opts.Limit}";

            if (opts.Offset.HasValue)            
                path += $"{QueryParams.OFFSET}={opts.Offset}";

            return _client.Execute<EngagementListHubSpotModel<T>, EngagementListRequestOptions>(path, opts);            
        }

        /// <summary>
        /// Lists recent engagements (i.e. in date order)
        /// </summary>
        /// <param name="opts">Options for querying</param>
        /// <returns>List of engagements, with additional metadata, e.g. total</returns>
        public EngagementListHubSpotModel<T> ListRecent<T>(EngagementListRequestOptions opts = null) where T : EngagementHubSpotModel
        {
            opts = opts ?? new EngagementListRequestOptions();

            var path = $"{GetRoute<T>()}/engagements/recent/modified?{QueryParams.COUNT}={opts.Limit}";

            if (opts.Offset.HasValue)            
                path += $"{QueryParams.OFFSET}={opts.Offset}";

            return _client.Execute<EngagementListHubSpotModel<T>, EngagementListRequestOptions>(path, opts);           
        }

        /// <summary>
        /// Deletes a given engagement (by ID)
        /// </summary>
        /// <param name="engagementId">The ID of the engagement</param>
        public void Delete(long engagementId)
            => _client.ExecuteOnly(GetRoute<EngagementHubSpotModel>(engagementId.ToString()), method: Method.DELETE);

        /// <summary>
        /// Associates an engagement with a specific object type and ID 
        /// </summary>
        /// <param name="engagementId">The ID of the enagement</param>
        /// <param name="objectType">The object type, e.g CONTACT</param>
        /// <param name="objectId">The ID of the object</param>
        public void Associate(long engagementId, string objectType, long objectId) 
            => _client.ExecuteOnly(GetRoute<EngagementHubSpotModel>(engagementId.ToString(), "associations", objectType, objectId.ToString()), method: Method.PUT);

        /// <summary>
        /// Lists associated engagements for a given object type and ID
        /// </summary>
        /// <param name="objectId">The object ID</param>
        /// <param name="objectType">The object type, e.g. CONTACT</param>
        /// <param name="opts">Options used for querying</param>
        /// <returns>List of associated engagements</returns>
        public EngagementListHubSpotModel<T> ListAssociated<T>(long objectId, string objectType, EngagementListRequestOptions opts = null) where T: EngagementHubSpotModel
        {
            opts = opts ?? new EngagementListRequestOptions();
            
            var path = $"{GetRoute<T>()}/engagements/associated/{objectType}/{objectId}/paged?{QueryParams.LIMIT}={opts.Limit}";

            if (opts.Offset.HasValue)            
                path += $"{QueryParams.OFFSET}={opts.Offset}";

            return _client.Execute<EngagementListHubSpotModel<T>, EngagementListRequestOptions>(path, opts);            
        }
    }
}
