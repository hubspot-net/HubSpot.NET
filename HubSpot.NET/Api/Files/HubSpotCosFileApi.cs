﻿namespace HubSpot.NET.Api.Files
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using HubSpot.NET.Api.Files.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

    public class HubSpotCosFileApi : ApiRoutable, IHubSpotCosFileApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/filemanager/api/v2";

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
        public FileListHubSpotModel<FileHubSpotModel> Upload(FileHubSpotModel entity)
        {
            var path = $"{GetRoute<FileHubSpotModel>()}/files";
            var data = _client.ExecuteMultipart<FileListHubSpotModel<FileHubSpotModel>>(path, entity.File, entity.Name,
                new Dictionary<string, string>()
                {
                    {"overwrite", entity.Overwrite.ToString()},
                    {"hidden", entity.Hidden.ToString()},
                    {"folder_paths", entity.FolderPaths}
                }); 
            return data;
        }

        /// <summary>
        /// Uploads the given file to the File Manager
        /// Set hidden = true when using for attachments to engagements
        /// </summary>
        /// <param name="entity">The file to upload</param>
        /// <returns>The uploaded file</returns>
        public Task<FileListHubSpotModel<FileHubSpotModel>> UploadAsync(FileHubSpotModel entity, CancellationToken cancellationToken = default)
        {
            var path = $"{GetRoute<FileHubSpotModel>()}/files";
            return _client.ExecuteMultipartAsync<FileListHubSpotModel<FileHubSpotModel>>(path, entity.File, entity.Name,
                new Dictionary<string, string>()
                {
                    {"overwrite", entity.Overwrite.ToString()},
                    {"hidden", entity.Hidden.ToString()},
                    {"folder_paths", entity.FolderPaths}
                }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a folder within the File Manager
        /// </summary>
        /// <param name="folder">Folder to create</param>
        /// <returns>The created folder</returns>
        public FolderHubSpotModel CreateFolder(FolderHubSpotModel folder)
        {
            var path = $"{GetRoute<FolderHubSpotModel>()}/folders";
            return _client.Execute<FolderHubSpotModel, FolderHubSpotModel>(path, folder, Method.POST);
        }

        /// <summary>
        /// Creates a folder within the File Manager
        /// </summary>
        /// <param name="folder">Folder to create</param>
        /// <returns>The created folder</returns>
        public Task<FolderHubSpotModel> CreateFolderAsync(FolderHubSpotModel folder, CancellationToken cancellationToken = default)
        {
            var path = $"{GetRoute<FolderHubSpotModel>()}/folders";
            return _client.ExecuteAsync<FolderHubSpotModel, FolderHubSpotModel>(path, folder, Method.POST, cancellationToken);
        }
    }
}
