using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Contact.Dto
{
    [DataContract]
    public class ContactSearchHubSpotModel<T> : ListHubSpotModel, IHubSpotModel where T : IHubSpotModel
    {

        /// <summary>
        /// Gets or sets the query term used to get the results.
        /// </summary>
        /// <value>
        /// The query term.
        /// </value>
        /// <remarks>
        /// This is a mapping of the "query" prop in the JSON return data from HubSpot
        /// </remarks>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        [DataMember(Name = "contacts")]
        public List<T> Contacts { get; set; } = new List<T>();

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total number of contacts found.
        /// </value>
        /// <remarks>
        /// This is a mapping of the "total" prop in the JSON return data from HubSpot
        /// </remarks>
        [DataMember(Name = "total")]
        public long Total { get; set; }
        public bool IsNameValue => false;
    }
}
