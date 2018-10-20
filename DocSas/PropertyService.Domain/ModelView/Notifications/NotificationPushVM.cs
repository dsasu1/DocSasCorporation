using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    public class NotificationPushVM
    {
        public string EndPoint { get; set; }
        public Guid UserId { get; set; }
    }
}
