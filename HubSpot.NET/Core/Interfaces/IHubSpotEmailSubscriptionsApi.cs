using HubSpot.NET.Api.EmailSubscriptions.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotEmailSubscriptionsApi
    {
        /// <summary>
        /// Gets the available email subscription types available in the portal
        /// </summary>
        SubscriptionTypeListHubSpotModel GetEmailSubscriptionTypes();
        /// <summary>
        /// Get subscription status for the given email address
        /// </summary>
        /// <param name="email"></param>
        SubscriptionStatusHubSpotModel GetStatus(string email);
        /// <summary>
        /// Unsubscribe the given email address from ALL email
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        void UnsubscribeAll(string email);
        /// <summary>
        /// Unsubscribe the given email address from the given subscription type
        /// WARNING: There is no UNDO for this operation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id">The ID of the subscription type</param>
        void UnsubscribeFrom(string email, long id);

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
        void SubscribeTo(string email, long id, string basis, string basisExplanation, bool setPortalSubscriptionBasis = true, bool setSubscriptionBasis = false, string subscriptionBasis = null, string subscriptionBasisExplanation = null);
    }
}