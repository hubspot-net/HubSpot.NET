using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Core;
using System.Collections.Generic;

namespace HubSpot.NET.Examples
{
    public class Deals
    {
        public static void Example()
        {
            /**
             * Initialize the API with your API Key
             * You can find or generate this under Integrations -> HubSpot API key
             */
            var api = new HubSpotApi("YOUR API KEY HERE");

            /**
             * Create a deal
             */
            var deal = api.Deal.Create(new DealHubSpotModel()
            {
                Amount = 10000,
                Name = "New Deal #1"
            });

            /**
             * Delete a deal
             */
            api.Deal.Delete(deal.Id.Value);

            /**
             * Get all deals
             */
            var deals = api.Deal.List<DealHubSpotModel>(false,
                new ListRequestOptions { PropertiesToInclude = new List<string> { "dealname", "amount" } });

            /**
             *  Get all deals
             *  This is commented in case the data has a large quantity of deals in HubSpot.
             */
            //var moreResults = true;
            //long offset = 0;
            //while (moreResults)
            //{
            //    var allDeals = api.Deal.List<DealHubSpotModel>(false,
            //        new ListRequestOptions { PropertiesToInclude = new List<string> { "dealname", "amount", "hubspot_owner_id", "closedate" }, Limit = 100, Offset = offset });

            //    moreResults = allDeals.MoreResultsAvailable;
            //    if (moreResults) offset = allDeals.ContinuationOffset;
            //}

            /**
             *  Get recently created deals, limited to 10 records
             *  Using DealRecentListHubSpotModel to accomodate deals returning in the "results" property.
             */
            var recentlyCreatedDeals = api.Deal.RecentlyCreated<DealHubSpotModel>(new DealRecentRequestOptions
            {
                Limit = 10,
                IncludePropertyVersion = false,
            });

            /**
             *  Get recently created deals, limited to 10 records
             *  Using DealRecentListHubSpotModel to accomodate deals returning in the "results" property.
             */
            var recentlyUpdatedDeals = api.Deal.RecentlyCreated<DealHubSpotModel>(new DealRecentRequestOptions
            {
                Limit = 10,
                IncludePropertyVersion = false,
            });
        }
    }
}
