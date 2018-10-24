using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    /// <summary>
    /// A single instance of a subscription type
    /// </summary>
    [DataContract]
    public class SubscriptionTypeHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "active")]
        public bool Active { get; set; }

        [DataMember(Name = "description")]
        public string Description { get;set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get;set; }

        [IgnoreDataMember]
        public bool IsNameValue { get; }        
    }
}
