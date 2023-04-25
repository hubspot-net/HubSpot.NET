using System.Threading;
using HubSpot.NET.Api.CustomObject;

namespace HubSpot.NET.Core.Interfaces;

public interface IHubSpotCustomObjectApi
{
    CustomObjectListHubSpotModel<T> List<T>(string idForCustomObject, ListRequestOptions opts = null) where T : CustomObjectHubSpotModel, new();

    CustomObjectListAssociationsModel<T> GetAssociationsToCustomObject<T>(string objectTypeId,
        string customObjectId,
        string idForDesiredAssociation, CancellationToken cancellationToken)
        where T : CustomObjectAssociationModel, new();

    string CreateWithDefaultAssociationToObject<T>(T entity, string associateObjectType, string associateToObjectId)
        where T : CreateCustomObjectHubSpotModel, new();
    
    string UpdateObject<T>(T entity)
        where T : UpdateCustomObjectHubSpotModel, new();
}