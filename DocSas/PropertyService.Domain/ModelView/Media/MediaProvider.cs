using DSCAppEssentials.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PropertyService.Domain.Utilities.PSEnums;
using System.Linq;
using DSCAppEssentials.Extensions;
using DSCAppEssentials.StorageProviders;
using DSCAppEssentials.Abstract;
using PropertyService.Domain.Managers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class MediaProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IMediaProvider" />
    public class MediaProvider : IMediaProvider
    {
        private readonly IPropertyProvider _propertyProvider;
        private readonly IUserProvider _userProviderr;
        private readonly IAzureStorageProvider _azureProvider;
        private readonly ISettingProvider _settingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaProvider"/> class.
        /// </summary>
        /// <param name="propertyProvider">The property provider.</param>
        /// <param name="azureProvider">The azure provider.</param>
        public MediaProvider(IPropertyProvider propertyProvider, IUserProvider userProvider, IAzureStorageProvider azureProvider, ISettingProvider settingProvider)
        {
            _propertyProvider = propertyProvider;
            _settingProvider = settingProvider;
            _azureProvider = azureProvider;
            _userProviderr = userProvider;
        }
        /// <summary>
        /// upload data as an asynchronous operation.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> UploadDataAsync(UploadOptions options)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            if (options != null && options.Files != null)
            {
                List<UploadFileContent> fileContents = new List<UploadFileContent>();
                StorageRequest storageRequest = new StorageRequest()
                {
                    ConnectionString = await _settingProvider.GetSetting(SettingKey.AzureBlobConnectionString)
                };
                if (options.FileType == PSFileType.image.ToString())
                {
                    storageRequest.ContainerName = options.ContainerName;

                    UploadFileContent fileContent = null;
                    foreach (var file in options.Files)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            fileContent = new UploadFileContent()
                            {
                                HasThumbnail = options.HasThumbnail,
                                MimeType = file.ContentType,
                                FileData = memoryStream.ToArray()
                            };
                        
                                fileContent.FileName = string.Concat(options.DirectoryId, "/", file.FileName);
                                if (fileContent.HasThumbnail)
                                {
                                    fileContent.ThumbnailFileData = Utility.Cropping(fileContent.FileData, options.ThumbnailWidth, options.ThumbnaiHeight);
                                    fileContent.FileThumbnailName = string.Concat(options.DirectoryId, "/", "thumbnail", file.FileName);
                                }
                                                                                     

                            fileContents.Add(fileContent);

                         }
                    }

                    storageRequest.UploadFiles = fileContents;
                    var result = await _azureProvider.Save(storageRequest);

                    if (options.UploadType == PSUploadType.CoverImage.ToString())
                    {
                        response = await _propertyProvider.UpdatePropertyImagesAsync(result, options.PropertyInformationId, options.UserId);
                    }
                    else if (options.UploadType == PSUploadType.ProfilePic.ToString())
                    {
                        response = await _userProviderr.UpdateUserProfileImagesAsync(result, options.UserId);
                    }
                }

            }
           

            response.ErrorMessage = msg;
            return response;
        }
    }
}
