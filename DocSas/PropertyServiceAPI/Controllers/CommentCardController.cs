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
    public class CommentCardsController : BaseController
    {

        private readonly ICommentCardProvider _commentCardProvider;

        public CommentCardsController(ICommentCardProvider commentCardProvider)
        {
            _commentCardProvider = commentCardProvider;
        }


        [HttpPost]
        [ProducesResponseType(typeof(CommentCardVM), (int)DSCHttpStatus.OK)]
        [Route("SaveCommendCard")]
        public async Task<IActionResult> PostCommendCard([FromBody]CommentCardVM commentCard)
        {
            if (ModelState.IsValid)
            {
                var response = await _commentCardProvider.SaveCommentCardAsync(commentCard);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);

            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CommentCardVM>), (int)DSCHttpStatus.OK)]
        [Route("GetCommendCards")]
        public async Task<IActionResult> GetCommendCards(Guid userId, Guid propertyId)
        {
            if (ModelState.IsValid)
            {
                var response = await _commentCardProvider.GetCommentCardsAsync(userId,propertyId);

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