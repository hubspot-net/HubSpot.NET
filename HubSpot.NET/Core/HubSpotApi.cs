namespace HubSpot.NET.Core
{
    using HubSpot.NET.Api.Company;
    using HubSpot.NET.Api.Contact;
    using HubSpot.NET.Api.Deal;
    using HubSpot.NET.Api.EmailEvents;
    using HubSpot.NET.Api.EmailSubscriptions;
    using HubSpot.NET.Api.Engagement;
    using HubSpot.NET.Api.Files;
    using HubSpot.NET.Api.OAuth;
    using HubSpot.NET.Api.OAuth.Dto;
    using HubSpot.NET.Api.Owner;
    using HubSpot.NET.Api.Properties;
    using HubSpot.NET.Api.Timeline;
    using HubSpot.NET.Core.Interfaces;

    /// <summary>
    /// Starting point for using HubSpot.NET
    /// </summary>
    public class HubSpotApi : IHubSpotApi
    {
        public IHubSpotOAuthApi OAuth { get; set; }
        public IHubSpotCompanyApi Company { get; private set; }
        public IHubSpotContactApi Contact { get; private set; }
        public IHubSpotContactPropertyApi ContactProperty { get; private set; }
        public IHubSpotDealApi Deal { get; private set; }
        public IHubSpotEngagementApi Engagement { get; private set; }
        public IHubSpotCosFileApi File { get; private set; }
        public IHubSpotOwnerApi Owner { get; private set; }
        public IHubSpotCompanyPropertiesApi CompanyProperties { get; private set; }
        public IHubSpotEmailEventsApi EmailEvents { get; private set; }
        public IHubSpotEmailSubscriptionsApi EmailSubscriptions { get; private set; }
        public IHubSpotTimelineApi Timelines { get; private set; }


        /// <summary>
        /// Creates a HubSpotApi using API key authentication instead of OAuth
        /// </summary>
        /// <param name="apiKey">The HubSpot API key for your application.</param>
        public HubSpotApi(string apiKey)
        {
            IHubSpotClient client = new HubSpotBaseClient(apiKey, HubSpotAuthenticationMode.HAPIKEY);
            InitializeRepos(client);
        }

        /// <summary>
        /// Creates a HubSpotApi using OAuth 2.0 authentication for all API calls. 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        public HubSpotApi(string clientId, string clientSecret, string appId, HubSpotToken token = null)
        {
            IHubSpotClient client = new HubSpotBaseClient(string.Empty, HubSpotAuthenticationMode.OAUTH, appId, token);
            InitializeRepos(client, clientId, clientSecret);
        }

        private void InitializeRepos(IHubSpotClient client, string clientId = "", string clientSecret = "")
        {
            OAuth = new HubSpotOAuthApi(client, clientId, clientSecret);
            Company = new HubSpotCompanyApi(client);
            CompanyProperties = new HubSpotCompaniesPropertiesApi(client);
            Contact = new HubSpotContactApi(client);
            ContactProperty = new HubSpotContactPropertyApi(client);
            Deal = new HubSpotDealApi(client);
            EmailEvents = new HubSpotEmailEventsApi(client);
            EmailSubscriptions = new HubSpotEmailSubscriptionsApi(client);
            Engagement = new HubSpotEngagementApi(client);
            File = new HubSpotCosFileApi(client);
            Owner = new HubSpotOwnerApi(client);
            CompanyProperties = new HubSpotCompaniesPropertiesApi(client);
            EmailSubscriptions = new HubSpotEmailSubscriptionsApi(client);
            Timelines = new HubSpotTimelineApi(client);

        }
    }
}
