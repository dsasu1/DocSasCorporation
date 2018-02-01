using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class User.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("Users")]
    public class User :PSBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Lang { get; set; } = "en";
        public string Password { get; set; }
        public Guid? SecurityQuestionId { get; set; }
        public string UserSecurityAns { get; set; } = string.Empty;
        public string ServiceCode { get; set; }
        public string PhotoOriginal { get; set; }
        public string PhotoThumbnail { get; set; }
        public string UserTypeEnum { get; set; }
        public string UserSalt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string TimeZone { get; set; }
        public bool IsAcceptedTerms { get; set; }
        public bool? IsActive { get; set; }
        public bool IsBanned { get; set; }
        public bool IsLockedOut { get; set; }
        public int LoginAttempts { get; set; }
        public string IpAddress { get; set; }
        public Guid? LoginSessionId { get; set; }
        public bool IsDemoAccount { get; set; }


    }
}
