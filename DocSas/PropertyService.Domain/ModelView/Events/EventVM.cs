using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class EventVM.
    /// </summary>
    [Serializable]
    public class EventVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string EventName { get; set; }
        public string Details { get; set; }
        public Guid PropertyInformationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartTimeType { get; set; }
        public string EndTimeType { get; set; }
        public bool IsAllDayEvent { get; set; }
        public string ShareWithEnum { get; set; }
        public int RsvpMaybe { get; set; }
        public int RsvpYes { get; set; }
        public int RsvpNo { get; set; }
        public DateTime AddedDateUtc { get; set; }
    }
}
