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

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class AwardEntryController : Controller
    {
        private readonly LangGenerate<AwardEntryLn> _lang;
        private readonly IRepository<AwardEntry> _repoAwardEntry;
        private readonly IRepository<EmployeeInfo> _repoEmployeeInfo;
        public AwardEntryController(IHostingEnvironment hostingEnvironment, IRepository<AwardEntry> repoAwardEntry, IRepository<EmployeeInfo> repoEmployeeInfo)
        {
            _lang = new LangGenerate<AwardEntryLn>(hostingEnvironment.ContentRootPath);
            _repoAwardEntry = repoAwardEntry;
            _repoEmployeeInfo = repoEmployeeInfo;
        }


        public IActionResult Index(int id)
        {
            ViewBag.employeeId = id.ToString();
            var model = new AwardEntryViewModel
            {
                fLang = _lang.PerseLang("Employee/AwardEntryEN.json", "Employee/AwardEntryBN.json", Request.Cookies["lang"]),
                awardEntrys = _repoAwardEntry.GetAll(),
                employeeInfos = _repoEmployeeInfo.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] AwardEntryViewModel model)
        {
            var Obj = new AwardEntry
            {
                Id = model.AwardEntryId,
                employeeId = model.employeeId,
                awardName = model.awardName,
                awardDate = Convert.ToDateTime(model.awardDate),
                purpose = model.purpose,
                status = "Pending"

            };

            _repoAwardEntry.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteAwardEntry(int id)
        {
            bool response;

            try
            {
                _repoAwardEntry.Delete(_repoAwardEntry.Get(id));
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