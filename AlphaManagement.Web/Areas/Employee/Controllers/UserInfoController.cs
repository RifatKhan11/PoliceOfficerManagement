using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Models.Auth;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class UserInfoController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly IEmployeeService _employeeService;
        private readonly IUserInfoes _userInfoes;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserInfoController(IHostingEnvironment hostingEnvironment,IEmployeeService employeeService, IUserInfoes userInfoes, RoleManager<ApplicationRole> roleManager)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _employeeService = employeeService;
            _userInfoes = userInfoes;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> UserList()
        {
            try
            {
                List<EmpViewModel> empDetail = new List<EmpViewModel>();
                var UserList = await _userInfoes.GetUserInfosFromEmployee();
                var data = await _roleManager.Roles.ToListAsync();
                foreach (var emp in UserList)
                {
                    Photograph photograph = new Photograph();
                    photograph = await _employeeService.GetEmployeePhotographByEmpId(emp.Id);
                    if (photograph == null)
                        photograph = new Photograph();

                    var EmpModel = new EmpViewModel()
                    {
                        Employee = emp,
                        EmpPhotograph = photograph
                    };
                    empDetail.Add(EmpModel);
                }

                var model = new EmployeeInfoViewModel
                {
                    applicationRoles = data,
                    EmpDetailList = empDetail,
                    fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                };
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region API


        public async Task<IActionResult> UserInfoByUserId(string id)
        {
            var userInfo = await _userInfoes.GetUserInfoByUserId(id);
            return Json(userInfo);
        }
        

        public async Task<IActionResult> UserInfofromEmployee(string id)
        {
            var userInfo = await _userInfoes.GetUserInfoListByFilteringForPoliceUser(id);
            return Json(userInfo);
        }

        public async Task<IActionResult> AllRoleWithUserRoleByUserId(string id)
        {
            var userRoles = await _userInfoes.GetRoleListByUserId(id);
            var roles = await _roleManager.Roles.Where(x => x.Name != "Super Admin").ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel rolesModel = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name,
                    userStatus=0
                };
                if (userRoles.Contains(data.Id))
                {
                    rolesModel.userStatus = 1;
                }
                lstRole.Add(rolesModel);
            }

            ApplicationRoleViewModel model = new ApplicationRoleViewModel
            {
                roleViewModels = lstRole,
            };
            return Json(model);
        }
        #endregion
    }
}