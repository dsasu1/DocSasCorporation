using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class ServiceRequest.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("ServiceRequests")]
    public class ServiceRequest : PSBaseEntity
    {


        public string Title { get; set; }
        public string Details { get; set; }
        public string Phone { get; set; }
        public bool GivePermission { get; set; }
        public bool HavePet { get; set; }
        public bool HaveAlarm { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantUnitId { get; set; }
        public string TenantUnitAddress { get; set; }
        public string RequestStatusKey { get; set; }
        public Guid PropertyInformationId { get; set; }
        public bool IsValid { get; set; }

    }
}
