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
    /// Returned from HubSpot's GET endpoint
    /// to get the subsctiptions statuses for one email address
    /// </summary>
    [DataContract]
    public class SubscriptionStatusHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "subscribed")]
        public bool Subscribed { get; set; }

        [DataMember(Name = "markedAsSpam")]
        public bool MarkedAsSpam { get;set; }

        [DataMember(Name = "bounced")]
        public bool Bounced { get; set; }

        [DataMember(Name = "email")]
        public string Email { get;set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "subscriptionStatuses")]
        public List<SubscriptionStatusDetailHubSpotModel> SubscriptionStatuses { get; set; } = new List<SubscriptionStatusDetailHubSpotModel>();

        [IgnoreDataMember]
        public bool IsNameValue { get; }        
    }
}
