using Microsoft.AspNetCore.Identity;
using PoliceOfficerManagement.Areas.Auth.Models;
using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Services.AuthServices.Interfaces
{
    public interface IUserInfoes
    {

        #region Role Management
        Task<IEnumerable<IdentityRole>> GetAllRoles();
        Task<int> SaveRole(RolesViewModel model);

        #endregion

        #region User Management
        Task<AspnetAndEmployeeModel> GetUserInfoByUserName(string userName);
        Task<AspnetAndEmployeeModel> GetEmployeUserInfo(string userName);
        Task<IEnumerable<EmployeInfo>> GetEmployeeByThana(int ThanaId);
        #endregion
    }
}
