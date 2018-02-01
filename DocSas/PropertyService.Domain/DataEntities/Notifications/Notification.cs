using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class Notification.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("Notifications")]
    public class Notification : PSBaseEntity
    {
        public Guid? PropertyInformationId { get; set; }
        public Guid UserId { get; set; }
        public string NotificationResourceKey { get; set; }
        public string NotificationAdditionalInfo { get; set; }
        public Guid? NotifeeUserId { get; set; }
        public string NotificationShowFor { get; set; }
        public string NotificationTypeEnum { get; set; }
        public Guid? NotificationTypeId { get; set; }
        public bool NotificationProcessed { get; set; }

    }
}
