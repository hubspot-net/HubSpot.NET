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
    public class OwnerListHubSpotModel<T> : IHubSpotModel, ICollection<T> 
        where T: OwnerHubSpotModel
    {
        private List<T> Owners { get; } = new List<T>();

        public bool IsNameValue => false;

        public IEnumerator<T> GetEnumerator() 
            => Owners.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item) 
            => Owners.Add(item);

        public void Clear() 
            => Owners.Clear();

        public bool Contains(T item) 
            => Owners.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) 
            => Owners.CopyTo(array, arrayIndex);

        public bool Remove(T item) 
            => Owners.Remove(item);

        public int Count 
            => Owners.Count;

        public bool IsReadOnly 
            => false;
    }
}
