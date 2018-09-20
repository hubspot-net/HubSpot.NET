using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core;
using Newtonsoft.Json;
using System;

namespace HubSpot.NET.Examples
{
    public class Examples
    {
        // enable args to be presented from CLI for automated test execution 
        static void Main(string[] args)
        {
            /**
             * Initialize the API with your API Key
             * You can find or generate this under Integrations -> HubSpot API key
             */
            string hapiKey = string.Empty; // YOUR KEY GOES HERE ... or supply it as args[0] either way.
            string clientId = string.Empty; // args[1]
            string clientSecret = string.Empty; // args[2]
            string appId = string.Empty; // args[3]
            if(args.Length >= 1)
            {
                hapiKey = args[0];

                if(args.Length > 4)
                {
                    clientId = args[1];
                    clientSecret = args[2];
                    appId = args[3];
                }
            }

            if(string.IsNullOrWhiteSpace(hapiKey))
            {
                Console.WriteLine("Invalid API Key, skipping API Key related tests...");
            }
            else
            {
                var hapiApi = new HubSpotApi(hapiKey);
                RunApiKeyExamples(hapiApi);
            }

            if(string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret) || string.IsNullOrWhiteSpace(appId))
            {
                Console.WriteLine("Invalid clientId, Secret, or AppID. Skipping OAuth related tests...");
            }
            else
            {
                var oauthApi = new HubSpotApi(clientId, clientSecret, appId);
                RunOAuthExamples(oauthApi);
            }

            var key = Console.ReadKey();
        }

        private static void RunApiKeyExamples(HubSpotApi hapiApi)
        {
            EmailSubscriptions.Example(hapiApi);
            Deals.Example(hapiApi);
            Contacts.Example(hapiApi);
            Companies.Example(hapiApi);
            CompanyProperties.Example(hapiApi);
        }
        private static void RunOAuthExamples(HubSpotApi oauthApi)
        {
            OAuth.Example(oauthApi);
            Timeline.Example(oauthApi);
        }

        private static string GetContactString()
        {
            return System.IO.File.ReadAllText("ContactExample.txt");
        }
    }
}
