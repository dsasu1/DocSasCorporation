using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class CommentCardVM.
    /// </summary>
    [Serializable]
    public class CommentCardVM
    {
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public Guid PropertyInformationId { get; set; }
        public bool IsAnonymous { get; set; }
        public byte[] UserPhoto { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime AddedDateUtc { get; set; }
    }
}
