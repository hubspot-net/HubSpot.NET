using HubSpot.NET.Api.Files.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCosFileApi
    {
        FolderHubSpotModel CreateFolder(FolderHubSpotModel folder);
        FileListHubSpotModel<FileHubSpotModel> Upload(FileHubSpotModel entity);
    }
}