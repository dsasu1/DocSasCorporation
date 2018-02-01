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
    public class NewsPostsController : BaseController
    {
        private readonly INewsPostProvider _messageProvider;

        public NewsPostsController(INewsPostProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EventVM), (int)DSCHttpStatus.OK)]
        [Route("SaveNewsPost")]
        public async Task<IActionResult> PostNews([FromBody]PostVM postVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _messageProvider.SaveNewsPostAsync(postVM);

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
        [Route("DeleteNewsPost")]
        public async Task<IActionResult> DeleteNews(Guid userId, Guid postId)
        {
            if (ModelState.IsValid)
            {
                var response = await _messageProvider.DeleteNewsPostAsync(userId, postId);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<PostVM>), (int)DSCHttpStatus.OK)]
        [Route("GetNewsPosts")]
        public async Task<IActionResult> GetNews(Guid userId, Guid propertyId)
        {
            if (ModelState.IsValid)
            {
                var response = await _messageProvider.GetNewsPostsAsync(userId, propertyId);

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