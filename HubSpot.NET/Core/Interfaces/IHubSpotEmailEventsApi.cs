using System;
using System.Collections.Generic;
using System.Linq;
using HubSpot.NET.Api.EmailEvents;
using HubSpot.NET.Api.EmailEvents.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotEmailEventsApi
    {
        T GetCampaignDataById<T>(long campaignId, long appId)
            where T : EmailCampaignDataHubSpotModel, new();

        EmailCampaignListHubSpotModel<T> ListCampaigns<T>(EmailCampaignListRequestOptions opts = null)
            where T : EmailCampaignHubSpotModel, new();

        EmailCampaignListHubSpotModel<T> RecentlyUpdatedCampaigns<T>(EmailCampaignListRequestOptions opts = null)
            where T : EmailCampaignHubSpotModel, new();

    }
}
