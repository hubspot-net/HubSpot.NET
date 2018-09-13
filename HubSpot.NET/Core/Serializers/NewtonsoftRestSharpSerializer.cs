namespace HubSpot.NET.Core.Serializers
{
    using Newtonsoft.Json;
    using RestSharp.Serializers;

    public class NewtonsoftRestSharpSerializer : ISerializer
    {

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public NewtonsoftRestSharpSerializer()
        {
            ContentType = "application/json";
        }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Content type for serialized content
        /// </summary>
        public string ContentType { get; set; }
    }
}
