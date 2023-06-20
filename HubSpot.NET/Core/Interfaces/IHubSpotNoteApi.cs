using HubSpot.NET.Api.Note.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotNoteApi
    {
        NoteHubSpotResponseModel Create(NoteHubSpotRequestModel entity);
    }
}