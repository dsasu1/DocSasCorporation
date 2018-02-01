using DSCAppEssentials.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IReviewProvider
    /// </summary>
    public interface IReviewProvider
    {
        /// <summary>
        /// Saves the review asynchronous.
        /// </summary>
        /// <param name="reviewVM">The review vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveReviewAsync(ReviewVM reviewVM);
        /// <summary>
        /// Gets the reviews asynchronous.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetReviewsAsync(Guid propertyId);
    }
}
