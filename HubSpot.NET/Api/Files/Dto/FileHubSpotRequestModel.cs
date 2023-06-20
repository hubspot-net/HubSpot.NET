using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Files.Dto
{
    /// <summary>
    /// Models a file, used for the COS files API
    /// </summary>

    [DataContract]
    public class FileHubSpotRequestModel : IHubSpotModel
    {
        [DataMember(Name="id")]
        public long Id { get;set; }

        [DataMember(Name="file")]
        public byte[] File { get;set; }

        [DataMember(Name="name")]
        public string Name { get;set; }

        [DataMember(Name="type")]
        public string Type { get; set; }

        [DataMember(Name="folderPath")]
        public string FolderPath { get; set; }
        
        [DataMember(Name="folderId")]
        public string FolderId { get; set; }
        
        [DataMember(Name="charsetHunch")]
        public string CharsetHunch { get; set; }
        
        [DataMember(Name="options")]
        public FileHubSpotRequestOptionsModel Options { get; set; }
        
        public bool IsNameValue { get; }

        public void ToHubSpotDataEntity(ref dynamic dataEntity)
        {
        }

        public void FromHubSpotDataEntity(dynamic hubspotData)
        {
        }

        public string RouteBasePath => "/filemanager/api/v3/files";
    }

    [DataContract]
    public class FileHubSpotRequestOptionsModel
    {
        [StringRange(AllowableValues = new[] {"PUBLIC_INDEXABLE", "PRIVATE_INDEXABLE", "PRIVATE"},
            ErrorMessage = "Must be PUBLIC_INDEXABLE, PRIVATE_INDEXABLE, or PRIVATE")]
        [DataMember(Name = "access")]
        public string Access { get; set; } = "PRIVATE";
        
        [DataMember(Name="ttl")]
        public string TTL { get; set; }
        
        [DataMember(Name="overwrite")]
        public bool Overwrite { get; set; }
        
        [StringRange(AllowableValues = new[] { "REJECT", "RETURN_EXISTING", "NONE" }, ErrorMessage = "Must be REJECT, RETURN_EXISTING, or NONE")]
        [DataMember(Name="duplicateValidationStrategy")]
        public string DuplicateValidationStrategy { get; set; }
        
        [StringRange(AllowableValues = new[] { "ENTIRE_PORTAL", "EXACT_FOLDER" }, ErrorMessage = "Must be ENTIRE_PORTAL or EXACT_FOLDER")]
        [DataMember(Name="duplicateValidationScope")]
        public string DuplicateValidationScope { get; set; }
    }

    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            var msg =
                $"Please enter one of the allowable values: {string.Join(", ", (AllowableValues ?? new string[] {"No allowable values found"}))}.";
            return new ValidationResult(msg);
        }
    }
}