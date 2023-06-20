namespace HubSpot.NET.Api.Note
{
    using System;
    using System.Net;
    using HubSpot.NET.Api.Note.Dto;
    using HubSpot.NET.Core;
    using HubSpot.NET.Core.Extensions;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

    public class HubSpotNoteApi : IHubSpotNoteApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotNoteApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a note
        /// </summary>
        /// <param name="entity">The note to create</param>
        /// <returns>The created note (with ID set)</returns>
        public NoteHubSpotResponseModel Create(NoteHubSpotRequestModel entity)
        {
            var path = $"{entity.RouteBasePath}";
            var data = _client.Execute<NoteHubSpotResponseModel>(path, entity, Method.POST, false);
            return data;
        }
    }
}
