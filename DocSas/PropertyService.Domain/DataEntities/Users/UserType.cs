using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class UserType.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("UserTypes")]
    public class UserType : PSBaseEntity
    {
        public string Title { get; set; }
        public string UserTypeEnum { get; set; }
        public bool? IsValid { get; set; }
        public string NoPropertyRedirectPage { get; set; }
    }
}
