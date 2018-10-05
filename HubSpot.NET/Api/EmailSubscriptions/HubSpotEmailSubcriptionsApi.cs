namespace HubSpot.NET.Api.EmailSubscriptions
{
    using HubSpot.NET.Api.EmailSubscriptions.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System.Collections.Generic;

    public class HubSpotEmailSubscriptionsApi : ApiRoutable, IHubSpotEmailSubscriptionsApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/email/public/v1";

        public HubSpotEmailSubscriptionsApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<SubscriptionTypeListHubSpotModel>("subscriptions");
            AddRoute<SubscriptionStatusHubSpotModel>("subscriptions");
            AddRoute<SubscriptionTimelineHubSpotModel>("subscriptions/timeline");

        }

        /// <summary>
        /// Gets the available email subscription types available in the portal
        /// </summary>
        public SubscriptionTypeListHubSpotModel GetEmailSubscriptionTypes() 
            => _client.Execute<SubscriptionTypeListHubSpotModel>(GetRoute<SubscriptionTypeListHubSpotModel>());

        /// <summary>
        /// Get subscription status for the given email address
        /// </summary>
        /// <param name="email"></param>
        public SubscriptionStatusHubSpotModel GetStatus(string email) 
            => _client.Execute<SubscriptionStatusHubSpotModel>(GetRoute<SubscriptionTypeListHubSpotModel>(email));


        /// <summary>
        /// Gets the timeline of subscription events for the portal
        /// </summary>
        /// <returns>An offset-based list of subscription change events.</returns>
        public SubscriptionTimelineHubSpotModel GetChangesTimeline()
            => _client.Execute<SubscriptionTimelineHubSpotModel>(GetRoute<SubscriptionTimelineHubSpotModel>());
        

        /// <summary>
        /// Unsubscribe the given email address from ALL email
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        public void UnsubscribeAll(string email) 
            => _client.ExecuteOnly(GetRoute<SubscriptionTypeListHubSpotModel>(email), new { unsubscribeFromAll = true }, Method.PUT);

        /// <summary>
        /// Unsubscribe the given email address from the given subscription type
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id">The ID of the subscription type</param>
        public void UnsubscribeFrom(string email, long id)
        {
            string path = GetRoute<SubscriptionTypeListHubSpotModel>(email);

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

            _client.ExecuteOnly(path, model, Method.PUT);
        }

        // TODO Add subscription capabilities
        //public void SubscribeAll(string email)
        //{
        //    string path = GetRoute<SubscriptionTypeListHubSpotModel>(email);

        //    SubscriptionTypeListHubSpotModel subs = GetEmailSubscriptionTypes();


        //}

        //public void SubscribeTo(string email, long id)
        //{
        //    string path = GetRoute<SubscriptionTypeListHubSpotModel>(email);

        //}
    }
}
