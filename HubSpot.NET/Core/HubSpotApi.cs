using HubSpot.NET.Api.CustomObject;
using HubSpot.NET.Api.Schemas;

namespace HubSpot.NET.Core
{
    using Api.Company;
    using Api.Contact;
    using Api.Deal;
    using Api.EmailEvents;
    using Api.EmailSubscriptions;
    using Api.Engagement;
    using Api.Files;
    using Api.OAuth;
    using Api.OAuth.Dto;
    using Api.Owner;
    using Api.Pipeline;
    using Api.Properties;
    using Api.Timeline;
    using Interfaces;

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
        public IHubSpotCompanyPropertyGroupsApi CompanyPropertyGroups { get; private set; }
        public IHubSpotEmailEventsApi EmailEvents { get; private set; }
        public IHubSpotEmailSubscriptionsApi EmailSubscriptions { get; private set; }
        public IHubSpotTimelineApi Timelines { get; private set; }
        public IHubSpotPipelineApi Pipelines { get; private set; }
        public IHubSpotCustomObjectApi CustomObjects { get; private set; }
        public IHubSpotSchemasApi Schemas { get; private set; }

        /// <summary>
        /// Creates a HubSpotApi using Private App Key authentication instead of OAuth 
        /// </summary>
        /// <remarks>
        /// API key authentication has been removed as it is no longer supported in HubSpot.
        /// https://developers.hubspot.com/docs/api/private-apps
        /// </remarks>
        /// <param name="privateAppKey">The HubSpot API key for your application.</param>
        public HubSpotApi(string privateAppKey)
        {
            IHubSpotClient client = new HubSpotBaseClient(privateAppKey);
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
            CompanyPropertyGroups = new HubSpotCompanyPropertyGroupsApi(client);
            EmailSubscriptions = new HubSpotEmailSubscriptionsApi(client);
            Timelines = new HubSpotTimelineApi(client);
            Pipelines = new HubSpotPipelinesApi(client);
            CustomObjects = new HubSpotCustomObjectApi(client);
            Schemas = new HubSpotSchemasApi(client);

        }

        
    }
}
