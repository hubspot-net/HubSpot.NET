using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core;

namespace HubSpot.NET.Api.Company.Dto
{
    /// <summary>
    /// Options used when querying for a list matching the query term
    /// </summary>
    [DataContract]
    public class CompanySearchRequestOptions : ListRequestOptions
    {
        /// <summary>
        /// Gets or set the query term to use when searching
        /// </summary>
        [DataMember(Name = "filterGroups")]
        public IList<SearchRequestFilterGroup> FilterGroups { get; set; }

        private int _limit = 20;
        private readonly int _upperLimit;

        /// <summary>
        /// Gets or sets the number of items to return.
        /// </summary>
        /// <remarks>
        /// Defaults to 20 which is also the HubSpot API default. Max value is 100
        /// </remarks>
        /// <value>
        /// The number of items to return.
        /// </value>
        [DataMember(Name = "limit")]
        public int Limit
        {
            get => _limit;
            set
            {
                if (value < 1 || value > _upperLimit)
                {
                    throw new ArgumentException(
                        $"Number of items to return must be a positive integer greater than 0, and less than {_upperLimit} - you provided {value}");
                }
                _limit = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HubSpot.NET.Core.ListRequestOptions"/> class.
        /// </summary>
        /// <param name="upperLimit">Upper limit for the amount of items to request for the list.</param>
        public CompanySearchRequestOptions(int upperLimit)
        {
            _upperLimit = upperLimit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HubSpot.NET.Core.ListRequestOptions"/> class.
        /// Sets the upper limit to 100 
        /// </summary>
        public CompanySearchRequestOptions()
            : this(100)
        {

        }

        /// <summary>
        /// Get or set the continuation offset when calling list many times to enumerate all your items
        /// </summary>
        /// <remarks>
        /// The return DTO from List contains the current "offset" that you can inject into your next list call 
        /// to continue the listing process
        /// </remarks>
        [DataMember(Name = "after")]
        public string Offset { get; set; } = null;

        [DataMember(Name = "properties")]
        public List<string> PropertiesToInclude { get; set; }
    }
}