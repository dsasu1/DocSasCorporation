using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class SecurityQuestion.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("SecurityQuestions")]
    public class SecurityQuestion : PSBaseEntity
    {
        public string Question { get; set; }
        public bool? IsValid { get; set; }
    }
}
