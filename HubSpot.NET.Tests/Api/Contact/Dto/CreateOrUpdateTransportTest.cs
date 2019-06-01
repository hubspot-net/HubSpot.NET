using HubSpot.NET.Api.Contact.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Xunit;

namespace HubSpot.NET.Tests.Api.Contact.Dto
{
    [Collection("DTO Collection")]
    public class CreateOrUpdateContactTransportTest
    {
        private CreateOrUpdateContactTransportModel sut;

        public CreateOrUpdateContactTransportTest() { }

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
        public void AllContactProperties_Custom_Or_Otherwise_ShouldBeIn_Properties_Transport_Dictionary(string email,
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
            var payload = new ContactHubSpotModel()
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

            var sut = new CreateOrUpdateContactTransportModel(payload);

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

        [Fact]
        public void CustomContactClasses_Should_Have_Their_Properties_Injected_Into_The_Properties_Transport_Dictionary()
        {
            var payload = new CustomContact("goose", 40);

            var sut = new CreateOrUpdateContactTransportModel(payload);

            Assert.Equal(payload.Flavor, sut.Properties.Where(x => x.Property == "flavor").First().Value);
        }
    }

    internal class CustomContact : ContactHubSpotModel
    {
        public CustomContact() : base()
        { }

        public CustomContact(string flavor, int legs): this()
        {
            Flavor = flavor;
            NumberOfLegs = legs;
        }

        [DataMember(Name = "flavor")]
        public string Flavor { get; set; } = string.Empty;
        [DataMember(Name = "legs")]
        public int NumberOfLegs { get; set; }
    }
}
