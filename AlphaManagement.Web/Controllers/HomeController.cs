using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AlphaManagement.DAL.Entity;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.DAL.Models;
using AlphaManagement.Web.Areas.Portfolio.Models;
using AlphaManagement.Web.Areas.Employee.Models;
using Microsoft.AspNetCore.Session;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;

namespace AlphaManagement.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IUserInfoes userInfoes;
        private readonly IInternalPostingServices _internalPostingServices;

        public HomeController(UserManager<ApplicationUser> userManager,IEmployeeService employeeService, IUserInfoes userInfoes, IInternalPostingServices _internalPostingServices)
        {
            _userManager = userManager;
            _employeeService = employeeService;
            this._internalPostingServices = _internalPostingServices;
            this.userInfoes = userInfoes;
        }

        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("IGP"))
            {
                return RedirectToAction(nameof(IGPDashBoard));
                // return RedirectToAction("AssignmentPostMaster", "Assignment", new { Area = "Employee" });
            }
            else if (roles.Contains("Admin"))
            {
                return RedirectToAction(nameof(AdminDashBoard));
            }
            else if (roles.Contains("Sub-Admin"))
            {
                return RedirectToAction(nameof(AdminDashBoard));
            }
            else if (roles.Contains("PHQ Approver"))
            {
                return RedirectToAction("PHQAdminDashBoard", "Home");
            }
            else if(roles.Contains("Super Admin"))
            {
                return RedirectToAction(nameof(DashBoard));
            }
            else if(roles.Contains("Reserve Office"))
            {
                return RedirectToAction("EmployeeListHeadQuaterReserve", "InternalEmployee", new { Area = "InternalPosting" });
            }
            else if (roles.Contains("General User"))
            {
                var empInfo = await _employeeService.GetEmployeeInfoById(userName);
                if (empInfo != null)
                {
                    //return RedirectToAction("Index", "EmployeeInfo", new { empIdentityUserSessionToken = empInfo?.Id, Area = "Employee" });
                    return RedirectToAction("Index", "Dashboard", new { empIdentityUserSessionToken = empInfo?.Id, Area = "Portfolio" });
                }
                else
                {
                    return RedirectToAction("UserLogin", "Account", new { Area = "Auth" });
                }
                
            }

            else if(roles.Contains("Departmental User"))
            {
                return RedirectToAction(nameof(DepartmentalDashBoard));
            }
            else if(roles.Contains("Portfolio Checker"))
            {
                return RedirectToAction(nameof(PortfolioCheckerDashBoard));
            }
            else if (roles.Contains("Traning1"))
            {
                return RedirectToAction("Index", "TrainingSkill", new { Area = "Portfolio" });
            }
            else if (roles.Contains("Disciplinary Action Entry Operator"))
            {
                return RedirectToAction("DisciplinaryAndAction", "TrainingSkill", new { Area = "Portfolio" });
            }
            else if (roles.Contains("BPA"))
            {
                return RedirectToAction("BPAinfoList", "DisciplinaryAction", new { Area = "Employee" });
            }
            else if (roles.Contains("SB"))
            {
                return RedirectToAction("SBinfoList", "DisciplinaryAction", new { Area = "Employee" });
            }
            else if (roles.Contains("Medical"))
            {
                return RedirectToAction("EmployeeMedicalInfo", "TrainingSkill", new { Area = "Portfolio" });
            }
            else if (roles.Contains("PM1 Gradation"))
            {
                return RedirectToAction("Index", "EmployeeGradation", new { Area = "Employee" });
            }
            else
            {
                return View();
            }
        }
        
        [Authorize(Roles = "Super Admin,Sub-Admin,PHQ Approver")]
        public async Task<IActionResult> DashBoard()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var enlistedMasterAssignments = await _employeeService.EnListedMasterDetails(applicationUser.Id);

            EmployeePortfolioDashBoardViewModel model = new EmployeePortfolioDashBoardViewModel
            {
                onGoing = await _employeeService.AssignmentMastersCount(applicationUser.Id),
                finalSubmit = await _employeeService.AssignmentPostedMastersApproveCount(applicationUser.Id),
                returned = await _employeeService.AssignmentReturnListCount(applicationUser.Id),
                registration = enlistedMasterAssignments.Count()
            };
            return View(model);
        }

        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        public async Task<IActionResult> AdminDashBoard()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var enlistedMasterAssignments = await _employeeService.EnListedMasterDetails(applicationUser.Id);

            var emp = await userInfoes.GetUserInfoByUserName(User.Identity.Name);
            EmployeePortfolioDashBoardViewModel model = new EmployeePortfolioDashBoardViewModel
            {
                onGoing = await userInfoes.GetRankWiseAssignmentCopyOngoingCount((int)emp.rankId),
                finalSubmit = await _employeeService.AssignmentPostedMastersApproveCount(applicationUser.Id),
                returned = await _employeeService.AssignmentReturnListCount(applicationUser.Id),
                registration = enlistedMasterAssignments.Count(),

                internalongoing = await userInfoes.GetRankWiseInternalAssignmentCopyOngoingCount((int)emp.rankId),
                internalApproved = await _internalPostingServices.ApprovedInternalAssignmentMastersListCount(applicationUser.Id),
                internalReturn = await _internalPostingServices.AssignmentReturnListCount(applicationUser.Id),
                overduejoining = await _employeeService.AssignmentDueCount(),
            };
            return View(model);
        }

        [Authorize(Roles = "Super Admin,Admin,Sub-Admin,PHQ Approver")]
        public async Task<IActionResult> PHQAdminDashBoard()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var emp = await userInfoes.GetUserInfoByUserName(User.Identity.Name);
            EmployeePortfolioDashBoardViewModel model = new EmployeePortfolioDashBoardViewModel
            {
                onGoing = await userInfoes.GetRankWiseInternalAssignmentCopyOngoingCount((int)emp.rankId),
                finalSubmit = await _internalPostingServices.ApprovedInternalAssignmentMastersListCount(applicationUser.Id),
                returned = await _internalPostingServices.AssignmentReturnListCount(applicationUser.Id),
                registration = await _employeeService.GetTotalPHQEmployee()
            };
            return View(model);
        }

        [Authorize(Roles = "Super Admin,IGP")]
        public async Task<IActionResult> IGPDashBoard()
        {
            EmployeePortfolioDashBoardViewModel model = new EmployeePortfolioDashBoardViewModel
            {
                onGoing = await _employeeService.AssignmentPostedMastersForApproveCount(),
                returned = await _employeeService.AssignmentMasterLockedCount(4),
                registration = await _employeeService.AssignmentMasterLockedCount(3),

                internalongoing = await _internalPostingServices.PendingEnlistMastersForIgpCount(),
                internalApproved = await _internalPostingServices.AssignmentInternalReturnAndLockCount(4),
                internalReturn = await _internalPostingServices.AssignmentInternalReturnAndLockCount(5),
            };
            return View(model);
        }

        [Authorize(Roles = "Super Admin,Departmental User,Admin,Sub-Admin")]
        public async Task<IActionResult> DepartmentalDashBoard()
        {
            EmployeePortfolioDashBoardViewModel model = new EmployeePortfolioDashBoardViewModel
            {
                registration = await _employeeService.GetStatusWiseEmployeeCount(1),
                onGoing = await _employeeService.GetStatusWiseEmployeeCount(2),
                finalSubmit = await _employeeService.GetStatusWiseEmployeeCount(3),
                varified = await _employeeService.GetStatusWiseEmployeeCount(4),
                returned = await _employeeService.GetStatusWiseEmployeeCount(6),
                checkedItem = await _employeeService.GetCheckedEmployeeCount(2),
                pendingItem = await _employeeService.GetCheckedEmployeeCount(1),
                //   employeeInfoList = await _employeeService.GetEmployeeInfoList(0)
                employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0, 0, "All"),


                todaysRegistration = await _employeeService.GetTodaysEmployeesStatusWise(1),
                todaysOnGoing = await _employeeService.GetTodaysEmployeesStatusWise(2),
                todaysFinalSubmit = await _employeeService.GetTodaysEmployeesStatusWise(3),
                todaysVarified = await _employeeService.GetTodaysEmployeesStatusWise(4),
                todaysReturned = await _employeeService.GetTodaysEmployeesStatusWise(6),

            };
            //  model.registred = await _employeeService.GetStatusWiseEmployeeCount(0);
            model.totalRegistration = model.onGoing + model.finalSubmit;// + model.varified + model.checkedItem;
            model.todaysTotalRegistration = model.todaysRegistration + model.todaysOnGoing + model.todaysFinalSubmit + model.todaysVarified + model.todaysReturned;
            model.unitList = await _employeeService.GetUnitWiseEmployeeCount();
            return View(model);
        }

        [Authorize(Roles = "Portfolio Checker,Admin")]
        public async Task<IActionResult> PortfolioCheckerDashBoard()
        {
            EmployeePortfolioDashBoardViewModel model = new EmployeePortfolioDashBoardViewModel
            {
                //registration = await _employeeService.GetStatusWiseEmployeeCount(1),
                onGoing = await _employeeService.GetStatusWiseEmployeeCount(2),
                finalSubmit = await _employeeService.GetStatusWiseEmployeeCount(3),
                varified = await _employeeService.GetStatusWiseEmployeeCount(4),
                returned = await _employeeService.GetStatusWiseEmployeeCount(6),
                pendingItem = await _employeeService.GetCheckedEmployeeCount(1),
                //checkedItem = await _employeeService.GetCheckedEmployeeCount(1),
                checkedItem = await _employeeService.GetCheckedEmployeeCountByChecker(1,HttpContext.User.Identity.Name),
                //   employeeInfoList = await _employeeService.GetEmployeeInfoList(0)
                employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0, 0, "All"),


                todaysRegistration = await _employeeService.GetTodaysEmployeesStatusWise(1),
                todaysOnGoing = await _employeeService.GetTodaysEmployeesStatusWise(2),
                todaysFinalSubmit = await _employeeService.GetTodaysEmployeesStatusWise(3),
                todaysVarified = await _employeeService.GetTodaysEmployeesStatusWise(4),
                todaysReturned = await _employeeService.GetTodaysEmployeesStatusWise(6),

            };
            //  model.registred = await _employeeService.GetStatusWiseEmployeeCount(0);
            model.totalRegistration = model.onGoing + model.finalSubmit;// + model.varified + model.returned + model.checkedItem;
            //model.totalRegistration = model.onGoing + model.finalSubmit + model.varified + model.checkedItem;
            model.todaysTotalRegistration = model.todaysRegistration + model.todaysOnGoing + model.todaysFinalSubmit + model.todaysVarified + model.todaysReturned;
            model.unitList = await _employeeService.GetUnitWiseEmployeeCount();
            return View(model);
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult GridMenuPage()
        {
            return View();
        }


        [Authorize(Roles = "PM1 Gradation")]
        public IActionResult GridMenuPagePM1()
        {
            return View();
        }
        public IActionResult Not404Found()
        {
            return View();
        }

        public async Task<IActionResult> Not404Verified()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult GridMenuAddressPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("global/api/GetEmployeeApiForChartJs")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeApiForChartJs()
        {
            return Json(await _employeeService.GetEmployeeForChartJs());
        }

        [Route("global/api/GetRankWiseCount")]
        [HttpGet]
        public async Task<IActionResult> GetRankWiseCount()
        {
            return Json(await _employeeService.GetRankWiseCount());
        }

        [Route("global/api/GetRankWiseVacancyCount")]
        [HttpGet]
        public async Task<IActionResult> GetRankWiseVacancyCount()
        {

            var data = await _employeeService.RankWiseVacancyCount();

            var result = new RankWiseVacancyViewModelForChart
            {
                 rankName = data.Select(x => x.rankName).ToList(),
                 totalVacant = data.Select(x => x.totalVacant).ToList()
            };
          
            return Json(result);
        }

        [Route("global/api/GetRankWisOverDueCount")]
        [HttpGet]
        public async Task<IActionResult> GetRankWisOverDueCount()
        {
            var data = await _employeeService.RankWiseOverDueCount();
            var result = new RankWiseOverDueViewModelForChart
            {
                rankName = data.Select(x => x.rankName).ToList(),
                totalEmployee = data.Select(x => x.totalEmployee).ToList()
            };
            return Json(result);
        }

        [Route("global/api/GetUnitWiseOverduePosting")]
        [HttpGet]
        public async Task<IActionResult> GetUnitWiseOverduePosting()
        {
            var data = await _employeeService.UnitWiseOverduePosting();
            var result = new UnitWiseOverduePostingViewModel
            {
                branchUnitName = data.Select(x => x.branchUnitName).ToList(),
                totalEmployee = data.Select(x => x.totalEmployee).ToList()
            };
            return Json(result);
        }


    }
}
