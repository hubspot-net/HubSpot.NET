using System;

namespace HubSpot.NET.Api.ContactList
{
    public class ListOptions
    {
        /// <summary>
        /// Get or set the continuation offset when calling list many times to enumerate all your items
        /// </summary>
        /// <remarks>
        /// The return DTO from List contains the current "offset" that you can inject into your next list call 
        /// to continue the listing process
        /// </remarks>
        public virtual long? Offset { get; set; } = null;

        private int _limit = 20;
        private readonly int _upperLimit = 250;

        /// <summary>
        /// Gets or sets the number of items to return.
        /// </summary>
        /// <remarks>
        /// Defaults to 20 which is also the HubSpot API default. Max value is 100
        /// </remarks>
        /// <value>
        /// The number of items to return.
        /// </value>
        public virtual int Limit
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
    }
}