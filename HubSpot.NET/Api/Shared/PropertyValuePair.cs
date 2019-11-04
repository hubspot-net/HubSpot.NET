namespace HubSpot.NET.Api.Shared
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PropertyValuePair
    {
        public PropertyValuePair() { }
        public PropertyValuePair(string prop, string value) : this()
        {
            Property = prop;
            Value = value;
        }

        [DataMember(Name = "property")]
        public string Property { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        public override string ToString() => $"{Property}: {Value}";
    }
}
