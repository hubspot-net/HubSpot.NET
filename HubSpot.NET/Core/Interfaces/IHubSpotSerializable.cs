using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotSerializable<T> : IHubSpotModel
    {
        void ToHubSpotDataEntity(ref T dataEntity);

        void FromHubSpotDataEntity(T hubspotData);
    }
}
