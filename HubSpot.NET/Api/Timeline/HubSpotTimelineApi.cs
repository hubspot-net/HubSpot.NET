namespace HubSpot.NET.Api.Timeline
{
    using HubSpot.NET.Api.Timeline.Dto;
	using HubSpot.NET.Core;
	using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using System.Collections.Generic;
	using System.Net;

	public class HubSpotTimelineApi : ApiRoutable, IHubSpotTimelineApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotTimelineApi(IHubSpotClient client)
        {
            _client = client;
            MidRoute = $"/integrations/v1/{client.AppId}";

            AddRoute<TimelineEventHubSpotModel>("/timeline/event");
            AddRoute<TimelineEventTypeHubSpotModel>("/timeline/event-types");
        }

        public void CreateOrUpdateEvent(TimelineEventHubSpotModel entity)
        {
            CreateTimelineEventModel transportModel = new CreateTimelineEventModel(entity.EventTypeId, entity.Id, entity.ContactEmail, entity.ExtraData);
            _client.ExecuteOnly(GetRoute<TimelineEventHubSpotModel>(), transportModel, RestSharp.Method.PUT);
        }
            

        public void CreateEventType(TimelineEventTypeHubSpotModel entity)
        => _client.ExecuteOnly(GetRoute<TimelineEventHubSpotModel>(), entity, RestSharp.Method.POST);
        

        public void DeleteEventType(long entityID)
        => _client.ExecuteOnly(GetRoute<TimelineEventTypeHubSpotModel>(entityID.ToString()), RestSharp.Method.DELETE);
        

        public TimelineEventHubSpotModel GetEventById(long entityID)
        {
            try
            {
                return _client.Execute<TimelineEventHubSpotModel>(GetRoute<TimelineEventHubSpotModel>(entityID.ToString()));
            }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        public IEnumerable<TimelineEventTypeHubSpotModel> GetAllEventTypes()
        => _client.Execute<List<TimelineEventTypeHubSpotModel>>(GetRoute<TimelineEventTypeHubSpotModel>());
        

        public void UpdateEvent(TimelineEventHubSpotModel entity)
        => _client.ExecuteOnly(GetRoute<TimelineEventHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT);
        

        public void UpdateEventType(TimelineEventTypeHubSpotModel entity)
        => _client.ExecuteOnly(GetRoute<TimelineEventTypeHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT);
        
    }
}