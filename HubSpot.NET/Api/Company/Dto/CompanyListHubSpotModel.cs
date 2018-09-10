using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Api.Company.Dto
{
    public class CompanyListHubSpotModel<T> : ListHubSpotModel, IHubSpotModel where T: IHubSpotModel
    {
        [DataMember(Name = "companies")]
        public IList<T> Companies { get; set; } = new List<T>();
        public bool IsNameValue => false;        
    }
}
