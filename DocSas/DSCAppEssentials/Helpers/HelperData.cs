using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DSCAppEssentials.Helpers
{
    /// <summary>
    /// Class DSCEmail.
    /// </summary>
    [Serializable]
    public class DSCEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DSCEmail"/> class.
        /// </summary>
        /// <param name="fromEmail">From email.</param>
        /// <param name="toEmail">To email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="htmlContent">Content of the HTML.</param>
        public DSCEmail(Tuple<string, string> fromEmail, List<string> toEmail, string subject, string htmlContent)
        {
            FromEmail = fromEmail;
            ToEmail = toEmail;
            Subject = subject;
            HTMLContent = Utility.HtmlDecode(HTMLContent);
            PlainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
        }
        public Tuple<string,string> FromEmail { get; set; }
        public List<string> ToEmail { get; set; }
        public string Subject { get; set; }
        public string HTMLContent { get; set; }
        public string PlainTextContent { get; set; }
        public string EmailApiKey { get; set; }
    }

    /// <summary>
    /// Class DSCResponse.
    /// </summary>
    [Serializable]
    public class DSCResponse
    {
        public bool IsSuccess { get { return ErrorMessage == null || ErrorMessage.Count < 1; } }
        public Dictionary<string, string> ErrorMessage { get; set; }
        public object ResponseData { get; set; }
    }

    /// <summary>
    /// Class StorageResponse.
    /// </summary>
    [Serializable]
    public class StorageResponse
    {
        public List<UploadResult> UploadedResults { get; set; }
    }

    /// <summary>
    /// Class StorageRequest.
    /// </summary>
    public class StorageRequest
    {
        public List<UploadFileContent> UploadFiles { get; set; }
        public string ContainerName { get; set; }
        public string ConnectionString { get; set; }
    }

    /// <summary>
    /// Class UploadFileContent.
    /// </summary>
    [Serializable]
    public class UploadFileContent
    {
        public string FileName { get; set; }
        public bool HasThumbnail { get; set; }
        public string FileThumbnailName { get; set; }
        public string MimeType { get; set; }
        public byte[] FileData { get; set; }
        public byte[] ThumbnailFileData { get; set; }
    }

    public class UploadResult 
    {
        public string Name { get; set; }
        public string UploadedUrl { get; set; }
        public string ThumbNailFileName { get; set; }
        public string ThumbNailUrl { get; set; }
        public bool IsSuccess { get; set; }
    }

}
