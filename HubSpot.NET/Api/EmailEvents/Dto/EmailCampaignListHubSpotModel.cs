using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.EmailEvents.Dto
{
    /// <summary>
    /// Models a set of results returned when querying for campaigns.
    /// </summary>
    [DataContract]
    public class EmailCampaignListHubSpotModel<T> : IHubSpotModel where T: EmailCampaignHubSpotModel
    {

        /// <summary>
        /// Gets or sets the campaign info.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        [DataMember(Name = "campaigns")]
        public IList<T> Campaigns { get; set; } = new List<T>();

        /// <summary>
        /// Gets or sets a value indicating whether more results are available.
        /// </summary>
        /// <value>
        /// <c>true</c> if [more results available]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// This is a mapping of the "hasMore" prop in the JSON return data from HubSpot
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
        /// This is an opaque Base64 encoded string that resisted 5 minutes of attempted interpretation.
        /// Not so much an offset, more like a token.
        /// </remarks>
        [DataMember(Name = "offset")]
        public string ContinuationOffset { get; set; }

        public string RouteBasePath => "/email/public/v1/campaigns";

        public bool IsNameValue => false;

        public virtual void ToHubSpotDataEntity(ref dynamic converted)
        {
        }

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }
    }
}
