using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class PageAccess.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("PageAccesses")]
    public class PageAccess: PSBaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid AppPageId { get; set; }
        public bool IsValid { get; set; }
    }
}
