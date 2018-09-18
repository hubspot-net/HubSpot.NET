namespace HubSpot.NET.Api.Contact.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    public class ContactPropertyModel : IHubSpotModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "groupName")]
        public string PropertyGroup { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "fieldType")]
        public string FieldType { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "options")]
        public LabelValuePair[] Options { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }

        [DataMember(Name = "formField")]
        public bool IsFormField { get; set; }

        public bool IsNameValue => false;
    }
}
