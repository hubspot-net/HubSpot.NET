using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Files.Dto
{
    /// <summary>
    /// Models a folder entity, used in the COS Files API
    /// </summary>
    public class FolderHubSpotModel : IHubSpotModel
    {
        [DataMember(Name="id")]
        public long Id { get;set; }

        [DataMember(Name="name")]
        public string Name { get;set; }

        [DataMember(Name="parent_folder_id")]
        public long Parent {get; set; }

        public bool IsNameValue { get; }
    }
}
