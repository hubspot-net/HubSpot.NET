using HubSpot.NET.Api.Pipeline.Dto;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Pipeline
{
    public class HubSpotPipelinesApi : IHubSpotPipelineApi
    {

        private readonly IHubSpotClient _client;

        public HubSpotPipelinesApi(IHubSpotClient client)
        {
            _client = client;
        }      

        /// <summary>
        /// Returns a list of all pipelines of the specified objectType
        /// </summary>
        /// <typeparam name="T">Implementation of PipelineHubSpotModel</typeparam>
        /// <param name="objectType">Must be one of: deals, tickets</param>
        /// <param name="includeInactive">Must be one of "EXCLUDE_DELETED" (default), or "INCLUDE_DELETED" </param>
        /// <returns>The requested list</returns>
        public PipelineListHubSpotModel<T> List<T>(string objectType, string includeInactive = "EXCLUDE_DELETED") where T : PipelineHubSpotModel, new()
        {
            string path = $"{new PipelineListHubSpotModel<T>().RouteBasePath}/pipelines/{objectType}?includeInactive={includeInactive}";

            var data = _client.Execute<PipelineListHubSpotModel<T>>(path, method: RestSharp.Method.GET);

            return data;
        }
    }
}
