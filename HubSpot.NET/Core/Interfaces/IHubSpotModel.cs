namespace HubSpot.NET.Core.Interfaces
{
    /// <summary>
    /// The base model for all HubSpot entities
    /// </summary>
    public interface IHubSpotModel
    {
        bool IsNameValue { get; }

        void ToHubSpotDataEntity(ref dynamic dataEntity);

        void FromHubSpotDataEntity(dynamic hubspotData);

        string RouteBasePath { get; }
    }
}
