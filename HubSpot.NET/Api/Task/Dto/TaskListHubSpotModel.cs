using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Task.Dto
{
    public class TaskListHubSpotModel<T> : IHubSpotModel where T : TaskHubSpotModel, new()
    {

        [DataMember(Name = "paging")]
        public PagingModel Paging { get; set; }

        /// <summary>
        /// Gets or sets the <typeparamref name="T"/>.
        /// </summary>
        /// <value>
        /// The <typeparamref name="T"/>.
        /// </value>
        [DataMember(Name = "results")]
        public IList<T> Results { get; set; } = new List<T>();

        public string RouteBasePath => "/crm/v3/objects/tasks";

        public bool IsNameValue => false;

        public virtual void ToHubSpotDataEntity(ref dynamic converted)
        {
        }

        public virtual void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }
    }
}