using HubSpot.NET.Core.Dictionaries;
using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    [DataContract]
    public class SubscriptionSubscribeHubSpotModel : IHubSpotModel
    {
        public SubscriptionSubscribeHubSpotModel() { }

        public SubscriptionSubscribeHubSpotModel(GDPRLegalBasis legalBasis, string explanation) : this()
        {
            PortalSubscriptionLegalBasis = GDPRLegalBases.GetBasis(legalBasis);
            PortalSubscriptionLegalBasisExplanation = explanation;
        }

        [DataMember(Name = "subscriptionStatuses")]
        public List<SubscriptionStatusDetailHubSpotModel> SubscriptionStatuses { get; set; } = new List<SubscriptionStatusDetailHubSpotModel>();

        [DataMember(Name = "portalSubscriptionLegalBasis")]
        public string PortalSubscriptionLegalBasis { get; set; }

        [DataMember(Name = "portalSubscriptionLegalBasisExplanation")]
        public string PortalSubscriptionLegalBasisExplanation { get; set; }

        [IgnoreDataMember]
        public bool IsNameValue { get; }
    }
}
