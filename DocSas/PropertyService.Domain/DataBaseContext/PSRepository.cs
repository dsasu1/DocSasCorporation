using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DSCAppEssentials.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using DSCAppEssentials.ErrorLogging;
using DSCAppEssentials.Resources;
using PropertyService.Domain.DataEntities;
using System.Linq;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.DataBaseContext
{
    /// <summary>
    /// Class PSRepository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="PropertyService.Domain.DataBaseContext.IPSRepository{T}" />
    public class PSRepository<T> : IPSRepository<T> where T : PSBaseEntity

    {

        private readonly PSDbContext _context;
        private DbSet<T> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="PSRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PSRepository(PSDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        /// <summary>
        /// add as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> AddAsync(T entity)
        {
            entity.NullCheck();

            // entity.Id = Guid.NewGuid(); 
            entity.AddedDateUtc = DateTime.UtcNow;
            entity.ModifiedDateUtc = DateTime.UtcNow;
            await _entities.AddAsync(entity);

           return  await SaveAsync();
        }

        /// <summary>
        /// add as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> AddAsync(IEnumerable<T> entity)
        {
            entity.NullCheck();

            await _entities.AddRangeAsync(entity);

            return await SaveAsync();
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            entity.NullCheck();

            _entities.Remove(entity);

           return await SaveAsync();
        }

        /// <summary>
        /// get as an asynchronous operation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="navigationPath">The navigation path.</param>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <param name="isOrderDesc">if set to <c>true</c> [is order desc].</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, string navigationPath = null, int numberOfRecords = 1000, bool isOrderDesc = false)
        {
            try
            {

                if (isOrderDesc)
                {
                    if (string.IsNullOrWhiteSpace(navigationPath))
                    {
                        if (filter == null)
                        {
                            return await _entities.OrderByDescending(x => x.AddedDateUtc).Take(numberOfRecords).ToListAsync();
                        }

                        return await _entities.Where(filter).OrderByDescending(x => x.AddedDateUtc).Take(numberOfRecords).ToListAsync();
                    }

                    if (filter == null)
                    {
                        return await _entities.OrderByDescending(x => x.AddedDateUtc).Take(numberOfRecords).Include(navigationPath).ToListAsync();
                    }

                    return await _entities.Where(filter).OrderByDescending(x => x.AddedDateUtc).Take(numberOfRecords).Include(navigationPath).ToListAsync();
                }

                if (string.IsNullOrWhiteSpace(navigationPath))
                {
                    if (filter == null)
                    {
                        return await _entities.Take(numberOfRecords).ToListAsync();
                    }

                    return await _entities.Where(filter).Take(numberOfRecords).ToListAsync();
                }

                if (filter == null)
                {
                    return await _entities.Take(numberOfRecords).Include(navigationPath).ToListAsync();
                }

                return await _entities.Where(filter).Take(numberOfRecords).Include(navigationPath).ToListAsync();


            }
            catch (Exception ex)
            {

                throw new DSCClientException(ExceptionMsg.ErrorOccuredTryAgain, ex);
            }
               
        }

        /// <summary>
        /// get single as an asynchronous operation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="navigationPath">The navigation path.</param>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <param name="isOrderDesc">if set to <c>true</c> [is order desc].</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, string navigationPath = null, int numberOfRecords = 1, bool isOrderDesc = false)
        {
            var item = await GetAsync(filter, navigationPath, numberOfRecords, isOrderDesc);

            return item.FirstOrDefault();

        }



        /// <summary>
        /// modify as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> ModifyAsync(T entity)
        {
            entity.NullCheck();
            entity.ModifiedDateUtc = DateTime.UtcNow;
            _entities.Update(entity);

            return await SaveAsync();
        }

        /// <summary>
        /// modify as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> ModifyAsync(IEnumerable<T> entity)
        {
            entity.NullCheck();
           
            _entities.UpdateRange(entity);

            return await SaveAsync();
        }

        public async Task<IEnumerable<Tresult>> GetQueryAsync<Tresult>(string storedProcName, Dictionary<string, object> parameters) where Tresult : class
        {
            try
            {
                StringBuilder sb = new StringBuilder(storedProcName);

                var procParameter = new object[parameters.Count];
                var count = 0;
                foreach (var item in parameters)
                {
                    procParameter[count] = item.Value;
                    if (count == 0)
                    {
                        sb.Append($" @p{count}");
                    }
                    else
                    {
                        sb.Append($" ,@p{count}");
                    }
                   
                    count++;
                }

                return await _context.Set<Tresult>().FromSql(sb.ToString(), parameters: procParameter).ToListAsync();
               
            }
            catch (Exception ex)
            {

                throw new DSCClientException(ExceptionMsg.ErrorOccuredTryAgain, ex);
            }
            
        }

        /// <summary>
        /// save as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <exception cref="DSCClientException">
        /// </exception>
        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw new DSCClientException(ExceptionMsg.ErrorOccuredTryAgain, ex);
            }
            catch (RetryLimitExceededException ex)
            {
                throw new DSCClientException(ExceptionMsg.ErrorOccuredTryAgain, ex);
            }
            catch(Exception ex)
            {

                throw new DSCClientException(ExceptionMsg.ErrorOccuredTryAgain, ex);
            }
        }

    }
}
