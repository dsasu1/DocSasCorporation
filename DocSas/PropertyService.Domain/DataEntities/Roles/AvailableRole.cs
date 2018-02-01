using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class AvailableRole.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("AvailableRoles")]
    public class AvailableRole : PSBaseEntity
    {
        public string Title { get; set; }
        public string RoleDesc { get; set; }
        public Guid ManagementUserId { get; set; }
        public bool HasManagementRights { get; set; }
        public bool IsValid { get; set; }
        public bool IsDemoAccount { get; set; }

    }
}
