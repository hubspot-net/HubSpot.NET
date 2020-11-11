using System.Collections.Generic;
using System.Runtime.Serialization;

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
