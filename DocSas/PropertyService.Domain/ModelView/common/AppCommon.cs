using System;
using System.Collections.Generic;
using System.Text;
using DSCAppEssentials.Extensions;
using DSCAppEssentials.Helpers.DSCEnums;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using System.Linq;
using System.Threading.Tasks;
using PropertyService.Domain.Managers;
using DSCAppEssentials.Helpers;
using AutoMapper;
using DSCAppEssentials.Abstract;
using DSCAppEssentials.Managers;
using PropertyService.Domain.Utilities.PSEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class AppCommon.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IAppCommon" />
    public class AppCommon : IAppCommon
    {
        private readonly IPSRepository<User> _userRepo;
        private readonly IPSRepository<VerificationCode> _verifyCodeRepo;
        private readonly IPSRepository<PropertyAccess> _propAccessRepo;
        private readonly IPSRepository<AvailableRole> _availableRole;
        private readonly IPSRepository<StaffRole> _staffRoleRepo;
        private readonly IPSRepository<TenantUnit> _tenantRepo;
        private readonly IPSRepository<Notification> _notifyRepo;
        private readonly IMapper _mapper;
        private readonly IPSRepository<PropertyInformation> _propertyRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppCommon"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="verifyCodeRepo">The verify code repo.</param>
        /// <param name="propertyRepo">The property repo.</param>
        /// <param name="availableRole">The available role.</param>
        /// <param name="propAccessRepo">The property access repo.</param>
        /// <param name="staffRoleRepo">The staff role repo.</param>
        /// <param name="tenantRepo">The tenant repo.</param>
        /// <param name="notifyRepo">The notify repo.</param>
        public AppCommon(IMapper mapper,IPSRepository<User> userRepo, IPSRepository<VerificationCode> verifyCodeRepo, IPSRepository<PropertyInformation> propertyRepo, IPSRepository<AvailableRole> availableRole, IPSRepository<PropertyAccess> propAccessRepo, IPSRepository<StaffRole> staffRoleRepo, IPSRepository<TenantUnit> tenantRepo, IPSRepository<Notification> notifyRepo)
        {
            _userRepo = userRepo;
            _verifyCodeRepo = verifyCodeRepo;
            _mapper = mapper;
            _propertyRepo = propertyRepo;
            _availableRole = availableRole;
            _propAccessRepo = propAccessRepo;
            _staffRoleRepo = staffRoleRepo;
            _tenantRepo = tenantRepo;
            _notifyRepo = notifyRepo;
        }

        /// <summary>
        /// Determines whether [is user account valid] [the specified user entity].
        /// </summary>
        /// <param name="userEntity">The user entity.</param>
        /// <returns>DSCResponse.</returns>
        public DSCResponse IsUserAccountValid(User userEntity)
        {
            var response = new DSCResponse();

            var msg = new Dictionary<string, string>();

            var isSuccess = false;

            if (userEntity == null)
            {
                msg.Add("InvalidAccount", "Invalid Account");
            }
            else if (!userEntity.IsActive.HasValue)
            {
                msg.Add("AccountNotConfirmed", "Confirm your account");
            }
            else if (userEntity.IsLockedOut)
            {
                msg.Add("LockedOut", "Account Locked");
            }
            else if (userEntity.IsActive.Value && !userEntity.IsBanned && !userEntity.IsLockedOut)
            {
                isSuccess = true;
            }
            else if (!userEntity.IsActive.Value && !userEntity.IsBanned && !userEntity.IsLockedOut)
            {
                isSuccess = true;
            }
            else
            {
                msg.Add("InvalidAccount", "Invalid Account");
            }

            response.ErrorMessage = msg;
            response.ResponseData = isSuccess;
            return response;
        }

        /// <summary>
        /// is user account valid as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> IsUserAccountValidAsync(Guid userId)
        {
            var response = new DSCResponse();
            var user = await _userRepo.GetSingleAsync(x => x.Id == userId);
            response = IsUserAccountValid(user);

            return response;
        }

        /// <summary>
        /// verify current password as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> VerifyCurrentPasswordAsync(UserVM user)
        {
            var msg = new Dictionary<string, string>();

            var response = new DSCResponse();

            var userEntity = await GetUserAsync(user.Id);

            var userVerify = IsUserAccountValid(userEntity);

            if (userVerify.IsSuccess)
            {
                var isValid = SecurityManager.VerifyPassword(user.Password, userEntity.Password);

                if (isValid)
                {
                    response.ResponseData = userEntity;
                }
                else
                {
                    msg.Add("InvalidPassword", "Invalid Password");
                }
            }
            else
            {
                msg = userVerify.ErrorMessage;
            }

            response.ErrorMessage = msg;
            return response;

        }

        /// <summary>
        /// is veriy code as an asynchronous operation.
        /// </summary>
        /// <param name="verifyCodeId">The verify code identifier.</param>
        /// <param name="vtype">The vtype.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> IsVeriyCodeAsync(Guid verifyCodeId, DSCVerifyType vtype)
        {
            var response = new DSCResponse();

            var msg = new Dictionary<string, string>();

            var entityVeriCode = await _verifyCodeRepo.GetSingleAsync(x => x.Id.Equals(verifyCodeId));

            if (entityVeriCode == null)
            {
                msg.Add("SomethingWrong", "Something went wrong, Please try again later.");
            }
            else if (!entityVeriCode.VerifyType.Equals(vtype.ToString()))
            {
                msg.Add("SomethingWrong", "Something went wrong, Please try again later.");
            }
            else
            {
                if (entityVeriCode.IsVerified)
                {
                    msg.Add("AccountAlreadyConfirmed", "Your account is already active.");
                }
                else if (entityVeriCode.IsExpired)
                {
                    msg.Add("AccountConfirmExpire", "Expired confirmation code");
                }
                else if (!entityVeriCode.IsVerified)
                {
                    entityVeriCode.IsVerified = true;
                    var success = await _verifyCodeRepo.ModifyAsync(entityVeriCode);

                    if (!success)
                    {
                        msg.Add("SomethingWrong", "Something went wrong, Please try again later.");
                    }
                    response.ResponseData = entityVeriCode;
                }
            }

            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// genarate verification code as an asynchronous operation.
        /// </summary>
        /// <param name="mType">Type of the m.</param>
        /// <param name="vType">Type of the v.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GenarateVerificationCodeAsync(DSCMediaType mType, DSCVerifyType vType, Guid userId)
        {
            var response = new DSCResponse();


            //Generate Verification
            var verificationCode = await _verifyCodeRepo.GetSingleAsync(x => x.UserId == userId && x.VerifyType == vType.ToString() && !x.IsVerified && !x.IsExpired);

            if (verificationCode == null)
            {
                verificationCode = new VerificationCode()
                {
                    MediaType = mType.ToString(),
                    VerifyType = vType.ToString(),
                    UserId = userId
                };

                var isOperationSuccess = await _verifyCodeRepo.AddAsync(verificationCode);

                if (!isOperationSuccess)
                {
                    response.ErrorMessage = new Dictionary<string, string>()
                    {
                      {"Failed", "Failed" }
                    };
                }
            }

            response.ResponseData = verificationCode;
            return response;
        }

        /// <summary>
        /// get verification code as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;VerificationCode&gt;.</returns>
        public async Task<VerificationCode> GetVerificationCodeAsync(Guid id)
        {
           var codes = await  _verifyCodeRepo.GetSingleAsync(x => x.Id == id);

            return codes;
        }

        /// <summary>
        /// get user as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        public async Task<User> GetUserAsync(Guid id)
        {
            var users = await _userRepo.GetSingleAsync(x => x.Id == id);

            return users;
        }

        /// <summary>
        /// get management user as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        public async Task<User> GetManagementUserAsync(Guid userId)
        {
            var user = await _userRepo.GetSingleAsync(x => x.Id == userId && x.IsActive.ToBool());

            return await GetManagementUserAsync(user);
        }

        /// <summary>
        /// get management user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        public async Task<User> GetManagementUserAsync(User user)
        {

            if (user != null)
            {
                if (user.UserTypeEnum == PSUserType.ManagementCompany.ToString())
                {
                    return user;
                }
                else if(user.UserTypeEnum == PSUserType.ManagementPersonnel.ToString())
                {
                    var roleResponse = await GetUserRoleAsync(user);

                    if (roleResponse.IsSuccess)
                    {
                        var availableRole = roleResponse.ResponseData as AvailableRoleVM;

                        if (availableRole != null)
                        {
                            return await _userRepo.GetSingleAsync(x => x.Id == availableRole.ManagementUserId && x.IsActive.ToBool());
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// get property access as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetPropertyAccessAsync(Guid userId, Guid roleId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var propertyAccess = await _propAccessRepo.GetAsync(x => x.RoleId == roleId && x.IsValid);
            response.ResponseData = propertyAccess;
            

            return response;
        }

        /// <summary>
        /// get user by code as an asynchronous operation.
        /// </summary>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        public async Task<User> GetUserByCodeAsync(string serviceCode)
        {
            var users = await _userRepo.GetSingleAsync(x => x.ServiceCode == serviceCode);

            return users;
        }
        /// <summary>
        /// get user by codes as an asynchronous operation.
        /// </summary>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>Task&lt;IEnumerable&lt;User&gt;&gt;.</returns>
        public async Task<IEnumerable<User>> GetUserByCodesAsync(string[] serviceCode)
        {
            var users = await _userRepo.GetAsync(x => serviceCode.Contains(x.ServiceCode), numberOfRecords:serviceCode.Length);

            return users;
        }


        /// <summary>
        /// get user properties as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetUserPropertiesAsync(Guid userId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var user = await GetUserAsync(userId);

            if (user != null)
            {
                return await GetUserPropertiesAsync(user, null);
            }

            return response;
        }
        /// <summary>
        /// get user properties as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="availableRoleVm">The available role vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetUserPropertiesAsync(User user, AvailableRoleVM availableRoleVm = null)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<PropertyInformation> propInfos = null;

            if (user.UserTypeEnum == PSUserType.ManagementCompany.ToString())
            {
                propInfos = await _propertyRepo.GetAsync(x => x.ManagementUserId == user.Id, navigationPath: "ZipCode");

            }
            else if(user.UserTypeEnum == PSUserType.ManagementPersonnel.ToString())
            {
                if (availableRoleVm == null)
                {
                    var avrResponse = await GetUserRoleAsync(user);

                    availableRoleVm = avrResponse.ResponseData as AvailableRoleVM;
                }

                if (availableRoleVm != null)
                {
                    var propAccessResponse = await GetPropertyAccessAsync(user.Id, availableRoleVm.Id);

                    var propAccess = propAccessResponse.ResponseData as IEnumerable<PropertyAccess>;

                    if (propAccess != null)
                    {
                        var propIds = propAccess.Select(x => x.PropertyInformationId).ToList();

                        if (propIds.Any())
                        {
                           propInfos = await _propertyRepo.GetAsync(x => propIds.Contains(x.Id), navigationPath: "ZipCode");
                        }
                    }
                }
               
            } 
            else if(user.UserTypeEnum == PSUserType.Tenant.ToString())
            {
                var units = await _tenantRepo.GetAsync(x => x.UserId == user.Id);

                if (units != null && units.Any())
                {
                    var propIds = units.Select(x => x.PropertyInformationId).Distinct().ToList();

                    propInfos = await _propertyRepo.GetAsync(x => propIds.Contains(x.Id), navigationPath: "ZipCode");
                }
            }

            if (propInfos != null)
            {
                var propsVM = _mapper.Map<IEnumerable<PropertyInformation>, IEnumerable<PropertyInformationVM>>(propInfos);
                response.ResponseData = propsVM.OrderBy(x => x.PropName).ToList();
            }

            return response;
        }
        /// <summary>
        /// get user role as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetUserRoleAsync(User user)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            AvailableRoleVM role = null;

            if (user.UserTypeEnum == PSUserType.ManagementCompany.ToString())
            {
                role = new AvailableRoleVM() { HasManagementRights = true , ManagementUserId = user.Id} ;
            }
            else if (user.UserTypeEnum == PSUserType.ManagementPersonnel.ToString())
            {
                var staffRole = await _staffRoleRepo.GetSingleAsync(x => x.UserId == user.Id && x.IsValid);

                if (staffRole != null)
                {
                    var avalaibleRole = await _availableRole.GetSingleAsync(x => x.Id == staffRole.RoleId && x.IsValid);

                    role = _mapper.Map<AvailableRoleVM>(avalaibleRole);

                    response.ResponseData = role;
                }

            }

            response.ResponseData = role;
            return response;
        }

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="notifyVM">The notify vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> AddNotification(NotificationVM notifyVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var isSuccess = false;

            var notify = _mapper.Map<Notification>(notifyVM);

            if (notify != null)
            {
                isSuccess = await _notifyRepo.AddAsync(notify);
            }

            response.ResponseData = isSuccess;
            return response;
        }
    }
}
