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
    /// Class ResidentProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IResidentProvider" />
    public class ResidentProvider : IResidentProvider
    {
        private readonly IAppCommon _appCommon;
        private readonly IPSRepository<TenantUnit> _tenantUnitRepo;
        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentProvider"/> class.
        /// </summary>
        /// <param name="appCommon">The application common.</param>
        /// <param name="tenantUnitRepo">The tenant unit repo.</param>
        public ResidentProvider(IAppCommon appCommon, IPSRepository<TenantUnit> tenantUnitRepo)
        {
            _appCommon = appCommon;
            _tenantUnitRepo = tenantUnitRepo;
        }

        /// <summary>
        /// get residents as an asynchronous operation.
        /// </summary>
        /// <param name="propertyInfoId">The property information identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetResidentsAsync(Guid propertyInfoId, Guid userId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<ResidentsVM> residents = null;

            var user = await _appCommon.GetUserAsync(userId);

            if (user != null)
            {
                if (user.UserTypeEnum == PSUserType.ManagementCompany.ToString())
                {
                    residents = await _tenantUnitRepo.GetQueryAsync<ResidentsVM>("GetResidents", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyInfoId }
                    });
                }
                else if (user.UserTypeEnum == PSUserType.ManagementPersonnel.ToString())
                {
                    residents = await _tenantUnitRepo.GetQueryAsync<ResidentsVM>("GetResidents", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyInfoId }
                    });
                }
                else
                {
                    residents = await _tenantUnitRepo.GetQueryAsync<ResidentsVM>("GetResidents", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyInfoId },
                        {"userId",userId }
                    });
                }
            }


            response.ResponseData = residents;

            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// change resident status as an asynchronous operation.
        /// </summary>
        /// <param name="residencyStatus">The residency status.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> ChangeResidentStatusAsync(ResidencyStatusVM residencyStatus)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var isSuccess = false;

            var tenant = await _tenantUnitRepo.GetSingleAsync(x => x.Id == residencyStatus.Id);

            if (tenant != null && tenant.PropertyInformationId == residencyStatus.PropertyInformationId && tenant.UserId == residencyStatus.ResidentUserId)
            {

                NotificationVM notification = new NotificationVM()
                {
                    UserId = tenant.UserId,
                    PropertyInformationId = tenant.PropertyInformationId,
                    NotificationTypeEnum = PSNotificationType.Residents.ToString(),
                    NotificationShowFor = tenant.UserId == residencyStatus.ChangerUserId ? PSNotificationShowFor.Property.ToString() : PSNotificationShowFor.User.ToString(),
                    NotificationTypeId = tenant.Id,
                    NotificationAdditionalInfo = tenant.UnitAddress

                };

                if (tenant.UserId != residencyStatus.ChangerUserId)
                {
                    notification.NotifeeUserId = tenant.UserId;
                }

                if (residencyStatus.StatusType == PSResidencyChangeType.MoveOut)
                {
                    
                    notification.NotificationResourceKey = NotificationResourceKeys.NotifyInitiatedAMoveout;
                    tenant.IsMovedOut = residencyStatus.ChangeValue;
                }
                else if (residencyStatus.StatusType == PSResidencyChangeType.Approval && residencyStatus.ChangeValue)
                {
                    notification.NotificationResourceKey = NotificationResourceKeys.NotifyApprovedResidency;
                    tenant.IsApproved = residencyStatus.ChangeValue;
                }
                else if (residencyStatus.StatusType == PSResidencyChangeType.Cancel || (residencyStatus.StatusType == PSResidencyChangeType.Approval && !residencyStatus.ChangeValue))
                {
                    if (residencyStatus.StatusType == PSResidencyChangeType.Cancel)
                    {
                        notification.NotificationResourceKey = NotificationResourceKeys.NotifyCancelledResidency;
                    }
                    else
                    {
                        notification.NotificationResourceKey = NotificationResourceKeys.NotifyDeniedResidency;
                    }
                    isSuccess = await _tenantUnitRepo.DeleteAsync(tenant);
                }


                if (residencyStatus.StatusType == PSResidencyChangeType.MoveOut || (residencyStatus.StatusType == PSResidencyChangeType.Approval && residencyStatus.ChangeValue))
                {
                    isSuccess = await _tenantUnitRepo.ModifyAsync(tenant);
                }

                await _appCommon.AddNotification(notification);

            }

            response.ResponseData = isSuccess;
            return response;
        }

    }
}
