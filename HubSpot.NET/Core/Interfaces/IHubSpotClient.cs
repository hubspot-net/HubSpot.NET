using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HubSpot.NET.Api.OAuth.Dto;
using RestSharp;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotClient
    {
        string AppId { get; }
        string BasePath { get; }
        T Execute<T>(string absoluteUriPath, Method method = Method.GET) where T: new();
        Task<T> ExecuteAsync<T>(string absoluteUriPath, Method method = Method.GET, CancellationToken cancellationToken = default) where T : new();

        T Execute<T,K>(string absoluteUriPath, K entity, Method method = Method.GET) where T: new();
        Task<T> ExecuteAsync<T, K>(string absoluteUriPath, K entity, Method method = Method.GET, CancellationToken cancellationToken = default) where T : new();

        T ExecuteMultipart<T>(string absoluteUriPath, byte[] data, string filename, Dictionary<string, string> parameters, Method method = Method.POST);
        Task<T> ExecuteMultipartAsync<T>(string absoluteUriPath, byte[] data, string filename, Dictionary<string, string> parameters, Method method = Method.POST, CancellationToken cancellationToken = default);

        void ExecuteOnly(string absoluteUriPath, Method method = Method.GET);
        Task ExecuteOnlyAsync(string absoluteUriPath, Method method = Method.GET, CancellationToken cancellationToken = default);

        void ExecuteOnly<T>(string absoluteUriPath, T entity, Method method = Method.GET);
        Task ExecuteOnlyAsync<T>(string absoluteUriPath, T entity, Method method = Method.GET, CancellationToken cancellationToken = default);

        void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET);
        Task ExecuteBatchAsync(string absoluteUriPath, List<object> entities, Method method = Method.GET, CancellationToken cancellationToken = default);

        void UpdateToken(HubSpotToken token);
    }
}