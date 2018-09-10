namespace HubSpot.NET.Api.OAuth.Dto
{
    using System.Runtime.Serialization;

    public class HubSpotToken
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }
    }
}