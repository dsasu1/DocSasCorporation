using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using AutoMapper;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using PropertyService.Domain.Utilities.PSEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class ReviewProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IReviewProvider" />
    public class ReviewProvider : IReviewProvider
    {
        private readonly IPSRepository<PropertyReview> _reviewRepo;

        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewProvider"/> class.
        /// </summary>
        /// <param name="reviewRepo">The review repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public ReviewProvider(IPSRepository<PropertyReview> reviewRepo, IMapper mapper, IAppCommon appCommon)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _appCommon = appCommon;
        }

        /// <summary>
        /// save review as an asynchronous operation.
        /// </summary>
        /// <param name="reviewVM">The review vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveReviewAsync(ReviewVM reviewVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var review = _mapper.Map<ReviewVM, PropertyReview>(reviewVM);

            if (review != null)
            {
                await _reviewRepo.AddAsync(review);


                NotificationVM notification = new NotificationVM()
                {
                    UserId = review.UserId,
                    PropertyInformationId = review.PropertyInformationId,
                    NotificationResourceKey = NotificationResourceKeys.NotifyNewReviewAdded,
                    NotificationTypeEnum = PSNotificationType.Review.ToString(),
                    NotificationShowFor = PSNotificationShowFor.Property.ToString(),
                    NotificationTypeId = review.Id,
                    NotificationAdditionalInfo =  Utility.GetShortTen(review.Title)

                };

                await _appCommon.AddNotification(notification);
            }


            response.ResponseData = review;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// get reviews as an asynchronous operation.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetReviewsAsync(Guid propertyId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<ReviewVM> reviews = null;


           reviews = await _reviewRepo.GetQueryAsync<ReviewVM>("GetReviews", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId }
            });
                             
            response.ResponseData = reviews;
            return response;
        }
    }
}
