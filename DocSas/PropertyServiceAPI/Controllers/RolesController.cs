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
    public class RolesController : BaseController
    {
        private readonly IRoleProvider _roleProvider;
        public RolesController(IRoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("SaveAvailableRole")]
        public async Task<IActionResult> PostAvailableRole([FromBody]AvailableRoleVM role)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleProvider.SaveAvailableRoleAsync(role);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

    
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AvailableRoleVM>), (int)DSCHttpStatus.OK)]
        [Route("GetAvailableRole")]
        public async Task<IActionResult> GetAvailableRole(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleProvider.GetAvailableRoleAsync(id);

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
        [Route("DeleteAvailableRole")]
        public async Task<IActionResult> DeleteAvailableRole(Guid userId, Guid roleId)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleProvider.DeleteAvailableRoleAsync(roleId, userId);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AvailableRoleVM[]>), (int)DSCHttpStatus.OK)]
        [Route("GetAvailableRoles")]
        public async Task<IActionResult> GetAvailableRoles(Guid userId)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleProvider.GetAvailableRolesAsync(userId);

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