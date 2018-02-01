using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyService.Domain.DataEntities
{
    /// <summary>
    /// Class ServiceCodeTrack.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.DataEntities.PSBaseEntity" />
    [Serializable]
    [Table("ServiceCodeTracks")]
    public class ServiceCodeTrack : PSBaseEntity
    {
        public string CodeType { get; set; }
        public long StartValue { get; set; }
        public long MaxInd { get; set; }
        public string AvailableLetters { get; set; }
        public int LettersLength { get; set; }
        public string NumberPosition { get; set; }
    }
}
