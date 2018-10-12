namespace HubSpot.NET.Api.Contact.Dto
{
    using HubSpot.NET.Core.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Models a Contact entity within HubSpot. Default properties are included here
    /// with the intention that you'd extend this class with properties specific to 
    /// your HubSpot account.
    /// </summary>
    [DataContract]
    public class ContactHubSpotModel : IHubSpotModel
    {
        /// <summary>
        /// Contacts unique ID in HubSpot
        /// </summary>
        [DataMember(Name = "vid")]
        [IgnoreDataMember]
        public long? Id { get; set; }

        private string _Email;
        [DataMember(Name = "email")]
        public string Email {
            set {
                _Email = value;
                if (Properties.ContainsKey("email"))
                    Properties["email"].Value = value;
            }
            get
            {
                if (string.IsNullOrWhiteSpace(_Email))
                { _Email = Properties.ContainsKey("email") ? Properties["email"].Value : string.Empty; }
                return _Email;
            }
        }

        private string _FirstName;
        [DataMember(Name = "firstname")]
        public string FirstName {
            set {
                _FirstName = value;
                if (Properties.ContainsKey("firstname"))
                    Properties["firstname"].Value = value;
            }
            get
            {
                if(string.IsNullOrWhiteSpace(_FirstName))
                { _FirstName = Properties.ContainsKey("firstname") ? Properties["firstname"].Value : string.Empty; }
                return _FirstName;
            }
        }

        private string _LastName;
        [DataMember(Name = "lastname")]
        public string LastName {
            set {
                _LastName = value;
                if (Properties.ContainsKey("lastname"))
                    Properties["lastname"].Value = value;
            }
            get
            {
                if(string.IsNullOrWhiteSpace(_LastName))
                { _LastName = Properties.ContainsKey("lastname") ? Properties["lastname"].Value : string.Empty; }
                return _LastName;
            }
        }

        private string _Website;
        [DataMember(Name = "website")]
        public string Website {
            set {
                _Website = value;
                if (Properties.ContainsKey("website"))
                    Properties["website"].Value = value;
            }
            get
            {
                if(string.IsNullOrWhiteSpace(_Website))
                { _Website = Properties.ContainsKey("website") ? Properties["website"].Value : string.Empty; }
                return _Website;
            }
        }

        private string _Company;
        [DataMember(Name = "company")]
        public string Company {
            set {
                _Company = value;
                if (Properties.ContainsKey("company"))
                    Properties["company"].Value = value;
            }
            get
            {
                if(string.IsNullOrWhiteSpace(_Company))
                { _Company = Properties.ContainsKey("company") ? Properties["company"].Value : string.Empty; }
                return _Company;
            }
        }

        private string _Phone;
        [DataMember(Name = "phone")]
        public string Phone {
            set {
                _Phone = value;
                if (Properties.ContainsKey("phone"))
                    Properties["phone"].Value = value;
            }
            get
            {
                if(string.IsNullOrWhiteSpace(_Phone))
                { _Phone = Properties.ContainsKey("phone") ? Properties["phone"].Value : string.Empty; }
                return _Phone;
            }
        }

        private string _Address;
        [DataMember(Name = "address")]
        public string Address {
            set {
                _Address = value;
                if (Properties.ContainsKey("address"))
                    Properties["address"].Value = value;
            }
            get
            {
                if(string.IsNullOrWhiteSpace(_Address))
                { _Address = Properties.ContainsKey("address") ? Properties["address"].Value : string.Empty; }
                return _Address;
            }
        }

        private string _City;
        [DataMember(Name = "city")]
        public string City {
            set {
                _City = value;
                if (Properties.ContainsKey("city"))
                    Properties["city"].Value = value;
            }
            get
            {
                if (string.IsNullOrWhiteSpace(_City))
                { _City = Properties.ContainsKey("city") ? Properties["city"].Value : string.Empty; }
                return _City;
            }
        }

        private string _State;
        [DataMember(Name = "state")]
        public string State {
            set {
                _State = value;
                if (Properties.ContainsKey("state"))
                    Properties["state"].Value = value;
            }
            get
            {
                if(string.IsNullOrWhiteSpace(_State))
                { _State = Properties.ContainsKey("state") ? Properties["state"].Value : string.Empty; }
                return _State;
            }
        }

        private string _ZipCode;
        [DataMember(Name = "zip")]
        public string ZipCode {
            set {
                _ZipCode = value;
                if (Properties.ContainsKey("zip"))
                    Properties["zip"].Value = value;
            }
            get {
                if (string.IsNullOrWhiteSpace(_ZipCode))
                { _ZipCode = Properties.ContainsKey("zip") ? Properties["zip"].Value : string.Empty; }      
                return _ZipCode;
            } }

        [DataMember(Name = "associatedcompanyid")]
        public long? AssociatedCompanyId { get; set; }

        [DataMember(Name = "hubspot_owner_id")]
        public long? OwnerId { get; set; }

        [DataMember(Name = "properties")]
        public Dictionary<string, ContactProperty> Properties { get; set; } = new Dictionary<string, ContactProperty>();
        public bool IsNameValue => false;
    }
}