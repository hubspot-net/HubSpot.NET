using System.Collections.Generic;
using HubSpot.NET.Api.ContactList;
using HubSpot.NET.Api.ContactList.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotContactListApi
    {
        ListContactListModel GetContactLists(ListOptions opts = null);

        ListContactListModel GetStaticContactLists(ListOptions opts = null);

        ContactListModel GetContactListById(long contactListId);

        ContactListUpdateResponseModel AddContactsToList(long listId, IEnumerable<long> contactIds);

        ContactListUpdateResponseModel RemoveContactsFromList(long listId, IEnumerable<long> contactIds);

        void DeleteContactList(long listId);

        ContactListModel CreateStaticContactList(string contactListName);
    }
}