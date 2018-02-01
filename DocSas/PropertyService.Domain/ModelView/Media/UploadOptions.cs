using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class UploadOptions.
    /// </summary>
    [Serializable]
    public class UploadOptions
    {
        public string UploadType { get; set; }
        public Guid UserId { get; set; }
        public string FileType { get; set; }
        public Guid PropertyInformationId { get; set; }
        public Guid DirectoryId { get; set; }
        public bool HasThumbnail { get; set; } = false;
        public int ThumbnailWidth { get; set; } 
        public int ThumbnaiHeight { get; set; }
        public string ContainerName { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    /// <summary>
    /// Class FileOptions.
    /// </summary>
    [Serializable]
    public class FileOptions
    {
        public string UploadType { get; set; }
        public Guid UserId { get; set; }
        public string FileType { get; set; }
        public Guid PropertyInformationId { get; set; }
    }
}
