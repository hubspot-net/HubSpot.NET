using HubSpot.NET.Api.Owner.Dto;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Owner
{
    public class HubSpotOwnerApi : IHubSpotOwnerApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotOwnerApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets all owners within your HubSpot account
        /// </summary>
        /// <returns>The set of owners</returns>
        public OwnerListHubSpotModel GetAll()
        {
            var path = $"{new OwnerHubSpotModel().RouteBasePath}/owners";

            return _client.ExecuteList<OwnerListHubSpotModel>(path, convertToPropertiesSchema: false);
        }
    }
}