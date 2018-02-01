using DSCAppEssentials.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IMessageProvider
    /// </summary>
    public interface INewsPostProvider
    {
        /// <summary>
        /// Saves the news post asynchronous.
        /// </summary>
        /// <param name="postVM">The post vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveNewsPostAsync(PostVM postVM);
        /// <summary>
        /// Gets the news posts asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetNewsPostsAsync(Guid userId, Guid propertyId);

        /// <summary>
        /// Deletes the news post asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="postId">The post identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> DeleteNewsPostAsync(Guid userId, Guid postId);
    }
}
