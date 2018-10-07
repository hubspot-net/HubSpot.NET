namespace HubSpot.NET.Api.EmailSubscriptions
{
    using HubSpot.NET.Api.EmailSubscriptions.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Dictionaries;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System.Collections.Generic;
    using System.Linq;

    public class HubSpotEmailSubscriptionsApi : ApiRoutable, IHubSpotEmailSubscriptionsApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/email/public/v1/subscriptions";

        public HubSpotEmailSubscriptionsApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<SubscriptionTimelineHubSpotModel>("timeline");
        }

        /// <summary>
        /// Gets the available email subscription types available in the portal
        /// </summary>
        public SubscriptionTypeListHubSpotModel GetEmailSubscriptionTypes() 
            => _client.Execute<SubscriptionTypeListHubSpotModel>(GetRoute<SubscriptionTypeListHubSpotModel>());

        /// <summary>
        ///     Gets a single subscription type filtered from the list of all subscriptions
        ///     or returns null;
        /// </summary>
        /// <param name="id">The target subscription type's ID</param>
        /// <returns>A SubscriptionType or null</returns>
        public SubscriptionTypeHubSpotModel GetEmailSubscription(long id) 
            => GetEmailSubscriptionTypes().Types.FirstOrDefault(x => x.Id == id);

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
        ///     Unsubscribe the given email address from the given subscription type
        ///     WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id">The ID of the subscription type</param>
        public void UnsubscribeFrom(string email, long id)
        {
            string path = GetRoute<SubscriptionTypeListHubSpotModel>(email);

            SubscriptionStatusHubSpotModel model = new SubscriptionStatusHubSpotModel();
            model.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(id, false));            

            _client.ExecuteOnly(path, model, Method.PUT);
        }

        /// <summary>
        /// Subscribes a contact to all subscription types.
        /// Can only be used when portal's GDPR compliance setting is turned off.
        /// </summary>
        /// <param name="email"></param>
        public void SubscribeAll(string email)
        {
            List<SubscriptionStatusDetailHubSpotModel> subs = new List<SubscriptionStatusDetailHubSpotModel>();
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            GetEmailSubscriptionTypes().Types.ForEach(sub =>
            {
                subs.Add(new SubscriptionStatusDetailHubSpotModel(sub.Id, true));
            });

            subRequest.SubscriptionStatuses.AddRange(subs);

            SendSubscriptionRequest(GetRoute(email), subRequest);
        }

        /// <summary>
        /// Subscribes a contact to all available subscriptions when GDPR compliance is enabled on portal
        /// </summary>
        /// <param name="email">The contact's email address</param>
        /// <param name="legalBasis">Legal Basis for subscribing the contact</param>
        /// <param name="explanation"></param>
        public void SubscribeAll(string email, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN)
        {
            List<SubscriptionStatusDetailHubSpotModel> subs = new List<SubscriptionStatusDetailHubSpotModel>();
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            GetEmailSubscriptionTypes().Types.ForEach(sub =>
            {
                subs.Add(new SubscriptionStatusDetailHubSpotModel(sub.Id, true, optState, legalBasis, explanation));
            });

            subRequest.SubscriptionStatuses.AddRange(subs);

            SendSubscriptionRequest(GetRoute(email), subRequest);
        }



        /// <summary>
        ///     Subscribes a contact to one subscription type by email. Can only be used when portal's GDPR compliance setting is turned off.
        /// </summary>
        /// <param name="email">The contact's email</param>
        /// <param name="id">The Id of the target SubscriptionType</param>
        public void SubscribeTo(string email, long id)
        {
            SubscriptionTypeHubSpotModel singleSub = GetEmailSubscription(id);
            if (singleSub == null)
                throw new KeyNotFoundException("The SubscriptionType ID provided does not exist in the SubscriptionType list");

            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();
            subRequest.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(singleSub.Id, true));

            SendSubscriptionRequest(GetRoute(email), subRequest);
        }

        /// <summary>
        /// Subscribes the contact to a single subscription type along with the OptState and the GDPR Legal Basis for being subscribed.
        /// </summary>
        /// <param name="email">The contact's email</param>
        /// <param name="id">The Subscription Type's ID</param>
        /// <param name="legalBasis">GDPR Legal Basis for subscribing</param>
        /// <param name="explanation">Explanation of GDPR Legal Basis</param>
        /// <param name="optState">The Opt State of the contact's subscription</param>
        public void SubscribeTo(string email, long id, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN)
        {
            SubscriptionTypeHubSpotModel singleSub = GetEmailSubscription(id);
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            if (singleSub == null)
                throw new KeyNotFoundException("The SubscriptionType ID provided does not exist in the SubscriptionType list");

            subRequest.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(singleSub.Id, true, optState, legalBasis, explanation));

            SendSubscriptionRequest(GetRoute(email), subRequest);
        }        

        private void SendSubscriptionRequest(string path, object payload)
            => _client.ExecuteOnly(path, payload, Method.PUT);
    }
}
