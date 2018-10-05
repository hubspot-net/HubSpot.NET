using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HubSpot.NET.Api.Timeline.Dto
{
    [DataContract]
    public class CreateTimelineEventModel : IHubSpotModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "eventTypeId")]
        public long EventTypeId { get; set; }

        [DataMember(Name = "extraData")]
        public Dictionary<string, string> ExtraData { get; set; }

        [DataMember(Name = "email")]
        public string ContactEmail { get; set; }

        public bool IsNameValue => false;

        public CreateTimelineEventModel() { }

        public CreateTimelineEventModel(long EventTypeId, long EventId, string Email)
        {
            Id = EventId;
            this.EventTypeId = EventTypeId;
            ContactEmail = Email;

        }

        public CreateTimelineEventModel(long EventTypeId, long EventId, string Email, Dictionary<string,string> ExtraData) : this(EventTypeId, EventId, Email)
        {
            this.ExtraData = ExtraData;
        }
    }
}
