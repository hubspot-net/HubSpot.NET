using HubSpot.NET.Api.Owner.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotOwnerApi
    {
        OwnerListHubSpotModel<T> GetAll<T>(OwnerGetAllRequestOptions opts = null)
            where T: OwnerHubSpotModel, new();
    }
}