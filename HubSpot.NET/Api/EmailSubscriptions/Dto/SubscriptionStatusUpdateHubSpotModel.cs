using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    /// <summary>
    /// The subscription status update hub spot model class
    /// </summary>
    /// <seealso cref="IHubSpotModel"/>
    public class SubscriptionStatusUpdateHubSpotModel : IHubSpotModel
    {
        /// <summary>
        /// Gets or sets the value of the subscription statuses
        /// </summary>
        [DataMember(Name = "subscriptionStatuses")]
        public List<SubscriptionStatusDetailHubSpotModel> SubscriptionStatuses { get; set; }

        /// <summary>
        /// Gets the value of the is name value
        /// </summary>
        [IgnoreDataMember]
        public bool IsNameValue { get; }

        /// <summary>
        /// Returns the hub spot data entity using the specified data entity
        /// </summary>
        /// <param name="dataEntity">The data entity</param>
        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        /// <summary>
        /// Creates the hub spot data entity using the specified hubspot data
        /// </summary>
        /// <param name="hubspotData">The hubspot data</param>
        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        /// <summary>
        /// Gets the value of the route base path
        /// </summary>
        [IgnoreDataMember]
        public string RouteBasePath => "/email/public/v1";
    }
}