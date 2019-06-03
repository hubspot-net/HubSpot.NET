using System;
using System.Collections.Generic;
using System.Linq;

namespace HubSpot.NET.Api.EmailEvents
{
    /// <summary>
    /// Options used when querying for email campaigns.
    /// </summary>
    public class EmailCampaignListRequestOptions
    {
        private int _limit = 20;

        /// <summary>
        /// Gets or sets the number of campaigns to return.
        /// </summary>
        /// <remarks>
        /// Defaults to 20 which is also the hubspot api default. Max value is 100
        /// </remarks>
        /// <value>
        /// The number of contacts to return.
        /// </value>
        public int Limit
        {
            get => _limit;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException(
                        $"Number of campaigns to return must be a positive integer greater than 0 - you provided {value}");
                }
                _limit = value;
            }
        }

        /// <summary>
        /// Get or set the continuation offset when calling list many times to enumerate all your campaigns.
        /// </summary>
        /// <remarks>
        /// The return DTO from List contains the "offset" string, an opaque, Base64-encoded value that you must inject into your next list call 
        /// to continue the listing process
        /// </remarks>
        public string Offset { get; set; } = null;
    }
}
