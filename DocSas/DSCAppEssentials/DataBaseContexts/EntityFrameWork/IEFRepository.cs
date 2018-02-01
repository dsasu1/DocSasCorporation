using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DSCAppEssentials.DataBaseContexts.EntityFrameWork
{
    /// <summary>
    /// Interface IEFRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEFRepository<T> where T : class
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="navigationPath">The navigation path.</param>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <param name="isOrderDesc">if set to <c>true</c> [is order desc].</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, string navigationPath = null, int numberOfRecords = 1000, bool isOrderDesc = false);
        /// <summary>
        /// Gets the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="navigationPath">The navigation path.</param>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <param name="isOrderDesc">if set to <c>true</c> [is order desc].</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, string navigationPath = null, int numberOfRecords = 1, bool isOrderDesc = false);
        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> AddAsync(T entity);
        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> AddAsync(IEnumerable<T> entity);
        /// <summary>
        /// Modifies the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> ModifyAsync(T entity);
        /// <summary>
        /// Modifies the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> ModifyAsync(IEnumerable<T> entity);
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> DeleteAsync(T entity);
        /// <summary>
        /// Gets the query asynchronous.
        /// </summary>
        /// <typeparam name="Tresult">The type of the tresult.</typeparam>
        /// <param name="storedProcName">Name of the stored proc.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Task&lt;IEnumerable&lt;Tresult&gt;&gt;.</returns>
        Task<IEnumerable<Tresult>> GetQueryAsync<Tresult>(string storedProcName, Dictionary<string, object> parameters) where Tresult : class;
    }
}
