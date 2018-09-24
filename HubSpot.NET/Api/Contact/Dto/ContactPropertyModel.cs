namespace HubSpot.NET.Api.Contact.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

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

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }

        [DataMember(Name = "displayMode")]
        public string DisplayMode { get; set; }

        [DataMember(Name = "formField")]
        public bool IsFormField { get; set; }

        [DataMember(Name = "deleted")]
        public bool? IsDeleted { get; set; }

        [DataMember(Name = "readOnlyValue")]
        public bool IsValueReadOnly { get; set; }

        [DataMember(Name = "readOnlyDefinition")]
        public bool HasReadonlySettings { get; set; }

        [DataMember(Name = "hidden")]
        public bool IsHidden { get; set; }

        [DataMember(Name = "mutableDefinitionNotDeletable")]
        public bool IsPropertyDeletable { get; set; }

        [DataMember(Name = "favorited")]
        public bool IsFavorited { get; set; }

        [DataMember(Name = "favoritedOrder")]
        public int FavoritedOrder { get; set; }

        [DataMember(Name = "calculated")]
        public bool IsCalculated { get; set; }

        [DataMember(Name = "externalOptions")]
        public bool HasExternalOptions { get; set; }

        [DataMember(Name = "options")]
        public List<ContactPropertyOption> Options { get; set; }

        public bool IsNameValue => false;
    }
}
