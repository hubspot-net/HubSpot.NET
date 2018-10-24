using HubSpot.NET.Api.EmailSubscriptions.Dto;
using HubSpot.NET.Core.Dictionaries;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotEmailSubscriptionsApi
    {
        /// <summary>
        /// Gets the available email subscription types available in the portal
        /// </summary>
        SubscriptionTypeListHubSpotModel GetSubscriptionTypes();
        /// <summary>
        /// Get subscription status for the given email address
        /// </summary>
        /// <param name="email"></param>
        SubscriptionStatusHubSpotModel GetSubscriptionStatusForContact(string email);
        SubscriptionTimelineHubSpotModel GetChangesTimeline();
        /// <summary>
        ///     Gets a single subscription type filtered from the list of all subscriptions
        ///     or returns null;
        /// </summary>
        /// <param name="id">The target subscription type's ID</param>
        /// <returns>A SubscriptionType or null</returns>
        /// <summary>
        /// Unsubscribe the given email address from ALL email
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email">The email of the contact unsubscribing</param>
        SubscriptionTypeHubSpotModel GetSubscription(long id);
        void UnsubscribeAll(string email);
        /// <summary>
        ///     Unsubscribe the given email address from the given subscription type
        ///     WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id">The ID of the subscription type</param>
        void UnsubscribeFrom(string email, long id);
        /// <summary>
        /// Enables unsubscribing from multiple email subscriptions at once.
        /// </summary>
        /// <param name="email">The contact's email.</param>
        /// <param name="ids">Target Subscription Ids</param>
        void UnsubscribeFrom(string email, params long[] ids);
        /// <summary>
        /// Subscribes a contact to all subscription types.
        /// Can only be used when portal's GDPR compliance setting is turned off.
        /// </summary>
        /// <param name="email"></param>
        void SubscribeAll(string email);
        /// <summary>
        ///     Subscribes a contact to one subscription type by email. Can only be used when portal's GDPR compliance setting is turned off.
        /// </summary>
        /// <param name="email">The contact's email</param>
        /// <param name="id">The Id of the target SubscriptionType</param>
        void SubscribeTo(string email, long id);
        /// <summary>
        /// Enables subscribing to multiple email subscriptions at once.
        /// </summary>
        /// <param name="email">The contact's email.</param>
        /// <param name="ids">Target Subscription Ids</param>
        void SubscribeTo(string email, params long[] ids);
        /// <summary>
        /// Subscribes a contact to all available subscriptions when GDPR compliance is enabled on portal
        /// </summary>
        /// <param name="email">The contact's email address</param>
        /// <param name="legalBasis">Legal Basis for subscribing the contact</param>
        /// <param name="explanation"></param>
        void SubscribeAll(string email, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN);
        /// <summary>
        /// Subscribes the contact to a single subscription type along with the OptState and the GDPR Legal Basis for being subscribed.
        /// </summary>
        /// <param name="email">The contact's email</param>
        /// <param name="id">The Subscription Type's ID</param>
        /// <param name="legalBasis">GDPR Legal Basis for subscribing</param>
        /// <param name="explanation">Explanation of GDPR Legal Basis</param>
        /// <param name="optState">The Opt State of the contact's subscription</param>
        void SubscribeTo(string email, long id, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN);
    }
}