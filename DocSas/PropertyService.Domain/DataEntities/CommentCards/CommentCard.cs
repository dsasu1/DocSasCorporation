using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class CommentCard.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("CommentCards")]
    public class CommentCard : PSBaseEntity
    {
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public Guid PropertyInformationId { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsValid { get; set; }

    }
}
