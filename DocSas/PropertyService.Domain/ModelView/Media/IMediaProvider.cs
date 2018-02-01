using DSCAppEssentials.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IMediaProvider
    /// </summary>
    public interface IMediaProvider
    {
        /// <summary>
        /// Uploads the data asynchronous.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> UploadDataAsync(UploadOptions options);
    }
}
