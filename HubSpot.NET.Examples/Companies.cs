using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class Companies
    {
        public static void Example()
        {
            /**
             * Initialize the API with your API Key
             * You can find or generate this under Integrations -> HubSpot API key
             */
            var api = new HubSpotApi("YOUR-API-KEY-HERE");

            /**
             * Create a company
             */
            var company = api.Company.Create(new CompanyHubSpotModel()
            {
                Domain = "squaredup.com",
                Name = "Squared Up"
            });

            /**
             * Update a company's property
             */
            company.Description = "Data Visualization for Enterprise IT";
            api.Company.Update(company);
           
            /**
             * Delete a contact
             */
            api.Company.Delete(company.Id.Value);

            /**
             * Get all companies with domain name "squaredup.com"
             */
            var companies = api.Company.GetByDomain<CompanySearchResultModel>("squaredup.com", new CompanySearchByDomain()
            {
                Limit = 10
            });
        }
    }
}
