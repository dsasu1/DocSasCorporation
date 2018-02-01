using System;
using System.Collections.Generic;
using System.Text;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using System.Linq;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using AutoMapper;
using PropertyService.Domain.Utilities.PSEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class NotificationProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.INotificationProvider" />
    public class NotificationProvider : INotificationProvider
    {
        private readonly IPSRepository<Notification> _notificationRepo;
        private readonly IPSRepository<NotificationViewTrack> _notificationTrackRepo;
        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationProvider"/> class.
        /// </summary>
        /// <param name="notificationRepo">The notification repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public NotificationProvider(IPSRepository<Notification> notificationRepo, IPSRepository<NotificationViewTrack> notificationTrackRepo,  IMapper mapper, IAppCommon appCommon)
        {
            _notificationRepo = notificationRepo;
            _mapper = mapper;
            _appCommon = appCommon;
            _notificationTrackRepo = notificationTrackRepo;
        }

        /// <summary>
        /// get notifications as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="isMainPageView">if set to <c>true</c> [is main page view].</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetNotificationsAsync(Guid userId, Guid propertyId, bool isMainPageView = false)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            NotificationMasterVM masterNotification = null;
            IEnumerable<NotificationVM> notifications = null;
            var lastViewedDate = DateTime.UtcNow.AddYears(-10);

            var user = await _appCommon.GetUserAsync(userId);
            var isManager = false;

            if (user != null)
            {
             
                if (user.UserTypeEnum.Equals(PSUserType.ManagementPersonnel.ToString()) || user.UserTypeEnum.Equals(PSUserType.ManagementCompany.ToString()))
                {
                    var roleResponse = await _appCommon.GetUserRoleAsync(user);

                    if (roleResponse.IsSuccess)
                    {
                        var role = roleResponse.ResponseData as AvailableRoleVM;

                        if (role != null)
                        {
                            isManager = role.HasManagementRights;
                        }
                    }
                }

                notifications = await _notificationRepo.GetQueryAsync<NotificationVM>("GetNotifications", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId },
                        {"UserId",userId },
                        { "UserType",user.UserTypeEnum },
                        { "IsManager",isManager }
                     });
                var track = await GetNotificationTracksAsync(userId, propertyId);

                if (track != null)
                {
                    lastViewedDate = track.ModifiedDateUtc;
                }

                if (isMainPageView)
                {
                    await SaveNotificationTracksAsync(track, userId, propertyId);
                }


                if (notifications.Any())
                {
                    masterNotification = new NotificationMasterVM()
                    {
                        LastViewDateUTC = lastViewedDate,
                        NonViewedCount = notifications.Count(x => x.AddedDateUtc > lastViewedDate),
                        NotificationVMS = notifications
                    };
                }

            }


            response.ResponseData = masterNotification;
            return response;
        }

        /// <summary>
        /// Gets the notification tracks asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;NotificationViewTrack&gt;.</returns>
        private async Task<NotificationViewTrack> GetNotificationTracksAsync(Guid userId, Guid propertyId)
        {
            return await _notificationTrackRepo.GetSingleAsync(x => x.UserId == userId && x.PropertyInformationId.Value == propertyId);
        }

        private async Task<bool> SaveNotificationTracksAsync(NotificationViewTrack track, Guid userId, Guid propertyInforId)
        {
            var isSuccess = false;
            if (track != null)
            {
                track.ViewCount++;
                isSuccess = await _notificationTrackRepo.ModifyAsync(track);
            }
            else
            {
                track = new NotificationViewTrack()
                {
                    ViewCount = 1,
                    UserId = userId,
                    PropertyInformationId = propertyInforId
                };

                isSuccess = await _notificationTrackRepo.AddAsync(track);
            }

            return isSuccess;
        }

    }
}
