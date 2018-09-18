namespace HubSpot.NET.Api.OAuth.Dto
{
    using System.Runtime.Serialization;

    public class HubSpotError
    {
        [DataMember(Name = "error")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "error_description")]
        public string Description { get; set; }

        public HubSpotError() { }

        public HubSpotError(string code, string desc)
        {
            ErrorCode = code;
            Description = desc;
        }

        public override string ToString()
            => $"Status: {(string.IsNullOrWhiteSpace(ErrorCode) ? "No Code": ErrorCode)}; Description: {(string.IsNullOrWhiteSpace(Description) ? "No Description" : Description)}";        
    }
}
