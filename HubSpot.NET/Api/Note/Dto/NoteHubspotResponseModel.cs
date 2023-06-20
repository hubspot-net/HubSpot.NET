using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Note.Dto
{
    /// <summary>
    /// Models an Note entity within HubSpot. 
    /// See API documentation for how this should be formed using 
    /// the correct type <see cref="NoteHubSpotNoteModel.Type"/>
    /// </summary>
    
    [DataContract]
    public class NoteHubSpotResponseModel : IHubSpotModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }
        
        [DataMember(Name = "properties")]
        public NoteHubspotPropertiesModel Properties { get; set; }
        
        [DataMember(Name = "createdAt")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "updatedAt")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "archived")]
        public bool Archived { get; set; }
        
        public bool IsNameValue { get; }

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }
        
        public string RouteBasePath => "/crm/v3/objects/notes";
    }
    
    [DataContract]
    public class NoteHubspotPropertiesModel
    {
        [DataMember(Name = "hs_attachment_ids")]
        public string HsAttachmentIds { get; set; }

        [DataMember(Name = "hs_createdate")]
        public string HsCreateDate { get; set; }

        [DataMember(Name = "hs_lastmodifieddate")]
        public string HsLastModifiedDate { get; set; }

        [DataMember(Name = "hs_note_body")]
        public string HsNoteBody { get; set; }

        [DataMember(Name = "hs_object_id")]
        public string HsObjectId { get; set; }

        [DataMember(Name = "hs_timestamp")]
        public string HsTimestamp { get; set; }

        [DataMember(Name = "hubspot_owner_id")]
        public string HubspotOwnerId { get; set; }
    }
}
