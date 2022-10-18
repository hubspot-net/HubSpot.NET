namespace HubSpot.NET.Core.OAuth.Dto
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RequestRefreshTokenHubSpotModel
    {
        [DataMember(Name = "grant_type")]
        public string GrantType { get; set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }

        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; }

        [DataMember(Name = "redirect_uri")]
        public string RedirectUri { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }


        public RequestRefreshTokenHubSpotModel()
        {
            GrantType = "refresh_token";
        }
    }
}
