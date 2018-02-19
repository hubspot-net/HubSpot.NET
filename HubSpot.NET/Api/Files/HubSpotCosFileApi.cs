using System.Collections.Generic;
using HubSpot.NET.Api.Files.Dto;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Files
{
    public class HubSpotCosFileApi : IHubSpotCosFileApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotCosFileApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Uploads the given file to the File Manager
        /// Set hidden = true when using for attachments to engagements
        /// </summary>
        /// <param name="entity">The file to upload</param>
        /// <returns>The uploaded file</returns>
        public FileListHubSpotModel<T> Upload<T>(FileHubSpotModel entity) where T: FileHubSpotModel, new()
        {
            var path = $"{new FileHubSpotModel().RouteBasePath}/files";
            var data = _client.ExecuteMultipart<FileListHubSpotModel<T>>(path, entity.File, entity.Name,
                new Dictionary<string, string>()
                {
                    {"overwrite", entity.Overwrite.ToString()},
                    {"hidden", entity.Hidden.ToString()},
                    {"folder_paths", entity.FolderPaths}
                }); 
            return data;
        }

        /// <summary>
        /// Creates a folder within the File Manager
        /// </summary>
        /// <param name="folder">Folder to create</param>
        /// <returns>The created folder</returns>
        public FolderHubSpotModel CreateFolder(FolderHubSpotModel folder)
        {
            var path = $"{new FolderHubSpotModel().RouteBasePath}/folders";
            return _client.Execute<FolderHubSpotModel>(path, folder, Method.POST, false);
        }
        

    }
}
