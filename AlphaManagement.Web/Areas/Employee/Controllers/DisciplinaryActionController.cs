using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.EmployeeService.interfaces;
using Microsoft.AspNetCore.Authorization;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.DAL.Entity.AddressData;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class DisciplinaryActionController : Controller
    {
        private readonly LangGenerate<DisciplinaryActionLn> _lang;
        private readonly IRepository<DisciplinaryAction> _repoDisciplinaryAction;
        private readonly IRepository<EmployeeInfo> _repoEmployeeInfo;
        private readonly IRepository<Offense> _repoOffense;
        private readonly IRepository<NaturalPunishment> _repoNaturalPunishment;
        private readonly IEmployeeService _employeeService;
        private readonly IUserInfoes _userInfoes;
        private readonly IReportService _reportService;
        private readonly IRepository<ACRInformation> _acrInformation;
        private readonly IRepository<TrainingCategory> _trainingCategory;
        private readonly IRepository<Country> _repoCountry;
        private readonly IRepository<TrainingInstitute> _trainingInstitute;
        private readonly IRepository<TraningLog> _traningLog;
        private readonly IRepository<SpecialSkillType> _specialSkillType;
        private readonly IRepository<EmployeeReportInfo> _repoEmployeeReportInfo;
        private readonly IRepository<MedicalMainCategory> _repoMedicalMainCategory;
        private readonly IRepository<MedicalSubCategory> _repoMedicalSubCategory;
        private readonly IRepository<EmployeeMadicalInfo> _repoEmployeeMadicalInfo;
        private readonly IRepository<SpecialBranchUnit> _repoSpecialBranchUnit;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<BCSBatch> _repoBCSBatch;

        public DisciplinaryActionController(IHostingEnvironment hostingEnvironment,
            IRepository<DisciplinaryAction> repoDisciplinaryAction,
            IRepository<EmployeeInfo> repoEmployeeInfo,
            IRepository<Offense> repoOffense,
            IRepository<NaturalPunishment> repoNaturalPunishment,
            IRepository<ACRInformation> acrInformation,
            IEmployeeService employeeService,
            IRepository<TrainingCategory> _trainingCategory,
            IUserInfoes userInfoes,
            IReportService reportService,
            IRepository<SpecialBranchUnit> _repoSpecialBranchUnit,
            IRepository<TrainingInstitute> _trainingInstitute,
            IRepository<SpecialSkillType> _specialSkillType,
            IRepository<BCSBatch> _repoBCSBatch,
            IRepository<Rank> _repoRank,
            IRepository<Country> _repoCountry,
            IRepository<EmployeeReportInfo> _repoEmployeeReportInfo,
            IRepository<MedicalMainCategory> _repoMedicalMainCategory,
            IRepository<MedicalSubCategory> _repoMedicalSubCategory,
            IRepository<EmployeeMadicalInfo> _repoEmployeeMadicalInfo,
            IRepository<TraningLog> _traningLog
            )
        {
            _lang = new LangGenerate<DisciplinaryActionLn>(hostingEnvironment.ContentRootPath);
            _repoDisciplinaryAction = repoDisciplinaryAction;
            _repoEmployeeInfo = repoEmployeeInfo;
            _repoOffense = repoOffense;
            _repoNaturalPunishment = repoNaturalPunishment;
            _employeeService = employeeService;
            _userInfoes = userInfoes;
            _acrInformation = acrInformation;
            this._repoRank = _repoRank;
            this._repoBCSBatch = _repoBCSBatch;
            this._repoSpecialBranchUnit = _repoSpecialBranchUnit;
            this._repoEmployeeMadicalInfo = _repoEmployeeMadicalInfo;
            this._repoMedicalSubCategory = _repoMedicalSubCategory;
            this._repoMedicalMainCategory = _repoMedicalMainCategory;
            this._trainingInstitute = _trainingInstitute;
            this._repoEmployeeReportInfo = _repoEmployeeReportInfo;
            this._trainingCategory = _trainingCategory;
            this._repoCountry = _repoCountry;
            this._traningLog = _traningLog;
            this._specialSkillType = _specialSkillType;
            _reportService = reportService;
        }

        [Authorize(Roles = "Admin,Disciplinary Action Entry Operator,Super Admin")]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeId = id.ToString();
            var model = new DisciplinaryActionViewModel
            {
                fLang = _lang.PerseLang("Employee/DisciplinaryActionEN.json", "Employee/DisciplinaryActionBN.json", Request.Cookies["lang"]),
                // disciplinaryActions = _repoDisciplinaryAction.GetAll(),
                employeeInfo = _employeeService.GetBasicEmployeeInfoById(id),
                employeeInfos = _repoEmployeeInfo.GetAll(),
                offenses = _repoOffense.GetAll(),
                naturalPunishments = _repoNaturalPunishment.GetAll(),
                disciplinaryActions = await _employeeService.GetDisciplinaryActions(id),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] DisciplinaryActionViewModel model)
        {
            var Obj = new DisciplinaryAction
            {
                Id = model.DisciplinaryActionId,
                employeeId = model.employeeId,
                //OffenseId = model.OffenseId,
                //naturalPunishmentId = Convert.ToInt32(model.naturalPunishmentId),
                punishmentDate = model.punishmentDate,
                startingDate = model.startingDate,
                endDate = model.endDate,
                goNumberWithDate = model.goNumberWithDate,
                remarks = model.remarks,
                referenceNumber = model.referenceNumber,
                OffenseName = model.OffenseName,
                PunishmentName = model.PunishmentName,
                status = model.status,
                goFileURL=model.goFileURL
            };
            if (model.status == null)
            {
                Obj.status = "0";
            }
            if (Obj.employeeId > 0)
            {
                _repoDisciplinaryAction.Update(Obj);
            }
            else
            {
                _repoDisciplinaryAction.Insert(Obj);
            }
            return RedirectToAction("Index", "DisciplinaryAction", new { id = model.employeeId, Area = "Employee" });  
            //return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteDisciplinaryAction(int id)
        {
            bool response;

            try
            {
                _repoDisciplinaryAction.Delete(_repoDisciplinaryAction.Get(id));
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
        public IActionResult DeleteAcrAction(int id)
        {
            bool response;

            try
            {
                _acrInformation.Delete(_acrInformation.Get(id));
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
        public IActionResult DeleteTraining(int id)
        {
            bool response;
            try
            {
                _traningLog.Delete(_traningLog.Get(id));
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
        public IActionResult DeleteBPAinfo(int id)
        {
            bool response;
            try
            {
                _repoEmployeeReportInfo.Delete(_repoEmployeeReportInfo.Get(id));
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
        public IActionResult DeleteMedicalinfo(int id)
        {
            bool response;
            try
            {
                _repoEmployeeMadicalInfo.Delete(_repoEmployeeMadicalInfo.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin")]
        public async Task<IActionResult> ACRInfo(int id)
        {

            //var userInfo = await _userInfoes.GetUserInfoBeforeRegister(User.Identity.Name);
            //if (userInfo.isVerified != 1)
            //{
            //    return RedirectToAction("Not404Verified", "Home");
            //}
            ViewBag.employeeId = id.ToString();
            var acr = new DisciplinaryActionViewModel {
                acrInformations = await _employeeService.GetACRInfoByEmpId(id),
                employee=await _employeeService.GetEmployeeProfileInfoById(id)
            };

            return View(acr);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin,Traning1")]
        public async Task<IActionResult> Traninginfo(int id)
        {
            ViewBag.employeeId = id.ToString();
            var emp = await _employeeService.GetEmployeeProfileInfoById(id);
            var acr = new DisciplinaryActionViewModel {
                traningLogs = await _employeeService.GetTraningLogInfoByEmpId(id),
                employee=await _employeeService.GetEmployeeInfoSingleById(emp.employeeCode),
                trainingInstitutes = _trainingInstitute.GetAll().OrderBy(x => x.trainingInstituteName),
                trainingCategories = _trainingCategory.GetAll().OrderBy(x => x.trainingCategoryName),
                countries = _repoCountry.GetAll().OrderBy(x => x.countryName),
                specialSkillTypes = _specialSkillType.GetAll()
            };

            return View(acr);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin,Traning1")]
        public async Task<IActionResult> FATInformation(int id)
        {
            ViewBag.employeeId = id.ToString();
            var emp = await _employeeService.GetEmployeeProfileInfoById(id);
            var acr = new DisciplinaryActionViewModel
            {
                foreignTravels = await _employeeService.GetForeignTravelsForFATById(emp.Id),
                employee = await _employeeService.GetEmployeeInfoSingleById(emp.employeeCode),
                countries = _repoCountry.GetAll().OrderBy(x => x.countryName)
            };

            return View(acr);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin,Medical")]
        public async Task<IActionResult> Medicalinfo(int id)
        {
            ViewBag.employeeId = id.ToString();
            var emp = await _employeeService.GetEmployeeProfileInfoById(id);
            var acr = new DisciplinaryActionViewModel
            {
                employeeMadicalInfos = await _employeeService.GetEmployeeMadicalInfoByEmpIde(id),
                employee = await _employeeService.GetEmployeeInfoSingleById(emp.employeeCode),
                medicalMainCategories = _repoMedicalMainCategory.GetAll(),
                medicalSubCategories = _repoMedicalSubCategory.GetAll(),
            };

            return View(acr);
        }


        [HttpPost]
        public async Task<IActionResult> Medicalinfo([FromForm] DisciplinaryActionViewModel model)
        {
            var traningLog = new EmployeeMadicalInfo
            {
                employeeInfoId = model.employeeId,
                Id = model.DisciplinaryActionId,
                medicalSubCategoryId = model.year,
                date = DateTime.Now,
                remarks=model.remarks,
                status=1
            };
            if (model.DisciplinaryActionId > 0)
            {
                _repoEmployeeMadicalInfo.Update(traningLog);
            }
            else
            {
                _repoEmployeeMadicalInfo.Insert(traningLog);
            }

            return RedirectToAction("Medicalinfo", "DisciplinaryAction", new { id = model.employeeId, Area = "Employee" });
        }


        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin,SB")]
        public async Task<IActionResult> SBinfo(int id)
        {
            ViewBag.employeeId = id.ToString();
            var emp = await _employeeService.GetEmployeeProfileInfoById(id);
            var acr = new DisciplinaryActionViewModel
            {
                employeeReportInfos = await _employeeService.GetEmployeeReportInfoByEmpIdandType(id, "SB"),
                employee = await _employeeService.GetEmployeeInfoSingleById(emp.employeeCode),
            };

            return View(acr);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin,SB")]
        public async Task<IActionResult> SBinfoList(int id)
        {
            var acr = new DisciplinaryActionViewModel
            {
                employeeInfos = await _employeeService.GetEmployeeReportInfoByType("SB",0,0,0),
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                ranks = _repoRank.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll()
            };
            return View(acr);
        }




        [HttpPost]
        public async Task<IActionResult> SBinfo([FromForm] DisciplinaryActionViewModel model)
        {
            var traningLog = new EmployeeReportInfo
            {
                employeeInfoId = model.employeeId,
                Id = model.DisciplinaryActionId,
                type = model.type,
                year = model.year,
                date = model.startingDate,
                description = model.status,
                status=1
            };
            if (model.DisciplinaryActionId > 0)
            {
                _repoEmployeeReportInfo.Update(traningLog);
            }
            else
            {
                _repoEmployeeReportInfo.Insert(traningLog);
            }

            return RedirectToAction("SBinfo", "DisciplinaryAction", new { id = model.employeeId, Area = "Employee" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin,BPA")]
        public async Task<IActionResult> BPAinfo(int id)
        {
            ViewBag.employeeId = id.ToString();
            var emp = await _employeeService.GetEmployeeProfileInfoById(id);
            var acr = new DisciplinaryActionViewModel {
                employeeReportInfos = await _employeeService.GetEmployeeReportInfoByEmpIdandType(id,"BPA"),
                employee = await _employeeService.GetEmployeeInfoSingleById(emp.employeeCode),
            };

            return View(acr);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,ACR Entry Operator,Super Admin,BPA")]
        public async Task<IActionResult> BPAinfoList(int id)
        {
            var acr = new DisciplinaryActionViewModel
            {
                employeeInfos = await _employeeService.GetEmployeeReportInfoByType("BPA", 0, 0, 0),
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                ranks = _repoRank.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll()
            };
            return View(acr);
        }

        [HttpPost]
        public async Task<IActionResult> BPAinfo([FromForm] DisciplinaryActionViewModel model)
        {
            var traningLog = new EmployeeReportInfo
            {
                employeeInfoId = model.employeeId,
                Id = model.DisciplinaryActionId,
                type = model.type,
                year = model.year,
                date = model.startingDate,
                description = model.status,
                status=1
            };
            if (model.DisciplinaryActionId > 0)
            {
                _repoEmployeeReportInfo.Update(traningLog);
            }
            else
            {
                _repoEmployeeReportInfo.Insert(traningLog);
            }

            return RedirectToAction("BPAinfo", "DisciplinaryAction", new { id = model.employeeId, Area = "Employee" });
        }




        [HttpPost]
        public async Task<IActionResult> TrainingInfo([FromForm] EmployeeViewModel model)
        {
            var traningLog = new TraningLog
            {
                employeeId = model.empId,
                Id = model.empTrainingId,
                trainingType = model.trainingTypes,
                trainingTitle = model.trainingName,
                trainingCategoryId = model.trainingCategoryId,
                countryId = model.trainingCountryId,
                referenceNumber = model.referenceNum,
                remarks = model.trDuration,
                trainingInstituteId = model.trainingInstituteId,
                fromDate = model.fromDate,
                toDate = model.toDate,
                sponsoringAgency = model.fortrainingInstitute,
                specialSkillTypeId = model.specialSkillTypeId,
                status=1
            };

            await _employeeService.SaveTrainingInformation(traningLog);

            return RedirectToAction("Traninginfo", "DisciplinaryAction", new { id = model.empId, Area = "Employee" });
        }



        [HttpPost]
        public IActionResult ACRInfo([FromForm] DisciplinaryActionViewModel model)
        {
            var data = new ACRInformation
            {
                Id = model.acrId,
                employeeId = model.employeeId,
                year = model.year,
                marks = model.marks,

            };
            if (data.Id > 0)
            {
                _acrInformation.Update(data);
            }
            else
            {
                _acrInformation.Insert(data);
            }

            return RedirectToAction("Index", "DisciplinaryAction", new { id = model.employeeId, Area = "Employee" });
        }

        public async Task<IActionResult> GetDisciplinaryActionsByEmpId(int id)
        {
            var result = await _employeeService.GetDisciplinaryActions(id);
            return Json(result);
        }

        public async Task<IActionResult> GetEmployeeReportInfoByEmpIdandType(int id,string type)
        {
            var result = await _employeeService.GetEmployeeReportInfoByEmpIdandType(id, type);
            return Json(result);
        }

        public async Task<IActionResult> GetEmployeeReportInfoByType(string type, int branchId, int rankId, int bcs)
        {
            var result = await _employeeService.GetEmployeeReportInfoByType(type, branchId, rankId, bcs);
            return Json(result);
        }

        public async Task<IActionResult> GetMedicalSubCatbyMainId(int id)
        {
            var result = await _employeeService.GetMedicalSubCatbyMainId(id);
            return Json(result);
        }

        public IActionResult GetMedicalSubCatbyId(int id)
        {
            var result = _repoMedicalSubCategory.Get(id);
            return Json(result);
        }

        #region Admin Super Admin Report
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult ReportApprove()
        {
            return View();
        }

        public async Task<IActionResult> PendingDiciplinaryList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits=_repoSpecialBranchUnit.GetAll(),
                bCSBatches=_repoBCSBatch.GetAll(),
                ranks=_repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> ApprovedDiciplinaryList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> PendingBPAList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> ApprovedBPAList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> PendingTrainingList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> ApprovedTrainingList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> PendingSBList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> ApprovedSBList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> PendingMedicalList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> ApprovedMedicalList()
        {
            var model = new DisciplinaryActionViewModel
            {
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                bCSBatches = _repoBCSBatch.GetAll(),
                ranks = _repoRank.GetAll()
            };
            return View(model);
        }

        #region API
        public async Task<IActionResult> GetPendingDiciplinaryList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWisePendingDisciplinaryActionList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> GetApprovedDiciplinaryList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWiseApprovedDisciplinaryActionList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> ApprovePendingDiciplinaryList(int id)
        {
            var data = await _reportService.ApproveDisciplinary(id);
            return Json(data);
        }

        public async Task<IActionResult> GetPendingTrainingList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWisePendingTrainingList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> GetApprovedTrainingList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWiseApprovedTrainingList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> ApprovePendingTrainingList(int id)
        {
            var data = await _reportService.ApproveTraningLog(id);
            return Json(data);
        }

        public async Task<IActionResult> GetPendingMedicalList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWisePendingMedicalList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> GetApprovedMedicalList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWiseApprovedMedicalList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> ApprovePendingMedicalList(int id)
        {
            var data = await _reportService.ApproveMedical(id);
            return Json(data);
        }

        public async Task<IActionResult> GetPendingSBList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWisePendingSBList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> GetApprovedSBList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWiseApprovedSBList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> ApprovePendingSBList(int id)
        {
            var data = await _reportService.ApproveSB(id);
            return Json(data);
        }

        public async Task<IActionResult> GetPendingBPAList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWisePendingBPAList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> GetApprovedBPAList(int unitId, int rankId, int bcsId)
        {
            var data = await _reportService.GetUnitRankBatchWiseApprovedBPAList(unitId, bcsId, rankId);
            return Json(data);
        }

        public async Task<IActionResult> ApprovePendingBPAList(int id)
        {
            var data = await _reportService.ApproveBPA(id);
            return Json(data);
        }
        #endregion
        #endregion
    }
}