using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Contact
{
    public class HubSpotContactApi : ApiRoutable, IHubSpotContactApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/contacts/v1";

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
        public T Create<T>(T entity) where T : ContactHubSpotModel
            => _client.Execute($"{GetRoute<T>()}/contact", entity, Method.POST);

        /// <summary>
        /// Creates or Updates a contact entity based on the Entity Email
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        public T CreateOrUpdate<T>(T entity) where T : ContactHubSpotModel
            => _client.Execute($"{GetRoute<T>()}/contact/createOrUpdate/email/{entity.Email}/", entity, Method.POST);

        /// <summary>
        /// Gets a single contact by ID from hubspot
        /// </summary>
        /// <param name="contactId">ID of the contact</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public T GetById<T>(long contactId) where T : ContactHubSpotModel 
            => _client.Execute<T>($"{GetRoute<T>()}/contact/vid/{contactId}/profile");

        /// <summary>
        /// Gets a contact by their email address
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public T GetByEmail<T>(string email) where T : ContactHubSpotModel
            => _client.Execute<T>($"{GetRoute<T>()}/contact/email/{email}/profile");

        /// <summary>
        /// Gets a contact by their user token
        /// </summary>
        /// <param name="userToken">User token to search for from hubspotutk cookie</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public T GetByUserToken<T>(string userToken) where T : ContactHubSpotModel
            => _client.Execute<T>($"{GetRoute<T>()}/contact/utk/{userToken}/profile");

        /// <summary>
        /// List all available contacts 
        /// </summary>
        /// <param name="properties">List of properties to fetch for each contact</param>
        /// <param name="opts">Request options - used for pagination etc.</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>A list of contacts</returns>
        public ContactListHubSpotModel<T> List<T>(ListRequestOptions opts = null) where T : ContactHubSpotModel
        {
            opts = opts ?? new ListRequestOptions();            

            var path = $"{GetRoute<T>()}/lists/all/contacts/all"
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())            
                path.SetQueryParam("property", opts.PropertiesToInclude);            

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("vidOffset", opts.Offset);            

            return _client.ExecuteList<ContactListHubSpotModel<T>>(path, opts);           
        }

        /// <summary>
        /// Updates a given contact
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="contact">The contact entity</param>
        public void Update<T>(T contact) where T : ContactHubSpotModel
        {
            if (contact.Id < 1)            
                throw new ArgumentException("Contact entity must have an id set!");                       

            _client.Execute($"{GetRoute<T>()}/contact/vid/{contact.Id}/profile", contact, Method.POST);
        }

        /// <summary>
        /// Deletes a given contact
        /// </summary>
        /// <param name="contactId">The ID of the contact</param>
        public void Delete(long contactId) 
            => _client.Execute($"{GetRoute<ContactHubSpotModel>()}/contact/vid/{contactId}", method: Method.DELETE);

        /// <summary>
        /// Update or create a set of contacts, this is the preferred method when creating/updating in bulk.
        /// Best performance is with a maximum of 250 contacts.
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="contacts">The set of contacts to update/create</param>
        public void Batch<T>(List<T> contacts) where T : ContactHubSpotModel
            => _client.ExecuteBatch($"{GetRoute<T>()}/contact/batch", contacts.Select(c => (object) c).ToList(), Method.POST);        

        /// <summary>
        /// Get recently updated (or created) contacts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opts">Request options</param>
        /// <returns></returns>
        public ContactListHubSpotModel<T> RecentlyUpdated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel
        {
            opts = opts ?? new ListRecentRequestOptions();            

            Url path = $"{GetRoute<T>()}/lists/recently_updated/contacts/recent"
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())            
                path.SetQueryParam("property", opts.PropertiesToInclude);            

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("vidOffset", opts.Offset);            

            if (!string.IsNullOrEmpty(opts.TimeOffset))            
                path = path.SetQueryParam("timeOffset", opts.TimeOffset);            
            
            path = path.SetQueryParam("propertyMode", opts.PropertyMode)
                        .SetQueryParam("formSubmissionMode", opts.FormSubmissionMode)
                        .SetQueryParam("showListMemberships", opts.ShowListMemberships);
            
            return _client.ExecuteList<ContactListHubSpotModel<T>>(path, opts);            
        }

        public ContactSearchHubSpotModel<T> Search<T>(ContactSearchRequestOptions opts = null) where T : ContactHubSpotModel
        {
            opts = opts ?? new ContactSearchRequestOptions();

            Url path = $"{GetRoute<T>()}/search/query"
                .SetQueryParam("q", opts.Query)
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())            
                path.SetQueryParam("property", opts.PropertiesToInclude);            

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("offset", opts.Offset);            

            return _client.ExecuteList<ContactSearchHubSpotModel<T>>(path, opts);            
        }

        /// <summary>
        /// Get a list of recently created contacts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opts">Request options</param>
        /// <returns></returns>
        public ContactListHubSpotModel<T> RecentlyCreated<T>(ListRecentRequestOptions opts = null) where T : ContactHubSpotModel
        {            
            opts = opts ?? new ListRecentRequestOptions();

            Url path = $"{GetRoute<T>()}/lists/all/contacts/recent"
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())            
                path.SetQueryParam("property", opts.PropertiesToInclude);

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("vidOffset", opts.Offset);

            if (!string.IsNullOrEmpty(opts.TimeOffset))            
                path = path.SetQueryParam("timeOffset", opts.TimeOffset);
            
            path = path.SetQueryParam("propertyMode", opts.PropertyMode)
                        .SetQueryParam("formSubmissionMode", opts.FormSubmissionMode)
                        .SetQueryParam("showListMemberships", opts.ShowListMemberships);   
            
            return _client.ExecuteList<ContactListHubSpotModel<T>>(path, opts);
        }
    }
}
