using HubSpot.NET.Api.Contact.Dto;
using System.Linq;
using System.Runtime.Serialization;
using Xunit;

namespace HubSpot.NET.Tests.Api.Contact.Dto
{
    [Collection("DTO Collection")]
    public class CreateOrUpdateContactTransportTest
    {
        private PropertyTransport<ContactHubSpotModel, PropertyValuePair> sut;

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

            var sut = new PropertyTransport<ContactHubSpotModel, PropertyValuePair>(payload);

            Assert.True(sut.Properties.Find(x => x.Name == "email") == null || email == sut.Properties.Find(x => x.Name == "email").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "firstname") == null || fName == sut.Properties.Find(x => x.Name == "firstname").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "lastname") == null || lName == sut.Properties.Find(x => x.Name == "lastname").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "website") == null || site == sut.Properties.Find(x => x.Name == "website").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "company") == null || comp == sut.Properties.Find(x => x.Name == "company").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "phone") == null || phone == sut.Properties.Find(x => x.Name == "phone").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "address") == null || addr == sut.Properties.Find(x => x.Name == "address").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "city") == null || city == sut.Properties.Find(x => x.Name == "city").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "state") == null || state == sut.Properties.Find(x => x.Name == "state").Value);
            Assert.True(sut.Properties.Find(x => x.Name == "zip") == null || zip == sut.Properties.Find(x => x.Name == "zip").Value);
        }

        [Fact]
        public void CustomContactClasses_Should_Have_Their_Properties_Injected_Into_The_Properties_Transport_Dictionary()
        {
            var payload = new CustomContact("goose", 40);

            var sut = new PropertyTransport<ContactHubSpotModel, PropertyValuePair>(payload);

            Assert.Equal(payload.Flavor, sut.Properties.Where(x => x.Name == "flavor").First().Value);
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
