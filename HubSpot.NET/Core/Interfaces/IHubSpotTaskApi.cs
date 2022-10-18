using HubSpot.NET.Api.Task.Dto;
using System.Collections.Generic;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotTaskApi
    {
        T Create<T>(T entity) where T : TaskHubSpotModel, new();
        void Delete(long dealId);
        T GetById<T>(long dealId, List<string> propertiesToInclude = null) where T : TaskHubSpotModel, new();
        T Update<T>(T entity) where T : TaskHubSpotModel, new();

        TaskListHubSpotModel<T> List<T>(ListRequestOptions opts = null)
            where T : TaskHubSpotModel, new();
    }
}