using System.Runtime.Serialization;

namespace HubSpot.NET
{
    [DataContract]
    public class NameValuePair : INameValuePair
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
