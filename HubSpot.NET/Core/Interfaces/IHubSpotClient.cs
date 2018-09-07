using System.Collections.Generic;
using RestSharp;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotClient
    {
        T Execute<T>(string absoluteUriPath, T entity = default, Method method = Method.GET, bool convertToPropertiesSchema = true);        
        T ExecuteMultipart<T>(string absoluteUriPath, byte[] data, string filename, Dictionary<string, string> parameters, Method method = Method.POST);
        T ExecuteList<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true);
        void Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true);
        void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET, bool convertToPropertiesSchema = true);
    }
}