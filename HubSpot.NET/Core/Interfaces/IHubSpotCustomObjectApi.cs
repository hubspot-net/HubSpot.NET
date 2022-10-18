using HubSpot.NET.Api.CustomObject;

namespace HubSpot.NET.Core.Interfaces;

public interface IHubSpotCustomObjectApi
{
    CustomObjectListHubSpotModel<T> List<T>(string idForCustomObject, ListRequestOptions opts = null) where T : CustomObjectHubSpotModel, new();
}