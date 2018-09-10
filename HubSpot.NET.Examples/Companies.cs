using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Core;

namespace HubSpot.NET.Examples
{
    public class Companies
    {
        public static void Example(HubSpotApi api)
        {
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
            var companies = api.Company.GetByDomain("squaredup.com", new CompanySearchByDomain()
            {
                Limit = 10
            });
        }
    }
}
