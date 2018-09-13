using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using HubSpot.NET.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HubSpot.NET.Core.Requests
{
    public class RequestSerializer
    {
        private readonly RequestDataConverter _requestDataConverter;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="RequestSerializer"/> class.
        ///// </summary>
        //protected RequestSerializer()
        //{
        //    _jsonSerializerSettings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //        NullValueHandling = NullValueHandling.Ignore
        //    };
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="RequestSerializer"/> class.
        ///// </summary>
        ///// <remarks>Use this constructor if you wish to override dependencies</remarks>
        ///// <param name="requestDataConverter">The request data converter.</param>
        //public RequestSerializer(RequestDataConverter requestDataConverter) : this()
        //{
        //    _requestDataConverter = requestDataConverter;
        //}

        ///// <summary>
        ///// Serializes the entity to JSON.
        ///// </summary>
        ///// <param name="obj">The entity.</param>
        ///// <param name="convertToPropertiesSchema"></param>
        ///// <returns>The serialized entity</returns>
        //public virtual string SerializeEntity<T>(T obj, bool convertToPropertiesSchema = true) where T: new()
        //{
        //    //Type objType = obj.GetType();
        //    //if (objType.GetInterfaces().Contains(typeof(IHubSpotSerializable<T>)) && convertToPropertiesSchema)
        //    //{
        //    //    IHubSpotSerializable<T> entity = obj as IHubSpotSerializable<T>;

        //    //    var converted = _requestDataConverter.ToHubspotDataEntity(entity);

        //    //    entity.ToHubSpotDataEntity(ref converted);

        //    //    return JsonConvert.SerializeObject(
        //    //        converted,
        //    //        _jsonSerializerSettings);
        //    //}

        //    //return JsonConvert.SerializeObject(
        //    //    obj,
        //    //    _jsonSerializerSettings);
        //}


        ///// <summary>
        ///// Serializes the entity to JSON.
        ///// </summary>
        ///// <param name="obj">The entity.</param>
        ///// <param name="convertToPropertiesSchema"></param>
        ///// <returns>The serialized entity</returns>
        //public virtual string SerializeEntity(List<object> obj, bool convertToPropertiesSchema = true)
        //{
        //    if (convertToPropertiesSchema == false)
        //    {
        //        var objs = obj.Select(e =>
        //        {
        //            IHubSpotModel entity = (IHubSpotModel) e;
        //            var converted = _requestDataConverter.ToHubspotDataEntity(entity, true);
        //            if (entity.GetType().IsAssignableFrom(typeof(IHubSpotSerializable)))
        //            {
        //                IHubSpotSerializable mapped = entity as IHubSpotSerializable;                     
        //                mapped.ToHubSpotDataEntity(ref converted);
        //            }

        //            return converted;
        //        });
        //    }

        //    return JsonConvert.SerializeObject(
        //        obj,
        //        _jsonSerializerSettings);
        //}

        ///// <summary>
        ///// Deserialize the given JSON into a <see cref="HubSpotModel"/>
        ///// </summary>
        ///// <param name="json">The json data returned by HubSpot that should be converted</param>
        ///// <param name="deserializeAsProperties">Does this entity use the properties schema (contacts, deals, companies)</param>
        ///// <returns>The deserialized entity</returns>
        //public virtual T DeserializeEntity<T>(string json, bool deserializeAsProperties = true)
        //{
        //    if (deserializeAsProperties == false)  // Edge case           
        //        return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
            

        //    ExpandoObject jobj = JsonConvert.DeserializeObject<ExpandoObject>(json);
        //    T converted = _requestDataConverter.FromHubSpotResponse<T>(jobj);

        //    if (typeof(T).IsAssignableFrom(typeof(IHubSpotSerializable)))
        //        (converted as IHubSpotSerializable).FromHubSpotDataEntity(jobj);

        //    return converted;           
        //}

        ///// <summary>
        ///// Deserialize the given JSON from a List requet into a <see cref="HubSpotModel"/>
        ///// </summary>
        ///// <param name="json">The JSON data returned from a List request to HubSpot</param>
        ///// <param name="deserializeAsProperties">Does this entity use the properties schema (contacts, deals, companies)</param>
        ///// <returns></returns>
        //public virtual T DeserializeListEntity<T>(string json, bool deserializeAsProperties = true)
        //{
        //    if (deserializeAsProperties == false)
        //        return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);            

        //    var expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(json);
        //    var converted = _requestDataConverter.FromHubSpotListResponse<T>(expandoObject);
        //    return converted;
        //}
    }
}
