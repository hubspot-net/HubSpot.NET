using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
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


public class CustomObjectListAssociationsModel<T> : IHubSpotModel where T : CustomObjectAssociationModel, new()
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


public class CustomObjectAssociationModel
{
    public string Id { get; set; }
    public string Type { get; set; }
}


public class HubSpotCustomObjectApi : IHubSpotCustomObjectApi
{
    private readonly IHubSpotClient _client;

    private readonly string RouteBasePath = "crm/v3/objects";

    public HubSpotCustomObjectApi(IHubSpotClient client)
    {
        _client = client;
    }
    
    /// <summary>
    /// List all objects of a custom object type in your system
    /// </summary>
    /// <param name="idForCustomObject">Should be prefaced with "2-"</param>
    /// <param name="opts"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public CustomObjectListHubSpotModel<T> List<T>(string idForCustomObject, ListRequestOptions opts = null) where T : CustomObjectHubSpotModel, new()
    {
        opts ??= new ListRequestOptions();

        var path = $"{RouteBasePath}/{idForCustomObject}"
            .SetQueryParam("count", opts.Limit);

        if (opts.PropertiesToInclude.Any())
            path = path.SetQueryParam("property", opts.PropertiesToInclude);

        if (opts.Offset.HasValue)
            path = path.SetQueryParam("vidOffset", opts.Offset);

        var response = _client.ExecuteList<CustomObjectListHubSpotModel<T>>(path, convertToPropertiesSchema: false);
        return response;
    }

    /// <summary>
    /// Get the list of associations between two objects (BOTH CUSTOM and NOT) 
    /// </summary>
    /// <param name="objectTypeId"></param>
    /// <param name="customObjectId"></param>
    /// <param name="idForDesiredAssociation"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public CustomObjectListAssociationsModel<T> GetAssociationsToCustomObject<T>(string objectTypeId,
        string customObjectId,
        string idForDesiredAssociation, CancellationToken cancellationToken) where T : CustomObjectAssociationModel, new()
    {
        var path = $"{RouteBasePath}/{objectTypeId}/{customObjectId}/associations/{idForDesiredAssociation}";

        var response = _client.ExecuteList<CustomObjectListAssociationsModel<T>>(path, convertToPropertiesSchema:  false);
        return response;
    }
}