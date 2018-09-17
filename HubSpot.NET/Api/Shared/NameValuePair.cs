using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HubSpot.NET.Api.Shared
{
    [DataContract]
    public class NameValuePair
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
