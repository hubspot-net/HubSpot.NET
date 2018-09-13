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
            string routeValue = Routes.ContainsKey(typeof(T)) ? Routes[typeof(T)] : string.Empty;
            return $"{MidRoute.TrimEnd('/')}/{routeValue.TrimStart('/')}";
        }

        /// <summary>
        /// Combines the parameters provided into a full URI with separating '/' characters.
        /// </summary>
        /// <typeparam name="T">The IHubSpotModel-based type key used for the route.</typeparam>
        /// <param name="orderedRouteValues">One or more route parameters to be combined, sans</param>
        /// <returns></returns>
        public virtual string GetRoute<T>(params string[] orderedRouteValues) where T: IHubSpotModel
        {
            string[] orderValuesFiltered = orderedRouteValues.Select(x => x.Trim('/')).ToArray();
            string combinedParams = string.Join("/", orderValuesFiltered);
            return $"{GetRoute<T>().TrimEnd('/')}/{combinedParams}";
        }

        public void AddRoute<T>(string newRoute) where T : IHubSpotModel 
            => Routes.Add(typeof(T), newRoute);       
    }
}
