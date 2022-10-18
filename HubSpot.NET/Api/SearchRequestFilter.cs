using System.Runtime.Serialization;

namespace HubSpot.NET.Api
{
    [DataContract]
    public class SearchRequestFilter
    {
        [DataMember(Name = "propertyName")]
        public string PropertyName { get; set; }

        [DataMember(Name = "operator")]
        public SearchRequestFilterOperatorType Operator { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        public SearchRequestFilter()
        {
            Operator = SearchRequestFilterOperatorType.EqualTo;
        }
    }
}