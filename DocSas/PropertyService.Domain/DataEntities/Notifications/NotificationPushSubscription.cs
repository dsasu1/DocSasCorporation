using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    [Serializable]
    [Table("NotificationPushSubscriptions")]
    public class NotificationPushSubscription : PSBaseEntity
    {
        public Guid UserId { get; set; }
        public string SubscriptionData { get; set; }
        public string BrowserFingerPrint { get; set; }
    }
}
