namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    public class SubscriptionTimelineChangeHubSpotModel
    {
        [DataMember(Name = "change")]
        public string Change { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "subscriptionId")]
        public long SubscriptionId { get; set; }

        [DataMember(Name = "portalId")]
        public long PortalId { get; set; }

        [DataMember(Name = "changeType")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public SubscriptionChangeType ChangeType { get; set; }

        [DataMember(Name = "causedByEvent")]
        public EmailEventHubSpotModel CausedByEvent { get; set; }
        
    }

    public enum SubscriptionChangeType
    {
        SUBSCRIPTION_STATUS,
        PORTAL_STATUS,   
        SUBSCRIPTION_SPAM_REPORT,
        PORTAL_SPAM_REPORT,
        PORTAL_BOUNCE
    }
}
