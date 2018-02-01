using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class VerificationCode.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("VerificationCodes")]
    public class VerificationCode : PSBaseEntity
    {
        public string VerifyCode { get; set; }
        public Guid UserId { get; set; }
        public string VerifyType { get; set; }
        public string MediaType { get; set; }
        public bool IsVerified { get; set; }
        public bool IsExpired { get; set; }

    }
}
