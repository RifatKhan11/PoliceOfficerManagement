using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.EmployeeService.Interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Domain.SMSService.interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Helpers;
using AlphaManagement.Web.Models;
using AlphaManagement.Web.PushNotification.Models;
using AlphaManagement.Web.PushNotification.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class AssignmentController : Controller
    {
        private readonly LangGenerate<AssignmentLn> _lang;
        private readonly IRepository<Assignment> _repoAssignment;
        private readonly IRepository<CancelAssignment> _repoCancelAssignment;
        private readonly IRepository<Designation> _repoDesignation;
        private readonly IRepository<Department> _repoDepartment;
        private readonly IRepository<SpecialBranchUnit> _repoUnit;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<SpecialSkillType> _repoSpecialSkillType;
        private readonly IRepository<ApprovalLog> _approvalLog;
        private readonly IRepository<EmployeeInfo> _repoEmployeeInfo;
        private readonly IRepository<BCSBatch> _repoBCSBatch;
        private readonly IRepository<AssignmentTransectionLog> _AssignmentTransectionLog;
        private readonly IEmployeeService _employeeService;
        private readonly IUserInfoes userInfoes;
        private readonly IAssignmentService _assignmentService;
        private readonly IRepository<AssignmentMaster> _assignmentMaster;
        private readonly IRepository<EnlistedAssignmentMaster> _EnlistedAssignmentMaster;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IPhotographService photographService;
        private readonly ISMSService _SMSService;
        private readonly IInternalPostingServices _internalServices;
        private readonly INotificationService _notificationService;
        private IMemoryCache cache;

        public AssignmentController(IHostingEnvironment hostingEnvironment, IRepository<Assignment> repoAssignment
            , IRepository<Department> repoDepartment,
            IRepository<SpecialBranchUnit> repoUnit,
            IRepository<Designation> repoDesignation,
            IRepository<EmployeeInfo> repoEmployeeInfo,
            IRepository<BCSBatch> repoBCSBatch,
            IRepository<AssignmentMaster> assignmentMaster, IRepository<CancelAssignment> _repoCancelAssignment,
            IRepository<Rank> repoRank,
            IRepository<ApprovalLog> _approvalLog,
            IRepository<EnlistedAssignmentMaster> _EnlistedAssignmentMaster,
            IRepository<AssignmentTransectionLog> _AssignmentTransectionLog,
            IEmployeeService employeeService,
            IUserInfoes userInfoes,
            IAssignmentService assignmentService,
            UserManager<ApplicationUser> userManager,
            IPhotographService photographService,
            IInternalPostingServices _internalServices,
            IMemoryCache memoryCache,
            IRepository<SpecialSkillType> _repoSpecialSkillType,
            ISMSService SMSService,
            INotificationService notificationService
            )
        {
            _lang = new LangGenerate<AssignmentLn>(hostingEnvironment.ContentRootPath);
            _repoAssignment = repoAssignment;
            this._repoCancelAssignment = _repoCancelAssignment;
            this._repoSpecialSkillType = _repoSpecialSkillType;
            this._EnlistedAssignmentMaster = _EnlistedAssignmentMaster;
            _repoDesignation = repoDesignation;
            _repoDepartment = repoDepartment;
            _repoRank = repoRank;
            _repoUnit = repoUnit;
            _repoBCSBatch = repoBCSBatch;
            _repoEmployeeInfo = repoEmployeeInfo;
            _employeeService = employeeService;
            _SMSService = SMSService;
            this.userInfoes = userInfoes;
            this._approvalLog = _approvalLog;
            this._AssignmentTransectionLog = _AssignmentTransectionLog;
            this._internalServices = _internalServices;
            _assignmentService = assignmentService;
            _assignmentMaster = assignmentMaster;
            _userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
            this.photographService = photographService;
            _notificationService = notificationService;
            cache = memoryCache;
        }


        public async Task<IActionResult> JobHistorySearch(string id)
        {

            try
            {
                if (id == string.Empty || id == null)
                {
                    id = User.Identity.Name;
                }

                //ApplicationUser applicationUser = await _userManager.FindByNameAsync(id);
                var userInfo = await userInfoes.GetUserInfoByUser(id);
                //var userInfo = await userInfoes.GetUserInfoBeforeRegisterById(Convert.ToInt32(user));
                EmployeeInfo employeeInfo = new EmployeeInfo();
                var empInfo = await userInfoes.GetUserInfoByUserId(userInfo.Id);
                if (empInfo == null)
                {
                    employeeInfo = new EmployeeInfo();
                }
                else
                {
                    employeeInfo = empInfo;
                }


                var model = new EmployeeInfoViewModel
                {
                    ApplicationUserId = userInfo.Id,
                    rank = _repoRank.GetAll(),
                    employeeInfo = employeeInfo,
                    specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Index(int id)
        {

            ViewBag.employeeId = id.ToString();
            var model = new AssignmentViewModel
            {
                fLang = _lang.PerseLang("Employee/AssignmentEN.json", "Employee/AssignmentBN.json", Request.Cookies["lang"]),
                assignments = _repoAssignment.GetAll(),
                designations = _repoDesignation.GetAll(),
                departments = _repoDepartment.GetAll(),
                specialBranchUnits = _repoUnit.GetAll(),
                ranks = _repoRank.GetAll(),
                employeeInfos = _repoEmployeeInfo.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] AssignmentViewModel model)
        {
            var Obj = new Assignment
            {
                Id = model.AssignmentId,
                employeeId = Convert.ToInt32(model.employeeId),
                assignmentTypeId = 1,
                EntryNo = model.EntryNo,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                designationId = model.designationId,
                departmentId = model.departmentId,
                Remarks = model.Remarks
            };
            _repoAssignment.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Super Admin,Admin,IGP,Sub-Admin")]
        public async Task<IActionResult> EmployeePosting(string refNo)
        {
            ViewBag.refNo = refNo;
            AssignmentViewModel model = new AssignmentViewModel
            {
                //specialBranchUnits=await _assignmentService.GetUnitWiseEmployeeInfo(),
                // specialBranchUnits=await _assignmentService.GetUnitWiseAllEmployeeInfos(),
                //branchUnitWiseEmployees = await _assignmentService.GetbranchUnitEmployeeInfosByRankUnit(0, 0, 0, 0,0),
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                bCSBatches = _repoBCSBatch.GetAll(),
                educations = await _assignmentService.GetEducationalQualifications(),
                specialSkillTypes = _repoSpecialSkillType.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> GetRankUnitWisePartialEmployeeView(int rankId, int unitId, int batch, int servicePeriod, int bandId)
        {
            try
            {
                var model = new AssignmentViewModel
                {
                    branchUnitWiseEmployees = await _assignmentService.GetbranchUnitEmployeeInfosByRankUnit(rankId, unitId, batch, servicePeriod, bandId),
                    //ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                    // specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                    // bCSBatches = _repoBCSBatch.GetAll(),
                    educations = await _assignmentService.GetEducationalQualifications()
                };
                return PartialView("_RankUnitWisePartialView", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetUnitRankWisePartialEmployeeView(int rankId, int unitId, int batch, int servicePeriod, int bandId, int isLocked, int isAttached, int isUnMission,int isPRL,int skillId)
        {
            try
            {
                var model = new AssignmentViewModel
                {
                    unitRankWiseEmployeeViewModels = await _assignmentService.GetUnitRankWiseEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission,isPRL,skillId),
                };
                return PartialView("_UnitRankWisePartialView", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> UnitRankOfficerListExportToExcel(int rankId, int unitId, int batch, int servicePeriod, int bandId, int isLocked, int isAttached, int isUnMission,int isPRL,int skillId)
        {
            DataTable dt = new DataTable("Officers_Info");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Unit"),
                                            new DataColumn("Sub Unit"),
                                            new DataColumn("BPNo"),
                                            new DataColumn("Officer Information"),
                                            new DataColumn("Expertise"),
                                            new DataColumn("Changed Unit"),
                                            new DataColumn("Changed Rank"),
                                            new DataColumn("Comment")});

            var employeeInfos = await _assignmentService.GetUnitRankWiseEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission,isPRL,skillId);

            foreach (var data in employeeInfos.Where(x => x.isParent == 1).GroupBy(x => new { x.unitId, x.mainUnit }).Select(x => new { unitId = x.Key.unitId, mainUnit = x.Key.mainUnit }))
            {
                dt.Rows.Add(data.mainUnit, "", "", "No Of Post= " + employeeInfos.Where(x => x.unitId == data.unitId).FirstOrDefault().numOfPost + ", Total Officer= " + employeeInfos.Where(x => x.unitId == data.unitId).FirstOrDefault().totalEmployee + ", Vacany= " + employeeInfos.Where(x => x.unitId == data.unitId).FirstOrDefault().vacantPost, "");
                foreach (var sitem in employeeInfos.Where(x => x.isParent == 1 && x.unitId == data.unitId && x.employeeId != 0))
                {
                    dt.Rows.Add("", "", sitem.employeeCode, sitem.name, sitem.expertise);
                }
            }

            foreach (var data in employeeInfos.Where(x => x.isParent == 0).GroupBy(x => new { x.specialBranchUnitId, x.mainUnit }).Select(x => new { unitId = x.Key.specialBranchUnitId, mainUnit = x.Key.mainUnit }))
            {
                int? nop = 0;
                int? temp = 0;
                int? vacant = 0;
                foreach (var uu in employeeInfos.Where(x => x.isParent == 0 && x.specialBranchUnitId == data.unitId).GroupBy(x => new { x.unitId, x.subUnit }).Select(x => new { unitId = x.Key.unitId, subUnit = x.Key.subUnit }))
                {
                    nop = nop + employeeInfos.Where(x => x.unitId == uu.unitId).FirstOrDefault().numOfPost;
                    temp = temp + employeeInfos.Where(x => x.unitId == uu.unitId).FirstOrDefault().totalEmployee;
                    vacant = nop - temp;
                }
                dt.Rows.Add(data.mainUnit, "", "", "No Of Post= " + nop + ", Total Officer= " + temp + ", Vacany= " + vacant, "");
                foreach (var subu in employeeInfos.Where(x => x.isParent == 0 && x.specialBranchUnitId == data.unitId).GroupBy(x => new { x.unitId, x.subUnit }).Select(x => new { unitId = x.Key.unitId, subUnit = x.Key.subUnit }))
                {
                    if (employeeInfos.Where(x => x.isParent == 0 && x.unitId == subu.unitId).Count() == 0)
                    {
                        dt.Rows.Add("", subu.subUnit, "", "No Of Post= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().numOfPost + ", Total Officer= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().totalEmployee + ", Vacany= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().vacantPost, "");
                    }
                    else
                    {
                        dt.Rows.Add("", subu.subUnit, "", "No Of Post= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().numOfPost + ", Total Officer= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().totalEmployee + ", Vacany= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().vacantPost, "");
                        foreach (var sitem in employeeInfos.Where(x => x.isParent == 0 && x.unitId == subu.unitId && x.employeeId > 0))
                        {
                            dt.Rows.Add("", "", sitem.employeeCode, sitem.name, sitem.expertise);
                        }
                    }
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BP_OfficerInformation.xlsx");
                }
            }
        }


        public async Task<IActionResult> ExportToExcel()
        {
            DataTable dt = new DataTable("Officers_Info");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Unit"),
                                            new DataColumn("Sub Unit"),
                                            new DataColumn("BPNo"),
                                            new DataColumn("Officer Information"),
                                            new DataColumn("Expertise"),
                                            new DataColumn("Changed Unit"),
                                            new DataColumn("Changed Rank"),
                                            new DataColumn("Comment")});

            var employeeInfos = await _assignmentService.GetUnitRankWiseEmployeeListExportToExcel();

            foreach (var data in employeeInfos.Where(x => x.isParent == 1).GroupBy(x => new { x.unitId, x.mainUnit }).Select(x => new { unitId = x.Key.unitId, mainUnit = x.Key.mainUnit }))
            {
                dt.Rows.Add(data.mainUnit, "", "", "No Of Post= " + employeeInfos.Where(x => x.unitId == data.unitId).FirstOrDefault().numOfPost + ", Total Officer= " + employeeInfos.Where(x => x.unitId == data.unitId).FirstOrDefault().totalEmployee + ", Vacany= " + employeeInfos.Where(x => x.unitId == data.unitId).FirstOrDefault().vacantPost, "");
                foreach (var sitem in employeeInfos.Where(x => x.isParent == 1 && x.unitId == data.unitId && x.employeeId != 0))
                {
                    dt.Rows.Add("", "", sitem.employeeCode, sitem.name, sitem.expertise);
                }
            }

            foreach (var data in employeeInfos.Where(x => x.isParent == 0).GroupBy(x => new { x.specialBranchUnitId, x.mainUnit }).Select(x => new { unitId = x.Key.specialBranchUnitId, mainUnit = x.Key.mainUnit }))
            {
                int? nop = 0;
                int? temp = 0;
                int? vacant = 0;
                foreach (var uu in employeeInfos.Where(x => x.isParent == 0 && x.specialBranchUnitId == data.unitId).GroupBy(x => new { x.unitId, x.subUnit }).Select(x => new { unitId = x.Key.unitId, subUnit = x.Key.subUnit }))
                {
                    nop = nop + employeeInfos.Where(x => x.unitId == uu.unitId).FirstOrDefault().numOfPost;
                    temp = temp + employeeInfos.Where(x => x.unitId == uu.unitId).FirstOrDefault().totalEmployee;
                    vacant = nop - temp;
                }
                dt.Rows.Add(data.mainUnit, "", "", "No Of Post= " + nop + ", Total Officer= " + temp + ", Vacany= " + vacant, "");
                foreach (var subu in employeeInfos.Where(x => x.isParent == 0 && x.specialBranchUnitId == data.unitId).GroupBy(x => new { x.unitId, x.subUnit }).Select(x => new { unitId = x.Key.unitId, subUnit = x.Key.subUnit }))
                {
                    if (employeeInfos.Where(x => x.isParent == 0 && x.unitId == subu.unitId).Count() == 0)
                    {
                        dt.Rows.Add("", subu.subUnit, "", "No Of Post= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().numOfPost + ", Total Officer= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().totalEmployee + ", Vacany= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().vacantPost, "");
                    }
                    else
                    {
                        dt.Rows.Add("", subu.subUnit, "", "No Of Post= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().numOfPost + ", Total Officer= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().totalEmployee + ", Vacany= " + employeeInfos.Where(x => x.unitId == subu.unitId).FirstOrDefault().vacantPost, "");
                        foreach (var sitem in employeeInfos.Where(x => x.isParent == 0 && x.unitId == subu.unitId && x.employeeId > 0))
                        {
                            dt.Rows.Add("", "", sitem.employeeCode, sitem.name, sitem.expertise);
                        }
                    }
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BP_OfficerInformation.xlsx");
                }
            }
        }

        public async Task<IActionResult> GetNewSuggetionList(int id, int rankId,int pageNumber,int pageSize, string refNum)
        {

            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            AssignmentViewModel model = new AssignmentViewModel
            {
                suggestedBrancheds = await _assignmentService.GetSuggestedPlaceForEmp(id, pageNumber, pageSize, ""),
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
            };
            return PartialView("_RankUnitWisePartialViewSuggested", model);
        }

        public async Task<IActionResult> GetNewSuggetionList2(int id, int rankId, int pageNumber, int pageSize, string refNum)
        {

            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            AssignmentViewModel model = new AssignmentViewModel
            {
                suggestedBrancheds = await _assignmentService.GetSuggestedPlaceForEmp(id, pageNumber, pageSize, ""),
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
            };
            return PartialView("_RankUnitWisePartialViewSuggestedNew", model);
        }

        public async Task<IActionResult> GetSuggetionList(int id, int rankId, string refNum)
        {

            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            AssignmentViewModel model = new AssignmentViewModel
            {
                suggestedBrancheds = await _assignmentService.GetSuggestedBranched(id, ""),
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
            };
            return PartialView("_RankUnitWisePartialViewSuggested", model);
        }

        public async Task<IActionResult> GetSuggetionNewList(int id, int rankId, string refNum)
        {

            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            AssignmentViewModel model = new AssignmentViewModel
            {
                suggestedBrancheds = await _assignmentService.GetSuggestedBranched(id, ""),
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
            };
            return PartialView("_RankUnitWisePartialViewSuggestedNew", model);
        }

        public async Task<IActionResult> SaveMoveToInfoFromOngoing(int detailId, int sectionId)
        {
            var enListed = await _assignmentService.GetAssignmentByIdSingle(detailId);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (enListed != null)
            {
                AssignmentTransectionLog data = new AssignmentTransectionLog
                {
                    assignmentId = enListed.Id,
                    oldSectionId = enListed.sectionId,
                    newSectionId = sectionId,
                    StartDate = DateTime.Now,
                    UpdateUserId = user.Id
                };
                _AssignmentTransectionLog.Insert(data);

                enListed.sectionId = sectionId;
                _repoAssignment.Update(enListed);
            }

            return Json("success");
        }


        public async Task<IActionResult> GetMoveToList(string refNum, int empId)
        {
            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            var data = new List<BranchUnitWiseEmployeesModel>();
            if (!cache.TryGetValue(refNum, out data))
            {
                data = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0, 0);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(700));

                cache.Set(refNum, data, cacheEntryOptions);
            }

            AssignmentViewModel model = new AssignmentViewModel
            {
                assignmentPostingModels = await _assignmentService.GetAssignmentPostingInfo(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId)),
                branchUnitWiseEmployees = data,
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
                presentUnits = await _employeeService.GetEmployeePresentUnit(empId),
                previousUnits = await _employeeService.GetEmployeePreviousUnit(empId),
                spouseUnits = await _employeeService.GetEmployeeSpouseUnit(empId),
                homeDistUnits = await _employeeService.GetEmployeePermanentAddUnit(empId)
            };
            return PartialView("_GetMoveToList", model);
        }

        public async Task<IActionResult> GetMoveToNewList(string refNum, int empId)
        {
            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            var data = new List<BranchUnitWiseEmployeesModel>();
            if (!cache.TryGetValue(refNum, out data))
            {
                data = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0, 0);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(700));

                cache.Set(refNum, data, cacheEntryOptions);
            }

            AssignmentViewModel model = new AssignmentViewModel
            {
                assignmentPostingModels = await _assignmentService.GetAssignmentPostingInfo(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId)),
                branchUnitWiseEmployees = data,
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
                presentUnits = await _employeeService.GetEmployeePresentUnit(empId),
                previousUnits = await _employeeService.GetEmployeePreviousUnit(empId),
                spouseUnits = await _employeeService.GetEmployeeSpouseUnit(empId),
                homeDistUnits = await _employeeService.GetEmployeePermanentAddUnit(empId)
            };
            return PartialView("_GetMoveToNewList", model);
        }

        public async Task<IActionResult> GetMoveToListOngoing(string refNum, int empId)
        {
            var enListed = await _assignmentService.GetEnlistedAssignmentOngoing(refNum);
            var empRnk = _employeeService.GetBasicEmployeeInfoById(empId);

            var data = new List<BranchUnitWiseEmployeesModel>();
            if (!cache.TryGetValue(refNum, out data))
            {
                data = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(empRnk.rankId), 0, 0);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(700));

                cache.Set(refNum, data, cacheEntryOptions);
            }

            AssignmentViewModel model = new AssignmentViewModel
            {
                branchUnitWiseEmployees = data,
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
                presentUnits = await _employeeService.GetEmployeePresentUnit(empId),
                homeDistUnits = await _employeeService.GetEmployeePermanentAddUnit(empId),
                previousUnits = await _employeeService.GetEmployeePreviousUnit(empId),
                spouseUnits = await _employeeService.GetEmployeeSpouseUnit(empId),
            };
            return PartialView("_GetMoveToList", model);
        }


        public async Task<IActionResult> EmployeePostingConfirm(string refNum, int? assignMasterId)
        {
            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            IEnumerable<EnlistedAssignment> lstEnlisted = new List<EnlistedAssignment>();
            if (enListed.Count() == 0)
            {
                lstEnlisted = new List<EnlistedAssignment>();
            }
            else
            {
                lstEnlisted = enListed;
            }
            var assignEmp = await _assignmentService.GetAssignmentByRef(refNum);
            //var data = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0, 0);

            AssignmentViewModel model = new AssignmentViewModel
            {

                //branchUnitWiseEmployees = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0, 0),
                assignmentPostingModels = await _assignmentService.GetAssignmentPostingInfo(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId)),
                enlistedAssignments = lstEnlisted,
                sections = await _employeeService.GetSectionWiseUnit(),
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                refNumber = refNum,
                assignMasterId = assignMasterId,
                enlistedId = lstEnlisted.FirstOrDefault().enlistedAssignmentMasterId,
                assignments = assignEmp,
                educations = await _assignmentService.GetEducationalQualifications()
            };
            //return Json(data);
            return View(model);
        }

        public async Task<IActionResult> EmployeePostingConfirmNew(string refNum, int? assignMasterId)
        {
            var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            IEnumerable<EnlistedAssignment> lstEnlisted = new List<EnlistedAssignment>();
            if (enListed.Count() == 0)
            {
                lstEnlisted = new List<EnlistedAssignment>();
            }
            else
            {
                lstEnlisted = enListed;
            }
            var assignEmp = await _assignmentService.GetAssignmentByRef(refNum);
            //var data = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0, 0);

            AssignmentViewModel model = new AssignmentViewModel
            {

                //branchUnitWiseEmployees = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0, 0),
                assignmentPostingModels = await _assignmentService.GetAssignmentPostingInfo(refNum, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId)),
                enlistedAssignments = lstEnlisted,
                sections = await _employeeService.GetSectionWiseUnit(),
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                refNumber = refNum,
                assignMasterId = assignMasterId,
                enlistedId = lstEnlisted.FirstOrDefault().enlistedAssignmentMasterId,
                badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                assignments = assignEmp,
            };
            return View(model);
        }

        public async Task<IActionResult> EditEmployeePosting(string refNum, int? assignMasterId)
        {
            try
            {
                IEnumerable<EnlistedAssignment> enListed = new List<EnlistedAssignment>();
                enListed = await _assignmentService.GetReturnedEnlistedAssignment(refNum);
                var assignEmp = await _assignmentService.GetAssignmentByRef(refNum);

                AssignmentViewModel model = new AssignmentViewModel
                {
                    branchUnitWiseEmployees = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(enListed?.FirstOrDefault()?.employee?.rankId), 0, 0),
                    //branchUnitWiseEmployees = await _assignmentService.GetbranchUnitEmployeeInfosByRankUnit(Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0,0),
                    enlistedAssignments = enListed,
                    sections = await _employeeService.GetSectionWiseUnit(),
                    ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                    refNumber = refNum,
                    assignMasterId = assignMasterId,
                    enlistedId = enListed?.FirstOrDefault()?.enlistedAssignmentMasterId,
                    assignments = assignEmp,
                    educations = await _assignmentService.GetEducationalQualifications()
                };
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> EditEmployeePostingConfirm(int assignDetailsId)
        {
            ViewBag.Id = assignDetailsId;
            var enListed = await _assignmentService.GetAssignmentById(assignDetailsId);
            AssignmentViewModel model = new AssignmentViewModel
            {
                branchUnitWiseEmployees = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(enListed.assignmentMaster.refNo, Convert.ToInt32(enListed.employee.rankId), 0, 0),
                assignment = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                educations = await _assignmentService.GetEducationalQualifications(),
                assignments = await _assignmentService.GetAssignmentByRef(enListed.assignmentMaster.refNo),
                refNumber = enListed.assignmentMaster.refNo,
                assignMasterId = enListed.assignmentMasterId,
                enlistedId = enListed.Id
            };
            return View(model);
        }

        public async Task<IActionResult> NewAddedEmployeePostingConfirm(string refNo)
        {
            try
            {
                var enListed = await _assignmentService.GetNewEnlistedByRefNo(refNo);
                var assignments = await _assignmentService.GetAssignmentByRef(refNo);
                AssignmentViewModel model = new AssignmentViewModel
                {
                    branchUnitWiseEmployees = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNo, Convert.ToInt32(enListed.FirstOrDefault().employee.rankId), 0, 0),
                    enlistedAssignments = enListed,
                    sections = await _employeeService.GetSectionWiseUnit(),
                    ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                    educations = await _assignmentService.GetEducationalQualifications(),
                    assignments = assignments,
                    refNumber = refNo,
                    assignMasterId = assignments.FirstOrDefault().assignmentMasterId,
                    enlistedId = enListed.FirstOrDefault().enlistedAssignmentMasterId
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> MultipleEditEmployeePostingConfirm(string array, string refNum, int assignMasterId)
        {
            var arr = array.Split(",");
            foreach (var item in arr)
            {
                var enlistUpdate = await _assignmentService.UpdateEnlistStatus(refNum, Convert.ToInt32(item));
            }
            return RedirectToAction("EmployeePostingConfirm", "Assignment", new { refNum = refNum, assignMasterId = assignMasterId, area = "Employee" });
        }

        [HttpPost]
        public IActionResult DeleteAssignment(int id)
        {
            bool response;

            try
            {
                _repoAssignment.Delete(_repoAssignment.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> InListInfo([FromForm] InRoleInfoViewModel model)
        {
            var refNum = "";
            var year = DateTime.Now.Date.Year;
            var month = DateTime.Now.Date.Month;
            var masterDetails = new EnlistedAssignmentMaster();
            string rand = RandomString(3);
            refNum = "BP-" + year + "-" + month + "-" + rand;
            var applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.empId != null)
            {
                var masterAssignment = new EnlistedAssignmentMaster
                {
                    Id = 0,
                    refNo = refNum,
                    refDate = DateTime.Now.Date,
                    typeId = 1,
                    statusId = 1,
                    ApplicationUserId = applicationUser.Id
                };
                int masterId = 0;
                if (model.refNo == null)
                {
                    masterId = await _employeeService.SaveEnlistedMasterAssignment(masterAssignment);
                }
                else
                {
                    masterDetails = await _assignmentService.GetEnlistedMasterByRefNo(model.refNo);
                    masterId = masterDetails.Id;
                }
                for (int i = 0; i < model.empId.Length; i++)
                {
                    var assignment = new EnlistedAssignment
                    {
                        Id = 0,
                        employeeId = model.empId[i],
                        statusId = 1,
                        typeId = 1,
                        rankId = model.empRank[i],
                        enlistedAssignmentMasterId = masterId
                    };
                    if (model.refNo != null)
                    {
                        assignment.typeId = 2;
                    }
                    var save = await _employeeService.SaveEnlistedAssignment(assignment);
                }
                if (model.refNo == null)
                {
                    var log = await _employeeService.SaveAlphaTransectionHistoryLog(12, applicationUser.Id, masterId, "Alpha", null);
                    var data = new PostingSaveViewModel
                    {
                        refNumber = refNum,
                        status = 1
                    };
                    return Json(data);
                    //return Json(refNum);
                }
                else
                {
                    var log = await _employeeService.SaveAlphaTransectionHistoryLog(12, applicationUser.Id, masterId, "Alpha", "IGP ");
                    var data = new PostingSaveViewModel
                    {
                        refNumber = masterDetails.refNo,
                        status = 2
                    };
                    return Json(data);
                    //return RedirectToAction("NewAddedEmployeePostingConfirm", new { refNo = masterDetails.refNo});
                }
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePostingData([FromForm] PostingViewModel model)
        {

            var refNum = model.referenceNum;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.employeeId != null)
            {
                try
                {
                    if (refNum == "FrezzListEmpty")
                    {
                        refNum = await _assignmentService.GetAssignRefNoIdByMasterId(model.AssignmentId);
                        if (refNum == null)
                        {
                            refNum = await _assignmentService.GetAssignRefNoIdByEnlistId(model.enlistedId);
                        }
                    }
                    string userId = user.Id;
                    if (model.AssignmentId > 0)
                    {
                        var assignInfo = await _assignmentService.GetAssignmentMasterById(model.AssignmentId);
                        userId = assignInfo.applicationUserId;
                    }
                    var assignmentMaster = new AssignmentMaster
                    {
                        Id = model.AssignmentId,
                        refNo = refNum,
                        refDate = DateTime.Now.Date,
                        applicationUserId = userId,
                        statusId = 1
                    };
                    var assignMasterId = await _employeeService.SaveAssignMaster(assignmentMaster);
                    for (int i = 0; i < model.employeeId.Length; i++)
                    {
                        var assignmentDetails = await _employeeService.GetAssignmentByRefNoEmpId(refNum, model.employeeId[i]);
                        var unit = await _assignmentService.GetSpecialBranchUnitBySectionId(model.sectionId[i]);
                        var emp = await _employeeService.GetEmployeeInfosByEmpId(model.employeeId[i]);
                        var assignDetailsId = 0;
                        if (assignmentDetails == null)
                        {
                            assignmentDetails = new Assignment();
                            assignmentDetails.Id = 0;
                        }
                        assignmentDetails.employeeId = model.employeeId[i];
                        assignmentDetails.StartDate = DateTime.Now.Date;
                        assignmentDetails.assignmentMasterId = assignMasterId;
                        assignmentDetails.sectionId = model.sectionId[i];
                        assignmentDetails.Remarks = null;
                        assignmentDetails.statusId = 1;
                        if (unit > 0)
                        {
                            assignmentDetails.specialBranchUnitId = unit;
                        }
                        if (emp != null)
                        {
                            assignmentDetails.rankId = emp.rankId;
                        }
                        assignDetailsId = await _employeeService.SaveAssignDetails(assignmentDetails);
                    }
                    var data = new PostingSaveViewModel
                    {
                        assignMasterId = assignMasterId,
                        refNumber = model.referenceNum
                    };
                    cache.Remove(model.referenceNum);
                    var log = await _employeeService.SaveAlphaTransectionHistoryLog(13, user.Id, assignMasterId, "Alpha", null);

                    return Json(data);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                var data = new PostingSaveViewModel
                {
                    assignMasterId = model.AssignmentId,
                    refNumber = model.referenceNum
                };
                return Json(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewPostingData([FromForm] PostingViewModel model)
        {

            var refNum = model.referenceNum;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.employeeId != null)
            {
                try
                {
                    var enlistDetails = await _employeeService.GetEnlistedListByRef(model.referenceNum);
                    var enmaster = _EnlistedAssignmentMaster.Get((int)enlistDetails.FirstOrDefault().enlistedAssignmentMasterId);
                    enmaster.statusId = 2;
                    _EnlistedAssignmentMaster.Update(enmaster);
                    for (int i=0;i<model.employeeId.Length;i++)
                    {
                        if (!enlistDetails.Any(x=>x.employeeId== model.employeeId[i]))
                        {
                            var enlistD = new EnlistedAssignment
                            {
                                Id = 0,
                                enlistedAssignmentMasterId = enlistDetails.FirstOrDefault().enlistedAssignmentMasterId,
                                employeeId= model.employeeId[i],
                                statusId=1,
                                typeId =1,
                                rankId = enlistDetails.FirstOrDefault().rankId
                            };
                            var save=await _employeeService.SaveEnlistedAssignment(enlistD);
                        }
                    }
                    string userId = user.Id;                    
                    var assignmentMaster = new AssignmentMaster
                    {
                        Id = model.AssignmentId,
                        refNo = refNum,
                        refDate = DateTime.Now.Date,
                        applicationUserId = userId,
                        statusId = 1
                    };
                    var assignMasterId = await _employeeService.SaveAssignMaster(assignmentMaster);
                    for (int i = 0; i < model.employeeId.Length; i++)
                    {
                        var assignmentDetails = await _employeeService.GetAssignmentByRefNoEmpId(refNum, model.employeeId[i]);
                        var unit = await _assignmentService.GetSpecialBranchUnitBySectionId(model.sectionId[i]);
                        var emp = await _employeeService.GetEmployeeInfosByEmpId(model.employeeId[i]);
                        var assignDetailsId = 0;
                        if (assignmentDetails == null)
                        {
                            assignmentDetails = new Assignment();
                            assignmentDetails.Id = 0;
                        }
                        assignmentDetails.employeeId = model.employeeId[i];
                        assignmentDetails.StartDate = DateTime.Now.Date;
                        assignmentDetails.assignmentMasterId = assignMasterId;
                        assignmentDetails.sectionId = model.sectionId[i];
                        assignmentDetails.Remarks = null;
                        assignmentDetails.statusId = 1;
                        if (unit > 0)
                        {
                            assignmentDetails.specialBranchUnitId = unit;
                        }
                        if (emp != null)
                        {
                            assignmentDetails.rankId = emp.rankId;
                        }
                        assignDetailsId = await _employeeService.SaveAssignDetails(assignmentDetails);
                    }
                    var data = new PostingSaveViewModel
                    {
                        assignMasterId = assignMasterId,
                        refNumber = model.referenceNum
                    };
                    cache.Remove(model.referenceNum);
                    var log = await _employeeService.SaveAlphaTransectionHistoryLog(13, user.Id, assignMasterId, "Alpha", null);

                    return Json(data);
                }
                catch (Exception ex)
                {
                    return Json("Fail");
                }
            }
            else
            {
                var data = new PostingSaveViewModel
                {
                    assignMasterId = model.AssignmentId,
                    refNumber = model.referenceNum
                };
                return Json(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveAddedPostingData([FromForm] PostingViewModel model)
        {
            var refNum = model.referenceNum;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.employeeId != null)
            {
                try
                {                    
                    for (int i = 0; i < model.employeeId.Length; i++)
                    {
                        var assignmentDetails = await _employeeService.GetAssignmentByRefNoEmpId(refNum, model.employeeId[i]);
                        var unit = await _assignmentService.GetSpecialBranchUnitBySectionId(model.sectionId[i]);
                        var assignDetailsId = 0;
                        if (assignmentDetails == null)
                        {
                            assignmentDetails = new Assignment();
                            assignmentDetails.Id = 0;
                        }
                        assignmentDetails.employeeId = model.employeeId[i];
                        assignmentDetails.StartDate = DateTime.Now.Date;
                        assignmentDetails.assignmentMasterId = model.AssignmentId;
                        assignmentDetails.sectionId = model.sectionId[i];
                        assignmentDetails.Remarks = null;
                        assignmentDetails.statusId = 1;
                        if (unit > 0)
                        {
                            assignmentDetails.specialBranchUnitId = unit;
                        }
                        assignDetailsId = await _employeeService.SaveAssignDetails(assignmentDetails);
                        var updateEnlist = await _employeeService.UpdateTypeEnlistedAssignment(model.employeeId[i], model.referenceNum);
                    }
                    var master = await _assignmentService.GetAssignmentMasterById(model.AssignmentId);
                    var data = new PostingSaveViewModel
                    {
                        assignMasterId = model.AssignmentId,
                        refNumber = "FrezzListEmpty",
                        status = (int)master.statusId,
                    };
                    cache.Remove(model.referenceNum);
                    var log = await _employeeService.SaveAlphaTransectionHistoryLog(13, user.Id, model.AssignmentId, "Alpha", null);

                    return Json(data);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                var data = new PostingSaveViewModel
                {
                    assignMasterId = model.AssignmentId,
                    refNumber = model.referenceNum
                };
                return Json(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePostingData([FromForm] PostingViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.employeeId != null)
            {
                try
                {
                    var master = await _employeeService.GetAssignmentsMasterIdByDeatilsId(model.AssignmentId);
                    for (int i = 0; i < model.employeeId.Length; i++)
                    {
                        var assignmentDetails = new Assignment
                        {
                            Id = model.AssignmentId,
                            employeeId = model.employeeId[i],
                            StartDate = DateTime.Now.Date,
                            assignmentMasterId = master,
                            sectionId = model.sectionId[i]
                        };
                        var assignDetailsId = await _employeeService.SaveAssignDetails(assignmentDetails);
                    }
                    return Json(master);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveFrezzPostingData([FromForm] PostingViewModel model)
        {
            var refNum = model.referenceNum;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            try
            {
                if (model.referenceNum != null)
                {
                    //var delete = await _employeeService.DeleteEnListedList(model.referenceNum);
                    var updateElistedStatus = await _employeeService.UpdateEnListedEmplyeeStatus(model.referenceNum);
                }
                if (model.employeeId != null || model.frezzEmployeeId != null)
                {
                    //var assignmentMaster = new EnlistedAssignmentMaster
                    //{
                    //    Id = 0,
                    //    refNo = refNum,
                    //    refDate = DateTime.Now.Date,
                    //    typeId = 2,
                    //    statusId = 1,
                    //    ApplicationUserId = user.Id
                    //};
                    //var masterId = await _employeeService.SaveEnlistedMasterAssignment(assignmentMaster);
                    var masterId = await _employeeService.GetEnlistedMasterIdByRef(model.referenceNum);
                    if (model.employeeId != null)
                    {
                        for (int i = 0; i < model.employeeId.Length; i++)
                        {
                            var assignment = new EnlistedAssignment
                            {
                                Id = 0,
                                employeeId = model.employeeId[i],
                                statusId = 1,
                                typeId = 2,
                                enlistedAssignmentMasterId = masterId
                            };
                            var check = await _assignmentService.CheckEnlistAndRemoveAssign(masterId, model.employeeId[i]);
                            if (check != null)
                            {
                                check.statusId = 1;
                                check.typeId = 2;
                                var save = _employeeService.SaveEnlistedAssignment(check);
                            }
                            else
                            {
                                var save = await _employeeService.SaveEnlistedAssignment(assignment);
                            }
                        }
                    }
                    if (model.frezzEmployeeId != null)
                    {
                        for (int i = 0; i < model.frezzEmployeeId.Length; i++)
                        {
                            var assignment = new EnlistedAssignment
                            {
                                Id = 0,
                                employeeId = model.frezzEmployeeId[i],
                                statusId = 1,
                                typeId = 2,
                                enlistedAssignmentMasterId = masterId
                            };
                            var check = await _assignmentService.CheckEnlistAndRemoveAssign(masterId, model.frezzEmployeeId[i]);
                            if (check != null)
                            {
                                check.statusId = 1;
                                check.typeId = 2;
                                var save = await _employeeService.SaveEnlistedAssignment(check);
                            }
                            else
                            {
                                var save = await _employeeService.SaveEnlistedAssignment(assignment);
                            }

                        }
                    }
                    var log = await _employeeService.SaveAlphaTransectionHistoryLog(12, user.Id, masterId, "Alpha", "Frezz emplyee added for");
                    return Json(refNum);
                }
                else
                {
                    return Json("FrezzListEmpty");
                }
            }
            catch (Exception ex)
            {
                return Json("Fail");
            }

        }

        public async Task<IActionResult> DeleteEnlistedAssignment(int id)
        {
            var delete = await _employeeService.DeleteEnlistedAssignment(id);
            return Json(delete);
        }

        #region Complete posting
        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> CompletePostingByAssignMaster(int id)
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
                var assignmentMaster = _assignmentMaster.Get(id);
                var assignmentMasterDetails = await _assignmentService.GetAssignmentDetailsByMasterId(id);
                foreach (var assignment in assignmentMasterDetails)
                {
                    assignment.statusId = 3;
                    _repoAssignment.Update(assignment);
                    var emp = await _employeeService.GetEmployeeProfileInfoById(assignment.employeeId);
                    emp.sectionId = assignment.sectionId;
                    emp.branchId = assignment.section.specialBranchUnitId;
                    _repoEmployeeInfo.Update(emp);
                }
                assignmentMaster.statusId = 20;
                _assignmentMaster.Update(assignmentMaster);
                var log = await _employeeService.SaveAlphaTransectionHistoryLog(20, applicationUser.Id, id, "Alpha", "Completed");
                return RedirectToAction("CompletedPostingList");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> CompletedPostingList()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }
            var model = new PostingReportViewModel
            {
                postingReportViews = await _employeeService.CompleteAssignmentMasters(userId)
            };
            return View(model);
        }
        #endregion

        #region Partial View

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveImage(string photo, string signature, int? empId)
        {

            try
            {
                if (photo.Contains("data:image/"))
                {
                    var t = "";
                    if (photo.Contains("data:image/png"))
                    {
                        t = photo.Substring(22);
                    }
                    else
                    {
                        t = photo.Substring(23);
                    }
                    //var t = photo.Substring(23);  // remove data:image/png;base64,

                    byte[] bytes = Convert.FromBase64String(t);

                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                    }
                    var randomFileName = Guid.NewGuid().ToString().Substring(0, 8) + ".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmpImages", randomFileName);
                    var size = new Size(300, 300);
                    var i2 = new Bitmap(image, size);
                    i2.Save(path, System.Drawing.Imaging.ImageFormat.Png);

                    var profilePhoto = await photographService.GetPhotographByType((int)empId, "profile");
                    if (profilePhoto != null)
                    {
                        profilePhoto.url = "EmpImages/" + randomFileName;
                        var save = await photographService.SavePhotograph(profilePhoto);
                    }
                    else
                    {
                        Photograph photograph = new Photograph
                        {
                            Id = 0,
                            employeeId = (int)empId,
                            url = "EmpImages/" + randomFileName,
                            type = "profile"
                        };
                        var save = await photographService.SavePhotograph(photograph);
                    }


                    // await photographService.DeleteempId(model.employeeID);

                }

                if (signature.Contains("data:image/"))
                {
                    var t = "";
                    if (signature.Contains("data:image/png"))
                    {
                        t = signature.Substring(22);
                    }
                    else
                    {
                        t = signature.Substring(23);
                    }  // remove data:image/png;base64,

                    byte[] bytes = Convert.FromBase64String(t);

                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                    }
                    var randomFileName = Guid.NewGuid().ToString().Substring(0, 8) + ".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmpImages", randomFileName);
                    var size = new Size(300, 80);
                    var i2 = new Bitmap(image, size);
                    i2.Save(path, System.Drawing.Imaging.ImageFormat.Png);

                    var profilePhoto = await photographService.GetPhotographByType((int)empId, "signature");
                    if (profilePhoto != null)
                    {
                        profilePhoto.url = "EmpImages/" + randomFileName;
                        var save = await photographService.SavePhotograph(profilePhoto);
                    }
                    else
                    {
                        Photograph photograph = new Photograph
                        {
                            Id = 0,
                            employeeId = (int)empId,
                            url = "EmpImages/" + randomFileName,
                            type = "signature"
                        };
                        var save = await photographService.SavePhotograph(photograph);
                    }
                }
                return Json("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }





        }

        public async Task<IActionResult> GetSuggestedPartialView(int empId)
        {
            string userName = User.Identity.Name;
            AssignmentViewModel model = new AssignmentViewModel
            {
                suggestedUnits = await _assignmentService.GetSugesstetUnit(empId, userName)
            };

            return PartialView("_SuggestedList", model);
        }

        #endregion

        #region ApiSettings

        public async Task<JsonResult> GetRankWiseEmployeInfo(int rankid)
        {
            var result = await _assignmentService.GetbranchUnitEmployeeInfosByRank(rankid);

            return Json(result);
        }


        [Route("Employee/Assignment/GetUnitWiseEmployeInfo")]
        public async Task<JsonResult> GetUnitWiseEmployeInfo()
        {
            var result = await _assignmentService.GetUnitWiseEmployeeInfo();
            return Json(result);
        }

        [Route("Employee/Assignment/GetEmployeeInfoWithFilter")]
        public async Task<JsonResult> GetEmployeeInfoWithFilter()
        {
            var result = await _assignmentService.GetUnitWiseEmployeeInfos();
            return Json(result);
        }

        [Route("Employee/Assignment/GetRankWiseEmployeeInfo")]
        public async Task<JsonResult> GetRankWiseEmployeeInfo()
        {
            var result = await _assignmentService.GetRankWiseEmployeeInfo();
            return Json(result);
        }

        [Route("Employee/Assignment/GetSugesstetUnit/{employeeId}")]
        public async Task<JsonResult> GetSugesstetUnit(int employeeId)
        {
            string userName = User.Identity.Name;
            var result = await _assignmentService.GetSugesstetUnit(employeeId, userName);
            return Json(result);
        }

        [HttpGet]
        public IActionResult PoliceSubunits()
        {
            var model = new AssignmentViewModel
            {
                specialBranchUnits = _repoUnit.GetAll(),

            };

            return Json(model);
        }


        public async Task<JsonResult> GetUnitWiseEmployeeInfos()
        {
            var result = await _assignmentService.GetUnitWiseEmployeeInfos();
            return Json(result);
        }


        #endregion

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IActionResult> CheckDistrict(int branchId, int employeeid)
        {
            var data = await _employeeService.CheckDistrictTypeBySecIdAndEmpId(branchId, employeeid);
            return Json(data);
        }

        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignmentMaster(int id)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }
            var emp = await userInfoes.GetUserInfoByUserName(User.Identity.Name);
            var data = new PostingReportViewModel
            {
                //postingReportViews = await _employeeService.AssignmentMasters(userId),
                postingReportViews = await userInfoes.GetRankWiseAssignmentCopyOngoing((int)emp.rankId),
                InternalpostingReportViews = await userInfoes.GetRankWiseInternalAssignmentCopyOngoing((int)emp.rankId),
            };
            return View(data);
        }

        [Authorize(Roles = "IGP")]
        [HttpGet]
        public async Task<IActionResult> AssignmentMasterLocked(int id)
        {
            var data = new PostingReportViewModel
            {
                postingReportViews = await _employeeService.AssignmentMasterLocked(3),
                Internalassignment2 = await _internalServices.ApprovedInternalAssignmentMastersListForIgp(4),
            };
            return View(data);
        }

        [Authorize(Roles = "IGP")]
        [HttpGet]
        public async Task<IActionResult> AssignmentMasterReturnFromIGP(int id)
        {
            var data = new PostingReportViewModel
            {
                postingReportViews = await _employeeService.AssignmentMasterLocked(4),
                Internalassignment2 = await _internalServices.ApprovedInternalAssignmentMastersListForIgp(5),
            };
            return View(data);
        }

        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> PendingAssignList(int id)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }

            var data = new PostingReportViewModel
            {
                postingReportViews = await _employeeService.PendingAssignmentMasters(userId)
            };
            //if (applicationUser.UserName != "sumon.kanti")
            //{
            //    data.postingReportViews = await _employeeService.AssignmentMasters(applicationUser.Id);
            //}
            //else
            //{
            //    data.postingReportViews = await _employeeService.AssignmentMasters("");
            //    //data.postingReportViews = await _employeeService.AssignmentMastersAll();
            //}
            return View(data);
        }

        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> CreatedReportList()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }

            var data = new PostingReportViewModel
            {
                postingReportViews = await _employeeService.ApprovedAssignmentMasters(userId)
            };
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterDetails(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = new AssignmentViewModel
            {
                assignments = await _employeeService.GetAssignmentsPriviousLockByMasterId(id),
                assignmentRevised = await _employeeService.GetAssignmentsRevisedByMasterId(id),
                //assignments=await _employeeService.GetAssignments(),
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                assignMasterId = id,
                applicationUser = user,
                employeeInfo = await _assignmentService.GetAssignmentMasterEmployeeInfoById(id),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterDetailsForNext(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = new AssignmentViewModel
            {
                assignments = await _employeeService.GetAssignmentsPriviousLockByMasterId(id),
                assignmentRevised = await _employeeService.GetAssignmentsRevisedByMasterId(id),
                //assignments=await _employeeService.GetAssignments(),
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                assignMasterId = id,
                applicationUser = user,
                employeeInfo = await _assignmentService.GetAssignmentMasterEmployeeInfoById(id),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterDetailsPreview(int id)
        {
            var data = new AssignmentViewModel
            {
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                assignmentRevised = await _employeeService.GetAssignmentsRevisedByMasterId(id),
                assignMasterId = id,
                employeeInfo = await _assignmentService.GetAssignmentMasterEmployeeInfoById(id),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id)
            };
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterDetailsComplete(int id)
        {
            var userinformation = await userInfoes.GetUserInfoByUserName(User.Identity.Name);
            var data = new AssignmentViewModel
            {
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                assignmentRevised = await _employeeService.GetAssignmentsRevisedByMasterId(id),
                assignMasterId = id,
                refNumber = userinformation.aspnetId,
                assignmentMaster = _assignmentMaster.Get(id),
                employeeInfo = await _assignmentService.GetAssignmentMasterEmployeeInfoById(id),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id),
                aspNetUserModel=userinformation
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentComment([FromForm] AssignmentViewModel model)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentById(model.AssignmentId);
                assignments.Remarks = model.Remarks;
                _repoAssignment.Update(assignments);
                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentMasterUpdate(int id)
        {
            try
            {
                var data = _assignmentMaster.Get(id);
                data.statusId = 2;
                _assignmentMaster.Update(data);
                

                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IActionResult> AssignmentMasterUpdateUndo(int id, string comment)
        {
            try
            {
                var aplog = _approvalLog.Get(id);
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _assignmentMaster.Get((int)aplog.masterId);
                var userinfo = await _userManager.FindByIdAsync(aplog.nextApprovarId);
                _employeeService.UpdateApprovalLogBymasterId((int)aplog.masterId);
                if (userinfo.UserName == "IGP")
                {
                    data.statusId = 1;
                }
                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = currentuser.Id,
                    isActive = 1,
                    notes = comment,
                    sequenseNo = 1
                };
                _approvalLog.Insert(approvalLog);
                _assignmentMaster.Update(data);
                cache.Remove(data.refNo);
                
                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IActionResult> AssignmentMasterUpdateWithComment(int id, string userId, string comment, string revideList)
        {
            try
            {
                var nextuser = await _userManager.FindByIdAsync(userId);
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _assignmentMaster.Get(id);
                var userinfo = await userInfoes.GetUserInfoByUserName(nextuser.UserName);
                var creator = await _userManager.FindByIdAsync(data.applicationUserId);
                var userinfoCreated = await userInfoes.GetUserInfoByUserName(creator.UserName);
                _employeeService.UpdateApprovalLogBymasterId(data.Id);
                if (userinfoCreated.roleName.Contains("Sub-Admin"))
                {
                    data.applicationUserId = currentuser.Id;
                }
                if (userinfo.roleName.Contains("IGP"))
                {
                    data.statusId = 2;
                }
                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = nextuser.Id,
                    isActive = 1,
                    notes = comment,
                };
                _approvalLog.Insert(approvalLog);
                _assignmentMaster.Update(data);
                cache.Remove(data.refNo);


                if (revideList != null)
                {

                    var listInfo = revideList.Split(",");

                    for (int i = 0; i < listInfo.Length; i++)
                    {
                        var assignment = await _assignmentService.GetAssignmentById(Convert.ToInt32(listInfo[i]));
                        CancelAssignment item = new CancelAssignment
                        {
                            assignmentMasterId = id,
                            employeeId = assignment.employeeId,
                            StartDate = DateTime.Now,
                            sectionId = assignment.sectionId,
                            rankId = assignment.rankId,
                            EntryNo = assignment?.assignmentMasterId,
                            Remarks = assignment?.assignmentMaster?.refNo,
                            designationName = assignment?.assignmentMaster?.memorandumNo,
                            specialBranchUnitId = assignment.specialBranchUnitId,
                        };
                        _repoCancelAssignment.Insert(item);
                    }
                }

                NotificationModel notificationModel = new NotificationModel
                {
                    //DeviceId = "BCi8StELob1hxkNWvpnAFf7YgxfnrmMjSxIlWmrwjA4y4diZNpsIEf3wdPYJpeiR6W1UxmQuqOTN6oxGVBq1EdI",
                    DeviceId = "694636604085",
                    IsAndroiodDevice = true,
                    Title = "Alpha Push Nofification",
                    Body = "Posting Proposal send to IGP Sir",
                };
                //FirebaseNotificationModel firebaseNotificationModel = new FirebaseNotificationModel
                //{
                //    To = "694636604085",
                //    Notification = notificationModel
                //};
                var result = await _notificationService.SendNotification(notificationModel);
                //_notificationService.Send(firebaseNotificationModel);

                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentPost(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = new AssignmentViewModel
            {
                assignmentRevised = await _employeeService.GetAssignmentsRevisedByMasterId(id),
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                employeeInfo = await _assignmentService.GetAssignmentMasterEmployeeInfoById(id),
                userInfo = user,
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterReturn(int id, string comment, string user)
        {
            var update = await _employeeService.ReturnAssignmentMaster(id);
            var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = _assignmentMaster.Get(id);
            var nextuser = await _userManager.FindByIdAsync(user);
            _employeeService.UpdateApprovalLogBymasterId(data.Id);

            ApprovalLog approvalLog = new ApprovalLog
            {
                masterId = data.Id,
                userId = currentuser.Id,
                isActive = 3,
                nextApprovarId = nextuser.Id,
                notes = comment,
            };
            _approvalLog.Insert(approvalLog);

            if (update > 0)
            {
                return Json("update");
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpGet]
        public IActionResult AssignmentDeletefromOgoing(int id)
        {
            try
            {
                var update = _repoAssignment.Get(id);
                _repoAssignment.Delete(update);
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignWaitingList(int id)
        {
            var data = new AssignmentViewModel
            {
                assignMasterId = id,
                assignments = await _employeeService.AssignmentPostedByMasterId(id),
            };
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentReturnDetails(int id)
        {
            var data = new AssignmentViewModel
            {
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                assignmentRevised = await _employeeService.GetAssignmentsRevisedByMasterId(id),
                assignMasterId = id,
                employeeInfo = await _assignmentService.GetAssignmentMasterEmployeeInfoById(id),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id)
            };
            return View(data);
        }

        [Authorize(Roles = "IGP,Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignmentPostMaster()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }

            var data = new AssignmentViewModel
            {
                assignment2 = await _employeeService.AssignmentPostedMastersForApprove(userId),
                Internalassignment1 = await _internalServices.PendingEnlistMastersForIgp(userId),
                applicationUser = applicationUser
            };
            //if (applicationUser.UserName == "IGP" || applicationUser.UserName == "sumon.kanti")
            //{
            //    data.assignment2 = await _employeeService.AssignmentPostedMastersAll();
            //}
            //else
            //{
            //    data.assignment2 = await _employeeService.AssignmentPostedMasters(applicationUser.Id);
            //}
            //data.applicationUser = applicationUser;
            return View(data);
        }

        [Authorize(Roles = "IGP,Super Admin,Sub-Admin,Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignmentPostMasterForApprove()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }

            var data = new AssignmentViewModel
            {
                assignment2 = await _employeeService.AssignmentPostedMastersForApprove(userId),
                applicationUser = applicationUser
            };
            return View(data);
        }

        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignmentReturnList()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }
            var data = new AssignmentViewModel
            {
                assignment2 = await _employeeService.AssignmentReturnList(userId),
                Internalassignment2 = await _internalServices.AssignmentReturnList(userId)
            };
            //if (applicationUser.UserName == "rakib.uddin")
            //{
            //    data.assignment2 = await _employeeService.AssignmentReturnListAll();
            //}
            //else
            //{
            //    data.assignment2 = await _employeeService.AssignmentReturnList(applicationUser.Id);
            //}
            //data.applicationUser = applicationUser;
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterPostUpdate(int id, string comment)
        {

            try
            {
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _assignmentMaster.Get(id);
                data.statusId = 3;
                _assignmentMaster.Update(data);
                await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _employeeService.UpdateAssignmentInfoForIGP(data.Id);

                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    isActive = 2,
                    notes = comment,
                };
                _approvalLog.Insert(approvalLog);

                //return RedirectToAction("AssignmentPostMaster", "Assignment");
                return Json("save");

            }
            catch (Exception Ex)
            {
                return Json(Ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterPostUpdateUndo(int id)
        {

            try
            {
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _assignmentMaster.Get(id);
                if (data.statusId == 4)
                {
                    _employeeService.UpdateApprovalLogBymasterId(data.Id);
                }
                data.statusId = 2;
                _assignmentMaster.Update(data);
                //await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _employeeService.UpdateAssignmentInfoForIGPUndo(data.Id);

                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = currentuser.Id,
                    isActive = 1,
                    notes = "Undo From IGP sir",
                    sequenseNo = 1
                };
                _approvalLog.Insert(approvalLog);

                //return RedirectToAction("AssignmentPostMaster", "Assignment");
                return Json("save");

            }
            catch (Exception Ex)
            {
                return Json(Ex.Message);
            }
        }

        [Authorize(Roles = "IGP,Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignmentMasterApprove()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;

            var data = new AssignmentViewModel
            {
                assignmentApprove = await _employeeService.AssignmentPostedMastersApprove(userId),
                Internalassignment2 = await _internalServices.ApprovedInternalAssignmentMastersList(userId),
                applicationUser = applicationUser
            };
            return View(data);
        }




        [Authorize(Roles = "IGP,Super Admin,Admin,Sub-Admin,PHQ Approver")]
        [HttpGet]
        public async Task<IActionResult> AllLockedAssignmentMaster()
        {
            var data = new AssignmentViewModel
            {
                assignmentApprove = await _employeeService.AllLockedAssignmentMaster(),
                Internalassignment2 = await _internalServices.ApprovedInternalAssignmentMastersListForIgp(4),
            };
            return View(data);
        }


        [Authorize(Roles = "IGP,Super Admin,Admin,Sub-Admin,PHQ Approver")]
        [HttpGet]
        public async Task<IActionResult> AllAssignmentMasterCopy()
        {
            var emp = await userInfoes.GetUserInfoByUserName(User.Identity.Name);
            var data = new AssignmentViewModel
            {
                assignmentApprove = await userInfoes.GetRankWiseAssignmentCopy((int)emp.rankId),
                Internalassignment2 = await userInfoes.GetRankWiseInternalAssignmentCopy((int)emp.rankId),
            };
            return View(data);
        }


        [Authorize(Roles = "Super Admin,Admin,Sub-Admin")]
        [HttpGet]
        public async Task<IActionResult> EnrollFrezzList()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var enlistedMasterAssignments = await _employeeService.EnListedMasterDetails(applicationUser.Id);
            var frezzMasterAssignments = await _employeeService.FrezzMasterDetails(applicationUser.Id);
            var data = new EnlistedAssignmentViewModel();
            data.enlistedMasterAssignments = enlistedMasterAssignments;
            data.frezzMasterAssignments = frezzMasterAssignments;
            return View(data);
        }


        [Route("global/api/GetAllEmployeeList")]
        [HttpGet]
        public async Task<IActionResult> GetUnionWardsByThanaId()
        {
            return Json(await _employeeService.GetEmployeeInfos());
        }


        [HttpGet]
        public async Task<IActionResult> GetLockedofficersListByMaster(int MasterId, int rankId)
        {
            return Json(await _employeeService.LockedOfficersByMasterId(MasterId, rankId));
        }

        [Route("global/api/SendSMSToIGP")]
        [HttpGet]
        public async Task<IActionResult> SendSMSToIGP()
        {
            var user = await userInfoes.GetUserInfoBeforeRegister(User.Identity.Name);
            var data = await _SMSService.SendSMSAsync("+8801674878443", "1234");
            return Json(data);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> GetPIMSDataByBPNo()
        {
            var employeeInfos = await _employeeService.GetEmployeeInfosByType(3);
            foreach (var employee in employeeInfos)
            {
                string url = String.Format("https://pims.police.gov.bd:8443/pimslive/webpims/opus/bpdetails/{0}", employee.employeeType.empTypeBn + employee.employeeCode);

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var smsData = await response.Content.ReadAsStringAsync();
                var pIMSBody = JsonConvert.DeserializeObject<PIMSBody>(await response.Content.ReadAsStringAsync());
                Web.Models.PIMSDataModel pIMS = new Web.Models.PIMSDataModel();
                if (pIMSBody.items.Count() > 0)
                {
                    pIMS = pIMSBody.items[0];
                }
                else
                {
                    pIMS = new Web.Models.PIMSDataModel();
                }

                if (pIMS.picture != "")
                {

                    var t = pIMS.picture;  // remove data:image/png;base64,

                    byte[] bytes = Convert.FromBase64String(t);

                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                    }
                    var randomFileName = Guid.NewGuid().ToString().Substring(0, 8) + ".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmpImages", randomFileName);
                    var size = new Size(300, 300);
                    var i2 = new Bitmap(image, size);
                    i2.Save(path, System.Drawing.Imaging.ImageFormat.Png);

                    var profilePhoto = await photographService.GetPhotographByType(employee.Id, "profile");
                    if (profilePhoto != null)
                    {
                        profilePhoto.url = "EmpImages/" + randomFileName;
                        var save = await photographService.SavePhotograph(profilePhoto);
                    }
                    else
                    {
                        Photograph photograph = new Photograph
                        {
                            Id = 0,
                            employeeId = employee.Id,
                            url = "EmpImages/" + randomFileName,
                            type = "profile"
                        };
                        var save = await photographService.SavePhotograph(photograph);
                    }


                    // await photographService.DeleteempId(model.employeeID);

                }
            }


            return Json(true);
        }

        public async Task<IActionResult> GetOfficerImage()
        {
            var officers = await _employeeService.GetOfficerImages();
            foreach (var emp in officers)
            {
                //string sourcePath = "wwwroot"+emp.url;
                //string[] files = Directory.GetFiles(sourcePath);

                //foreach (string file in files)
                //{
                //    System.IO.File.Copy(sourcePath, destinationPath);
                //}
                //System.IO.File.Copy(Server.MapPath("/sourcefolder/myimage.jpg"), Server.MapPath("/destinationfolder/myimage1.jpg"));

                //string SfileName = Path.Combine("OneTouch", emp.SLNo + "_" + emp.Name + ".jpeg");
                var SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", emp.url);

                string fileName = Path.Combine("OneTouch", emp.SLNo + "_" + emp.Name + "_0.jpeg");
                var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                try
                {
                    System.IO.File.Copy(SourcePath, destinationPath);
                    //using (var stream = new FileStream(path, FileMode.Create))
                    //{
                    //    file.CopyTo(stream);
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignetureByBp(string Bp)
        {
            var data = await _employeeService.GetEmployeeSignatureByBp(Bp);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeBasicInfoForPostingByempId(int Id)
        {
            var data = await _employeeService.GetEmployeeBasicInfoForPostingByempId(Id);
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAssignMasterForNoteSheet(AssignmentViewModel model)
        {
            try
            {
                var masterData = await _assignmentService.GetAssignmentMasterById(Convert.ToInt32(model.assignMasterId));
                masterData.noteSheetTitle = model.noteSheetTitle;
                masterData.noteSheetDescription = model.noteComment;
                _assignmentMaster.Update(masterData);
                return Json("Save");
                //return RedirectToAction("PostingConfirmNotePdf", "Report", new { id = model.assignMasterId });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}