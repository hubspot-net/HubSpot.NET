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

        public virtual string GetRoute<T>() where T : IHubSpotModel 
            => MidRoute + (Routes[typeof(T)] ?? string.Empty);

        public void AddRoute<T>(string newRoute) where T : IHubSpotModel 
            => Routes.Add(typeof(T), newRoute);       
    }
}
