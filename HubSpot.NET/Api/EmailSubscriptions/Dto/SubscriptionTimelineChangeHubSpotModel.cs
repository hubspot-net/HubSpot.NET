namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime.Serialization;

    /// <summary>
    ///     This DTO will be used when returning the timeline of subscriptions.
    ///     It should be listed in the "changes" object.
    ///     <para>
    ///         For reference: <see href="https://developers.hubspot.com/docs/methods/email/get_subscriptions_timeline"/>
    ///     </para>
    /// </summary>
    [DataContract]
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
        [JsonConverter(typeof(StringEnumConverter))]
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
