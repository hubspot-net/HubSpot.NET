using HubSpot.NET.Api.Pipeline.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotPipelineApi
    {
        
        PipelineListHubSpotModel<T> List<T>(string objectType, string includeInactive = "EXCLUDE_DELETED")
          where T : PipelineHubSpotModel, new();

        Task<PipelineListHubSpotModel<T>> ListAsync<T>(string objectType, string includeInactive = "EXCLUDE_DELETED", CancellationToken cancellationToken = default)
          where T : PipelineHubSpotModel, new();
    }
}