using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class AppPage.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("AppPages")]
    public class AppPage : PSBaseEntity
    {
        public string Title { get; set; }
        public string PageDesc { get; set; }
        public string ComponentName { get; set; }
        public string PageUrl { get; set; }
        public string MenuType { get; set; }
        public string QueryStringType { get; set; }
        public bool IsValid { get; set; }
    }
}
