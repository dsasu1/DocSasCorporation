using System;
using System.Collections.Generic;
using System.Text;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using DSCAppEssentials.Helpers;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers.DSCEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IAppCommon
    /// </summary>
    public interface IAppCommon
    {
        /// <summary>
        /// Determines whether [is user account valid] [the specified user entity].
        /// </summary>
        /// <param name="userEntity">The user entity.</param>
        /// <returns>DSCResponse.</returns>
        DSCResponse IsUserAccountValid(User userEntity);

        /// <summary>
        /// Determines whether [is user account valid asynchronous] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> IsUserAccountValidAsync(Guid userId);

        /// <summary>
        /// Verifies the current password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> VerifyCurrentPasswordAsync(UserVM user);

        /// <summary>
        /// Determines whether [is veriy code asynchronous] [the specified verify code identifier].
        /// </summary>
        /// <param name="verifyCodeId">The verify code identifier.</param>
        /// <param name="vtype">The vtype.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> IsVeriyCodeAsync(Guid verifyCodeId, DSCVerifyType vtype);

        /// <summary>
        /// Genarates the verification code asynchronous.
        /// </summary>
        /// <param name="mType">Type of the m.</param>
        /// <param name="vType">Type of the v.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GenarateVerificationCodeAsync(DSCMediaType mType, DSCVerifyType vType, Guid userId);

        /// <summary>
        /// Gets the verification code asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;VerificationCode&gt;.</returns>
        Task<VerificationCode> GetVerificationCodeAsync(Guid id);

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        Task<User> GetUserAsync(Guid id);
        /// <summary>
        /// Gets the user by code asynchronous.
        /// </summary>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        Task<User> GetUserByCodeAsync(string serviceCode);
        /// <summary>
        /// Gets the management user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        Task<User> GetManagementUserAsync(Guid userId);
        /// <summary>
        /// Gets the management user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        Task<User> GetManagementUserAsync(User user);
        /// <summary>
        /// Gets the user by codes asynchronous.
        /// </summary>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>Task&lt;IEnumerable&lt;User&gt;&gt;.</returns>
        Task<IEnumerable<User>> GetUserByCodesAsync(string[] serviceCode);
        /// <summary>
        /// Gets the user properties asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetUserPropertiesAsync(Guid userId);
        /// <summary>
        /// Gets the property access asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetPropertyAccessAsync(Guid userId, Guid roleId);
        /// <summary>
        /// Gets the user properties asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="availableRoleVm">The available role vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetUserPropertiesAsync(User user, AvailableRoleVM availableRoleVm = null);
        /// <summary>
        /// Gets the user role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetUserRoleAsync(User user);

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="notifyVM">The notify vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> AddNotification(NotificationVM notifyVM);

    }
}
