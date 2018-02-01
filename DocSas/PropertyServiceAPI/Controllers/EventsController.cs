using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyService.Domain.ModelView;
using DSCAppEssentials.Helpers.DSCEnums;
using PropertyService.Domain.DataEntities;

namespace PropertyServiceAPI.Controllers
{

    [Route("api/[Controller]")]
    public class EventsController : BaseController
    {

        private readonly IEventProvider _eventProvider;

        public EventsController(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EventVM), (int)DSCHttpStatus.OK)]
        [Route("SavePropertyEvent")]
        public async Task<IActionResult> PostEvents([FromBody]EventVM eventVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _eventProvider.SaveEvent(eventVM);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<EventVM>), (int)DSCHttpStatus.OK)]
        [Route("GetPropertyEvents")]
        public async Task<IActionResult> GetEvents(Guid userId, Guid propertyId)
        {
            if (ModelState.IsValid)
            {
                var response = await _eventProvider.GetEvents(userId, propertyId);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }
    }
}