namespace HubSpot.NET.Api.Timeline.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using System;
    using System.Runtime.Serialization;

    public class TimelineEventTypeHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "applicationId")]
        public long ApplicationId { get; set; }

        [DataMember(Name = "objectType")]
        public dynamic ObjectType { get; set; }

        [DataMember(Name = "headerTemplate")]
        public string HeaderTemplate { get; set; }

        [DataMember(Name = "detailTemplate")]
        public string DetailTemplate { get; set; }

        public string RouteBasePath => "/timeline/event-types";
        public bool IsNameValue => false;

        public void FromHubSpotDataEntity(dynamic hubspotData) { }

        public void ToHubSpotDataEntity(ref dynamic dataEntity) { }
    }
}
