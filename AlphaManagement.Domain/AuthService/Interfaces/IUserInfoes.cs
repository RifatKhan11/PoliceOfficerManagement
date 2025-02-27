using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.Auth;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.AuthService.Interfaces
{
    public interface IUserInfoes
    {
        Task<IEnumerable<EmployeeInfo>> GetAllUserInfo();
      //  Task<IEnumerable<EmployeeListVM>> GetEmployeeUserForAlpha(string roleid);
        Task<IEnumerable<AspnetAndEmployeeModel>> GetUserInfoListByFilteringForPoliceUser(string id);
        Task<ApplicationUser> GetUserInfoByUser(string userName);
        Task<ApplicationUser> GetApplicationUserByUserId(string userId);
        Task<EmployeeInfo> GetEmployeeInfoByUserName(string userName);
        Task<AspNetUsersViewModel> GetUserInfoByUserName(string userName);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoList();
        Task<IEnumerable<AlphaModule>> GetAllAlphaModule();
        Task<bool> DeleteUserRoleListByUserId(string Id);
        Task<bool> DeleteRoleById(string Id);
        Task<int> SaveUserInfo(UserInformation userInformation);
        Task<UserInformation> GetUserInfoBeforeRegisterById(int id);
        Task<UserInformation> GetUserInfoBeforeRegister(string userName);
        void DeleteUserInfoBeforeRegister(string userName);
        Task<EmployeeInfo> GetUserInfoByUserId(string userId);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfo();
        Task<IEnumerable<string>> GetRoleListByUserId(string Id);
        Task<IEnumerable<EmployeeInfo>> GetUserInfosFromEmployee();
        Task<int> UserInactiveById(string userId);
        Task<int> UserActiveById(string userId);
        Task<IEnumerable<Navbar>> GetAllPage();
        Task<string> RemoveUserLocoutTimeByUserName(string userName);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoListForProxyAdmin(string userRoleId, string userName);
        Task<UserInformation> GetUserInfoBeforeRegisterByBpNumber(string bp);
        Task<ApplicationUser> GetUserInfoByBP(string BPNo);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoForAlpha();
        Task<int> SaveApplicationUserInfo(ApplicationUser user);

        Task<EmployeeInfo> GetUserInfoByEmpId(int userId);
        Task<IEnumerable<PostingReportView>> GetRankWiseAssignmentCopy(int rank);
        Task<IEnumerable<InternalPostingReportVM>> GetRankWiseInternalAssignmentCopy(int rank);
        Task<IEnumerable<PostingReportView>> GetRankWiseAssignmentCopyOngoing(int rank);
        Task<int?> GetRankWiseAssignmentCopyOngoingCount(int rank);
        Task<IEnumerable<PostingReportView>> GetRankWiseInternalAssignmentCopyOngoing(int rank);
        Task<int?> GetRankWiseInternalAssignmentCopyOngoingCount(int rank);
    }
}
