namespace HubSpot.NET.Examples
{
    using Core;

    public class OAuth
    {
        public static void Example(HubSpotApi hubspot, string redirectCode = "", string redirectUri = "")
        {
            var token = hubspot.OAuth.Authorize(redirectCode, redirectUri);
        }
    }
}