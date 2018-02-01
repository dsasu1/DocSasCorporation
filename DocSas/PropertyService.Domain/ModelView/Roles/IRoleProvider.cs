using System;
using System.Collections.Generic;
using System.Text;
using DSCAppEssentials.Helpers;
using System.Threading.Tasks;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IRoleProvider
    /// </summary>
    public interface IRoleProvider
    {
        /// <summary>
        /// Saves the available role asynchronous.
        /// </summary>
        /// <param name="roleVM">The role vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveAvailableRoleAsync(AvailableRoleVM roleVM);
        /// <summary>
        /// Gets the available roles asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetAvailableRolesAsync(Guid userId);
        /// <summary>
        /// Gets the available role asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetAvailableRoleAsync(Guid id);

        /// <summary>
        /// Deletes the available role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> DeleteAvailableRoleAsync(Guid roleId, Guid userId);
    }
}
