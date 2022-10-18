using System;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Task.Dto
{
	/// <summary>
	/// Models a Task entity within HubSpot. Default properties are included here
	/// with the intention that you'd extend this class with properties specific to 
	/// your HubSpot account.
	/// </summary>
	[DataContract]
	public class TaskHubSpotModel : IHubSpotModel
	{
		/// <summary>
		/// Contacts unique Id in HubSpot
		/// </summary>
		[DataMember(Name = "taskId")]
		[IgnoreDataMember]
		public long? Id { get; set; }

		[DataMember(Name = "hs_task_subject")]
		public string Subject { get; set; }

		[DataMember(Name = "hubspot_owner_id")]
		public long? OwnerId { get; set; }

		[DataMember(Name = "hs_task_body")]
		public string Notes { get; set; }

		[DataMember(Name = "hs_task_status")]
		public string Status { get; set; }

		[DataMember(Name = "hs_task_priority")]
		public string Priority { get; set; }

		[DataMember(Name = "hs_task_type")]
		public string Type { get; set; }

		[DataMember(Name = "hs_timestamp")]
		public DateTime DueDate { get; set; }

		public string RouteBasePath => "/crm/v3/objects/tasks";
		public bool IsNameValue => true;

		public void FromHubSpotDataEntity(dynamic hubspotData)
		{
		}

		public void ToHubSpotDataEntity(ref dynamic dataEntity)
		{
		}
	}
}