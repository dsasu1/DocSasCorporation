using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class PropertyReview.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("PropertyReviews")]
    public class PropertyReview: PSBaseEntity
    {
        public Guid UserId { get; set; }
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
        public bool IsValid { get; set; }
        public int UnHelpful { get; set; }
    }
}
