namespace HubSpot.NET.Core
{
    using HubSpot.NET.Api.OAuth.Dto;
    using HubSpot.NET.Core.Interfaces;
    using HubSpot.NET.Core.Requests;
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public class HubSpotBaseClient : IHubSpotClient
    {
        private readonly RequestSerializer _serializer = new RequestSerializer(new RequestDataConverter());
        private readonly RestClient _client;

        private string _baseUrl => "https://api.hubapi.com";
        private readonly HubSpotAuthenticationMode _mode;

        // Used for HAPIKEY method
        private readonly string _apiKeyName = "hapikey";
        private readonly string _apiKey;

        // Used for OAUTH
        public string AppId { get; private set; }
        private HubSpotToken _token;

        /// <summary>
        /// Creates a HubSpot client with the specified authentication scheme (Default: HAPIKEY). 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="mode"></param>
        public HubSpotBaseClient(string apiKey, HubSpotAuthenticationMode mode = HubSpotAuthenticationMode.HAPIKEY, string appId = "", HubSpotToken token = null)
        { 
            _apiKey = apiKey;
            _client = new RestClient(_baseUrl);
            _mode = mode;
            _token = token;
            AppId = appId;
        }


        public T Execute<T>(string absoluteUriPath, T entity = default, Method method = Method.GET, bool convertToPropertiesSchema = true)
        {
            string json = entity != default ? _serializer.SerializeEntity(entity, convertToPropertiesSchema) : null;
            return SendRequest(absoluteUriPath, method, json, responseData => _serializer.DeserializeEntity<T>(responseData, convertToPropertiesSchema));
        }

        public void Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true) 
            => SendRequest(absoluteUriPath, method, _serializer.SerializeEntity(entity, convertToPropertiesSchema));

        public void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET, bool convertToPropertiesSchema = true) 
            => SendRequest(absoluteUriPath, method, _serializer.SerializeEntity(entities, convertToPropertiesSchema));

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
            if (!response.IsSuccessful())
                throw new HubSpotException("Error from HubSpot", response.Content); // lettuce get some good exception info back

            return JsonConvert.DeserializeObject<T>(response.Content);         
        }

        public T ExecuteList<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true)
        {
            return SendRequest(absoluteUriPath, method, _serializer.SerializeEntity(entity), responseData =>
                {
                    return _serializer.DeserializeListEntity<T>(responseData, convertToPropertiesSchema);
                });
        }

        public void UpdateToken(HubSpotToken token)
        {
            _token = token;
        }

        #region 'private methods'
        private T SendRequest<T>(string path, Method method, string json, Func<string, T> deserializeFunc) 
        {
            string responseData = SendRequest(path, method, json);

            if (string.IsNullOrWhiteSpace(responseData))
                return default;            

            return deserializeFunc(responseData);
        }

        private string SendRequest(string path, Method method, string json)
        {

            RestRequest request = ConfigureRequestAuthentication(path, method);

            if (!string.IsNullOrWhiteSpace(json))            
                request.AddParameter("application/json", json, ParameterType.RequestBody);            

            IRestResponse response = _client.Execute(request);

            if (!response.IsSuccessful())            
                throw new HubSpotException("Error from HubSpot", response.Content);            

            return response.Content;
        } 
        
        private RestRequest ConfigureRequestAuthentication(string path, Method method)
        {
            RestRequest request = new RestRequest(path, method);
            switch(_mode)
            {
                case HubSpotAuthenticationMode.OAUTH:
                    request.AddHeader("Authorization", GetAuthHeader(_token));
                    break;
                default:
                    request.AddQueryParameter(_apiKeyName, _apiKey);
                    break;
            }

            return request;
        }

        private string GetAuthHeader(HubSpotToken token)
        {
            return $"Bearer {token.AccessToken}";
        }
        #endregion
    }

    public enum HubSpotAuthenticationMode
    {
        HAPIKEY, OAUTH
    }
}