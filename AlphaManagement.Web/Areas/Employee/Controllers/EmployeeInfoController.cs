using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
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
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data;
using ClosedXML.Excel;
using AlphaManagement.Domain.RepositoryService.Interfaces;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class EmployeeInfoController : Controller
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

        public IRepository<SpouseRelation> _spouseRelation { get; }

        private readonly IRepository<Spouse> _spouse;
        private readonly IRepository<EducationalQualification> _educationalQualification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public EmployeeInfoController(
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
            IRepository<Disease> disease,
            IRepository<StatusInfo> statusInfo,
            IConfiguration _configuration,


            IRepository<EducationalQualification> educationalQualification,
            IRepository<Banks> banks,
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
            _statusInfo = statusInfo;
            _district = district;
            _repoBCSBatch = repoBCSBatch;
            _repoPHQTRType = repoPHQTRType;
            this.userInfoes = userInfoes;
            this._employeePrintHistoryLog = _employeePrintHistoryLog;
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
                    //var userAgent = Request.Headers["User-Agent"].ToString();
                    //var mechineName = Environment.MachineName;

                    //var remote = HttpContext.Connection.RemoteIpAddress;
                    //var local = HttpContext.Connection.LocalIpAddress;
                    //string userip = remote.ToString();
                    //string useripLocal = local.ToString();

                    //UnauthorizeUserLog userLog = new UnauthorizeUserLog
                    //{
                    //    userId = User.Identity.Name,
                    //    logTime = DateTime.Now,
                    //    status = 1,
                    //    ipAddress = userip,
                    //    pcName = mechineName,
                    //    browserName = userAgent,
                    //    temptationString = empIdentityUserSessionToken,
                    //};
                    //int ipId = await accessLogHistoryService.SaveUnauthorizeUserLog(userLog);

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
                    pHQTRTypes = _repoPHQTRType.GetAll(),
                    specialBranchUnitsALL = _repoBranch.GetAll(),
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
                    pHQTRTypes = _repoPHQTRType.GetAll(),
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

        public async Task<IActionResult> EditAdminEmployeeIndex(string empIdentityUserSessionToken)
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
                    pHQTRTypes = _repoPHQTRType.GetAll(),
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

        public async Task<IActionResult> EditAdminEmployeeIndexNew(string empIdentityUserSessionToken)
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
                    pHQTRTypes = _repoPHQTRType.GetAll(),
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





        [Route("global/api/GetAssignMent/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssignMent(int id)
        {
            var assignments = await _employeeService.GetAssignmentInfoByEmpId(id);
            return Json(assignments);
        }

        public IActionResult ImageCroper()
        {
            return View();
        }

        public async Task<IActionResult> SecuritySettings(string id)
        {
            EmployeeInfoViewModel model = new EmployeeInfoViewModel
            {
                employeeInfo = await _employeeService.GetEmployeeInfoById(id),
            };
            return View(model);
        }

        [Route("global/api/GetFamilyInfoByEmpId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetFamilyInfoByEmpId(int id)
        {
            var spouse = await _employeeService.GetSpouseInfoByEmpId(id);
            return Json(spouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetDiseasesInfoByMedicalId(int id)
        {
            var data = await _employeeService.GetDiseasesInfoByMedicalId(id);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetDiseasesInfoById(int id)
        {
            var data = await _employeeService.GetDiseasesInfoById(id);
            return Json(data);
        }

        [Route("global/api/GetPromotionByEmpId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPromotionByEmpId(int id)
        {
            var promotion = await _employeeService.GetPromotionInfoByEmpId(id);
            return Json(promotion);
        }

        [Route("global/api/GetSpecialBranchUnitChild/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSpecialBranchUnitChild(int id)
        {
            var promotion = await _employeeService.GetSpecialBranchUnitChild(id);
            return Json(promotion);
        }

        [Route("global/api/GetPHQTRTypeById/{id}")]
        [HttpGet]
        public IActionResult GetPHQTRTypeById(int id)
        {
            var promotion = _repoPHQTRType.Get(id);
            return Json(promotion);
        }

        [Route("global/api/GetSpecialBranchUnitById/{id}")]
        [HttpGet]
        public IActionResult GetSpecialBranchUnitById(int id)
        {
            var promotion = _specialBranchUnit.Get(id);
            return Json(promotion);
        }

        [Route("global/api/GetEducationByEmpId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEducationByEmpId(int id)
        {
            var education = await _employeeService.GetEducationalQualificationInfoByEmpId(id);
            return Json(education);
        }

        [Route("global/api/GetTrainingByEmpId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTrainingByEmpId(int id)
        {
            var training = await _employeeService.GetTraningLogInfoByEmpId(id);
            return Json(training);
        }

        [Route("global/api/GetTrainingByEmpId/{id}/{type}")]
        [HttpGet]
        public async Task<IActionResult> GetTrainingByEmpIdType(int id, string type)
        {
            var training = await _employeeService.GetTraningLogInfoByEmpIdType(id, type);
            return Json(training);
        }

        [Route("global/api/GetAwardByEmpId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAwardByEmpId(int id)
        {
            var training = await _employeeService.GetAwardInfoByEmpId(id);
            return Json(training);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromForm] EmployeeViewModel model)
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
                var empInfo = _employeeService.GetBasicEmployeeInfoById((int)model.empId);
                empInfo.Id = model.empId;
                empInfo.ApplicationUserId = applicationUser.Id;
                empInfo.nameBangla = model.nameBangla;
                empInfo.nameEnglish = model.nameEnglish;
                empInfo.employeeCode = model.bpNo;
                empInfo.rankId = model.rankId;
                empInfo.designation = model.designation;
                empInfo.designationsId = model.designationsId;
                empInfo.fatherNameEnglish = model.fatherNameEnglish;
                empInfo.motherNameEnglish = model.motherNameEnglish;
                empInfo.gender = model.gender;
                empInfo.sectionName = model.sectionName;
                empInfo.sectionId = model.sectionId;
                empInfo.nationalID = model.nationalID;
                empInfo.homeDistrict = model.homeDistrict;
                empInfo.dateOfBirth = model.dateOfBirth;
                empInfo.LPRDate = model.lprDate;
                empInfo.bloodGroup = model.bloodGroup;
                empInfo.height = model.height;
                empInfo.weight = model.weight;
                empInfo.identificationSign = model.identificationSign;
                empInfo.religionId = model.religionId;
                empInfo.maritalStatus = model.maritalStatus;
                empInfo.tribal = model.tribal;
                empInfo.passportNo = model.passportNo;
                empInfo.rationId = model.rationId;
                empInfo.drivingLicense = model.drivingLicense;
                empInfo.branchId = model.specialBranchUnitId;
                empInfo.pHQTRTypeId = model.PhqTrType;
                empInfo.countryId = model.PhqCountry;
                empInfo.attachmentBranchId = model.attachmentBranchId;
                empInfo.isApproved = 2;

                int id = await _employeeService.SaveEmployeeInformation(empInfo);
                model.empId = id;

                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveMoreInfo([FromForm] EmployeeViewModel model)
        {
            try
            {
                var empInfo = _employeeService.GetBasicEmployeeInfoById((int)model.empId);

                empInfo.Id = model.empId;
                empInfo.joiningDatePresentWorkstation = model.joiningDatePresentWorkstation;
                empInfo.joiningDateGovtService = model.joiningDateGovt;
                empInfo.bCSBatchId = model.bCSBatchId;
                empInfo.bcsPosition = model.bcsPosition;
                empInfo.servicePeriod = model.servicePeriod;
                empInfo.joiningDesignation = model.joiningDesignation;
                empInfo.mobileNumberOffice = model.mobileNumberOffice;
                empInfo.mobileNumberPersonal = model.mobileNumberPersonal;
                empInfo.emailAddress = model.emailOffice;
                empInfo.emailAddressPersonal = model.emailPersonal;
                empInfo.promotionDate = model.lastpromotionDate;
                empInfo.banksId = model.salaryBankId;
                empInfo.salaryAccountNo = model.salaryAccountNo;
                empInfo.otherBanksId = model.otherBankId;
                empInfo.otherBankAccountNo = model.otherBankAccountNo;
                empInfo.linkdInId = model.linkdInId;
                empInfo.facebookId = model.facebookId;
                empInfo.skypeId = model.skypeId;
                empInfo.skill = model.skill;
                empInfo.extraActivity = model.extraActivity;
                empInfo.extraSkill = model.extraSkill;
                empInfo.extraActivitys = model.extraActivitys;
                empInfo.departmentalPromotionYear = model.departmentalPromotionYear;
                empInfo.isApproved = 2;
                empInfo.pabx = model.status;
                int id = await _employeeService.SaveEmployeeInformation(empInfo);
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
                var employee = await _employeeService.GetEmployeeInfoById(model.bpNo);
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
                employee.isApproved = 2;
                int id = await _employeeService.SaveEmployeeInformation(employee);
                model.empId = id;

                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("global/api/GetEmployeeApi")]
        [HttpGet]
        public IActionResult GetEmployeeApi()
        {
            var data = new EmployeeInfoViewModel
            {
                employeeInfos = _employeeInfo.GetAll(),
            };
            return Json(data);
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
                    statusInfos = _statusInfo.GetAll(),
                    degrees = _degree.GetAll(),
                    organizations = _organizationService.GetAll(),
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


        [HttpGet]
        public async Task<IActionResult> EmployeeInfoList()
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
                    statusInfos = _statusInfo.GetAll(),
                    degrees = _degree.GetAll(),
                    organizations = _organizationService.GetAll(),
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
            };

            await _employeeService.SaveFamilyInformation(spouse);

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentsInfo([FromForm] AssignmentViewModel model)
        {
            try
            {
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
                    designationName = model.designationName
                };

                await _employeeService.SaveAssignmentInformation(assignment);
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
            var promotion = new PromotionLog
            {
                employeeId = model.empId,
                Id = model.promotionId,
                rankId = model.promotionrankId,
                goDate = model.PromotionjoiningDate,
                date = Convert.ToDateTime(model.PromotionDate?.ToString("yyyy-MM-dd")),
                remark = model.remarks,
                goNumber = model.referenceNum
            };

            await _employeeService.SavePromotionInfo(promotion);
            return Json(model);
        }


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
                travelDate = Convert.ToDateTime(model.travelDate?.ToString("yyyy-MM-dd")),
                travelEndDate = Convert.ToDateTime(model.travelEndDate?.ToString("yyyy-MM-dd")),
            };
            var contry = await _addressService.GetCountryByCountryId(Convert.ToInt32(model.countryId));

            await _employeeService.SaveForeignTravel(foreignTravel);
            foreignTravel.country = contry;
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

        public async Task<IActionResult> DeleteReturnReasonById(int id)
        {
            var status = "Success";
            try
            {
                var data = await _employeeService.DeleteReturnResaonById(id);
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
                sponsoringAgency = model.fortrainingInstitute

            };

            await _employeeService.SaveTrainingInformation(traningLog);
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> AwardInfo([FromForm] EmployeeViewModel model)
        {
            var award = new AwardEntry
            {
                employeeId = model.empId,
                Id = model.empAwardId,
                awardId = Convert.ToInt32(model.awardName),
                purpose = model.purpose,
                awardDate = Convert.ToDateTime(model.awardDate),
                referenceNumber = model.referenceNum,
                awardName = model.otherAwardName
            };

            await _employeeService.SaveAwardInformation(award);
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
            };
            var delete = await _employeeService.DeleteAddressInfoByEmpIdType(model.empId, model.addressType);
            await _employeeService.SaveAddressInformation(addressInformation);
            var data = await _employeeService.GetAddressByEmpIdType(model.empId, model.addressType);
            return Json(data);
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




        #region API Section
        [Route("global/api/levelOfEducations")]
        [HttpGet]
        public IActionResult LevelOfEducations()
        {
            return Json(_lavelofEducation.GetAll());
        }
        #endregion

        #region API Section
        [Route("global/api/organizations")]
        [HttpGet]
        public IActionResult Organizations()
        {
            return Json(_organizationService.GetAll());
        }
        #endregion

        #region API Section
        [Route("global/api/results")]
        [HttpGet]
        public IActionResult Results()
        {
            return Json(_result.GetAll());
        }
        #endregion


        #region API Section
        [Route("global/api/degrees/{id}")]
        [HttpGet]
        public async Task<IActionResult> Degrees(int id)
        {
            return Json(await _degreeService.GetDegree(id));
        }
        #endregion

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
                    employeeReportInfosSB = await _employeeService.GetEmployeeReportInfoByEmpIdandType(empInfo.Id, "SB"),
                    employeeReportInfosBPA = await _employeeService.GetEmployeeReportInfoByEmpIdandType(empInfo.Id, "BPA"),
                    badgeAndActivityModels = await _employeeService.GetBadgeAndActivityModelList(),
                    employeeMadicalInfos = await _employeeService.GetEmployeeMadicalInfoByEmpIde(empInfo.Id),
                    medicalInfo = medicalIno
                };
                return PartialView("_EmployeeInformation", model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<IActionResult> GetEmployeeInfoPartialViewById(string id)
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
                    employeeReportInfosSB = await _employeeService.GetEmployeeReportInfoByEmpIdandType(empInfo.Id, "SB"),
                    employeeReportInfosBPA = await _employeeService.GetEmployeeReportInfoByEmpIdandType(empInfo.Id, "BPA"),
                    inLawsAddress = inLawAddress,
                    meternalAddress = meternalAddress,
                    medicalInfo = medicalIno
                };
                return PartialView("_EmployeeInfoById", model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<IActionResult> GetEmployeeInfoNewPartialViewById(string id)
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
                    employeeReportInfosSB = await _employeeService.GetEmployeeReportInfoByEmpIdandType(empInfo.Id, "SB"),
                    employeeReportInfosBPA = await _employeeService.GetEmployeeReportInfoByEmpIdandType(empInfo.Id, "BPA"),
                    medicalInfo = medicalIno
                };
                return PartialView("_EmployeeInfoNewById", model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        #region report
        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoReportView(string id, string code)
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
                    employeeCode = code
                };
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<IActionResult> EmployeeInfoReportPIMS(string id)
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
                    addressInformation = addressInformation
                };
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoReportViewPDF(string empIdentityUserSessionToken)
        {
            try
            {
                if (User.Identity.Name == null)
                {
                    return RedirectToAction("Not404Found", "Home");
                }

                var empInfo = _employeeService.GetBasicEmployeeInfoById(Convert.ToInt32(empIdentityUserSessionToken));
                if (empInfo?.employeeCode != User.Identity.Name || empInfo == null)
                {

                    return RedirectToAction("Not404Found", "Home");
                }
                string printCode = RandomString(12);
                string fileName;
                string host = _configuration["Host:Link"];
                string scheme = Request.Scheme;
                string url = string.Empty;
                url = $"" + scheme + "://" + host + "/Employee/EmployeeInfo/EmployeeInfoReportView?id=" + empInfo?.employeeCode + "&code=" + printCode;

                string status = myPDF.GeneratePDFLegal(out fileName, url);

                if (status != "done")
                {
                    return Content("<h1>Something Went Wrong</h1>");
                }

                var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var mechineName = Environment.MachineName;
                var userAgent = Request.Headers["User-Agent"].ToString();

                EmployeePrintHistoryLog data = new EmployeePrintHistoryLog
                {
                    userId = User.Identity.Name,
                    logTime = DateTime.Now,
                    status = 1,
                    ipAddress = ip,
                    pcName = mechineName,
                    browserName = userAgent,
                    employeeInfosId = empInfo.Id,
                    printCode = printCode,
                    fileName = "/pdf/" + fileName
                };

                _employeePrintHistoryLog.Insert(data);

                var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
                return new FileStreamResult(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoDynamicReportViewPDF(string queryString)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Not404Found", "Home");
            }

            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/EmployeeInfo/EmployeeInfoDynamicReportView?queryString=" + queryString;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoDynamicReportView(string queryString)
        {
            var model = new EmployeeInfoViewModel
            {
                EmployeeReports = await _employeeService.GetEmployeeInfos(queryString)
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult EmployeeInfoReportPIMSPDF(string id)
        {
            string fileName;
            string host = "localhost:44378";
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/EmployeeInfo/EmployeeInfoReportPIMS?id=" + id;

            string status = myPDF.GenerateLandscapePDF_A3(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        #endregion

        #region Api
        [HttpGet]
        [Route("/Employee/EmployeeInfo/EmployeeInfoById/{id}")]
        public async Task<IActionResult> EmployeeInfoById(int id)
        {
            var data = await _employeeService.GetEmployeeProfileInfoById(id);
            return Json(data);
        }
        #endregion

        [HttpGet]
        [Route("Employee/EmployeeInfo/EmployeeInfoProfileView/{id}")]
        public async Task<IActionResult> EmployeeInfoProfileView(int id)
        {
            try
            {
                EmployeeInfo employeeInfo = new EmployeeInfo();
                var empInfo = await _employeeService.GetEmployeeProfileInfoById(id);
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

                var model = new EmployeeProfileModel
                {
                    employeeInfo = employeeInfo,
                    photograph = photograph,
                    spouses = spouses,
                    assignments = assignments,
                    educationalQualifications = educationalQualifications,
                    awardEntries = awardEntries,
                    promotionLogs = promotionLogs,
                    traningLogs = traningLogs,
                    disciplinaryActions = disciplinaryActions,
                    addressInformation = addressInformation
                };
                return Json(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [Route("/global/api/GetForeignTravelByEmpId/{id}")]
        public async Task<IActionResult> GetForeignTravelByEmpId(int id)
        {
            try
            {
                var data = await _employeeService.GetForeignTravelsById(id);
                return Json(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("/global/api/GetMedicalInfoByEmpId/{id}")]
        public async Task<IActionResult> GetMedicalInfoByEmpId(int id)
        {
            try
            {
                var data = await _employeeService.GetMedicalInfoByEmpId(id);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> allSearch(string filter)
        {
            string userName = HttpContext.User.Identity.Name;
            ViewBag.FilterData = filter;
            var result = await _employeeService.GetGlobalSearchInfo(userName, filter);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeInfoConfirmation(int id)
        {
            var info = await _employeeService.GetEmployeeInfosByEmpId(id);
            if (info != null)
            {
                info.isApproved = 3;
                var save = await _employeeService.SaveEmployeeInformation(info);
                if (save > 0)
                {
                    await _employeeService.SaveEmployeeTransectionHistoryLog(info.Id, 3, info.ApplicationUserId, "Portfolio", "");
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeInfoConfirmationChecked(int id)
        {
            var info = await _employeeService.GetEmployeeInfosByEmpId(id);
            if (info != null)
            {
                info.isAdminCheck = 2;
                info.isApproved = 8;
                var save = await _employeeService.SaveEmployeeInformation(info);
                if (save > 0)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeInfoConfirmationReturn(int id)
        {
            var info = await _employeeService.GetEmployeeInfosByEmpId(id);
            if (info != null)
            {
                info.isAdminCheck = 3;
                var save = await _employeeService.SaveEmployeeInformation(info);
                if (save > 0)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {
                return Json("Fail");
            }
        }

        // [Route("global/api/relDegreeSubjects1/{id}")]
        [HttpGet]
        public async Task<IActionResult> RelDegreeSubjects(int Id)
        {
            return Json(await _degreeService.GetSubjectByDegreeId(Id));
        }


        [Route("global/api/relDegreeSubjects1/{id}")]
        [HttpGet]
        public async Task<IActionResult> RelDegreeSubjects1(int Id)
        {
            return Json(await _degreeService.GetSubjectByDegreeId(Id));
        }


        [Route("global/api/GetDistrictsByDivisonId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDistrictsByDivisonId(int Id)
        {
            return Json(await _addressService.GetDistrictsByDivisonId(Id));
        }

        [Route("global/api/GetThanasByDistrictId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetThanasByDistrictId(int Id)
        {
            return Json(await _addressService.GetThanasByDistrictId(Id));
        }

        [Route("global/api/GetUnionWardsByThanaId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUnionWardsByThanaId(int Id)
        {
            return Json(await _addressService.GetUnionWardsByThanaId(Id));
        }

        [Route("global/api/GetPostalCodeByThanaId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPostalCodeByThanaId(int Id)
        {
            return Json(await _addressService.GetPostCodeByThanaId(Id));
        }

        [Route("global/api/GetAllDivision/")]
        [HttpGet]
        public IActionResult GetDistrictsByDivisonId()
        {
            return Json(_division.GetAll());
        }

        //public async Task<IEnumerable<District>> GetDistrictsByDivisonId(int DivisionId)
        //{
        //    return await _context.Districts.Where(X => X.divisionId == DivisionId).ToListAsync();
        //}

        //// GET: api/AddressMaster/GetDistrictByDivisionId
        //[HttpGet("{id}")]
        //public async Task<IEnumerable<District>> GetDistrictByDivisionId(int id)
        //{

        //    var districts = await addressService.GetDistrictsByDivisonId(id);

        //    return districts;
        //}

        [Route("global/api/GetEmployeeSearchByUnitId/{unitId}/{rankId}/{batchId}/{statusid}/{periodType}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeSearchByUnitId(int unitId, int rankId, int batchId, int statusid, string periodType)
        {
            string userName = User.Identity.Name;
            var empsearch = await _employeeService.GetEmployeeInfoListForSp(userName, unitId, rankId, batchId, statusid, periodType);
            return Json(empsearch);
        }

        [Route("global/api/GetEmployeeSearchByRankId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeSearchByRankId(int id)
        {
            var empsearchrank = await _employeeService.GetEmployeeSearchByRankId(id);
            return Json(empsearchrank);
        }

        [Route("global/api/GetEmployeeSearchByDistrictId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeSearchByDistrictId(int id)
        {
            var empsearchdistrict = await _employeeService.GetEmployeeSearchByDistrictId(id);
            return Json(empsearchdistrict);
        }

        [Route("/global/api/EmployeeInfoDetails1/{id}")]
        [HttpGet]
        public async Task<IActionResult> EmployeeInfoDetails1(int id)
        {
            var data = await _employeeService.EmployeeInfoDetails(id);
            return Json(data);
        }

        [Route("/Employee/EmployeeInfo/GetLPRDate/{birthDate}")]
        [HttpGet]
        public async Task<IActionResult> GetLPRDate(string birthDate)
        {
            var data = await _employeeService.GetLPRDateByBirthDate(birthDate);
            return Json(data);
        }

        [Route("/global/api/GetPostingPlaceBySpecialBarnchId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPostingPlaceBySpecialBarnchId(int id)
        {
            var data = await _employeeService.GetPostingPlaceByBranchId(id);
            return Json(data);
        }

        [Route("/global/api/GetSectionInfoForEmployee/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSectionInfoForEmployee(int id)
        {
            var data = await _employeeService.GetSectionInfoForEmployee(id);
            return Json(data);
        }

        public IActionResult OfficerPortfolio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetMultipleEmployeeInfos(EmployeeReport model)
        {
            var result = await _employeeService.GetEmployeeInformationByQueryList(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetMultipleEmployeeInfosForGradation(EmployeeReport model)
        {
            var result = await _employeeService.GetEmployeeInformationByQueryListForGradation(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfos(string queryString)
        {

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            return Json(await _employeeService.GetEmployeeInfos(queryString));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoBySearch(string queryString)
        {

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            return Json(await _employeeService.GetEmployeeInfosBySearch(queryString));
        }

        [Authorize]
        public async Task<IActionResult> SearchEmployeeDetails(string BpNo)
        {

            if (BpNo == string.Empty || BpNo == null)
            {
                BpNo = User.Identity.Name;
            }

            var userInfo = await userInfoes.GetUserInfoByUser(BpNo);
            EmployeeInfo employeeInfo = new EmployeeInfo();
            var empInfo = await _employeeService.GetEmployeeInfoById(BpNo);
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
            var transactionLog = await _employeeService.GetPortfolioTransectionHistoryLog(empInfo.Id);
            var searchResult = new EmployeeInfoViewModel
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
                TransectionHistoryLog = transactionLog
            };
            return View(searchResult);
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHJKMNPQRSTVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #region Employee Details by Batch
        public IActionResult EmployeeDetailsInfo()
        {
            var model = new EmployeeInfoViewModel
            {
                bCSBatch = _repoBCSBatch.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> SearchEmployeeByBCSBatch(int? bcsBatchId)
        {
            //var newData = new List<EmpolyeeDetailsByBCS>();
            //var data = await _employeeService.GetEmployeeInfoByBCSBatch((int)bcsBatchId);
            var data = await _employeeService.GetCorrectionEmployeeInfoByBCSBatch((int)bcsBatchId);
            //foreach (var item in data)
            //{
            //    var newData1 = new EmpolyeeDetailsByBCS
            //    {
            //        id = item.id,
            //        name = item.name,
            //        fatherName = item.fatherName,
            //        motherName = item.motherName,
            //        bp = item.bp,
            //        rank = item.rank,
            //        workingPlace = item.workingPlace,
            //        address = item.address
            //    };
                //var bpNo = "BP" + item.bp.Replace(" ", "").Trim();
                //string url = String.Format("https://pims.police.gov.bd:8443/pimslive/webpims/opus/bpdetails/{0}", bpNo);

                //HttpClient client = new HttpClient();
                //HttpResponseMessage response = await client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //var smsData = await response.Content.ReadAsStringAsync();
                //var pIMSBody = JsonConvert.DeserializeObject<Web.Models.PIMSBody>(await response.Content.ReadAsStringAsync());
                //if (pIMSBody.items.Count() > 0)
                //{
                //    newData1.fatherName = pIMSBody.items[0].father_name;
                //    newData1.motherName = pIMSBody.items[0].mother_name;
                //    if (item.name == null)
                //    {
                //        newData1.name = pIMSBody.items[0].bangla_name;
                //    }
                //}
                //newData.Add(newData1);
            //}
            //return Json(newData);
            return Json(data);
        }


        [AllowAnonymous]
        public IActionResult SearchEmployeeByBCSBatchPdfAction(int bcsBatchId)
        {
            string fileName;
            string host = "localhost:44380";
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/EmployeeInfo/SearchEmployeeByBCSBatchPdf?bcsBatchId=" + bcsBatchId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> SearchEmployeeByBCSBatchPdf(int bcsBatchId)
        {
            //var newData = new List<EmpolyeeDetailsByBCS>();
            //var data = await _employeeService.GetEmployeeInfoByBCSBatch((int)bcsBatchId);
            var data = await _employeeService.GetCorrectionEmployeeInfoByBCSBatch((int)bcsBatchId);
            //foreach (var item in data)
            //{
                //var newData1 = new EmpolyeeDetailsByBCS
                //{
                //    id = item.id,
                //    name = item.name,
                //    bcsBatch = item.bcsBatch,
                //    fatherName = item.fatherName,
                //    motherName = item.motherName,
                //    rank = item.rank,
                //    bp = item.bp,
                //    workingPlace = item.workingPlace,
                //    address = item.address
                //};
                //var bpNo = "BP" + item.bp.Replace(" ", "").Trim();
                //string url = String.Format("https://pims.police.gov.bd:8443/pimslive/webpims/opus/bpdetails/{0}", bpNo);

                //HttpClient client = new HttpClient();
                //HttpResponseMessage response = await client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //var smsData = await response.Content.ReadAsStringAsync();
                //var pIMSBody = JsonConvert.DeserializeObject<Web.Models.PIMSBody>(await response.Content.ReadAsStringAsync());
                //if (pIMSBody.items.Count() > 0)
                //{
                //    newData1.fatherName = pIMSBody.items[0].father_name;
                //    newData1.motherName = pIMSBody.items[0].mother_name;
                //    if (item.name == null)
                //    {
                //        newData1.name = pIMSBody.items[0].bangla_name;
                //    }
                //}
                //newData.Add(newData1);
            //}

            var model = new EmpolyeeDetailsByBCSForPDF
            {
                //empolyeeDetailsByBCS = newData
                pMCorrectionDataModels = data
            };
            return View(model);

        }


        [AllowAnonymous]
        public async Task<IActionResult> SearchEmployeeByBCSBatchExcelAction(int bcsBatchId)
        {
            var data = await _employeeService.GetEmployeeInfoByBCSBatch((int)bcsBatchId);
            DataTable dt = new DataTable("Alpha "+data?.FirstOrDefault()?.bcsBatch+" List");
            dt.Columns.AddRange(new DataColumn[10]);
            //dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Name"),
            //                            new DataColumn("BP"),
            //                            new DataColumn("BCS Batch"),
            //                            new DataColumn("BCS Position"),
            //                            new DataColumn("Rank"),
            //                            new DataColumn("Father Name"),
            //                            new DataColumn("Mother Name"),
            //                            new DataColumn("Working Place"),
            //                            new DataColumn("Address") });
            DataColumn column = new DataColumn();
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("BP", typeof(String));
            dt.Columns.Add("BCS Batch", typeof(String));
            dt.Columns.Add("BCS Position (Database)", typeof(String));
            dt.Columns.Add("BCS Position (Original)", typeof(String));
            dt.Columns.Add("Rank", typeof(String));
            dt.Columns.Add("Father Name", typeof(String));
            dt.Columns.Add("Mother Name", typeof(String));
            dt.Columns.Add("Working Place", typeof(String));
            dt.Columns.Add("Address", typeof(String));
            foreach (var item in data)
            {
                //var bpNo = "BP" + item.bp.Replace(" ", "").Trim();
                //string url = String.Format("https://pims.police.gov.bd:8443/pimslive/webpims/opus/bpdetails/{0}", bpNo);

                //HttpClient client = new HttpClient();
                //HttpResponseMessage response = await client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //var smsData = await response.Content.ReadAsStringAsync();
                //var pIMSBody = JsonConvert.DeserializeObject<Web.Models.PIMSBody>(await response.Content.ReadAsStringAsync());
                //if (pIMSBody.items.Count() > 0)
                //{
                //    if (item.name == null)
                //    {
                //        dt.Rows.Add(pIMSBody.items[0].bangla_name, item.bp, item.bcsBatch,item.bcsPosition, "", item.rank, pIMSBody.items[0].father_name, pIMSBody.items[0].mother_name, item.workingPlace, item.address);
                //    }
                //    else
                //    {
                //        dt.Rows.Add(item.name, item.bp, item.bcsBatch, item.bcsPosition, "", item.rank, pIMSBody.items[0].father_name, pIMSBody.items[0].mother_name, item.workingPlace, item.address);
                //    }                    
                //}
                //else
                //{
                //    dt.Rows.Add(item.name, item.bp, item.bcsBatch, item.bcsPosition, "", item.rank, item.fatherName, item.motherName, item.workingPlace, item.address);
                //}
                dt.Rows.Add(item.name, item.bp, item.bcsBatch, item.bcsPosition, "", item.rank, item.fatherName, item.motherName, item.workingPlace, item.address);
                //dt.Rows.Add("1", "AAA", "BBB", "");
                ////Column1
                //DataColumn column = new DataColumn();
                //column.ColumnName = "Id";
                //column.DataType = typeof(int);
                //column.AllowDBNull = true;
                //dt.Columns.Add(column);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BCS Batch Wise List.xlsx");
                }
            }
        }
        #endregion

       
        #region PDF File
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmployeeInfoReportPdf(string queryString)
        {
            string scheme = Request.Scheme;
            //var host = Request.Host;
            var host = "localhost:44380";
            string filename = "";
            var url = scheme + "://" + host + "/Employee/EmployeeInfo/EmployeeInfoReportPdfView?queryString=" + queryString;
            var status = myPDF.GenerateLandscapePDF(out filename, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + filename, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoReportPdfView(string queryString)
        {
            EmployeeSearchReport model = new EmployeeSearchReport
            {
                employeeInfoReports = await _employeeService.GetEmployeeInfosBySearch(queryString)
            };
            return View(model);
        }
        #endregion
        #region Excel File
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmployeeInfoReportExcel(string queryString)
        {
            List<EmployeeSearchReport> employeeInfoReports = new List<EmployeeSearchReport>();
             employeeInfoReports = await _employeeService.GetEmployeeInfosBySearch(queryString);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ক্রমিক নং";
                worksheet.Cell(currentRow, 2).Value = "কর্মকর্তার নাম,জন্ম তারিখ, জন্ম জেলা ও শিক্ষাগত যোগ্যতা";
                worksheet.Cell(currentRow, 3).Value = "অভিজ্ঞতা (পদের \nনাম) নিয়োগের \nতারিখসহ";
                worksheet.Cell(currentRow, 4).Value = "পুলিশ ক্যাডারের \nপ্রারম্ভিক পদে কমিশনের সুপারিশ অনুযায়ী নিয়মিত নিয়োগ (সরাসরি / পদোন্নতি)";
                worksheet.Cell(currentRow, 5).Value = "সিনিয়র স্কেলে নিয়মিত পদোন্নতির তারিখ";
                worksheet.Cell(currentRow, 6).Value = "সামরিক কর্মকর্তাদের \nপ্রাসঙ্গিক তথ্যঃ- \nক) কমিশন প্রাপ্তির তারিখ \nখ) অবসর গ্রহণের তারিখ \nগ) সিভিল পদে নিয়োগ \n(পদের নাম ও তারিখসহ)";
                worksheet.Cell(currentRow, 7).Value = "বর্তমান পদে \nনিয়মিত নিয়োগের \nতারিখ";
                worksheet.Cell(currentRow, 8).WorksheetColumn().Shrink(4).Value = "ক্যাডারে প্রথমে এ্যাডহক";
                worksheet.Cell(currentRow, 9).Value = "মন্তব্য";
                //worksheet.Cell(currentRow, 10).Value = "";
                //worksheet.Cell(currentRow, 11).Value = "";
                //worksheet.Cell(currentRow, 12).Value = "";
                 currentRow = 2;
                worksheet.Cell(currentRow, 1).Value = "১";
                worksheet.Cell(currentRow, 2).Value = "২";
                worksheet.Cell(currentRow, 3).Value = "৩";
                worksheet.Cell(currentRow, 4).Value = "৪";
                worksheet.Cell(currentRow, 5).Value = "৫";
                worksheet.Cell(currentRow, 6).Value = "৬";
                worksheet.Cell(currentRow, 7).Value = "৭";
                worksheet.Cell(currentRow, 8).Value = "৮(ক)";
                worksheet.Cell(currentRow, 9).Value =  "৮(খ)";
                worksheet.Cell(currentRow, 10).Value = "৮(গ)";
                worksheet.Cell(currentRow, 11).Value = "৮(ঘ)";
                worksheet.Cell(currentRow, 12).Value = "";
                foreach (var data in employeeInfoReports)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = "";
                    worksheet.Cell(currentRow, 2).Value ="জনাব "+data.nameBangla+" \nবিপি-"+ LanguageProcessing.BanglaToEnglishConvert(data.BPNumberBn) +" \nজন্ম তারিখ:  "+ LanguageProcessing.EnlishToBanglaDateConvert (data.dateOfbirth)+" \nজন্ম জেলা:  "+data.birthPlace+" \nশিক্ষাগত যোগ্যতা:  "+ LanguageProcessing.EnlishToBanglaDegreeConvert(data.educationalQualifaction);
                    worksheet.Cell(currentRow, 3).Value = "";
                    worksheet.Cell(currentRow, 4).Value = "";
                    worksheet.Cell(currentRow, 5).Value = "";
                    worksheet.Cell(currentRow, 6).Value = "";
                    worksheet.Cell(currentRow, 7).Value = "";
                    worksheet.Cell(currentRow, 8).Value = "";
                    worksheet.Cell(currentRow, 9).Value = "";
                    worksheet.Cell(currentRow, 10).Value = "";
                    worksheet.Cell(currentRow, 11).Value = "";
                    worksheet.Cell(currentRow, 12).Value = "";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
                }
            }
        }
        #endregion
    }
}