namespace HubSpot.NET.Core.OAuth
{
	using RestSharp.Serializers;

	internal class FakeSerializer : ISerializer
    {
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }

        internal FakeSerializer()
        {
            ContentType = "application/x-www-form-urlencoded";
        }
        public string Serialize(object obj)
        {
            return obj.ToString();
        }
    }
}