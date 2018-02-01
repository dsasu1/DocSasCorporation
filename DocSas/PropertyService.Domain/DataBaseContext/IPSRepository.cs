using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PropertyService.Domain.DataEntities;
using DSCAppEssentials.DataBaseContexts.EntityFrameWork;

namespace PropertyService.Domain.DataBaseContext
{
    /// <summary>
    /// Interface IPSRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="DSCAppEssentials.DataBaseContexts.EntityFrameWork.IEFRepository{T}" />
    public interface IPSRepository<T> : IEFRepository<T> where T : PSBaseEntity

    {
        
    }
}
