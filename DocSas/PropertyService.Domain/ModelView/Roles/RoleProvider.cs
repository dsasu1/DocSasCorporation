using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using AutoMapper;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using PropertyService.Domain.Utilities.PSEnums;
using System.Linq;

namespace PropertyService.Domain.ModelView
{
    public class RoleProvider : IRoleProvider
    {

        private readonly IPSRepository<AvailableRole> _availableRoleRepo;
        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProvider"/> class.
        /// </summary>
        /// <param name="availableRoleRepo">The available role repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public RoleProvider(IPSRepository<AvailableRole> availableRoleRepo, IMapper mapper, IAppCommon appCommon)
        {
            _availableRoleRepo = availableRoleRepo;
            _mapper = mapper;
            _appCommon = appCommon;
        }

        /// <summary>
        /// get available roles as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetAvailableRolesAsync(Guid userId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var management = await _appCommon.GetManagementUserAsync(userId);

            if (management != null)
            {
                var roles = await _availableRoleRepo.GetAsync(x => x.ManagementUserId == management.Id);

                var roleVMs = _mapper.Map<IEnumerable<AvailableRoleVM>>(roles);

                response.ResponseData = roleVMs.OrderBy(x => x.Title).ToList(); ;
            }

           
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// get available role as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetAvailableRoleAsync(Guid id)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var role = await _availableRoleRepo.GetSingleAsync(x => x.Id == id);

            var map = _mapper.Map<AvailableRoleVM>(role);

            response.ResponseData = map;

            response.ErrorMessage = msg;
            return response;
        }


        /// <summary>
        /// save available role as an asynchronous operation.
        /// </summary>
        /// <param name="roleVM">The role vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveAvailableRoleAsync(AvailableRoleVM roleVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var issuccess = false;

            if (roleVM.Id == Guid.Empty)
            {
                var management = await _appCommon.GetManagementUserAsync(roleVM.CreatorUserId);

                if (management != null)
                {
                    var role = new AvailableRole()
                    {
                        Title = roleVM.Title,
                        RoleDesc = roleVM.RoleDesc,
                        ManagementUserId = management.Id,
                        HasManagementRights = roleVM.HasManagementRights,
                        IsValid = true

                    };
                    await _availableRoleRepo.AddAsync(role);
                    issuccess = true;
                }

            }
            else
            {
                var role = await _availableRoleRepo.GetSingleAsync(x => x.Id == roleVM.Id);

                if (role != null)
                {
                    role.Title = roleVM.Title;
                    role.RoleDesc = roleVM.RoleDesc;
                    role.HasManagementRights = roleVM.HasManagementRights;
                    role.IsValid = roleVM.IsValid;

                    await _availableRoleRepo.ModifyAsync(role);

                    issuccess = true;
                }
            }

            response.ResponseData = issuccess;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// delete available role as an asynchronous operation.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> DeleteAvailableRoleAsync(Guid roleId, Guid userId)
        {
            var response = new DSCResponse();
            var issuccess = false;

            var management = await _appCommon.GetManagementUserAsync(userId);

            if (management != null)
            {
                var role = await _availableRoleRepo.GetSingleAsync(x => x.Id == roleId && management.Id == x.ManagementUserId);

                if (role != null)
                {
                    issuccess = await _availableRoleRepo.DeleteAsync(role);
                }
            }

            response.ResponseData = issuccess;
            return response;
        }
    }
}
