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
    public class PromotionLogController : Controller
    {
        private readonly LangGenerate<PromotionLogLn> _lang;
        private readonly IRepository<PromotionLog> _repoPromotionLog;
        private readonly IRepository<EmployeeInfo> _repoEmployeeInfo;
        private readonly IRepository<Designation> _repoDesignation;
        private readonly IRepository<SalaryGrade> _repoSalaryGrade;
        private readonly IRepository<EmployeeInfo> _employeee;

        public PromotionLogController(IHostingEnvironment hostingEnvironment,
            IRepository<PromotionLog> repoPromotionLog,
            IRepository<EmployeeInfo> repoEmployeeInfo,
            IRepository<Designation> repoDesignation,
            IRepository<SalaryGrade> repoSalaryGrade,
            IRepository<EmployeeInfo> employeee
            )
        {
            _lang = new LangGenerate<PromotionLogLn>(hostingEnvironment.ContentRootPath);
            _repoPromotionLog = repoPromotionLog;
            _repoEmployeeInfo = repoEmployeeInfo;
            _repoDesignation = repoDesignation;
            _repoSalaryGrade = repoSalaryGrade;
            _employeee = employeee;
        }


        public IActionResult Index(int id)
        {
            ViewBag.employeeId = id.ToString();
            var model = new PromotionLogViewModel
            {
                fLang = _lang.PerseLang("Employee/PromotionLogEN.json", "Employee/PromotionLogBN.json", Request.Cookies["lang"]),
                promotionLogs = _repoPromotionLog.GetAll(),
                employeeInfos = _repoEmployeeInfo.GetAll(),
                newDesignations = _repoDesignation.GetAll(),
                oldDesignations = _repoDesignation.GetAll(),
                payScales = _repoSalaryGrade.GetAll(),
                employeeInfo = _employeee.Get(id),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] PromotionLogViewModel model)
        {
            var Obj = new PromotionLog
            {
                Id = model.PromotionLogId,
                employeeId = model.employeeId,
                date = model.date,
                designationNewId = model.designationNewId,
                designationOldId = model.designationOldId,
                remark = model.remark,
                goNumber = model.goNumber,
                goDate = model.goDate,
                payScaleId = model.payScaleId
            };
            if (Obj.Id>0)
            {
                _repoPromotionLog.Update(Obj);
            }
            else
            {
                _repoPromotionLog.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeletePromotionLog(int id)
        {
            bool response;

            try
            {
                _repoPromotionLog.Delete(_repoPromotionLog.Get(id));
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