namespace HubSpot.NET.Api.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;    
    using HubSpot.NET.Api.Contact.Dto;
    using HubSpot.NET.Core;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;

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
        /// <returns>The contact entity or null if the contact does not exist.</returns>
        public ContactHubSpotModel GetById(long contactId, bool IncludeHistory = true)
        {
            try
            {
                if (IncludeHistory)
                {
                    return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact","vid", contactId.ToString(),"profile"));
                }
                else
                {
                    return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "vid", contactId.ToString(), "profile?propertyMode=value_only"));
                }
            }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        public ContactHubSpotModel GetById(long Id) => GetById(Id, true);

        /// <summary>
        /// Gets a contact by their email address
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity or null if the contact does not exist.</returns>
        public ContactHubSpotModel GetByEmail(string email, bool IncludeHistory = true)
        {
            try
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
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
            }
        }

        /// <summary>
        /// Gets a contact by their user token
        /// </summary>
        /// <param name="userToken">User token to search for from hubspotutk cookie</param>
        /// <typeparam name="T">Implementation of ContactHubSpotModel</typeparam>
        /// <returns>The contact entity or null if the contact does not exist.</returns>
        public ContactHubSpotModel GetByUserToken(string userToken, bool IncludeHistory = true)
        {
            try
            {
                if (IncludeHistory)
                {
                    return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "utk", userToken, "profile"));
                }
                else
                {
                    return _client.Execute<ContactHubSpotModel>(GetRoute<ContactHubSpotModel>("contact", "utk", userToken, "profile?propertyMode=value_only"));
                }
            }
            catch (HubSpotException exception)
            {
                if (exception.ReturnedError.StatusCode == HttpStatusCode.NotFound)
                    return null;
                throw;
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

            string path = GetRoute<ContactHubSpotModel>("lists", "all", "contacts", "all");
                 
            path += $"{QueryParams.COUNT}={opts.Limit}";

            if (opts.PropertiesToInclude.Any())            
                path += $"{QueryParams.PROPERTY}={opts.PropertiesToInclude}";            

            if (opts.Offset.HasValue)            
                path = path += $"{QueryParams.VID_OFFSET}={opts.Offset}";

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

            string path = GetRoute<ContactHubSpotModel>("lists", "recently_updated", "contacts", "recent");

            path += $"?{QueryParams.COUNT}={opts.Limit}";

            if (opts.PropertiesToInclude.Any())            
                path += $"{QueryParams.PROPERTY}={opts.PropertiesToInclude}";            

            if (opts.Offset.HasValue)            
                path += $"{QueryParams.VID_OFFSET}={opts.Offset}";            

            if (!string.IsNullOrEmpty(opts.TimeOffset))            
                path += $"{QueryParams.TIME_OFFSET}={opts.TimeOffset}";            
            
            path += $"{QueryParams.PROPERTY_MODE}={opts.PropertyMode}" +
                $"&{QueryParams.FORM_SUBMISSION_MODE}={opts.FormSubmissionMode}" +
                $"&{QueryParams.SHOW_LIST_MEMBERSHIPS}={opts.ShowListMemberships}";
            
            return _client.Execute<ContactListHubSpotModel<ContactHubSpotModel>, ListRecentRequestOptions>(path, opts);
        }

        public ContactSearchHubSpotModel<ContactHubSpotModel> Search(ContactSearchRequestOptions opts = null)
        {
            opts = opts ?? new ContactSearchRequestOptions();

            string path = GetRoute<ContactHubSpotModel>("search", "query");
                
            path += $"q={opts.Query}&{QueryParams.COUNT}={opts.Limit}";

            if (opts.PropertiesToInclude.Any())            
                path += $"{QueryParams.PROPERTY}={opts.PropertiesToInclude}";            


            if (opts.Offset.HasValue)            
                path = path += $"{QueryParams.OFFSET}={opts.Offset}";            

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

            string path = GetRoute<ContactHubSpotModel>("lists", "all", "contacts", "recent");
                 
            path += $"{QueryParams.COUNT}={opts.Limit}";

            if (opts.PropertiesToInclude.Any())            
                path += $"{QueryParams.PROPERTY}={opts.PropertiesToInclude}";

            if (opts.Offset.HasValue)            
                path = path += $"{QueryParams.VID_OFFSET}={opts.Offset}";

            if (!string.IsNullOrEmpty(opts.TimeOffset))            
                path = path += $"{QueryParams.TIME_OFFSET}={opts.TimeOffset}";
            
            path += $"{QueryParams.PROPERTY_MODE}={opts.PropertyMode}"
                    + $"{QueryParams.FORM_SUBMISSION_MODE}={opts.FormSubmissionMode}"
                    + $"{QueryParams.SHOW_LIST_MEMBERSHIPS}={opts.ShowListMemberships}";   
            
            return _client.Execute<ContactListHubSpotModel<ContactHubSpotModel>, ListRecentRequestOptions>(path, opts);
        }

        /// <summary>
        /// Get all contacts in a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public ContactListHubSpotModel<ContactHubSpotModel> GetList(long listId, ListRequestOptions opts = null)
        {
            if (opts == null)
            {
                opts = new ListRequestOptions();
            }

            string path = GetRoute<ContactHubSpotModel>("lists", $"{listId}", "contacts", "all");
            path += $"{QueryParams.COUNT}={opts.Limit}";


            if (opts.PropertiesToInclude.Any())
            {
                path += $"{QueryParams.PROPERTY}={opts.PropertiesToInclude}";
            }

            if (opts.Offset.HasValue)
            {
                path = path += $"{QueryParams.VID_OFFSET}={opts.Offset}";
            }

            var data = _client.Execute<ContactListHubSpotModel<ContactHubSpotModel>>(path);

            return data;
        }
    }
}
