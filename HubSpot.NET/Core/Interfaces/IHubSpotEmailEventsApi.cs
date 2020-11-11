using System.Threading;
using System.Threading.Tasks;
using HubSpot.NET.Api.EmailEvents;
using HubSpot.NET.Api.EmailEvents.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotEmailEventsApi
    {
        T GetCampaignDataById<T>(long campaignId, long appId)
            where T : EmailCampaignDataHubSpotModel, new();

        Task<T> GetCampaignDataByIdAsync<T>(long campaignId, long appId, CancellationToken cancellationToken = default)
            where T : EmailCampaignDataHubSpotModel, new();

        EmailCampaignListHubSpotModel<T> ListCampaigns<T>(EmailCampaignListRequestOptions opts = null)
            where T : EmailCampaignHubSpotModel, new();

        Task<EmailCampaignListHubSpotModel<T>> ListCampaignsAsync<T>(EmailCampaignListRequestOptions opts = null, CancellationToken cancellationToken = default)
            where T : EmailCampaignHubSpotModel, new();

        EmailCampaignListHubSpotModel<T> RecentlyUpdatedCampaigns<T>(EmailCampaignListRequestOptions opts = null)
            where T : EmailCampaignHubSpotModel, new();

        Task<EmailCampaignListHubSpotModel<T>> RecentlyUpdatedCampaignsAsync<T>(EmailCampaignListRequestOptions opts = null, CancellationToken cancellationToken = default)
            where T : EmailCampaignHubSpotModel, new();

    }
}
