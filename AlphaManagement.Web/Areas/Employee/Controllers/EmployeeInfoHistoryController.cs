using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EmployeeInfoHistoryController : Controller
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
        private readonly IUserInfoes userInfoes;
        private readonly IRepository<LevelofEducation> _lavelofEducation;
        private readonly IRepository<Organization> _organizationService;
        private readonly IRepository<Result> _result;
        private readonly IRepository<Degree> _degree;
        private readonly IRepository<AddressInformation> _addressInformation;

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
        private readonly IRepository<BCSBatch> _bCSBatch;
        private readonly IRepository<ForeignTravel> _repoForeignTravel;
        private readonly IRepository<Country> _repoCountry;
        private readonly IRepository<UnionWard> _repoUnionWord;
        private readonly IRepository<Thana> _repoThana;
        private readonly IRepository<Disease> _disease;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IAccessLogHistoryService accessLogHistoryService;

        public IRepository<SpouseRelation> _spouseRelation { get; }

        private readonly IRepository<Spouse> _spouse;
        private readonly IRepository<EducationalQualification> _educationalQualification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public EmployeeInfoHistoryController(
            IHostingEnvironment hostingEnvironment,
            IRepository<Designation> repoDesignation,
            IRepository<Rank> repoRank,
            IRepository<Religion> repoReligion,
            IRepository<Section> repoSection,
            IRepository<SpecialBranchUnit> repoBranch,
            IRepository<District> district,
            IRepository<BCSBatch> repoBCSBatch,
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
            IRepository<Disease> disease,
            IConfiguration _configuration,


            IRepository<EducationalQualification> educationalQualification,
            IRepository<Banks> banks,
            IRepository<BCSBatch> bCSBatch,
            IRepository<AddressInformation> addressInformation,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,

            IUserInfoes userInfoes,
            IDegreeService degreeService,
            IAddressServices addressService,
            IEmployeeService employeeService,
            IPhotographService photographService,
            IAccessLogHistoryService accessLogHistoryService,
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
            _repoRank = repoRank;
            _repoReligion = repoReligion;
            _repoSection = repoSection;
            _repoBranch = repoBranch;
            _district = district;
            _repoBCSBatch = repoBCSBatch;
            this.userInfoes = userInfoes;
            this.accessLogHistoryService = accessLogHistoryService;
            _lavelofEducation = lavelofEducation;
            _organizationService = organizationService;
            _result = result;
            _degree = degree;
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
            _bCSBatch = bCSBatch;
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
            _repoUnionWord = repoUnionWord;
        }


        public async Task<IActionResult> CustomCropSignuture(string id)
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
                signature = await _employeeService.GetEmployeeSignatureByEmpId(empInfo.Id),
                employeeInfo = employeeInfo
            };
            return View(model);
        }
        public async Task<IActionResult> CustomCrop(string id)
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
                photograph = await _employeeService.GetEmployeePhotographByEmpId(empInfo.Id),
                employeeInfo = employeeInfo
            };
            //return PartialView("CustomCrop",model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CustomCropSignuture(string filename, IFormFile blob, int employeeId, int signatureID, string localPath)
        {
            try
            {
                var Message = "";
                using (var image = Image.Load(blob.OpenReadStream()))
                {
                    localPath = "EmpImages";
                    string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
                    Message = Path.Combine(localPath, DateTime.Now.Ticks + systemFileExtenstion);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Message);
                    image.Mutate(x => x.Resize(300, 80));
                    image.Save(path);
                    var signId = 0;
                    if (signatureID > 0)
                    {
                        signId = signatureID;
                    }

                    Photograph data = new Photograph
                    {
                        Id = signId,
                        employeeId = employeeId,
                        url = Message,
                        type = "signature"
                    };
                    await photographService.SavePhotograph(data);
                }

                return Json(Message);
            }
            catch (Exception)
            {
                return Json("Failed");
            }
        }

        public async Task<IActionResult> AdminCheckedEmployeeList()
        {
            var data = new EmployeeInfoViewModel
            {
                employeeInfoList = await _employeeService.GetCheckedEmployeeInfoList()
                //  employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0),

            };
            return View(data);
        }

        public async Task<IActionResult> AdminCheckedUpdatedEmployeeList()
        {
            var data = new EmployeeInfoViewModel
            {
                employeeInfoList = await _employeeService.GetCheckedUpdatedEmployeeInfoList()
                //  employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0),

            };
            return View(data);
        }


        public async Task<IActionResult> AdminReturnUpdatedEmployeeList()
        {
            var data = new EmployeeInfoViewModel
            {
                employeeInfoList = await _employeeService.GetCheckedUpdatedEmployeeInfoListUpdate()
                //  employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, 0, 0, 0),

            };
            return View(data);
        }

        public async Task<IActionResult> AdminCheckedUpdatedEmployeeListForAdmin(int unitId, int rankId, int batchId)
        {
            var data = new EmployeeInfoViewModel
            {
                employeeId = unitId,
                ranks = _repoRank.GetAll(),
                units = _specialBranchUnit.GetAll(),
                bCSBatch = _bCSBatch.GetAll(),
                employeeInfoList = await _employeeService.LoadCheckedUpdatedEmployeeInfoList(rankId, unitId, batchId)
            };
            return View(data);
        }


        public async Task<IActionResult> AdminCheckedUpdatedEmployeeListForAdminJson(int unitId, int rankId, int batchId)
        {
            var data = new EmployeeInfoViewModel
            {
                employeeId = unitId,
                ranks = _repoRank.GetAll(),
                units = _specialBranchUnit.GetAll(),
                bCSBatch = _bCSBatch.GetAll(),
                //employeeInfoList = await _employeeService.LoadCheckedUpdatedEmployeeInfoList(rankId, unitId, batchId),
                employeeInfos_SPs = await _employeeService.GetCheckedEmployeeListBySp(rankId, unitId, batchId)
            };
            return Json(data);
        }

        public async Task<IActionResult> CheckedEmployeeListForAdmin(int unitId, int rankId, int batchId)
        {
            var data = new EmployeeInfoViewModel
            {
                employeeInfoList = await _employeeService.LoadCheckedUpdatedEmployeeInfoList(rankId, unitId, batchId)
            };
            return PartialView("_CheckedListForAdmin",data);
        }

        public async Task<IActionResult> LoadCheckedOfficerList(int unitId, int rankId, int batchId)
        {
            var data = new EmployeeInfoViewModel
            {
                employeeInfoList = await _employeeService.LoadCheckedUpdatedEmployeeInfoList(rankId, unitId, batchId)
            };
            return PartialView("_CheckedEmployeeList",data);
        }

        [HttpPost]
        public async Task<IActionResult> CustomCrop(string filename, IFormFile blob, int employeeId, int photoId, string localPath)
        {
            try
            {
                var Message = "";
                using (var image = Image.Load(blob.OpenReadStream()))
                {
                    localPath = "EmpImages";
                    string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
                    Message = Path.Combine(localPath, DateTime.Now.Ticks + systemFileExtenstion);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Message);
                    image.Mutate(x => x.Resize(300, 300));
                    image.Save(path);
                    var pictureId = 0;
                    if (photoId > 0)
                    {
                        pictureId = photoId;
                    }
                    Photograph data = new Photograph
                    {
                        Id = pictureId,
                        employeeId = employeeId,
                        url = Message,
                        type = "profile"
                    };
                    await photographService.SavePhotograph(data);
                }

                return Json(Message);
            }
            catch (Exception)
            {
                return Json("Failed");
            }
        }

        public async Task<IActionResult> Index(string empIdentityUserSessionToken)
        {
            try
            {


                var id = Convert.ToInt32(empIdentityUserSessionToken);
                var _empInfo = _employeeService.GetBasicEmployeeInfoById(id);
                if (_empInfo?.employeeCode != User.Identity.Name || _empInfo == null)
                {
                    var userAgent = Request.Headers["User-Agent"].ToString();
                    var mechineName = Environment.MachineName;

                    var remote = HttpContext.Connection.RemoteIpAddress;
                    var local = HttpContext.Connection.LocalIpAddress;
                    string userip = remote.ToString();
                    string useripLocal = local.ToString();

                    UnauthorizeUserLog userLog = new UnauthorizeUserLog
                    {
                        userId = User.Identity.Name,
                        logTime = DateTime.Now,
                        status = 1,
                        ipAddress = userip,
                        pcName = mechineName,
                        browserName = userAgent,
                        temptationString = empIdentityUserSessionToken,
                    };
                    int ipId = await accessLogHistoryService.SaveUnauthorizeUserLog(userLog);

                    return RedirectToAction("Not404Found", "Home");
                }

                //if (id == string.Empty || id == null)
                //{
                //    id = User.Identity.Name;
                //}

                //if(id!= User.Identity.Name)
                //{
                //    return RedirectToAction("Not404Found", "Home");
                //}

                //ApplicationUser applicationUser = await _userManager.FindByNameAsync(id);
                var userInfo = await userInfoes.GetUserInfoByUser(_empInfo?.employeeCode);
                if (userInfo == null)
                {
                    return RedirectToAction("Not404Found", "Home");
                }
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
                    educationalQualifications = _educationalQualification.GetAll().Where(x => x.employeeId == empInfo.Id).ToList(),
                    addressInformations = _addressInformation.GetAll().Where(x => x.employeeInfoId == empInfo.Id).ToList(),
                    presentAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Present Address"),
                    permanentAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Permanent Address"),
                    spouseAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Spouse Address"),
                    maternalFamilyAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Maternal Family Address"),
                    ApplicationUserId = userInfo.Id,
                    branch = _repoBranch.GetAll(),
                    medicalInfo = await _employeeService.GetMedicalInfoByEmpId(empInfo.Id),
                    section = _repoSection.GetAll(),
                    designations = _repoDesignation.GetAll(),
                    rank = _repoRank.GetAll(),
                    diseases = _disease.GetAll(),
                    religions = _repoReligion.GetAll().OrderBy(x => x.name),
                    districts = _district.GetAll().OrderBy(x => x.districtName),
                    thanas = _repoThana.GetAll().OrderBy(x => x.thanaName),
                    unionwards = _repoUnionWord.GetAll().OrderBy(x => x.unionName),
                    bCSBatches = _repoBCSBatch.GetAll(),
                    employeeInfo = employeeInfo,
                    organizations = _organizationService.GetAll().OrderBy(x => x.organizationName),
                    levelofEducations = _lavelofEducation.GetAll().OrderBy(x => x.levelofeducationName),
                    results = _result.GetAll().OrderBy(x => x.resultName),
                    Divisions = _division.GetAll().OrderBy(x => x.divisionName),
                    sections = _section.GetAll().OrderBy(x => x.Name),
                    specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                    diseasesList = await _employeeService.GetDiseases(),
                    Vaccines = await _employeeService.GetVaccines(),
                    applicationUser = userInfo,
                    relations = _relation.GetAll().OrderBy(x => x.relationName),
                    offenses = _offense.GetAll().OrderBy(x => x.offense),
                    spouseRelations = _spouseRelation.GetAll().OrderBy(x => x.relationName),
                    trainingInstitutes = _trainingInstitute.GetAll().OrderBy(x => x.trainingInstituteName),
                    trainingCategories = _trainingCategory.GetAll().OrderBy(x => x.trainingCategoryName),
                    naturalPunishments = _naturalPunishment.GetAll(),
                    spouses = _spouse.GetAll(),
                    banks = _banks.GetAll().OrderBy(x => x.bankName),
                    photograph = await _employeeService.GetEmployeePhotographByEmpId(empInfo.Id),
                    signature = await _employeeService.GetEmployeeSignatureByEmpId(empInfo.Id),
                    fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                    fLangaward = _award.PerseLang("Employee/AwardEntryEN.json", "Employee/AwardEntryBN.json", Request.Cookies["lang"]),
                    fLangDiciplinary = _disiplinary.PerseLang("Employee/DisciplinaryActionEN.json", "Employee/DisciplinaryActionBN.json", Request.Cookies["lang"]),
                    flangeducation = _education.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                    fLangtraining = _training.PerseLang("Employee/TraningLogEN.json", "Employee/TraningLogBN.json", Request.Cookies["lang"]),

                    spousesInfo = await _employeeService.GetSpouseInfoByEmpId(empInfo.Id),

                    assignments = await _employeeService.GetAssignmentInfoByEmpId(empInfo.Id),
                    employeeReturnReasons = await _employeeService.GetEmployeeReturnReasonByEmpId(empInfo.Id),

                    educationalQualification = await _employeeService.GetEducationalQualificationInfoByEmpId(empInfo.Id),
                    awardEntries = await _employeeService.GetAwardInfoByEmpId(empInfo.Id),
                    awardEntryList = _repoAwardEntry.GetAll().OrderBy(x => x.awardName),
                    awardList = _repoAward.GetAll().OrderBy(x => x.awardName),
                    promotionLogs = await _employeeService.GetPromotionInfoByEmpId(empInfo.Id),
                    traningLogs = await _employeeService.GetTraningLogInfoByEmpId(empInfo.Id),
                    disciplinaryActions = await _employeeService.GetDisciplinaryByEmpId(empInfo.Id),
                    addressInformation = await _employeeService.GetAddressInformationByEmpId(empInfo.Id),
                    foreignTravels = await _employeeService.GetForeignTravelsById(empInfo.Id),
                    countries = _repoCountry.GetAll().OrderBy(x => x.countryName),
                    ACRInfo = await _employeeService.GetEmployeeACRInfo(empInfo.Id)

                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> AdminEmployeeIndex(string empIdentityUserSessionToken)
        {
            try
            {
                var id = Convert.ToInt32(empIdentityUserSessionToken);
                var _empInfo = _employeeService.GetBasicEmployeeInfoById(id);
                var userInfo = await userInfoes.GetUserInfoByUser(_empInfo?.employeeCode);

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
                    educationalQualifications = _educationalQualification.GetAll().Where(x => x.employeeId == empInfo.Id).ToList(),
                    addressInformations = _addressInformation.GetAll().Where(x => x.employeeInfoId == empInfo.Id).ToList(),
                    presentAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Present Address"),
                    permanentAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Permanent Address"),
                    spouseAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Spouse Address"),
                    maternalFamilyAdd = await _employeeService.GetAddressByEmpIdType(empInfo.Id, "Maternal Family Address"),
                    ApplicationUserId = userInfo.Id,
                    branch = _repoBranch.GetAll(),
                    medicalInfo = await _employeeService.GetMedicalInfoByEmpId(empInfo.Id),
                    section = _repoSection.GetAll(),
                    designations = _repoDesignation.GetAll(),
                    rank = _repoRank.GetAll(),
                    diseases = _disease.GetAll(),
                    religions = _repoReligion.GetAll().OrderBy(x => x.name),
                    districts = _district.GetAll().OrderBy(x => x.districtName),
                    thanas = _repoThana.GetAll().OrderBy(x => x.thanaName),
                    unionwards = _repoUnionWord.GetAll().OrderBy(x => x.unionName),
                    bCSBatches = _repoBCSBatch.GetAll(),
                    employeeInfo = employeeInfo,
                    organizations = _organizationService.GetAll().OrderBy(x => x.organizationName),
                    levelofEducations = _lavelofEducation.GetAll().OrderBy(x => x.levelofeducationName),
                    results = _result.GetAll().OrderBy(x => x.resultName),
                    Divisions = _division.GetAll().OrderBy(x => x.divisionName),
                    sections = _section.GetAll().OrderBy(x => x.Name),
                    specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                    diseasesList = await _employeeService.GetDiseases(),
                    Vaccines = await _employeeService.GetVaccines(),
                    applicationUser = userInfo,
                    relations = _relation.GetAll().OrderBy(x => x.relationName),
                    offenses = _offense.GetAll().OrderBy(x => x.offense),
                    spouseRelations = _spouseRelation.GetAll().OrderBy(x => x.relationName),
                    trainingInstitutes = _trainingInstitute.GetAll().OrderBy(x => x.trainingInstituteName),
                    trainingCategories = _trainingCategory.GetAll().OrderBy(x => x.trainingCategoryName),
                    naturalPunishments = _naturalPunishment.GetAll(),
                    spouses = _spouse.GetAll(),
                    banks = _banks.GetAll().OrderBy(x => x.bankName),
                    photograph = await _employeeService.GetEmployeePhotographByEmpId(empInfo.Id),
                    signature = await _employeeService.GetEmployeeSignatureByEmpId(empInfo.Id),
                    fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                    fLangaward = _award.PerseLang("Employee/AwardEntryEN.json", "Employee/AwardEntryBN.json", Request.Cookies["lang"]),
                    fLangDiciplinary = _disiplinary.PerseLang("Employee/DisciplinaryActionEN.json", "Employee/DisciplinaryActionBN.json", Request.Cookies["lang"]),
                    flangeducation = _education.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                    fLangtraining = _training.PerseLang("Employee/TraningLogEN.json", "Employee/TraningLogBN.json", Request.Cookies["lang"]),

                    spousesInfo = await _employeeService.GetSpouseInfoByEmpId(empInfo.Id),

                    assignments = await _employeeService.GetAssignmentInfoByEmpId(empInfo.Id),
                    employeeReturnReasons = await _employeeService.GetEmployeeReturnReasonByEmpId(empInfo.Id),

                    educationalQualification = await _employeeService.GetEducationalQualificationInfoByEmpId(empInfo.Id),
                    awardEntries = await _employeeService.GetAwardInfoByEmpId(empInfo.Id),
                    awardEntryList = _repoAwardEntry.GetAll().OrderBy(x => x.awardName),
                    awardList = _repoAward.GetAll().OrderBy(x => x.awardName),
                    promotionLogs = await _employeeService.GetPromotionInfoByEmpId(empInfo.Id),
                    traningLogs = await _employeeService.GetTraningLogInfoByEmpId(empInfo.Id),
                    disciplinaryActions = await _employeeService.GetDisciplinaryByEmpId(empInfo.Id),
                    addressInformation = await _employeeService.GetAddressInformationByEmpId(empInfo.Id),
                    foreignTravels = await _employeeService.GetForeignTravelsById(empInfo.Id),
                    countries = _repoCountry.GetAll().OrderBy(x => x.countryName),
                    ACRInfo = await _employeeService.GetEmployeeACRInfo(empInfo.Id)

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
        public async Task<IActionResult> Index([FromForm] EmployeeViewModel model)
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
                var employee = new EmployeeInfo
                {
                    Id = model.empId,
                    ApplicationUserId = applicationUser.Id,
                    nameBangla = model.nameBangla,
                    nameEnglish = model.nameEnglish,
                    employeeCode = model.bpNo,
                    rankId = model.rankId,
                    designation = model.designation,
                    designationsId = model.designationsId,
                    // currentWorkStation = model.currentWorkStation,
                    joiningDatePresentWorkstation = model.joiningDatePresentWorkstation,
                    fatherNameEnglish = model.fatherNameEnglish,
                    motherNameEnglish = model.motherNameEnglish,
                    // spouseName = model.spouseName,
                    gender = model.gender,
                    sectionName = model.sectionName,
                    sectionId = model.sectionId,
                    nationalID = model.nationalID,
                    homeDistrict = model.homeDistrict,
                    // spouseHomeDistrict = model.spouseHomeDistrict,
                    // permanentAddress  = model.permanentAddress,
                    //presentAddress = model.presentAddress,
                    dateOfBirth = model.dateOfBirth,
                    joiningDateGovtService = model.joiningDateGovt,
                    bCSBatchId = model.bCSBatchId,
                    bcsPosition = model.bcsPosition,
                    // retiredDate = model.retiredDate,
                    servicePeriod = model.servicePeriod,
                    joiningDesignation = model.joiningDesignation,
                    bloodGroup = model.bloodGroup,
                    height = model.height,
                    weight = model.weight,
                    identificationSign = model.identificationSign,
                    religionId = model.religionId,
                    maritalStatus = model.maritalStatus,
                    tribal = model.tribal,
                    // tribal = Convert.ToInt32(tribals),
                    mobileNumberOffice = model.mobileNumberOffice,
                    mobileNumberPersonal = model.mobileNumberPersonal,
                    emailAddress = model.emailOffice,
                    emailAddressPersonal = model.emailPersonal,
                    promotionDate = model.lastpromotionDate,
                    pabx = model.status,
                    passportNo = model.passportNo,
                    telephoneOffice = model.telephoneOffice,
                    banksId = model.salaryBankId,
                    salaryAccountNo = model.salaryAccountNo,
                    otherBanksId = model.otherBankId,
                    otherBankAccountNo = model.otherBankAccountNo,
                    rationId = model.rationId,
                    drivingLicense = model.drivingLicense,
                    // remarks = model.remarks,
                    linkdInId = model.linkdInId,
                    facebookId = model.facebookId,
                    skypeId = model.skypeId,
                    skill = model.skill,
                    extraActivity = model.extraActivity,
                    branchId = model.specialBranchUnitId,
                    LPRDate = model.lprDate,
                    extraSkill = model.extraSkill,
                    extraActivitys = model.extraActivitys,
                    departmentalPromotionYear = model.departmentalPromotionYear,
                    isApproved = 2,
                };
                int id = await _employeeService.SaveEmployeeInformation(employee);
                model.empId = id;

                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> AdminUpdate([FromForm] EmployeeViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var employee = await _employeeService.GetEmployeeInfoSingleById(model.bpNo);
                var EmployeeInfoHistory = new EmployeeInfoHistory
                {
                    Id = 0,
                    nameBangla = employee.nameBangla,
                    nameEnglish = employee.nameEnglish,
                    employeeCode = employee.employeeCode,
                    rankId = employee.rankId,
                    designation = employee.designation,
                    designationsId = employee.designationsId,
                    joiningDatePresentWorkstation = employee.joiningDatePresentWorkstation,
                    fatherNameEnglish = employee.fatherNameEnglish,
                    motherNameEnglish = employee.motherNameEnglish,
                    gender = employee.gender,
                    sectionName = employee.sectionName,
                    sectionId = employee.sectionId,
                    nationalID = employee.nationalID,
                    homeDistrict = employee.homeDistrict,
                    dateOfBirth = employee.dateOfBirth,
                    joiningDateGovtService = employee.joiningDateGovtService,
                    bCSBatchId = employee.bCSBatchId,
                    bcsPosition = employee.bcsPosition,
                    servicePeriod = employee.servicePeriod,
                    joiningDesignation = employee.joiningDesignation,
                    bloodGroup = employee.bloodGroup,
                    height = employee.height,
                    weight = employee.weight,
                    identificationSign = employee.identificationSign,
                    religionId = employee.religionId,
                    maritalStatus = employee.maritalStatus,
                    tribal = employee.tribal,
                    mobileNumberOffice = employee.mobileNumberOffice,
                    mobileNumberPersonal = employee.mobileNumberPersonal,
                    emailAddress = employee.emailAddress,
                    emailAddressPersonal = employee.emailAddressPersonal,
                    promotionDate = employee.promotionDate,
                    pabx = employee.pabx,
                    passportNo = employee.passportNo,
                    telephoneOffice = employee.telephoneOffice,
                    banksId = employee.banksId,
                    salaryAccountNo = employee.salaryAccountNo,
                    otherBanksId = employee.otherBanksId,
                    otherBankAccountNo = employee.otherBankAccountNo,
                    rationId = employee.rationId,
                    drivingLicense = employee.drivingLicense,
                    linkdInId = employee.linkdInId,
                    facebookId = employee.facebookId,
                    skypeId = employee.skypeId,
                    skill = employee.skill,
                    extraActivity = employee.extraActivity,
                    branchId = employee.branchId,
                    LPRDate = employee.LPRDate,
                    extraSkill = employee.extraSkill,
                    extraActivitys = employee.extraActivitys,
                    departmentalPromotionYear = employee.departmentalPromotionYear,
                    entryType = 1,
                    UpdateUserId = user.Id,
                    pHQTRTypeId = employee.pHQTRTypeId,
                    countryId = employee.countryId,
                    attachmentBranchId = employee.attachmentBranchId,
                    //isApproved = 2,
                };

                await _employeeService.SaveEmployeeEmployeeInfoHistory(EmployeeInfoHistory);

                employee.nameBangla = model.nameBangla;
                employee.nameEnglish = model.nameEnglish;
                employee.employeeCode = model.bpNo;
                employee.rankId = model.rankId;
                employee.designation = model.designation;
                employee.designationsId = model.designationsId;
                employee.joiningDatePresentWorkstation = model.joiningDatePresentWorkstation;
                employee.fatherNameEnglish = model.fatherNameEnglish;
                employee.motherNameEnglish = model.motherNameEnglish;
                employee.gender = model.gender;
                employee.sectionName = model.sectionName;
                employee.sectionId = model.sectionId;
                employee.nationalID = model.nationalID;
                employee.homeDistrict = model.homeDistrict;
                employee.dateOfBirth = model.dateOfBirth;
                employee.joiningDateGovtService = model.joiningDateGovt;
                employee.bCSBatchId = model.bCSBatchId;
                employee.bcsPosition = model.bcsPosition;
                employee.servicePeriod = model.servicePeriod;
                employee.joiningDesignation = model.joiningDesignation;
                employee.bloodGroup = model.bloodGroup;
                employee.height = model.height;
                employee.weight = model.weight;
                employee.identificationSign = model.identificationSign;
                employee.religionId = model.religionId;
                employee.maritalStatus = model.maritalStatus;
                employee.tribal = model.tribal;
                employee.mobileNumberOffice = model.mobileNumberOffice;
                employee.mobileNumberPersonal = model.mobileNumberPersonal;
                employee.emailAddress = model.emailOffice;
                employee.emailAddressPersonal = model.emailPersonal;
                employee.promotionDate = model.lastpromotionDate;
                employee.pabx = model.status;
                employee.passportNo = model.passportNo;
                employee.telephoneOffice = model.telephoneOffice;
                employee.banksId = model.salaryBankId;
                employee.salaryAccountNo = model.salaryAccountNo;
                employee.otherBanksId = model.otherBankId;
                employee.otherBankAccountNo = model.otherBankAccountNo;
                employee.rationId = model.rationId;
                employee.drivingLicense = model.drivingLicense;
                employee.linkdInId = model.linkdInId;
                employee.facebookId = model.facebookId;
                employee.skypeId = model.skypeId;
                employee.skill = model.skill;
                employee.extraActivity = model.extraActivity;
                employee.branchId = model.specialBranchUnitId;
                employee.LPRDate = model.lprDate;
                employee.extraSkill = model.extraSkill;
                employee.extraActivitys = model.extraActivitys;
                employee.departmentalPromotionYear = model.departmentalPromotionYear;
                employee.isAdminEntry = 1;
                employee.countryId = model.PhqCountry;
                employee.pHQTRTypeId = model.PhqTrType;
                employee.attachmentBranchId = model.attachmentBranchId;
                //employee.isApproved = 2;
                int id = await _employeeService.SaveEmployeeInformation(employee);
                model.empId = id;
                await _employeeService.SaveEmployeeTransectionHistoryLog(employee.Id, 11, user.Id, "Portfolio", "Parsonal Information");
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            try
            {
                List<EmpViewModel> empDetail = new List<EmpViewModel>();
                var EmpList = await _employeeService.GetEmployeeListForAlphaPersonalProfile();
                var model = new EmployeeInfoViewModel
                {
                    employeeListVMs = EmpList,
                    specialBranchUnits = _specialBranchUnit.GetAll(),
                    rank = _repoRank.GetAll(),
                    bCSBatch = _repoBCSBatch.GetAll(),
                    districts = _district.GetAll(),
                    Divisions = _division.GetAll(),
                    thanas = _repoThana.GetAll(),
                    religions = _repoReligion.GetAll(),
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


        public async Task<IActionResult> FamilyInfo([FromForm] EmployeeViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var prev = await _employeeService.GeSpouseById(model.spouseId);
                if (prev != null)
                {
                    if (prev.isAdminEntry == null)
                    {
                        var spouseHistory = new SpouseHistory
                        {
                            employeeId = prev.employeeId,
                            spouseRelationId = prev.spouseRelationId,
                            spouseName = prev.spouseName,
                            fatherName = prev.fatherName,
                            motherName = prev.motherName,
                            districtId = prev.districtId,
                            maritalStatus = prev.maritalStatus,
                            bloodGroup = prev.bloodGroup,
                            nid = prev.birthCertificate,
                            email = prev.email,
                            dateOfBirth = prev.dateOfBirth,
                            occupation = prev.occupation,
                            remarks = prev.remarks,
                            contact = prev.contact,
                            entryType = 1,
                            UpdateUserId = user.Id,
                            spouseId = prev.Id
                        };

                        await _employeeService.SaveSpouseHistory(spouseHistory);
                    }
                }

                var spouse = new Spouse
                {
                    employeeId = model.empId,
                    Id = model.spouseId,
                    spouseRelationId = model.spouseRelationId,
                    spouseName = model.Name,
                    fatherName = model.fatherName,
                    motherName = model.motherName,
                    districtId = model.spousedistrictId,
                    maritalStatus = model.spouseMaritalStatus,
                    bloodGroup = model.spouseBloodGroup,
                    nid = model.birthCertificate,
                    email = model.email,
                    dateOfBirth = model.spouseDateOfBirth,
                    occupation = model.occupation,
                    remarks = model.description,
                    contact = model.mobile,
                    isAdminEntry = 1
                };

                await _employeeService.SaveFamilyInformation(spouse);
                await _employeeService.SaveEmployeeTransectionHistoryLog(model.empId, 11, user.Id, "Portfolio", "Family Information");
                return Json(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentsInfo([FromForm] AssignmentViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var prev = await _employeeService.GetAssignmentInfoById(model.empJobHistoryId);
                if (prev != null)
                {
                    if (prev.isAdminEntry == null)
                    {
                        var AssignmentHistory = new AssignmentHistory
                        {
                            employeeId = prev.employeeId,
                            sectionId = prev.sectionId,
                            specialBranchUnitId = prev.specialBranchUnitId,
                            rankId = prev.rankId,
                            sectionName = prev.sectionName,
                            Remarks = prev.Remarks,
                            StartDate = prev.StartDate,
                            EndDate = prev.EndDate,
                            servicePeriod = prev.servicePeriod,
                            isDelete = prev.isDelete,
                            entryType = 1,
                            statusId = 3,
                            assignmentId = prev.Id,
                            UpdateUserId = user.Id
                        };

                        await _employeeService.SaveAssignmentAssignmentHistory(AssignmentHistory);
                    }
                }
                var assignment = new Assignment
                {
                    employeeId = model.empId,
                    Id = model.empJobHistoryId,
                    sectionId = model.secId,
                    specialBranchUnitId = model.jobSpecialBranchUnitId,
                    rankId = model.ranksId,
                    sectionName = model.jobsectionName,
                    Remarks = model.reasonofTransfer,
                    StartDate = model.AssignjoiningDate,
                    EndDate = model.AssignresignDate,
                    servicePeriod = model.servicePeriod,
                    isDelete = model.jobRunningCheckBox,
                    statusId = 3,
                    isAdminEntry = 1
                };

                await _employeeService.SaveAssignmentInformation(assignment);
                await _employeeService.SaveEmployeeTransectionHistoryLog(model.empId, 11, user.Id, "Portfolio", "Job History");
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> PromotionInfo([FromForm] EmployeeViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var prev = await _employeeService.GetPromotionInfoById(model.promotionId);
                if (prev != null)
                {
                    if (prev.isAdminEntry == null)
                    {
                        var promotionHistory = new PromotionLogHistory
                        {
                            employeeId = prev.employeeId,
                            rankId = prev.rankId,
                            goDate = prev.goDate,
                            date = prev.date,
                            remark = prev.remark,
                            goNumber = prev.goNumber,
                            promotionLogId = prev.Id,
                            entryType = 1,
                            UpdateUserId = user.Id
                        };

                        await _employeeService.SavePromotionLogHistory(promotionHistory);
                    }
                }

                var promotion = new PromotionLog
                {
                    employeeId = model.empId,
                    Id = model.promotionId,
                    rankId = model.promotionrankId,
                    goDate = model.PromotionjoiningDate,
                    date = Convert.ToDateTime(model.PromotionDate?.ToString("yyyy-MM-dd")),
                    remark = model.remarks,
                    goNumber = model.referenceNum,
                    isAdminEntry = 1
                };

                await _employeeService.SavePromotionInfo(promotion);
                await _employeeService.SaveEmployeeTransectionHistoryLog(model.empId, 11, user.Id, "Portfolio", "Promotion Information");
                return Json(model);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveForeignTravelData([FromForm] ForeignTravelViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var prev = await _employeeService.GetForeignTravelById((int)model.ForeignTravelId);
            if (prev != null)
            {
                if (prev.isAdminEntry == null)
                {
                    var promotionHistory = new ForeignTravelHistory
                    {
                        employeeId = prev.employeeId,
                        countryId = prev.countryId,
                        referenceNumber = prev.referenceNumber,
                        travelDuration = prev.travelDuration,
                        travelPurpose = prev.travelPurpose,
                        travelDate = prev.travelDate,
                        travelEndDate = prev.travelEndDate,
                        foreignTravelId = prev.Id,
                        entryType = 1,
                        UpdateUserId = user.Id
                    };

                    await _employeeService.SaveForeignTravelHistory(promotionHistory);
                }
                   
            }

            var foreignTravel = new ForeignTravel
            {
                Id = (int)model.ForeignTravelId,
                employeeId = model.employeeId,
                countryId = model.countryId,
                referenceNumber = model.referenceNumber,
                travelDuration = model.travelDuration,
                travelPurpose = model.travelPurpose,
                travelDate = Convert.ToDateTime(model.travelDate?.ToString("yyyy-MM-dd")),
                travelEndDate = Convert.ToDateTime(model.travelEndDate?.ToString("yyyy-MM-dd")),
                isAdminEntry = 1
            };
            var contry = await _addressService.GetCountryByCountryId(Convert.ToInt32(model.countryId));

            await _employeeService.SaveForeignTravel(foreignTravel);
            foreignTravel.country = contry;
            string remarks = "";
            if (model.travelPurpose == "UN Mission")
            {
                remarks = "remarks";
            }
            else
            {
                remarks = model.travelPurpose;
            }
            await _employeeService.SaveEmployeeTransectionHistoryLog((int)model.employeeId, 11, user.Id, "Portfolio", remarks);
            return Json(foreignTravel);
        }

        public async Task<IActionResult> DeleteForeignTravelDataById(int id)
        {
            try
            {
                var status = "";
                var data = await _employeeService.DeleteForeignTravel(id);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json("Success");
            }

        }

        public async Task<IActionResult> DeleteMedicalInfoById(int id)
        {
            var status = "Success";
            try
            {
                var delRel = await _employeeService.DeleteMedicalDiseaseInfoByMedicalId(id);
                if (delRel > 0)
                {
                    var data = await _employeeService.DeleteMedicalInfoById(id);
                    if (data > 0)
                    {
                        status = "Success";
                    }
                    else
                    {
                        status = "Failed";
                    }
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }
        }

        public async Task<IActionResult> DeletePromotionInfoById(int id)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeletePromotionInfoById(id);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }
        }

        public async Task<IActionResult> DeleteFamilyInfoById(int id)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeleteFamilyInfoById(id);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }
        }

        public async Task<IActionResult> DeleteJobInfoById(int id)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeleteAssignmentInfoById(id);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }
        }

        public async Task<IActionResult> DeletePreAddressById(int id)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeletePresentAddressById(id);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }

        }

        public async Task<IActionResult> DeleteEducationalQualificationInfoById(int id)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeleteEducationalQualificationInfoById(id);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }

        }

        public async Task<IActionResult> DeleteDisciplinaryInfoById(int id)
        {
            var status = "";
            var data = await _employeeService.DeleteDisciplinaryById(id);
            if (data > 0)
            {
                status = "Success";
            }
            else
            {
                status = "Failed";
            }
            return Json(status);
        }

        public async Task<IActionResult> DeleteAwardInfoById(int id)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeleteAwardById(id);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }

        }

        public async Task<IActionResult> DeleteTrainingInfoById(int training)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeleteTrainingInfoById(training);
                if (data > 0)
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }
                return Json(status);
            }
            catch (Exception)
            {
                return Json(status);
            }

        }

        [HttpPost]
        public async Task<IActionResult> EducationInfo([FromForm] EmployeeViewModel model)
        {
            int organizationId = 0;
            if (model.OtherOrganization != null || model.organizationId == 5000)
            {
                var data = _organizationService.GetAll().Where(x => x.organizationName.ToLower().Trim() == model.OtherOrganization.ToLower().Trim()).ToList();
                if (data.Count() <= 0)
                {
                    Organization organization = new Organization()
                    {
                        organizationName = model.OtherOrganization,
                        organizationNameBn = model.OtherOrganization,
                        organizationType = "University",
                        countryId = model.educationCountryId,
                    };
                    organizationId = await _degreeService.SaveOrganization(organization);
                }
            }
            //var NeworganizationName = _organizationService.GetAll()
            //           .OrderByDescending(p => p.Id)
            //           .FirstOrDefault();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var prev = await _employeeService.GetEducationalQualificationInfoById(model.empEducationId);

            if (prev != null)
            {
                if (prev.isAdminEntry == null)
                {
                    var educationalQualificationHistory = new EducationalQualificationHistory
                    {
                        employeeId = prev.employeeId,
                        degreeId = prev.degreeId,
                        reldegreesubjectId = prev.reldegreesubjectId,
                        organizationId = prev.organizationId,
                        resultId = prev.resultId,
                        passingYear = prev.passingYear,
                        institution = prev.institution,
                        grade = prev.grade,
                        entryType = 1,
                        UpdateUserId = user.Id,
                        educationalQualificationId = prev.Id
                    };

                    await _employeeService.SaveEducationalQualificationHistory(educationalQualificationHistory);
                }
            }

            var educationalQualification = new EducationalQualification
            {
                employeeId = model.empId,
                Id = model.empEducationId,
                degreeId = model.degreeId,
                reldegreesubjectId = model.reldegreesubjectId,
                organizationId = model.organizationId != 5000 ? model.organizationId : organizationId,
                resultId = model.resultId,
                passingYear = model.passingYear,
                institution = model.institution,
                isAdminEntry = 1
            };
            if (model.resultId == 1)
            {
                educationalQualification.grade = model.gradeClass;
            }
            else if (model.resultId == 2)
            {
                educationalQualification.grade = model.gradeGPA;
            }
            else
            {
                educationalQualification.grade = model.gradeGPA;
            }
            await _employeeService.SaveEducationInformation(educationalQualification);
            await _employeeService.SaveEmployeeTransectionHistoryLog(model.empId, 11, user.Id, "Portfolio", "Educational Information");
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> TrainingInfo([FromForm] EmployeeViewModel model)
        {

            if (model.Other != null || model.trainingInstituteId == 10000)
            {
                var data = _trainingInstitute.GetAll().Where(x => x.trainingInstituteName.ToLower().Trim() == model.Other.ToLower().Trim()).ToList();
                if (data.Count <= 0)
                {
                    TrainingInstitute Obj = new TrainingInstitute()
                    {
                        trainingInstituteName = model.Other,
                        trainingInstituteNameBn = model.Other,
                        trainingInstituteShortName = model.Other,
                    };
                    _trainingInstitute.Insert(Obj);
                }

            }
            var NewTrainingInstitute = _trainingInstitute.GetAll()
                       .OrderByDescending(p => p.Id)
                       .FirstOrDefault();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var prev = await _employeeService.GetTraningLogInfoById(model.empTrainingId);
            if (prev != null)
            {
                if (prev.isAdminEntry == null)
                {
                    var traningLogHistory = new TraningLogHistory
                    {
                        employeeId = prev.employeeId,
                        trainingType = prev.trainingType,
                        trainingTitle = prev.trainingTitle,
                        trainingCategoryId = prev.trainingCategoryId,
                        countryId = prev.countryId,
                        referenceNumber = prev.referenceNumber,
                        remarks = prev.remarks,
                        trainingInstituteId = prev.trainingInstituteId,
                        fromDate = prev.fromDate,
                        toDate = prev.toDate,
                        sponsoringAgency = prev.sponsoringAgency,
                        entryType = 1,
                        traningLogId = prev.Id,
                        UpdateUserId = user.Id
                    };

                    await _employeeService.SaveTraningLogHistory(traningLogHistory);
                }
               
            }

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
                trainingInstituteId = model.trainingInstituteId == 10000 ? NewTrainingInstitute.Id : model.trainingInstituteId,
                fromDate = model.fromDate,
                toDate = model.toDate,
                sponsoringAgency = model.fortrainingInstitute,
                isAdminEntry = 1

            };

            await _employeeService.SaveTrainingInformation(traningLog);
            await _employeeService.SaveEmployeeTransectionHistoryLog(model.empId, 11, user.Id, "Portfolio", "Training Information");
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> AwardInfo([FromForm] EmployeeViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var prev = await _employeeService.GetAwardInfoById(model.empAwardId);
            if (prev != null)
            {
                if (prev.isAdminEntry == null)
                {
                    var promotionHistory = new AwardEntryHistory
                    {
                        employeeId = prev.employeeId,
                        awardId = prev.awardId,
                        purpose = prev.purpose,
                        awardDate = prev.awardDate,
                        referenceNumber = prev.referenceNumber,
                        awardName = prev.awardName,
                        awardEntryId = prev.Id,
                        entryType = 1,
                        UpdateUserId = user.Id
                    };

                    await _employeeService.SaveAwardEntryHistory(promotionHistory);
                }
               
            }

            var award = new AwardEntry
            {
                employeeId = model.empId,
                Id = model.empAwardId,
                awardId = Convert.ToInt32(model.awardName),
                purpose = model.purpose,
                awardDate = Convert.ToDateTime(model.awardDate),
                referenceNumber = model.referenceNum,
                awardName = model.otherAwardName,
                isAdminEntry = 1
            };

            await _employeeService.SaveAwardInformation(award);
            await _employeeService.SaveEmployeeTransectionHistoryLog(model.empId, 11, user.Id, "Portfolio", "Award Information");
            var data = await _employeeService.GetAwardInfoByEmpId(model.empId);
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> DisciplinaryInfo([FromForm] EmployeeViewModel model)
        {
            var disciplinary = new DisciplinaryAction
            {
                employeeId = model.empId,
                Id = model.disciplinaryId,
                OffenseId = model.offenseId,
                naturalPunishmentId = model.naturalPunishmentId,

                punishmentDate = model.dispunishmentDate,
                startingDate = model.punishmentstartingDate,
                endDate = model.punishmentendingDate,
                goNumberWithDate = model.disDuration,
                status = model.status,
                referenceNumber = model.referenceNum,
                remarks = model.remark,
            };

            await _employeeService.SaveDisciplinaryInformation(disciplinary);
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddressInfo([FromForm] EmployeeViewModel model)
        {
            try
            {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var prev = await _employeeService.GetAddressById(model.addressId);
            if (prev != null)
            {
                if (prev.isAdminEntry == null)
                {
                    var AddressInformationHistory = new AddressInformationHistory
                    {
                        employeeInfoId = prev.employeeInfoId,
                        type = prev.type,
                        divisionId = prev.divisionId,
                        districtId = prev.districtId,
                        thanaId = prev.thanaId,
                        unionWardId = prev.unionWardId,
                        postCode = prev.postCode,
                        addressDetails = prev.addressDetails,
                        addressInformationId = prev.Id,
                        UpdateUserId = user.Id,
                        entryType = 1,
                    };
                    await _employeeService.SaveAddressInformationHistory(AddressInformationHistory);
                }
                    
            }
            var addressInformation = new AddressInformation
            {
                employeeInfoId = model.empId,
                Id = model.addressId,
                type = model.addressType,
                divisionId = model.divisionId,
                districtId = model.districtId,
                thanaId = model.thanaId,
                unionWardId = model.unionId,
                postCode = model.postCode,
                addressDetails = model.AddressDetails,
                isAdminEntry = 1
            };
            var delete = await _employeeService.DeleteAddressInfoByEmpIdType(model.empId, model.addressType);
            await _employeeService.SaveAddressInformation(addressInformation);
            await _employeeService.SaveEmployeeTransectionHistoryLog(model.empId, 11, user.Id, "Portfolio", "Address Information");
            var data = await _employeeService.GetAddressByEmpIdType(model.empId, model.addressType);
            return Json(data.Id);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> SavePermanentAddressAsPresent(int empId, string type)
        {
            var preAdd = await _employeeService.GetAddressByEmpIdType(empId, "Permanent Address");
            var addressInformation = new AddressInformation
            {
                employeeInfoId = empId,
                Id = 0,
                type = type,
                divisionId = preAdd.divisionId,
                districtId = preAdd.districtId,
                thanaId = preAdd.thanaId,
                unionWardId = preAdd.unionWardId,
                postCode = preAdd.postCode,
                addressDetails = preAdd.addressDetails,
                isDelete = 1
            };
            var delete = await _employeeService.DeleteAddressInfoByEmpIdType(empId, type);
            await _employeeService.SaveAddressInformation(addressInformation);
            var data = await _employeeService.GetAddressByEmpIdType(empId, type);
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMedicalData([FromForm] EmployeeViewModel model)
        {
            var medicalInfo = new MedicalInfo
            {
                employeeInfoId = model.empId,
                Id = model.empMedicalId,
                date = model.date,
                checkUpDate = model.checkUpDate,
                recoverydate = model.recoverydate,
                hospitalName = model.hospitalName,
                lastCheckupHistory = model.lastCheckupHistory,
                remarks = model.remarks,
                referanceDoctor = model.referanceDoctor,
                CVRMedicalInjury = model.CVRMedicalInjury,
                year = model.year
            };
            if (model.isDelete == "on")
            {
                medicalInfo.isDelete = 1;
            }
            if (model.isHospitalised == "on")
            {
                medicalInfo.isHospitalise = 1;
            }
            else
            {
                medicalInfo.isHospitalise = 0;
            }
            if (model.isVaccinated == "on")
            {
                medicalInfo.satatus = 1;
                medicalInfo.date = model.vaccinatedDate;
            }
            else
            {
                medicalInfo.satatus = 0;
            }
            int medicalId = await _employeeService.SaveMedicalInfo(medicalInfo);
            if (model.empMedicalId > 0)
            {
                int removeDisease = await _employeeService.DeleteMedicalDiseaseInfoByMedicalId(medicalId);
            }
            if (model.diseaseIds != null && model.diseaseIds.Length > 0)
            {
                foreach (var items in model.diseaseIds)
                {
                    EmployeeMedicalDisease medicalDisease = new EmployeeMedicalDisease
                    {
                        medicalId = medicalId,
                        diseaseId = items
                    };
                    await _employeeService.SaveMedicalDiseaseInfo(medicalDisease);
                }
            }
            //if (model.vaccineIds != null && model.vaccineIds.Length > 0)
            //{
            //    foreach (var items in model.vaccineIds)
            //    {
            //        EmployeeMedicalVaccine medicalVaccine = new EmployeeMedicalVaccine
            //        {
            //            medicalId = medicalId,
            //            vaccinesId = items
            //        };
            //        await _employeeService.SaveMedicalVaccineInfo(medicalVaccine);
            //    }
            //}
            //var data = await _employeeService.GetMedicalInfoByEmpId(model.empId);
            var data = await _employeeService.GetMedicalInfoByMedId(medicalId);
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMedicalData1([FromForm] EmployeeViewModel model)
        {
            if (model.diseaseIdArr != null && model.diseaseIdArr.Length > 0)
            {
                for (int i = 0; i < model.diseaseIdArr.Length; i++)
                {
                    var medicalInfo = new MedicalInfo
                    {
                        employeeInfoId = model.empId,
                        Id = model.medicalIdArr[i],
                        isHospitalise = model.isHospitalisedArr[i],
                        isDelete = model.isVaccinatedArr[i],
                        date = model.vaccinatedDateArr[i],
                        satatus = model.isUnderArr[i],
                        year = model.yearArr[i],
                        lastCheckupHistory = model.observationArr[i]
                    };
                    int medicalId = await _employeeService.SaveMedicalInfo(medicalInfo);
                    EmployeeMedicalDisease medicalDisease = new EmployeeMedicalDisease
                    {
                        medicalId = medicalId,
                        diseaseId = model.diseaseIdArr[i]
                    };
                    await _employeeService.SaveMedicalDiseaseInfo(medicalDisease);
                }
            }
            var data = await _employeeService.GetMedicalInfoByEmpId(model.empId);
            return Json(data);
        }



        //POST: Photograph/Create
        //[HttpPost]
        public async Task<ActionResult> ProfilePictureSave([FromForm] PhotographViewModel model)
        {
            string status = "";
            string filename = "formal.jpg";
            string signaturefileName = "";
            string Message = "success";
            var localPath = "EmpImages";
            if (model.empPhoto != null)
            {
                var image = Image.Load(model.empPhoto.OpenReadStream());
                string systemFileExtenstion = filename.Substring(filename.LastIndexOf('.'));
                filename = Path.Combine(localPath, DateTime.Now.Ticks + systemFileExtenstion);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);
                image.Mutate(x => x.Resize(300, 300));
                image.Save(path);
            }
            string signaturemessage = "";
            if (model.signaturePhoto != null)
            {

                signaturemessage = FileSave.SaveImage(out signaturefileName, model.signaturePhoto);
            }
            if (Message == "success")
            {
                Photograph data = new Photograph
                {
                    employeeId = model.employeeID,
                    url = filename,
                    type = "profile"
                };
                if (model.photographID == 0 || model.photographID < 0)
                {
                    data.Id = 0;
                }
                else
                {
                    data.Id = model.photographID;
                }
                // await photographService.DeleteempId(model.employeeID);
                var save = await photographService.SavePhotograph(data);
                if (save == true)
                {
                    status = "success";
                }
                else
                {
                    status = "failed";
                }
            }
            if (signaturemessage == "success")
            {
                //await photographService.DeleteempId(model.employeeID);
                Photograph result = new Photograph
                {
                    employeeId = model.employeeID,
                    url = signaturefileName,
                    type = "signature"
                };
                if (model.signatureID == 0 || model.signatureID < 0)
                {
                    result.Id = 0;
                }
                else
                {
                    result.Id = model.signatureID;
                }

                //await photographService.DeleteempId(model.employeeID);
                var signatureSave = await photographService.SavePhotograph(result);
                if (signatureSave == true)
                {
                    status = "success";
                }
                else
                {
                    status = "failed";
                }
            }
            return Json(status);
        }


        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoReportViewPDF(string empIdentityUserSessionToken)
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
            url = $"" + scheme + "://" + host + "/Employee/EmployeeInfoHistory/EmployeeInfoReportView?id=" + empInfo?.employeeCode;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
       
        public async Task<IActionResult> EmployeeInfoReportView(string id)
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
                    medicalInfo = medicalIno,

                    employeeInfoHistory = await _employeeService.GetEmployeeInfoHistoryById(id),
                    awardEntryHistories = await _employeeService.GetAwardEntryHistoryByEmpId(empInfo.Id),
                    spouseHistories = await _employeeService.GetSpouseHistoryByEmpId(empInfo.Id),
                    assignmentHistories = await _employeeService.GetAssignmentHistoryByEmpId(empInfo.Id),
                    educationalQualificationHistories = await _employeeService.GetEducationalQualificationHistoryByEmpId(empInfo.Id),
                    promotionLogHistories = await _employeeService.GetPromotionLogHistoryByEmpId(empInfo.Id),
                    traningLogHistories = await _employeeService.GetTraningLogHistoryByEmpId(empInfo.Id),
                    foreignTravelHistories = await _employeeService.GetForeignTravelHistoryById(empInfo.Id),
                    addressInformationHistories = await _employeeService.GetAddressInformationHistoryByEmpId(empInfo.Id),
                };
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoMobileReportViewPDF(string empIdentityUserSessionToken)
        {

            var empInfo = _employeeService.GetBasicEmployeeInfoById(Convert.ToInt32(empIdentityUserSessionToken));

            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/EmployeeInfoHistory/EmployeeInfoReportViewForMobile?id=" + empInfo?.employeeCode;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoReportViewForMobile(string id)
        {
            try
            {
                
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
                    medicalInfo = medicalIno,

                    employeeInfoHistory = await _employeeService.GetEmployeeInfoHistoryById(id),
                    awardEntryHistories = await _employeeService.GetAwardEntryHistoryByEmpId(empInfo.Id),
                    spouseHistories = await _employeeService.GetSpouseHistoryByEmpId(empInfo.Id),
                    assignmentHistories = await _employeeService.GetAssignmentHistoryByEmpId(empInfo.Id),
                    educationalQualificationHistories = await _employeeService.GetEducationalQualificationHistoryByEmpId(empInfo.Id),
                    promotionLogHistories = await _employeeService.GetPromotionLogHistoryByEmpId(empInfo.Id),
                    traningLogHistories = await _employeeService.GetTraningLogHistoryByEmpId(empInfo.Id),
                    foreignTravelHistories = await _employeeService.GetForeignTravelHistoryById(empInfo.Id),
                    addressInformationHistories = await _employeeService.GetAddressInformationHistoryByEmpId(empInfo.Id),
                };
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoReportPreviousPDF(string empIdentityUserSessionToken)
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
            url = $"" + scheme + "://" + host + "/Employee/EmployeeInfoHistory/EmployeeInfoReportPrevious?id=" + empInfo?.employeeCode;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        public async Task<IActionResult> EmployeeInfoReportPrevious(string id)
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
                    medicalInfo = medicalIno,

                    employeeInfoHistory = await _employeeService.GetEmployeeInfoHistoryById(id),
                    awardEntryHistories = await _employeeService.GetAwardEntryHistoryByEmpId(empInfo.Id),
                    spouseHistories = await _employeeService.GetSpouseHistoryByEmpId(empInfo.Id),
                    assignmentHistories = await _employeeService.GetAssignmentHistoryByEmpId(empInfo.Id),
                    educationalQualificationHistories = await _employeeService.GetEducationalQualificationHistoryByEmpId(empInfo.Id),
                    promotionLogHistories = await _employeeService.GetPromotionLogHistoryByEmpId(empInfo.Id),
                    traningLogHistories = await _employeeService.GetTraningLogHistoryByEmpId(empInfo.Id),
                    foreignTravelHistories = await _employeeService.GetForeignTravelHistoryById(empInfo.Id),
                    addressInformationHistories = await _employeeService.GetAddressInformationHistoryByEmpId(empInfo.Id),
                };
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [AllowAnonymous]
        public IActionResult GetEmployeeListReportPdf(int unitId, int rankId, int batchId, int statusId, string periodType)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/EmployeeInfoHistory/GetEmployeeListReport?unitId=" + unitId + "&&rankId=" + rankId + "&&batchId=" + batchId + "&&statusId=" + statusId + "&&periodType=" + periodType;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> GetEmployeeListReport(int unitId, int rankId, int batchId, int statusId, string periodType)
        {
            periodType = (periodType == null) ? "" : periodType;
            var data = new EmployeeInfoViewModel
            {
                employeeInfosViewModelFor_SPs = await _employeeService.GetEmployeeInfoListForSp(User.Identity.Name, unitId, rankId, batchId, statusId, periodType),

            };
            return View(data);
        }

    }
}