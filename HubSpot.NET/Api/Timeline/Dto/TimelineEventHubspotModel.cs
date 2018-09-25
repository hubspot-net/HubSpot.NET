namespace HubSpot.NET.Api.Timeline.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Models a set of results returned from the Timeline endpoint.
    /// </summary>
    [DataContract]
    public class TimelineEventHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "applicationId")]
        public long ApplicationId { get; set; }

        [DataMember(Name = "portalId")]
        public long PortalId { get; set; }

        [DataMember(Name = "eventTypeId")]
        public long EventTypeId { get; set; }

        [DataMember(Name = "objectType")]
        public string ObjectType { get; set; }

        [DataMember(Name = "objectId")]
        public long ObjectId { get; set; }

        [DataMember(Name = "extraData")]
        public Dictionary<string, string> ExtraData { get; set; }

        [DataMember(Name = "email")]
        public string ContactEmail { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }

        public bool IsNameValue => false;
    }
}
