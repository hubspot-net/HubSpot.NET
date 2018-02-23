using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Api.Properties.Dto;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class CompanyProperties
    {
        public static void Example()
        {
            /**
             * Initialize the API with your API Key
             * You can find or generate this under Integrations -> HubSpot API key
             */
            var api = new HubSpotApi("YOUR-API-KEY-HERE");

            /**
             * Get all company properties
             */
            var properties = api.CompanyProperties.GetAll();

            /**
             * Create a new company property
             * See https://developers.hubspot.com/docs/methods/companies/create_company_property for information of type/field type etc.
             */
            var newProp = api.CompanyProperties.Create(new CompanyPropertyHubSpotModel()
            {
                Name = "exampleproperty", //should be lowercase
                Label = "Example Property",
                Description = "This is an example property",
                GroupName = "companyinformation",
                Type = "string",
                FieldType = "text"
            });
        }
    }
}
