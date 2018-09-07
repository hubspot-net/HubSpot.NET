using HubSpot.NET.Core.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Deal.Dto
{
    /// <summary>
    /// Models a set of results returned when querying for sets of recently created or modified deals
    /// </summary>
    /// <remarks>
    /// The sole reason for this class is because HubSpot uses a different property when returning 
    /// recently created or modified deals versus all deals.
    /// 
    /// With retrieving all deals the deals are returned in the property "deals".
    /// With recent deals the deals are returned in the property "results".
    /// </remarks>
    public class DealRecentListHubSpotModel<T> : DealListHubSpotModel<T>
    {
        /// <summary>
        /// Gets or sets the deals.
        /// </summary>
        /// <value>
        /// The list of deals.
        /// </value>
        [DataMember(Name = "results")]
        public new IList<T> Deals { get; set; } = new List<T>();

    }
}
