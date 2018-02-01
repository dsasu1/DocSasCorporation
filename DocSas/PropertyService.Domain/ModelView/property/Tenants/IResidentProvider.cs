using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IResidentProvider
    /// </summary>
    public interface IResidentProvider
    {
        /// <summary>
        /// Gets the residents asynchronous.
        /// </summary>
        /// <param name="propertyInfoId">The property information identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetResidentsAsync(Guid propertyInfoId, Guid userId);
        /// <summary>
        /// Changes the resident status asynchronous.
        /// </summary>
        /// <param name="residencyStatus">The residency status.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> ChangeResidentStatusAsync(ResidencyStatusVM residencyStatus);

    }
}
