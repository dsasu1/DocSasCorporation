using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IServiceRequestProvider
    /// </summary>
    public interface IServiceRequestProvider
    {
        /// <summary>
        /// Saves the service request asynchronous.
        /// </summary>
        /// <param name="serviceRequest">The service request.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveServiceRequestAsync(ServiceRequestVM serviceRequest);
        /// <summary>
        /// Gets the service requests asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetServiceRequestsAsync(Guid userId, Guid propertyId);

        /// <summary>
        /// Saves the service request status asynchronous.
        /// </summary>
        /// <param name="serviceRequestStatus">The service request status.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveServiceRequestStatusAsync(ServiceRequestStatusVM serviceRequestStatus);
    }
}
