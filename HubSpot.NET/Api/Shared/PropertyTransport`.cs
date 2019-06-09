namespace HubSpot.NET.Api.Shared
{
    using HubSpot.NET.Api.Contact.Dto;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class PropertyTransport<T>
    {
        [DataMember(Name = "properties")]
        public PropertyValuePairCollection Properties { get; set; } = new PropertyValuePairCollection();
        
        public void ToPropertyTransportModel(T model)
        {
            PropertyInfo[] properties = model.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                var memberAttrib = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;
                object value = prop.GetValue(model);

                if (value == null || memberAttrib == null)                
                    continue;                

                if (prop.PropertyType.IsArray && typeof(PropertyValuePair).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    PropertyValuePair[] pairs = value as PropertyValuePair[];
                    foreach (var item in pairs)
                    {
                        Properties.Add(item);
                    }
                    continue;
                }
                else if(typeof(IEnumerable<>).IsAssignableFrom(prop.PropertyType) && typeof(PropertyValuePair).IsAssignableFrom(prop.PropertyType.GetElementType()))
                {
                    IEnumerable<PropertyValuePair> pairs = value as IEnumerable<PropertyValuePair>;
                    foreach (var item in pairs)
                    {
                        Properties.Add(item);
                    }
                    continue;
                }

                Properties.Add(new PropertyValuePair(memberAttrib.Name, value.ToString()));
            }
        }

        public void FromPropertyTransportModel(out T model)
        {
            model = (T) Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).FullName);

            PropertyInfo[] props = model.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                var memberAttrib = prop.GetCustomAttribute(typeof(DataMemberAttribute)) as DataMemberAttribute;

                PropertyValuePair pair = Properties[memberAttrib.Name];
                prop.SetValue(model, pair.Value);
            }
        }
    }
}
