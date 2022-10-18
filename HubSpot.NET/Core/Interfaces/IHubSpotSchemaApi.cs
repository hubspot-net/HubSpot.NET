using HubSpot.NET.Api.Schemas;

namespace HubSpot.NET.Core.Interfaces;

public interface IHubSpotSchemaApi
{
    SchemaListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T : SchemaHubSpotModel, new();

}