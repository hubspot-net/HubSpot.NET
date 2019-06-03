using System;
using System.Collections.Generic;
using System.Linq;
using HubSpot.NET.Api.EmailEvents;
using HubSpot.NET.Api.EmailEvents.Dto;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class EmailEvents
    {

        public static void Example(HubSpotApi api)
        {
            /**
             * Get all campaigns
             */
            var campaignInfos = api.EmailEvents.RecentlyUpdatedCampaigns<EmailCampaignHubSpotModel>(
                new EmailCampaignListRequestOptions { Limit = 100 });

            Console.WriteLine($"Count: {campaignInfos.Campaigns.Count}");

            while (campaignInfos.MoreResultsAvailable)
            {
                campaignInfos = api.EmailEvents.RecentlyUpdatedCampaigns<EmailCampaignHubSpotModel>(
                    new EmailCampaignListRequestOptions { Limit = 100, Offset = campaignInfos.ContinuationOffset });

                Console.WriteLine($"Count: {campaignInfos.Campaigns.Count}");
            }

            /**
             * Get campaign data
             */
            var campaign = campaignInfos.Campaigns.First();
            var campaignData = api.EmailEvents.GetCampaignDataById<EmailCampaignDataHubSpotModel>(campaign.Id, campaign.AppId);
        }

    }
}
