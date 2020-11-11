using HubSpot.NET.Api.Files.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCosFileApi
    {
        FolderHubSpotModel CreateFolder(FolderHubSpotModel folder);
        Task<FolderHubSpotModel> CreateFolderAsync(FolderHubSpotModel folder, CancellationToken cancellationToken = default);
        FileListHubSpotModel<FileHubSpotModel> Upload(FileHubSpotModel entity);
        Task<FileListHubSpotModel<FileHubSpotModel>> UploadAsync(FileHubSpotModel entity, CancellationToken cancellationToken = default);
    }
}