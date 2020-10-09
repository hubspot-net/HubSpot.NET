namespace HubSpot.NET.Api.EmailEvents
{
    using HubSpot.NET.Api.EmailEvents.Dto;
    using HubSpot.NET.Core;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System.Net;

    public class HubSpotEmailEventsApi : IHubSpotEmailEventsApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotEmailEventsApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets campaign data by ID from hubspot
        /// </summary>
        /// <param name="campaignId">The campaign ID to query.</param>
        /// <param name="appId">The app ID to query.</param>
        /// <typeparam name="T">Implementation of EmailCampaignDataHubSpotModel</typeparam>
        /// <returns>The campaign data entity or null if the compaign does not exist.</returns>
        public T GetCampaignDataById<T>(long campaignId, long appId) where T : EmailCampaignDataHubSpotModel, new()
        {
            var path = $"{(new T()).RouteBasePath}/{campaignId}?{QueryParams.APP_ID}={appId}";

            try
            {
                var data = _client.Execute<T>(path, Method.GET);
                return data;
            }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        /// <summary>
        /// Gets a list of email campaigns.
        /// </summary>
        /// <typeparam name="T">Implementation of EmailCampaignHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of email campaigns</returns>
        public EmailCampaignListHubSpotModel<T> ListCampaigns<T>(EmailCampaignListRequestOptions opts = null) where T : EmailCampaignHubSpotModel, new()
        {
            if (opts == null)
            {
                opts = new EmailCampaignListRequestOptions { Limit = 250 };
            }

            var path = $"{new EmailCampaignListHubSpotModel<T>().RouteBasePath}/by-id?{QueryParams.LIMIT}={opts.Limit}";

            if (!string.IsNullOrEmpty(opts.Offset))            
                path += $"{QueryParams.OFFSET}={opts.Offset}";

            var data = _client.Execute<EmailCampaignListHubSpotModel<T>>(path);

            return data;
        }

        /// <summary>
        /// Gets a list of email campaigns.
        /// </summary>
        /// <typeparam name="T">Implementation of EmailCampaignHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of email campaigns</returns>
        public EmailCampaignListHubSpotModel<T> RecentlyUpdatedCampaigns<T>(EmailCampaignListRequestOptions opts = null) where T : EmailCampaignHubSpotModel, new()
        {
            if (opts == null)
            {
                opts = new EmailCampaignListRequestOptions { Limit = 250 };
            }

            var path = $"{new EmailCampaignListHubSpotModel<T>().RouteBasePath}?{QueryParams.LIMIT}={opts.Limit}";

            if (!string.IsNullOrEmpty(opts.Offset))            
                path += $"{QueryParams.OFFSET}={opts.Offset}";

            var data = _client.Execute<EmailCampaignListHubSpotModel<T>>(path);

            return data;
        }

    }
}
