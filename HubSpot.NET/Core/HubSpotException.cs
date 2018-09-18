using HubSpot.NET.Api.OAuth.Dto;
using System;

namespace HubSpot.NET.Core
{
    [Serializable]
    public class HubSpotException : Exception
    {
        public string RawJsonResponse { get; set; }
        public HubSpotError ReturnedError { get; set; }

        public HubSpotException()
        {
        }

        public HubSpotException(string message) : base(message)
        {
        }

        public HubSpotException(string message, string jsonResponse) : base(message)
        {
            RawJsonResponse = jsonResponse;
        }

        public HubSpotException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HubSpotException(string message, HubSpotError error) : base(message)
        {
            ReturnedError = error;
        }

        
        public override string Message => base.Message + $", JSONResponse={RawJsonResponse??"Empty"}";
    }
}
