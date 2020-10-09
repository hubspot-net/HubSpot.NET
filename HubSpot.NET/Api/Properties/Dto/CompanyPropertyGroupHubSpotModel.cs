using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Properties.Dto
{
    public class CompanyPropertyGroupHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "fulcrumPortalId")]
        public int FulcrumPortalId { get; set; }
        
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        
        [DataMember(Name = "displayOrder")]
        public int DisplayOrder { get; set; }
        
        [DataMember(Name = "hubspotDefined")]
        public bool HubSpotDefined { get; set; }
        
        public bool IsNameValue => false;
    }
}