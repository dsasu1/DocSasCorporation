using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using DSCAppEssentials.Helpers.DSCEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface ICodeGeneratorProvider
    /// </summary>
    public interface ICodeGeneratorProvider
    {
        /// <summary>
        /// Generates the code asynchronous.
        /// </summary>
        /// <param name="cType">Type of the c.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GenerateCodeAsync(DSCCodeType cType);
    }
}
