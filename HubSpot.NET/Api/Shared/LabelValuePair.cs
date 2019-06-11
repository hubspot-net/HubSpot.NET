using System.Runtime.Serialization;

namespace HubSpot.NET
{
    [DataContract]
    public class LabelValuePair : INameValuePair
    {
        [DataMember(Name = "label")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
