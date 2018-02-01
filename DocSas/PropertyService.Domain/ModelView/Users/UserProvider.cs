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
using PropertyService.Domain.ModelView.External;


namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class UserProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IUserProvider" />
    public class UserProvider : IUserProvider
    {
        private readonly IMapper _mapper;
        private readonly IPSRepository<UserType> _userTypeRepo;
        private readonly IPSRepository<User> _userRepo;
        private readonly IPSRepository<NotificationTemplate> _notifyTemplateRepo;
        private readonly IPSRepository<SecurityQuestion> _securityQuestionRepo;
        private readonly IPSRepository<ServiceCodeTrack> _serviceTrackRepo;
        private readonly IEmailProvider _emailProvider;
        private readonly ISettingProvider _settingProvider;
        private readonly ICodeGeneratorProvider _codeGeneratorProvider;  
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProvider"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="serviceTrackRepo">The service track repo.</param>
        /// <param name="userTypeRepo">The user type repo.</param>
        /// <param name="notifyTemplateRepo">The notify template repo.</param>
        /// <param name="verifyCodeRepo">The verify code repo.</param>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="securityQuestionRepo">The security question repo.</param>
        /// <param name="emailProvider">The email provider.</param>
        /// <param name="settingProvider">The setting provider.</param>
        /// <param name="codeGeneratorProvider">The code generator provider.</param>
        /// <param name="appCommon">The application common.</param>
        public UserProvider(IMapper mapper, IPSRepository<ServiceCodeTrack> serviceTrackRepo, IPSRepository<UserType> userTypeRepo, IPSRepository<NotificationTemplate> notifyTemplateRepo, IPSRepository<VerificationCode> verifyCodeRepo, IPSRepository<User> userRepo, IPSRepository<SecurityQuestion> securityQuestionRepo, IEmailProvider emailProvider, ISettingProvider settingProvider, ICodeGeneratorProvider codeGeneratorProvider, IAppCommon appCommon)
        {
            _userTypeRepo = userTypeRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _notifyTemplateRepo = notifyTemplateRepo;        
            _emailProvider = emailProvider;
            _settingProvider = settingProvider;
            _securityQuestionRepo = securityQuestionRepo;
            _serviceTrackRepo = serviceTrackRepo;
            _codeGeneratorProvider = codeGeneratorProvider;
            _appCommon = appCommon;

        }

        /// <summary>
        /// verify user session as an asynchronous operation.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> VerifyUserSessionAsync(UserVM userVM)
        {

            var response = new DSCResponse();
            var isSuccess = false;

            var user = await _appCommon.GetUserAsync(userVM.Id);

            if (user != null && userVM.LoginSessionId.HasValue && userVM.LoginSessionId == userVM.LoginSessionId)
            {
                var verifyUser = _appCommon.IsUserAccountValid(user);

                if (verifyUser.IsSuccess)
                {
                    var data = verifyUser.ResponseData as bool?;

                    if (data.HasValue)
                    {
                        isSuccess = data.Value;
                    }
                  
                }
            }

            response.ResponseData = isSuccess;
            return response;
        }

        /// <summary>
        /// has management rights as an asynchronous operation.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> HasManagementRightsAsync(UserVM userVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var isSuccess = false;

            var user = await _appCommon.GetUserAsync(userVM.Id);

            if (user != null && userVM.LoginSessionId.HasValue && userVM.LoginSessionId == userVM.LoginSessionId)
            {
                var roleResponse = await _appCommon.GetUserRoleAsync(user);

                var roleVM = roleResponse.ResponseData as AvailableRoleVM;

                if (roleVM != null)
                {
                    isSuccess = roleVM.HasManagementRights;
                }
            }

            response.ResponseData = isSuccess;
            return response;

        }

        /// <summary>
        /// verify change password as an asynchronous operation.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> VerifyChangePasswordAsync(UserVM userVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var verResponse = await _appCommon.IsVeriyCodeAsync(userVM.VericationCodeId, DSCVerifyType.ForgotPassword);

            if (verResponse.IsSuccess)
            {
                var verCode = verResponse.ResponseData as VerificationCode;
                userVM.Id = verCode.UserId;
                response.ResponseData = userVM;
            }
            else
            {
                msg = verResponse.ErrorMessage;
            }         

            response.ErrorMessage = msg;
            return response;


        }

        /// <summary>
        /// save new password as an asynchronous operation.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveNewPasswordAsync(UserVM userVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var vericode = await _appCommon.GetVerificationCodeAsync(userVM.VericationCodeId);

            if (vericode == null)
            {
                msg.Add("SomethingWrong", "Something went wrong");
            }
            else
            {

                var entity = await _appCommon.GetUserAsync(vericode.UserId);

                var userVerify = _appCommon.IsUserAccountValid(entity);

                if (userVerify.IsSuccess)
                {
                    //generate password salt
                    entity.UserSalt = SecurityManager.GenerateSalt();

                    //hash password
                    entity.Password = SecurityManager.HashPassword(userVM.Password, entity.UserSalt);

                    var success = await _userRepo.ModifyAsync(entity);

                    if (!success)
                    {
                        msg.Add("SomethingWrong", "Something went wrong");
                    }

                }
                else
                {
                    msg = userVerify.ErrorMessage;
                }

            }

            response.ErrorMessage = msg;
            return response;

        }

        /// <summary>
        /// confirm user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> ConfirmUserAsync(UserVM user)
        {
            var isOperationSuccess = false;
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var verResponse = await _appCommon.IsVeriyCodeAsync(user.VericationCodeId, DSCVerifyType.Register);

            if (verResponse.IsSuccess)
            {
                var verCode = verResponse.ResponseData as VerificationCode;

                var userEntity = await _appCommon.GetUserAsync(verCode.UserId);

                if (userEntity != null)
                {
                    var code = await _codeGeneratorProvider.GenerateCodeAsync(DSCCodeType.User);
                    if (code.IsSuccess)
                    {
                        userEntity.IsActive = true;
                        userEntity.ServiceCode = code.ResponseData.ToString();
                        isOperationSuccess = await _userRepo.ModifyAsync(userEntity);
                        response.ResponseData = isOperationSuccess;
                    }
               
                }

            }
            else
            {
                msg = verResponse.ErrorMessage;
            }

            if (!isOperationSuccess)
            {
               msg.Add("SomethingWrong", "Something went wrong");
            }

            response.ErrorMessage = msg;
            return response;


        }

        /// <summary>
        /// get user type as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetUserTypeAsync(Guid id)
        {
            var data = await _userTypeRepo.GetSingleAsync(x => x.Id == id);
            var model = _mapper.Map<UserType, UserTypeVM>(data);

            return new DSCResponse()
            {
                ResponseData = model
            };
        }

        /// <summary>
        /// get user types as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetUserTypesAsync()
        {           
            var data = await _userTypeRepo.GetAsync(x => x.IsValid.ToBool());
            var model = _mapper.Map<IEnumerable<UserType>, IEnumerable<UserTypeVM>>(data);

            return new DSCResponse()
            {
                ResponseData = model
            };
        }

        /// <summary>
        /// login user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> LoginUserAsync(UserVM user)
        {
            var msg = new Dictionary<string, string>();

            var response = new DSCResponse();

            var userEntity = await _userRepo.GetSingleAsync(x => x.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));

            var userVerify = _appCommon.IsUserAccountValid(userEntity);

            if (userVerify.IsSuccess)
            {
                var isValid = SecurityManager.VerifyPassword(user.Password, userEntity.Password);

                if (isValid)
                {
                    UserSessionVM sessionVM = new UserSessionVM();
                    if (userEntity.IsActive.HasValue && !userEntity.IsActive.ToBool())
                    {
                        userEntity.IsActive = true;
                    }
                    userEntity.LastLogin = DateTime.UtcNow;
                    userEntity.LoginSessionId = Guid.NewGuid();
                    await _userRepo.ModifyAsync(userEntity);

                    //Map
                    var userVM = _mapper.Map<User, UserVM>(userEntity);

                    var utypes = await _userTypeRepo.GetSingleAsync(x => x.UserTypeEnum == userEntity.UserTypeEnum);

                    var userTypeVM = _mapper.Map<UserType, UserTypeVM>(utypes);

                    var userRole = await _appCommon.GetUserRoleAsync(userEntity);

                   var role = userRole.ResponseData as AvailableRoleVM;

                    if (role != null)
                    {
                        sessionVM.IsManager = role.HasManagementRights;
                        sessionVM.ManagementId = role.ManagementUserId;
                    }

                    if (userTypeVM != null)
                    {
                        sessionVM.UserTypeEnum = userTypeVM.UserTypeEnum;
                        sessionVM.UserTypeId = userTypeVM.Id;
                        sessionVM.UserTypeTitle = userTypeVM.Title;
                        sessionVM.NoPropertyRedirectPage = userTypeVM.NoPropertyRedirectPage;
                    }


                    //Clean
                    userVM.Password = string.Empty;
                    userVM.UserSecurityAns = string.Empty;

                    sessionVM.UserVM = userVM;                   

                    response.ResponseData = sessionVM;
                }
                else
                {
                    msg.Add("InvalidAccount", "Invalid Account");
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
        /// deactivate account as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> DeactivateAccountAsync(UserVM user)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var isSuccess = false;

            var entity = await _appCommon.GetUserAsync(user.Id);

            if (entity != null && entity.IsActive.ToBool())
            {
                var isValid = SecurityManager.VerifyPassword(user.Password, entity.Password);
                if (isValid)
                {
                    entity.IsActive = false;
                    isSuccess = await _userRepo.ModifyAsync(entity);
                }
                else
                {
                    msg.Add("InvalidPassword", "Invalid password");
                }
               
            }
            else
            {
                msg.Add("AccountNotFound", "Account not found");
            }

            response.ResponseData = isSuccess;
            response.ErrorMessage = msg;
            return response;

        }

        /// <summary>
        /// register user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> RegisterUserAsync(UserVM user)
        {
            var isOperationSuccess = false;
            var msg = new Dictionary<string, string>();

            var response = new DSCResponse();

            //validation

            var validUsers = await _userRepo.GetSingleAsync(x => x.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));

            if (validUsers != null)
            {
                msg.Add("AccountAlreadyExist", "Account already registered.");
                response.ErrorMessage = msg;
                return response;
            }

            //Map to Entity
            var entity = _mapper.Map<UserVM, User>(user);

            //generate password salt
            entity.UserSalt = SecurityManager.GenerateSalt();

            //hash password
            entity.Password = SecurityManager.HashPassword(entity.Password, entity.UserSalt);

            //Add User
            isOperationSuccess = await _userRepo.AddAsync(entity);

            if (isOperationSuccess)
            {
                var genResponse = await GenerateAndSendRegisterEmailAsync(entity);

                if (!genResponse.IsSuccess)
                {
                    msg = genResponse.ErrorMessage;
                }
            }

            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// retrieve account as an asynchronous operation.
        /// </summary>
        /// <param name="userVM">The user vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> RetrieveAccountAsync(UserVM userVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var entity = await _userRepo.GetSingleAsync(x => x.Email.Equals(userVM.Email, StringComparison.OrdinalIgnoreCase));

            if (entity == null)
            {
                msg.Add("AccountNotFound", "Account not found");
            }
            else if (entity.IsDemoAccount)
            {
                msg.Add("DemoFunctionalityLimited", "Demo item: Functionality not allowed.");
            }
            else if (!entity.IsActive.HasValue)
            {
                msg.Add("AccountNotConfirmed", "Confirm your account");
            }
            else if (entity.IsLockedOut)
            {
                msg.Add("AccountLockedOut", "Account Locked");
            }
            else if (entity.IsBanned)
            {
                msg.Add("AccountBanned", "User Banned");
            }
            else if(entity.IsActive.Value)
            {
                if (entity.SecurityQuestionId.HasValue)
                {
                    var securityQuestions = await _securityQuestionRepo.GetSingleAsync(x => x.Id == entity.SecurityQuestionId);

                    if (securityQuestions != null)
                    {
                        userVM.SecurityQuestion = securityQuestions.Question;
                        userVM.Id = entity.Id;
                        userVM.FirstName = entity.FirstName;
                       
                    }                  

                }
                else
                {
                    var genResponse = await GenerateAndSendPasswordResetAsync(entity);
                    if (!genResponse.IsSuccess)
                    {
                        msg.Add("Failed", "Failed");
                    }
                }
            
            }
            else
            {
                msg.Add("AccountNotFound", "Account not found");
            }

            response.ResponseData = userVM;
            response.ErrorMessage = msg;
            return response;
        }

        public async Task<DSCResponse> VerifySecurityAnswerAsync(UserVM userVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var entity = await _appCommon.GetUserAsync(userVM.Id);

            if (entity == null)
            {
                msg.Add("AccountNotFound", "Account not found");
            }
            else
            {
                if (!entity.UserSecurityAns.Equals(userVM.UserSecurityAns, StringComparison.OrdinalIgnoreCase)) 
                {
                    msg.Add("InValidSecurityAnswer", "Answer is incorrect.");
                }
                else
                {
                    response.ResponseData = true;
                    //send change password link
                    var genResponse = await GenerateAndSendPasswordResetAsync(entity);
                    if (!genResponse.IsSuccess)
                    {
                        msg.Add("Failed", "Failed");
                    }
                }


            }

            
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// generate and send password reset as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GenerateAndSendPasswordResetAsync(User entity)
        {
            var response = new DSCResponse();
            bool isSuccess = false;

            //send change password link
            var verResponse = await _appCommon.GenarateVerificationCodeAsync(DSCMediaType.Email, DSCVerifyType.ForgotPassword, entity.Id);

            if (verResponse.IsSuccess)
            {
                var verificationCode = verResponse.ResponseData as VerificationCode;

                //Generate confirmation Notification
                var templates = await _notifyTemplateRepo.GetSingleAsync(x => x.TemplateType.Equals(NotifyTemplateKey.ForgotPassword, StringComparison.OrdinalIgnoreCase));

                if (templates != null)
                {
                    var appUrl = await _settingProvider.GetSetting(SettingKey.NewPasswordUrl);
                    var template = templates;
                    var emailBody = string.Format(template.TemplateHtml, entity.FirstName, string.Concat(appUrl, verificationCode?.Id));
                    emailBody =Utility.HtmlDecode(emailBody);
                    var dscEmail = new DSCEmail(new Tuple<string, string>(template.TemplateFromEmail, template.TemplateFromName),
                    new List<string>() { entity.Email }, template.TemplateSubject, emailBody);

                    await _emailProvider.SendEmailAsync(dscEmail);

                    isSuccess = true;
                }
            }

            if (!isSuccess)
            {
                response.ErrorMessage = new Dictionary<string, string>()
                {
                    {"Failed", "Failed" }
                };
            }

            response.ResponseData = isSuccess;
            return response;
        }

        /// <summary>
        /// generate and send register email as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GenerateAndSendRegisterEmailAsync(User entity)
        {
            var response = new DSCResponse();
            var isSuccess = false;
            var verResponse = await _appCommon.GenarateVerificationCodeAsync(DSCMediaType.Email, DSCVerifyType.Register, entity.Id);

            if (verResponse.IsSuccess)
            {
                var verificationCode = verResponse.ResponseData as VerificationCode;
                //Generate confirmation Notification
                var templates = await _notifyTemplateRepo.GetSingleAsync(x => x.TemplateType.Equals(NotifyTemplateKey.RegisterConfirmation, StringComparison.OrdinalIgnoreCase));

                if (templates != null)
                {
                    var appUrl = await _settingProvider.GetSetting(SettingKey.RegisterConfirmUrl);
                    var template = templates;
                    var emailBody = string.Format(template.TemplateHtml, entity.FirstName, string.Concat(appUrl, verificationCode?.Id));
                    emailBody = Utility.HtmlDecode(emailBody);
                    var dscEmail = new DSCEmail(new Tuple<string, string>(template.TemplateFromEmail, template.TemplateFromName),
                    new List<string>() { entity.Email }, template.TemplateSubject, emailBody);

                    await _emailProvider.SendEmailAsync(dscEmail);

                    isSuccess = true;
                }

            }

            if (!isSuccess)
            {
                response.ErrorMessage = new Dictionary<string, string>()
                {
                    {"Failed", "Failed" }
                };
            }

            response.ResponseData = isSuccess;
            return response;
        }

        /// <summary>
        /// save user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveUserAsync(UserVM user)
        {
            var response = new DSCResponse();
            var msg = new Dictionary<string, string>();
            Enum.TryParse(user.ChangeType, out PSChangeType changeType);
            User userEntity = null;

            if (changeType == PSChangeType.PasswordChange || changeType == PSChangeType.SecurityQuestion)
            {
                var verifyPasswordResponse = await _appCommon.VerifyCurrentPasswordAsync(user);
                if (verifyPasswordResponse.IsSuccess)
                {
                    userEntity = verifyPasswordResponse.ResponseData as User;
                }
                else
                {
                    msg = verifyPasswordResponse.ErrorMessage;
                }
             }
            else
            {

                userEntity = await _appCommon.GetUserAsync(user.Id);
            }
       
                if (userEntity != null)
                {
                    switch (changeType)
                    {
                        case PSChangeType.SecurityQuestion:
                            userEntity.SecurityQuestionId = user.SecurityQuestionId;
                            userEntity.UserSecurityAns = user.UserSecurityAns;
                            break;
                        case PSChangeType.PersonalInfo:
                        if ((string.IsNullOrWhiteSpace(userEntity.UserName) && !string.IsNullOrWhiteSpace(user.UserName))  || (!userEntity.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
                        {
                           
                            var checkUser = await _userRepo.GetSingleAsync(x => x.UserName != null && x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase));

                            if (checkUser ==null)
                            {
                                userEntity.UserName = user.UserName;
                            }
                            else
                            {
                                msg.Add("UserNameTaken", "Username Taken");
                            }
             
                        }

                        if (msg.Count < 1  && !userEntity.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
                        {
                   
                            var checkUser = await _userRepo.GetSingleAsync(x => x.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));

                            if (checkUser == null)
                            {
                                userEntity.Email = user.Email;
                            }
                            else
                            {
                                msg.Add("EmailTaken", "Email Taken");
                            }
 
                        }

                        userEntity.FirstName = user.FirstName;
                        userEntity.LastName = user.LastName;
                        userEntity.Lang = user.Lang;
                           
                            break;
                        case PSChangeType.PasswordChange:
                            //generate password salt
                            userEntity.UserSalt = SecurityManager.GenerateSalt();
                            //hash password
                            userEntity.Password = SecurityManager.HashPassword(user.NewPassword, userEntity.UserSalt);
                            break;
                        case PSChangeType.UserProfileImage:
                           userEntity.PhotoThumbnail = user.PhotoThumbnail;
                           userEntity.PhotoOriginal = user.PhotoOriginal;
                           break;
                        default:
                            break;

                    }

                if (msg.Count < 1)
                {
                    var success = await _userRepo.ModifyAsync(userEntity);

                    if (changeType == PSChangeType.UserProfileImage)
                    {
                        response.ResponseData = user;
                    }
                    else
                    {
                        response.ResponseData = true;
                    }

                    if (!success)
                    {
                        msg.Add("SomethingWrong", "Something went wrong");
                    }

                }

            }
           
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// verify recaptcha as an asynchronous operation.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> VerifyRecaptchaAsync(RecaptchaRequest req)
        {
            var response = new DSCResponse();
          
                var url = await _settingProvider.GetSetting(SettingKey.RecaptchaUrl);
                var secret = await _settingProvider.GetSetting(SettingKey.RecaptchaSecret);

            if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(secret))
            {
                url = string.Concat(url, $"?secret={secret}&response={req.response}");
                var result = await Utility.HttpPostItemAsync(url, null);               
                if (!string.IsNullOrWhiteSpace(result))
                {

                    var data = Utility.FromJson<RecaptchaResponse>(result);

                    if (data != null)
                    {
                        response.ResponseData = data;
                    }
                }

            }


            return response;
        }

        /// <summary>
        /// update user profile images as an asynchronous operation.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> UpdateUserProfileImagesAsync(StorageResponse result, Guid userId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            if (result.UploadedResults != null && result.UploadedResults.First().IsSuccess)
            {
                var fileResult = result.UploadedResults.First();

                var userVM = new UserVM() {
                        Id = userId,
                        PhotoOriginal = fileResult.Name,
                        PhotoThumbnail =  fileResult.Name,
                        ChangeType = PSChangeType.UserProfileImage.ToString()
                };

               var saveResponse = await SaveUserAsync(userVM);

               if (saveResponse.IsSuccess)
                {
                    response.ResponseData = saveResponse.ResponseData;
                }
                else
                {
                    msg.Add("SomethingWrong", "Something went wrong");
                }
            }

            return response;
        }


    }
}
