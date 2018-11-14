using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;
using HubSpot.NET.Core.JsonConverters;
using Newtonsoft.Json;

namespace HubSpot.NET.Api.EmailEvents.Dto
{
    [DataContract]
    public class EmailCampaignDataHubSpotModel : IHubSpotModel
    {

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "appId")]
        public long AppId { get; set; }

        [DataMember(Name = "appName")]
        public string AppName { get; set; }

        [DataMember(Name = "contentId")]
        public long ContentId { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "counters")]
        public Dictionary<string, int> Counters { get; set; }

        [DataMember(Name = "lastProcessingFinishedAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTime? LastProcessingFinishedAt { get; set; }

        [DataMember(Name = "lastProcessingStartedAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTime? LastProcessingStartedAt { get; set; }

        [DataMember(Name = "lastProcessingStateChangeAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTime? LastProcessingStateChangeAt { get; set; }

        [DataMember(Name = "numIncluded")]
        public int NumIncluded { get; set; }

        [DataMember(Name = "processingState")]
        public string ProcessingState { get; set; }

        [DataMember(Name = "scheduledAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTime? ScheduledAt { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        public bool IsNameValue => false;

        public string RouteBasePath => "/email/public/v1/campaigns";

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public virtual void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }
    }
}
