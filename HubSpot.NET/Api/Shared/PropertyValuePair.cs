namespace HubSpot.NET.Api.Shared
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PropertyValuePair
    {
        [DataMember(Name = "property")]
        public string Property { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
