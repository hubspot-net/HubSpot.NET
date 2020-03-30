using HubSpot.NET.Api.Pipeline.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotPipelineApi
    {
        
        PipelineListHubSpotModel<T> List<T>(string objectType, string includeInactive = "EXCLUDE_DELETED")
          where T : PipelineHubSpotModel, new();
    }
}