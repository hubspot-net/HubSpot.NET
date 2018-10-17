using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Deal.Dto
{
    /// <summary>
    /// Models a set of results returned when querying for sets of deals
    /// </summary>
    [DataContract]
    public class DealListHubSpotModel<T> : ListHubSpotModel, IHubSpotModel
    {
        /// <summary>
        /// Gets or sets the deals.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        [DataMember(Name = "deals")]
        public IList<T> Deals { get; set; } = new List<T>();        

        public bool IsNameValue => false;
    }
}
