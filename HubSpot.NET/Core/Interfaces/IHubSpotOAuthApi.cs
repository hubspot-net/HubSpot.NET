namespace HubSpot.NET.Core.Interfaces
{
    using HubSpot.NET.Api.OAuth.Dto;

    public interface IHubSpotOAuthApi
    {
        HubSpotToken Authorize(string basePath, string redirectCode, string redirectUri);
        HubSpotToken Refresh(string basePath, string redirectUri, HubSpotToken token);
        void UpdateCredentials(string id, string secret);
    }
}
