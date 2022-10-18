using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Company.Dto
{
    [DataContract]
    public class SearchRequestFilterGroup
    {
        [DataMember(Name = "filters")]
        public IList<SearchRequestFilter> Filters { get; set; }
    }
}