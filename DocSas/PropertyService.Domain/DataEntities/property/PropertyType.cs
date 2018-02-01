using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class PropertyType.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("PropertyTypes")]
    public class PropertyType : PSBaseEntity
    {
   
        public string Title { get; set; }
        public bool IsValid { get; set; }
        public string PropertyTypeEnum { get; set; }
    }
}
