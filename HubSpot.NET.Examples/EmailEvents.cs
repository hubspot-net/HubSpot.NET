using System;
using System.Linq;
using System.Threading.Tasks;
using HubSpot.NET.Api.EmailEvents;
using HubSpot.NET.Api.EmailEvents.Dto;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class EmailEvents
    {

        public static async Task Example(HubSpotApi api)
        {
            /**
             * Get all campaigns
             */
            var campaignInfos = await api.EmailEvents.RecentlyUpdatedCampaignsAsync<EmailCampaignHubSpotModel>(
                new EmailCampaignListRequestOptions { Limit = 100 });

            Console.WriteLine($"Count: {campaignInfos.Campaigns.Count}");

            while (campaignInfos.MoreResultsAvailable)
            {
                campaignInfos = await api.EmailEvents.RecentlyUpdatedCampaignsAsync<EmailCampaignHubSpotModel>(
                    new EmailCampaignListRequestOptions { Limit = 100, Offset = campaignInfos.ContinuationOffset });

                Console.WriteLine($"Count: {campaignInfos.Campaigns.Count}");
            }

            /**
             * Get campaign data
             */
            var campaign = campaignInfos.Campaigns.First();
            var campaignData = await api.EmailEvents.GetCampaignDataByIdAsync<EmailCampaignDataHubSpotModel>(campaign.Id, campaign.AppId);
        }

    }
}
