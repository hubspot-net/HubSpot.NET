using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Deal.Dto
{
	/// <summary>
	/// Models the associations of a deal to companies & contacts
	/// </summary>
	[DataContract]
    public class DealHubSpotAssociations
    {
        [DataMember(Name = "associatedCompanyIds")]
        public long[] AssociatedCompany { get; set; }

        [DataMember(Name = "associatedVids")]
        public long[] AssociatedContacts { get; set; }
    }
}
