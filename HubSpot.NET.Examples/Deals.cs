﻿using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubSpot.NET.Examples
{
    public class Deals
    {
        public static async Task Example(HubSpotApi api)
        {
            try
            {
                await Tests(api);
                Console.WriteLine("Deals tests completed successfully!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Deals tests failed!");
                Console.WriteLine(ex.ToString());
            }
        }
        private static async Task Tests(HubSpotApi api)
        {
            /**
             * Create a deal
             */
            var deal = await api.Deal.CreateAsync(new DealHubSpotModel()
            {
                Amount = 10000,
                Name = "New Deal #1"
            });

            /**
             * Delete a deal
             */
            await api.Deal.DeleteAsync(deal.Id.Value);

            /**
             * Get all deals
             */
            var deals = await api.Deal.ListAsync(false,
                new ListRequestOptions(250) { PropertiesToInclude = new List<string> { "dealname", "amount" } });

            /**
             *  Get all deals
             *  This is commented in case the HubSpot data has a large quantity of deals.
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
             *  Get recently created deals since 7 days ago, limited to 10 records
             *  Will default to 30 day if Since is not set.
             *  Using DealRecentListHubSpotModel to accomodate deals returning in the "results" property.
             */
            var currentdatetime = DateTime.SpecifyKind(DateTime.Now.AddDays(-7), DateTimeKind.Utc);
            var since = ((DateTimeOffset)currentdatetime).ToUnixTimeMilliseconds().ToString();

            var recentlyCreatedDeals = await api.Deal.RecentlyCreatedAsync(new DealRecentRequestOptions
            {
                Limit = 10,
                IncludePropertyVersion = false,
                Since = since
            });

            /**
             *  Get recently created deals since 7 days ago, limited to 10 records
             *  Will default to 30 day if Since is not set.
             *  Using DealRecentListHubSpotModel to accomodate deals returning in the "results" property.
             */
            var recentlyUpdatedDeals = await api.Deal.RecentlyCreatedAsync(new DealRecentRequestOptions
            {
                Limit = 10,
                IncludePropertyVersion = false,
                Since = since
            });
        }
    }
}
