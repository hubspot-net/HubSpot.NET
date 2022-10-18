using System.Collections.Generic;
using HubSpot.NET.Core.OAuth.Dto;
using RestSharp;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotClient
    {
        T Execute<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true) where T : IHubSpotModel, new();
        T Execute<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag) where T : IHubSpotModel, new();

        T Execute<T>(string absoluteUriPath, Method method = Method.GET, bool convertToPropertiesSchema = true) where T : IHubSpotModel, new();
        T Execute<T>(string absoluteUriPath, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag) where T : IHubSpotModel, new();

        void Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true);
        void Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag);

        void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET, bool convertToPropertiesSchema = true);
        void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag);

        T ExecuteMultipart<T>(string absoluteUriPath, byte[] data, string filename, Dictionary<string,string> parameters, Method method = Method.POST) where T : new();

        T ExecuteList<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true) where T : IHubSpotModel, new();
        T ExecuteList<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag) where T : IHubSpotModel, new();

        void UpdateToken(HubSpotToken token);
    }
}