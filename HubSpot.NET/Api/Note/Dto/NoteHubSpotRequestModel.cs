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
    public class NoteHubSpotRequestModel : IHubSpotModel
    {
        [DataMember(Name = "properties")]
        public NoteHubSpotRequestPropertiesModel Properties { get; set; }

        [DataMember(Name = "associations")]
        public List<NoteHubSpotRequestAssociationsModel> Associations { get; set; }
        
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
    public class NoteHubSpotRequestPropertiesModel
    {
        [DataMember(Name = "hs_timestamp")]
        public string HsTimestamp { get; set; }

        [DataMember(Name = "hs_note_body")]
        public string HsNoteBody { get; set; }

        [DataMember(Name = "hubspot_owner_id")]
        public string HubspotOwnerId { get; set; }

        [DataMember(Name = "hs_attachment_ids")]
        public string HsAttachmentIds { get; set; }
    }
    
    [DataContract]
    public class NoteHubSpotRequestAssociationsModel
    {
        [DataMember(Name = "to")]
        public NoteHubSpotRequestAssociationToModel To { get; set; }

        [DataMember(Name = "types")]
        public List<NoteHubspotRequestAssociationTypeModel> Types { get; set; }
    }
    
    [DataContract]
    public class NoteHubSpotRequestAssociationToModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }

    [DataContract]
    public class NoteHubspotRequestAssociationTypeModel
    {
        [DataMember(Name = "associationCategory")]
        public string AssociationCategory { get; set; }

        [DataMember(Name = "associationTypeId")]
        public string AssociationTypeId { get; set; }
    }
}