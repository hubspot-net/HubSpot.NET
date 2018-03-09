using System.Linq;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class EmailSubscriptions
    {
        public static void Example()
        {
            /**
             * Initialize the API with your API Key
             * You can find or generate this under Integrations -> HubSpot API key
             */
            var api = new HubSpotApi("YOUR-API-KEY-HERE");

            /**
             * Get the available subscription types
             */
            var all = api.EmailSubscriptions.GetEmailSubscriptionTypes();

            /**
             * Get the subscription statuses for the given email address
             * A missing type implies that they have not opted out
             */
            var john = api.EmailSubscriptions.GetStatus("john@squaredup.com");

            /**
             * Unsubscribe a user from ALL emails
             * WARNING: You cannot undo this
             */
            api.EmailSubscriptions.UnsubscribeAll("john@squaredup.com");


            /**
             * Unsubscribe a user from a given email type
             * WARNING: You cannot undo this
             */
            var type = all.Types.First();
            api.EmailSubscriptions.UnsubscribeFrom("dan@squaredup.com", type.Id);

        }
    }
}
