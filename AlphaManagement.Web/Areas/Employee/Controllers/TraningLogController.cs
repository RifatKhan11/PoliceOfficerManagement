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
using AlphaManagement.DAL.Entity.AddressData;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class TraningLogController : Controller
    {
        private readonly LangGenerate<TraningLogLn> _lang;
        private readonly IRepository<TraningLog> _repoTraningLog;

        private readonly IRepository<TrainingInstitute> _repoTrainingInstitute;
        private readonly IRepository<TrainingCategory> _repoTrainingCategory;
        private readonly IRepository<Country> _repoCountry;
        private readonly IRepository<EmployeeInfo> _repoEmployeeInfo;
        public TraningLogController(IHostingEnvironment hostingEnvironment,
            IRepository<TraningLog> repoTraningLog,
            IRepository<TrainingInstitute> repoTrainingInstitute,
            IRepository<TrainingCategory> repoTrainingCategory,
            IRepository<Country> repoCountry,
            IRepository<EmployeeInfo> repoEmployeeInfo
         )
        {
            _lang = new LangGenerate<TraningLogLn>(hostingEnvironment.ContentRootPath);
            _repoTraningLog = repoTraningLog;
            _repoTrainingInstitute = repoTrainingInstitute;
            _repoTrainingCategory = repoTrainingCategory;
            _repoCountry = repoCountry;
            _repoEmployeeInfo = repoEmployeeInfo;
        }


        public IActionResult Index(int id)
        {
            ViewBag.employeeId = id.ToString();
            var model = new TraningLogViewModel
            {
                fLang = _lang.PerseLang("Employee/TraningLogEN.json", "Employee/TraningLogBN.json", Request.Cookies["lang"]),
                traningLogs = _repoTraningLog.GetAll(),
                employeeInfos = _repoEmployeeInfo.GetAll(),
                countries = _repoCountry.GetAll(),
                trainingCategories = _repoTrainingCategory.GetAll(),
                trainingInstitutes = _repoTrainingInstitute.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] TraningLogViewModel model)
        {
            var Obj = new TraningLog
            {
                Id = model.TraningLogId,
                employeeId = model.employeeId,
                fromDate = model.fromDate,
                toDate = model.toDate,
                countryId = model.countryId,
                trainingCategoryId = model.trainingCategoryId,
                trainingInstituteId =model.trainingInstituteId,
                trainingTitle = model.trainingTitle,
                remarks = model.remarks,
                sponsoringAgency = model.sponsoringAgency
            };
            _repoTraningLog.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteTraningLog(int id)
        {
            bool response;

            try
            {
                _repoTraningLog.Delete(_repoTraningLog.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }
    }
}