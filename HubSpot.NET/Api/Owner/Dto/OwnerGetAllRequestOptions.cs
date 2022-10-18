namespace HubSpot.NET.Api.Owner.Dto
{
	/// <summary>
	/// Options used when querying for lists of owners.
	/// </summary>
	public class OwnerGetAllRequestOptions
	{
		/// <summary>
		/// If specified as true, include inactive owner (defined as owner without any active remotes.)
		/// </summary>
		public bool IncludeInactive { get; set; }

		/// <summary>
		/// Search for owners matching the specified email address.
		/// </summary>
		public string EmailAddress { get; set; }
	}
}
