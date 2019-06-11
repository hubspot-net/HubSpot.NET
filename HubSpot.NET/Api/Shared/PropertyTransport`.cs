namespace HubSpot.NET
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    [DataContract]
    public class PropertyTransport<TSource, TOutputPropertyType>
        where TSource : class
        where TOutputPropertyType : INameValuePair, new()
    {
        public PropertyTransport() { }

        public PropertyTransport(TSource model)
        {
            LoadProperties(model);
        }

        [DataMember(Name = "properties")]
        public List<TOutputPropertyType> Properties { get; set; } = new List<TOutputPropertyType>();
        
        public void LoadProperties(TSource model)
        {
            PropertyInfo[] properties = model.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                var memberAttrib = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;
                object value = prop.GetValue(model);

                if (value == null || memberAttrib == null)                
                    continue;                

                if (prop.PropertyType.IsArray && typeof(TOutputPropertyType).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    var pairs = value as TOutputPropertyType[];
                    foreach (var item in pairs)
                    {
                        Properties.Add(item);
                    }
                    continue;
                }
                else if(typeof(IEnumerable<>).IsAssignableFrom(prop.PropertyType) && typeof(TOutputPropertyType).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    var pairs = value as IEnumerable<TOutputPropertyType>;
                    foreach (var item in pairs)
                    {
                        Properties.Add(item);
                    }
                    continue;
                }


                Type listType = Properties.GetType();
                Type propertyPairType = listType.GetGenericArguments()[0];
                var nvPair = (TOutputPropertyType)Activator.CreateInstance(propertyPairType) ;
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

                TOutputPropertyType pair = Properties.Find(x => x.Name == memberAttrib.Name);
                prop.SetValue(model, pair.Value);
            }
        }
    }
}
