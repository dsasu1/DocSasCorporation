using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class PropertyEnabledPage.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("PropertyEnabledPages")]
    public class PropertyEnabledPage : PSBaseEntity
    {
        public Guid PropertyInformationId { get; set; }
        public Guid AppPageId { get; set; }
        public bool IsValid { get; set; }
    }
}
