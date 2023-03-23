using HubSpot.NET.Api.Associations;
using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Contact;
using HubSpot.NET.Api.ContactList;
using HubSpot.NET.Api.CustomObject;
using HubSpot.NET.Api.Deal;
using HubSpot.NET.Api.EmailSubscriptions;
using HubSpot.NET.Api.Engagement;
using HubSpot.NET.Api.Files;
using HubSpot.NET.Api.Owner;
using HubSpot.NET.Api.Properties;
using HubSpot.NET.Api.Schemas;
using HubSpot.NET.Core.Interfaces;
using HubSpot.NET.Core.OAuth.Dto;

namespace HubSpot.NET.Core
{
    public class HubSpotObjectIds
    {
        public static readonly string Contact = "0-1";
        public static readonly string Deal = "0-3";
    }
    
    /// <summary>
    /// Starting point for using HubSpot.NET
    /// </summary>
    public class HubSpotApi : IHubSpotApi
    {
        public IHubSpotCompanyApi Company { get; protected set; }
        public IHubSpotContactApi Contact { get; protected set; }
        public IHubSpotDealApi Deal { get; protected set; }
        public IHubSpotEngagementApi Engagement { get; protected set; }
        public IHubSpotCosFileApi File { get; protected set; }
        public IHubSpotOwnerApi Owner { get; protected set; }
		public IHubSpotTaskApi Task { get; protected set; }
		public IHubSpotCompanyPropertiesApi CompanyProperties { get; protected set; }
        public IHubSpotContactListApi ContactLists { get; protected set; }

        public IHubSpotEmailSubscriptionsApi EmailSubscriptions { get; protected set; }
        public IHubSpotCustomObjectApi CustomObjects { get; protected set; }
        public IHubSpotSchemaApi Schema { get; protected set; }
        public IHubSpotAssociationsApi Associations { get; protected set; }

        protected virtual void Initialise(IHubSpotClient client)
		{
            Company = new HubSpotCompanyApi(client);
            Contact = new HubSpotContactApi(client);
            Deal = new HubSpotDealApi(client);
            Engagement = new HubSpotEngagementApi(client);
            File = new HubSpotCosFileApi(client);
            Owner = new HubSpotOwnerApi(client);
            CompanyProperties = new HubSpotCompaniesPropertiesApi(client);
            EmailSubscriptions = new HubSpotEmailSubscriptionsApi(client);
            ContactLists = new HubSpotContactListApi(client);
            Associations = new HubSpotAssociationsApi(client);
            CustomObjects = new HubSpotCustomObjectApi(client, Associations);
            Schema = new HubSpotSchemaApi(client);
        }

        


        public HubSpotApi(string apiKey)
        {
            IHubSpotClient client = new HubSpotBaseClient(apiKey);

            Initialise(client);
        }

        public HubSpotApi(HubSpotToken token)
        {
            IHubSpotClient client = new HubSpotBaseClient(token);

            Initialise(client);
        }
    }
}