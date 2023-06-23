using System;
using System.Collections.Generic;
using HubSpot.NET.Core.Extensions;
using HubSpot.NET.Core.Interfaces;
using HubSpot.NET.Core.OAuth.Dto;
using HubSpot.NET.Core.Requests;
using HubSpot.NET.Core.Serializers;
using RestSharp;

namespace HubSpot.NET.Core
{
    public sealed class HubSpotBaseClient : IHubSpotClient
    {
        private readonly RequestSerializer _serializer = new RequestSerializer(new RequestDataConverter());
        private RestClient _client;

        public static string BaseUrl { get => "https://api.hubapi.com"; }

        private readonly HubSpotAuthenticationMode _mode;

        // Used for HAPIKEY method
        
        private readonly string _apiKey;

        // Used for OAUTH
        private HubSpotToken _token;

        private void Initialise()
        {
            _client = new RestClient(BaseUrl);
        }

        /// <summary>
        /// Creates a HubSpot client with the authentication scheme HAPIKEY.
        /// </summary>
        public HubSpotBaseClient(string privateAppKey)
        {
            _apiKey = privateAppKey;
            _mode = HubSpotAuthenticationMode.PRIVATE_APP_KEY;
            Initialise();
        }

        /// <summary>
        /// Creates a HubSpot client with the authentication scheme OAUTH.
        /// </summary>
        public HubSpotBaseClient(HubSpotToken token)
        {
            _token = token;
            _mode = HubSpotAuthenticationMode.OAUTH;
            Initialise();
        }

        public T Execute<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true) where T : IHubSpotModel, new()
        {
            return Execute<T>(absoluteUriPath, entity, method, convertToPropertiesSchema ? SerialisationType.PropertiesSchema : SerialisationType.Raw);
        }
        public T Execute<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag) where T : IHubSpotModel, new()
        {
            string json = (method == Method.GET || entity == null)
                ? null
                : _serializer.SerializeEntity(entity, serialisationType);

            T data = SendRequest(absoluteUriPath, method, json, responseData => (T)_serializer.DeserializeEntity<T>(responseData, serialisationType != SerialisationType.Raw));

            return data;
        }

        public T Execute<T>(string absoluteUriPath, Method method = Method.GET, bool convertToPropertiesSchema = true) where T : IHubSpotModel, new()
        {
            return Execute<T>(absoluteUriPath, method, convertToPropertiesSchema ? SerialisationType.PropertiesSchema : SerialisationType.Raw);

        }
        public T Execute<T>(string absoluteUriPath, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag) where T : IHubSpotModel, new()
        {
            T data = SendRequest(absoluteUriPath, method, null, responseData => (T)_serializer.DeserializeEntity<T>(responseData, serialisationType != SerialisationType.Raw));

            return data;
        }

        public void Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true)
        {
            Execute(absoluteUriPath, entity, method, convertToPropertiesSchema ? SerialisationType.PropertiesSchema : SerialisationType.Raw);
        }
        public void Execute(string absoluteUriPath, object entity = null, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag)
        {
            string json = (method == Method.GET || entity == null)
                ? null
                : _serializer.SerializeEntity(entity, serialisationType);

            SendRequest(absoluteUriPath, method, json);
        }

        public void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET, bool convertToPropertiesSchema = true)
        {
            ExecuteBatch(absoluteUriPath, entities, method, convertToPropertiesSchema ? SerialisationType.PropertiesSchema : SerialisationType.Raw);
        }
        public void ExecuteBatch(string absoluteUriPath, List<object> entities, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag)
        {
            string json = (method == Method.GET || entities == null)
                ? null
                : _serializer.SerializeEntity(entities, serialisationType);

            SendRequest(absoluteUriPath, method, json);
        }

        public T ExecuteMultipart<T>(string absoluteUriPath, byte[] data, string filename, Dictionary<string,string> parameters, Method method = Method.POST) where T : new()
        {
            string path = $"{BaseUrl}{absoluteUriPath}";
            IRestRequest request = ConfigureRequestAuthentication(path, method, null);

            request.AddFileBytes("file", data, filename);

            foreach (KeyValuePair<string, string> kvp in parameters)
            {
                if (string.IsNullOrEmpty(kvp.Value))
                {
                    continue;
                }
                
                request.AddParameter(kvp.Key, kvp.Value);
            }
               

            IRestResponse<T> response = _client.Execute<T>(request);

            T responseData = response.Data;

            if (!response.IsSuccessful())
                throw new HubSpotException("Error from HubSpot", new HubSpotError(response.StatusCode, response.StatusDescription));

            return responseData;
        }

        public T ExecuteList<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, bool convertToPropertiesSchema = true) where T : IHubSpotModel, new()
        {
            return ExecuteList<T>(absoluteUriPath, entity, method, convertToPropertiesSchema ? SerialisationType.PropertiesSchema : SerialisationType.Raw);
        }
        public T ExecuteList<T>(string absoluteUriPath, object entity = null, Method method = Method.GET, SerialisationType serialisationType = SerialisationType.PropertyBag) where T : IHubSpotModel, new()
        {
            string json = (method == Method.GET || entity == null)
                ? null
                : _serializer.SerializeEntity(entity, true);

            var data = SendRequest(
                absoluteUriPath,
                method,
                json,
                responseData => (T)_serializer.DeserializeListEntity<T>(responseData, serialisationType != SerialisationType.Raw));
            return data;
        }

        private T SendRequest<T>(string path, Method method, string json, Func<string, T> deserializeFunc) where T : IHubSpotModel, new()
        {
            string responseData = SendRequest(path, method, json);

            if (string.IsNullOrWhiteSpace(responseData))
                return default;

            return deserializeFunc(responseData);
        }

        private string SendRequest(string path, Method method, string json)
        {
            IRestRequest request = ConfigureRequestAuthentication(path, method);

            if (method != Method.GET && !string.IsNullOrWhiteSpace(json))
                request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _client.Execute(request);

            string responseData = response.Content;

            if (!response.IsSuccessful())
                throw new HubSpotException("Error from HubSpot", new HubSpotError(response.StatusCode, response.StatusDescription), responseData);

            return responseData;
        }

        /// <summary>
        /// Configures a <see cref="RestRequest"/> based on the authentication scheme detected and configures the endpoint path relative to the base path.
        /// </summary>
        private RestRequest ConfigureRequestAuthentication(string path, Method method, string contentType = "application/json")
        {
#if NET451
            RestRequest request = new RestRequest(path, method);
            request.RequestFormat = DataFormat.Json;
#else
            RestRequest request = new RestRequest(path, method, DataFormat.Json);
#endif
            switch (_mode)
            {
                case HubSpotAuthenticationMode.OAUTH:
                    request.AddHeader("Authorization", GetAuthHeader(_token));
                    break;
                case HubSpotAuthenticationMode.HAPIKEY:
                    throw new NotSupportedException(
                        "This authentication mode is no longer supported by hubspot as of November 30, 2020");
                default:
                    request.AddHeader("Authorization", $"Bearer {_apiKey}");

                    if (!contentType.IsNullOrEmpty())
                    {
                        request.AddHeader("Content-Type", contentType);
                    }
                    
                    break;
            }

            request.JsonSerializer = new NewtonsoftRestSharpSerializer();
            return request;
        }

        private string GetAuthHeader(HubSpotToken token) => $"Bearer {token.AccessToken}";


        /// <summary>
        /// Updates the OAuth token used by the client.
        /// </summary>
        public void UpdateToken(HubSpotToken token) => _token = token;
    }
}