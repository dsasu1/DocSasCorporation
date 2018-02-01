using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class TenantUnit.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("TenantUnits")]
    public class TenantUnit : PSBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid PropertyInformationId { get; set; }
        public string UnitAddress { get; set; }
        public string UnitName { get; set; }
        public bool IsDefault { get; set; }
        public bool? IsApproved { get; set; }
        public bool IsMovedOut { get; set; }
        public bool IsDemoAccount { get; set; }

    }
}
