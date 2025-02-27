using System;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;
using AlphaManagement.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using AlphaManagement.Web.Areas.MasterData.Models;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Domain.EmployeeService.interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class SectionController : Controller
    {

        private readonly LangGenerate<SectionLn> _lang;
        private readonly IRepository<Section> _repoSection;
        private readonly IEmployeeService _employeeService;

        public SectionController(IHostingEnvironment hostingEnvironment, IRepository<Section> repoSection, IEmployeeService employeeService)
        {
            _lang = new LangGenerate<SectionLn>(hostingEnvironment.ContentRootPath);
            _repoSection = repoSection;
            _employeeService = employeeService;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new SectionViewModel
            {
                fLang = _lang.PerseLang("MasterData/SectionEN.json", "MasterData/SectionBN.json", Request.Cookies["lang"]),
                sections = _repoSection.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] SectionViewModel model)
        {
            var Obj = new Section
            {
                Id=model.SectionId,
                Code = model.Code,
                Name = model.Name,
                NameBN = model.NameBN,
                shortName = model.shortName,
                shortOrder = model.shortOrder

            };

            if (model.SectionId > 0)
            {

                _repoSection.Update(Obj);
            }
            else
            {
                _repoSection.Insert(Obj);
            }

            

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteSection(int id)
        //{

        //    _repoSection.Delete(_repoSection.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteSection(int id)
        {
            bool response;

            try
            {
                _repoSection.Delete(_repoSection.Get(id));
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
        public async Task<IActionResult> SectionWiseUnit()
        {
            var data = new EmployeeInfoViewModel
            {
                specialBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                sections = await _employeeService.GetSectionWiseUnit(),
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult SectionWiseUnit([FromForm]EmployeeInfoViewModel model)
        {
            if (model.SubbranchId!=null)
            {
                model.branchId = model.SubbranchId;
            }
            var special = new Section
            {
                Id=(int)model.sectionId,
                Name=model.nameEnglish,
                NameBN=model.nameBangla,
                specialBranchUnitId=model.branchId
            };
            if (model.sectionId>0)
            {
                _repoSection.Update(special);
            }
            else
            {
                _repoSection.Insert(special);
            }
            return RedirectToAction(nameof(SectionWiseUnit));
        }

        [HttpPost]
        public IActionResult DeletePostedUnit(int id)
        {
            bool response;

            try
            {
                _repoSection.Delete(_repoSection.Get(id));
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