namespace HubSpot.NET
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PropertyValuePair : INameValuePair
    {
        public PropertyValuePair() { }
        public PropertyValuePair(string prop, string value) : this()
        {
            Name = prop;
            Value = value;
        }

        [DataMember(Name = "property")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        public override string ToString() => $"{Name}: {Value}";
    }
}
