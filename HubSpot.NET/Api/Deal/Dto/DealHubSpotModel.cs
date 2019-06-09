using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Deal.Dto
{
    /// <summary>
    /// Models the associations of a deal to companies & contacts
    /// </summary>
    [DataContract]
    public class DealHubSpotAssociations
    {
        [DataMember(Name = "associatedCompanyIds")]
        public long[] AssociatedCompanies { get; set; }

        [DataMember(Name = "associatedVids")]
        public long[] AssociatedContacts { get; set; }

        [DataMember(Name = "associatedDealIds")]
        public long[] AssociatedDeals { get; set; }

        public void AddContactById(long contactId)
        {
            int index = AssociatedContacts.Length - 1;

            if (index < 0)
                index = 0;

            AssociatedContacts[index] = contactId;
        }

        public void AddCompanyById(long companyId)
        {
            int index = AssociatedCompanies.Length - 1;

            if (index < 0)
                index = 0;

            AssociatedCompanies[index] = companyId;
        }

        public void AddDealById(long dealId)
        {
            int index = AssociatedDeals.Length - 1;

            if (index < 0)
                index = 0;

            AssociatedDeals[index] = dealId;
        }
    }

    /// <summary>
    /// Models a Deal entity within HubSpot. Default properties are included here
    /// with the intention that you'd extend this class with properties specific to 
    /// your HubSpot account.
    /// </summary>
    [DataContract]
    public class DealHubSpotModel : IHubSpotSerializable<DealHubSpotModel>
    {
        public DealHubSpotModel()
        {
        }
        /// <summary>
        /// Contacts unique Id in HubSpot
        /// </summary>
        [DataMember(Name = "dealId")]
        [IgnoreDataMember]
        public long? Id { get; set; }

        [DataMember(Name = "dealname")]
        public string Name { get; set; }

        [DataMember(Name = "dealstage")]
        public string Stage { get; set; }

        [DataMember(Name = "pipeline")]
        public string Pipeline { get; set; }

        [DataMember(Name = "hubspot_owner_id")]
        public long? OwnerId { get; set; }

        [DataMember(Name = "closedate")]
        public string CloseDate { get; set; }

        [DataMember(Name = "amount")]
        public double? Amount { get; set; }

        [DataMember(Name = "dealtype")]
        public string DealType { get; set; }

        [IgnoreDataMember]
        public DealHubSpotAssociations Associations { get; set; }
        
        public bool IsNameValue => true;

        public void AssociateContact(ContactHubSpotModel contact)
        {
            if(contact.Id.HasValue)
                Associations.AddContactById(contact.Id.Value);
        }

        public void AssociateCompany(CompanyHubSpotModel company)
        {
            if (company.Id.HasValue)
                Associations.AddCompanyById(company.Id.Value);
        }

        public void AssociateDeal(DealHubSpotModel deal)
        {
            if (deal.Id.HasValue && deal.Id.Value != Id.Value)
                Associations.AddDealById(deal.Id.Value);
        }

        public virtual void ToHubSpotDataEntity(ref DealHubSpotModel converted) 
        {
            converted.Associations = Associations;
        }

        public virtual void FromHubSpotDataEntity(DealHubSpotModel hubspotData)
        {
            if (hubspotData.Associations != null)
            {
                Associations.AssociatedContacts = hubspotData.Associations.AssociatedContacts;
                Associations.AssociatedCompanies = hubspotData.Associations.AssociatedCompanies;
            }
        }
    }
}
