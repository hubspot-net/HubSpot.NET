using HubSpot.NET.Api.Pipeline.Dto;
using HubSpot.NET.Core;
using System;
using System.Threading.Tasks;

namespace HubSpot.NET.Examples
{
    public class Pipelines
    {
        internal static async Task Example(HubSpotApi api)
        {
            try
            {
                await Tests(api);
                Console.WriteLine("Pipelines tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pipelines tests failed!");
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task Tests(HubSpotApi api)
        {
            //Get a list of pipelines and stages
            var list = await api.Pipelines.ListAsync<PipelineHubSpotModel>("deals", "INCLUDE_DELETED");
        }
    }
}
