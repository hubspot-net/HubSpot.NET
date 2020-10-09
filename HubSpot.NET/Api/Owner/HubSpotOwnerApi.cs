namespace HubSpot.NET.Api.Owner
{
    using HubSpot.NET.Api.Owner.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;

    public class HubSpotOwnerApi : ApiRoutable, IHubSpotOwnerApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/owners/v2";

        public HubSpotOwnerApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets all owners within your HubSpot account
        /// </summary>
        /// <returns>The set of owners</returns>
        public OwnerListHubSpotModel<T> GetAll<T>() where T : OwnerHubSpotModel
            => _client.Execute<OwnerListHubSpotModel<T>>(GetRoute<T>("owners"));
    }
}