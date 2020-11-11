using HubSpot.NET.Api.Properties.Dto;
using HubSpot.NET.Core;
using System.Threading.Tasks;

namespace HubSpot.NET.Examples
{
    public class CompanyProperties
    {
        public static async Task Example(HubSpotApi api)
        {
           /**
             * Get all company properties
             */
            var properties = api.CompanyProperties.GetAll();

            /**
             * Create a new company property
             * See https://developers.hubspot.com/docs/methods/companies/create_company_property for information of type/field type etc.
             */
            var newProp = await api.CompanyProperties.CreateAsync(new CompanyPropertyHubSpotModel()
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
