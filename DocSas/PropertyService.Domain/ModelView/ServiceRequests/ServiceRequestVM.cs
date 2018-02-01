using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class ServiceRequestVM.
    /// </summary>
    [Serializable]
    public class ServiceRequestVM
    {
        public Guid Id { get; set; }
        public DateTime AddedDateUtc { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Phone { get; set; }
        public bool GivePermission { get; set; }
        public string UserName { get; set; }
        public bool HavePet { get; set; }
        public bool HaveAlarm { get; set; }
        public Guid UserId { get; set; }
        public Guid? TenantUnitId { get; set; }
        public string TenantUnitAddress { get; set; }
        public string RequestStatusKey { get; set; }
        public Guid PropertyInformationId { get; set; }
    }

    [Serializable]
    public class ServiceRequestStatusVM
    {
        public Guid Id { get; set; }    
        public Guid UserId { get; set; }
        public string StatusKey { get; set; }

    }
}
