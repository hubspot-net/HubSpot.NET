using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Company
{
    public class CompanyListRequestOptions
    {
        private int _limit = 20;
        public int Limit
        {
            get => _limit;
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentException(
                        $"Number of companies to return must be a positive ingeteger greater than 0 - you provided {value}");
                }
                _limit = value;
            }
        }

        public int? Offset { get; set; } = null;

        public List<string> PropertiesToInclude { get; set; } = new List<string>();
    }

    /// <summary>
    /// Options used when searching for companies by domain.
    /// </summary>
    [DataContract]
    public class CompanySearchByDomain
    {
        [DataMember(Name = "limit")]
        public int Limit { get; set; } = 100;

        [DataMember(Name = "requestOptions")]
        public CompanySearchRequestOptions RequestOptions { get; set; } = new CompanySearchRequestOptions();

        [DataMember(Name = "offset")]
        public CompanySearchOffset Offset { get; set; } = new CompanySearchOffset();

        public string RouteBasePath => "/companies/v2";
        public bool IsNameValue => true;
        public void AcceptHubSpotDataEntity(ref object converted)
        {

        }
    }

    [DataContract]
    public class CompanySearchRequestOptions
    {
        [DataMember(Name = "properties")]
        public List<string> Properties { get; set; } = new List<string> { "domain", "name", "website" };
    }

    [DataContract]
    public class CompanySearchOffset
    {

        [DataMember(Name = "isPrimary")]
        public bool IsPrimary { get; set; } = true;

        [DataMember(Name = "companyId")]
        public long CompanyId { get; set; }
    }

}
