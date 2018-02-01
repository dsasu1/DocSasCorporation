using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface ICommentCardProvider
    /// </summary>
    public interface ICommentCardProvider
    {
        /// <summary>
        /// Saves the comment card asynchronous.
        /// </summary>
        /// <param name="commentCard">The comment card.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveCommentCardAsync(CommentCardVM commentCard);
        /// <summary>
        /// Gets the comment cards asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetCommentCardsAsync(Guid userId, Guid propertyId);
    }
}
