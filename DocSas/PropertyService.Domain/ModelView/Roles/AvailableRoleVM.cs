using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class AvailableRoleVM.
    /// </summary>
    [Serializable]
    public class AvailableRoleVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string RoleDesc { get; set; }
        public Guid ManagementUserId { get; set; }
        public Guid CreatorUserId { get; set; }
        public bool HasManagementRights { get; set; }
        public bool IsValid { get; set; }
        public bool IsDemoAccount { get; set; }

    }
}
