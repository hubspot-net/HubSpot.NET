namespace HubSpot.NET.Api.Shared
{
    using HubSpot.NET.Api.Contact.Dto;
    using HubSpot.NET.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    [DataContract]
    public class PropertyTransport<TSource, TPropertyType>
        where TSource : class
        where TPropertyType : INameValuePair, new()
    {

        public PropertyTransport(TSource model)
        {
            LoadProperties(model);
        }

        [DataMember(Name = "properties")]
        public List<TPropertyType> Properties { get; set; } = new List<TPropertyType>();
        
        public void LoadProperties(TSource model)
        {
            PropertyInfo[] properties = model.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                var memberAttrib = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;
                object value = prop.GetValue(model);

                if (value == null || memberAttrib == null)                
                    continue;                

                if (prop.PropertyType.IsArray && typeof(TPropertyType).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    var pairs = value as TPropertyType[];
                    foreach (var item in pairs)
                    {
                        Properties.Add(item);
                    }
                    continue;
                }
                else if(typeof(IEnumerable<>).IsAssignableFrom(prop.PropertyType) && typeof(TPropertyType).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    var pairs = value as IEnumerable<TPropertyType>;
                    foreach (var item in pairs)
                    {
                        Properties.Add(item);
                    }
                    continue;
                }


                Type type = model.GetType();
                TPropertyType nvPair = (TPropertyType)Activator.CreateInstance(type) ;
                nvPair.Name = memberAttrib.Name;
                nvPair.Value = value.ToString();

                Properties.Add(nvPair);
            }
        }

        public void ExtractProperties(out TSource model)
        {
            model = (TSource) Assembly.GetAssembly(typeof(TSource)).CreateInstance(typeof(TSource).FullName);

            PropertyInfo[] props = model.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                var memberAttrib = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;

                TPropertyType pair = Properties.Find(x => x.Name == memberAttrib.Name);
                prop.SetValue(model, pair.Value);
            }
        }
    }
}
