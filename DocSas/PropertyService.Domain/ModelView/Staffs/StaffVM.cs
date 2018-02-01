using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class StaffVM.
    /// </summary>
    [Serializable]
    public class StaffVM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string[] serviceCodeValues { get; set; }
        public Guid ManagementUserId { get; set; }
        public Guid CreatorUserId { get; set; }
        public string serviceCode { get; set; }
        public bool IsValid { get; set; }
        public bool IsDemoAccount { get; set; }
    }
}
