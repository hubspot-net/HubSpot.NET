using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace HubSpot.NET.Core
{
    public static class RestSharpExtensions
    {
        public static bool IsSuccessful(this IRestResponse response)
        {
            return (int) response.StatusCode >= 200 
                   && (int) response.StatusCode <= 299 
                   && response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}
