using System;
using System.Collections.Generic;
using System.Text;
using PropertyService.Domain.Utilities.PSEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class ResidentsVM.
    /// </summary>
    [Serializable]
    public class ResidentsVM
    {
        public Guid Id { get; set; } // TenantUnitId
        public Guid UserId { get; set; }
        public Guid PropertyInformationId { get; set; }
        public string UnitAddress { get; set; }
        public string UnitName { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public bool IsDefault { get; set; }
        public bool? IsApproved { get; set; }
        public bool IsMovedOut { get; set; }
        public bool IsDemoAccount { get; set; }
    }

    /// <summary>
    /// Class ResidencyStatusVM.
    /// </summary>
    [Serializable]
    public class ResidencyStatusVM
    {
       public Guid Id { get; set; } // TenantUnitId
       public Guid ResidentUserId { get; set; }
       public Guid ChangerUserId { get; set; }
       public Guid PropertyInformationId { get; set; }
       public  PSResidencyChangeType StatusType { get; set; }
       public bool ChangeValue { get; set; }
    }
}
