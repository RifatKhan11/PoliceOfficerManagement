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
using AlphaManagement.Domain.MasterDataServices.Interfaces;

namespace AlphaManagement.Web.Areas.AddressData.Controllers
{
    [Area("AddressData")]
    public class PoliceUnitController : Controller
    {
        private readonly LangGenerate<PoliceUnitLn> _lang;
        private readonly IRepository<PoliceUnit> _repoPoliceUnit;
        private readonly IRepository<RangeMetro> _repoRangeMetro;
        private readonly IRepository<PoliceThana> _repoPoliceThana;
        private readonly IAddressServices _addressServices;
        public PoliceUnitController(IHostingEnvironment hostingEnvironment,
            IAddressServices addressServices,
            IRepository<PoliceUnit> repoPoliceUnit,
            IRepository<RangeMetro> repoRangeMetro,
            IRepository<PoliceThana> repoPoliceThana
            
            )
        {
            _lang = new LangGenerate<PoliceUnitLn>(hostingEnvironment.ContentRootPath);
            _repoPoliceUnit = repoPoliceUnit;
            _repoRangeMetro = repoRangeMetro;
            _repoPoliceThana = repoPoliceThana;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new PoliceUnitViewModel
            {
                fLang = _lang.PerseLang("AddressData/PoliceUnitEN.json", "AddressData/PoliceUnitBN.json", Request.Cookies["lang"]),
                policeUnitList =await _addressServices.GetAllPoliceUnit(),
                rangeMetroList = _repoRangeMetro.GetAll(),
                policeThanaList = _repoPoliceThana.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] PoliceUnitViewModel model)
        {
            var Obj = new PoliceUnit
            {
                Id = model.PoliceUnitId,
                rangeMetroId = model.rangeMetroId,
                policeThanaId = model.policeThanaId,
                unitName = model.unitName,
                unitNameBn = model.unitNameBn,
                //isActive = model.isActive,
                //isReportable = model.isReportable,
                //latitude = model.latitude,
                //longitude = model.longitude,
            };
            if (model.PoliceUnitId > 0)
            {
                _repoPoliceUnit.Update(Obj);
            }
            else
            {
                _repoPoliceUnit.Insert(Obj);
            }

       

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeletePoliceUnit(int id)
        {
            bool response;

            try
            {
                _repoPoliceUnit.Delete(_repoPoliceUnit.Get(id));
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