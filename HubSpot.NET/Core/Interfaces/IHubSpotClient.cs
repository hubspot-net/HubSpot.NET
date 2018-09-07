using System.Collections.Generic;
using RestSharp;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotClient<T> where T : IHubSpotModel
    {
        T Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true);
        T Execute(string absoluteUriPath, Method method = Method.GET, bool convertToPropertiesSchema = true);
        T ExecuteMultipart(string absoluteUriPath, byte[] data, string filename, Dictionary<string, string> parameters, Method method = Method.POST);
        T ExecuteList(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true);
    }

    public interface IHubSpotClient
    {
        void Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true);
        void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET, bool convertToPropertiesSchema = true);
    }
}