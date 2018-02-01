using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class UserTypeVM.
    /// </summary>
    [Serializable]
    public class UserTypeVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string UserTypeEnum { get; set; }
        public string NoPropertyRedirectPage { get; set; }
    }
}
