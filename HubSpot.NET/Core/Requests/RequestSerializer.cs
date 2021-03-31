using HubSpot.NET.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace HubSpot.NET.Core.Requests
{
    public class RequestSerializer
    {
        //Fields
        private readonly RequestDataConverter _requestDataConverter;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestSerializer"/> class.
        /// </summary>
        protected RequestSerializer()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestSerializer"/> class.
        /// </summary>
        /// <remarks>Use this constructor if you wish to override dependencies</remarks>
        /// <param name="requestDataConverter">The request data converter.</param>
        public RequestSerializer(RequestDataConverter requestDataConverter) : this()
        {
            this._requestDataConverter = requestDataConverter;
        }

        /// <summary>
        /// Deserialize the given JSON into a <see cref="IHubSpotModel"/>
        /// </summary>
        /// <param name="json">The json data returned by HubSpot tha should be converted</param>
        /// <returns>The deserialized entity</returns>
        public virtual IHubSpotModel DeserializeEntity<T>(string json) where T : IHubSpotModel, new()
        {
            var jobj = JsonConvert.DeserializeObject<ExpandoObject>(json);
            var converted = this._requestDataConverter.FromHubSpotResponse<T>(jobj);
            //converted.FromHubSpotDataEntity(jobj);

            return converted;
        }

        /// <summary>
        /// Deserialize the given JSON from a List request into a <see cref="IHubSpotModel"/>
        /// </summary>
        /// <param name="json">The JSON data returned from a List request to HubSpot</param>
        /// <returns></returns>
        public virtual IHubSpotModel DeserializeListEntity<T>(string json) where T : IHubSpotModel, new()
        {
            var expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(json);
            var converted = this._requestDataConverter.FromHubSpotListResponse<T>(expandoObject);

            return converted;
        }

        /// <summary>
        /// Serializez the entity to JSON.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The serialized entity</returns>
        public virtual string SerializeEntity(object obj)
        {            
            if (obj is IHubSpotModel entity)
            {
                var converted = _requestDataConverter.ToHubSpotDataEntity(entity);
                //entity.ToHubSpotDataEntity(ref converted);
                return JsonConvert.SerializeObject(converted, _jsonSerializerSettings);
            }
            return JsonConvert.SerializeObject(obj, _jsonSerializerSettings);
        }

        public virtual string SerializeBatchEntity<T>(ICollection<T> obj) where T : IHubSpotModel,new()
        {
            var result = new List<dynamic>();
            foreach(var i in obj)
            {
                var converted = _requestDataConverter.ToHubSpotDataEntity(i);
                //entity.ToHubSpotDataEntity(ref converted);
                result.Add(converted);
            }
            var test = JsonConvert.SerializeObject(result, _jsonSerializerSettings);

            return JsonConvert.SerializeObject(result, _jsonSerializerSettings);
        }
    }
}
