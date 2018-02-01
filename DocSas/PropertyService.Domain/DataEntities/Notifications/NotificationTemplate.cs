using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class NotificationTemplate.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("NotificationTemplates")]
    public class NotificationTemplate : PSBaseEntity
    {
        public string TemplateType { get; set; }
        public string Lang { get; set; }
        public string TemplateHtml { get; set; }
        public string TemplatePlainText { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateFromEmail { get; set; }
        public string TemplateFromName { get; set; }
        public string MediaType { get; set; }
        public bool IsValid { get; set; }
 

    }
}
