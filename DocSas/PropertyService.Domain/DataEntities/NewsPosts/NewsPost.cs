using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class NewsPost.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("NewsPosts")]
    public class NewsPost:PSBaseEntity
    {
        public Guid UserId { get; set; }
        public string Details { get; set; }
        public Guid PropertyInformationId { get; set; }
        public int Likes { get; set; }
        public int UnLikes { get; set; }
        public string ShareWithEnum { get; set; }
    }
}
