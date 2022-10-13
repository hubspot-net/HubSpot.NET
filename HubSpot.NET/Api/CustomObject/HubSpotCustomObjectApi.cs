using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Api.Shared;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using NameValuePair = HubSpot.NET.Api.Shared.NameValuePair;

namespace HubSpot.NET.Api.CustomObject
{
    public class CustomObjectListHubSpotModel<T> : ListHubSpotModel, IHubSpotModel where T: IHubSpotModel
    {
        [DataMember(Name = "results")]
        public IList<T> Results { get; set; } = new List<T>();
        public bool IsNameValue => false;        
    }

    [DataContract]
    public class CustomObjectHubSpotModel : IHubSpotModel
    {
        public CustomObjectHubSpotModel()
        {
            
        }

        public CustomObjectHubSpotModel(string customObjectSchemaId)
        {
            CustomObjectSchemaId = customObjectSchemaId;
        }
        public string CustomObjectSchemaId { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTimeOffset? CreatedAt { get; set; }

        [DataMember(Name = "updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [IgnoreDataMember]
        [JsonProperty(PropertyName = "properties")]
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public bool IsNameValue => false;
    }
    
    public interface IHubSpotCustomObjectApi : IHubSpotCustomObjectApi<CustomObjectHubSpotModel>
    { }
    
    public interface IHubSpotCustomObjectApi<T> : ICRUDable<T>
        where T : IHubSpotModel
    {
        CustomObjectListHubSpotModel<T> List(ListRequestOptions opts = null);
    }
    
    public class HubSpotCustomObjectApi : ApiRoutable, IHubSpotCustomObjectApi
    {
        private readonly IHubSpotClient _client;
        // https://developers.hubspot.com/docs/api/crm/crm-custom-objects
        public override string MidRoute => "/crm/v3/objects";        

        public HubSpotCustomObjectApi(IHubSpotClient client)
        {
            _client = client;
        }


        public CustomObjectListHubSpotModel<CustomObjectHubSpotModel> List(ListRequestOptions opts = null)
        {
            opts = opts ?? new ListRequestOptions();

            var path = GetRoute<CustomObjectHubSpotModel>();

            return _client.Execute<CustomObjectListHubSpotModel<CustomObjectHubSpotModel>, ListRequestOptions>(path, opts);
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public CustomObjectHubSpotModel Create(CustomObjectHubSpotModel entity)
        {
            var model = new NameTransportModel<CustomObjectHubSpotModel>();
            model.ToPropertyTransportModel(entity);
            var rawModel = new RawPropertyTransportModel<CustomObjectHubSpotModel>(model);

            var baseRoute = GetRoute<CustomObjectHubSpotModel>();
            var postRoute = $"{baseRoute}2-{entity.CustomObjectSchemaId}";
            return _client.Execute<CustomObjectHubSpotModel, RawPropertyTransportModel<CustomObjectHubSpotModel>>(
                postRoute, rawModel, Method.POST);

        }

        public CustomObjectHubSpotModel GetById(long id)
        {
            throw new NotImplementedException();
        }

        public CustomObjectHubSpotModel Update(CustomObjectHubSpotModel entity)
        {
            throw new NotImplementedException();
        }
    }
}