using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
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

        public bool IsNameValue { get; }

        public string RouteBasePath => "/email/public/v1";
    }
}
