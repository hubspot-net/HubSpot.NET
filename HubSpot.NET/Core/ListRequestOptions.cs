using System;
using System.Collections.Generic;

namespace HubSpot.NET.Core
{
    /// <summary>
    /// Options used when querying for lists of items.
    /// </summary>
    public class ListRequestOptions
    {
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
        public ListRequestOptions(int upperLimit)
        {
            _upperLimit = upperLimit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HubSpot.NET.Core.ListRequestOptions"/> class.
        /// Sets the upper limit to 100 
        /// </summary>
        public ListRequestOptions()
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
        public long? Offset { get; set; } = null;

        public List<string> PropertiesToInclude { get; set; } = new List<string>();
    }
}
