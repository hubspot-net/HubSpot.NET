namespace HubSpot.NET.Examples
{
    using HubSpot.NET.Core;
    using System.Threading.Tasks;

    public class OAuth
    {
        public static async Task Example(HubSpotApi hubspot, string redirectCode = "", string redirectUri = "")
        {
            var token = await hubspot.OAuth.AuthorizeAsync(redirectCode, redirectUri);
        }
    }
}