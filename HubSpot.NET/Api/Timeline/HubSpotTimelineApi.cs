namespace HubSpot.NET.Api.Timeline
{
    using HubSpot.NET.Api.Timeline.Dto;
	using HubSpot.NET.Core;
	using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using System.Collections.Generic;
	using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

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

        public Task CreateOrUpdateEventAsync(TimelineEventHubSpotModel entity, CancellationToken cancellationToken = default)
        {
            CreateTimelineEventModel transportModel = new CreateTimelineEventModel(entity.EventTypeId, entity.Id, entity.ContactEmail, entity.ExtraData);
            return _client.ExecuteOnlyAsync(GetRoute<TimelineEventHubSpotModel>(), transportModel, RestSharp.Method.PUT, cancellationToken);
        }

        public void CreateEventType(TimelineEventTypeHubSpotModel entity)
            => _client.ExecuteOnly(GetRoute<TimelineEventHubSpotModel>(), entity, RestSharp.Method.POST);

        public Task CreateEventTypeAsync(TimelineEventTypeHubSpotModel entity, CancellationToken cancellationToken = default)
            => _client.ExecuteOnlyAsync(GetRoute<TimelineEventHubSpotModel>(), entity, RestSharp.Method.POST, cancellationToken);

        public void DeleteEventType(long entityID)
            => _client.ExecuteOnly(GetRoute<TimelineEventTypeHubSpotModel>(entityID.ToString()), RestSharp.Method.DELETE);
        
        public Task DeleteEventTypeAsync(long entityID, CancellationToken cancellationToken = default)
            => _client.ExecuteOnlyAsync(GetRoute<TimelineEventTypeHubSpotModel>(entityID.ToString()), RestSharp.Method.DELETE, cancellationToken);
        
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

        public async Task<TimelineEventHubSpotModel> GetEventByIdAsync(long entityID, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _client.ExecuteAsync<TimelineEventHubSpotModel>(GetRoute<TimelineEventHubSpotModel>(entityID.ToString()), cancellationToken: cancellationToken).ConfigureAwait(false);
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
        
        public async Task<IEnumerable<TimelineEventTypeHubSpotModel>> GetAllEventTypesAsync(CancellationToken cancellationToken = default)
        {
            return await _client.ExecuteAsync<List<TimelineEventTypeHubSpotModel>>(GetRoute<TimelineEventTypeHubSpotModel>(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public void UpdateEvent(TimelineEventHubSpotModel entity)
            => _client.ExecuteOnly(GetRoute<TimelineEventHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT);

        public Task UpdateEventAsync(TimelineEventHubSpotModel entity, CancellationToken cancellationToken = default)
            => _client.ExecuteOnlyAsync(GetRoute<TimelineEventHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT, cancellationToken);
        
        public void UpdateEventType(TimelineEventTypeHubSpotModel entity)
            => _client.ExecuteOnly(GetRoute<TimelineEventTypeHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT);

        public Task UpdateEventTypeAsync(TimelineEventTypeHubSpotModel entity, CancellationToken cancellationToken = default)
            => _client.ExecuteOnlyAsync(GetRoute<TimelineEventTypeHubSpotModel>(entity.Id.ToString()), entity, RestSharp.Method.PUT, cancellationToken);       
    }
}