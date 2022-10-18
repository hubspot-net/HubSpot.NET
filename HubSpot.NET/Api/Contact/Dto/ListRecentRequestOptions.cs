using HubSpot.NET.Core;
using System.Collections.Generic;

namespace HubSpot.NET.Api.Contact.Dto
{
    /// <summary>
    /// Options used when querying for lists of items.
    /// </summary>
    public class ListRecentRequestOptions : ListRequestOptions
    {
        /// <summary>
        /// Used for pagination
        /// </summary>
        public string TimeOffset { get; set; }

        /// <summary>
        /// Specififes if the current value for a property should be fetched or all historical values
        /// </summary>
        public string PropertyMode { get; set; } = "value_only";

        /// <summary>
        /// Specifies if all/none/newest/oldest form submissions should be fetched
        /// </summary>
        public string FormSubmissionMode { get; set; } = "newest";

        /// <summary>
        /// Whether to retrieve current list memberships for the contacts
        /// </summary>
        public bool ShowListMemberships { get; set; } = false;

        public ListRecentRequestOptions()
		{
            PropertiesToInclude = new List<string> { "createdate", "lastmodifieddate", "firstname", "lastname", "email", "company" };
		}
    }
}
