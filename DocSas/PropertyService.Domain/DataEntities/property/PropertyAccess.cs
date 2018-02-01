using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class PropertyAccess.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("PropertyAccesses")]
    public class PropertyAccess : PSBaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid PropertyInformationId { get; set; }
        public bool IsValid { get; set; }
    }
}
