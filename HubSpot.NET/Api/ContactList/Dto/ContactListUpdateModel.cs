using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.ContactList.Dto
{
    [DataContract]
    public class ContactListUpdateModel : IHubSpotModel
    {
        [DataMember(Name = "vids")] 
        public List<long> ContactIds = new List<long>();

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