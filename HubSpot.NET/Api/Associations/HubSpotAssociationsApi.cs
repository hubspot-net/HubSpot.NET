using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Associations;

public class HubSpotAssociationsApi : IHubSpotAssociationsApi
{
    private readonly IHubSpotClient _client;
    public HubSpotAssociationsApi(IHubSpotClient client)
    {
        _client = client;
    }
    
    /// <summary>
    /// Adds the ability to associate via the default association
    /// See the PUT documentation here: https://developers.hubspot.com/docs/api/crm/associations
    /// </summary>
    /// <param name="objectType">the type of the object you're associating (e.g. contact).</param>
    /// <param name="objectId"> the ID of the record to associate.</param>
    /// <param name="toObjectType"> the ID of the record to associate.</param>
    /// <param name="toObjectId"> the ID of the record to associate.</param>
    public void AssociationToObject(string objectType, string objectId, string toObjectType, string toObjectId)
    {


        
        var associationPath =
            $"/crm/v4/objects/{objectType}/{objectId}/associations/default/{toObjectType}/{toObjectId}";
        _client.Execute(associationPath, null, Method.PUT, convertToPropertiesSchema: false);
        
    }
    
}