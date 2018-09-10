namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    public class SubscriptionTimelineHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "hasMore")]
        public bool HasMore { get; set; }

        [DataMember(Name = "offset")]
        public string Offset { get; set; }

        [DataMember(Name = "timeline")]
        public IOrderedEnumerable<SubscriptionTimelineChangeHubSpotModel> Timeline { get; set; }

        public bool IsNameValue => throw new NotImplementedException();
    }
}
