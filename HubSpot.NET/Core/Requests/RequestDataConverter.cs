using HubSpot.NET.Api.Shared;
using HubSpot.NET.Core.Extensions;
using HubSpot.NET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Requests
{
    public class RequestDataConverter
    {

        //Methods
        /// <summary>
        /// Converts a single "dynamic" representation of an entity into a typed entity
        /// </summary>
        /// <remarks>
        /// The dynamic object being passed in should have a prop called "properties" which contains all the object properties to map, as well
        /// as vid and other root level objects stored in the HubSpot JSON response
        /// </remarks>
        /// <param name="dynamicObject">An <see cref="ExpandoObject"/> instance that contains a single HubSpot entity to deserialize</param>
        /// <param name="dto">An instantiated DTO that shall recieve data</param>
        /// <returns>The populated DTO</returns>
        internal object ConvertSingleEntity(ExpandoObject dynamicObject, object dto)
        {
            var expandoDict = (IDictionary<string, object>)dynamicObject;
            var dtoProps = dto.GetType().GetProperties();

            // The vid is the "id" of the entity
            if (expandoDict.TryGetValue("vid", out var vidData))
            {
                // TODO use properly serialized name of prop to find it
                var vidProp = dtoProps.SingleOrDefault(q => q.GetPropSerializedName() == "vid");
                vidProp?.SetValue(dto, vidData);
            }

            // The dealId is the "id" of the deal entity
            if (expandoDict.TryGetValue("dealId", out var dealIdData))
            {
                // TODO use properly serialized name of prop to find it
                var dealIdProp = dtoProps.SingleOrDefault(q => q.GetPropSerializedName() == "dealId");
                dealIdProp?.SetValue(dto, dealIdData);
            }

            // The companyId is the "id" of the company entity
            if (expandoDict.TryGetValue("companyId", out var companyIdData))
            {
                // TODO use properly serialized name of prop to find it
                var companyIdProp = dtoProps.SingleOrDefault(q => q.GetPropSerializedName() == "companyId");
                companyIdProp?.SetValue(dto, companyIdData);
            }

            // The Properties objec in the json / response data contains all the props we wish to map - if that does not exist
            // we cannot proceed
            if (!expandoDict.TryGetValue("properties", out var dynamicProperties)) return dto;

            foreach (var dynamicProp in dynamicProperties as ExpandoObject)
            {
                // prop.Key contains the name of the property we wish to map into the DTO
                // prop.Value contains the data returned by HubSpot, which is also an object
                // in there we need to go get the "value" prop to get the actual value

                if (!((IDictionary<string, Object>)dynamicProp.Value).TryGetValue("value", out object dynamicValue))
                {
                    continue;
                }

                // TODO use properly serialized name of prop to find and set it's value
                try
                {
                    var targetProp = dtoProps.SingleOrDefault(q => q.GetPropSerializedName() == dynamicProp.Key);
                    if (targetProp != null)
                    {
                        Type t = Nullable.GetUnderlyingType(targetProp.PropertyType) ?? targetProp.PropertyType;

                        var value = t.IsValueType ? Activator.CreateInstance(t) : null;
                        
                        if (!string.IsNullOrEmpty(dynamicValue.ToString()))
                        {
                            if (t == typeof(System.Decimal)) {                                
                                decimal number = 0;
                                var style = NumberStyles.AllowDecimalPoint;
                                var culture = CultureInfo.CreateSpecificCulture("fr-FR");
                                if (Decimal.TryParse(dynamicValue.ToString(), style, culture, out number))
                                    value = number;
                                else
                                    value = Math.Round(Decimal.Parse(dynamicValue.ToString(), new NumberFormatInfo() { NumberDecimalSeparator = "." }), 4);

                            } 
                            else value = Convert.ChangeType(dynamicValue, t);
                        }

                        targetProp?.SetValue(dto, dynamicValue.GetType() == targetProp.PropertyType ? dynamicValue : value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ConvertSingleEntity, ERROR : {ex}");
                }

            }

            if (!expandoDict.TryGetValue("associations", out var dynamicAssociations)) return dto;
            foreach (var dynamicAssoc in dynamicAssociations as ExpandoObject)
            {
                var value = dynamicAssoc.Value as List<object>;
                
                if (dynamicAssoc.Key != "associatedCompanyIds" && dynamicAssoc.Key != "associatedVids"
                    && dynamicAssoc.Key != "associatedDealIds")
                {
                    continue;
                }

                // TODO use properly serialized name of prop to find and set it's value
                var targetProp = dtoProps.SingleOrDefault(q => q.GetPropSerializedName() == dynamicAssoc.Key);
                targetProp?.SetValue(dto, value.OfType<long>().ToList());
                
            }
            return dto;
        }

        public T FromHubSpotListResponse<T>(ExpandoObject dynamicObject) where T : IHubSpotModel, new()
        {
            // get a handle to the underlying dictionary values of te ExpandoObject
            var expandoDict = (IDictionary<string, object>)dynamicObject;

            // For LIST contacts the "contacts" property should be populated, for LIST companies the "companies" property should be populated, and so on
            // in our T item, search for a property that is an IList<IHubSpotModel> and use that as our prop name selector into the DynamoObject....
            // So on the IContactListHubSpotModel whe have a IList<IHubSpotModel> Contacts - find that prop, lowercase to contacts and that prop should
            //  be in the DynamoObject from HubSpot! Tricky stuff
            var targetType = typeof(IHubSpotModel);
            var data = new T();
            var dataProps = data.GetType().GetProperties();
            var dataTargetProp = dataProps.SingleOrDefault(p => targetType.IsAssignableFrom(p.PropertyType.GenericTypeArguments.FirstOrDefault()));

            if (dataTargetProp == null)
            {
                throw new ArgumentException("Unable to locate a property on the data class that implements IList<T> where T is a IHubSpotModel");
            }

            var propSerializedName = dataTargetProp.GetPropSerializedName();
            if (!expandoDict.ContainsKey(propSerializedName))
            {
                throw new ArgumentException($"The json data does not contain a property of name {propSerializedName} which is required to decode the json data");
            }

            // Find the generic type for the List in question
            var genericEntityType = dataTargetProp.PropertyType.GenericTypeArguments.First();
            // get a handle to Add on the list (actually from ICollection)
            var listAddMethod = dataTargetProp.PropertyType.FindMethodRecursively("Add", genericEntityType);
            // Condensed version of : https://stackoverflow.com/a/4194063/1662254
            var listInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericEntityType));
            if (listAddMethod == null)
            {
                throw new ArgumentException("Unable to locat Add method on the list of items to deserialize into - is it an IList ?");
            }

            // Convert all the entities
            var jsonEntities = expandoDict[propSerializedName];
            foreach (var entry in jsonEntities as List<object>)
            {
                // convert single entity
                var expandoEntry = entry as ExpandoObject;
                var dto = ConvertSingleEntity(expandoEntry, Activator.CreateInstance(genericEntityType));
                // add entity to list
                listAddMethod.Invoke(listInstance, new[] { dto });
            }
            // assign our reflected list instance onto the data object
            dataTargetProp.SetValue(data, listInstance);
            var allPropNamesInSerializedFormat = GetAllPropsWithSerializedNameAsKey(dataProps);
            // Now try to map any remaining props from the dynamo object into the response dto we shall return
            foreach (var kvp in expandoDict)
            {
                // skip the property with all the items for the response as we have already mapped that
                if (kvp.Key == propSerializedName) continue;

                // The Key of the currend item should be mapped, so we have to find a property in the target dto that "Serializes" into this value...
                if (!allPropNamesInSerializedFormat.TryGetValue(kvp.Key, out PropertyInfo theProp))
                {
                    continue;
                }
                // whe have a property which name serializes to the kvp.Key, let's set the data

                // If theProp is a complex type we canot just use SetValue, we need a conversion
                if (theProp.PropertyType.IsComplexType())
                {
                    var expandoEntry = kvp.Value as ExpandoObject;
                    var dto = ConvertSingleEntity(expandoEntry, Activator.CreateInstance(theProp.PropertyType));
                    theProp.SetValue(data, dto);
                }
                else // simple value type, assign it
                {
                    theProp.SetValue(data, kvp.Value);
                }
            }
            return data;
        }

        /// <summary>
        /// Convert from the dynamicly typed <see cref="ExpandoObject"/> into a strongly typed <see cref="IHubSpotModel"/>
        /// </summary>
        /// <param name="dynamicObject">The <see cref="ExpandoObject"/> representation of the returned json</param>
        /// <returns></returns>
        public T FromHubSpotResponse<T>(ExpandoObject dynamicObject) where T : IHubSpotModel, new() =>
            ((T)this.ConvertSingleEntity(dynamicObject, new T()));

        private IDictionary<string, PropertyInfo> GetAllPropsWithSerializedNameAsKey(PropertyInfo[] dataProps)
        {
            var dictionary = new Dictionary<string, PropertyInfo>();
            foreach (var prop in dataProps)
            {
                var propName = prop.GetPropSerializedName();
                dictionary.Add(propName, prop);
            }
            return dictionary;
        }

        /// <summary>
        /// Converts the given <paramref name="entity"/> to a hubspot data entity
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public dynamic ToHubSpotDataEntity(IHubSpotModel entity)
        {
            dynamic mapped = new ExpandoObject();

            /* HSCompanyId */
             var hsCompanyId = entity.GetType().GetProperties()
                .FirstOrDefault(x => x.Name == "HSCompanyId")?.GetValue(entity);

            if (hsCompanyId != null)
                mapped.ObjectId = hsCompanyId;

            /* HSContactId */
            var hsContactId = entity.GetType().GetProperties()
                    .FirstOrDefault(x => x.Name == "HSContactId")?.GetValue(entity);

            if (hsContactId != null)
                mapped.Vid = hsContactId;

            /* HSDealId */
            var hsDealId = entity.GetType().GetProperties()
                    .FirstOrDefault(x => x.Name == "HSDealId")?.GetValue(entity);

            if (hsDealId != null)
                mapped.ObjectId = hsDealId;

            /* associatedCompanyIds */
            var hsAssociatedCompanyIds = entity.GetType().GetProperties()
                    .FirstOrDefault(x => x.Name == "associatedCompanyIds")?.GetValue(entity);

            var hsAssociatedVids = entity.GetType().GetProperties()
                .FirstOrDefault(x => x.Name == "associatedVids")?.GetValue(entity);

            if(hsAssociatedCompanyIds != null || hsAssociatedVids != null) {
                mapped.Associations = new Associations();

                if (hsAssociatedCompanyIds != null)
                {
                    var v = new List<long>();
                    var propValue = (List<long>)entity.GetType().GetProperties()
                        .FirstOrDefault(x => x.Name == "associatedCompanyIds")?.GetValue(entity);
                    mapped.Associations.AssociatedCompanyIds = propValue;
                }

                if (hsAssociatedVids != null)
                {
                    var propValue = (List<long>)entity.GetType().GetProperties()
                        .FirstOrDefault(x => x.Name == "associatedVids")?.GetValue(entity);
                    mapped.Associations.AssociatedVids = propValue;
                }

            }

            /* Properties */
            mapped.Properties = new List<HubspotDataEntityProp>();

            var allProps = entity.GetType().GetProperties();
            foreach (var prop in allProps)
            {
                if (prop.HasIgnoreDataMemberAttribute()) { continue; }
                var propSerializedName = prop.GetPropSerializedName();
                if (prop.Name.Equals("HSCompanyId")
                    || prop.Name.Equals("HSDealId")
                    || prop.Name.Equals("RouteBasePath") 
                    || prop.Name.Equals("IsNameValue")
                    || prop.Name.Equals("associatedCompanyIds")
                    || prop.Name.Equals("associatedVids")
                    || prop.Name.Equals("associatedDealIds")
                    || prop.Name.StartsWith("hs_")) 
                { continue; }                
                // IF we have an complex type on the entity that we are trying to convert, let's NOT get the 
                // string value of it, but simply pass the object along - it will be serialized later as JSON...
                var propValue = prop.GetValue(entity);
                var value = propValue.IsComplexType() ? propValue : propValue?.ToString();
                var item = new HubspotDataEntityProp
                {
                    Property = propSerializedName,
                    Value = value
                };
                if (entity.IsNameValue)
                {
                    item.Property = null;
                    item.Name = propSerializedName;
                }
                if (item.Value == null) { continue; }
                mapped.Properties.Add(item);
            }
            return mapped;
        }
    }
}
