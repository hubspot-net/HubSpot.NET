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
        public ContactHubSpotModel Create(ContactHubSpotModel entity)
        {
            CreateOrUpdateContactTransportModel transport = new CreateOrUpdateContactTransportModel(entity);
            string path = GetRoute<ContactHubSpotModel>("contact");

            return _client.Execute<ContactHubSpotModel, CreateOrUpdateContactTransportModel>(path, transport, Method.POST);
        }

        /// <summary>
        /// Creates or Updates a contact entity based on the Entity Email
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        public ContactHubSpotModel CreateOrUpdate(ContactHubSpotModel entity)
        {
            CreateOrUpdateContactTransportModel transport = new CreateOrUpdateContactTransportModel(entity);
            string path = GetRoute<ContactHubSpotModel>("contact", "createOrUpdate", "email", entity.Email);

            return _client.Execute<ContactHubSpotModel, CreateOrUpdateContactTransportModel>(path, transport, Method.POST);
        }

        /// <summary>
        /// Creates or updates a contact entity based on the entity's current email.
        /// </summary>
        /// <param name="originalEmail">The email the server knows, assuming the entity email may be different.</param>
        /// <param name="entity">The contact entity to update on the server.</param>
        /// <returns>The updated entity (with ID set)</returns>
        public ContactHubSpotModel CreateOrUpdate(string originalEmail, ContactHubSpotModel entity)
        {
            CreateOrUpdateContactTransportModel transport = new CreateOrUpdateContactTransportModel(entity);
            string path = GetRoute<ContactHubSpotModel>("contact", "createOrUpdate", "email", originalEmail);

            return _client.Execute<ContactHubSpotModel, CreateOrUpdateContactTransportModel>(path, transport, Method.POST);
        }

        /// <summary>
        /// Gets a single contact by ID from hubspot
        /// </summary>
        /// <param name="contactId">ID of the contact</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public ContactHubSpotModel GetById(long contactId, bool IncludeHistory = true)
        {
            if(IncludeHistory)
            {
                return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact","vid", contactId.ToString(),"profile"));
            }
            else
            {
                return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "vid", contactId.ToString(), "profile?propertyMode=value_only"));
            }
        }

        public ContactHubSpotModel GetById(long Id) => GetById(Id, true);

        /// <summary>
        /// Gets a contact by their email address
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public ContactHubSpotModel GetByEmail(string email, bool IncludeHistory = true)
        {
            if (IncludeHistory)
            {
                return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "email", email, "profile"));
            }
            else
            {
                return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "email", email, "profile?propertyMode=value_only"));
            }
        }

        /// <summary>
        /// Gets a contact by their user token
        /// </summary>
        /// <param name="userToken">User token to search for from hubspotutk cookie</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity</returns>
        public ContactHubSpotModel GetByUserToken(string userToken, bool IncludeHistory = true)
        {
            if(IncludeHistory)
            {
                return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "utk", userToken, "profile"));
            }
            else
            {
                return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "utk", userToken, "profile?propertyMode=value_only"));
            }
        }

        /// <summary>
        /// List all available contacts 
        /// </summary>
        /// <param name="properties">List of properties to fetch for each contact</param>
        /// <param name="opts">Request options - used for pagination etc.</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>A list of contacts</returns>
        public ContactListHubSpotModel<ContactHubSpotModel> List(ListRequestOptions opts = null)
        {
            opts = opts ?? new ListRequestOptions();            

            var path = GetRoute<ContactHubSpotModel>("lists", "all", "contacts","all")
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())            
                path.SetQueryParam("property", opts.PropertiesToInclude);            

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("vidOffset", opts.Offset);            

            return _client.Execute<ContactListHubSpotModel<ContactHubSpotModel>, ListRequestOptions>(path, opts);           
        }

        /// <summary>
        /// Updates a given contact
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="contact">The contact entity</param>
        public ContactHubSpotModel Update(ContactHubSpotModel contact)
        {
            if (contact.Id < 1)            
                throw new ArgumentException("Contact entity must have an id set!");                       

            return _client.Execute<ContactHubSpotModel, ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact","vid", contact.Id.ToString(), "profile"), contact, Method.POST);
        }

        /// <summary>
        /// Deletes a given contact
        /// </summary>
        /// <param name="contactId">The ID of the contact</param>
        public void Delete(long contactId) 
            => _client.ExecuteOnly(GetRoute<ContactHubSpotModel>("contact", "vid", contactId.ToString()), method: Method.DELETE);

        /// <summary>
        /// Update or create a set of contacts, this is the preferred method when creating/updating in bulk.
        /// Best performance is with a maximum of 250 contacts.
        /// </summary>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <param name="contacts">The set of contacts to update/create</param>
        public void Batch(List<ContactHubSpotModel> contacts)
            => _client.ExecuteBatch(GetRoute<ContactHubSpotModel>("contact", "batch"), contacts.Select(c => (object) c).ToList(), Method.POST);        

        /// <summary>
        /// Get recently updated (or created) contacts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opts">Request options</param>
        /// <returns></returns>
        public ContactListHubSpotModel<ContactHubSpotModel> RecentlyUpdated(ListRecentRequestOptions opts = null)
        {
            opts = opts ?? new ListRecentRequestOptions();            

            Url path = GetRoute<ContactHubSpotModel>("lists", "recently_updated","contacts","recent")
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
            
            return _client.Execute<ContactListHubSpotModel<ContactHubSpotModel>, ListRecentRequestOptions>(path, opts);            
        }

        public ContactSearchHubSpotModel<ContactHubSpotModel> Search(ContactSearchRequestOptions opts = null)
        {
            opts = opts ?? new ContactSearchRequestOptions();

            Url path = GetRoute<ContactHubSpotModel>("search","query")
                .SetQueryParam("q", opts.Query)
                .SetQueryParam("count", opts.Limit);

            if (opts.PropertiesToInclude.Any())            
                path.SetQueryParam("property", opts.PropertiesToInclude);            

            if (opts.Offset.HasValue)            
                path = path.SetQueryParam("offset", opts.Offset);            

            return _client.Execute<ContactSearchHubSpotModel<ContactHubSpotModel>, ContactSearchRequestOptions>(path, opts);            
        }

        /// <summary>
        /// Get a list of recently created contacts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opts">Request options</param>
        /// <returns></returns>
        public ContactListHubSpotModel<ContactHubSpotModel> RecentlyCreated(ListRecentRequestOptions opts = null)
        {            
            opts = opts ?? new ListRecentRequestOptions();

            Url path = GetRoute<ContactHubSpotModel>("lists","all","contacts","recent")
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
            
            return _client.Execute<ContactListHubSpotModel<ContactHubSpotModel>, ListRecentRequestOptions>(path, opts);
        }
    }
}
