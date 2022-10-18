using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Owner.Dto
{
    /// <summary>
    /// Models a owner within HubSpot
    /// </summary>
    [DataContract]
    public class OwnerHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "ownerId")]
        [IgnoreDataMember]
        public long? Id { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        public string RouteBasePath => "/owners/v2";
        public bool IsNameValue => true;

        public virtual void ToHubSpotDataEntity(ref dynamic converted)
        {
        }

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }
    }
}
