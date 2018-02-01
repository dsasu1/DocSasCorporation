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
    public class StaffsController : BaseController
    {
        private readonly IStaffProvider _staffProvider;
        public StaffsController(IStaffProvider staffProvider)
        {
            _staffProvider = staffProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("SaveStaff")]
        public async Task<IActionResult> PostStaff([FromBody]StaffVM staff)
        {
            if (ModelState.IsValid)
            {
                var response = await _staffProvider.SaveStaffAsync(staff);

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
        [Route("DeleteStaff")]
        public async Task<IActionResult> DeleteStaff(Guid userId,Guid staffId)
        {
            if (ModelState.IsValid)
            {
                var response = await _staffProvider.DeleteStaffAsync(staffId, userId);

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
        [Route("DeleteStaffRole")]
        public async Task<IActionResult> DeleteStaffRole(Guid userId, Guid staffRoleId)
        {
            if (ModelState.IsValid)
            {
                var response = await _staffProvider.DeleteStaffRoleAsync(staffRoleId, userId);

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
        [Route("SaveRoleStaff")]
        public async Task<IActionResult> PostRoleStaff([FromBody]StaffRoleForm staff)
        {
            if (ModelState.IsValid)
            {
                var response = await _staffProvider.SaveRoleStaffAsync(staff);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StaffRoleVM>), (int)DSCHttpStatus.OK)]
        [Route("GetStaffs")]
        public async Task<IActionResult> GetStaffs(Guid userId)
        {
            if (ModelState.IsValid)
            {
                var response = await _staffProvider.GetStaffsAsync(userId);

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