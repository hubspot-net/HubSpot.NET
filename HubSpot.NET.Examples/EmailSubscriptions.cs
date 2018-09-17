using System;
using System.Linq;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class EmailSubscriptions
    {
        public static void Example(HubSpotApi api)
        {
            try
            {
                Tests(api);
                Console.WriteLine("Email Subscriptions tests passed.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Email Subscriptions tests failed!");
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Tests(HubSpotApi api)
        {
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
