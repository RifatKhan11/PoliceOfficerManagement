using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Portfolio.Models;
using AlphaManagement.Web.Helpers;
using AlphaManagement.Web.Models.JsonModel;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AlphaManagement.Web.Areas.Portfolio.Controllers
{
    [Area("Portfolio")]
    [Authorize]
    public class DashBoardController : Controller
    {
        private IPortfolioDashBoard _portfolioDashBoard;
        private readonly IRepository<Country> _repoCountry;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<SpecialBranchUnit> _repoUnit;
        private readonly IRepository<BCSBatch> _repoBatch;
        private readonly IEmployeeService _employeeService;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _userRole;
        private readonly IAddressServices _addressService; private readonly string rootPath;
        private readonly IConfiguration _configuration;
        private readonly LangGenerate<JsonRank> _jsonRank;
        private readonly LangGenerate<JsonUnit> _jsonUnit;
        private readonly LangGenerate<JsonContact> _jsonContact;
        private readonly MyPDF myPDF;
        public string FileName;
        public DashBoardController(IPortfolioDashBoard portfolioDashBoard, IAddressServices addressService, IEmployeeService employeeService, IRepository<Country> repoCountry, IRepository<SpecialBranchUnit> repoUnit, IRepository<BCSBatch> repoBatch,
            IRepository<Rank> repoRank, IHostingEnvironment hostingEnvironment, IConfiguration _configuration, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> userRole, IConverter converter)
        {
            _portfolioDashBoard = portfolioDashBoard;
            _repoCountry = repoCountry;
            _repoRank = repoRank;
            _userManager = userManager;
            _userRole = userRole;
            _repoUnit = repoUnit;
            _repoBatch = repoBatch;
            _employeeService = employeeService;
            _addressService = addressService;
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            this._configuration = _configuration;
            rootPath = hostingEnvironment.ContentRootPath;
            _jsonRank = new LangGenerate<JsonRank>(hostingEnvironment.ContentRootPath);
            _jsonUnit = new LangGenerate<JsonUnit>(hostingEnvironment.ContentRootPath);
            _jsonContact = new LangGenerate<JsonContact>(hostingEnvironment.ContentRootPath);
        }

        public async Task<IActionResult> Index()
        {
            var BpNo = User.Identity.Name;
            var model = new DashBoardViewModel
            {
                employeeInfo = await _portfolioDashBoard.GetUserEmployeeInfo(BpNo)
            };
            return View(model);
        }

        #region AK47
        [HttpPost]
        public async Task<IActionResult> SaveForeignTravelData([FromForm] ForeignTravelViewModel model)
        {
            var foreignTravel = new ForeignTravel
            {
                Id = 0,
                employeeId = model.employeeId,
                countryId = model.countryId,
                referenceNumber = model.referenceNumber,
                travelDuration = model.travelDuration,
                travelPurpose = model.travelPurpose,
                status = 2,
                travelDate = Convert.ToDateTime(model.travelDate?.ToString("yyyy-MM-dd")),
                travelEndDate = Convert.ToDateTime(model.travelEndDate?.ToString("yyyy-MM-dd")),
            };
            await _employeeService.SaveForeignTravel(foreignTravel);
            return Json("Save");
        }

        public async Task<IActionResult> ListForArticle47()
        {
            var BpNo = User.Identity.Name;
            var model = new DashBoardViewModel
            {
                employeeInfo = await _portfolioDashBoard.GetUserEmployeeInfo(BpNo),
                assignments = await _portfolioDashBoard.GetAssignmentsByEmployeeCode(BpNo),
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                rank = _repoRank.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAssignmentForArticle47(DashBoardViewModel model)
        {
            try
            {
                var assignment = await _employeeService.GetAssignmentInfoById(model.assignmentId);
                if (model.type == 1)
                {
                    assignment.StartDate = model.joiningDate;
                    assignment.articleType = ArticleType.TakingCharge;
                }
                else if (model.type == 2)
                {
                    assignment.EndDate = model.releaseDate;
                    assignment.articleType = ArticleType.ToDepurture;
                }
                if (model.Schedule == 1)
                {
                    assignment.releaseDayTime = DayTime.Morning;
                }
                else if (model.Schedule == 2)
                {
                    assignment.joinDayTime = DayTime.Afternoon;
                }
                assignment.articleStatus = ArticleStatus.Suberviser;
                assignment.supervisorId = model.supervisorId;
                var update = await _employeeService.SaveAssignDetails(assignment);
                if (update == assignment.Id)
                {
                    return Json("Done");
                }
                else
                {
                    return Json("Fail");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> Article47Index(int assignmentId)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            var assignment = await _employeeService.GetAssignmentInfoById(assignmentId);
            if (assignment?.employee?.rank?.Id == 1 || assignment?.employee?.rank?.Id == 2 || assignment?.employee?.rank?.Id == 4 || assignment?.employee?.rank?.Id == 6 || assignment?.employee?.rank?.Id == 14)
            {
                if (Convert.ToString(assignment?.articleType) == "TakingCharge")
                {
                    url = $"" + scheme + "://" + host + "/Portfolio/DashBoard/Article41AboveSP?assignmentId=" + assignmentId;
                }
                else if (Convert.ToString(assignment?.articleType) == "ToDepurture")
                {
                    url = $"" + scheme + "://" + host + "/Portfolio/DashBoard/ReleaseArticle41AboveSP?assignmentId=" + assignmentId;
                }
                else
                {
                    url = $"" + scheme + "://" + host + "/Portfolio/DashBoard/Article41AboveSP?assignmentId=" + assignmentId;
                }
            }
            else
            {
                if (Convert.ToString(assignment?.articleType) == "TakingCharge")
                {
                    url = $"" + scheme + "://" + host + "/Portfolio/DashBoard/Article41?assignmentId=" + assignmentId;
                }
                else if (Convert.ToString(assignment?.articleType) == "ToDepurture")
                {
                    url = $"" + scheme + "://" + host + "/Portfolio/DashBoard/ReleaseArticle41?assignmentId=" + assignmentId;
                }
                else
                {
                    url = $"" + scheme + "://" + host + "/Portfolio/DashBoard/Article41?assignmentId=" + assignmentId;
                }
            }
            string status = myPDF.GeneratePDFLegal(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Article41AboveSP(int assignmentId)
        {
            var model = new DashBoardViewModel
            {
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Article41(int assignmentId)
        {
            var model = new DashBoardViewModel
            {
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReleaseArticle41AboveSP(int assignmentId)
        {
            var model = new DashBoardViewModel
            {
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReleaseArticle41(int assignmentId)
        {
            var model = new DashBoardViewModel
            {
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        #region Preview AK47
        [AllowAnonymous]
        public async Task<IActionResult> PreviewArticle41AboveSP(int assignmentId)
        {
            var username = User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(username);
            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var model = new DashBoardViewModel
            {
                user = userInfo,
                userRole = userRoles,
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PreviewArticle41(int assignmentId)
        {
            var username = User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(username);
            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var model = new DashBoardViewModel
            {
                user = userInfo,
                userRole = userRoles,
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PreviewArticle41ForApprove(int assignmentId)
        {
            var username = User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(username);
            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var model = new DashBoardViewModel
            {
                user = userInfo,
                userRole = userRoles,
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> PreviewArticle41ForAdminApprove(int assignmentId)
        {
            var username = User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(username);
            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var model = new DashBoardViewModel
            {
                user = userInfo,
                userRole = userRoles,
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PreviewArticle41ForSuperviser(int assignmentId)
        {
            var username = User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(username);
            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var model = new DashBoardViewModel
            {
                user = userInfo,
                userRole = userRoles,
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PreviewReleaseArticle41AboveSP(int assignmentId)
        {
            var username = User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(username);
            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var model = new DashBoardViewModel
            {
                user = userInfo,
                userRole = userRoles,
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PreviewReleaseArticle41(int assignmentId)
        {
            var username = User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(username);
            var userRoles = await _userManager.GetRolesAsync(userInfo);
            var model = new DashBoardViewModel
            {
                user = userInfo,
                userRole = userRoles,
                assignment = await _employeeService.GetAssignmentInfoById(assignmentId)
            };
            return View(model);
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> SupervisorList(string id)
        {
            var data = await _portfolioDashBoard.GetSupervisorInfo(id);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> Article47ForAdmin()
        {
            var data = new DashBoardViewModel
            {
                assignments = await _portfolioDashBoard.GetAssignmentsForAdmin()
            };
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> ApprovedArticle47ForAdmin()
        {
            var data = new DashBoardViewModel
            {
                assignments = await _portfolioDashBoard.GetApprovedA47ForAdmin()
            };
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Article47ForSupervisor()
        {
            var supervisor = await _employeeService.GetEmployeeInfoById(User.Identity.Name);
            var data = new DashBoardViewModel
            {
                assignments = await _portfolioDashBoard.GetAssignmentsForSupervisor(supervisor.Id)
            };
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> PreviewArticle47ForAdmin(int id)
        {
            var assignment = await _employeeService.GetAssignmentInfoById(id);
            if (assignment?.employee?.rank?.Id == 1 || assignment?.employee?.rank?.Id == 2 || assignment?.employee?.rank?.Id == 4 || assignment?.employee?.rank?.Id == 6 || assignment?.employee?.rank?.Id == 14)
            {
                if (Convert.ToString(assignment?.articleType) == "TakingCharge")
                {
                    return RedirectToAction("PreviewArticle41AboveSP", "DashBoard", new { area = "Portfolio", assignmentId = id });
                }
                else if (Convert.ToString(assignment?.articleType) == "ToDepurture")
                {
                    return RedirectToAction("PreviewReleaseArticle41AboveSP", "DashBoard", new { area = "Portfolio", assignmentId = id });
                }
                else
                {
                    return Json("Type Not Selected");
                }
            }
            else
            {
                if (Convert.ToString(assignment?.articleType) == "TakingCharge")
                {
                    return RedirectToAction("PreviewArticle41", "DashBoard", new { area = "Portfolio", assignmentId = id });
                }
                else if (Convert.ToString(assignment?.articleType) == "ToDepurture")
                {
                    return RedirectToAction("PreviewReleaseArticle41", "DashBoard", new { area = "Portfolio", assignmentId = id });
                }
                else
                {
                    return Json("Type Not Selected");
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateAssignmentForArticle47Admin(int id)
        {
            try
            {
                var assignment = await _employeeService.GetAssignmentInfoById(id);
                assignment.articleStatus = ArticleStatus.Approved;
                assignment.statusId = 3;
                var update = await _employeeService.SaveAssignDetails(assignment);
                var empId = await _employeeService.UpdateEmployeeInfoAfterArticle47(id);
                return Json("Done");
            }
            catch (Exception ex)
            {

                return Json("Fail");
            }

        }

        public async Task<IActionResult> UpdateAssignmentForArticle47Supervisor(int id)
        {
            try
            {
                var assignment = await _employeeService.GetAssignmentInfoById(id);
                assignment.articleStatus = ArticleStatus.Ongoing;
                var update = await _employeeService.SaveAssignDetails(assignment);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json("Fail");
            }

        }
        #endregion

        #region Travel Clearence
        public async Task<IActionResult> TravelClearence()
        {
            var BpNo = User.Identity.Name;
            var model = new DashBoardViewModel
            {
                employeeInfo = await _portfolioDashBoard.GetUserEmployeeInfo(BpNo),
                countries = _repoCountry.GetAll().OrderBy(x => x.countryName),
                //foreignTravels = await _portfolioDashBoard.GetForeignTravelsById(BpNo)
            };
            return View(model);
        }
        #endregion

        #region Office Order
        public async Task<IActionResult> OfficeOrder()
        {
            var data = new PostingReportViewModel
            {
                postingReportViews = await _employeeService.ApprovedAssignmentMasters("")
            };
            return View(data);
        }
        #endregion

        #region Officer's Search
        public async Task<IActionResult> OfficersSearch()
        {
            var BpNo = User.Identity.Name;
            var model = new DashBoardViewModel
            {
                rank = _repoRank.GetAll(),
                batches = _repoBatch.GetAll(),
                specialBranchUnits = _repoUnit.GetAll(),
                employeeInfo = await _portfolioDashBoard.GetUserEmployeeInfo(BpNo),
            };
            return View(model);
        }

        public async Task<IActionResult> SearchEmployee(string input, int rank, int unit, int batch)
        {
            var data = await _portfolioDashBoard.GetSearchEmployeeInfo(input, rank, unit, batch);
            return Json(data);
        }
        #endregion

        #region PhoneBook
        public async Task<IActionResult> PhoneBookIndex()
        {

            List<JsonRank> ranks = _jsonRank.JsonPerser("https://firebasestorage.googleapis.com/v0/b/policeapp-e5fac.appspot.com/o/rank.json?alt=media&token=6e57b2e4-5263-4555-b50e-4eef601e1c07");
            List<JsonContact> contacts = _jsonContact.JsonPerser("https://firebasestorage.googleapis.com/v0/b/policeapp-e5fac.appspot.com/o/contact.json?alt=media&token=05bb3aad-a6a3-415e-bff1-7dbc6a82d133");
            List<JsonUnit> units = _jsonUnit.JsonPerser("https://firebasestorage.googleapis.com/v0/b/policeapp-e5fac.appspot.com/o/unit.json?alt=media&token=ee6215ee-a567-455e-88da-623ff1b159c2");
            //List<JsonRank> ranks = _jsonRank.JsonPerser("rank.json");
            //List<JsonContact> contacts = _jsonContact.JsonPerser("contact.json");
            //List<JsonUnit> units = _jsonUnit.JsonPerser("unit.json");
            foreach (var data in contacts)
            {
                var str = data.unit_id.Split(",");
                data.lastplace = str.Last();
            }
            var model = new PhoneBookViewModel
            {
                jsonRanks = ranks,
                jsonUnits = units,
                jsonContacts = contacts,
                //unitWisePhoneBooks = await _portfolioDashBoard.GetUnitWiseSectionPhoneBook()
            };

            return View(model);
        }
        #endregion
    }
}