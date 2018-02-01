using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class Country.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("Countries")]
    public class Country : PSBaseEntity
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string ContinentCode { get; set; }
        public bool IsValid { get; set; }
        public bool IsLookupEnabled { get; set; }
        public bool IsSupportCounty { get; set; }
        public bool IsSupportProvince { get; set; }
        public bool IsSupportZip { get; set; }
        
    }
}
