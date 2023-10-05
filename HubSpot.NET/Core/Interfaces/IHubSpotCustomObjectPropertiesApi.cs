using HubSpot.NET.Api.Properties.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotCustomObjectPropertiesApi
    {
        CustomObjectPropertyHubSpotModel GetProperty<T>(string customObjectName, string customObjectProperty) where T : CustomObjectPropertyHubSpotModel, new();
        CustomObjectPropertyHubSpotModel UpdateProperty<T>(string customObjectName, string customObjectProperty, CustomObjectPropertyHubSpotModel property) where T : CustomObjectPropertyHubSpotModel, new();
    }
}
