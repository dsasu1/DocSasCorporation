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
    public class ServiceRequestsController : BaseController
    {

        private readonly IServiceRequestProvider _serviceRequest;

        public ServiceRequestsController(IServiceRequestProvider serviceRequest)
        {
            _serviceRequest = serviceRequest;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceRequestVM), (int)DSCHttpStatus.OK)]
        [Route("SaveServiceRequest")]
        public async Task<IActionResult> PostServiceRequest([FromBody]ServiceRequestVM serviceRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceRequest.SaveServiceRequestAsync(serviceRequest);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("SaveRequestStatus")]
        public async Task<IActionResult> SaveRequestStatus([FromBody]ServiceRequestStatusVM serviceRequestStatus)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceRequest.SaveServiceRequestStatusAsync(serviceRequestStatus);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<ServiceRequestVM>), (int)DSCHttpStatus.OK)]
        [Route("GetServiceRequests")]
        public async Task<IActionResult> GetServiceRequests(Guid userId, Guid propertyId)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceRequest.GetServiceRequestsAsync(userId, propertyId);

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