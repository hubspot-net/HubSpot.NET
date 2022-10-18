using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.EmailSubscriptions.Dto
{
    public class SubscribeHubSpotModel : IHubSpotModel
    {
        
        [DataMember(Name = "portalSubscriptionLegalBasis")]
        public string PortalSubscriptionLegalBasis { get;set; }

        [DataMember(Name = "portalSubscriptionLegalBasisExplanation")]
        public string PortalSubscriptionLegalBasisExplanation { get; set; }
        
        [IgnoreDataMember]
        [Obsolete( "This property is obsolete. Use PortalSubscriptionLegalBasis instead.",false)]
        public string SubscriptionLegalBasis {
            get
            {
                return PortalSubscriptionLegalBasis;
            }
            set
            {
                PortalSubscriptionLegalBasis = value;
            }
        }

        [IgnoreDataMember]
        [Obsolete( "This property is obsolete. Use PortalSubscriptionLegalBasisExplanation instead.",false)]
        public string SubscriptionLegalBasisExplanation {
            get
            {
                return PortalSubscriptionLegalBasisExplanation;
            }
            set
            {
                PortalSubscriptionLegalBasisExplanation = value;
            }
        }

        [DataMember(Name = "subscriptionStatuses")]
        public List<SubscribeStatusHubSpotModel> SubscriptionStatuses { get; set; }

        [IgnoreDataMember]
        public bool IsNameValue => true;

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        [IgnoreDataMember]
        public string RouteBasePath => "/email/public/v1/subscriptions";
    }

}
