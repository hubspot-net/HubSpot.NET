using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HubSpot.NET.Api.Pipeline.Dto
{
    [DataContract]
    public class PipelineListHubSpotModel<T> : IHubSpotModel where T : PipelineHubSpotModel, new()
    {

        [DataMember(Name = "results")]
        public IList<T> Pipelines { get; set; } = new List<T>();

        public string RouteBasePath => "/crm-pipelines/v1";

        public bool IsNameValue => false;

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }
    }
}