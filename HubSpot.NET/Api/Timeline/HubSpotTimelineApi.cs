namespace HubSpot.NET.Api.Timeline
{
    using HubSpot.NET.Api.Timeline.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using System.Collections.Generic;

    public class HubSpotTimelineApi : ApiRoutable, IHubSpotTimelineApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotTimelineApi(IHubSpotClient client)
        {
            _client = client;
            MidRoute = $"/integrations/v1/{client.AppId}";
        }

        public TimelineEventTypeHubSpotModel CreateOrUpdateEvent(TimelineEventTypeHubSpotModel entity)
        => _client.Execute<TimelineEventTypeHubSpotModel, TimelineEventTypeHubSpotModel>(GetRoute<TimelineEventTypeHubSpotModel>(), entity, RestSharp.Method.POST);
            

        public void CreateEventType(TimelineEventHubSpotModel entity)
        => _client.ExecuteOnly(GetRoute<TimelineEventHubSpotModel>(), entity, RestSharp.Method.POST);
        

        public void DeleteEventType(long entityID)
        => _client.ExecuteOnly(GetRoute<TimelineEventTypeHubSpotModel>(entityID.ToString()), RestSharp.Method.DELETE);
        

        public TimelineEventHubSpotModel GetEventById(long entityID)
        =>_client.Execute<TimelineEventHubSpotModel>(GetRoute<TimelineEventHubSpotModel>(entityID.ToString()));
        

        public IEnumerable<TimelineEventTypeHubSpotModel> GetAllEventTypes()
        => _client.Execute<List<TimelineEventTypeHubSpotModel>>(GetRoute<TimelineEventTypeHubSpotModel>());
        

        public void UpdateEvent(TimelineEventHubSpotModel entity)
        => _client.ExecuteOnly(GetRoute<TimelineEventHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT);
        

        public void UpdateEventType(TimelineEventTypeHubSpotModel entity)
        => _client.ExecuteOnly(GetRoute<TimelineEventTypeHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT);
        
    }
}