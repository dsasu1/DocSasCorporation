using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class LanguageVM.
    /// </summary>
    [Serializable]
    public class LanguageVM
    {
        public string DisplayName { get; set; }
        public string Lang { get; set; }
        public string TextDirection { get; set; }
    }
}
