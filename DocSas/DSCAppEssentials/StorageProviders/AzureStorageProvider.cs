using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace DSCAppEssentials.StorageProviders
{
    public class AzureStorageProvider : IAzureStorageProvider
    {

        public async Task<StorageResponse> Save(StorageRequest request)
        {
            StorageResponse storeResponse = new StorageResponse();
            List<UploadResult> results = new List<UploadResult>();

            var container = await GetContainer(request.ContainerName, request.ConnectionString);

            foreach (var item in request.UploadFiles)
            {
                var result = await Upload(container, item);

                if (item.HasThumbnail && item.ThumbnailFileData != null)
                {
                    var thumbNailResult  = await Upload(container, new UploadFileContent() { FileData = item.ThumbnailFileData, FileName = item.FileThumbnailName, MimeType = item.MimeType });

                    if (thumbNailResult.IsSuccess)
                    {
                        result.ThumbNailFileName = thumbNailResult.Name;
                        result.ThumbNailUrl = thumbNailResult.UploadedUrl;
                    }
                }


                results.Add(result);
           

            }


            storeResponse.UploadedResults = results;
           return storeResponse;
        }

        private async Task<UploadResult> Upload(CloudBlobContainer container, UploadFileContent item)
        {
            var blockBlob = container.GetBlockBlobReference(item.FileName);
             await blockBlob.UploadFromByteArrayAsync(item.FileData, 0, item.FileData.Length);

            if (blockBlob.Uri.AbsoluteUri != null)
            {
                return new UploadResult() { Name = blockBlob.Name, UploadedUrl = blockBlob.Uri.AbsoluteUri, IsSuccess = true };
            }

            return new UploadResult() { IsSuccess = false };
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Task&lt;CloudBlobContainer&gt;.</returns>
        private async Task<CloudBlobContainer> GetContainer(string containerName, string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync();

            await container.SetPermissionsAsync(
             new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            return container;


        }
    }
}
