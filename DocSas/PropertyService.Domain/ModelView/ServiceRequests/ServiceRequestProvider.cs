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
    public class ServiceRequestProvider : IServiceRequestProvider
    {

        private readonly IPSRepository<ServiceRequest> _serviceRequestRepo;
        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRequestProvider"/> class.
        /// </summary>
        /// <param name="serviceRequestRepo">The service request repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public ServiceRequestProvider(IPSRepository<ServiceRequest> serviceRequestRepo, IMapper mapper, IAppCommon appCommon)
        {
            _serviceRequestRepo = serviceRequestRepo;
            _mapper = mapper;
            _appCommon = appCommon;
        }


        /// <summary>
        /// save service request as an asynchronous operation.
        /// </summary>
        /// <param name="serviceRequest">The service request.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveServiceRequestAsync(ServiceRequestVM serviceRequest)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var srRequest = _mapper.Map<ServiceRequestVM, ServiceRequest>(serviceRequest);

            if (srRequest != null)
            {
                srRequest.IsValid = true;
                await _serviceRequestRepo.AddAsync(srRequest);

                NotificationVM notification = new NotificationVM()
                {
                    UserId = serviceRequest.UserId,
                    PropertyInformationId = serviceRequest.PropertyInformationId,
                     NotificationResourceKey =  NotificationResourceKeys.NotifyNewServiceRequested,
                     NotificationTypeEnum = PSNotificationType.ServiceRequest.ToString(),
                     NotificationShowFor = PSNotificationShowFor.Property.ToString(),
                     NotificationTypeId = srRequest.Id,
                     NotificationAdditionalInfo = string.Concat(srRequest.TenantUnitAddress, " - ", srRequest.Title)
                     
                };

                await _appCommon.AddNotification(notification);
            }


            response.ResponseData = srRequest;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// save service request status as an asynchronous operation.
        /// </summary>
        /// <param name="serviceRequestStatus">The service request status.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveServiceRequestStatusAsync(ServiceRequestStatusVM serviceRequestStatus)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var isSuccess = false;
            var isAllow = false;

            var serviceRequest = await _serviceRequestRepo.GetSingleAsync(x => x.Id == serviceRequestStatus.Id);
            if (serviceRequest != null)
            {
                if (serviceRequest.UserId == serviceRequestStatus.UserId)
                {
                    isAllow = true;
                }
                else
                {
                    var management = await _appCommon.GetManagementUserAsync(serviceRequestStatus.UserId);
                    isAllow = management != null;
                  
                }
            }


            if (isAllow)
            {
                serviceRequest.RequestStatusKey = serviceRequestStatus.StatusKey;
                isSuccess = await _serviceRequestRepo.ModifyAsync(serviceRequest);


                NotificationVM notification = new NotificationVM()
                {
                    UserId = serviceRequest.UserId,
                    PropertyInformationId = serviceRequest.PropertyInformationId,
                    NotificationResourceKey = NotificationResourceKeys.NotifyServiceStatusChanged + serviceRequestStatus.StatusKey,
                    NotificationTypeEnum = PSNotificationType.ServiceRequest.ToString(),
                    NotificationShowFor = serviceRequest.UserId == serviceRequestStatus.UserId ? PSNotificationShowFor.Property.ToString() : PSNotificationShowFor.User.ToString(),
                    NotificationTypeId = serviceRequest.Id,
                    NotifeeUserId = serviceRequest.UserId == serviceRequestStatus.UserId ? Guid.Empty : serviceRequest.UserId,
                    NotificationAdditionalInfo = string.Concat(serviceRequest.TenantUnitAddress, " - ", serviceRequest.Title)

                };

                await _appCommon.AddNotification(notification);
            }


            response.ResponseData = isSuccess;
            response.ErrorMessage = msg;
            return response;
        }


        /// <summary>
        /// get service requests as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetServiceRequestsAsync(Guid userId, Guid propertyId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<ServiceRequestVM> srs = null;

            var user = await _appCommon.GetUserAsync(userId);

            if (user != null)
            {
                if (user.UserTypeEnum == PSUserType.ManagementCompany.ToString())
                {
                    srs = await _serviceRequestRepo.GetQueryAsync<ServiceRequestVM>("GetServiceRequests", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId }
                    });
                }
                else if (user.UserTypeEnum == PSUserType.ManagementPersonnel.ToString())
                {
                    srs = await _serviceRequestRepo.GetQueryAsync<ServiceRequestVM>("GetServiceRequests", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId }
                    });
                }
                else
                {
                    srs = await _serviceRequestRepo.GetQueryAsync<ServiceRequestVM>("GetServiceRequests", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId },
                        {"userId",userId }
                    });
                }
            }


            response.ResponseData = srs;
            return response;
        }
    }
}
