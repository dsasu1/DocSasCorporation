using System;
using System.Collections.Generic;
using System.Text;
using PropertyService.Domain.DataEntities;
namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class HourVM.
    /// </summary>
    [Serializable]
    public class HourVM
    {
        public Guid UserId { get; set; }
        public Guid PropertyInformationId { get; set; }
        public List<OperationHourVM> Hours { get; set; }
    }

    /// <summary>
    /// Class OperationHourVM.
    /// </summary>
    [Serializable]
    public class OperationHourVM
    {
        public Guid Id { get; set; }
        public Guid PropertyInformationId { get; set; }
        public string DayKey { get; set; }
        public string OpenTime { get; set; }
        public string OpenTimeType { get; set; }
        public string CloseTime { get; set; }
        public string CloseTimeType { get; set; }
        public bool IsClosed { get; set; }
        public byte[] Version { get; set; }
    }
}
