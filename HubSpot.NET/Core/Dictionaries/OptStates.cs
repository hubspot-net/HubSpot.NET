using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Dictionaries
{
    /// <summary>
    /// Class used to access the values of the OptStatesDictionary for subscriptions
    /// </summary>
    public class OptStates
    {
        private static readonly Dictionary<OptState, string> OptStatesDictionary = new Dictionary<OptState, string>
        {
            { OptState.OPT_IN , "OPT_IN" },
            { OptState.NOT_OPTED, "NOT_OPTED" },
            { OptState.OPT_OUT, "OPT_OUT" }
        };

        public static string GetState(OptState state)
            => OptStatesDictionary[state];               
    }

    public enum OptState
    {
        OPT_IN = 0,
        NOT_OPTED = 1,
        OPT_OUT = 2
    }
}
