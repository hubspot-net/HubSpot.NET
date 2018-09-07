using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Api.EmailSubscriptions.Dto;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.EmailSubscriptions
{
    public class HubSpotEmailSubscriptionsApi : ApiRoutable, IHubSpotEmailSubscriptionsApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/email/public/v1";

        public HubSpotEmailSubscriptionsApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the available email subscription types available in the portal
        /// </summary>
        public SubscriptionTypeListHubSpotModel GetEmailSubscriptionTypes() 
            => _client.ExecuteList<SubscriptionTypeListHubSpotModel>($"{GetRoute<SubscriptionTypeListHubSpotModel>()}/subscriptions", 
                                                                        convertToPropertiesSchema: false);

        /// <summary>
        /// Get subscription status for the given email address
        /// </summary>
        /// <param name="email"></param>
        public SubscriptionStatusHubSpotModel GetStatus(string email) 
            => _client.Execute<SubscriptionStatusHubSpotModel>($"{GetRoute<SubscriptionTypeListHubSpotModel>()}/subscriptions/{email}",
                                                                    convertToPropertiesSchema: false);

        /// <summary>
        /// Unsubscribe the given email address from ALL email
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        public void UnsubscribeAll(string email) 
            => _client.Execute($"{GetRoute<SubscriptionTypeListHubSpotModel>()}/subscriptions/{email}",
                                    new { unsubscribeFromAll = true }, Method.PUT, false);

        /// <summary>
        /// Unsubscribe the given email address from the given subscription type
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id">The ID of the subscription type</param>
        public void UnsubscribeFrom(string email, long id)
        {
            var path = $"{GetRoute<SubscriptionTypeListHubSpotModel>()}/subscriptions/{email}";

            SubscriptionStatusHubSpotModel model = new SubscriptionStatusHubSpotModel
            {
                SubscriptionStatuses = new List<SubscriptionStatusDetailHubSpotModel>()
                {
                    new SubscriptionStatusDetailHubSpotModel()
                    {
                        Id = id,
                        Subscribed = false
                    }
                }
            };

            _client.Execute(path, model, Method.PUT, false);
        }
    }
}
