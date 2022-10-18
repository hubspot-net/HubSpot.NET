using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Engagement.Dto
{
    /// <summary>
    /// Models a set of engagements returned by the API
    /// </summary>
    [DataContract]
    public class EngagementListHubSpotModel<T> : IHubSpotModel where T: EngagementHubSpotModel, new()
    {
        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        [DataMember(Name = "results")]
        public IList<T> Engagements { get; set; } = new List<T>();

        /// <summary>
        /// Gets or sets a value indicating whether more results are available.
        /// </summary>
        /// <value>
        /// <c>true</c> if [more results available]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// This is a mapping of the "has-more" prop in the JSON return data from HubSpot
        /// </remarks>
        [DataMember(Name = "hasMore")]
        public bool MoreResultsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the continuation offset.
        /// </summary>
        /// <value>
        /// The continuation offset.
        /// </value>
        /// <remarks>
        /// This is a mapping of the "vid-offset" prop in the JSON reeturn data from HubSpot
        /// </remarks>
        [DataMember(Name = "offset")]
        public long ContinuationOffset { get; set; }

        public string RouteBasePath => "";

        public bool IsNameValue => false;
        public virtual void ToHubSpotDataEntity(ref dynamic converted)
        {
        }

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }
    }
}
