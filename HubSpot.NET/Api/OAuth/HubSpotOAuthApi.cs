namespace HubSpot.NET.Api.OAuth
{
    using HubSpot.NET.Api.OAuth.Dto;
    using HubSpot.NET.Core;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using HubSpot.NET.Core.Serializers;
    using Newtonsoft.Json;
    using RestSharp;
    using System;

    public class HubSpotOAuthApi : ApiRoutable, IHubSpotOAuthApi
    {
        public string ClientId { get; private set; }
        private string _clientSecret;
        private IHubSpotClient _client;

        public override string MidRoute => " /oauth/v1/token";

        public HubSpotOAuthApi(IHubSpotClient client, string clientId, string clientSecret)
        {
            _client = client;
            ClientId = clientId;
            _clientSecret = clientSecret;
        }

        public HubSpotToken Authorize(string basePath, string redirectCode, string redirectUri)
        {
            RequestTokenHubSpotModel model = new RequestTokenHubSpotModel()
            {
                ClientId = ClientId,
                RedirectCode = redirectCode,
                RedirectUri = redirectUri
            };

            HubSpotToken token = InitiateRequest(model, basePath);
            _client.UpdateToken(token);
            return token;
        }

        public HubSpotToken Refresh(string basePath, string redirectUri, HubSpotToken token)
        {
            RequestRefreshTokenHubSpotModel model = new RequestRefreshTokenHubSpotModel()
            {
                ClientId = ClientId,
                ClientSecret = _clientSecret,
                RedirectUri = redirectUri,
                RefreshToken = token.RefreshToken
            };

            HubSpotToken refreshToken = InitiateRequest(model, basePath);
            _client.UpdateToken(refreshToken);
            return token;
        }

        public void UpdateCredentials(string id, string secret)
        {
            ClientId = id;
            _clientSecret = secret;
        }

        private HubSpotToken InitiateRequest<K>(K model, string basePath)
        {
            RestClient client = new RestClient();
            string path = $"{basePath.TrimEnd('/')}/{MidRoute}";
            RestRequest request = new RestRequest(new Uri(path));
            request.JsonSerializer = new NewtonsoftRestSharpSerializer(); // because we need a hero, one that can serialize all the things
            request.AddBody(model);

            IRestResponse<HubSpotToken> serverReponse = client.Post<HubSpotToken>(request);

            if (serverReponse.ResponseStatus != ResponseStatus.Completed)
            {
                throw new TimeoutException("Server did not respond to authorization request.");
            }

            if (serverReponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new HubSpotException("Error generating authentication token.", JsonConvert.DeserializeObject<HubSpotError>(serverReponse.Content));
            }

            return serverReponse.Data;
        }
    }
}
