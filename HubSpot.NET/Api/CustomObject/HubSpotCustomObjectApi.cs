using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Extensions;
using HubSpot.NET.Core.Interfaces;
using Newtonsoft.Json;

namespace HubSpot.NET.Api.CustomObject;

public class CustomObjectListHubSpotModel<T> : IHubSpotModel where T: CustomObjectHubSpotModel, new()
{
    [DataMember(Name = "results")]
    public IList<T> Results { get; set; } = new List<T>();
    public bool IsNameValue => false;        
    
    public string RouteBasePath => "crm/v3/objects";
    public virtual void ToHubSpotDataEntity(ref dynamic converted)
    {
    }

    public virtual void FromHubSpotDataEntity(dynamic hubspotData)
    {
    }
}

[DataContract]
public class CustomObjectHubSpotModel : IHubSpotModel
{

    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "createdAt")]
    public DateTime? CreatedAt { get; set; }

    [DataMember(Name = "updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [IgnoreDataMember]
    [JsonProperty(PropertyName = "properties")]
    public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    public bool IsNameValue => false;
    public void ToHubSpotDataEntity(ref dynamic dataEntity)
    {
    }

    public void FromHubSpotDataEntity(dynamic hubspotData)
    {
    }

    public string RouteBasePath => "crm/v3/objects";
}


public class HubSpotCustomObjectApi : IHubSpotCustomObjectApi
{
    private readonly IHubSpotClient _client;

    public HubSpotCustomObjectApi(IHubSpotClient client)
    {
        _client = client;
    }
    
    public CustomObjectListHubSpotModel<T> List<T>(string idForCustomObject, ListRequestOptions opts = null) where T : CustomObjectHubSpotModel, new()
    {
        opts ??= new ListRequestOptions();

        var path = $"{new CustomObjectHubSpotModel().RouteBasePath}/2-{idForCustomObject}"
            .SetQueryParam("count", opts.Limit);

        if (opts.PropertiesToInclude.Any())
            path = path.SetQueryParam("property", opts.PropertiesToInclude);

        if (opts.Offset.HasValue)
            path = path.SetQueryParam("vidOffset", opts.Offset);

        var response = _client.ExecuteList<CustomObjectListHubSpotModel<T>>(path, convertToPropertiesSchema: true);
        return response;
    }
}