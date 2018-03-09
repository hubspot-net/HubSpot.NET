using System.Runtime.Serialization;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    public class SubscriptionStatusDetailHubSpotModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "subscribed")]
        public bool Subscribed { get;set; }

        [DataMember(Name = "updatedAt")]
        public string UpdatedAt { get;set; }
    }
}