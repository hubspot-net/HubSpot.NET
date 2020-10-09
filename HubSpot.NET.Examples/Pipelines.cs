using HubSpot.NET.Api.Pipeline.Dto;
using HubSpot.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Examples
{
    public class Pipelines
    {
        internal static void Example(HubSpotApi api)
        {
            try
            {
                Tests(api);
                Console.WriteLine("Pipelines tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pipelines tests failed!");
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Tests(HubSpotApi api)
        {
            //Get a list of pipelines and stages
            var list = api.Pipelines.List<PipelineHubSpotModel>("deals", "INCLUDE_DELETED");

        }
    }
}
