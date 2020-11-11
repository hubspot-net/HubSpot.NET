namespace HubSpot.NET.Core.Interfaces
{
    using HubSpot.NET.Api.Timeline.Dto;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IHubSpotTimelineApi
    {
        void CreateOrUpdateEvent(TimelineEventHubSpotModel entity);
        Task CreateOrUpdateEventAsync(TimelineEventHubSpotModel entity, CancellationToken cancellationToken = default);
        void CreateEventType(TimelineEventTypeHubSpotModel entity);
        Task CreateEventTypeAsync(TimelineEventTypeHubSpotModel entity, CancellationToken cancellationToken = default);
        void DeleteEventType(long entityID);
        Task DeleteEventTypeAsync(long entityID, CancellationToken cancellationToken = default);
        void UpdateEventType(TimelineEventTypeHubSpotModel entity);
        Task UpdateEventTypeAsync(TimelineEventTypeHubSpotModel entity, CancellationToken cancellationToken = default);
        TimelineEventHubSpotModel GetEventById(long entityID);
        Task<TimelineEventHubSpotModel> GetEventByIdAsync(long entityID, CancellationToken cancellationToken = default);
        IEnumerable<TimelineEventTypeHubSpotModel> GetAllEventTypes();
        Task<IEnumerable<TimelineEventTypeHubSpotModel>> GetAllEventTypesAsync(CancellationToken cancellationToken = default);
    }
}
