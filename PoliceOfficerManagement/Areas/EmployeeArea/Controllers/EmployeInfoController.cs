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
                Ranks = await _masterDataServices.GetRank()

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

            var id = await _employeeServices.SaveEmployeeInfo(data);
            return Json(id);
        }

        [HttpPost]
        public async Task<IActionResult> InActiveEmployee(int Id)
        {
            var data = await _employeeServices.InActiveEmployeeById(Id);
            return Json(true);
        }
        public IActionResult EmployeeList()
        {
            return View();
        }
    }
}
