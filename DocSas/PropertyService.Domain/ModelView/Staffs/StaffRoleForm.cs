using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class StaffRoleForm.
    /// </summary>
    [Serializable]
    public class StaffRoleForm
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        public StaffRoleVM[] StaffRoleInput { get; set; }
    }
}
