using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Contact.Dto
{
    /// <summary>
    /// Models a set of results returned when querying for sets of contacts
    /// </summary>
    [DataContract]
    public class ContactListHubSpotModel<T> : ListHubSpotModel, IHubSpotModel where T: ContactHubSpotModel, new()
    {
        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        [DataMember(Name = "contacts")]
        public IList<T> Contacts { get; set; } = new List<T>();

        public string RouteBasePath => "/contacts/v1";

        public bool IsNameValue => false;
    }
}
