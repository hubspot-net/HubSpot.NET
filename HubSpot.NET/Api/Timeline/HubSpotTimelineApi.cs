namespace HubSpot.NET.Api.Timeline
{
    using HubSpot.NET.Api.Timeline.Dto;
    using HubSpot.NET.Core.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class HubSpotTimelineApi : IHubSpotTimelineApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotTimelineApi(IHubSpotClient client)
        {
            _client = client;
        }

        public TimelineEventTypeHubSpotModel CreateOrUpdateEvent(TimelineEventTypeHubSpotModel entity)
        {
            TimelineEventTypeHubSpotModel result = _client.Execute<TimelineEventTypeHubSpotModel>($"{entity.RouteBasePath}", entity, RestSharp.Method.POST);
            return result;
        }

        public void CreateEventType(TimelineEventHubSpotModel entity)
        {
            _client.Execute($"{entity.RouteBasePath}", entity, RestSharp.Method.POST);
        }

        public void DeleteEventType(long entityID)
        {
            string path = new TimelineEventTypeHubSpotModel().RouteBasePath;
            _client.Execute($"{path}/{entityID}", RestSharp.Method.DELETE);
        }

        public TimelineEventHubSpotModel GetEventById(long entityID)
        {
            string path = new TimelineEventHubSpotModel().RouteBasePath;
            return _client.Execute<TimelineEventHubSpotModel>($"{path}/{entityID}", RestSharp.Method.GET);
        }

        public IEnumerable<TimelineEventTypeHubSpotModel> GetAllEventTypes()
        {
            string path = new TimelineEventTypeHubSpotModel().RouteBasePath;
            return _client.Execute<TimelineEventTypeHubSpotModel>($"{path}", true);
        }

        public void UpdateEvent(TimelineEventHubSpotModel entity)
        {
            _client.Execute($"{entity.RouteBasePath}/{entity.Id}", entity, RestSharp.Method.PUT);
        }

        public void UpdateEventType(TimelineEventTypeHubSpotModel entity)
        {
            _client.Execute($"{entity.RouteBasePath}/{entity.Id}", entity, RestSharp.Method.PUT);
        }
    }
}
