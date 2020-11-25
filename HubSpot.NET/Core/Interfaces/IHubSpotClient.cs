using System.Collections.Generic;
using HubSpot.NET.Api.OAuth.Dto;
using RestSharp;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotClient
    {
        string AppId { get; }
        string BasePath { get; }

        T Execute<T>(string absoluteUriPath, Method method = Method.GET) where T: new();
        T Execute<T,K>(string absoluteUriPath, K entity, Method method = Method.GET) where T: new();        
        T ExecuteMultipart<T>(string absoluteUriPath, byte[] data, string filename, Dictionary<string, string> parameters, Method method = Method.POST);
        void ExecuteOnly(string absoluteUriPath, Method method = Method.GET);
        void ExecuteOnly<T>(string absoluteUriPath, T entity, Method method = Method.GET);
        void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET);
        void UpdateToken(HubSpotToken token);
    }
}