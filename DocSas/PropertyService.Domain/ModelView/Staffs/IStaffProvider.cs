using System;
using System.Collections.Generic;
using System.Text;
using DSCAppEssentials.Helpers;
using System.Threading.Tasks;


namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IStaffProvider
    /// </summary>
    public interface IStaffProvider
    {
        /// <summary>
        /// Saves the staff asynchronous.
        /// </summary>
        /// <param name="staffVM">The staff vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveStaffAsync(StaffVM staffVM);
        /// <summary>
        /// Saves the role staff asynchronous.
        /// </summary>
        /// <param name="staffVM">The staff vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveRoleStaffAsync(StaffRoleForm staffVM);
        /// <summary>
        /// Gets the staffs asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetStaffsAsync(Guid userId);

        /// <summary>
        /// Deletes the staff asynchronous.
        /// </summary>
        /// <param name="staffId">The staff identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> DeleteStaffAsync(Guid staffId, Guid userId);

        /// <summary>
        /// Deletes the staff role asynchronous.
        /// </summary>
        /// <param name="staffRoleId">The staff role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> DeleteStaffRoleAsync(Guid staffRoleId, Guid userId);
    }
}
