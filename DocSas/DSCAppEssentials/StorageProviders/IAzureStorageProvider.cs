using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace DSCAppEssentials.StorageProviders
{
    /// <summary>
    /// Interface IAzureStorageProvider
    /// </summary>
    public interface IAzureStorageProvider
    {
        /// <summary>
        /// Saves the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;StorageResponse&gt;.</returns>
        Task<StorageResponse> Save(StorageRequest request);
    }
}
