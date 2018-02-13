using System;
using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Deal
{
    public class HubSpotDealApi : IHubSpotDealApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotDealApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a deal entity
        /// </summary>
        /// <typeparam name="T">Implementation of DealHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        public T Create<T>(T entity) where T : DealHubSpotModel, new()
        {
            var path = $"{entity.RouteBasePath}/deal";
            var data = _client.Execute<T>(path, entity, Method.POST);
            return data;
        }

        /// <summary>
        /// Gets a single deal by ID
        /// </summary>
        /// <param name="dealId">ID of the deal</param>
        /// <typeparam name="T">Implementation of DealHubSpotModel</typeparam>
        /// <returns>The deal entity</returns>
        public T GetById<T>(long dealId) where T : DealHubSpotModel, new()
        {
            var path = $"{new T().RouteBasePath}/deal/{dealId}";
            var data = _client.Execute<T>(path, Method.GET);
            return data;
        }

        /// <summary>
        /// Updates a given deal
        /// </summary>
        /// <typeparam name="T">Implementation of DealHubSpotModel</typeparam>
        /// <param name="entity">The deal entity</param>
        /// <returns>The updated deal entity</returns>
        public T Update<T>(T entity) where T : DealHubSpotModel, new()
        {
            if (entity.Id < 1)
            {
                throw new ArgumentException("Deal entity must have an id set!");
            }

            var path = $"{entity.RouteBasePath}/deal/{entity.Id}";

            var data = _client.Execute<T>(path, entity, method: Method.PUT);
            return data;
        }

        /// <summary>
        /// Deletes a given deal (by ID)
        /// </summary>
        /// <param name="dealId">ID of the deal</param>
        public void Delete(long dealId)
        {
            var path = $"{new DealHubSpotModel().RouteBasePath}/deal/{dealId}";

            _client.Execute(path, method: Method.DELETE);
        }
    }
}
