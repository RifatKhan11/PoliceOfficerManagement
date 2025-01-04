using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PoliceOfficerManagement.Areas.Auth.Models;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Data.Entity;
using PoliceOfficerManagement.Services.AuthServices.Interfaces;
using PoliceOfficerManagement.Services.Dapper.IInterfaces;

namespace PoliceOfficerManagement.Services.AuthServices
{
    public class UserInfoesService : IUserInfoes
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDapper _dapper;
        public UserInfoesService(AppDbContext context, RoleManager<IdentityRole> roleManager, IDapper dapper)
        {
            _context = context;
            this.roleManager = roleManager;
            this._dapper = dapper;
        }


        #region Role Management
        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {
            var data = await roleManager.Roles.ToListAsync();

            return data;
        }

        public async Task<int> SaveRole(RolesViewModel model)
        {

            IdentityRole role = new IdentityRole();

            role.Name = model.name;

            await roleManager.CreateAsync(role);

            return 1;
        }
        #endregion



        #region Find User Infos

        public async Task<AspnetAndEmployeeModel> GetUserInfoByUserName(string userName)
        {
            return await (from U in _context.Users
                          join ur in _context.UserRoles on U.Id equals ur.UserId
                          join R in _context.Roles on ur.RoleId equals R.Id
                          where U.UserName == userName
                          select new AspnetAndEmployeeModel
                          {
                              Id = U.Id,
                              UserName = U.UserName,
                              FullName = U.FullName,
                              Email = U.Email,
                              Citizenship = U.ImagePath,
                              roleId = R.Name,
                              isActive = U.isActive,
                              ApplicationUserId = U.Id,
                              roleName=R.Name,
                          }).FirstOrDefaultAsync();
        }
        #endregion

        public async Task<AspnetAndEmployeeModel> GetEmployeUserInfo(string userName)
        {
            var data = await (
                from U in _context.Users
                join ur in _context.UserRoles on U.Id equals ur.UserId
                join R in _context.Roles on ur.RoleId equals R.Id
                join e in _context.employeeInfos on U.UserName equals e.employeeCode
                where U.UserName == userName
                select new AspnetAndEmployeeModel
                {
                    Id = U.Id,
                    empId = e.Id,
                    UserName = U.UserName,
                    FullName = U.FullName,
                    Email = U.Email,
                    Citizenship = U.ImagePath,
                    roleId = R.Name,
                    isActive = U.isActive,
                    ApplicationUserId = U.Id
                }).FirstOrDefaultAsync();

            return data;

        }

        public async Task<IEnumerable<EmployeInfo>> GetEmployeeByThana(int ThanaId)
        {
            var data = await _context.employeeInfos.ToListAsync();
            return data;
        }



    }
}
