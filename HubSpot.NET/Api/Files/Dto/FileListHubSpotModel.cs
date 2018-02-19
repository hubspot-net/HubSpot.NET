using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Files.Dto
{
    /// <summary>
    /// Models a list of files
    /// </summary>
    public class FileListHubSpotModel<T> : IHubSpotModel where T: FileHubSpotModel, new()
    {
        [DataMember(Name="objects")]
        public List<T> Objects { get;set; }

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
