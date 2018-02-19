using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Company.Dto
{
    /// <summary>
    /// Models a set of results returned from the companies endpoint.
    /// </summary>
    [DataContract]
    public class CompanySearchResultModel<T> : IHubSpotModel where T: CompanyHubSpotModel, new()
    {
        [DataMember(Name = "results")]
        public IList<T> Results { get; set; }

        [DataMember(Name = "hasMore")]
        public bool MoreResultsAvailable { get; set; }

        [DataMember(Name="offset")]
        public CompanySearchOffset Offset { get; set; }

        public bool IsNameValue => false;

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public string RouteBasePath => "";
    }
}