using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Abstracts
{
    public abstract class ApiRoutable
    {
        /// <summary>
        /// The route to the HubSpot API appended directly after the base URI
        /// </summary>
        public virtual string MidRoute { get; } = string.Empty;

        /// <summary>
        /// Dictionary of Entity specific routes to be accessed by entity type
        /// </summary>                
        protected virtual Dictionary<Type, string> Routes { get; set; } = new Dictionary<Type, string>();

        /// <summary>
        /// Provides the route to an endpoint relative to the specified type key.
        /// </summary>
        /// <typeparam name="T">The IHubSpotModel-based type key used for the route.</typeparam>
        /// <returns></returns>
        public virtual string GetRoute<T>() where T : IHubSpotModel 
            => $"{MidRoute.TrimEnd('/')}/{(Routes[typeof(T)] ?? string.Empty).TrimStart('/')}";

        /// <summary>
        /// Combines the parameters provided into a full URI with separating '/' characters.
        /// </summary>
        /// <typeparam name="T">The IHubSpotModel-based type key used for the route.</typeparam>
        /// <param name="orderedRouteValues">One or more route parameters to be combined, sans</param>
        /// <returns></returns>
        public virtual string GetRoute<T>(params string[] orderedRouteValues) where T: IHubSpotModel
        {
            string combinedParams = string.Join("/", orderedRouteValues);
            return $"{GetRoute<T>().TrimEnd('/')}/{combinedParams.TrimStart('/')}";
        }

        public void AddRoute<T>(string newRoute) where T : IHubSpotModel 
            => Routes.Add(typeof(T), newRoute);       
    }
}
