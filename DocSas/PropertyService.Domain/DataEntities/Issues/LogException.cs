using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class LogException.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("LogExceptions")]
    public class LogException : PSBaseEntity
    {

        public Guid? UserId { get; set; }
        public string ExceptionMessage { get; set; }
        public bool IsResolved { get; set; }

    }
}
