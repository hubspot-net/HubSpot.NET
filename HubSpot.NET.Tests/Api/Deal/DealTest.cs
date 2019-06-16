using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Api.Contact.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HubSpot.NET.Tests
{
    [Collection("DTO Collection")]
    public class DealTest
    {
        public DealHubSpotModel sut;

        [Fact]
        public void AssociatingAContact_ShouldAddTheContactId_ToAssociations()
        {
            ContactHubSpotModel contact = new ContactHubSpotModel()
            {
                Id = 42
            };

            sut = new DealHubSpotModel();

            int assocContacts = sut.Associations.AssociatedContacts.Count;

            sut.AssociateContact(contact);

            //Count of associated contacts before 
            Assert.Equal(0, assocContacts);
            // contact of associated contacts after
            Assert.Single(sut.Associations.AssociatedContacts);

            //
            Assert.Equal(42, sut.Associations.AssociatedContacts.First());
        }
        
        [Fact]
        public void AssociatingACompany_ShouldAddTheCompanyId_ToAssociations()
        {
            CompanyHubSpotModel comp = new CompanyHubSpotModel() { Id = 42 };

            sut = new DealHubSpotModel();

            int assocComps = sut.Associations.AssociatedCompanies.Count;

            sut.AssociateCompany(comp);

            // Pre-assoc company count
            Assert.Equal(0, assocComps);

            Assert.Single(sut.Associations.AssociatedCompanies);

            Assert.Equal(42, sut.Associations.AssociatedCompanies.First());
        }

        [Fact]
        public void AssociatingADeal_ShouldAddtheDealId_ToAssociations()
        {
            DealHubSpotModel deal = new DealHubSpotModel() { Id = 42 };

            sut = new DealHubSpotModel() { Id = 1 };

            int assocDeals = sut.Associations.AssociatedDeals.Count;

            sut.AssociateDeal(deal);

            // Pre-assoc deal count
            Assert.Equal(0, assocDeals);

            Assert.Single(sut.Associations.AssociatedDeals);

            Assert.Equal(42, sut.Associations.AssociatedDeals.First());
        }
    }
}
