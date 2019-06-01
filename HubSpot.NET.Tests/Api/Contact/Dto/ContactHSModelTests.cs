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

            Assert.Equal(email, sut.Properties["email"].Value);
            Assert.Equal(fName, sut.Properties["firstname"].Value);
            Assert.Equal(lName, sut.Properties["lastname"].Value);
            Assert.Equal(site, sut.Properties["website"].Value);
            Assert.Equal(comp, sut.Properties["company"].Value);
            Assert.Equal(phone, sut.Properties["phone"].Value);
            Assert.Equal(addr, sut.Properties["address"].Value);
            Assert.Equal(city, sut.Properties["city"].Value);
            Assert.Equal(state, sut.Properties["state"].Value);
            Assert.Equal(zip, sut.Properties["zip"].Value);
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

            //Assert.Equal(sut.Properties["email"].Value, sut.Email);
            //Assert.Equal(sut.Properties["firstname"].Value, sut.FirstName);
            //Assert.Equal(sut.Properties["lastname"].Value, sut.LastName);
            //Assert.Equal(sut.Properties["website"].Value, sut.Website);
            //Assert.Equal(sut.Properties["company"].Value, sut.Company);
            //Assert.Equal(sut.Properties["phone"].Value, sut.Phone);
            //Assert.Equal(sut.Properties["address"].Value, sut.Address);
            //Assert.Equal(sut.Properties["city"].Value, sut.City);
            //Assert.Equal(sut.Properties["state"].Value, sut.State);
            //Assert.Equal(sut.Properties["zip"].Value, sut.ZipCode);
            Assert.True(sut.Properties["email"] == null || email == sut.Properties["email"].Value);
            Assert.True(sut.Properties["firstname"] == null || fName == sut.Properties["firstname"].Value);
            Assert.True(sut.Properties["lastname"] == null || lName == sut.Properties["lastname"].Value);
            Assert.True(sut.Properties["website"] == null || site == sut.Properties["website"].Value);
            Assert.True(sut.Properties["company"] == null || comp == sut.Properties["company"].Value);
            Assert.True(sut.Properties["phone"] == null || phone == sut.Properties["phone"].Value);
            Assert.True(sut.Properties["address"] == null || addr == sut.Properties["address"].Value);
            Assert.True(sut.Properties["city"] == null || city == sut.Properties["city"].Value);
            Assert.True(sut.Properties["state"] == null || state == sut.Properties["state"].Value);
            Assert.True(sut.Properties["zip"] == null || zip == sut.Properties["zip"].Value);
        }        
    }
}
