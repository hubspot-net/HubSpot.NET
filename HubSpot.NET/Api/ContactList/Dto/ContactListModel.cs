using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.ContactList.Dto
{
    [DataContract]
    public class ContactListModel: IHubSpotModel
    {
        [DataMember(Name = "listId")]
        public long ListId { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "listType")]
        public string ListType { get; set; }

        [DataMember(Name = "dynamic")]
        public bool Dynamic { get; set; }
        
        [DataMember(Name = "metadata")]
        public dynamic Metadata { get; set; }

        [IgnoreDataMember]
        public bool IsNameValue => true;

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