using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    public class PropertyAccessForm
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        public PropertyInformationVM[] PropertyInfos { get; set; }
    }
}
