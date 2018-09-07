using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotSerializable : IHubSpotModel
    {
        void ToHubSpotDataEntity(ref dynamic dataEntity);

        void FromHubSpotDataEntity(dynamic hubspotData);
    }
}
