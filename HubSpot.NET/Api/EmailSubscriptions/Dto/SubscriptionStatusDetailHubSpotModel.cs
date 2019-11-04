using HubSpot.NET.Core.Dictionaries;
using HubSpot.NET.Core.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    /// <summary>
    /// This class is intended for use with HubSpot's PUT endpoint
    /// to update the subscription status of a single email. This includes subscribing and unsubscribing.
    ///     <para>
    ///         /email/public/v1/subscriptions/:email_address
    ///     </para>
    /// </summary>
    [DataContract]
    public class SubscriptionStatusDetailHubSpotModel : IHubSpotModel
    {
        public SubscriptionStatusDetailHubSpotModel() { }

        public SubscriptionStatusDetailHubSpotModel(long id, bool subscribed) : this()
        {
            Id = id;
            Subscribed = subscribed;
        }

        /// <summary>
        ///     The basic SubscriptionStatusDetail, for use when GDPR compliance is not enabled on portal
        /// </summary>
        /// <param name="id">The target subscription's ID</param>
        /// <param name="subscribed">Whether or not the contact is subscribing (if false, they are unsubscribing)</param>
        /// <param name="optState">The OptState of the contact for this subscription</param>
        public SubscriptionStatusDetailHubSpotModel(long id, bool subscribed, OptState optState) : this(id, subscribed)
        {
            OptInState = OptStates.GetState(optState);
        }

        /// <summary>
        /// Used when GDPR compliance is enabled on portal
        /// </summary>
        /// <param name="id">The target subscription's ID</param>
        /// <param name="subscribed">Whether or not the contact is subscribing (if false, they are unsubscribing)</param>
        /// <param name="optState">The OptState of the contact for this subscription</param>
        /// <param name="legalBasis">The legal basis on which the contact can be added to this subscription</param>
        /// <param name="explanation">A brief explanation of why they should be subscribed</param>
        public SubscriptionStatusDetailHubSpotModel(long id, bool subscribed, OptState optState, GDPRLegalBasis legalBasis, string explanation) : this(id, subscribed, optState)
        {
            LegalBasis = GDPRLegalBases.GetBasis(legalBasis);
            LegalBasisExplanation = explanation;
        }
                     
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "subscribed")]
        public bool Subscribed { get;set; }

        [DataMember(Name = "optState")]
        public string OptInState { get; set; }

        [DataMember(Name = "legalBasis")]
        public string LegalBasis { get; set; }

        [DataMember(Name = "legalBasisExplanation")]
        public string LegalBasisExplanation { get; set; }

        [IgnoreDataMember]
        public bool IsNameValue { get; }
    }
}