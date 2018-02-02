using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Files.Dto
{
    /// <summary>
    /// Models a list of files
    /// </summary>
    public class FileListHubSpotModel : IHubSpotModel
    {
        [DataMember(Name="objects")]
        public List<FileHubSpotModel> Objects { get;set; }

        public bool IsNameValue { get; }

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public string RouteBasePath => "";
    }
}
