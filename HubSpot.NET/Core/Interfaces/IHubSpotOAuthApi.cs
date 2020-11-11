namespace HubSpot.NET.Core.Interfaces
{
    using HubSpot.NET.Api.OAuth.Dto;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IHubSpotOAuthApi
    {
        HubSpotToken Authorize(string redirectCode, string redirectUri);
        Task<HubSpotToken> AuthorizeAsync(string redirectCode, string redirectUri, CancellationToken cancellationToken = default);

        HubSpotToken Refresh(string redirectUri, HubSpotToken token);
        Task<HubSpotToken> RefreshAsync(string redirectUri, HubSpotToken token, CancellationToken cancellationToken = default);

        void UpdateCredentials(string id, string secret);
    }
}
