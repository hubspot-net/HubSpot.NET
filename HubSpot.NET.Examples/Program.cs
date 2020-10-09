namespace HubSpot.NET.Examples
{
    using HubSpot.NET.Core;
    using System;
    using System.IO;

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

                if(args.Length > 3)
                {
                    clientId = args[1];
                    clientSecret = args[2];
                    appId = args[3];
                }
            }
            else
            {
                bool authChosen = false;
                Console.WriteLine("How would you like to authenticate your requests? HAPIKey or OAuth?");
                string authType;
                while (authChosen == false)
                {
                    authType = Console.ReadLine().ToLowerInvariant();

                    switch (authType)
                    {
                        case "hapikey":
                            Console.WriteLine("Please enter the HAPIKey:");
                            bool valid = Guid.TryParse(Console.ReadLine(), out Guid guidResult);
                            hapiKey = valid ? guidResult.ToString() : string.Empty;

                            if (string.IsNullOrWhiteSpace(hapiKey))
                            {
                                Console.WriteLine("That is not a valid HAPIKey. Please try again");
                                break;
                            }

                            authChosen = true;

                            break;
                        case "oauth":
                            Console.WriteLine("Please enter the ClientID for your app:");
                            clientId = Console.ReadLine();

                            Console.WriteLine("Please enter the ClientSecret for your app:");
                            clientSecret = Console.ReadLine();

                            Console.WriteLine("Please enter the AppId for your app:");
                            appId = Console.ReadLine();

                            authChosen = true;
                            break;
                        default:
                            Console.WriteLine("That is not a valid selection. Please choose HAPIKey or OAuth.");
                            break;
                    }

                }                
            }

            while(string.IsNullOrWhiteSpace(hapiKey) || !Guid.TryParse(hapiKey, out Guid result))
            {
                Console.WriteLine("Invalid API Key, try again");
                hapiKey = Console.ReadLine();
            }

            var hapiApi = new HubSpotApi(hapiKey);
            RunApiKeyExamples(hapiApi);            

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
            //EmailSubscriptions.Example(hapiApi);
            //Deals.Example(hapiApi);
            //Contacts.Example(hapiApi);
            //Companies.Example(hapiApi);
            //CompanyProperties.Example(hapiApi);
            Pipelines.Example(hapiApi);
        }

        private static void RunOAuthExamples(HubSpotApi oauthApi)
        {
            OAuth.Example(oauthApi);
            Timeline.Example(oauthApi);
        }

        private static string GetContactString() 
            => File.ReadAllText("ContactExample.txt");
    }
}
