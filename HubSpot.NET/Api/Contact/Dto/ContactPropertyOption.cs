namespace HubSpot.NET.Api.Contact.Dto
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ContactPropertyOption
    {
        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "doubleData")]
        public string DoubleData { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "readonly")]
        public bool? ReadOnly { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }

        [DataMember(Name = "hidden")]
        public bool IsHidden { get; set; }
    }
}
