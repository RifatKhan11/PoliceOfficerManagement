using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoliceOfficerManagement.Areas.EmployeeArea.Models;
using PoliceOfficerManagement.Areas.MasterData.Models;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Data.Entity;
using PoliceOfficerManagement.Services.AuthServices.Interfaces;
using PoliceOfficerManagement.Services.Employee.Interfaces;
using PoliceOfficerManagement.Services.MasterData.Interfaces;

namespace PoliceOfficerManagement.Areas.EmployeeArea.Controllers
{
    [Area("EmployeeArea")]
    public class EmployeInfoController : Controller
    {
        private readonly IEmployeeServices _employeeServices;
        private readonly IMasterDataServices _masterDataServices;
        private readonly IUserInfoes userInfoes;
        private readonly string rootPath;
        public string FileName;

        public EmployeInfoController(IWebHostEnvironment hostingEnvironment, IMasterDataServices masterDataServices,IUserInfoes userInfoes, IEmployeeServices employeeServices)
        {
            

            this.userInfoes = userInfoes;
            this._employeeServices = employeeServices;
            this._masterDataServices = masterDataServices;
            rootPath = hostingEnvironment.ContentRootPath;
        }


        public async Task<IActionResult> CreateEmployeeProfile()
        {
            var model = new EmployeInfoViewModel
            {
                employeInfos = await _employeeServices.GetEmployeeInfo(),
                divisions = await _masterDataServices.GetAllDivision(),
                institutionInfos = await _masterDataServices.GetAllInstitutionInfo(),
                institutionInfoTraning = await _masterDataServices.GetAllInstitutionInfoForTraning(),
                Ranks = await _masterDataServices.GetRank(),
                RangeMetros =await _masterDataServices.GetAllRangeMetros(),

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeProfile(EmployeInfoViewModel model)
        {
            EmployeInfo data = new EmployeInfo
            {
                Id = model.Id,
                nameEn  = model.nameEn, 
                nameBn  = model.nameBn, 
                employeeCode  = model.employeeCode, 
                nidNumber  = model.nidNumber, 
                joiningDate  = model.joiningDate, 
                retirementDate  = model.retirementDate, 
                joiningRankId  = model.joiningRankId ,
                homeDistrict  = model.homeDistrict, 
                homeUpazila  = model.homeUpazila, 
                reMarks  = model.reMarks,
                personalPhoneNumber = model.personalPhoneNumber,
                officePhoneNumber = model.officePhoneNumber, 

            };

            var empId = await _employeeServices.SaveEmployeeInfo(data);

            List<EducationalInfo> edu = new List<EducationalInfo>();
            List<TrainingInfo> train = new List<TrainingInfo>();
            List<PostingPlace> post = new List<PostingPlace>();
            List<AdderssInfo> add = new List<AdderssInfo>();
            if (model.eInstituteId != null && model.eInstituteId.Count() > 0)
            {
               ;
                for (int i = 0; i < model.eInstituteId.Length; i++)
                {
                    var edus = new EducationalInfo
                    {
                        instituteId = model.eInstituteId[i],
                        passingYear= model.ePassingYear[i],
                        batchNo = model.eBatchNo[i],
                        grade = model.eGrade[i],
                        degreeName = model.eDegreeName[i],
                        achievement = model.eAchievement[i],
                        courseDuration = model.eCourseDuration[i],
                        employeeId = empId,
                    };
                    edu.Add(edus);
                }
            }
            
            if (model.instituteId != null && model.instituteId.Count() > 0)
            {
                
                for (int i = 0; i < model.instituteId.Length; i++)
                {
                    var trn = new TrainingInfo
                    {
                        instituteId = model.instituteId[i],
                        passingYear= model.passingYear[i],
                        batchNo = model.batchNo[i],
                        grade = model.grade[i],
                        degreeName = model.degreeName[i],
                        achievement = model.achievement[i],
                        courseDuration = model.courseDuration[i],
                        employeeId = empId,
                    };
                    train.Add(trn);
                }
            }
            if (model.rankId != null && model.rankId.Count() > 0)
            {
                
                for (int i = 0; i < model.rankId.Length; i++)
                {
                    var trn = new PostingPlace
                    {
                        rankId = model.rankId[i],
                        postingFrom= model.postingFrom[i],
                        postingTo = model.postingTo[i],
                        thanaId = model.thanaId[i] == 0 ? null : model.thanaId[i],
                        zoneId = model.zoneId[i] == 0 ? null : model.zoneId[i],
                        districtId = model.districtId[i] == 0 ? null : model.districtId[i],
                        rangeId = model.rangeId[i] == 0 ? null : model.rangeId[i],
                        reMarks = model.pReMarks[i],
                        //promotionDate = model.promotionDate[i],
                        employeeId = empId,
                    };
                    post.Add(trn);
                }
            }
            if (model.addressType != null && model.addressType.Count() > 0)
            {
                
                for (int i = 0; i < model.addressType.Length; i++)
                {
                    var trn = new AdderssInfo
                    {
                        addressType = model.addressType[i],
                        roadInfo = model.roadInfo[i],
                        villegeId = model.aVillegeId[i]== 0 ? null : model.aVillegeId[i],
                        unionId = model.aUnionId[i] == 0 ? null : model.aUnionId[i],
                        thanaId = model.aThanaId[i] == 0 ? null : model.aThanaId[i],
                        districtId = model.aDistrictId[i] == 0 ? null : model.aDistrictId[i],
                        divisionId = model.aDivisionId[i] == 0 ? null : model.aDivisionId[i],                       
                        employeeId = empId,
                    };
                    add.Add(trn);
                }
            }

            await _employeeServices.SaveEmployeeOtherInfo(edu, train, post, add);
            return RedirectToAction("CreateEmployeeProfile");
            //return Json(empId);

        }

        [HttpPost]
        public async Task<IActionResult> InActiveEmployee(int Id)
        {
            var data = await _employeeServices.InActiveEmployeeById(Id);
            return Json(true);
        }
        public async Task<IActionResult> EmployeeList()
        {
            var model = new EmployeInfoViewModel
            {
                RangeMetros = await _masterDataServices.GetAllRangeMetros(),

            };
            return View(model);
        }
        public async Task<IActionResult> SearchEmployeePartial(int rangeId, int districtId, int zoneId, string name)
        {

            var emp = await _employeeServices.GetEmployeInfoSearch(rangeId, districtId, zoneId, name);
            EmployeeInfoModel model =new EmployeeInfoModel();
            model.Employees = emp;
            return PartialView("_EmployeePartial", model);
        }

        public async Task<IActionResult> EmployeeProfile(int empId)
        {
            var employee = await _employeeServices.GetEmployeInfoById(empId);
            var traing= await _employeeServices.GetTrainingInfoByEmpId(empId);
            var edu= await _employeeServices.GetEducationalInfoByEmpId(empId);
            var addr= await _employeeServices.GetAdderssInfoByEmpId(empId);
            var post= await _employeeServices.GetPostingPlaceByEmpId(empId);
            var model = new EmployeeProfileModel
            {
                EmployeeInfo = employee,
                TrainingInfo = traing,
                EducationalInfo = edu,
                AdderssInfo = addr,
                PostingPlace = post
            };
            return View(model);
        }
    }
}
