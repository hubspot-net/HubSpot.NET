using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Api.Properties.Dto
{
    public class EnumerationOption
    {
        [DataMember(Name = "label")]
        public string Label {get; set; }

        [DataMember(Name = "value")]
        public string Value {get; set; }
    }
}
