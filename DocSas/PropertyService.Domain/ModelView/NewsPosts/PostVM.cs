using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class PostVM.
    /// </summary>
    [Serializable]
    public class PostVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public string UserTypeEnum { get; set; }
        public string Details { get; set; }
        public Guid PropertyInformationId { get; set; }
        public int Likes { get; set; }
        public int UnLikes { get; set; }
        public string ShareWithEnum { get; set; }
        public DateTime AddedDateUtc { get; set; }
    }
}
