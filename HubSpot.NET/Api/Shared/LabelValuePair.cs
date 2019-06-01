using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Shared
{
    [DataContract]
    public class LabelValuePair
    {
        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
