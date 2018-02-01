using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using PropertyService.Domain.ModelView.External;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IUserProvider
    /// </summary>
    public interface IUserProvider
    {
        /// <summary>
        /// Gets the user types asynchronous.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetUserTypesAsync();
        /// <summary>
        /// Gets the user type asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetUserTypeAsync(Guid id);
        /// <summary>
        /// Registers the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> RegisterUserAsync(UserVM user);
        /// <summary>
        /// Saves the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveUserAsync(UserVM user);
        /// <summary>
        /// Logins the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> LoginUserAsync(UserVM user);
        /// <summary>
        /// Confirms the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> ConfirmUserAsync(UserVM user);
        /// <summary>
        /// Retrieves the account asynchronous.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> RetrieveAccountAsync(UserVM userVM);
        /// <summary>
        /// Verifies the security answer asynchronous.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> VerifySecurityAnswerAsync(UserVM userVM);
        /// <summary>
        /// Verifies the change password asynchronous.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> VerifyChangePasswordAsync(UserVM userVM);
        /// <summary>
        /// Saves the new password asynchronous.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveNewPasswordAsync(UserVM userVM);
        /// <summary>
        /// Verifies the recaptcha asynchronous.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> VerifyRecaptchaAsync(RecaptchaRequest req);
        /// <summary>
        /// Verifies the user session asynchronous.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> VerifyUserSessionAsync(UserVM userVM);
        /// <summary>
        /// Determines whether [has management rights asynchronous] [the specified user vm].
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> HasManagementRightsAsync(UserVM userVM);
        /// <summary>
        /// Deactivates the account asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> DeactivateAccountAsync(UserVM user);

        /// <summary>
        /// Updates the user profile images asynchronous.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> UpdateUserProfileImagesAsync(StorageResponse result, Guid userId);
    }
}
