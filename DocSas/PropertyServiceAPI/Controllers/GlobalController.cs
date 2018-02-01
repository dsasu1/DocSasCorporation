using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyService.Domain.ModelView;
using PropertyService.Domain.DataEntities;
using DSCAppEssentials.Helpers.DSCEnums;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PropertyServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class GlobalController : BaseController
    {

        private readonly IMiscProvider _miscProvider;

        public GlobalController(IMiscProvider miscProvider)
        {
            _miscProvider = miscProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SecurityQuestion>), (int)DSCHttpStatus.OK)]
        [Route("GetSecurityQuestions")]
        public async Task<IActionResult> GetSecurityQuestions()
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetSecurityQuestionsAsync();

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SecurityQuestion), (int)DSCHttpStatus.OK)]
        [Route("GetSecurityQuestion")]
        public async Task<IActionResult> GetSecurityQuestion(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetSecurityQuestionAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<Country>), (int)DSCHttpStatus.OK)]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetCountriesAsync();

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ZipCode>), (int)DSCHttpStatus.OK)]
        [Route("GetZipCodes")]
        public async Task<IActionResult> GetZipCodes()
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetZipCodesAsync();

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ZipCode>), (int)DSCHttpStatus.OK)]
        [Route("GetZipCodeByCode")]
        public async Task<IActionResult> GetZipCodeByCode(string code)
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetZipCodeByCodeAsync(code);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ZipCode), (int)DSCHttpStatus.OK)]
        [Route("GetZipCode")]
        public async Task<IActionResult> GetZipCode(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetZipCodeAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ZipCode), (int)DSCHttpStatus.OK)]
        [Route("GetZipLookup")]
        public async Task<IActionResult> GetZipLookup(string code)
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.LookUpZipAsync(code);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Country>), (int)DSCHttpStatus.OK)]
        [Route("GetCountry")]
        public async Task<IActionResult> GetCountry(Guid id)
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetCountryAsync(id);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LanguageVM>), (int)DSCHttpStatus.OK)]
        [Route("GetLanguages")]
        public async Task<IActionResult> GetLanguages()
        {
            if (ModelState.IsValid)
            {
                var response = await _miscProvider.GetLanguagesAsync();

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
