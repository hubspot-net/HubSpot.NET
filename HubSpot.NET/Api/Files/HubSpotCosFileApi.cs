using Newtonsoft.Json;

namespace HubSpot.NET.Api.Files
{
    using System.Collections.Generic;
    using HubSpot.NET.Api.Files.Dto;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

    public class HubSpotCosFileApi : IHubSpotCosFileApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotCosFileApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a folder within the File Manager
        /// </summary>
        /// <param name="folder">Folder to create</param>
        /// <returns>The created folder</returns>
        public FolderHubSpotModel CreateFolder(FolderHubSpotModel folder)
        {
            var path = $"{new FolderHubSpotModel().RouteBasePath}";
            return _client.Execute<FolderHubSpotModel>(path, folder, Method.POST, false);
        }
        
        /// <summary>
        /// Uploads the given file to the File Manager
        /// </summary>
        /// <param name="entity">The file to upload</param>
        /// <returns>The uploaded file</returns>
        public FileHubSpotResponseModel UploadFile(FileHubSpotRequestModel entity)
        {
            var path = $"{new FileHubSpotRequestModel().RouteBasePath}/upload";
            var data = _client.ExecuteMultipart<FileHubSpotResponseModel>(path, entity.File, entity.Name,
                new Dictionary<string, string>()
                {
                    {"folderPath", entity.FolderPath},
                    {"folderId", entity.FolderId},
                    {"options", JsonConvert.SerializeObject(entity.Options)}
                }); 
            return data;
        }
        

    }
}
