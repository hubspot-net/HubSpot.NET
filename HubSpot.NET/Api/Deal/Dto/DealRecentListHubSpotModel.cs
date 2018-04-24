using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Deal.Dto
{
    /// <summary>
    /// Models a set of results returned when querying for sets of deals
    /// </summary>
    /// <remarks>
    /// The sole reason for this class is because HubSpot returns the list for recently modified/created deals
    /// in a 'results' property instead of the 'deals' property used when retrieving all deals.
    /// </remarks>
    [DataContract]
    public class DealRecentListHubSpotModel<T> : DealListHubSpotModel<T>, IHubSpotModel where T: DealHubSpotModel, new()
    {
        /// <summary>
        /// Gets or sets the deals.
        /// </summary>
        /// <value>
        /// The deals.
        /// </value>
        [DataMember(Name = "results")]
        public new IList<T> Deals { get; set; } = new List<T>();
    }
}
