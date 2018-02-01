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
  
    [Route("api/[controller]")]
    public class ResidentsController : BaseController
    {
        private readonly IResidentProvider _residentProvider;
        public ResidentsController(IResidentProvider residentProvider)
        {
            _residentProvider = residentProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResidentsVM>), (int)DSCHttpStatus.OK)]
        [Route("GetResidents")]
        public async Task<IActionResult> GetResidents(Guid propertyInformationId, Guid userId)
        {
            if (ModelState.IsValid)
            {
                var response = await _residentProvider.GetResidentsAsync(propertyInformationId, userId);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("SaveResidencyStatus")]
        public async Task<IActionResult> PutResidentStatus([FromBody] ResidencyStatusVM residencyStatusVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _residentProvider.ChangeResidentStatusAsync(residencyStatusVM);

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