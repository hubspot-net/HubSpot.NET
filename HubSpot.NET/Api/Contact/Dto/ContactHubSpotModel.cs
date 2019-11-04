namespace HubSpot.NET.Api.Contact.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Models a Contact entity within HubSpot. Default properties are included here
    /// with the intention that you'd extend this class with properties specific to 
    /// your HubSpot account.
    /// </summary>
    [DataContract]
    public class ContactHubSpotModel : IHubSpotModel
    {
        public ContactHubSpotModel() { }

        /// <summary>
        /// Contacts unique ID in HubSpot
        /// </summary>
        [DataMember(Name = "vid")]
        [IgnoreDataMember]
        public long? Id { get; set; }
        
        [DataMember(Name = "email")]
        public string Email { get; set; }
        
        [DataMember(Name = "firstname")]
        public string FirstName { get; set; }
        
        [DataMember(Name = "lastname")]
        public string LastName { get; set; }
        
        [DataMember(Name = "website")]
        public string Website { get; set; }
        [DataMember(Name = "company")]
        public string Company { set; get; }
        [DataMember(Name = "phone")]
        public string Phone { set; get; }
        [DataMember(Name = "address")]
        public string Address { set; get; }
        [DataMember(Name = "city")]
        public string City { set; get; }
        [DataMember(Name = "state")]
        public string State { set; get; }
        [DataMember(Name = "zip")]
        public string ZipCode { set; get; }

        [DataMember(Name = "associatedcompanyid")]
        public long? AssociatedCompanyId { get; set; }

        [DataMember(Name = "hubspot_owner_id")]
        public long? OwnerId { get; set; }

        // Used for return values
        [DataMember(Name = "properties")]
        public Dictionary<string, ContactProperty> Properties { get; set; } = new Dictionary<string, ContactProperty>();

        /// <summary>
        /// Loads all properties into the ContactProperty Dictionary even from custom contact models
        /// </summary>
        public void LoadProperties()
        {
            PropertyInfo[] properties = GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                var key = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;
                object value = prop.GetValue(this);

                if (value == null || key == null || key.Name == "properties")
                    continue;

                Properties.Add(key.Name, new ContactProperty(value));
            }
        }

        [IgnoreDataMember]
        public bool IsNameValue => false;
    }
}