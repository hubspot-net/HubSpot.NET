using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Schemas
{
    public class SchemasListHubSpotModel<T> : ListHubSpotModel, IHubSpotModel where T: IHubSpotModel
    {
        [DataMember(Name = "results")]
        public IList<T> Results { get; set; } = new List<T>();
        public bool IsNameValue => false;        
    }
    
    [DataContract]
    public class SchemasSchemaHubSpotModel : IHubSpotModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }

        [DataMember(Name = "updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [DataMember(Name = "properties")]
        public List<SchemasPropertyModel> Properties { get; set; }

        [DataMember(Name = "associations")]
        public List<SchemasAssociationModel> Associations { get; set; }

        [DataMember(Name = "labels")]
        public SchemasLabelsModel Labels { get; set; }

        [DataMember(Name = "requiredProperties")]
        public List<string> RequiredProperties { get; set; }

        [DataMember(Name = "searchableProperties")]
        public List<string> SearchableProperties { get; set; }

        [DataMember(Name = "primaryDisplayProperty")]
        public string PrimaryDisplayProperty { get; set; }

        [DataMember(Name = "metaType")]
        public string MetaType { get; set; }

        [DataMember(Name = "fullyQualifiedName")]
        public string FullyQualifiedName { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public bool IsNameValue => false;
    }

    public class SchemasAssociationModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "fromObjectTypeId")]
        public string FromObjectTypeId { get; set; }

        [DataMember(Name = "toObjectTypeId")]
        public string ToObjectTypeId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    public class SchemasLabelsModel
    {
        [DataMember(Name = "singular")]
        public string Singular { get; set; }

        [DataMember(Name = "plural")]
        public string Plural { get; set; }
    }

    public class SchemasPropertyModel
    {
        [DataMember(Name = "updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "fieldType")]
        public string FieldType { get; set; }

        [DataMember(Name = "groupName")]
        public string GroupName { get; set; }

        [DataMember(Name = "displayOrder")]
        public long? DisplayOrder { get; set; }

        [DataMember(Name = "calculated")]
        public bool? Calculated { get; set; }

        [DataMember(Name = "externalOptions")]
        public bool? ExternalOptions { get; set; }

        [DataMember(Name = "archived")]
        public bool? Archived { get; set; }

        [DataMember(Name = "hasUniqueValue")]
        public bool? HasUniqueValue { get; set; }
    }
    
    public interface IHubSpotSchemasApi : IHubSpotSchemasApi<SchemasSchemaHubSpotModel>
    { }
    
    public interface IHubSpotSchemasApi<T> : ICRUDable<T>
        where T : IHubSpotModel
    {
        SchemasListHubSpotModel<T> List(ListRequestOptions opts = null);
    }
    
    public class HubSpotSchemasApi : ApiRoutable, IHubSpotSchemasApi
    {
        private readonly IHubSpotClient _client;
        // https://developers.hubspot.com/docs/api/crm/crm-custom-objects
        public override string MidRoute => "/crm/v3/schemas";        

        public HubSpotSchemasApi(IHubSpotClient client)
        {
            _client = client;
        }


        public SchemasListHubSpotModel<SchemasSchemaHubSpotModel> List(ListRequestOptions opts = null)
        {
            opts = opts ?? new ListRequestOptions();

            var path = GetRoute<SchemasSchemaHubSpotModel>();

            return _client.Execute<SchemasListHubSpotModel<SchemasSchemaHubSpotModel>, ListRequestOptions>(path, opts);
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public SchemasSchemaHubSpotModel Create(SchemasSchemaHubSpotModel entity)
        {
            throw new NotImplementedException();
        }

        public SchemasSchemaHubSpotModel GetById(long id)
        {
            throw new NotImplementedException();
        }

        public SchemasSchemaHubSpotModel Update(SchemasSchemaHubSpotModel entity)
        {
            throw new NotImplementedException();
        }
    }
}