using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EmployeeProfileController : Controller
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

        public IRepository<SpouseRelation> _spouseRelation { get; }

        private readonly IRepository<Spouse> _spouse;
        private readonly IRepository<EducationalQualification> _educationalQualification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public EmployeeProfileController(
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
            IConverter converter


        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
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


        public async Task<IActionResult> Index(int id)
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(applicationUser);
                var roleList = new List<UserRoleVM>();
                if (roles.Count() > 0)
                {
                    foreach (var item in roles)
                    {
                        var role = new UserRoleVM()
                        {
                            roleName = item
                        };
                        roleList.Add(role);
                    }
                }
                var model = new EmployeeInfoViewModel
                {
                    employeeInfo = await _employeeService.GetEmployeeProfileInfoById(id),
                    specialBranchUnits = _specialBranchUnit.GetAll(),
                    rank = _repoRank.GetAll(),
                    districts = _district.GetAll(),
                    userRoles = roleList,
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
        public async Task<IActionResult> EmployeeList()
        {
            try
            {
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
        public async Task<IActionResult> PRLEmployeeList(int? unitId=0, int? rankId=0, int? batchId = 0)
        {
            try
            {
                var EmpList = await _employeeService.GetPRLEmployeeListForAlphaProfile(rankId,unitId, batchId);
                var model = new EmployeeInfoViewModel
                {
                    employeeListVMs = EmpList,
                    specialBranchUnits = _specialBranchUnit.GetAll(),
                    rank = _repoRank.GetAll(),
                    bCSBatch = _repoBCSBatch.GetAll()
                };
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}