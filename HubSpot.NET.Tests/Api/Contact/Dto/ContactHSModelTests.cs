using HubSpot.NET.Api.Contact.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HubSpot.NET.Tests.Api.Contact.Dto
{
    [Collection("DTO Collection")]
    public class ContactHSModelTests
    {
        private ContactHubSpotModel sut;

        public ContactHSModelTests() { }

        [Theory]
        [InlineData("email@email.com",null, null, null, null, null, null, null, null, null)]
        [InlineData("email@email.com","Firsty", null, null, null, null, null, null, null, null)]
        [InlineData("email@email.com","Firsty", "Lasty", null, null, null, null, null, null, null)]
        [InlineData("email@email.com","Firsty", "Lasty", "www.site.com", null, null, null, null, null, null)]
        [InlineData("email@email.com","Firsty", "Lasty", "www.site.com", "Some Company", null, null, null, null, null)]
        [InlineData("email@email.com","Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", null, null, null, null)]
        [InlineData("email@email.com","Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", null, null, null)]
        [InlineData("email@email.com","Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", "Nowhere", null, null)]
        [InlineData("email@email.com","Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", "Nowhere", "Universe", null)]
        [InlineData("email@email.com","Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", "Nowhere", "Universe", "12345")]
        public void WhenPropertiesAssigned_ThroughConstructor_The_Values_Should_Be_In_Properties_Dictionary(string email, 
                                                                                                            string fName, 
                                                                                                            string lName, 
                                                                                                            string site, 
                                                                                                            string comp, 
                                                                                                            string phone, 
                                                                                                            string addr, 
                                                                                                            string city,
                                                                                                            string state,
                                                                                                            string zip)
        {
            sut = new ContactHubSpotModel()
            {
                Email = email,
                FirstName = fName,
                LastName = lName,
                Website = site,
                Company = comp,
                Phone = phone,
                Address = addr,
                City = city,
                State = state,
                ZipCode = zip
            };

            sut.LoadProperties();

            Assert.True(!sut.Properties.ContainsKey("email") || email == sut.Properties["email"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("firstname") || fName == sut.Properties["firstname"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("lastname") || lName == sut.Properties["lastname"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("website") || site == sut.Properties["website"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("company") || comp == sut.Properties["company"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("phone") || phone == sut.Properties["phone"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("address") || addr == sut.Properties["address"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("city") || city == sut.Properties["city"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("state") || state == sut.Properties["state"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("zip") ||  zip == sut.Properties["zip"].Value as string);
        }

        [Theory]
        [InlineData("email@email.com", null, null, null, null, null, null, null, null, null)]
        [InlineData("email@email.com", "Firsty", null, null, null, null, null, null, null, null)]
        [InlineData("email@email.com", "Firsty", "Lasty", null, null, null, null, null, null, null)]
        [InlineData("email@email.com", "Firsty", "Lasty", "www.site.com", null, null, null, null, null, null)]
        [InlineData("email@email.com", "Firsty", "Lasty", "www.site.com", "Some Company", null, null, null, null, null)]
        [InlineData("email@email.com", "Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", null, null, null, null)]
        [InlineData("email@email.com", "Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", null, null, null)]
        [InlineData("email@email.com", "Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", "Nowhere", null, null)]
        [InlineData("email@email.com", "Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", "Nowhere", "Universe", null)]
        [InlineData("email@email.com", "Firsty", "Lasty", "www.site.com", "Some Company", "888-777-6655", "42 E Meanin of Life", "Nowhere", "Universe", "12345")]
        public void WhenPropertiesAssigned_ThroughConstructor_The_Values_In_PropertiesDictionary_Should_Match_object_properties(string email,
                                                                                                    string fName,
                                                                                                    string lName,
                                                                                                    string site,
                                                                                                    string comp,
                                                                                                    string phone,
                                                                                                    string addr,
                                                                                                    string city,
                                                                                                    string state,
                                                                                                    string zip)
        {
            sut = new ContactHubSpotModel()
            {
                Email = email,
                FirstName = fName,
                LastName = lName,
                Website = site,
                Company = comp,
                Phone = phone,
                Address = addr,
                City = city,
                State = state,
                ZipCode = zip
            };

            sut.LoadProperties();

            Assert.True(!sut.Properties.ContainsKey("email") || email == sut.Properties["email"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("firstname") || fName == sut.Properties["firstname"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("lastname") || lName == sut.Properties["lastname"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("website") || site == sut.Properties["website"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("company") || comp == sut.Properties["company"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("phone") || phone == sut.Properties["phone"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("address") || addr == sut.Properties["address"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("city") || city == sut.Properties["city"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("state") || state == sut.Properties["state"].Value as string);
            Assert.True(!sut.Properties.ContainsKey("zip") || zip == sut.Properties["zip"].Value as string);
        }        

        [Fact]
        public void CustomContactModels_WillLoadTheirProperties_IntoThePropertiesDictionary_OnLoadProperties()
        {
            sut = new CustomContact("cheese", 5);

            sut.LoadProperties();

            Assert.True(sut.Properties.ContainsKey("flavor"));
            Assert.True(sut.Properties.ContainsKey("legs"));

            Assert.Equal("cheese", sut.Properties["flavor"].Value);
            Assert.Equal(5, sut.Properties["legs"].Value);
        }
    }
}
