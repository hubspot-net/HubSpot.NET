using HubSpot.NET.Core.Interfaces;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace HubSpot.NET.Api.CustomObject;
public class HubspotEquipmentObjectModel : IHubSpotModel
{
#nullable enable

    [DataMember(Name = "additional_information")]
    public string? AdditionalInformation { get; set; }

    [DataMember(Name = "backoffice_id")]
    public string? BackofficeId { get; set; }

    [DataMember(Name = "bb_eng_legacy_equipmentcategory_slug")]
    public string? BbEngLegacyEquipmentCategorySlug { get; set; }

    [DataMember(Name = "category")]
    public string? Category { get; set; }

    [DataMember(Name = "city")]
    public string? City { get; set; }

    [DataMember(Name = "commission_percent")]
    public double? CommissionPercent { get; set; }

    [DataMember(Name = "date_of_first_qualified_lead")]
    public DateTime? DateOfFirstQualifiedLead { get; set; }

    [DataMember(Name = "equipment_title")]
    public string? EquipmentTitle { get; set; }

    [DataMember(Name = "fleet_identifier")]
    public string? FleetIdentifier { get; set; }

    [DataMember(Name = "hs_created_by_user_id")]
    public string? HsCreatedByUserId { get; set; }

    [DataMember(Name = "hs_createdate")]
    public DateTime? HsCreateDate { get; set; }

    [DataMember(Name = "hs_lastmodifieddate")]
    public DateTime? HsLastModifiedDate { get; set; }

    [DataMember(Name = "hs_object_id")]
    public string? HsObjectId { get; set; }

    [DataMember(Name = "hs_object_source")]
    public string? HsObjectSource { get; set; }

    [DataMember(Name = "hs_object_source_id")]
    public string? HsObjectSourceId { get; set; }

    [DataMember(Name = "hs_object_source_user_id")]
    public string? HsObjectSourceUserId { get; set; }

    [DataMember(Name = "hs_pinned_engagement_id")]
    public string? HsPinnedEngagementId { get; set; }

    [DataMember(Name = "hs_read_only")]
    public bool? HsReadOnly { get; set; }

    [DataMember(Name = "hs_unique_creation_key")]
    public string? HsUniqueCreationKey { get; set; }

    [DataMember(Name = "hs_updated_by_user_id")]
    public string? HsUpdatedByUserId { get; set; }
    
    [DataMember(Name = "hs_was_imported")]
    public bool? HsWasImported { get; set; }

    [DataMember(Name = "hubspot_owner_assigneddate")]
    public DateTime? HubspotOwnerAssignedDate { get; set; }

    [DataMember(Name = "hubspot_owner_id")]
    public string? HubspotOwnerId { get; set; }

    [DataMember(Name = "hubspot_team_id")]
    public string? HubspotTeamId { get; set; }

    [DataMember(Name = "item_number")]
    public string? ItemNumber { get; set; }

    [DataMember(Name = "karintest")]
    public string? KarinTest { get; set; }

    [DataMember(Name = "line_item_id")]
    public string? LineItemId { get; set; }

    [DataMember(Name = "listing_to_lead_time")]
    public string? ListingToLeadTime { get; set; }

    [DataMember(Name = "location")]
    public string? Location { get; set; }

    [DataMember(Name = "location_combined")]
    public string? LocationCombined { get; set; }

    [DataMember(Name = "machine_posted_datestamp")]
    public DateTime? MachinePostedDatestamp { get; set; }

    [DataMember(Name = "make")]
    public string? Make { get; set; }

    [DataMember(Name = "make__other")]
    public string? MakeOther { get; set; }

    [DataMember(Name = "new_associated_deal")]
    public DateTime? NewAssociatedDeal { get; set; }

    [DataMember(Name = "photo")]
    public string? Photo { get; set; }

    [DataMember(Name = "poc_email")]
    public string? PocEmail { get; set; }

    [DataMember(Name = "product_id")]
    public string? ProductId { get; set; }

    [DataMember(Name = "qualified_to_buy_datestamp")]
    public DateTime? QualifiedToBuyDatestamp { get; set; }

    [DataMember(Name = "seller_company_id")]
    public string? SellerCompanyId { get; set; }

    [DataMember(Name = "seller_company_name")]
    public string? SellerCompanyName { get; set; }

    [DataMember(Name = "seller_contact_id")]
    public string? SellerContactId { get; set; }

    [DataMember(Name = "seller_deal_id")]
    public string? SellerDealId { get; set; }

    [DataMember(Name = "state")]
    public string? State { get; set; }

    [DataMember(Name = "submission_idempotent_id")]
    public string? SubmissionIdempotentId { get; set; }

    [DataMember(Name = "time_between_posted_and_first_buyer")]
    public string? TimeBetweenPostedAndFirstBuyer { get; set; }

    [DataMember(Name = "warranty_eligible")]
    public bool? WarrantyEligible { get; set; }

    [DataMember(Name = "year")]
    public string? Year { get; set; }

    [DataMember(Name = "year1")]
    public string? Year1 { get; set; }

    [DataMember(Name = "zip_code")]
    public string? ZipCode { get; set; }

    [DataMember(Name = "name")]
    public string? Name { get; set; }

    [DataMember(Name = "model")]
    public string? Model { get; set; }

    [DataMember(Name = "vin")]
    public string? Vin { get; set; }

    [DataMember(Name = "hoursmileage")]
    public string? HoursMileage { get; set; }

    public bool IsNameValue => false;
    public void ToHubSpotDataEntity(ref dynamic dataEntity)
    {
    }

    public void FromHubSpotDataEntity(dynamic hubspotData)
    {
    }

    public string RouteBasePath => "crm/v3/objects";
}