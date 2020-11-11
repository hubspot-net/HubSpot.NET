using HubSpot.NET.Core;
using System;
using System.Threading.Tasks;

namespace HubSpot.NET.Examples
{
    public class Timeline
    {
        public static async Task Example(HubSpotApi api)
        {
            try
            {
                await Tests(api);
                Console.Write("Timeline tests passed!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Timeline tests failed!", ex.ToString());
            }
        }

        private static async Task Tests(HubSpotApi api)
        {
            var eventTypes = await api.Timelines.GetAllEventTypesAsync();

        }
    }
}
