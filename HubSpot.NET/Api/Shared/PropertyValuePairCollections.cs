using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HubSpot.NET.Api.Shared
{
    public class PropertyValuePairCollection : ICollection<PropertyValuePair>
    {
        public List<PropertyValuePair> Properties { get; set; } = new List<PropertyValuePair>();        

        public int Count => Properties.Count;

        public bool IsReadOnly => false;

        public PropertyValuePair this[string index]
        {
            get => Properties.FirstOrDefault(x => x.Property == index);
            set => this[index] = value;
        }

        public void Add(PropertyValuePair item) => Properties.Add(item);
        public void Clear() => Properties.Clear();
        public bool Contains(PropertyValuePair item) => Properties.Contains(item);
        public void CopyTo(PropertyValuePair[] array, int arrayIndex) => Properties.CopyTo(array, arrayIndex);
        public bool Remove(PropertyValuePair item) => Properties.Remove(item);
        public IEnumerator<PropertyValuePair> GetEnumerator() => Properties.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Properties.GetEnumerator();
    }
}
