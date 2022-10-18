using System.Runtime.Serialization;

namespace HubSpot.NET.Api.Contact.Dto
{
	public enum SortingOrderType
	{
		[EnumMember(Value = "ASC")]
		Ascending,

		[EnumMember(Value = "DESC")]
		Descending
	}
}