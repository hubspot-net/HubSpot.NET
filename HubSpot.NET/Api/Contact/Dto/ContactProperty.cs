using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HubSpot.NET.Api.Contact.Dto
{
    [DataContract]
    public class ContactProperty
    {
        public ContactProperty() { }

        public ContactProperty(object value)
        {
            Value = value;
        }

        [DataMember(Name = "value")]
        public object Value { get; set; }

        [DataMember(Name = "versions")]
        List<ContactPropertyVersion> Versions { get; set; } = new List<ContactPropertyVersion>();
    }

    [DataContract]
    public class ContactPropertyVersion
    {
        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "source-type")]
        public string SourceType { get; set; }

        [DataMember(Name = "source-id")]
        public string SourceId { get; set; }

        [DataMember(Name = "source-label")]
        public string SourceLabel { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }

        [DataMember(Name = "selected")]
        public bool Selected { get; set; }
    }
}
