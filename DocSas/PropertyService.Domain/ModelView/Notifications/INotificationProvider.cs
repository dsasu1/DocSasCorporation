using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface INotificationProvider
    /// </summary>
    public interface INotificationProvider
    {
        /// <summary>
        /// Gets the notifications asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="isMainPageView">if set to <c>true</c> [is main page view].</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetNotificationsAsync(Guid userId, Guid propertyId, bool isMainPageView = false);
    }
}
