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
    public class PropertiesListHubSpotModel<T> : IHubSpotModel, ICollection<T> where T : IHubSpotModel
    {
        private List<T> Properties { get; } = new List<T>();

        public string RouteBasePath
        {
            get
            {
                var entity = (T)Activator.CreateInstance(typeof(T));
                return "/properties/v1" + entity.RouteBasePath;
            }
        }

        public bool IsNameValue => false;
        public virtual void ToHubSpotDataEntity(ref dynamic converted)
        {
        }

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Properties.Add(item);
        }

        public void Clear()
        {
            Properties.Clear();
        }

        public bool Contains(T item)
        {
            return Properties.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Properties.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return Properties.Remove(item);
        }

        public int Count => Properties.Count;
        public bool IsReadOnly => false;
    }
}
