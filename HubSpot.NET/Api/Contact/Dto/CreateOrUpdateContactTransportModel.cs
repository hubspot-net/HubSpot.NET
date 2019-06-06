namespace HubSpot.NET.Api.Contact.Dto
{
    using HubSpot.NET.Api.Shared;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateOrUpdateContactTransportModel : PropertyTransport<ContactHubSpotModel>
    {
        public CreateOrUpdateContactTransportModel() { }

        public CreateOrUpdateContactTransportModel(ContactHubSpotModel model)
        {
            ToPropertyTransportModel(model);
        }
    }
}
