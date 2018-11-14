using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Contact;
using HubSpot.NET.Api.Deal;
using HubSpot.NET.Api.EmailEvents;
using HubSpot.NET.Api.EmailSubscriptions;
using HubSpot.NET.Api.Engagement;
using HubSpot.NET.Api.Files;
using HubSpot.NET.Api.Owner;
using HubSpot.NET.Api.Properties;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Core
{
    /// <summary>
    /// Starting point for using HubSpot.NET
    /// </summary>
    public class HubSpotApi : IHubSpotApi
    {
        public IHubSpotCompanyApi Company { get; }
        public IHubSpotCompanyPropertiesApi CompanyProperties { get; }
        public IHubSpotContactApi Contact { get; }
        public IHubSpotDealApi Deal { get; }
        public IHubSpotEmailEventsApi EmailEvents { get; }
        public IHubSpotEmailSubscriptionsApi EmailSubscriptions { get; }
        public IHubSpotEngagementApi Engagement { get; }
        public IHubSpotCosFileApi File { get; }
        public IHubSpotOwnerApi Owner { get; }


        public HubSpotApi(string apiKey)
        {
            IHubSpotClient client = new HubSpotBaseClient(apiKey);

            Company = new HubSpotCompanyApi(client);
            CompanyProperties = new HubSpotCompaniesPropertiesApi(client);
            Contact = new HubSpotContactApi(client);
            Deal = new HubSpotDealApi(client);
            EmailEvents = new HubSpotEmailEventsApi(client);
            EmailSubscriptions = new HubSpotEmailSubscriptionsApi(client);
            Engagement = new HubSpotEngagementApi(client);
            File = new HubSpotCosFileApi(client);
            Owner = new HubSpotOwnerApi(client);
        }

    }

}
