using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IMiscProvider
    /// </summary>
    public interface IMiscProvider
    {
        /// <summary>
        /// Gets the security questions asynchronous.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetSecurityQuestionsAsync();
        /// <summary>
        /// Gets the security question asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetSecurityQuestionAsync(Guid id);
        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetCountriesAsync();
        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetLanguagesAsync();
        /// <summary>
        /// Gets the country asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetCountryAsync(Guid id);
        /// <summary>
        /// Gets the language asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetLanguageAsync(Guid id);
        /// <summary>
        /// Gets the zip codes asynchronous.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetZipCodesAsync();
        /// <summary>
        /// Gets the zip code asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetZipCodeAsync(Guid id);
        /// <summary>
        /// Gets the zip code by code asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> GetZipCodeByCodeAsync(string code);
        /// <summary>
        /// Looks up zip asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        Task<DSCResponse> LookUpZipAsync(string code);

    }
}
