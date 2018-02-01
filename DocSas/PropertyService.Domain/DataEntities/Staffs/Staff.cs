using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class Staff.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("Staffs")]
    public class Staff : PSBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ManagementUserId { get; set; }
        public string Title { get; set; }
        public bool IsValid { get; set; }
        public bool IsDemoAccount { get; set; }
    }
}
