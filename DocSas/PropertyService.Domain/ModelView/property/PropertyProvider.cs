using System;
using System.Collections.Generic;
using System.Text;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using System.Linq;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using AutoMapper;
using DSCAppEssentials.Abstract;
using PropertyService.Domain.Managers;
using DSCAppEssentials.Helpers.DSCEnums;
using PropertyService.Domain.Utilities.PSEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class PropertyProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IPropertyProvider" />
    public class PropertyProvider : IPropertyProvider
    {
        private readonly IPSRepository<PropertyInformation> _propertyRepo;
        private readonly IPSRepository<PropertyType> _propertTypeRepo;
        private readonly IPSRepository<ZipCode> _zipCodeRepo;  
        private readonly IPSRepository<OperationHour> _hourRepo;
        private readonly IPSRepository<PropertyAccess> _propAccessRepo;
        private readonly IPSRepository<TenantUnit> _tenantUnitRepo;
        private readonly IMapper _mapper;
        private readonly ICodeGeneratorProvider _codeGeneratorProvider;
       private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyProvider"/> class.
        /// </summary>
        /// <param name="propertyRepo">The property repo.</param>
        /// <param name="hourRepo">The hour repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="codeGeneratorProvider">The code generator provider.</param>
        /// <param name="propertTypeRepo">The propert type repo.</param>
        /// <param name="zipCodeRepo">The zip code repo.</param>
        /// <param name="propAccessRepo">The property access repo.</param>
        /// <param name="tenantUnitRepo">The tenant unit repo.</param>
        /// <param name="appCommon">The application common.</param>
        public PropertyProvider(IPSRepository<PropertyInformation> propertyRepo, IPSRepository<OperationHour> hourRepo, IMapper mapper, ICodeGeneratorProvider codeGeneratorProvider, IPSRepository<PropertyType> propertTypeRepo, IPSRepository<ZipCode> zipCodeRepo, IPSRepository<PropertyAccess> propAccessRepo, IPSRepository<TenantUnit>  tenantUnitRepo, IAppCommon appCommon)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
            _codeGeneratorProvider = codeGeneratorProvider;
            _propertTypeRepo = propertTypeRepo;
            _zipCodeRepo = zipCodeRepo;         
            _hourRepo = hourRepo;
            _appCommon = appCommon;
            _propAccessRepo = propAccessRepo;
            _tenantUnitRepo = tenantUnitRepo;

        }
        /// <summary>
        /// save property information as an asynchronous operation.
        /// </summary>
        /// <param name="propInfoVM">The property information vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SavePropertyInfoAsync(PropertyInformationVM propInfoVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            PropertyInformation propInfo = null;

            var management = await _appCommon.GetManagementUserAsync(propInfoVM.UserId);

            if (management != null)
            {
                propInfoVM.AboutUs = Utility.HtmlEncode(propInfoVM.AboutUs);

                if (propInfoVM.Id != Guid.Empty)
                {
                   
                    propInfo = await _propertyRepo.GetSingleAsync(x => x.Id == propInfoVM.Id, "ZipCode");

                    if (propInfo != null && management.Id == propInfo.ManagementUserId)
                    {
                        propInfo.PropName = propInfoVM.PropName;
                        propInfo.CoverOriginal = propInfoVM.CoverOriginal;
                        propInfo.CoverThumbnail = propInfoVM.CoverThumbnail;
                        propInfo.PropType = propInfoVM.PropType;
                        propInfo.StreetOne = propInfoVM.StreetOne;
                        propInfo.StreetTwo = propInfoVM.StreetTwo;
                        propInfo.Email = propInfoVM.Email;
                        propInfo.Phone = propInfoVM.Phone;
                        propInfo.Fax = propInfoVM.Fax;
                        propInfo.Weburl = propInfoVM.Weburl;
                        propInfo.AboutUs = propInfoVM.AboutUs;
                    }


                }
                else
                {
                    propInfo = _mapper.Map<PropertyInformationVM, PropertyInformation>(propInfoVM);

                    propInfo.UrlFriendlyName = CreateUrlFriendlyPropertyName(propInfo);

                    var property = await _propertyRepo.GetSingleAsync(x => x.UrlFriendlyName.Equals(propInfo.UrlFriendlyName), navigationPath: "ZipCode");

                    if (property != null)
                    {
                        msg.Add("PropertyAlreadyExists", "Property already exists");
                        response.ErrorMessage = msg;
                        return response;
                    }

                    propInfo.ManagementUserId = management.Id;
                    
                }

                var zip = propInfo.ZipCode;

                if (zip != null)
                {
                    if (zip.Id != Guid.Empty)
                    {
                      
                        var zipEntity = await _zipCodeRepo.GetSingleAsync(x => x.Id == zip.Id);

                        if (zipEntity != null)
                        {
                            if ((!string.IsNullOrWhiteSpace(zip.County) && string.IsNullOrWhiteSpace(zipEntity.County)))
                            {
                                zipEntity.County = zip.County;
                                await _zipCodeRepo.ModifyAsync(zipEntity);
                            }
                        }


                        propInfo.ZipId = zip.Id;
                        propInfo.ZipCode = null;
                    }
                    else if (propInfoVM.Id != Guid.Empty)
                    {
                        await _zipCodeRepo.AddAsync(zip);
                        propInfo.ZipId = zip.Id;
                        propInfo.ZipCode = null;
                    }

                }

                if (propInfoVM.Id == Guid.Empty)
                {
                    var code = await _codeGeneratorProvider.GenerateCodeAsync(DSCCodeType.Property);
                    if (code.IsSuccess)
                    {
                        propInfo.IsValid = true;
                        propInfo.PropCode = code.ResponseData.ToString();
                    }

                    await _propertyRepo.AddAsync(propInfo);

                }
                else
                {
                    propInfo.ZipCode = zip;
                    await _propertyRepo.ModifyAsync(propInfo);
                }

                if (propInfo.ZipCode == null)
                {
                    propInfo.ZipCode = zip;
                }

                var result = _mapper.Map<PropertyInformation, PropertyInformationVM>(propInfo);

                response.ResponseData = result;

            }


            return response;
        }

        /// <summary>
        /// save hour as an asynchronous operation.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveHourAsync(HourVM hour)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<OperationHour> hourEntity;

            if (hour != null && hour.Hours != null)
            {
                hourEntity = _mapper.Map<IEnumerable<OperationHour>>(hour.Hours);
                if (hour.Hours.First().Id == Guid.Empty)
                {
                    await _hourRepo.AddAsync(hourEntity);
                }
                else
                {
                    await _hourRepo.ModifyAsync(hourEntity);
                }

               var hourVm = _mapper.Map<IEnumerable<OperationHourVM>>(hourEntity);

                if (hourVm != null)
                {
                    hour.Hours = hourVm.ToList();
                    response.ResponseData = hour;
                }
            }

           
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// save tenant home as an asynchronous operation.
        /// </summary>
        /// <param name="tenantVM">The tenant vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveTenantHomeAsync(TenantUnitVM tenantVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var isSuccess = false;

            if (tenantVM.Id == Guid.Empty)
            {
                var property =  await _propertyRepo.GetSingleAsync(x => x.PropCode == tenantVM.PropertyCode && x.IsValid);

                if (property != null)
                {
                    var unit = await _tenantUnitRepo.GetSingleAsync(x => x.PropertyInformationId == property.Id && x.UserId == tenantVM.UserId  && !x.IsMovedOut);

                    if (unit != null)
                    {
                        msg.Add("HomeIsAlreadyAssinedToYou", "Home is taken");
                    }
                    else
                    {
                        var tenant = new TenantUnit()
                        {
                            PropertyInformationId =property.Id,
                            UserId = tenantVM.UserId,
                            UnitAddress = tenantVM.UnitAddress,
                            UnitName = tenantVM.UnitName


                        };

                        isSuccess =  await _tenantUnitRepo.AddAsync(tenant);

                        NotificationVM notification = new NotificationVM()
                        {
                            UserId = tenant.UserId,
                            PropertyInformationId = tenant.PropertyInformationId,
                            NotificationResourceKey = NotificationResourceKeys.NotifyNewResidentRequest,
                            NotificationTypeEnum = PSNotificationType.Residents.ToString(),
                            NotificationShowFor = PSNotificationShowFor.Property.ToString(),
                            NotificationTypeId = tenant.Id,
                            NotificationAdditionalInfo = tenant.UnitAddress

                        };

                        await _appCommon.AddNotification(notification);
                    }
                }
                else
                {
                    msg.Add("InvalidPropertyCode", "Invalid property code");
                }
            }
            else
            {
                var currentTenant = await _tenantUnitRepo.GetSingleAsync(x => x.Id == tenantVM.Id);

                if (currentTenant != null)
                {
                    currentTenant.UnitName = tenantVM.UnitName;

                   isSuccess =  await _tenantUnitRepo.ModifyAsync(currentTenant);
                }
            }


            response.ResponseData = isSuccess;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// get propety hour as an asynchronous operation.
        /// </summary>
        /// <param name="propertyInfoId">The property information identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetPropetyHourAsync(Guid propertyInfoId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            HourVM hourView= new HourVM();

            var hourEntity = await _hourRepo.GetAsync(x => x.PropertyInformationId == propertyInfoId);

            if (hourEntity.Any())
            {
                var hourVm = _mapper.Map<IEnumerable<OperationHourVM>>(hourEntity);
                hourView.Hours = hourVm.ToList();
                response.ResponseData = hourView;
            }
           
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// get property types as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetPropertyTypesAsync()
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var props = await _propertTypeRepo.GetAsync(x=>x.IsValid == true);

            response.ResponseData = props;
            return response;
        }

        /// <summary>
        /// save role property as an asynchronous operation.
        /// </summary>
        /// <param name="propVM">The property vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveRolePropertyAsync(PropertyAccessForm propVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var issuccess = false;

            var management = await _appCommon.GetManagementUserAsync(propVM.UserId);

            if (management != null)
            {
                var propRole = new List<PropertyAccess>();
                foreach (var item in propVM.PropertyInfos)
                {
                    if (item.ManagementUserId  != management.Id)
                    {
                        continue;
                    }
                    propRole.Add(new PropertyAccess() { RoleId = propVM.RoleId, PropertyInformationId = item.Id, IsValid = true, AddedDateUtc = DateTime.UtcNow, ModifiedDateUtc = DateTime.UtcNow });
                }

                await _propAccessRepo.AddAsync(propRole);

                issuccess = true;
            }

            response.ResponseData = issuccess;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// get property as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetPropertyAsync(Guid id)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            PropertyInformationVM propVm = null;

            var property = await _propertyRepo.GetSingleAsync(x => x.Id == id, navigationPath: "ZipCode");

            if (property != null)
            {
                propVm = _mapper.Map<PropertyInformationVM>(property);
                response.ResponseData = propVm;
            }


            return response;
        }


        /// <summary>
        /// get property by friendly name as an asynchronous operation.
        /// </summary>
        /// <param name="friendlyUrl">The friendly URL.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetPropertyByFriendlyNameAsync(string friendlyUrl)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            PropertyInformationVM propVm = null;

            var property = await _propertyRepo.GetSingleAsync(x => x.UrlFriendlyName.Equals(friendlyUrl), navigationPath:"ZipCode");

            if (property != null)
            {
                propVm = _mapper.Map<PropertyInformationVM>(property);
              
            }

            response.ResponseData = propVm;
            return response;
        }

        /// <summary>
        /// get user properties as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetUserPropertiesAsync(Guid userId)
        {
            return await _appCommon.GetUserPropertiesAsync(userId);
        }

        /// <summary>
        /// get property access as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetPropertyAccessAsync(Guid userId, Guid roleId)
        {
            return await _appCommon.GetPropertyAccessAsync(userId, roleId);
        }

        /// <summary>
        /// Creates the name of the URL friendly property.
        /// </summary>
        /// <param name="propInforma">The property informa.</param>
        /// <returns>System.String.</returns>
        private string CreateUrlFriendlyPropertyName(PropertyInformation propInforma)
        {
            var result = string.Empty;

            var propName = propInforma.PropName.Trim();

            var propNameArray = propName.Split(new string[] { " ", "  " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in propNameArray)
            {
                result += item.Trim();
            }

            var city = propInforma.ZipCode.City.Trim().Replace(" ", "").Replace("  ", "");
            var province = propInforma.ZipCode.Province.Trim().Replace(" ", "").Replace("  ", "");
            result = string.Concat(result,"_",city, "_", province);

            return result;
        }

        /// <summary>
        /// delete property as an asynchronous operation.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> DeletePropertyAsync(Guid propertyId, Guid userId)
        {
            var response = new DSCResponse();
            var msg = new Dictionary<string, string>();
            var issuccess = false;

            var management = await _appCommon.GetManagementUserAsync(userId);

            if (management != null)
            {
                var tenants = await  _tenantUnitRepo.GetSingleAsync(x => x.PropertyInformationId == propertyId);

                if (tenants == null)
                {
                    var prop = await _propertyRepo.GetSingleAsync(x => x.Id == propertyId && x.ManagementUserId == management.Id);

                    if (prop != null)
                    {
                        issuccess = await _propertyRepo.DeleteAsync(prop);

                    }
                }
                else
                {
                    msg.Add("CantDeleteProperty", "Property has Tenants, deletion is not allowed");
                }
              
            }

            response.ErrorMessage = msg;
            response.ResponseData = issuccess;
            return response;
        }

        /// <summary>
        /// delete property acess as an asynchronous operation.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> DeletePropertyAcessAsync(Guid propertyId, Guid roleId, Guid userId)
        {
            var response = new DSCResponse();
            var issuccess = false;

            var management = await _appCommon.GetManagementUserAsync(userId);

            if (management != null)
            {
                var prop = await _propAccessRepo.GetSingleAsync(x => x.PropertyInformationId == propertyId && x.RoleId == roleId);

                if (prop != null)
                {                  
                    issuccess = await _propAccessRepo.DeleteAsync(prop);

                }
            }

            response.ResponseData = issuccess;
            return response;
        }

        /// <summary>
        /// update property images as an asynchronous operation.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="propertyInfoId">The property information identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> UpdatePropertyImagesAsync(StorageResponse result, Guid propertyInfoId, Guid userId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            if (result.UploadedResults != null && result.UploadedResults.First().IsSuccess)
            {
                var propResponse = await GetPropertyAsync(propertyInfoId);

                if (propResponse.ResponseData != null)
                {
                    var prop = propResponse.ResponseData as PropertyInformationVM;

                    if (prop != null)
                    {
                        var fileResult = result.UploadedResults.First();
                        prop.CoverOriginal = fileResult.Name;
                        prop.CoverThumbnail = fileResult.Name;
                        prop.UserId = userId;

                        var saveResponse = await SavePropertyInfoAsync(prop);

                        if (saveResponse.IsSuccess)
                        {
                            response.ResponseData = saveResponse.ResponseData;
                        }

                    }
                }
            }

            

            return response;
        }
    }
}
