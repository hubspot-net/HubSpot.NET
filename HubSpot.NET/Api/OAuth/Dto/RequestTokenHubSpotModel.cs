namespace HubSpot.NET.Api.OAuth.Dto
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RequestTokenHubSpotModel
    {
        [DataMember(Name = "grant_type")]
        public string GrantType { get; set; }

        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }

        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; }

        [DataMember(Name = "redirect_uri")]
        public string RedirectUri { get; set; }

        [DataMember(Name = "code")]
        public string RedirectCode { get; set; }

        public RequestTokenHubSpotModel()
        {
            GrantType = "authorization_code";
        }
    }
}
