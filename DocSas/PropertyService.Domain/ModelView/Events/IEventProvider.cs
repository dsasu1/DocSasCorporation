using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Interface IEventProvider
    /// </summary>
    public interface IEventProvider
    {
        
        Task<DSCResponse> SaveEvent(EventVM eventVM);

        Task<DSCResponse> GetEvents(Guid userId, Guid propertyId);
    }
}
