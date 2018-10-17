using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface ISearchResults<T> where T : IHubSpotModel
    {
        [DataMember(Name = "results")]
        IList<T> Results { get; set; }
    }

    public interface ISearchResults
    {
        [DataMember(Name = "hasMore")]
        bool MoreResultsAvailable { get; set; }
    }
}
