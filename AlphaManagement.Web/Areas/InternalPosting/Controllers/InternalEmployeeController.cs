using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.EmployeeService.Interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Areas.InternalPosting.Models;
using AlphaManagement.Web.Helpers;
using AlphaManagement.Web.Models;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AlphaManagement.Web.Areas.InternalPosting.Controllers
{
    [Area("InternalPosting")]
    [Authorize(Roles = "Super Admin,Admin,IGP,PHQ Approver,Reserve Office,PM1 Gradation")]
    public class InternalEmployeeController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly LangGenerate<AwardEntryLn> _award;
        private readonly LangGenerate<DisciplinaryActionLn> _disiplinary;
        private readonly LangGenerate<EducationalQualificationLn> _education;
        private readonly LangGenerate<TraningLogLn> _training;

        private readonly IRepository<Designation> _repoDesignation;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<Religion> _repoReligion;
        private readonly IRepository<Section> _repoSection;
        private readonly IRepository<SpecialBranchUnit> _repoBranch;
        private readonly IRepository<District> _district;
        private readonly IRepository<BCSBatch> _repoBCSBatch;
        private readonly IRepository<PHQTRType> _repoPHQTRType;
        private readonly IUserInfoes userInfoes;
        private readonly IRepository<LevelofEducation> _lavelofEducation;
        private readonly IRepository<Organization> _organizationService;
        private readonly IRepository<Result> _result;
        private readonly IRepository<Degree> _degree;
        private readonly IRepository<AddressInformation> _addressInformation;
        private readonly IRepository<StatusInfo> _statusInfo;
        private readonly IRepository<EmployeePrintHistoryLog> _employeePrintHistoryLog;
        private readonly IRepository<Department> _repoDepartment;
        private readonly IRepository<InternalAssignment> _repoInterAssignment;
        private readonly IRepository<InternalAssignmentMaster> _repoInterAssignmentMaster;
        private readonly IRepository<InternalEnlistedAssignment> _repoInternalAssignment;
        private readonly IRepository<InternalEnlistedAssignmentMaster> _repoInternalAssignmentMaster;
        private readonly IRepository<InternalApprovalLog> _repoInternalApprovalLog;
        private readonly IRepository<InternalAssignmentTransectionLog> _repoInternalAssignmentTransectionLog;
        private readonly IRepository<InternalAnulipiList> _anulipiList;
        private readonly IRepository<InternalAssignmentAnulipi> _internalAssignmentAnulipi;
        private readonly IRepository<AssignmentAnulipiPreview> _assignmentAnulipiPre;
        private readonly IConfiguration _configuration;

        public IRepository<RelDegreeSubject> _relDegreeSubject { get; }

        private readonly IDegreeService _degreeService;
        private readonly IRepository<Division> _division;
        private readonly IAddressServices _addressService;
        private readonly IRepository<EmployeeInfo> _employeeInfo;
        private readonly IRepository<Section> _section;
        private readonly IRepository<SpecialBranchUnit> _specialBranchUnit;
        private UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IPhotographService photographService;
        private readonly IRepository<Relation> _relation;
        private readonly IRepository<NaturalPunishment> _naturalPunishment;
        private readonly IRepository<Offense> _offense;
        private readonly IRepository<TrainingCategory> _trainingCategory;
        private readonly IRepository<TrainingInstitute> _trainingInstitute;
        private readonly IRepository<AwardEntry> _repoAwardEntry;
        private readonly IRepository<Award> _repoAward;
        private readonly IRepository<Banks> _banks;
        private readonly IRepository<ForeignTravel> _repoForeignTravel;
        private readonly IRepository<Country> _repoCountry;
        private readonly IRepository<UnionWard> _repoUnionWord;
        private readonly IRepository<Thana> _repoThana;
        private readonly IRepository<Disease> _disease;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IAccessLogHistoryService accessLogHistoryService;
        private readonly IAssignmentService _assignmentService;
        private readonly IInternalPostingServices _internalServices;

        public IRepository<SpouseRelation> _spouseRelation { get; }

        private readonly IRepository<Spouse> _spouse;
        private readonly IRepository<EducationalQualification> _educationalQualification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public InternalEmployeeController(
            IHostingEnvironment hostingEnvironment,
            IRepository<Designation> repoDesignation,
            IRepository<Rank> repoRank,
            IRepository<Religion> repoReligion,
            IRepository<Section> repoSection,
            IRepository<SpecialBranchUnit> repoBranch,
            IRepository<EmployeePrintHistoryLog> _employeePrintHistoryLog,
            IRepository<District> district,
            IRepository<BCSBatch> repoBCSBatch,
            IRepository<PHQTRType> repoPHQTRType,
            IRepository<LevelofEducation> lavelofEducation,
            IRepository<Organization> organizationService,
            IRepository<Result> result,
            IRepository<Degree> degree,
            IRepository<RelDegreeSubject> relDegreeSubject,
            IRepository<Division> division,
            IRepository<EmployeeInfo> employeeInfo,
            IRepository<Section> section,
            IRepository<SpecialBranchUnit> specialBranchUnit,
            IRepository<Relation> relation,
            IRepository<NaturalPunishment> naturalPunishment,
            IRepository<Offense> offense,
            IRepository<TrainingCategory> trainingCategory,
            IRepository<TrainingInstitute> trainingInstitute,
            IRepository<SpouseRelation> spouseRelation,
            IRepository<Spouse> spouse,
            IRepository<AwardEntry> repoAwardEntry,
            IRepository<Award> repoAward,
            IRepository<ForeignTravel> repoForeignTravel,
            IRepository<Country> repoCountry,
            IRepository<UnionWard> repoUnionWord,
            IRepository<Thana> repoThana,
            IRepository<InternalEnlistedAssignment> repoInternalAssignment,
            IRepository<InternalAssignmentMaster> repoInterAssignmentMaster,
            IRepository<InternalEnlistedAssignmentMaster> repoInternalAssignmentMaster,
            IRepository<InternalAssignmentTransectionLog> _repoInternalAssignmentTransectionLog,
            IRepository<InternalApprovalLog> repoInternalApprovalLog,
            IRepository<InternalAssignment> repoInterAssignment,
            IRepository<Disease> disease,
            IRepository<StatusInfo> statusInfo,
            IRepository<Department> repoDepartment,
            IRepository<InternalAnulipiList> anulipiList,
            IConfiguration _configuration,
            IRepository<InternalAssignmentAnulipi> internalAssignmentAnulipi,
            IRepository<AssignmentAnulipiPreview> assignmentAnulipiPre,

            IRepository<EducationalQualification> educationalQualification,
            IRepository<Banks> banks,
            IRepository<AddressInformation> addressInformation,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,

            IUserInfoes userInfoes,
            IAssignmentService assignmentService,
            IDegreeService degreeService,
            IAddressServices addressService,
            IEmployeeService employeeService,
            IPhotographService photographService,
            IAccessLogHistoryService accessLogHistoryService,
            IInternalPostingServices internalServices,
            IConverter converter


            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;

            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _award = new LangGenerate<AwardEntryLn>(hostingEnvironment.ContentRootPath);
            _disiplinary = new LangGenerate<DisciplinaryActionLn>(hostingEnvironment.ContentRootPath);
            _education = new LangGenerate<EducationalQualificationLn>(hostingEnvironment.ContentRootPath);
            _training = new LangGenerate<TraningLogLn>(hostingEnvironment.ContentRootPath);

            _repoDesignation = repoDesignation;
            _repoDepartment = repoDepartment;
            _repoRank = repoRank;
            _repoReligion = repoReligion;
            _repoSection = repoSection;
            _repoBranch = repoBranch;
            _statusInfo = statusInfo;
            _district = district;
            _repoBCSBatch = repoBCSBatch;
            _repoPHQTRType = repoPHQTRType;
            _repoInternalAssignment = repoInternalAssignment;
            _repoInterAssignmentMaster = repoInterAssignmentMaster;
            _repoInternalAssignmentMaster = repoInternalAssignmentMaster;
            _repoInternalApprovalLog = repoInternalApprovalLog;
            _repoInterAssignment = repoInterAssignment;
            _anulipiList = anulipiList;
            this._repoInternalAssignmentTransectionLog = _repoInternalAssignmentTransectionLog;
            this.userInfoes = userInfoes;
            this._employeePrintHistoryLog = _employeePrintHistoryLog;
            this.accessLogHistoryService = accessLogHistoryService;
            _lavelofEducation = lavelofEducation;
            _organizationService = organizationService;
            _result = result;
            _degree = degree;
            _assignmentService = assignmentService;
            _relDegreeSubject = relDegreeSubject;
            _degreeService = degreeService;
            _division = division;
            _addressService = addressService;
            _employeeInfo = employeeInfo;
            _section = section;
            _specialBranchUnit = specialBranchUnit;
            _employeeService = employeeService;
            this.photographService = photographService;
            this._configuration = _configuration;
            _relation = relation;
            _naturalPunishment = naturalPunishment;
            _offense = offense;
            _trainingCategory = trainingCategory;
            _trainingInstitute = trainingInstitute;
            _spouseRelation = spouseRelation;
            _spouse = spouse;
            _banks = banks;
            _disease = disease;
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
            _educationalQualification = educationalQualification;
            _addressInformation = addressInformation;
            _repoAwardEntry = repoAwardEntry;
            _repoAward = repoAward;
            _repoForeignTravel = repoForeignTravel;
            _repoCountry = repoCountry;
            _repoThana = repoThana;
            _internalAssignmentAnulipi = internalAssignmentAnulipi;
            _assignmentAnulipiPre = assignmentAnulipiPre;
            _repoUnionWord = repoUnionWord;
            _internalServices = internalServices;
        }

        public async Task<IActionResult> EmployeeIndex(string refNo)
        {
            ViewBag.refNo = refNo;
            AssignmentViewModel model = new AssignmentViewModel
            {
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                bCSBatches = _repoBCSBatch.GetAll(),
                phqSections = await _internalServices.GetDepartmentWiseTotalEmployee(),
                educations = await _assignmentService.GetEducationalQualifications(),
                unitRankWiseEmployeeViewModels = await _internalServices.GetSectionRankWiseEmployeeList(0, 0, 0, 0, 0, 0, 0, 0)
            };
            return View(model);
        }

        public async Task<IActionResult> EmployeeIndexAdditional(string refNo)
        {
            ViewBag.refNo = refNo;
            AssignmentViewModel model = new AssignmentViewModel
            {
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                bCSBatches = _repoBCSBatch.GetAll(),
                phqSections = _repoDepartment.GetAll(),
                educations = await _assignmentService.GetEducationalQualifications(),
                internalAssignments = await _internalServices.GetActiveInternalAssignments()
            };
            return View(model);
        }

        public async Task<IActionResult> InternalEmployeePosting(string refNo)
        {
            ViewBag.refNo = refNo;
            AssignmentViewModel model = new AssignmentViewModel
            {
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                bCSBatches = _repoBCSBatch.GetAll(),
                //phqSections = _repoDepartment.GetAll(),
                phqSections = await _internalServices.GetDepartmentWiseTotalEmployee(),
                educations = await _assignmentService.GetEducationalQualifications(),
                unitRankWiseEmployeeViewModels = await _internalServices.GetSectionRankWiseEmployeeList(0, 0, 0, 0, 0, 0, 0, 0)
            };
            return View(model);
        }

        public async Task<IActionResult> EmployeeListHeadQuater()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);

            AssignmentViewModel model = new AssignmentViewModel
            {
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                bCSBatches = _repoBCSBatch.GetAll(),
                phqSections = _repoDepartment.GetAll(),
                educations = await _assignmentService.GetEducationalQualifications(),
                unitRankWiseEmployeeViewModels = await _internalServices.GetSectionRankWiseEmployeeList(0, 0, 0, 0, 0, 0, 0, 0),
                roleName = roles.FirstOrDefault()
            };
            return View(model);
        }

        public async Task<IActionResult> EmployeeListHeadQuaterReserve()
        {
            AssignmentViewModel model = new AssignmentViewModel
            {
                ranks = await _assignmentService.GetRankWiseEmployeeInfo(),
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                bCSBatches = _repoBCSBatch.GetAll(),
                phqSections = _repoDepartment.GetAll(),
                educations = await _assignmentService.GetEducationalQualifications(),
                unitRankWiseEmployeeViewModels = await _internalServices.GetSectionRankWiseEmployeeList(0, 0, 0, 0, 0, 0, 0, 0)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveInternalPosting(InternalEmployeeInfoVM model)
        {
            try
            {
                if (model.employeeId.Count() > 0)
                {
                    var refNum = "";
                    var year = DateTime.Now.Date.Year;
                    var month = DateTime.Now.Date.Month;
                    var masterDetails = new EnlistedAssignmentMaster();
                    string rand = RandomString(3);
                    refNum = "PHQ-" + year + "-" + month + "-" + rand;
                    var applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    var obj = new InternalEnlistedAssignmentMaster
                    {
                        Id = 0,
                        refNo = refNum,
                        refDate = DateTime.Now.Date,
                        typeId = 1,
                        statusId = 1,
                        assignmentTypeId = 1,
                        ApplicationUserId = applicationUser.Id
                    };
                    var masterId = await _internalServices.SaveInternalEnlistMaster(obj);
                    for (int i = 0; i < model.employeeId.Length; i++)
                    {
                        var obj1 = new InternalEnlistedAssignment
                        {
                            Id = 0,
                            employeeId = model.employeeId[i],
                            sectionId = model.sectionId[i],
                            enlistedAssignmentMasterId = masterId,
                            statusId = 1,
                            typeId = 1,
                            remarks = "PHQ Internal Posting"
                        };
                        var detailsId = await _internalServices.SaveInternalEnlist(obj1);
                    }
                    return Json(masterId);
                }
                return Json("Fail");
            }
            catch (Exception ex)
            {
                return Json("Fail");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveInternalPostingAdditional(InternalEmployeeInfoVM model)
        {
            try
            {
                if (model.employeeId.Count() > 0)
                {
                    var refNum = "";
                    var year = DateTime.Now.Date.Year;
                    var month = DateTime.Now.Date.Month;
                    var masterDetails = new EnlistedAssignmentMaster();
                    string rand = RandomString(3);
                    refNum = "PHQ-" + year + "-" + month + "-" + rand;
                    var applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    var obj = new InternalEnlistedAssignmentMaster
                    {
                        Id = 0,
                        refNo = refNum,
                        refDate = DateTime.Now.Date,
                        typeId = 1,
                        statusId = 1,
                        assignmentTypeId = 2,
                        ApplicationUserId = applicationUser.Id
                    };
                    var masterId = await _internalServices.SaveInternalEnlistMaster(obj);
                    for (int i = 0; i < model.employeeId.Length; i++)
                    {
                        var obj1 = new InternalEnlistedAssignment
                        {
                            Id = 0,
                            employeeId = model.employeeId[i],
                            sectionId = model.sectionId[i],
                            enlistedAssignmentMasterId = masterId,
                            statusId = 1,
                            typeId = 1,
                            remarks = "PHQ Internal Posting"
                        };
                        var detailsId = await _internalServices.SaveInternalEnlist(obj1);
                    }
                    return Json(masterId);
                }
                return Json("Fail");
            }
            catch (Exception ex)
            {
                return Json("Fail");
            }
        }

        [HttpGet]
        public async Task<IActionResult> InternalPostingDetails(int enlistMasterId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = new InternalAssignmentViewModel
            {
                assignmentDetailsModals = await _internalServices.AssignmentDetailsModalByMasterId(enlistMasterId),
                assignMasterId = enlistMasterId,
                applicationUser = user,
                employeeInfo = await _internalServices.GetAssignmentMasterEmployeeInfoById(enlistMasterId),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _internalServices.GetBadgeAndActivityModelList(),
                phqSections = await _internalServices.GetDepartmentWiseTotalEmployee(),
                postingReportViews = await _internalServices.GetApprovalLogFullByMasterId(enlistMasterId),
                EntryNo = await _internalServices.InternalMinimumRankStatus(enlistMasterId)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> InternalAssignmentPost(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = new InternalAssignmentViewModel
            {
                assignmentDetailsModals = await _internalServices.AssignmentDetailsModalByMasterId(id),
                assignMasterId = id,
                userInfo = user,
                employeeInfo = await _internalServices.GetAssignmentMasterEmployeeInfoById(id),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _internalServices.GetBadgeAndActivityModelList(),
                phqSections = await _internalServices.GetDepartmentWiseTotalEmployee(),
                postingReportViews = await _internalServices.GetApprovalLogFullByMasterId(id)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> InternalPostingDetailsPreview(int enlistMasterId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.AssignmentId = await _employeeService.GetinternalAssignmentMasterIdByEnlishmentId(enlistMasterId);
            var data = new InternalAssignmentViewModel
            {
                assignmentDetailsModals = await _internalServices.AssignmentDetailsModalByMasterId(enlistMasterId),
                assignMasterId = enlistMasterId,
                applicationUser = user,
                employeeInfo = await _internalServices.GetAssignmentMasterEmployeeInfoById(enlistMasterId),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _internalServices.GetBadgeAndActivityModelList(),
                postingReportViews = await _internalServices.GetApprovalLogFullByMasterId(enlistMasterId)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();
            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> InternalPostingDetailsPreviewOnly(int enlistMasterId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = new InternalAssignmentViewModel
            {
                assignmentDetailsModals = await _internalServices.AssignmentDetailsModalByMasterId(enlistMasterId),
                assignMasterId = enlistMasterId,
                applicationUser = user,
                employeeInfo = await _internalServices.GetAssignmentMasterEmployeeInfoById(enlistMasterId),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                badgeAndActivityModels = await _internalServices.GetBadgeAndActivityModelList(),
                postingReportViews = await _internalServices.GetApprovalLogFullByMasterId(enlistMasterId)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();
            return View(data);
        }

        public async Task<IActionResult> AssignmentMasterUpdateUndo(int id, string comment)
        {
            try
            {
                var aplog = _repoInternalApprovalLog.Get(id);
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _repoInternalAssignmentMaster.Get((int)aplog.masterId);
                var userinfo = await _userManager.FindByIdAsync(aplog.nextApprovarId);
                var Createuserinfo = await _userManager.FindByIdAsync(aplog.userId);
                _internalServices.UpdateApprovalLogBymasterId((int)aplog.masterId);
                if (Createuserinfo.UserName == "IGP")
                {
                    data.statusId = 3;
                }
                else if (Createuserinfo.UserName == "PHQ Approver")
                {
                    data.statusId = 1;
                }
                else
                {
                    data.statusId = 2;
                }
                InternalApprovalLog approvalLog = new InternalApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = currentuser.Id,
                    isActive = 1,
                    notes = comment,
                    sequenseNo = 1
                };
                _repoInternalApprovalLog.Insert(approvalLog);
                _repoInternalAssignmentMaster.Update(data);

                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IActionResult> InternalPendingAssignList()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }
            var emp = await userInfoes.GetUserInfoByUserName(User.Identity.Name);
            var data = new InternalPostingReportViewModel
            {
                postingReportViews = await userInfoes.GetRankWiseInternalAssignmentCopyOngoing((int)emp.rankId)
            };
            return View(data);
        }

        public async Task<IActionResult> InternalAssignListForIGP()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin" || roles.FirstOrDefault() == "IGP")
            {
                userId = "";
            }

            var data = new InternalPostingReportViewModel
            {
                postingReportViews = await _internalServices.PendingEnlistMastersForIgp(userId)
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentComment([FromForm] AssignmentViewModel model)
        {
            try
            {
                var assignments = await _internalServices.GetAssignmentById(model.AssignmentId);
                assignments.remarks = model.Remarks;
                _repoInternalAssignment.Update(assignments);
                return Json("save");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IActionResult> SaveMoveToInfoFromOngoing(int detailId, int sectionId)
        {
            var enListed = await _assignmentService.GetInternalEnlistedAssignmentIdSingle(detailId);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (enListed != null)
            {
                InternalAssignmentTransectionLog data = new InternalAssignmentTransectionLog
                {
                    assignmentId = enListed.Id,
                    oldSectionId = enListed.sectionId,
                    newSectionId = sectionId,
                    StartDate = DateTime.Now,
                    UpdateUserId = user.Id
                };
                _repoInternalAssignmentTransectionLog.Insert(data);

                enListed.sectionId = sectionId;
                _repoInternalAssignment
                    .Update(enListed);
            }

            return Json("success");
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterReturn(int id, string comment, string user)
        {
            var update = await _employeeService.ReturnInternalAssignmentMaster(id);
            var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = _repoInternalAssignmentMaster.Get(id);
            var nextuser = await _userManager.FindByIdAsync(user);
            _employeeService.UpdateInternalApprovalLogBymasterId(data.Id);

            InternalApprovalLog approvalLog = new InternalApprovalLog
            {
                masterId = data.Id,
                userId = currentuser.Id,
                isActive = 3,
                nextApprovarId = nextuser.Id,
                notes = comment,
            };
            _repoInternalApprovalLog.Insert(approvalLog);

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
        public async Task<IActionResult> AssignmentMasterPostUpdate(int id, string comment)
        {
            try
            {
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _repoInternalAssignmentMaster.Get(id);
                data.statusId = 4;
                _repoInternalAssignmentMaster.Update(data);
                int type = 1;
                if (data.assignmentTypeId == 2)
                {
                    type = 2;
                }
                //await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _internalServices.UpdateAssignmentInfoForIGP(data.Id);

                InternalApprovalLog approvalLog = new InternalApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    isActive = 2,
                    notes = comment,
                };
                _repoInternalApprovalLog.Insert(approvalLog);

                InternalAssignmentMaster internalAssignmentMaster = new InternalAssignmentMaster
                {
                    applicationUserId = data.ApplicationUserId,
                    refNo = data.refNo,
                    refDate = Convert.ToDateTime(data.refDate),
                    statusId = 3,
                    internalEnlistedAssignmentMasterId = data.Id,
                };
                int masterid = await _internalServices.SaveInternalAssignmentMaster(internalAssignmentMaster);

                var details = await _internalServices.GetInternalEnlistedAssignmentsByMasterId(id);

                foreach (var item in details)
                {
                    InternalAssignment internalAssignment = new InternalAssignment
                    {
                        assignmentMasterId = masterid,
                        employeeId = item.employeeId,
                        departmentId = item.sectionId,
                        receiveRefNo = item.remarks,
                        assignmentTypeId = type,
                        statusId = 3
                    };
                    _repoInterAssignment.Insert(internalAssignment);
                }

                return RedirectToAction("Index", "Home");

            }
            catch (Exception Ex)
            {
                return Json(Ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> InternalAssignmentDeletefromOgoing(int id)
        {
            try
            {
                var update = _repoInternalAssignment.Get(id);
                _repoInternalAssignment.Delete(update);
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public IActionResult DischargeInternalEmployee(int id)
        {
            try
            {
                var update = _repoInterAssignment.Get(id);
                update.statusId = 100;
                _repoInterAssignment.Update(update);
                return Json("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> InternalAssignmentMasterUpdateWithComment(int id, string userId, string comment)
        {
            try
            {
                var nextuser = await _userManager.FindByIdAsync(userId);
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _repoInternalAssignmentMaster.Get(id);
                var userinfo = await userInfoes.GetUserInfoByUserName(nextuser.UserName);
                _internalServices.UpdateApprovalLogBymasterId(data.Id);
                if (userinfo.roleName.Contains("IGP"))
                {
                    data.statusId = 3;
                }
                else
                {
                    data.statusId = 2;
                }
                InternalApprovalLog approvalLog = new InternalApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = nextuser.Id,
                    isActive = 1,
                    notes = comment,
                };
                _repoInternalApprovalLog.Insert(approvalLog);
                _repoInternalAssignmentMaster.Update(data);

                return Json("save");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> PostingReportEntry(int id)
        {
            ViewBag.Id = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                internalAnulipiLists = _anulipiList.GetAll(),
                assignmentVMs = await _internalServices.InternalAssignmentPostedByMasterId(id),
                internalAssignmentALL = await _internalServices.InternalAssignmentAll(),
                internalAssignments = await _internalServices.InternalAssignmentDetailsByMasterId(id)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostingReportEntryUpdate([FromForm]PostingReportViewModel model)
        {
            try
            {
                var assMaster = await _internalServices.InternalAssignmentMasterEnlistId(model.assignMasterId);
                for (int i = 0; i < model.anulipiIds.Length; i++)
                {
                    var anu = new InternalAssignmentAnulipi
                    {
                        assignmentMasterId = assMaster.Id,
                        anulipiListId = model.anulipiIds[i],
                        anulipiText = model.newAnulipiTxt[i],
                        shortOrder = i
                    };
                    if (model.anulipiIds[i] == 0)
                    {
                        anu.anulipiListId = null;
                    }
                    _internalAssignmentAnulipi.Insert(anu);
                }
                var data = await _internalServices.InternalAssignmentMasterEnlistId(model.assignMasterId);
                data.memorandumNo = model.refNo;
                data.statusId = 18;
                data.refDate = model.refDate;
                data.title = model.title;
                data.description = model.description;
                _repoInterAssignmentMaster.Update(data);
                //var data0 = await _internalServices.InternalAssignmentMaster(data.refNo);
                //_repoInterAssignmentMaster.Update(data0);
                var data1 = await _internalServices.InternamAssignments(assMaster.Id);
                foreach (var item in data1)
                {
                    item.statusId = 18;
                    await _internalServices.SaveInternamAssignments(item);
                }
                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreatedInternalReportList()
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
                postingReportViews = await _internalServices.ApprovedInternalAssignmentMasters(userId)
            };
            return View(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingReportPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/InternalPosting/InternalEmployee/PostingReport/" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingReport(int id)
        {
            PostingReportViewModel model = new PostingReportViewModel
            {
                internalAssignmentAnulipis = await _internalServices.AssignmentMastersRopo(id),
                internalAssignments = await _internalServices.InternalAssignmentsDetailsByMasterId(id),
                assignmentVMs = await _internalServices.InternalAssignmentPostedByMasterId(id),
                internalAssignmentALL = await _internalServices.InternalAssignmentAll()
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult WaitAssignListPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/InternalPosting/InternalEmployee/WaitAssignList?id=" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> WaitAssignList(int id)
        {
            ViewBag.Id = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                internalEnlistAssignments = await _internalServices.AssignmentDetailsModalByMasterId(id),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostingReportPreview([FromForm]PostingReportViewModel model)
        {
            try
            {
                var master = await _internalServices.InternalAssignmentMasterEnlistId(model.assignMasterId);

                if (model?.anulipiIds?.Length > 0)
                {
                    _internalServices.DeleteAssignmentsAunilipiPreviewByMasterId(master.Id);
                    for (int i = 0; i < model.anulipiIds.Length; i++)
                    {
                        var anu = new AssignmentAnulipiPreview
                        {
                            internalAssignMasterId = master.Id,
                            anulipiListId = model.anulipiIds[i],
                            anulipiText = model.newAnulipiTxt[i],
                            shortOrder = i
                        };
                        if (model.anulipiIds[i] == 0)
                        {
                            anu.anulipiListId = null;
                        }
                        _assignmentAnulipiPre.Insert(anu);
                    }

                }

                var data = _repoInterAssignmentMaster.Get(master.Id);
                data.memorandumNo = model.refNo;
                data.refDate = model.refDate;
                data.title = model.title;
                data.description = model.description;
                _repoInterAssignmentMaster.Update(data);

                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingReportPreviewPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/InternalPosting/InternalEmployee/PostingReportPreview/" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingReportPreview(int id)
        {
            PostingReportViewModel model = new PostingReportViewModel
            {
                assignmentAnulipisPreview = await _internalServices.AssignmentMastersRoportPre(id),
                internalAssignments = await _internalServices.InternalAssignmentsDetailsByMasterId(id),
                assignmentVMs = await _internalServices.InternalAssignmentPostedByMasterId(id),
                internalAssignmentALL = await _internalServices.InternalAssignmentAll()
            };
            return View(model);
        }

        #region Partial View
        public async Task<IActionResult> GetUnitRankWisePartialEmployeeView(string rankId, int unitId, int batch, int servicePeriod, int bandId, int isLocked, int isAttached, int isUnMission)
        {
            try
            {
                if (rankId == null)
                {
                    rankId = "0";
                }
                var model = new InternalAssignmentViewModel
                {
                    unitRankWiseEmployeeViewModels = await _internalServices.GetMutipleRankWiseSectionEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission),
                    //unitRankWiseEmployeeViewModels = await _internalServices.GetSectionRankWiseEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission),
                };
                return PartialView("_UnitSectionWisePartialView2", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetUnitRankWisePartialEmployeeView2(int rankId, int unitId, int batch, int servicePeriod, int bandId, int isLocked, int isAttached, int isUnMission)
        {
            try
            {
                var model = new InternalAssignmentViewModel
                {
                    unitRankWiseEmployeeViewModels = await _internalServices.GetSectionRankWiseEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission),
                };
                return PartialView("_UnitSectionWisePartialView2", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetPartialEmployeeInfo(string id)
        {
            try
            {
                if (id == string.Empty || id == null)
                {
                    id = User.Identity.Name;
                }

                var userInfo = await userInfoes.GetUserInfoByUser(id);
                EmployeeInfo employeeInfo = new EmployeeInfo();
                var empInfo = await _employeeService.GetEmployeeInfoById(id);
                if (empInfo == null)
                {
                    employeeInfo = new EmployeeInfo();
                }
                else
                {
                    employeeInfo = empInfo;
                }
                Photograph photograph = new Photograph();
                photograph = await _employeeService.GetEmployeePhotographByEmpId(empInfo.Id);
                if (photograph == null)
                    photograph = new Photograph();
                IEnumerable<Spouse> spouses = new List<Spouse>();
                spouses = await _employeeService.GetSpouseInfoByEmpId(empInfo.Id);
                if (spouses == null)
                    spouses = new List<Spouse>();
                Spouse spouse = new Spouse();
                spouse = await _employeeService.GetSpouseInfoByEmpIdRelId(empInfo.Id);
                if (spouse == null)
                    spouse = new Spouse();
                IEnumerable<Assignment> assignments = new List<Assignment>();
                assignments = await _employeeService.GetAssignmentInfoByEmpId(empInfo.Id);
                if (assignments == null)
                    assignments = new List<Assignment>();
                IEnumerable<EducationalQualification> educationalQualifications = new List<EducationalQualification>();
                educationalQualifications = await _employeeService.GetEducationalQualificationInfoByEmpId(empInfo.Id);
                if (educationalQualifications == null)
                    educationalQualifications = new List<EducationalQualification>();
                IEnumerable<AwardEntry> awardEntries = new List<AwardEntry>();
                awardEntries = await _employeeService.GetAwardInfoByEmpId(empInfo.Id);
                if (awardEntries == null)
                    awardEntries = new List<AwardEntry>();
                IEnumerable<PromotionLog> promotionLogs = new List<PromotionLog>();
                promotionLogs = await _employeeService.GetPromotionInfoByEmpId(empInfo.Id);
                if (promotionLogs == null)
                    promotionLogs = new List<PromotionLog>();
                IEnumerable<TraningLog> traningLogs = new List<TraningLog>();
                traningLogs = await _employeeService.GetTraningLogInfoByEmpId(empInfo.Id);
                if (traningLogs == null)
                    traningLogs = new List<TraningLog>();
                IEnumerable<DisciplinaryAction> disciplinaryActions = new List<DisciplinaryAction>();
                disciplinaryActions = await _employeeService.GetDisciplinaryByEmpId(empInfo.Id);
                if (disciplinaryActions == null)
                    disciplinaryActions = new List<DisciplinaryAction>();
                IEnumerable<AddressInformation> addressInformation = new List<AddressInformation>();
                addressInformation = await _employeeService.GetAddressInformationByEmpId(empInfo.Id);
                if (addressInformation == null)
                    addressInformation = new List<AddressInformation>();

                IEnumerable<ForeignTravel> travails = new List<ForeignTravel>();
                travails = await _employeeService.GetForeignTravelsById(empInfo.Id);
                if (travails == null)
                    travails = new List<ForeignTravel>();

                var presentAddress = await _addressService.GetAllPresentAddress(empInfo.Id);
                if (presentAddress == null)
                    presentAddress = new AddressInformation();
                var parmenantAddress = await _addressService.GetAllParmenantAddress(empInfo.Id);
                if (parmenantAddress == null)
                    parmenantAddress = new AddressInformation();
                var inLawAddress = await _addressService.GetInLawsAddress(empInfo.Id);
                if (inLawAddress == null)
                    inLawAddress = new AddressInformation();
                var meternalAddress = await _addressService.GetMeternalAddress(empInfo.Id);
                if (meternalAddress == null)
                    meternalAddress = new AddressInformation();
                var medicalIno = await _employeeService.GetMedicalInfoByEmpId(empInfo.Id);
                if (medicalIno == null)
                    medicalIno = new List<MedicalInfoViewModel>();
                var signuture = await _addressService.GetSignutureById(empInfo.Id);

                var model = new EmployeeInfoViewModel
                {
                    applicationUser = userInfo,
                    employeeInfo = employeeInfo,
                    photograph = photograph,
                    spouses = spouses,
                    spouse = spouse,
                    assignments = assignments,
                    educationalQualifications = educationalQualifications,
                    awardEntries = awardEntries,
                    promotionLogs = promotionLogs,
                    traningLogs = traningLogs,
                    disciplinaryActions = disciplinaryActions,
                    addressInformation = addressInformation,
                    foreignTravels = travails,
                    signature = signuture,
                    presentAddress = presentAddress,
                    parmenantAddress = parmenantAddress,
                    inLawsAddress = inLawAddress,
                    meternalAddress = meternalAddress,
                    medicalInfo = medicalIno
                };
                return PartialView("_EmployeeInformation", model);
                //return PartialView("_UnitSectionWisePartialView2", model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        #endregion

        #region Api
        public async Task<IActionResult> GetPHQEmployeeList(int rankId, int unitId, int batch, int servicePeriod, int bandId, int isLocked, int isAttached, int isUnMission)
        {
            try
            {
                return Json(await _internalServices.GetSectionRankWiseEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetEmployeeListBySectionId(int id)
        {
            var data = await _internalServices.GetEmployeeListBySectionId(id);
            return Json(data);
        }
        #endregion

        #region Random Number
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region For PHQ Dashboard
        public async Task<IActionResult> OverDue()
        {
            PostingVacancy model = new PostingVacancy
            {
                employeeInfos = await _internalServices.GetOverDueEmployeeInfoList(),
                sections = _repoDepartment.GetAll(),
                ranks = await _assignmentService.GetRankGreaterASP(),
                batches = _repoBCSBatch.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> GetOverDueEmployeeInfoListFilter(int Id, int branch, int batch)
        {
            return Json(await _internalServices.GetOverDueEmployeeInfoListFilter(Id, branch, batch));
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetOverDuePositionReport(int Id, int branch, int batch)
        {
            PostingVacancy model = new PostingVacancy
            {
                employeeInfos = await _internalServices.GetOverDueEmployeeInfoList(Id, branch, batch),
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult GetOverDuePositionReportPdf(int Id, int branch, int batch)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/InternalPosting/InternalEmployee/GetOverDuePositionReport?id=" + Id + "&&branch=" + branch + "&&batch=" + batch;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

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
                InternalpostingReportViews = await userInfoes.GetRankWiseInternalAssignmentCopyOngoing((int)emp.rankId),
            };
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AssignmentMasterApprove()
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;

            var data = new AssignmentViewModel
            {
                //assignmentApprove = await _employeeService.AssignmentPostedMastersApprove(userId),
                Internalassignment2 = await _internalServices.ApprovedInternalAssignmentMastersList(userId),
                applicationUser = applicationUser
            };
            return View(data);
        }

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
                //assignment2 = await _employeeService.AssignmentReturnList(userId),
                Internalassignment2 = await _internalServices.AssignmentReturnList(userId)
            };
            return View(data);
        }
        #endregion

        #region Create Edit
        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> GetPIMSDataByBPNo(string bpNo)
        {
            if (bpNo.Contains("BP") || bpNo.Contains("CIV"))
            {
                if (bpNo.StartsWith("BP"))
                {
                    bpNo = bpNo.Replace("BP", "").Trim();
                }
                var check = await _internalServices.GetEmployeeInfoByBpNo(bpNo);
                PIMSDataModel pIMS = new PIMSDataModel();

                if (check == null)
                {
                    if (bpNo.StartsWith("BP"))
                    {
                        bpNo = bpNo.Replace(" ", "").Trim();
                    }
                    else
                    {
                        bpNo = "BP" + bpNo.Replace(" ", "").Trim();
                    }

                    string url = String.Format("https://pims.police.gov.bd:8443/pimslive/webpims/opus/bpdetails/{0}", bpNo);
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var smsData = await response.Content.ReadAsStringAsync();
                    var pIMSBody = JsonConvert.DeserializeObject<PIMSBody>(await response.Content.ReadAsStringAsync());
                    if (pIMSBody.items.Count() > 0)
                    {
                        pIMS = pIMSBody.items[0];
                    }
                    else
                    {
                        pIMS = new PIMSDataModel();
                    }

                    return Json(pIMS);
                }
                else
                {
                    pIMS.employee_status = "alreayExist";
                }

                return Json(pIMS);
            }
            else
            {
                var empInfo = await _internalServices.GetEmployeeInfoByBpNo(bpNo);
                var bpNumber = "";
                if (empInfo.employeeTypeId == 3)
                {
                    bpNumber = "CIV" + bpNo;
                }
                else
                {
                    bpNumber = "BP" + bpNo;
                }
                PIMSDataModel pIMS = new PIMSDataModel();
                if (bpNo.StartsWith("BP"))
                {
                    bpNo = bpNo.Replace(" ", "").Trim();
                }
                else
                {
                    bpNo = "BP" + bpNo.Replace(" ", "").Trim();
                }
                string url = String.Format("https://pims.police.gov.bd:8443/pimslive/webpims/opus/bpdetails/{0}", bpNumber);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var smsData = await response.Content.ReadAsStringAsync();
                var pIMSBody = JsonConvert.DeserializeObject<PIMSBody>(await response.Content.ReadAsStringAsync());
                if (pIMSBody.items.Count() > 0)
                {
                    pIMS = pIMSBody.items[0];
                }
                else
                {
                    pIMS = new PIMSDataModel();
                }
                return Json(pIMS);
            }

        }


        public async Task<IActionResult> SavePHQEmployee()
        {
            try
            {
                var model = new EmployeeInfoViewModel
                {
                    branch = _repoBranch.GetAll(),
                    rank = _repoRank.GetAll(),
                    section = _repoSection.GetAll(),
                    designations = _repoDesignation.GetAll(),
                    religions = _repoReligion.GetAll().OrderBy(x => x.name),
                    organizations = _organizationService.GetAll().OrderBy(x => x.organizationName),
                    Divisions = _division.GetAll().OrderBy(x => x.divisionName),
                    department = _repoDepartment.GetAll(),
                    sections = _section.GetAll().OrderBy(x => x.Name),
                    banks = _banks.GetAll().OrderBy(x => x.bankName),
                    fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                    fLangaward = _award.PerseLang("Employee/AwardEntryEN.json", "Employee/AwardEntryBN.json", Request.Cookies["lang"]),
                    fLangDiciplinary = _disiplinary.PerseLang("Employee/DisciplinaryActionEN.json", "Employee/DisciplinaryActionBN.json", Request.Cookies["lang"]),
                    flangeducation = _education.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                    fLangtraining = _training.PerseLang("Employee/TraningLogEN.json", "Employee/TraningLogBN.json", Request.Cookies["lang"]),
                    specialBranchUnits = _repoBranch.GetAll(),
                    awardEntryList = _repoAwardEntry.GetAll().OrderBy(x => x.awardName),
                    awardList = _repoAward.GetAll().OrderBy(x => x.awardName),
                    countries = _repoCountry.GetAll().OrderBy(x => x.countryName),
                    pHQTRTypes = _repoPHQTRType.GetAll(),


                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SavePHQEmployee([FromForm] EmployeeViewModel model)
        {
            try
            {
                var employeeCode = "";
                var baseImge = "";
                int? emptype = 0;
                if (model.bpNo.Contains("BP"))
                {
                    employeeCode = model.bpNo.Substring(2);
                    emptype = 2;
                }
                else if (model.bpNo.Contains("CIV"))
                {
                    employeeCode = model.bpNo.Substring(3);
                    emptype = 3;
                }
                else
                {
                    employeeCode = model.bpNo;
                    emptype = model.empTypeId;
                };
                EmployeeInfo empInfo = new EmployeeInfo
                {
                    Id = model.empId,
                    nameBangla = model.nameBangla,
                    nameEnglish = model.nameEnglish,
                    employeeCode = employeeCode,
                    rankId = model.rankId,
                    employeeTypeId = emptype,
                    designation = model.designation,
                    fatherNameEnglish = model.fatherNameEnglish,
                    motherNameEnglish = model.motherNameEnglish,
                    gender = model.gender,
                    sectionId = model.sectionId,
                    mobileNumberPersonal = model.mobileNumberPersonal,
                    nationalID = model.nationalID,
                    dateOfBirth = model.dateOfBirth,
                    bloodGroup = model.bloodGroup,
                    height = model.height,
                    joiningDateGovtService = model.policejoiningrank,
                    joiningDatePresentWorkstation = model.dateofjoining,
                    LPRDate = model.retirementdate,
                    weight = model.weight,
                    religionId = model.religionId,
                    maritalStatus = model.maritalStatus,
                    rationId = model.rationId,
                    branchId = model.specialBranchUnitId,
                };
                int id = await _employeeService.SaveEmployeeInformation(empInfo);
                model.empId = id;

                EmployeeGradation gradation = new EmployeeGradation
                {
                    employeeId = id,
                    nameBangla = model.nameBangla,
                    nameEnglish = model.nameEnglish,
                    employeeCode = employeeCode,
                    rankId = model.rankId,
                    gradationSerial=model.gradationSerial,
                    dateOfBirth = model.dateOfBirth,
                    joiningDateGovtService = model.policejoiningrank,
                    joiningDatePresentWorkstation = model.dateofjoining,
                    LPRDate = model.retirementdate,
                    sectionId=model.sectionId,
                    branchId = model.specialBranchUnitId,
                };
                await _employeeService.SaveEmployeeGradationInformation(gradation);

                if (model.photoPictures != null)
                {
                    if (model.photoPictures.Contains("data:image/"))
                    {

                        if (model.photoPictures.Contains("data:image/png"))
                        {
                            baseImge = model.photoPictures.Substring(22);
                        }
                        else
                        {
                            baseImge = model.photoPictures.Substring(23);
                        }
                    }
                    else
                    {
                        baseImge = model.photoPictures;
                    }

                    if (model.photoPictures != null)
                    {
                        int photoId = 0;
                        if (Convert.ToInt32(model.empId) > 0)
                        {
                            var photoInfo = await photographService.GetPhotographByType(Convert.ToInt32(model.empId), "profile");
                            if (photoInfo != null)
                            {
                                photoId = photoInfo.Id;
                            }
                        }

                        byte[] bytes = Convert.FromBase64String(baseImge);
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

                        Photograph photograph = new Photograph
                        {
                            Id = photoId,
                            employeeId = model.empId,
                            url = "EmpImages\\" + randomFileName,
                            type = "profile"
                        };
                        await photographService.SavePhotograph(photograph);
                    }
                    else
                    {

                    }
                }


                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> EditPHQEmployee(string empIdentityUserSessionToken)
        {
            try
            {

                var id = Convert.ToInt32(empIdentityUserSessionToken);
                var empInfo = await userInfoes.GetUserInfoByEmpId(id);

                var model = new EmployeeInfoViewModel
                {
                    branch = _repoBranch.GetAll(),
                    section = _repoSection.GetAll(),
                    designations = _repoDesignation.GetAll(),
                    department = _repoDepartment.GetAll(),
                    rank = _repoRank.GetAll(),
                    religions = _repoReligion.GetAll().OrderBy(x => x.name),
                    specialBranchUnitsALL = _repoBranch.GetAll(),
                    employeeInfo = empInfo,
                    sections = _section.GetAll().OrderBy(x => x.Name),
                    specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                    photograph = await _employeeService.GetEmployeePhotographByEmpId(empInfo.Id),
                    fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                    fLangaward = _award.PerseLang("Employee/AwardEntryEN.json", "Employee/AwardEntryBN.json", Request.Cookies["lang"]),
                    fLangDiciplinary = _disiplinary.PerseLang("Employee/DisciplinaryActionEN.json", "Employee/DisciplinaryActionBN.json", Request.Cookies["lang"]),
                    flangeducation = _education.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                    fLangtraining = _training.PerseLang("Employee/TraningLogEN.json", "Employee/TraningLogBN.json", Request.Cookies["lang"]),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveImage(string photo, int? empId)
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
                return Json("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }





        }

        #endregion

        #region Lock
        public async Task<IActionResult> LockInternal(int UserId, string verifyMessage)
        {
            try
            {
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _repoInternalAssignmentMaster.Get(Convert.ToInt32(UserId));
                data.statusId = 4;
                _repoInternalAssignmentMaster.Update(data);
                int type = 1;
                if (data.assignmentTypeId == 2)
                {
                    type = 2;
                }
                var update = await _internalServices.UpdateAssignmentInfoForIGP(data.Id);

                InternalApprovalLog approvalLog = new InternalApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    isActive = 2,
                    notes = verifyMessage,
                };
                _repoInternalApprovalLog.Insert(approvalLog);

                InternalAssignmentMaster internalAssignmentMaster = new InternalAssignmentMaster
                {
                    applicationUserId = data.ApplicationUserId,
                    refNo = data.refNo,
                    refDate = Convert.ToDateTime(data.refDate),
                    statusId = 4,
                    internalEnlistedAssignmentMasterId = data.Id,
                };
                int masterid = await _internalServices.SaveInternalAssignmentMaster(internalAssignmentMaster);

                var details = await _internalServices.GetInternalEnlistedAssignmentsByMasterId(Convert.ToInt32(UserId));

                foreach (var item in details)
                {
                    InternalAssignment internalAssignment = new InternalAssignment
                    {
                        assignmentMasterId = masterid,
                        employeeId = item.employeeId,
                        departmentId = item.sectionId,
                        receiveRefNo = item.remarks,
                        assignmentTypeId = type,
                        statusId = 4
                    };
                    _repoInterAssignment.Insert(internalAssignment);
                }
                return Json("Lock");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        #endregion

        public async Task<IActionResult> EditPostingConfirmDetails(int id)
        {
            ViewBag.AssignmentMasterId = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                aVM = await _employeeService.GetInternalAssignmentListbyMasterId(id),
                internalAssignments = await _employeeService.InternalAssignmentPostedByMasterId(id),
                internalApprovals = await _assignmentService.GetInternalApprovalLogByAssignmentMasterId(id),
            };
            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> PostingConfirmNote(int id)
        {
            ViewBag.AssignmentMasterId = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                aVM = await _employeeService.GetInternalAssignmentListbyMasterId(id),
                internalAssignments = await _employeeService.InternalAssignmentPostedByMasterId(id),
                internalApprovals = await _assignmentService.GetInternalApprovalLogByAssignmentMasterId(id),
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingConfirmNotePdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/InternalPosting/InternalEmployee/PostingConfirmNote?id=" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [HttpPost]
        public IActionResult SaveAssignMasterForNoteSheet(AssignmentViewModel model)
        {
            try
            {
                var masterData = _repoInterAssignmentMaster.Get((int)model.assignMasterId);
                masterData.noteSheetTitle = model.noteSheetTitle;
                masterData.noteSheetDescription = model.noteComment;
                _repoInterAssignmentMaster.Update(masterData);
                return Json("Save");
                //return RedirectToAction("PostingConfirmNotePdf", "Report", new { id = model.assignMasterId });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IActionResult> GetUnitRankWisePartialEmployeeViewJson(string rankId, int unitId, int batch, int servicePeriod, int bandId, int isLocked, int isAttached, int isUnMission)
        {
            try
            {
                if (rankId == null)
                {
                    rankId = "0";
                }
                return Json(await _internalServices.GetMutipleRankWiseSectionEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}