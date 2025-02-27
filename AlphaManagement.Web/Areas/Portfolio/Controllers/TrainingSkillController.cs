using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Portfolio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.Portfolio.Controllers
{
    [Area("Portfolio")]
    [Authorize]
    public class TrainingSkillController : Controller
    {
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<SpecialBranchUnit> _specialBranchUnit;
        private readonly IRepository<BCSBatch> _bCSBatch;
        private readonly IRepository<TraningLog> _trainingLog;
        private readonly IEmployeeService _employeeService;
        private UserManager<ApplicationUser> _userManager;

        public TrainingSkillController(IRepository<Rank> repoRank, IRepository<SpecialBranchUnit> specialBranchUnit, IRepository<BCSBatch> bCSBatch,IEmployeeService employeeService, IRepository<TraningLog> trainingLog, UserManager<ApplicationUser> userManager)
        {
            _repoRank = repoRank;
            _specialBranchUnit = specialBranchUnit;
            _bCSBatch = bCSBatch;
            _employeeService = employeeService;
            _trainingLog = trainingLog;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var data = new DashBoardViewModel
            {
                rank = _repoRank.GetAll(),
                specialBranchUnits = _specialBranchUnit.GetAll(),
                batches = _bCSBatch.GetAll()
            };
            return View(data);
        }

        public async Task<IActionResult> GetEmployeeListWithTrainingSkills(int unitId, int rankId, int batchId)
        {
            var data = await _employeeService.GetEmployeeListWithTrainingSkills(rankId, unitId, batchId);
            return Json(data);
        }

        //Portfolio/TrainingSkill/GetEmployeeInfoByBP?BP=
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployeeInfoByBP(string BP)
        {
            var data = await _employeeService.GetEmployeeInfoByBP(BP);
            return Json(data);
        }

        #region Diciplinary
        [Authorize(Roles = "Admin,Super Admin,Disciplinary Action Entry Operator")]
        public IActionResult DisciplinaryAndAction()
        {
            var data = new DashBoardViewModel
            {
                rank = _repoRank.GetAll(),
                specialBranchUnits = _specialBranchUnit.GetAll(),
                batches = _bCSBatch.GetAll()
            };
            return View(data);
        }

        public async Task<IActionResult> GetDiciplinaryEmployeeList(int unitId, int rankId, int batchId)
        {
            var data = new EmployeeInfoViewModel
            {
                employeeId = unitId,
                ranks = _repoRank.GetAll(),
                units = _specialBranchUnit.GetAll(),
                bCSBatch = _bCSBatch.GetAll(),
                employeeInfos_SPs = await _employeeService.GetDiciplinaryEmployeeListBySp(rankId, unitId, batchId)
            };
            return Json(data);
        }

        public async Task<IActionResult> GetEmployeeDiciplinaryInfo(int empId)
        {
            var data = await _employeeService.GetDiciplinaryActionByEmpId(empId);
            return Json(data);
        }
        #endregion
        #region Medical
        [Authorize(Roles = "Admin,Super Admin,Medical")]
        public IActionResult EmployeeMedicalInfo()
        {
            var data = new DashBoardViewModel
            {
                rank = _repoRank.GetAll(),
                specialBranchUnits = _specialBranchUnit.GetAll(),
                batches = _bCSBatch.GetAll()
            };
            return View(data);
        }

        public async Task<IActionResult> GetMedicalEmployeeList(int unitId, int rankId, int batchId)
        {
            var data = new EmployeeInfoViewModel
            {
                employeeId = unitId,
                ranks = _repoRank.GetAll(),
                units = _specialBranchUnit.GetAll(),
                bCSBatch = _bCSBatch.GetAll(),
                employeeInfos_SPs = await _employeeService.GetMedicalInfoEmployeeListBySp(rankId, unitId, batchId)
            };
            return Json(data);
        }

        public async Task<IActionResult> GetEmployeeMedicalInfo(int empId)
        {
            var data = await _employeeService.GetMedicalInfosByEmpId(empId);
            return Json(data);
        }
        public async Task<IActionResult> GetEmployeeTrainingLog(int empId)
        {
            var data = await _employeeService.GetTraningLogInfoByEmpId(empId);
            return Json(data);
        }
        #endregion
        #region For User
        public async Task<IActionResult> EmployeeIndex()
        {
            var user = User.Identity.Name;
            var userInfo =await _userManager.FindByNameAsync(user);
            var usersRole = await _employeeService.GetUserRole(userInfo.Id);
            if (usersRole == "BPA")
            {
                ViewBag.RID = 1;
            }
            else if (usersRole == "SB")
            {
                ViewBag.RID = 2;
            }
            else if (usersRole == "Medical")
            {
                ViewBag.RID = 3;
            }
            else if (usersRole == "Traning1")
            {
                ViewBag.RID = 4;
            }
            else if (usersRole == "Disciplinary Action Entry Operator")
            {
                ViewBag.RID = 5;
            }
            else
            {
                ViewBag.RID = 0;
            }
            
            var data = new DashBoardViewModel
            {
                rank = _repoRank.GetAll(),
                specialBranchUnits = _specialBranchUnit.GetAll(),
                batches = _bCSBatch.GetAll()                
            };
            return View(data);
        }


        public async Task<IActionResult> GetEmployeeList(int unitId, int rankId, int batchId)
        {
            var user = User.Identity.Name;
            var data = await _employeeService.GetAllEmployeeList(rankId, unitId, batchId, user);
            return Json(data);
        }
        #endregion
    }
}