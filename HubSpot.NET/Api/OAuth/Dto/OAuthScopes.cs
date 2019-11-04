namespace HubSpot.NET.Api.OAuth.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    public enum OAuthScopes
    {
        Automation = 0, // automation
        BusinessIntelligence = 1, // business-intelligence
        Contacts = 2, // contacts
        Content = 3, // content
        ECommerce = 4, // e-commerce
        Files = 5, // files
        Forms = 6, // forms
        HubDb = 7, //hubdb
        IntegrationSync = 8, // integration-sync
        Reports = 9, // reports
        Social = 10, // social
        Tickets = 11, // tickets
        Timeline = 12, // timeline
        TransactionalEmail = 13 // transactional-email

    }
}
