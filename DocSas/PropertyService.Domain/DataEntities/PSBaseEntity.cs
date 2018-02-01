using System;
using System.Collections.Generic;
using System.Text;
using DSCAppEssentials.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class PSBaseEntity.
    /// </summary>
    [Serializable]
    public class PSBaseEntity 
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime AddedDateUtc { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDateUtc { get; set; } = DateTime.UtcNow;


        [Timestamp]
        public byte[] Version { get; set; }
    }
}
