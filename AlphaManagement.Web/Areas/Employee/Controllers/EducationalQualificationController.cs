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

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EducationalQualificationController : Controller
    {
        private readonly LangGenerate<EducationalQualificationLn> _lang;
        private readonly IRepository<EducationalQualification> _repoEducationalQualification;
        private readonly IRepository<Result> _repoResult;
        private readonly IRepository<EmployeeInfo> _repoEmployeeInfo;
        private readonly IRepository<Degree> _repoDegree;
        private readonly IRepository<Organization> _repoOrganization;
        private readonly IRepository<RelDegreeSubject> _repoRelDegreeSubject;
        private readonly IRepository<Subject> _repoSubject;
        public EducationalQualificationController(IHostingEnvironment hostingEnvironment,
            IRepository<EducationalQualification> repoEducationalQualification,
            IRepository<Result> repoResult,
            IRepository<EmployeeInfo> repoEmployeeInfo,
            IRepository<Degree> repoDegree,
            IRepository<Organization> repoOrganization,
            IRepository<RelDegreeSubject> repoRelDegreeSubject,
            IRepository<Subject> repoSubject
            )
        {
            _lang = new LangGenerate<EducationalQualificationLn>(hostingEnvironment.ContentRootPath);
            _repoEducationalQualification = repoEducationalQualification;
            _repoResult = repoResult;
            _repoEmployeeInfo = repoEmployeeInfo;
            _repoDegree = repoDegree;
            _repoOrganization = repoOrganization;
            _repoRelDegreeSubject = repoRelDegreeSubject;
            _repoSubject = repoSubject;
        }


        public IActionResult Index(int id)
        {
            ViewBag.employeeId = id.ToString();

            var model = new EducationalQualificationViewModel
            {
                fLang = _lang.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                educationalQualifications = _repoEducationalQualification.GetAll(),
                results= _repoResult.GetAll(),
                employeeInfos= _repoEmployeeInfo.GetAll(),
                degrees= _repoDegree.GetAll(),
                organizations= _repoOrganization.GetAll(),
                relDegreeSubjects= _repoRelDegreeSubject.GetAll(),
                subjects = _repoSubject.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] EducationalQualificationViewModel model)
        {
            var Obj = new EducationalQualification
            {
                Id = model.EducationalQualificationId,
                employeeId = model.employeeId,
                institution = model.institution,
                resultId = model.resultId,
                grade = model.grade,
                passingYear = model.passingYear,
                degreeId = model.degreeId,
                organizationId = model.organizationId,
                reldegreesubjectId = model.reldegreesubjectId
            };
            _repoEducationalQualification.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteEducationalQualification(int id)
        {
            bool response;

            try
            {
                _repoEducationalQualification.Delete(_repoEducationalQualification.Get(id));
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