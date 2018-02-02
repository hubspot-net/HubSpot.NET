using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Owner.Dto
{

    /// <summary>
    /// Models a set of owners in HubSpot
    /// </summary>
    [DataContract]
    public class OwnerListHubSpotModel : IHubSpotModel, ICollection<OwnerHubSpotModel>
    {
        private List<OwnerHubSpotModel> Owners { get; } = new List<OwnerHubSpotModel>();

        public string RouteBasePath => "/owners/v2";

        public bool IsNameValue => false;
        public virtual void ToHubSpotDataEntity(ref dynamic converted)
        {
        }

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public IEnumerator<OwnerHubSpotModel> GetEnumerator()
        {
            return Owners.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(OwnerHubSpotModel item)
        {
            Owners.Add(item);
        }

        public void Clear()
        {
            Owners.Clear();
        }

        public bool Contains(OwnerHubSpotModel item)
        {
            return Owners.Contains(item);
        }

        public void CopyTo(OwnerHubSpotModel[] array, int arrayIndex)
        {
            Owners.CopyTo(array, arrayIndex);
        }

        public bool Remove(OwnerHubSpotModel item)
        {
            return Owners.Remove(item);
        }

        public int Count => Owners.Count;
        public bool IsReadOnly => false;
    }
}
