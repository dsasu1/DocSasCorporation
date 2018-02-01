using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class UserVM.
    /// </summary>
    [Serializable]
    public class UserVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Lang { get; set; }
        public Guid? SecurityQuestionId { get; set; }
        public string SecurityQuestion { get; set; }
        public string UserSecurityAns { get; set; }
        public string ServiceCode { get; set; }
        public string PhotoOriginal { get; set; }
        public string PhotoThumbnail { get; set; }
        public string  UserTypeEnum { get; set; }
        public DateTime? LastLogin { get; set; }
        public string TimeZone { get; set; }
        public bool IsAcceptedTerms { get; set; }
        public bool? IsActive { get; set; }
        public bool IsBanned { get; set; }
        public bool IsLockedOut { get; set; }
        public Guid VericationCodeId { get; set; }
        public string ChangeType { get; set; }
        public Guid? LoginSessionId { get; set; }
        public bool IsDemoAccount { get; set; }


    }
}
