using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class Setting.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("Settings")]
    public class Setting : PSBaseEntity
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public bool IsValid { get; set; }

    }
}
