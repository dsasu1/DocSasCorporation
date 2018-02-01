using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class PropertyInformation.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("PropertyInformations")]
    public class PropertyInformation : PSBaseEntity
    {
        public Guid ManagementUserId { get; set; }
        public string PropName { get; set; }
        public string PropCode { get; set; }
        public string UrlFriendlyName { get; set; }
        public string AboutUs { get; set; }
        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
       // [ForeignKey("ZipCodes")]
        public Guid ZipId { get; set; }
        public string PropType { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Weburl { get; set; }
        public string PropTimezone { get; set; }
        public string CoverOriginal { get; set; }
        public string CoverThumbnail { get; set; }
        public bool IsValid { get; set; }
        public bool IsLive { get; set; }
        public bool IsDemoAccount { get; set; }

        public ZipCode ZipCode { get; set; }
    
    }
}
