namespace HubSpot.NET.Core.Interfaces
{
    using Api.OAuth.Dto;

    public interface IHubSpotOAuthApi
    {
        HubSpotToken Authorize(string redirectCode, string redirectUri);
        HubSpotToken Refresh(string redirectUri, HubSpotToken token);
        void UpdateCredentials(string id, string secret);
    }
}
