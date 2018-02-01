using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class ZipCode.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("ZipCodes")]
    public class ZipCode : PSBaseEntity
    {
      
        public string Code { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string County { get; set; }
        public string CountryCode { get; set; }
        public bool IsValid { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


    }
}
