namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
        /// Returned from HubSpot's GET endpoint to view the time-ordered
        /// list of subscriptions for a portal
        /// <para>
        ///     /email/public/v1/subscriptions/timeline
        /// </para>
    /// </summary>
    [DataContract]
    public class SubscriptionTimelineHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "hasMore")]
        public bool HasMore { get; set; }

        [DataMember(Name = "offset")]
        public string Offset { get; set; }

        [DataMember(Name = "timeline")]
        public IOrderedEnumerable<SubscriptionTimelineChangeHubSpotModel> Timeline { get; set; }

        [IgnoreDataMember]
        public bool IsNameValue => false;
    }
}
