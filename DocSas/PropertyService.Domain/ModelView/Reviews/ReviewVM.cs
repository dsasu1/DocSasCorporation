using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class ReviewVM.
    /// </summary>
    [Serializable]
    public class ReviewVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public Guid PropertyInformationId { get; set; }
        public int OverallRating { get; set; }
        public int StaffRating { get; set; }
        public int NoiseRating { get; set; }
        public int MaintenanceRating { get; set; }
        public int NeighborRating { get; set; }
        public int SafetyRating { get; set; }
        public int GroundsRating { get; set; }
        public int Helpful { get; set; }
        public int UnHelpful { get; set; }
        public DateTime AddedDateUtc { get; set; }
    }
}
