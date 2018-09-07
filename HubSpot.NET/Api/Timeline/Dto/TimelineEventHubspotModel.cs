namespace HubSpot.NET.Api.Timeline.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using System.Runtime.Serialization;

    /// <summary>
    /// Models a set of results returned from the Timeline endpoint.
    /// </summary>
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
        public dynamic ExtraData { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }

        public string RouteBasePath => "/timeline/event";
        public bool IsNameValue => false;

        public void FromHubSpotDataEntity(dynamic hubspotData) { }

        public void ToHubSpotDataEntity(ref dynamic dataEntity) { }
    }
}
