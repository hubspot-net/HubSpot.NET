namespace HubSpot.NET.Api.OAuth.Dto
{
    using System.Runtime.Serialization;

    public class HubSpotError
    {
        [DataMember(Name = "error")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "error_description")]
        public string Description { get; set; }
    }
}
