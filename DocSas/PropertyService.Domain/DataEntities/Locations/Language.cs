using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class Language.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("Languages")]
    public class Language  : PSBaseEntity
    {
        public string DisplayName { get; set; }
        public string Lang { get; set; }
        public string TextDirection { get; set; }
        public bool IsValid { get; set; }
        public bool IsDefault { get; set; }
 

    }
}
