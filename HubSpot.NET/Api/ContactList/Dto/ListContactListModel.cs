using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.ContactList.Dto
{
    [DataContract]
    public class ListContactListModel : IHubSpotModel
    {
        [DataMember(Name = "lists")]
        public List<ContactListModel> Lists { get; set; } = new List<ContactListModel>();
        
        /// <summary>
        /// Gets or sets a value indicating whether more results are available.
        /// </summary>
        /// <value>
        /// <c>true</c> if [more results available]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// This is a mapping of the "has-more" prop in the JSON return data from HubSpot
        /// </remarks>
        [DataMember(Name = "has-more")]
        public bool MoreResultsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the continuation offset.
        /// </summary>
        /// <value>
        /// The continuation offset.
        /// </value>
        /// <remarks>
        /// This is a mapping of the "vid-offset" prop in the JSON reeturn data from HubSpot
        /// </remarks>
        [DataMember(Name = "offset")]
        public long ContinuationOffset { get; set; }

        [IgnoreDataMember]
        public bool IsNameValue => false;

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        [IgnoreDataMember]
        public string RouteBasePath => "/contacts/v1/lists";
    }
}