using HubSpot.NET.Core;

namespace HubSpot.NET.Api.Contact.Dto
{
    /// <summary>
    /// Options used when querying for a list matching the query term
    /// </summary>
    public class ContactSearchRequestOptions : ListRequestOptions
    {   
        /// <summary>
        /// Gets or set the query term to use when searching
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or set the internal property name (e.g. vid) and sort contact search results by that field.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// (defaults to "DESC") The order results are ordered by with respect to <see cref="SortBy"/>.
        /// </summary>
        public SortingOrderType Order { get; set; }

        public ContactSearchRequestOptions()
		{
            Order = SortingOrderType.Descending;
        }
    }
}