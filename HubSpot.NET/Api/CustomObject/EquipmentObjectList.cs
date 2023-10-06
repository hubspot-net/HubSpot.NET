using System;
using System.Collections.Generic;

namespace HubSpot.NET.Api.CustomObject
{
    public static class EquipmentObjectList
    {
        public static string GetEquipmentPropsList()
        {
            var equipmentPropsList =  new List<string>
            {
                "additional_information",
                "backoffice_id",
                "bb_eng_legacy_equipmentcategory_slug",
                "category",
                "city",
                "commission_percent",
                "date_of_first_qualified_lead",
                "equipment_title",
                "fleet_identifier",
                "hs_all_accessible_team_ids",
                "hs_all_assigned_business_unit_ids",
                "hs_all_owner_ids",
                "hs_all_team_ids",
                "hs_created_by_user_id",
                "hs_createdate",
                "hs_lastmodifieddate",
                "hs_merged_object_ids",
                "hs_object_id",
                "hs_object_source",
                "hs_object_source_id",
                "hs_object_source_user_id",
                "hs_pinned_engagement_id",
                "hs_read_only",
                "hs_unique_creation_key",
                "hs_updated_by_user_id",
                "hs_user_ids_of_all_notification_followers",
                "hs_user_ids_of_all_notification_unfollowers",
                "hs_user_ids_of_all_owners",
                "hs_was_imported",
                "hubspot_owner_assigneddate",
                "hubspot_owner_id",
                "hubspot_team_id",
                "item_number",
                "karintest",
                "line_item_id",
                "listing_to_lead_time",
                "location",
                "location_combined",
                "machine_posted_datestamp",
                "make",
                "make__other",
                "new_associated_deal",
                "photo",
                "poc_email",
                "product_id",
                "qualified_to_buy_datestamp",
                "seller_company_id",
                "seller_company_name",
                "seller_contact_id",
                "seller_deal_id",
                "state",
                "submission_idempotent_id",
                "time_between_posted_and_first_buyer",
                "warranty_eligible",
                "year",
                "year1",
                "zip_code",
                "name",
                "model",
                "vin",
                "hoursmileage"
            };
            return string.Join(",", equipmentPropsList);
        }
    }
}
