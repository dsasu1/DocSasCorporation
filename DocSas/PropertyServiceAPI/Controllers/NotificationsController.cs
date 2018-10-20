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
    public class NotificationsController : BaseController
    {

        private readonly INotificationProvider _notifyProvider;

        public NotificationsController(INotificationProvider notificationProvider)
        {
            _notifyProvider = notificationProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(NotificationMasterVM), (int)DSCHttpStatus.OK)]
        [Route("GetNotifications")]
        public async Task<IActionResult> GetNotifications(Guid userId, Guid propertyId, bool isMainPageView)
        {
            if (ModelState.IsValid)
            {
                var response = await _notifyProvider.GetNotificationsAsync(userId, propertyId, isMainPageView);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("PostNotificationSubscription")]
        public async Task<IActionResult> PostNotificationSubscription(NotificationPushVM notificationPushVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _notifyProvider.SaveNotificationSubAsync(notificationPushVM);

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