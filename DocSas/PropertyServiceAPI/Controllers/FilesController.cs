using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyService.Domain.ModelView;
using DSCAppEssentials.Helpers.DSCEnums;

namespace PropertyServiceAPI.Controllers
{

    [Route("api/[controller]")]
    public class FilesController : BaseController
    {
        private readonly IMediaProvider _mediaProvider;
        public FilesController(IMediaProvider mediaProvider)
        {
            _mediaProvider = mediaProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)DSCHttpStatus.OK)]
        [Route("FileUpload")]
        public async Task<IActionResult> PostFileUpload([FromForm] UploadOptions options)
        {
            if (ModelState.IsValid)
            {

                var response = await _mediaProvider.UploadDataAsync(options);

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