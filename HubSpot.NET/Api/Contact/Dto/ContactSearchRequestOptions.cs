using HubSpot.NET.Core;
using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Contact.Dto
{
    /// <summary>
    /// Options used when querying for a list matching the query term
    /// </summary>
    [DataContract]
    public class ContactSearchRequestOptions : ListRequestOptions
    {   
        /// <summary>
        /// Gets or set the query term to use when searching
        /// </summary>
        public string Query { get; set; }

    }
}