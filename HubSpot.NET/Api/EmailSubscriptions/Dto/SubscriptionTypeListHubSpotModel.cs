using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    /// <summary>
    /// Returned from the GET all HubSpot endpoint for subscriptions
    ///     <para>
    ///         /email/public/v1/subscriptions
    ///     </para>
    /// </summary>
    [DataContract]
    public class SubscriptionTypeListHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "subscriptionDefinitions")]
        public List<SubscriptionTypeHubSpotModel> Types { get; set; } = new List<SubscriptionTypeHubSpotModel>();

        [IgnoreDataMember]
        public bool IsNameValue => false;

        internal SubscriptionTypeHubSpotModel Where()
        {
            throw new NotImplementedException();
        }
    }
}
