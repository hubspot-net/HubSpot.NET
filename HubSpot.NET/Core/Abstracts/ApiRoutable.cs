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
        public virtual Dictionary<IHubSpotModel, string> Routes { get; set; } = new Dictionary<IHubSpotModel, string>();

        public abstract string GetRoute<T>(T entity) where T : IHubSpotModel;
        public abstract void AddRoute<T>(string newRoute) where T: IHubSpotModel;
        public void AddRoute<T>(T entity, string newRoute) where T : IHubSpotModel
            => AddRoute<T>(newRoute);        
    }
}
