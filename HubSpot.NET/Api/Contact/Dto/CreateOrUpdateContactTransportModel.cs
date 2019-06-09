namespace HubSpot.NET.Api.Contact.Dto
{
    using HubSpot.NET.Api.Shared;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateOrUpdateContactTransport : PropertyTransport<ContactHubSpotModel, PropertyValuePair>
    {
        public CreateOrUpdateContactTransport() { }

        public CreateOrUpdateContactTransport(ContactHubSpotModel model)
        {
            LoadProperties(model);
        }
    }
}
