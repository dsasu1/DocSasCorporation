using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class UserSessionVM.
    /// </summary>
    [Serializable]
    public class UserSessionVM
    {
        public bool IsManager { get; set; }
        public Guid UserTypeId { get; set; }
        public string UserTypeTitle { get; set; }
        public string UserTypeEnum { get; set; }
        public string NoPropertyRedirectPage { get; set; }
        public Guid ManagementId { get; set; }
        public UserVM UserVM { get; set; }
    }
}
