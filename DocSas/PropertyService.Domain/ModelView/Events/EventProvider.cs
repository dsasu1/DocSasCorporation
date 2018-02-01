using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using AutoMapper;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using PropertyService.Domain.Utilities.PSEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class EventProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IEventProvider" />
    public class EventProvider : IEventProvider
    {
        private readonly IPSRepository<PropertyEvent> _eventRepo;

        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;
        /// <summary>
        /// Initializes a new instance of the <see cref="EventProvider"/> class.
        /// </summary>
        /// <param name="eventRepo">The event repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public EventProvider(IPSRepository<PropertyEvent> eventRepo, IMapper mapper, IAppCommon appCommon)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
            _appCommon = appCommon;
        }
        public async Task<DSCResponse> SaveEvent(EventVM eventVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var events = _mapper.Map<EventVM, PropertyEvent>(eventVM);

            if (events != null)
            {
                await _eventRepo.AddAsync(events);
            }


            response.ResponseData = events;
            response.ErrorMessage = msg;
            return response;
        }

        public async Task<DSCResponse> GetEvents(Guid userId, Guid propertyId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<EventVM> events = null;

            var user = await _appCommon.GetUserAsync(userId);

            if (user != null)
            {

                events = await _eventRepo.GetQueryAsync<EventVM>("GetEvents", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId }
                    });

            }


            response.ResponseData = events;
            return response;
        }
    }
}
