using HubSpot.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Api.Deal.Dto
{
    public class DealRecentRequestOptions : ListRequestOptions
    {
        public DealRecentRequestOptions(int upperLimit) : base(upperLimit)
        { }

        public DealRecentRequestOptions() : base(100)
        { }

        /// <summary>
        /// Used to specify the oldest timestamp to use to retrieve deals
        /// </summary>
        public string Since { get; set; }

        /// <summary>
        /// Specififes if the current value for a property should be fetched or all historical values
        /// </summary>
        public bool IncludePropertyVersion { get; set; } = false;
    }
}
