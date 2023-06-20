using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Files.Dto
{
    [DataContract]
    public class FileHubSpotResponseModel : IHubSpotModel
    {
        [DataMember(Name = "objects")]
        public List<FileHubSpotResponseObjectModel> Objects { get; set; }
        
        public bool IsNameValue { get; }

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public string RouteBasePath => "/filemanager/api/v3/files";
    }

    [DataContract]
    public class FileHubSpotResponseObjectModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "portal_id")]
        public int PortalId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "size")]
        public int Size { get; set; }

        [DataMember(Name = "height")]
        public object Height { get; set; }

        [DataMember(Name = "width")]
        public object Width { get; set; }

        [DataMember(Name = "encoding")]
        public object Encoding { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "extension")]
        public string Extension { get; set; }

        [DataMember(Name = "cloud_key")]
        public string CloudKey { get; set; }

        [DataMember(Name = "s3_url")]
        public string S3Url { get; set; }

        [DataMember(Name = "friendly_url")]
        public string FriendlyUrl { get; set; }

        [DataMember(Name = "meta")]
        public MetaData Meta { get; set; }

        [DataMember(Name = "created")]
        public long Created { get; set; }

        [DataMember(Name = "updated")]
        public long Updated { get; set; }

        [DataMember(Name = "deleted_at")]
        public int DeletedAt { get; set; }

        [DataMember(Name = "folder_id")]
        public long FolderId { get; set; }

        [DataMember(Name = "hidden")]
        public bool Hidden { get; set; }

        [DataMember(Name = "cloud_key_hash")]
        public string CloudKeyHash { get; set; }

        [DataMember(Name = "archived")]
        public bool Archived { get; set; }

        [DataMember(Name = "created_by")]
        public int CreatedBy { get; set; }

        [DataMember(Name = "deleted_by")]
        public object DeletedBy { get; set; }

        [DataMember(Name = "replaceable")]
        public bool Replaceable { get; set; }

        [DataMember(Name = "default_hosting_url")]
        public string DefaultHostingUrl { get; set; }

        [DataMember(Name = "teams")]
        public List<object> Teams { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "is_indexable")]
        public bool IsIndexable { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "cdn_purge_embargo_time")]
        public object CdnPurgeEmbargoTime { get; set; }

        [DataMember(Name = "file_hash")]
        public string FileHash { get; set; }
    }

    [DataContract]
    public class MetaData
    {
        [DataMember(Name = "allows_anonymous_access")]
        public bool AllowsAnonymousAccess { get; set; }

        [DataMember(Name = "expires_at")]
        public long ExpiresAt { get; set; }

        [DataMember(Name = "indexable")]
        public bool Indexable { get; set; }
    }
}
