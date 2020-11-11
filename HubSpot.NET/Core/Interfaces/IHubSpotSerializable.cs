namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotSerializable<T> : IHubSpotModel
    {
        void ToHubSpotDataEntity(ref T dataEntity);

        void FromHubSpotDataEntity(T hubspotData);
    }
}
