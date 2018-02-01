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
    public class ReviewsController : BaseController
    {
        private readonly IReviewProvider _reviewProvider;

        public ReviewsController(IReviewProvider reviewProvider)
        {
            _reviewProvider = reviewProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewVM), (int)DSCHttpStatus.OK)]
        [Route("SaveReview")]
        public async Task<IActionResult> PostReview([FromBody]ReviewVM review)
        {
            if (ModelState.IsValid)
            {
                var response = await _reviewProvider.SaveReviewAsync(review);

                if (response.IsSuccess)
                {
                    return Ok(response.ResponseData);
                }

                return BadRequest(response.ErrorMessage);
            }

            return BadRequest(ModelState);
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<ReviewVM>), (int)DSCHttpStatus.OK)]
        [Route("GetReviews")]
        public async Task<IActionResult> GetReviews(Guid propertyId)
        {
            if (ModelState.IsValid)
            {
                var response = await _reviewProvider.GetReviewsAsync(propertyId);

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