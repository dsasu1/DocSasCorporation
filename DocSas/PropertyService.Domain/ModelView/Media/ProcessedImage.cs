using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class ProcessedImage.
    /// </summary>
    [Serializable]
    public class ProcessedImage
    {
        public byte[] OriginalImage { get; set; }
        public byte[] ThumbnailImage { get; set; }
        public string ImageType { get; set; }
    }
}
