using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class NotificationViewTrack.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("NotificationViewTracks")]
    public class NotificationViewTrack : PSBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? PropertyInformationId { get; set; }
        public int ViewCount { get; set; }
  
    }
}
