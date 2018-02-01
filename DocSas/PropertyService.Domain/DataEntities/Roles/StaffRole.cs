using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class StaffRole.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("StaffRoles")]
    public class StaffRole : PSBaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid StaffId { get; set; }
        public Guid UserId { get; set; }
        public bool IsValid { get; set; }
    }
}
