using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class TenantUnitVM.
    /// </summary>
    [Serializable]
    public  class TenantUnitVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PropertyInformationId { get; set; }
        public string PropertyCode { get; set; }
        public string UnitAddress { get; set; }
        public string UnitName { get; set; }
        public bool IsDefault { get; set; }
        public bool? IsApproved { get; set; }
        public bool IsMovedOut { get; set; }
        public bool IsDemoAccount { get; set; }
    }
}
