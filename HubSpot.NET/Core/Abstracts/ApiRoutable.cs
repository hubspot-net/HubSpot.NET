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
        public virtual string MidRoute { get; protected set; } = string.Empty;

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
        {
            string routeValue = TryGetRouteValue<T>();
            return $"{MidRoute.TrimEnd('/')}/{routeValue.TrimStart('/')}";
        }

        /// <summary>
        /// Provides the route to the midroute endpoint for the DTO group.
        /// This should be used when there is no need to add any parameters
        /// </summary>
        /// <returns>The midroute</returns>
        public virtual string GetRoute() 
            => $"{MidRoute.TrimEnd('/')}";

        /// <summary>
        /// Provides the route to the midroute endpoint for the DTO group,
        /// including the 
        /// </summary>
        /// <param name="param"></param>
        /// <returns>The full route for the request</returns>
        public virtual string GetRoute(params string[] orderedRouteValues)
        {
            string[] orderValuesFiltered = FilterRouteValues(orderedRouteValues);
            string combinedParams = string.Join("/", orderValuesFiltered);
            return $"{GetRoute()}/{combinedParams}";
        }

        /// <summary>
        /// Combines the parameters provided into a full URI with separating '/' characters.
        /// </summary>
        /// <typeparam name="T">The IHubSpotModel-based type key used for the route.</typeparam>
        /// <param name="orderedRouteValues">One or more route parameters to be combined, sans</param>
        /// <returns>The full route for the request</returns>
        public virtual string GetRoute<T>(params string[] orderedRouteValues) where T: IHubSpotModel
        {
            string[] orderValuesFiltered = FilterRouteValues(orderedRouteValues);
            string combinedParams = string.Join("/", orderValuesFiltered);
            return $"{GetRoute<T>().TrimEnd('/')}/{combinedParams}";
        }

        public void AddRoute<T>(string newRoute) where T : IHubSpotModel 
            => Routes.Add(typeof(T), newRoute);

        /// <summary>
        /// Cleans the provided strings to not have any leading or training '/'s
        /// </summary>
        /// <param name="values">The string values to be cleaned</param>
        /// <returns>An array of cleaned string parameters</returns>
        private string[] FilterRouteValues(string[] values)
            => values.Select(x => x.Trim('/')).ToArray();

        private string TryGetRouteValue<T>() where T : IHubSpotModel 
            => Routes.ContainsKey(typeof(T)) ? Routes[typeof(T)] : string.Empty;
    }
}
