using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Api.Engagement.Dto;
using HubSpot.NET.Api.Files.Dto;
using HubSpot.NET.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HubSpot.NET.Examples
{
    public class Contacts
    {
        public static void Example()
        {
            /**
             * Initialize the API with your API Key
             * You can find or generate this under Integrations -> HubSpot API key
             */
            var api = new HubSpotApi("YOUR-API-KEY-HERE");

            /**
             * Create a contact
             */
            var contact = api.Contact.Create(new ContactHubSpotModel()
            {
                Email = "john@squaredup.com",
                FirstName = "John",
                LastName = "Smith",
                Phone = "00000 000000",
                Company = "Squared Up Ltd."
            });

            /**
             * Update a contact's property
             */
            contact.Phone = "111111 11111";
            api.Contact.Update(contact);

            /**
             * Upload a file (to attach to a contact)
             */
            var file = new FileHubSpotModel()
            {
                File = File.ReadAllBytes("MY FILE PATH"),
                Name = "File.png",
                Hidden = true, //set to true for engagements
            };

            var uploaded = api.File.Upload<FileHubSpotModel>(file);
            var fileId = uploaded.Objects.First().Id;

            /**
             * Add a Note engagement to a contact with a file attachment
             */
            api.Engagement.Create(new EngagementHubSpotModel()
            {
                Engagement = new EngagementHubSpotEngagementModel()
                {
                    Type = "NOTE" //used for file attachments
                },
                Metadata = new
                {
                    body = "This is an example note"
                },
                Associations = new EngagementHubSpotAssociationsModel()
                {
                    ContactIds = new List<long>() { contact.Id.Value } //use the ID of the created contact from above
                },
                Attachments = new List<EngagementHubSpotAttachmentModel>() {
                    new EngagementHubSpotAttachmentModel()
                    {
                        Id = fileId
                    }
                }
            });

            /**
             * Delete a contact
             */
            api.Contact.Delete(contact.Id.Value);

            /**
             * Get all contacts with specific properties
             * By default only a few properties are returned
             */
            var contacts = api.Contact.List<ContactHubSpotModel>(
                new ListRequestOptions { PropertiesToInclude = new List<string> { "firstname", "lastname", "email" } });

            /**
             * Get the most recently updated contacts, limited to 10
             */
            var recentlyUpdated = api.Contact.RecentlyUpdated<ContactHubSpotModel>(new ListRecentRequestOptions()
            {
                Limit = 10
            });

            /**
             * Get the most recently created contacts, limited to 10
             */
            var recentlyCreated = api.Contact.RecentlyCreated<ContactHubSpotModel>(new ListRecentRequestOptions()
            {
                Limit = 10
            });
        }
    }
}
