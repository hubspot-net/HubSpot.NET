namespace HubSpot.NET.Api.OAuth.Dto
{
    using System.Net;
    using System.Runtime.Serialization;

    [DataContract]
    public class HubSpotError
    {
        [DataMember(Name = "error")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "error_description")]
        public string Description { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public HubSpotError() { }

        public HubSpotError(string code, string desc)
        {
            ErrorCode = code;
            Description = desc;
        }
        public HubSpotError(HttpStatusCode code, string desc)
        {
            Description = desc;
            StatusCode = code;
            ErrorCode = code.ToString();
        }

        public override string ToString()
            => $"Status: {(string.IsNullOrWhiteSpace(ErrorCode) ? "No Code": ErrorCode)}; Description: {(string.IsNullOrWhiteSpace(Description) ? "No Description" : Description)}";        
    }
}
