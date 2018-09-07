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
            var eventTypes = api.Timelines.GetAllEventTypes();
            
        }
    }
}
