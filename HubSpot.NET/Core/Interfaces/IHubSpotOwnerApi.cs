using HubSpot.NET.Api.Owner.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotOwnerApi
    {
        OwnerListHubSpotModel<T> GetAll<T>() where T : OwnerHubSpotModel;
        Task<OwnerListHubSpotModel<T>> GetAllAsync<T>(CancellationToken cancellationToken = default) where T : OwnerHubSpotModel;
    }
}