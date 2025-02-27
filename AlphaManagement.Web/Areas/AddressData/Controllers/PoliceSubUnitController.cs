using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.AddressData.Models;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using AlphaManagement.Web.Helpers;
using Microsoft.AspNetCore.Hosting;


namespace AlphaManagement.Web.Areas.AddressData.Controllers
{
    [Area("AddressData")]
    public class PoliceSubUnitController : Controller
    {
        private readonly LangGenerate<PoliceSubUnitLn> _lang;
        private readonly IRepository<PoliceSubUnit> _repoPoliceSubUnit;
        private readonly IRepository<PoliceUnit> _repoPoliceUnit;
        public PoliceSubUnitController(IHostingEnvironment hostingEnvironment, IRepository<PoliceSubUnit> repoPoliceSubUnit, IRepository<PoliceUnit> repoPoliceUnit)
        {
            _lang = new LangGenerate<PoliceSubUnitLn>(hostingEnvironment.ContentRootPath);
            _repoPoliceSubUnit = repoPoliceSubUnit;
            _repoPoliceUnit = repoPoliceUnit;
        }


        public IActionResult Index()
        {
            var model = new PoliceSubUnitViewModel
            {
                fLang = _lang.PerseLang("AddressData/PoliceSubUnitEN.json", "AddressData/PoliceSubUnitBN.json", Request.Cookies["lang"]),
                policeSubUnitList = _repoPoliceSubUnit.GetAll(),
                policeUnitList = _repoPoliceUnit.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] PoliceSubUnitViewModel model)
        {
            var Obj = new PoliceSubUnit
            {
                Id = model.PoliceSubUnitId,
                subunitName = model.subunitName,
                subunitNameBn = model.subunitNameBn,
                isActive = model.isActive,
                isReportable = model.isReportable,
                latitude = model.latitude,
                longitude = model.longitude,
            };
            _repoPoliceSubUnit.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult DeletePoliceSubUnit(int id)
        {
            bool response;

            try
            {
                _repoPoliceSubUnit.Delete(_repoPoliceSubUnit.Get(id));
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