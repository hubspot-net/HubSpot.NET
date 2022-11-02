using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Associations;

public class HubSpotAssociationsApi : IHubSpotAssociationsApi
{
    private readonly IHubSpotClient _client;
    public HubSpotAssociationsApi(IHubSpotClient client)
    {
        _client = client;
    }
    
}