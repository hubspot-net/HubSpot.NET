using System.Linq;
using Newtonsoft.Json;

namespace HubSpot.NET.Api.Shared
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class RawPropertyTransportModel<T>
    {
        [DataMember(Name = "properties")]
        [JsonProperty(PropertyName = "properties")]
        public Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        public RawPropertyTransportModel(NameTransportModel<T> properties)
        {
            ConvertToSimpleDictionary(properties);
        }

        private void ConvertToSimpleDictionary(NameTransportModel<T> properties)
        {
            foreach (var valuePair in properties.Properties)
            {
                Properties.Add(valuePair.Name, valuePair.Value);
            }
        }
    }

    
    

    [DataContract]
    public class NameTransportModel<T>
    {
        [DataMember(Name = "properties")]
        public List<NameValuePair> Properties { get; set; } = new List<NameValuePair>();


        public void ToPropertyTransportModel(T model)
        {
            var properties = model.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var memberAttrib = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;
                var value = prop.GetValue(model);
                if (value == null || memberAttrib == null)
                {
                    continue;
                }

                if (prop.PropertyType.IsArray && typeof(NameValuePair).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    var pairs = value as NameValuePair[];
                    Properties.AddRange(pairs);
                    continue;
                }
                else if (typeof(IEnumerable<>).IsAssignableFrom(prop.PropertyType) && typeof(NameValuePair).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    var pairs = value as IEnumerable<NameValuePair>;
                    Properties.AddRange(pairs);
                    continue;
                }

                Properties.Add(new NameValuePair() { Name = memberAttrib.Name, Value = value.ToString() });
            }
        }

        public void FromPropertyTransportModel(out T model)
        {
            model = (T)Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).FullName);

            var props = model.GetType().GetProperties();

            foreach (var prop in props)
            {
                var memberAttrib = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;

                var pair = Properties.Find(x => x.Name == memberAttrib.Name);
                prop.SetValue(model, pair.Value);
            }
        }
    }
}
