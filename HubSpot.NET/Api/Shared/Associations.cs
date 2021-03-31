using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HubSpot.NET.Api.Shared
{
    [DataContract]
    public class Associations
    {
        [DataMember(Name = "associatedCompanyIds")]
        public List<long> AssociatedCompanyIds { get; set; }
        [DataMember(Name = "associatedVids")]
        public List<long> AssociatedVids { get; set; }
    }
}
