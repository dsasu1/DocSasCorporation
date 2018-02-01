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
    /// Class StaffProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IStaffProvider" />
    public class StaffProvider : IStaffProvider
    {
        private readonly IPSRepository<Staff> _staffRepo;
        private readonly IPSRepository<StaffRole> _staffRoleRepo;
        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffProvider"/> class.
        /// </summary>
        /// <param name="staffRepo">The staff repo.</param>
        /// <param name="staffRoleRepo">The staff role repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public StaffProvider(IPSRepository<Staff> staffRepo, IPSRepository<StaffRole> staffRoleRepo,IMapper mapper, IAppCommon appCommon)
        {
            _staffRepo = staffRepo;
            _mapper = mapper;
            _appCommon = appCommon;
            _staffRoleRepo = staffRoleRepo;
        }

        /// <summary>
        /// get staffs as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetStaffsAsync(Guid userId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var management = await _appCommon.GetManagementUserAsync(userId);

            if (management != null)
            {
              var  staffs = await _staffRepo.GetQueryAsync<StaffRoleVM>("GetStaffs", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"managementUserId",management.Id }
                    });

                response.ResponseData = staffs;
            }


            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// save staff as an asynchronous operation.
        /// </summary>
        /// <param name="staffVM">The staff vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveStaffAsync(StaffVM staffVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var issuccess = false;

            if (staffVM.Id == Guid.Empty)
            {
                var management = await _appCommon.GetManagementUserAsync(staffVM.CreatorUserId);

                if (management != null)
                {
                    var users = await _appCommon.GetUserByCodesAsync(staffVM.serviceCodeValues);
                    var staffs = new List<Staff>();

                    foreach (var item in users)
                    {
                        staffs.Add(new Staff() { UserId = item.Id, ManagementUserId = management.Id, Title = staffVM.Title, IsValid = true, AddedDateUtc = DateTime.UtcNow, ModifiedDateUtc = DateTime.UtcNow });
                    }

                    await _staffRepo.AddAsync(staffs);

                    issuccess = true;
                }              
              
            }
            else
            {
                var staff = await _staffRepo.GetSingleAsync(x => x.Id == staffVM.Id);

                if (staff != null)
                {
                    staff.Title = staffVM.Title;

                    await _staffRepo.ModifyAsync(staff);

                   issuccess = true;
                }
            }

            response.ResponseData = issuccess;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// save role staff as an asynchronous operation.
        /// </summary>
        /// <param name="staffVM">The staff vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveRoleStaffAsync(StaffRoleForm staffVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            var issuccess = false;

                var management = await _appCommon.GetManagementUserAsync(staffVM.UserId);

                if (management != null)
                {
                  var stRole = new List<StaffRole>();
                  foreach (var item in staffVM.StaffRoleInput)
                  {
                    stRole.Add(new StaffRole() { RoleId = staffVM.RoleId, StaffId= item.Id, UserId = item.UserId, IsValid = true, AddedDateUtc = DateTime.UtcNow, ModifiedDateUtc = DateTime.UtcNow });
                  }

                await _staffRoleRepo.AddAsync(stRole);

                    issuccess = true;
                }

            response.ResponseData = issuccess;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// delete staff as an asynchronous operation.
        /// </summary>
        /// <param name="staffId">The staff identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> DeleteStaffAsync(Guid staffId, Guid userId)
        {
            var response = new DSCResponse();
            var issuccess = false;

            var management = await _appCommon.GetManagementUserAsync(userId);

            if (management != null)
            {
                var staff = await _staffRepo.GetSingleAsync(x => x.Id == staffId && management.Id == x.ManagementUserId);

                if (staff != null)
                {
                    issuccess = await _staffRepo.DeleteAsync(staff);

                    var staffRole = await _staffRoleRepo.GetSingleAsync(x => x.StaffId == staff.Id);

                    if (staffRole != null)
                    {
                        issuccess = await _staffRoleRepo.DeleteAsync(staffRole);
                    }
                }
            }

            response.ResponseData = issuccess;
           return response;
        }

        /// <summary>
        /// delete staff role as an asynchronous operation.
        /// </summary>
        /// <param name="staffRoleId">The staff role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> DeleteStaffRoleAsync(Guid staffRoleId, Guid userId)
        {
            var response = new DSCResponse();
            var issuccess = false;

            var management = await _appCommon.GetManagementUserAsync(userId);

            if (management != null)
            {              
                    var staffRole = await _staffRoleRepo.GetSingleAsync(x => x.Id == staffRoleId);

                    if (staffRole != null)
                    {                     
                        issuccess = await _staffRoleRepo.DeleteAsync(staffRole);
                    }
                
            }

            response.ResponseData = issuccess;
            return response;
        }

    }
}
