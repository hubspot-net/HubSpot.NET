using HubSpot.NET.Api.Files.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCosFileApi
    {
        FolderHubSpotModel CreateFolder(FolderHubSpotModel folder);
        FileListHubSpotModel<T> Upload<T>(FileHubSpotModel entity) where T: FileHubSpotModel, new();
    }
}