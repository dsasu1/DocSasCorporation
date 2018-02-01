using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IPropertyProvider
    /// </summary>
    public interface IPropertyProvider
    {
        /// <summary>
        /// Saves the property information asynchronous.
        /// </summary>
        /// <param name="propInfoVM">The property information vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SavePropertyInfoAsync(PropertyInformationVM propInfoVM);
        /// <summary>
        /// Gets the property asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetPropertyAsync(Guid id);
        /// <summary>
        /// Gets the property types asynchronous.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetPropertyTypesAsync();
        /// <summary>
        /// Saves the hour asynchronous.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveHourAsync(HourVM hour);
        /// <summary>
        /// Gets the propety hour asynchronous.
        /// </summary>
        /// <param name="propertiInfoId">The properti information identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetPropetyHourAsync(Guid propertiInfoId);
        /// <summary>
        /// Gets the property by friendly name asynchronous.
        /// </summary>
        /// <param name="friendlyUrl">The friendly URL.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetPropertyByFriendlyNameAsync(string friendlyUrl);
        /// <summary>
        /// Saves the role property asynchronous.
        /// </summary>
        /// <param name="propVM">The property vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveRolePropertyAsync(PropertyAccessForm propVM);
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
        /// Saves the tenant home asynchronous.
        /// </summary>
        /// <param name="tenantVM">The tenant vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> SaveTenantHomeAsync(TenantUnitVM tenantVM);

        /// <summary>
        /// Deletes the property asynchronous.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> DeletePropertyAsync(Guid propertyId, Guid userId);

        /// <summary>
        /// Deletes the property acess asynchronous.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> DeletePropertyAcessAsync(Guid propertyId, Guid roleId, Guid userId);

        /// <summary>
        /// Updates the property images asynchronous.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="propertyInfoId">The property information identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> UpdatePropertyImagesAsync(StorageResponse result, Guid propertyInfoId, Guid userId);
    }
}
