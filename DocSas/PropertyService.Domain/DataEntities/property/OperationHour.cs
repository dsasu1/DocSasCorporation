using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class OperationHour.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("OperationHours")]
    public class OperationHour : PSBaseEntity
    {
        public Guid PropertyInformationId { get; set; }
        public string DayKey { get; set; }
        public string OpenTime { get; set; }
        public string OpenTimeType { get; set; }
        public string CloseTime { get; set; }
        public string CloseTimeType { get; set; }
        public bool IsClosed { get; set; }
    }
}
