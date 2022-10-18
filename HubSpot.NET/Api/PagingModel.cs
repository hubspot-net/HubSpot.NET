using System.Runtime.Serialization;

namespace HubSpot.NET.Api
{
	[DataContract]
    public class PagingModel
    {
        [DataMember(Name = "next")]
        public NextModel Next { get; set; }
    }
}
