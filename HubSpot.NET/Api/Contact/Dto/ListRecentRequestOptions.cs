using HubSpot.NET.Core;
using System.Text;

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

        public override string GetQueryString(string initialValue = "")
        {
            StringBuilder sb = new StringBuilder(initialValue);

            if (Offset.HasValue)
                sb.Append($"&{QueryParams.VID_OFFSET}={Offset.Value}");

            if (!string.IsNullOrEmpty(TimeOffset))
                sb.Append($"&{QueryParams.TIME_OFFSET}={TimeOffset}");

            sb.Append($"&{QueryParams.PROPERTY_MODE}={PropertyMode}");
            sb.Append($"&{QueryParams.FORM_SUBMISSION_MODE}={FormSubmissionMode}");
            sb.Append($"&{QueryParams.SHOW_LIST_MEMBERSHIPS}={ShowListMemberships}");

            return base.GetQueryString(sb.ToString());
        }
    }
}
