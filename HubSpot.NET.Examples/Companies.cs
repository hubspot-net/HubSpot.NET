using HubSpot.NET.Api.Company;
using HubSpot.NET.Api.Company.Dto;
using HubSpot.NET.Core;
using System;
using System.Threading.Tasks;

namespace HubSpot.NET.Examples
{
    public class Companies
    {
        public static async Task Example(HubSpotApi api)
        {
            try
            {
                await Tests(api);
                Console.WriteLine("Companies example completed successfully.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Companies tests failed!");
                Console.WriteLine(ex.ToString());
            }  
        }

        private static async Task Tests(HubSpotApi api)
        {
            /**
            * Create a company
            */
            var company = await api.Company.CreateAsync(new CompanyHubSpotModel()
            {
                Domain = "squaredup.com",
                Name = "Squared Up"
            });

            /**
             * Update a company's property
             */
            company.Description = "Data Visualization for Enterprise IT";
            await api.Company.UpdateAsync(company);

            /**
             * Get all companies with domain name "squaredup.com"
             */
            var companies = await api.Company.GetByDomainAsync("squaredup.com", new CompanySearchByDomain()
            {
                Limit = 10
            });

            /**
             * Delete a contact
             */
            await api.Company.DeleteAsync(company.Id.Value);
        }
    }
}
