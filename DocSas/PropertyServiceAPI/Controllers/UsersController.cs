using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyService.Domain.ModelView;
using DSCAppEssentials.Helpers.DSCEnums;
using PropertyService.Domain.ModelView.External;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropertyServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {

        private readonly IUserProvider _userProvider;
       
        public UsersController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserTypeVM>), (int)DSCHttpStatus.OK)]
        [Route("GetUserTypes")]
        public async Task<IActionResult> GetUserTypes()
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.GetUserTypesAsync();

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
            
        }

 
        [HttpGet]
        [ProducesResponseType(typeof(UserTypeVM), (int)DSCHttpStatus.OK)]
        [Route("GetUserType")]
        public async Task<IActionResult> GetUserType([FromQuery]Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.GetUserTypeAsync(id);

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
        [Route("RegisterUser")]
        public async Task<IActionResult> PostUser([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {

                var response = await _userProvider.RegisterUserAsync(user);

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
        [Route("VerifyUserSession")]
        public async Task<IActionResult> PostUserVerifySession([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {

                var response = await _userProvider.VerifyUserSessionAsync(user);

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
        [Route("DeactivateAccount")]
        public async Task<IActionResult> PostDeactivateAccount([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {

               var response = await _userProvider.DeactivateAccountAsync(user);

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
        [Route("HasManagementRights")]
        public async Task<IActionResult> PostHasManagenebtRights([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {

                var response = await _userProvider.HasManagementRightsAsync(user);

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
        [Route("ModifyUser")]
        public async Task<IActionResult> PutUserChange([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {

                var response = await _userProvider.SaveUserAsync(user);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);


            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserSessionVM),(int)DSCHttpStatus.OK)]
        [Route("LoginUser")]
        public async Task<IActionResult> PostVerifyUser([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {

                var response = await _userProvider.LoginUserAsync(user);

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
        [Route("ConfirmUser")]
        public async Task<IActionResult> PutConfirmUser([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.ConfirmUserAsync(user);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }
                
                return BadRequest(response.ErrorMessage);
                
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserVM), (int)DSCHttpStatus.OK)]
        [Route("RetrieveAccount")]
        public async Task<IActionResult> PostRetrieveAccount([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.RetrieveAccountAsync(user);

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
        [Route("VerifySecurityAnswer")]
        public async Task<IActionResult> PostVerifySecurityAnswer([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.VerifySecurityAnswerAsync(user);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RecaptchaResponse), (int)DSCHttpStatus.OK)]
        [Route("VerifyRecaptcha")]
        public async Task<IActionResult> PostVerifyRecaptcha([FromBody] RecaptchaRequest recaptchaReq)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.VerifyRecaptchaAsync(recaptchaReq);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [ProducesResponseType(typeof(UserVM), (int)DSCHttpStatus.OK)]
        [Route("VerifyChangePassword")]
        public async Task<IActionResult> PostVerifyChangePassword([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.VerifyChangePasswordAsync(user);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserVM), (int)DSCHttpStatus.OK)]
        [Route("SaveNewPassword")]
        public async Task<IActionResult> PostSaveNewPassword([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProvider.SaveNewPasswordAsync(user);

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
