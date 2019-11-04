using HubSpot.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Examples
{
    public class Timeline
    {
        public static void Example(HubSpotApi api)
        {
            try
            {
                Tests(api);
                Console.Write("Timeline tests passed!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Timeline tests failed!", ex.ToString());
            }
        }

        private static void Tests(HubSpotApi api)
        {
            var eventTypes = api.Timelines.GetAllEventTypes();

        }
    }
}
