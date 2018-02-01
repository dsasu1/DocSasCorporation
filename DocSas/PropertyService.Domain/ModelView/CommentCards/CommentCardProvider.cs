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
    /// Class CommentCardProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.ICommentCardProvider" />
    public class CommentCardProvider : ICommentCardProvider
    {
        private readonly IPSRepository<CommentCard> _commentCardRepo;
       
        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentCardProvider"/> class.
        /// </summary>
        /// <param name="commentCardRepo">The comment card repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public CommentCardProvider(IPSRepository<CommentCard> commentCardRepo, IMapper mapper,  IAppCommon appCommon)
        {
            _commentCardRepo = commentCardRepo;
            _mapper = mapper;
            _appCommon = appCommon;
        }
        /// <summary>
        /// save comment card as an asynchronous operation.
        /// </summary>
        /// <param name="commentCard">The comment card.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveCommentCardAsync(CommentCardVM commentCard)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var comment = _mapper.Map<CommentCardVM, CommentCard>(commentCard);

            if (comment != null)
            {
                await _commentCardRepo.AddAsync(comment);

                NotificationVM notification = new NotificationVM()
                {
                    UserId = comment.IsAnonymous ? Guid.Empty : comment.UserId,
                    PropertyInformationId = comment.PropertyInformationId,
                    NotificationResourceKey = NotificationResourceKeys.NotifyNewCommentCardReceieved,
                    NotificationTypeEnum = PSNotificationType.CommentCard.ToString(),
                    NotificationShowFor = PSNotificationShowFor.Property.ToString(),
                    NotificationTypeId = comment.Id,
                    NotificationAdditionalInfo = comment.IsAnonymous ? NotificationResourceKeys.NotifyAnonymousComment : Utility.GetShortTen(comment.Comment)

                };

                await _appCommon.AddNotification(notification);

            }


            response.ResponseData = comment;
            response.ErrorMessage = msg;
            return response;
        }


        /// <summary>
        /// get comment cards as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetCommentCardsAsync(Guid userId, Guid propertyId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<CommentCardVM> comms = null;

            var user = await _appCommon.GetUserAsync(userId);

            if(user != null)
            {
                if (user.UserTypeEnum == PSUserType.ManagementCompany.ToString())
                {
                    comms = await _commentCardRepo.GetQueryAsync<CommentCardVM>("GetCommentCards", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId }
                    });
                }
                else if(user.UserTypeEnum == PSUserType.ManagementPersonnel.ToString())
                {
                    comms = await _commentCardRepo.GetQueryAsync<CommentCardVM>("GetCommentCards", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId }
                    });
                }
                else
                {
                    comms = await _commentCardRepo.GetQueryAsync<CommentCardVM>("GetCommentCards", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId },
                        {"userId",userId }
                    });
                }
            }


            response.ResponseData = comms;
            return response;
        }

       

    }
}
