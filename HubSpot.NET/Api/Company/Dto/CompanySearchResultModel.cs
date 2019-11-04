using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Company.Dto
{
    /// <summary>
    /// Models a set of results returned from the companies endpoint.
    /// </summary>
    /// 
    [DataContract]
    public class CompanySearchResultModel : CompanySearchResultModel<CompanyHubSpotModel>
    {
        [DataMember(Name = "hasMore")]
        public bool MoreResultsAvailable { get; set; }

        [DataMember(Name="offset")]
        public CompanySearchOffset Offset { get; set; }
    }

    public class CompanySearchResultModel<T> where T : IHubSpotModel
    {
        [DataMember(Name = "results")]
        public IList<T> Results { get; set; }
    }
}