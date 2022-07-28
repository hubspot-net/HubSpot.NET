using System;

namespace HubSpot.NET.Core
{
    using Api.OAuth.Dto;
    using Interfaces;
    using Serializers;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Collections.Generic;

    public class HubSpotBaseClient : IHubSpotClient
    {
        private readonly RestClient _client;

        private static string _baseUrl => "https://api.hubapi.com";
        private readonly HubSpotAuthenticationMode _mode;
        
        public string BasePath { get => _baseUrl; }

        
        private readonly string _privateAppKey;

        // Used for OAUTH
        public string AppId { get; private set; }
        private HubSpotToken _token;

        /// <summary>
        /// Creates a HubSpot client with the specified authentication scheme (Default: PRIVATE_APP_KEY). 
        /// </summary>
        /// <param name="privateAppKey"></param>
        /// <param name="mode"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        public HubSpotBaseClient(string privateAppKey, HubSpotAuthenticationMode mode = HubSpotAuthenticationMode.PRIVATE_APP_KEY, string appId = "", HubSpotToken token = null)
        { 
            _privateAppKey = privateAppKey;
            _client = new RestClient(_baseUrl);
            _mode = mode;
            _token = token;
            AppId = appId;
        }

        public T Execute<T>(string path, Method method = Method.GET) where T : new() 
            => SendReceiveRequest<T>(path, method);

        public T Execute<T, K>(string absoluteUriPath, K entity, Method method = Method.GET) where T : new() 
            => SendReceiveRequest<T, K>(absoluteUriPath, method, entity);

        public void ExecuteOnly(string absoluteUriPath, Method method = Method.GET) 
            => SendOnlyRequest(absoluteUriPath, method);

        public void ExecuteOnly<T>(string absoluteUriPath, T entity, Method method = Method.GET) 
            => SendOnlyRequest(absoluteUriPath, method, entity);

        public void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET) 
            => SendOnlyRequest(absoluteUriPath, method, entities);

        public T ExecuteMultipart<T>(string absoluteUriPath, byte[] data, string filename, Dictionary<string,string> parameters, Method method = Method.POST)
        {
            var fullUrl = $"{_baseUrl}{absoluteUriPath}";
            var request = ConfigureRequestAuthentication(fullUrl, method);

            request.AddFile(filename, data, filename);
        
            foreach (var kvp in parameters)
            {
                request.AddParameter(kvp.Key, kvp.Value);
            }

            var response = _client.Execute(request);
            if (!response.IsSuccessful)
                throw new HubSpotException("Error from HubSpot", response.Content); // lettuce get some good exception info back

            return JsonConvert.DeserializeObject<T>(response.Content);         
        }

        /// <summary>
        /// Updates the OAuth token used by the client.
        /// </summary>
        /// <param name="token"></param>
        public void UpdateToken(HubSpotToken token) 
            => _token = token;

        #region 'private methods'
        /// <summary>
        /// Sends requests to the given endpoint and returns an entity object of T.
        /// </summary>
        /// <typeparam name="T">The expected return entity type.</typeparam>
        /// <param name="path">The path to the endpoint.</param>
        /// <param name="method">The REST method used.</param>
        /// <returns>An entity of type T returned from the request.</returns>
        private T SendReceiveRequest<T>(string path, Method method) where T : new()
        {
            var request = ConfigureRequestAuthentication(path, method);
            var response = _client.Execute(request);

            if (response.IsSuccessful == false)
                throw new HubSpotException("Error from HubSpot", new HubSpotError(response.StatusCode, response.StatusDescription), response.Content);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        /// <summary>
        /// Sends requests with a given entity JSON body to the target endpoint and returns a result object.
        /// </summary>
        /// <typeparam name="T">The type for the return object.</typeparam>
        /// <typeparam name="K">The type of the sending object.</typeparam>
        /// <param name="path">The path to the endpoint.</param>
        /// <param name="method">The REST method used.</param>
        /// <param name="entity">The entity being sent in the request.</param>
        /// <returns>An entity of type T returned from the request.</returns>
        private T SendReceiveRequest<T,K>(string path, Method method, K entity) where T: new()
        {
            var request = ConfigureRequestAuthentication(path, method);
           
            if(!entity.Equals(default(K)))
                request.AddJsonBody(entity);            

            var response = _client.Execute(request);

            if (response.IsSuccessful == false)
                throw new HubSpotException("Error from HubSpot", new HubSpotError(response.StatusCode, response.StatusDescription), response.Content);


            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        /// <summary>
        /// Sends a one way request to the server with no return data.
        /// </summary>
        /// <typeparam name="T">The outbound entity type.</typeparam>
        /// <param name="path">The endpoint target.</param>
        /// <param name="method">The REST method to use.</param>
        /// <param name="entity">The entity being sent to the endpoint.</param>
        private void SendOnlyRequest<T>(string path, Method method, T entity)
        {

            var request = ConfigureRequestAuthentication(path, method);

            if (!entity.Equals(default(T)))
                request.AddJsonBody(entity);

            var response = _client.Execute(request);

            if (!response.IsSuccessful)
                throw new HubSpotException("Error from HubSpot", new HubSpotError(response.StatusCode, response.StatusDescription), response.Content);
        }

        /// <summary>
        /// Sends a one way request to the server with no return data.
        /// </summary>
        /// <param name="path">The endpoint target.</param>
        /// <param name="method">The REST method to use.</param>
        private void SendOnlyRequest(string path, Method method)
        {

            var request = ConfigureRequestAuthentication(path, method);

            var response = _client.Execute(request);

            if (!response.IsSuccessful)
                throw new HubSpotException("Error from HubSpot", new HubSpotError(response.StatusCode, response.StatusDescription), response.Content);
        }
        /// <summary>
        /// Configures a RestRequest based on the authentication scheme detected and configures the endpoint path relative to the base path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private RestRequest ConfigureRequestAuthentication(string path, Method method)
        {
            var fullPath = $"{BasePath.TrimEnd('/')}/{path.Trim('/')}";
            var request = new RestRequest(fullPath, method, DataFormat.Json);
            switch(_mode)
            {
                case HubSpotAuthenticationMode.OAUTH:
                    request.AddHeader("Authorization", GetAuthHeaderForToken(_token));
                    break;
                case HubSpotAuthenticationMode.PRIVATE_APP_KEY:
                    request.AddHeader("Authorization", GetAuthHeaderForPrivateApp(_privateAppKey));
                    request.AddHeader("Content-Type", "application/json");
                    break;
            }

            request.JsonSerializer = new NewtonsoftRestSharpSerializer();            
            return request;
        }

        private string GetAuthHeaderForToken(HubSpotToken token) 
            => $"Bearer {token.AccessToken}";
        
        private static string GetAuthHeaderForPrivateApp(string token) 
            => $"Bearer {token}";
        #endregion
    }
     
    public enum HubSpotAuthenticationMode
    {
        OAUTH,
        PRIVATE_APP_KEY
        
    }
}