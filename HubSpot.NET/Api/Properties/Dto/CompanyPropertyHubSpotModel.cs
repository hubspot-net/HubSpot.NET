using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Properties.Dto
{
    public class CompanyPropertyHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "name")]
        public string Name { get;set; }

        [DataMember(Name = "label")]
        public string Label { get;set; }

        [DataMember(Name = "description")]
        public string Description { get;set; }

        [DataMember(Name = "groupName")]
        public string GroupName { get;set; }

        [DataMember(Name = "type")]
        public string Type { get;set; }

        [DataMember(Name = "fieldType")]
        public string FieldType { get;set; }

        [DataMember(Name = "options")]
        public List<EnumerationOption> Options { get;set; }

        public bool IsNameValue => false;       
    }
}
