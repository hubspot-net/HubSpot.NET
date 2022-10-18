namespace HubSpot.NET.Api.Task
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using HubSpot.NET.Api.Task.Dto;
    using HubSpot.NET.Core;
    using HubSpot.NET.Core.Extensions;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

    public class HubSpotTaskApi : IHubSpotTaskApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotTaskApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a Task entity
        /// </summary>
        /// <typeparam name="T">Implementation of TaskHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Create<T>(T entity) where T : TaskHubSpotModel, new()
        {
            string path = $"{entity.RouteBasePath}";

            return _client.Execute<T>(path, entity, Method.POST, SerialisationType.PropertyBag);
        }

        /// <summary>
        /// Gets a specific task by its ID
        /// </summary>
        /// <typeparam name="T">Implementation of TaskHubSpotModel</typeparam>
        /// <param name="taskId">The ID</param>
        /// <returns>The task entity or null if the task does not exist</returns>
        public T GetById<T>(long taskId, List<string> propertiesToInclude = null) where T : TaskHubSpotModel, new()
        {
            string path = $"{new T().RouteBasePath}/{taskId}";

            if (propertiesToInclude == null)
                propertiesToInclude = new List<string> { "hs_task_subject", "hubspot_owner_id", "hs_task_body", "hs_task_status", "hs_task_priority", "hs_task_type", "hs_timestamp" };

            if (propertiesToInclude.Any())
                path = path.SetQueryParam("properties", propertiesToInclude);

            try
            {
                return _client.Execute<T>(path, Method.GET, SerialisationType.PropertyBag);
             }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        public TaskListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T: TaskHubSpotModel, new()
        {
            if (opts == null)
                opts = new ListRequestOptions();

            string path = $"{new T().RouteBasePath}"
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())
                path = path.SetQueryParam("properties", opts.PropertiesToInclude);

            if (opts.Offset.HasValue)
                path = path.SetQueryParam("after", opts.Offset);

            TaskListHubSpotModel<T> data = _client.ExecuteList<TaskListHubSpotModel<T>>(path, convertToPropertiesSchema: true);

            return data;
        }

        /// <summary>
        /// Updates a given task entity, any changed properties are updated
        /// </summary>
        /// <typeparam name="T">Implementation of TaskHubSpotModel</typeparam>
        /// <param name="entity">The task entity</param>
        /// <returns>The updated task entity</returns>
        public T Update<T>(T entity) where T : TaskHubSpotModel, new()
        {
            if (entity.Id == null || entity.Id < 1)
                throw new ArgumentException("Task entity must have an id set!");

            long entityId = entity.Id.Value;
            string path = $"{entity.RouteBasePath}/{entity.Id}";

            T data = _client.Execute<T>(path, entity, Method.PATCH, SerialisationType.PropertyBag);
            // this just undoes some dirty meddling
            entity.Id = entityId;

            return data;
        }

        /// <summary>
        /// Deletes the given task
        /// </summary>
        /// <param name="taskId">ID of the task</param>
        public void Delete(long taskId)
        {
            var path = $"{new TaskHubSpotModel().RouteBasePath}/{taskId}";

            _client.Execute(path, method: Method.DELETE, convertToPropertiesSchema: true);
        }
    }
}