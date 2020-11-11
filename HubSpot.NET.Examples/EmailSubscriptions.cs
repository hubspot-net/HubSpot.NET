using System;
using System.Linq;
using System.Threading.Tasks;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class EmailSubscriptions
    {
        public static async Task Example(HubSpotApi api)
        {
            try
            {
                await Tests(api);
                Console.WriteLine("Email Subscriptions tests passed.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Email Subscriptions tests failed!");
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task Tests(HubSpotApi api)
        {
           /**
             * Get the available subscription types
             */
            var all = await api.EmailSubscriptions.GetSubscriptionTypesAsync();

            /**
             * Get the subscription statuses for the given email address
             * A missing type implies that they have not opted out
             */
            //var john = await api.EmailSubscriptions.GetSubscriptionStatusForContactAsync("john@squaredup.com");

            /**
             * Unsubscribe a user from ALL emails
             * WARNING: You cannot undo this
             */
           // await api.EmailSubscriptions.UnsubscribeAllAsync("john@squaredup.com");


            /**
             * Unsubscribe a user from a given email type
             * WARNING: You cannot undo this
             */
            var type = all.Types.First();
           // await api.EmailSubscriptions.UnsubscribeFromAsync("dan@squaredup.com", type.Id);

            await api.EmailSubscriptions.SubscribeToAsync("dev@vtrpro.com", type.Id);
        }
    }
}
