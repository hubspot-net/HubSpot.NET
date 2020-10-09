using HubSpot.NET.Core.Interfaces;
using HubSpot.NET.Core.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Pipeline.Dto
{

    [DataContract]
    public class StageHubSpotModel
    {

        [DataMember(Name = "stageId")]
        string StageId { get; set; }

        [DataMember(Name = "createdAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        DateTime? CreatedDate { get; set; }

        [DataMember(Name = "updatedAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        DateTime? ModifiedDate { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }

        [DataMember(Name = "metadata")]
        public StageMetadata Metadata { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }
    }

    [DataContract]
    public class StageMetadata
    {
        [DataMember(Name = "isClosed")]
        public bool IsClosed { get; set; }

        [DataMember(Name = "probability")]
        public decimal Probability { get; set; }

        [DataMember(Name = "ticketState")]
        public string TicketState { get; set; }
    }

    [DataContract]
    public class PipelineHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "pipelineId")]
        string PipelineId { get; set; }

        [DataMember(Name = "createdAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        DateTime? CreatedDate { get; set; }

        [DataMember(Name = "updatedAt")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        DateTime? ModifiedDate { get; set; }

        [DataMember(Name = "objectType")]
        public string ObjectType { get; set; }

        [DataMember(Name = "objectTypeId")]
        public string ObjectTypeId { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }

        [DataMember(Name = "stages")]
        public IEnumerable<StageHubSpotModel> Stages { get; set; }

        [DataMember(Name = "default")]
        public bool Default { get; set; }

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public bool IsNameValue => false;

        public string RouteBasePath => "/crm-pipelines/v1";

    }

}
