using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Web.Areas.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaManagement.Web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class MasterRoleController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<AlphaModule> _repoModule;
        private readonly IRepository<Navbar> _repoNavbar;
        private IUserInfoes _userInfoes;
        private INavbarService _navbarService;
        public MasterRoleController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IUserInfoes userInfoes, IRepository<AlphaModule> repoModule, IRepository<Navbar> repoNavbar, INavbarService navbarService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userInfoes = userInfoes;
            _repoModule = repoModule;
            _repoNavbar = repoNavbar;
            _navbarService = navbarService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> PageCreate()
        {
            var data = new PageAssignViewModel
            {
                module = _repoModule.GetAll(),
                navbars = _repoNavbar.GetAll()
            };
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNavbar([FromForm] PageAssignViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.module = _repoModule.GetAll();
                model.navbars = _repoNavbar.GetAll();
                return View(model);
            }
            int? parentId = model.parentID;
            if (model.isParent == 2)
            {
                parentId = model.bandID;
            }

            Navbar data = new Navbar
            {
                Id = model.Id ?? 0,
                nameOption = model.nameOption,
                nameOptionBangla = model.nameOptionBangla,
                moduleId = model.moduleId,
                area = model.area,
                controller = model.controller,
                action = model.action,
                imageClass = model.imageClass,
                activeLi = model.activeLi,
                status = model.status,
                isParent = model.isParent,
                parentID = (int)parentId,
                displayOrder = model.displayOrder
            };

            await _navbarService.SaveNavbarItem(data);

            return RedirectToAction(nameof(PageCreate));
        }

        #region Api
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var data = await _roleManager.Roles.ToListAsync();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPageList()
        {
            var data = await _userInfoes.GetAllPage();
            return Json(data);
        }
        #endregion

        #region Admin

        [Authorize]
        public async Task<IActionResult> UserProxyByAdmin()
        {
            string userName = HttpContext.User.Identity.Name;
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);
            }
            UserListViewModel model = new UserListViewModel
            {
                userRoles = lstRole,
            };

            return View(model);
        }

        [AllowAnonymous]
        [Route("api/Account/AllUserListByFiltering/{userRoleId}/{userName}")]
        [HttpGet]
        public async Task<IActionResult> AllUserListByFiltering(string userRoleId, string userName)
        {
            var result = await _userInfoes.GetUserInfoListForProxyAdmin(userRoleId, userName);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> SwitchedUser(string userId, string securityCode)
        {
            string userName = HttpContext.User.Identity.Name;
            string returnUrl = "/";
            ApplicationUser user = await _userManager.FindByNameAsync(userId);
            if (user != null && securityCode == "ALPHA%BP")
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                var roles = await _userManager.GetRolesAsync(user);
                //var role = await _userInfoes.GetUserInfoByUserName(userId);
                if (roles.Contains("IGP"))
                {
                    return RedirectToAction("AssignmentPostMaster", "Assignment", new { Area = "Employee" });
                }
                else if (roles.Contains("Admin"))
                {
                    return RedirectToAction("AdminDashboard", "Home");
                }
                else if (roles.Contains("Super Admin"))
                {
                    return RedirectToAction("Dashboard", "Home");
                }
                else if (roles.Contains("General User"))
                {
                    return RedirectToAction("Index", "EmployeeInfo", new { empIdentityUserSessionToken = userName, Area = "Employee" });
                }
                else if (roles.Contains("Disciplinary Action Entry Operator") || roles.Contains("ACR Entry Operator"))
                {
                    return RedirectToAction("ACRDisiplinaryVerify", "Account", new { UserId = user.Id, Area = "Auth" });
                }
                else if (roles.Contains("Departmental User"))
                {
                    return RedirectToAction("DepartmentalDashBoard", "PortfolioDashboard");
                }
                else if (roles.Contains("Portfolio Checker"))
                {
                    return RedirectToAction("PortfolioCheckerDashBoard", "PortfolioDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                return RedirectToAction(nameof(UserProxyByAdmin));
            }


        }
        #endregion
    }
}