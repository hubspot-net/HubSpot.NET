using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Engagement.Dto
{
    /// <summary>
    /// Models a set of engagements returned by the API
    /// </summary>
    [DataContract]
    public class EngagementListHubSpotModel<T> : ListHubSpotModel, IHubSpotModel where T: EngagementHubSpotModel
    {
        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        [DataMember(Name = "results")]
        public IList<T> Engagements { get; set; } = new List<T>();        
        public bool IsNameValue => false;
    }
}
