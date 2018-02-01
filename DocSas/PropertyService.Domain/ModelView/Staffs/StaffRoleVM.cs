using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class StaffRoleVM.
    /// </summary>
    [Serializable]
    public class StaffRoleVM
    {
        public Guid Id { get; set; } //StaffId
        public Guid UserId { get; set; }
        public Guid? StaffRoleId { get; set; }
        public string Title { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public bool IsDemoAccount { get; set; }

    }
}
