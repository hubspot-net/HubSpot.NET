using System.Collections.Generic;
using HubSpot.NET.Api.ContactList.Dto;
using HubSpot.NET.Core.Extensions;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.ContactList
{
    /// <summary>
    /// The hub spot contact list api class
    /// </summary>
    /// <seealso cref="IHubSpotContactListApi"/>
    public class HubSpotContactListApi : IHubSpotContactListApi
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly IHubSpotClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubSpotContactListApi"/> class
        /// </summary>
        /// <param name="client">The client</param>
        public HubSpotContactListApi(IHubSpotClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the contact lists using the specified opts
        /// </summary>
        /// <param name="opts">The opts</param>
        /// <returns>The data</returns>
        public ListContactListModel GetContactLists(ListOptions opts = null)
        {
            if (opts == null)
            {
                opts = new ListOptions();
            }
            
            var path = $"{new ListContactListModel().RouteBasePath}".SetQueryParam("count", opts.Limit);
            if (opts.Offset.HasValue)
            {
                path = path.SetQueryParam("offset", opts.Offset);
            }
            
            var data = _client.ExecuteList<ListContactListModel>(path, convertToPropertiesSchema: false);

            return data;
        }

        /// <summary>
        /// Gets the static contact lists using the specified opts
        /// </summary>
        /// <param name="opts">The opts</param>
        /// <returns>The data</returns>
        public ListContactListModel GetStaticContactLists(ListOptions opts = null)
        {
            if (opts == null)
            {
                opts = new ListOptions();
            }
            
            var path = $"{new ListContactListModel().RouteBasePath}/static".SetQueryParam("count", opts.Limit);
            if (opts.Offset.HasValue)
            {
                path = path.SetQueryParam("offset", opts.Offset);
            }
            
            var data = _client.ExecuteList<ListContactListModel>(path, convertToPropertiesSchema: false);

            return data;
        }

        /// <summary>
        /// Gets the contact list by id using the specified contact list id
        /// </summary>
        /// <param name="contactListId">The contact list id</param>
        /// <returns>The data</returns>
        public ContactListModel GetContactListById(long contactListId)
        {
            var path = $"{new ContactListModel().RouteBasePath}/{contactListId}";

            var data = _client.ExecuteList<ContactListModel>(path, convertToPropertiesSchema: false);

            return data;
        }

        /// <summary>
        /// Adds the contacts to list using the specified list id
        /// </summary>
        /// <param name="listId">The list id</param>
        /// <param name="contactIds">The contact ids</param>
        /// <returns>The data</returns>
        public ContactListUpdateResponseModel AddContactsToList(long listId, IEnumerable<long> contactIds)
        {
            var model = new ContactListUpdateModel();
            var path = $"{model.RouteBasePath}/{listId}/add";
            model.ContactIds.AddRange(contactIds);
            var data = _client.Execute<ContactListUpdateResponseModel>(path, model, Method.POST, convertToPropertiesSchema: false);

            return data;
        }

        /// <summary>
        /// Removes the contacts from list using the specified list id
        /// </summary>
        /// <param name="listId">The list id</param>
        /// <param name="contactIds">The contact ids</param>
        /// <returns>The data</returns>
        public ContactListUpdateResponseModel RemoveContactsFromList(long listId, IEnumerable<long> contactIds)
        {
            var model = new ContactListUpdateModel();
            var path = $"{model.RouteBasePath}/{listId}/remove";
            model.ContactIds.AddRange(contactIds);
            var data = _client.Execute<ContactListUpdateResponseModel>(path, model, Method.POST, convertToPropertiesSchema: false);

            return data;
        }

        /// <summary>
        /// Deletes the contact list using the specified list id
        /// </summary>
        /// <param name="listId">The list id</param>
        public void DeleteContactList(long listId)
        {
            var path = $"{new ContactListModel().RouteBasePath}/{listId}";
            _client.Execute(path, method: Method.DELETE, convertToPropertiesSchema: true);
        }

        /// <summary>
        /// Creates the static contact list using the specified contact list name
        /// </summary>
        /// <param name="contactListName">The contact list name</param>
        /// <returns>The data</returns>
        public ContactListModel CreateStaticContactList(string contactListName )
        {
            var model = new ContactListModel()
            {
                Name = contactListName,
                Dynamic = false
            };
            var path = $"{model.RouteBasePath}";
            var data = _client.Execute<ContactListModel>(path, model, Method.POST, convertToPropertiesSchema: false);
            return data;
        }
    }
}