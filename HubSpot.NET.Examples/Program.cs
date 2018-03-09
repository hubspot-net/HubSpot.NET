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
            Deals.Example();

            Companies.Example();

            Contacts.Example();

            CompanyProperties.Example();

            EmailSubscriptions.Example();
        }
    }
}
