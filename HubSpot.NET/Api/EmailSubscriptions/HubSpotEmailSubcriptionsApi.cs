namespace HubSpot.NET.Api.EmailSubscriptions
{
    using System.Collections.Generic;
    using HubSpot.NET.Api.EmailSubscriptions.Dto;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

    /// <summary>
    /// The hub spot email subscriptions api class
    /// </summary>
    /// <seealso cref="IHubSpotEmailSubscriptionsApi"/>
    public class HubSpotEmailSubscriptionsApi : IHubSpotEmailSubscriptionsApi
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly IHubSpotClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubSpotEmailSubscriptionsApi"/> class
        /// </summary>
        /// <param name="client">The client</param>
        public HubSpotEmailSubscriptionsApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the available email subscription types available in the portal
        /// </summary>
        public SubscriptionTypeListHubSpotModel GetEmailSubscriptionTypes()
        {
            var path = $"{new SubscriptionTypeListHubSpotModel().RouteBasePath}/subscriptions";

            return _client.ExecuteList<SubscriptionTypeListHubSpotModel>(path, convertToPropertiesSchema: false);
        }

        /// <summary>
        /// Get subscription status for the given email address
        /// </summary>
        /// <param name="email"></param>
        public SubscriptionStatusHubSpotModel GetStatus(string email)
        {
            var path = $"{new SubscriptionTypeListHubSpotModel().RouteBasePath}/subscriptions/{email}";

            return _client.Execute<SubscriptionStatusHubSpotModel>(path, Method.GET, false);
        }


        /// <summary>
        /// Unsubscribe the given email address from ALL email
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        public void UnsubscribeAll(string email)
        {
            var path = $"{new SubscriptionTypeListHubSpotModel().RouteBasePath}/subscriptions/{email}";

            _client.Execute(path, new { unsubscribeFromAll = true }, Method.PUT, false);
        }

        /// <summary>
        /// Unsubscribe the given email address from the given subscription type
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id">The ID of the subscription type</param>
        public void UnsubscribeFrom(string email, long id)
        {
            var path = $"{new SubscriptionTypeListHubSpotModel().RouteBasePath}/subscriptions/{email}";

            var model = new SubscriptionStatusUpdateHubSpotModel
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



        /// <summary>
        /// Subscribe the given email address to the given subscription type
        /// See <see cref="https://legacydocs.hubspot.com/docs/methods/email/update_status"/>
        /// </summary>        /// <param name="email">The email</param>
        /// <param name="id">The id</param>
        /// <param name="basis">The basis</param>
        /// <param name="basisExplanation">The basis explanation</param>
        /// <param name="setPortalSubscriptionBasis">The set portal subscription basis</param>
        /// <param name="setSubscriptionBasis">The set subscription basis</param>
        /// <param name="subscriptionBasis">The subscription basis</param>
        /// <param name="subscriptionBasisExplanation">The subscription basis explanation</param>
        public void SubscribeTo(string email, long id, string basis, string basisExplanation, bool setPortalSubscriptionBasis = true, bool setSubscriptionBasis = false, string subscriptionBasis = null, string subscriptionBasisExplanation = null)
        {
            var subscription = new SubscribeStatusHubSpotModel()
            {
                Id = id,
                Subscribed = true,
                OptState = "OPT_IN"
            };
            if (setSubscriptionBasis)
            {
                if (string.IsNullOrEmpty(subscriptionBasis))
                {
                    subscriptionBasis = basis;
                }

                if (subscriptionBasisExplanation == null)
                {
                    subscriptionBasisExplanation = basisExplanation;
                }

                subscription.LegalBasis = subscriptionBasis;
                subscription.LegalBasisExplanation = subscriptionBasisExplanation;
            }

            var model = new SubscribeHubSpotModel
            {
                SubscriptionStatuses = new List<SubscribeStatusHubSpotModel>() { subscription }
            };
            if (setPortalSubscriptionBasis)
            {
                model.PortalSubscriptionLegalBasis = basis;
                model.PortalSubscriptionLegalBasisExplanation = basisExplanation;
            }

            var path = $"{model.RouteBasePath}/{email}";

            _client.Execute(path, model, Method.PUT, false);
        }
    }
}