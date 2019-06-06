using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Properties.Dto
{

    /// <summary>
    /// Models a set of properties in HubSpot (contacts, companies etc.)
    /// </summary>
    [DataContract]
    public class PropertiesListHubSpotModel<T> : IHubSpotModel, ICollection<T> 
        where T : IHubSpotModel
    {
        private List<T> Properties { get; } = new List<T>();
        public bool IsNameValue => false;

        public IEnumerator<T> GetEnumerator() 
            => Properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item) 
            => Properties.Add(item);

        public void Clear() 
            => Properties.Clear();

        public bool Contains(T item) 
            => Properties.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) 
            => Properties.CopyTo(array, arrayIndex);

        public bool Remove(T item) 
            => Properties.Remove(item);

        public int Count 
            => Properties.Count;

        public bool IsReadOnly
            => false;
    }
}
