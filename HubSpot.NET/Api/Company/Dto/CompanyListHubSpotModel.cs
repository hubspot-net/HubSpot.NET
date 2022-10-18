using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Api.Company.Dto
{
    public class CompanyListHubSpotModel<T> : IHubSpotModel where T: CompanyHubSpotModel, new()
    {
        [DataMember(Name = "companies")]
        public IList<T> Companies { get; set; } = new List<T>();

        public bool IsNameValue => false;

        public string RouteBasePath => "/companies/v2";

        [DataMember(Name = "has-more")]
        public bool MoreResultsAvailable { get; set; }

        [DataMember(Name = "offset")]
        public long ContinuationOffset { get; set; }


        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }
    }
}
