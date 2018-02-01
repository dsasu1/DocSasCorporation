using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyService.Domain.ModelView;
using DSCAppEssentials.Helpers.DSCEnums;
using PropertyService.Domain.DataEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropertyServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class PropertyController : BaseController
    {
        private readonly IPropertyProvider _propertyProvider;

        public PropertyController(IPropertyProvider propertyProvider)
        {
            _propertyProvider = propertyProvider;
        }


        [HttpGet]
        [ProducesResponseType(typeof(PropertyInformationVM), (int)DSCHttpStatus.OK)]
        [Route("GetProperty")]
        public async Task<IActionResult> GetProperty([FromQuery]Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.GetPropertyAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PropertyInformationVM), (int)DSCHttpStatus.OK)]
        [Route("GetPropertyByUrl")]
        public async Task<IActionResult> GetPropertyByUrl([FromQuery]string id)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.GetPropertyByFriendlyNameAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PropertyInformationVM), (int)DSCHttpStatus.OK)]
        [Route("GetPropertyById")]
        public async Task<IActionResult> GetPropertyById([FromQuery]Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.GetPropertyAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyType>), (int)DSCHttpStatus.OK)]
        [Route("GetPropertyTypes")]
        public async Task<IActionResult> GetPropertyTypes()
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.GetPropertyTypesAsync();

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyInformationVM>), (int)DSCHttpStatus.OK)]
        [Route("GetUserProperties")]
        public async Task<IActionResult> GetUserProperties([FromQuery] Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.GetUserPropertiesAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyAccess>), (int)DSCHttpStatus.OK)]
        [Route("GetPropertyAccess")]
        public async Task<IActionResult> GetPropertyAccess([FromQuery] Guid userId, [FromQuery] Guid roleId)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.GetPropertyAccessAsync(userId, roleId);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PropertyInformationVM), (int)DSCHttpStatus.OK)]
        [Route("SavePropertyInfo")]
        public async Task<IActionResult> PostPropertyInfo([FromBody]PropertyInformationVM propInfo)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.SavePropertyInfoAsync(propInfo);

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
        [Route("SaveTenantHome")]
        public async Task<IActionResult> PostTenantHome([FromBody]TenantUnitVM tenantVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.SaveTenantHomeAsync(tenantVM);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }



        [HttpPost]
        [ProducesResponseType(typeof(HourVM), (int)DSCHttpStatus.OK)]
        [Route("SaveHour")]
        public async Task<IActionResult> PostHour([FromBody]HourVM hour)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.SaveHourAsync(hour);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(HourVM), (int)DSCHttpStatus.OK)]
        [Route("GetPropertyHours")]
        public async Task<IActionResult> GetPropertyHour(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.GetPropetyHourAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("DeleteProperty")]
        public async Task<IActionResult> DeleteProperty(Guid propertyId, Guid userId)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.DeletePropertyAsync(propertyId,userId);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("DeletePropertyAccess")]
        public async Task<IActionResult> DeletePropertyAccess(Guid userId, Guid propertyId, Guid roleId)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.DeletePropertyAcessAsync(propertyId, roleId,userId);

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
        [Route("SaveRoleProperty")]
        public async Task<IActionResult> PostRoleProperty([FromBody]PropertyAccessForm prop)
        {
            if (ModelState.IsValid)
            {
                var response = await _propertyProvider.SaveRolePropertyAsync(prop);

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
