using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Contact
{
    public class HubSpotContactApi : IHubSpotContactApi
    {
        private readonly IHubSpotClient _client;

        public HubSpotContactApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a contact entity
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Create<T>(T entity) where T : ContactHubSpotModel, new()
        {
            var path = $"{entity.RouteBasePath}/contact";
            return _client.Execute<T>(path, entity, Method.POST);
        }

        /// <summary>
        /// Gets a single contact by ID from hubspot
        /// </summary>
        /// <param name="contactId">ID of the contact</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public T GetById<T>(long contactId) where T : ContactHubSpotModel, new()
        {
            var path = $"{new T().RouteBasePath}/contact/vid/{contactId}/profile";
            var data = _client.Execute<T>(path, Method.GET);
            return data;
        }

        /// <summary>
        /// Gets a contact by their email address
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public T GetByEmail<T>(string email) where T : ContactHubSpotModel, new()
        {
            var path =  $"{new T().RouteBasePath}/contact/email/{email}/profile";
            var data = _client.Execute<T>(path, Method.GET);
            return data;
        }

        /// <summary>
        /// List all available contacts 
        /// </summary>
        /// <param name="properties">List of properties to fetch for each contact</param>
        /// <param name="opts">Request options - used for pagination etc.</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>A list of contacts</returns>
        public ContactListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T : ContactHubSpotModel, new()
        {
            if (opts == null)
            {
                opts = new ListRequestOptions();
            }

            var path = $"{new ContactHubSpotModel().RouteBasePath}/lists/all/contacts/all"
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())
            {
                path.SetQueryParam("property", opts.PropertiesToInclude);
            }

            if (opts.Offset.HasValue)
            {
                path = path.SetQueryParam("vidOffset", opts.Offset);
            }

            var data = _client.ExecuteList<ContactListHubSpotModel<T>>(path, opts);

            return data;
        }

        /// <summary>
        /// Updates a given contact
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="contact">The contact entity</param>
        public void Update<T>(T contact) where T : ContactHubSpotModel, new()
        {
            if (contact.Id < 1)
            {
                throw new ArgumentException("Contact entity must have an id set!");
            }

            var path = $"{contact.RouteBasePath}/contact/vid/{contact.Id}/profile";

            _client.Execute(path, contact, Method.POST);
        }
        
        /// <summary>
        /// Deletes a given contact
        /// </summary>
        /// <param name="contactId">The ID of the contact</param>
        public void Delete(long contactId)
        {
            var path = $"{new ContactHubSpotModel().RouteBasePath}/contact/vid/{contactId}";

            _client.Execute(path, method: Method.DELETE);
        }

        /// <summary>
        /// Update or create a set of contacts, this is the preferred method when creating/updating in bulk.
        /// Best performance is with a maximum of 250 contacts.
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="contacts">The set of contacts to update/create</param>
        public void Batch<T>(List<T> contacts) where T : ContactHubSpotModel, new()
        {
            var path =  $"{new T().RouteBasePath}/contact/batch";

            _client.ExecuteBatch(path, contacts.Select(c => (object) c).ToList(), Method.POST);
        }

        /// <summary>
        /// Get recently updated (or created) contacts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opts">Request options</param>
        /// <returns></returns>
        public ContactListHubSpotModel<T> RecentlyUpdated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel, new()
        {
            if (opts == null)
            {
                opts = new ListRecentRequestOptions();
            }

            var path = $"{new ContactHubSpotModel().RouteBasePath}/lists/recently_updated/contacts/recent"
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())
            {
                path.SetQueryParam("property", opts.PropertiesToInclude);
            }

            if (opts.Offset.HasValue)
            {
                path = path.SetQueryParam("vidOffset", opts.Offset);
            }

            if (!string.IsNullOrEmpty(opts.TimeOffset))
            {
                path = path.SetQueryParam("timeOffset", opts.TimeOffset);
            }
            
            path = path.SetQueryParam("propertyMode", opts.PropertyMode);
            
            path = path.SetQueryParam("formSubmissionMode", opts.FormSubmissionMode);
            
            path = path.SetQueryParam("showListMemberships", opts.ShowListMemberships);
            
            var data = _client.ExecuteList<ContactListHubSpotModel<T>>(path, opts);

            return data;
        }

        /// <summary>
        /// Get a list of recently created contacts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opts">Request options</param>
        /// <returns></returns>
        public ContactListHubSpotModel<T> RecentlyCreated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel, new()
        {
            if (opts == null)
            {
                opts = new ListRecentRequestOptions();
            }

            var path = $"{new ContactHubSpotModel().RouteBasePath}/lists/all/contacts/recent"
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())
            {
                path.SetQueryParam("property", opts.PropertiesToInclude);
            }

            if (opts.Offset.HasValue)
            {
                path = path.SetQueryParam("vidOffset", opts.Offset);
            }

            if (!string.IsNullOrEmpty(opts.TimeOffset))
            {
                path = path.SetQueryParam("timeOffset", opts.TimeOffset);
            }
            
            path = path.SetQueryParam("propertyMode", opts.PropertyMode);
            
            path = path.SetQueryParam("formSubmissionMode", opts.FormSubmissionMode);
            
            path = path.SetQueryParam("showListMemberships", opts.ShowListMemberships);
            
            var data = _client.ExecuteList<ContactListHubSpotModel<T>>(path, opts);

            return data;
        }
    }
}