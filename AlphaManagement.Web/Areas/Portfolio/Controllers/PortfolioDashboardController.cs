using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlphaManagement.Web.Areas.Portfolio.Controllers
{
    [Area("Portfolio")]
    public class PortfolioDashboardController : Controller
    {
        private IEmployeeService _employeeService;
        private readonly MyPDF myPDF;
        private string rootPath;
        private IHostingEnvironment hostingEnvironment;
        private readonly IUserInfoes userInfoes;
        private readonly IAddressServices _addressService;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<SpecialBranchUnit> _specialBranchUnit;
        private readonly IRepository<BCSBatch> _bCSBatch;
        private readonly IRepository<EmployeeReturnReason> _returnReason;
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public PortfolioDashboardController(
             IEmployeeService employeeService,
             IUserInfoes userInfoes,
             IAddressServices _addressService,
             IRepository<Rank> repoRank,
             IRepository<SpecialBranchUnit> specialBranchUnit,
             IRepository<BCSBatch> bCSBatch,
             IRepository<EmployeeReturnReason> returnReason,
             UserManager<ApplicationUser> userManager,
            IConfiguration _configuration,
            IHostingEnvironment hostingEnvironment,
            IConverter converter
            )
        {
            this._employeeService = employeeService;
            this.userInfoes = userInfoes;
            this.hostingEnvironment = hostingEnvironment;
            this._addressService = _addressService;
            _repoRank = repoRank;
            _specialBranchUnit = specialBranchUnit;
            _bCSBatch = bCSBatch;
            _returnReason = returnReason;
            _userManager = userManager;
            this._configuration = _configuration;
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        [Authorize(Roles = "Super Admin,PHQ Approver")]
        public async Task<IActionResult> Index()
        {
            EmployeePortfolioDashBoardViewModel model = new EmployeePortfolioDashBoardViewModel
            {
               // registration = await _employeeService.GetStatusWiseEmployeeCount(1),
                onGoing = await _employeeService.GetStatusWiseEmployeeCount(2),
                finalSubmit = await _employeeService.GetStatusWiseEmployeeCount(3),
                varified = await _employeeService.GetStatusWiseEmployeeCount(4),
                returned = await _employeeService.GetStatusWiseEmployeeCount(6),
                checkedItem = await _employeeService.GetCheckedEmployeeCount(2),
                todaycheckedItem = await _employeeService.GetCheckedEmployeeCount(0),
                //   employeeInfoList = await _employeeService.GetEmployeeInfoList(0)
                employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0, 0, "All"),

               // todaysRegistration = await _employeeService.GetTodaysEmployeesStatusWise(1),
                todaysOnGoing = await _employeeService.GetTodaysEmployeesStatusWise(2),
                todaysFinalSubmit = await _employeeService.GetTodaysEmployeesStatusWise(3),
                todaysVarified = await _employeeService.GetTodaysEmployeesStatusWise(4),
                todaysReturned = await _employeeService.GetTodaysEmployeesStatusWise(6),

            };
            //  model.registred = await _employeeService.GetStatusWiseEmployeeCount(0);
            model.totalRegistration = model.onGoing + model.finalSubmit;// + model.varified + model.returned + model.checkedItem;
            model.todaysTotalRegistration =  model.todaysOnGoing + model.todaysFinalSubmit + model.todaysVarified + model.todaysReturned;
            model.unitList = await _employeeService.GetUnitWiseEmployeeCount();
            return View(model);
        }


        public async Task<IActionResult> EmployeeList(int statusId, string periodType)
        {
            ViewBag.status = statusId;
            ViewBag.periodType = (periodType == null) ? "" : periodType;
            var data = new EmployeeInfoViewModel
            {
                ranks = _repoRank.GetAll(),
                units = _specialBranchUnit.GetAll(),
                bCSBatch = _bCSBatch.GetAll(),
                //   employeeInfoList = await _employeeService.GetEmployeeInfoList(statusId)
                //  employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0),

            };
            return View(data);
        }
          public async Task<IActionResult> EmployeeListNew()
        {
            ViewBag.status = 0;
            ViewBag.periodType = "All";
            var data = new EmployeeInfoViewModel
            {
                ranks = _repoRank.GetAll(),
                units = _specialBranchUnit.GetAll(),
                bCSBatch = _bCSBatch.GetAll(),
                //   employeeInfoList = await _employeeService.GetEmployeeInfoList(statusId)
                //  employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0),

            };
            return View(data);
        }


        public async Task<IActionResult> EmployeeListForChecker(int statusId, string periodType)
        {
            ViewBag.status = statusId;
            ViewBag.periodType = (periodType == null) ? "" : periodType;
            var data = new EmployeeInfoViewModel
            {
                ranks = _repoRank.GetAll(),
                units = _specialBranchUnit.GetAll(),
                bCSBatch = _bCSBatch.GetAll(),
                //   employeeInfoList = await _employeeService.GetEmployeeInfoList(statusId)
                //  employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0),

            };
            return View(data);
        }

        [Authorize(Roles = "Super Admin,Departmental User,Admin,Portfolio Checker,PHQ Approver")]
        public async Task<IActionResult> EmployeePartialList(int unitId, int rankId, int batchId, int statusId, string periodType)
        {
            periodType = (periodType == null) ? "" : periodType;
            var data = new EmployeeInfoViewModel
            {
                employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, unitId, rankId, batchId, statusId, periodType),

            };
            return PartialView("_EmployeePartialList", data);
        }

        [Authorize(Roles = "Super Admin,Departmental User,Admin,Portfolio Checker,PHQ Approver")]
        public async Task<IActionResult> EmployeePartialListNew(int unitId, int rankId, int batchId, int statusId, string periodType)
        {
            periodType = (periodType == null) ? "" : periodType;
            var data = new EmployeeInfoViewModel
            {
                employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, unitId, rankId, batchId, statusId, periodType),

            };
            return PartialView("_EmployeePartialListNew", data);
        }

        

        [Authorize(Roles = "Super Admin,Departmental User,Admin,PHQ Approver")]
        public async Task<IActionResult> EmployeeDetails(string empIdentityUserSessionToken)
        {
            try
            {
                if (User.Identity.Name == null)
                {
                    return RedirectToAction("Not404Found", "Home");
                }


                var _empInfo = _employeeService.GetBasicEmployeeInfoById(Convert.ToInt32(empIdentityUserSessionToken));
                if (_empInfo == null)
                {
                    return RedirectToAction("Not404Found", "Home");
                }
                
                var userInfo = await userInfoes.GetUserInfoByUser(_empInfo?.employeeCode);
                EmployeeInfo employeeInfo = new EmployeeInfo();
                var empInfo = await _employeeService.GetEmployeeInfoById(_empInfo?.employeeCode);
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
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [Authorize(Roles = "Super Admin,Departmental User,Portfolio Checker,Admin,PHQ Approver")]
        public async Task<IActionResult> EmployeeDetailsForChecker(string empIdentityUserSessionToken)
        {
            try
            {
                if (User.Identity.Name == null)
                {
                    return RedirectToAction("Not404Found", "Home");
                }


                var _empInfo = _employeeService.GetBasicEmployeeInfoById(Convert.ToInt32(empIdentityUserSessionToken));
                if (_empInfo == null)
                {
                    return RedirectToAction("Not404Found", "Home");
                }

                var userInfo = await userInfoes.GetUserInfoByUser(_empInfo?.employeeCode);
                EmployeeInfo employeeInfo = new EmployeeInfo();
                var empInfo = await _employeeService.GetEmployeeInfoById(_empInfo?.employeeCode);
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
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> UpdateSecuritySetting(string empCode, int status)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var empInfo = await _employeeService.GetEmployeeInfoById(empCode);
            if (empInfo == null)
            {
                return Json(new { Success = false, Message = "Employee Info Not Found." });
            }
            try
            {
                empInfo.designationCheck = status;
                int id = await _employeeService.SaveEmployeeInformation(empInfo);
                return Json(new { Success = true, Message = "Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = "Invalid Attempt!" });
            }
        }

        public async Task<IActionResult> UpdateEmployeeStatus(string empCode, int status)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var empInfo = await _employeeService.GetEmployeeInfoById(empCode);
            if (empInfo == null)
            {
                return Json(new { Success = false, Message = "Employee Info Not Found." });
            }
            try
            {
                if (status == 8)
                {
                    empInfo.isAdminCheck = 2;
                }
                empInfo.isApproved = status;
                int id = await _employeeService.SaveEmployeeInformation(empInfo);
                await _employeeService.SaveEmployeeTransectionHistoryLog(empInfo.Id, status, user.Id, "Portfolio", "");
                return Json(new { Success = true, Message = "Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = "Invalid Attempt!" });
            }
        }

        public async Task<IActionResult> UpdateEmployeeStatusChecked(string empCode, int status)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var empInfo = await _employeeService.GetEmployeeInfoById(empCode);
            if (empInfo == null)
            {
                return Json(new { Success = false, Message = "Employee Info Not Found." });
            }
            try
            {
                empInfo.isAdminCheck = status;
                int id = await _employeeService.SaveEmployeeInformation(empInfo);
                await _employeeService.SaveEmployeeTransectionHistoryLog(empInfo.Id, 5, user.Id, "Portfolio", "");
                return Json(new { Success = true, Message = "Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = "Invalid Attempt!" });
            }
        }
        

        public async Task<IActionResult> UpdateEmployeeStatusUndoByEditUser(string empCode, int status)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var empInfo = await _employeeService.GetEmployeeInfoById(empCode);
            if (empInfo == null)
            {
                return Json(new { Success = false, Message = "Employee Info Not Found." });
            }
            try
            {
                empInfo.isAdminCheck = 1;
                empInfo.isApproved = 3;
                int id = await _employeeService.SaveEmployeeInformation(empInfo);
                await _employeeService.SaveEmployeeTransectionHistoryLog(empInfo.Id, 12, user.Id, "Portfolio", "");
                return Json(new { Success = true, Message = "Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = "Invalid Attempt!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReturnPortfolio(int returnEmpId, int[] returnType, string[] returnReason)
        {
            var userInfo = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            if (returnType.Length > 0)
            {
                for (int i = 0; i < returnType.Length; i++)
                {
                    EmployeeReturnReason reason = new EmployeeReturnReason
                    {
                        Id = 0,
                        type = returnType[i],
                        reason = returnReason[i],
                        employeeInfoId = returnEmpId,
                        status = 1,
                        ApplicationUserId = userInfo.Id
                    };
                    _returnReason.Insert(reason);
                }

                _employeeService.UpdateEmployeeInfoStatusById(returnEmpId, 6);
            }
            return RedirectToAction(nameof(EmployeeList));
        }

        [HttpPost]
        public async Task<IActionResult> ReturnPortfolioForCheck(int returnEmpId, int[] returnType, string[] returnReason)
        {
            var userInfo = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            if (returnType.Length > 0)
            {
                for (int i = 0; i < returnType.Length; i++)
                {
                    EmployeeReturnReason reason = new EmployeeReturnReason
                    {
                        Id = 0,
                        type = returnType[i],
                        reason = returnReason[i],
                        employeeInfoId = returnEmpId,
                        status = 2,
                        ApplicationUserId = userInfo.Id
                    };
                    _returnReason.Insert(reason);
                }

                 _employeeService.UpdateEmployeeInfoisAdminCheckStatusById(returnEmpId, 1);
            }
            return RedirectToAction("AdminCheckedEmployeeList", "EmployeeInfoHistory", new { Area = "Employee" });
        }

        [HttpPost]
        public async Task<IActionResult> ReturnPortfolioForCheckSingle(int returnEmpId, int returnTypeId, string returnRemarks)
        {
            var userInfo = await userInfoes.GetUserInfoByUser(User.Identity.Name);
                    EmployeeReturnReason reason = new EmployeeReturnReason
                    {
                        Id = 0,
                        type = returnTypeId,
                        reason = returnRemarks,
                        employeeInfoId = returnEmpId,
                        status = 2,
                        ApplicationUserId = userInfo.Id
                    };
                    _returnReason.Insert(reason);

             _employeeService.UpdateEmployeeInfoisAdminCheckStatusById(returnEmpId, 1);
            await _employeeService.SaveEmployeeTransectionHistoryLog(returnEmpId,6,userInfo.Id,"Portfolio",returnRemarks);
            return Json(await _employeeService.GetEmployeeReturnReasonEmployee(returnEmpId));
        }



        [HttpPost]
        public async Task<ActionResult> EditPermissionEmployees([FromForm] EmployeePortfolioDashBoardViewModel model)
        {
            string notUpdated = "";
            string userName = User.Identity.Name;
            try
            {
                if (model.employeeCodes.Count() > 0)
                {

                    for (int i = 0; i < model.employeeCodes.Length; i++)
                    {
                        if (model.isEditCheck[i] == 1)
                        {
                            var empInfo = await _employeeService.GetEmployeeInfoById(model.employeeCodes[i]);
                            if (empInfo == null)
                            {
                                notUpdated += model.employeeCodes[i] + ",";
                            }
                            try
                            {
                                empInfo.isApproved = 1;
                                int id = await _employeeService.SaveEmployeeInformation(empInfo);

                            }
                            catch (Exception ex)
                            {
                                notUpdated += model.employeeCodes[i] + ",";
                                // return Json(new { Success = false, Message = "Invalid Attempt!" });
                            }
                        }




                    }



                }
                return Json(new { Success = true, Message = "Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Invalid Attempts", notUpdated, Success = false });
                //throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> VarifiedEmployees([FromForm] EmployeePortfolioDashBoardViewModel model)
        {
            string notUpdated = "";
            string userName = User.Identity.Name;
            try
            {
                if (model.employeeCodes.Count() > 0)
                {

                    for (int i = 0; i < model.employeeCodes.Length; i++)
                    {



                        if (model.isCheck[i] == 1)
                        {
                            var empInfo = await _employeeService.GetEmployeeInfoById(model.employeeCodes[i]);
                            if (empInfo == null)
                            {
                                notUpdated += model.employeeCodes[i] + ",";
                            }
                            try
                            {
                                empInfo.isApproved = 4;
                                int id = await _employeeService.SaveEmployeeInformation(empInfo);

                            }
                            catch (Exception ex)
                            {
                                notUpdated += model.employeeCodes[i] + ",";
                                // return Json(new { Success = false, Message = "Invalid Attempt!" });
                            }
                        }




                    }



                }
                return Json(new { Success = true, Message = "Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Invalid Attempts", notUpdated, Success = false });
                //throw;
            }
        }

        [AllowAnonymous]
        public IActionResult EmployeeInfoReportViewPDF(string empIdentityUserSessionToken)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Not404Found", "Home");
            }

            var empInfo = _employeeService.GetBasicEmployeeInfoById(Convert.ToInt32(empIdentityUserSessionToken));
            
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Portfolio/PortfolioDashboard/EmployeeInfoReportViewAdmin?id=" + empInfo?.employeeCode;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        public async Task<IActionResult> EmployeeInfoReportViewAdmin(string id)
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
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [Route("global/api/GetEmployeeReturnReasonEmployee/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeReturnReasonEmployee(int Id)
        {
            return Json(await _employeeService.GetEmployeeReturnReasonEmployee(Id));
        }

        [Route("global/api/GetRankWiseEmployeeCount")]
        [HttpGet]
        public async Task<IActionResult> GetRankWiseEmployeeCount()
        {
            return Json(await _employeeService.GetRankWiseEmployeeCount());
        }

        [Route("global/api/GetRankWiseEmployeeCountChecked")]
        [HttpGet]
        public async Task<IActionResult> GetRankWiseEmployeeCountChecked()
        {
            return Json(await _employeeService.GetRankWiseEmployeeCountChecked());
        }

        [Route("global/api/GetDivisionWiseEmployeeCount")]
        [HttpGet]
        public async Task<IActionResult> GetDivisionWiseEmployeeCount()
        {
            var result = await _employeeService.GetDivisionWiseEmployeeCount();
            return Json(result);
        }

        [Route("api/PortfolioDashboard/GetPercentWisePrograss")]
        [HttpGet]
        public async Task<IActionResult> GetPercentWisePrograss()
        {
            var result = await _employeeService.GetPercentWisePrograss();
            return Json(result);
        }
        

        [Route("global/api/GetBatchWiseEmployeeCount")]
        [HttpGet]
        public async Task<IActionResult> GetBatchWiseEmployeeCount()
        {
            return Json(await _employeeService.GetBatchWiseEmployeeCount());
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUnitWiseEmployeePortfolioList(int unitId, int rankId, int batchId, int divisionId)
        {

            var data = new EmployeeInfoViewModel
            {
                UnitWiseEmployeeInfosViewModelFor_SP = await _employeeService.GetUnitWiseEmployeeInfoListForSp(User.Identity.Name, unitId, rankId, batchId, divisionId)
            };

            return PartialView("_unitWiseEmployeePartialList", data);
            // return Json(employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetPercentPrograssEmployeePortfolioList(int? status)
        {
            var data = new EmployeeInfoViewModel
            {
                UnitWiseEmployeeInfosViewModelFor_SP = await _employeeService.GetEmployeePercentPrograssList(status)
            };
            
            return PartialView("_unitWiseEmployeePartialList", data);
            // return Json(employees);
        }

    }
}