using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Engagement.Dto
{
    /// <summary>
    /// Models an Engagement entity within HubSpot. 
    /// See API documentation for how this should be formed using 
    /// the correct type <see cref="EngagementHubSpotEngagementModel.Type"/>
    /// </summary>
    public class EngagementHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "engagement")]
        public EngagementHubSpotEngagementModel Engagement { get; set; }

        [DataMember(Name = "associations")]
        public EngagementHubSpotAssociationsModel Associations { get; set; }
        
        [DataMember(Name = "attachments")]
        public IList<EngagementHubSpotAttachmentModel> Attachments { get; set; }

        [DataMember(Name = "metadata")]
        public dynamic Metadata { get; set; }
        public bool IsNameValue => false;
    }

    public class EngagementHubSpotAttachmentModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }
    }

    public class EngagementHubSpotAssociationsModel
    {
        [DataMember(Name = "contactIds")]
        public IList<long> ContactIds { get; set; }

        [DataMember(Name = "companyIds")]
        public IList<long> CompanyIds { get; set; }

        [DataMember(Name = "dealIds")]
        public IList<long> DealIds { get; set; }
        
        [DataMember(Name = "ownerIds")]
        public IList<long> OwnerIds { get; set; }
    }

    public class EngagementHubSpotEngagementModel
    {
        [DataMember(Name = "id")]
        public long? Id { get; set; }

        [DataMember(Name = "type")]
        public string Type { get;set; }

        [DataMember(Name = "ownerId")]
        public long? OwnerId { get; set; }
    }
}
