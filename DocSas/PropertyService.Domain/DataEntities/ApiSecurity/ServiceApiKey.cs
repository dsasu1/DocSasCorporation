using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class ServiceApiKey.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("ServiceApiKeys")]
    public class ServiceApiKey: PSBaseEntity
    {

        public string AppApiKey { get; set; }
        public string AppName { get; set; }
        public string AppHostName { get; set; }
        public string AppDomainUrl { get; set; }
        public string IpAddress { get; set; }
        public bool IsValid { get; set; }

    }
}
