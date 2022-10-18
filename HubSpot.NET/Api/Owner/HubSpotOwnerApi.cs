namespace HubSpot.NET.Api.Owner
{
    using HubSpot.NET.Api.Owner.Dto;
    using HubSpot.NET.Core.Extensions;
    using HubSpot.NET.Core.Interfaces;

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
        public OwnerListHubSpotModel<T> GetAll<T>(OwnerGetAllRequestOptions opts = null)
            where T: OwnerHubSpotModel, new()
        {
            string path = $"{new OwnerHubSpotModel().RouteBasePath}/owners";

            if (opts != null)
            {
                if (opts.IncludeInactive)
                    path = path.SetQueryParam("includeInactive", "true");
                if (!string.IsNullOrWhiteSpace(opts.EmailAddress))
                    path = path.SetQueryParam("email", opts.EmailAddress);
            }

            return _client.ExecuteList<OwnerListHubSpotModel<T>>(path, convertToPropertiesSchema: false);
        }
    }
}