using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Company.Dto
{
	/// <summary>
	/// Models the associations of a deal to companies & contacts
	/// </summary>
	[DataContract]
    public class CompanyHubSpotAssociations
    {
        [DataMember(Name = "associatedDealIds")]
        public long[] AssociatedDeals { get; set; }

        [DataMember(Name = "associatedVids")]
        public long[] AssociatedContacts { get; set; }
    }
}
