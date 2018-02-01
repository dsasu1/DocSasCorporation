using System;
using System.Collections.Generic;
using System.Text;
using PropertyService.Domain.DataEntities;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class PropertyInformationVM.
    /// </summary>
    [Serializable]
    public class PropertyInformationVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ManagementUserId { get; set; }
        public string PropName { get; set; }
        public string PropCode { get; set; }
        public string AboutUs { get; set; }
        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public string UrlFriendlyName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Weburl { get; set; }
        public string PropTimezone { get; set; }
        public string PropType { get; set; }
        public string CoverOriginal { get; set; }
        public string CoverThumbnail { get; set; }
        public bool IsLive { get; set; }
        public bool IsValid { get; set; }
        public string IPAddress { get; set; }
        public bool IsDemoAccount { get; set; }
        public ZipCode ZipCode { get; set; }

    }
}
