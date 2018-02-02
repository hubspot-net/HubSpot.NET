using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Api.Engagement.Dto;
using HubSpot.NET.Api.Files.Dto;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class Examples
    {
        static void Main(string[] args)
        {
            var api = new HubSpotApi("MY API KEY");

            // Create a contact
            var contact = api.Contact.Create(new ContactHubSpotModel()
            {
                Email = "john@squaredup.com",
                FirstName = "John",
                LastName = "Smith",
                Phone = "00000 000000",
                Company = "Squared Up Ltd."
            });

            // Update a contact
            contact.Phone = "111111 11111";
            api.Contact.Update(contact);

            // Upload a file
            var file = new FileHubSpotModel()
            {
                File = File.ReadAllBytes("MY FILE PATH"),
                Name = "MyFile.png",
                Hidden = true, //set to true for engagements
            };

            var uploaded = api.File.Upload(file);
            var fileId = uploaded.Objects.First().Id;

            // Create a NOTE engagement with a file attachment
            api.Engagement.Create(new EngagementHubSpotModel()
            {
                Engagement = new EngagementHubSpotEngagementModel()
                {
                    Type = "NOTE" //used for file attachments
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

            // Delete a contact
            api.Contact.Delete(contact.Id.Value);

        }
    }
}
