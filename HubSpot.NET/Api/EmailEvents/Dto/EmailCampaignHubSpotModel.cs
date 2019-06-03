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
    public class EmailCampaignHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "appId")]
        public long AppId { get; set; }

        [DataMember(Name = "appName")]
        public string AppName { get; set; }

        [DataMember(Name ="lastUpdatedTime")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTime? LastUpdatedTime { get; set; }

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
