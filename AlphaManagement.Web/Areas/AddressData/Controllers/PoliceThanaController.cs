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
    public class PoliceThanaController : Controller
    {
        private readonly LangGenerate<PoliceThanaLn> _lang;
        private readonly IRepository<PoliceThana> _repoPoliceThana;
        private readonly IRepository<RangeMetro> _repoRangeMetro;
        private readonly IRepository<DivisionDistrict> _repoDivisionDistrict;
        private readonly IRepository<ZoneCircle> _repoZoneCircle;
        private readonly IRepository<Thana> _repoThana;
        private readonly IAddressServices _addressServices;
        public PoliceThanaController(IHostingEnvironment hostingEnvironment, IAddressServices addressServices, IRepository<PoliceThana> repoPoliceThana, IRepository<RangeMetro> repoRangeMetro,
            IRepository<DivisionDistrict> repoDivisionDistrict,
            IRepository<ZoneCircle> repoZoneCircle,
            IRepository<Thana> repoThana
            )
        {
            _lang = new LangGenerate<PoliceThanaLn>(hostingEnvironment.ContentRootPath);
            _repoPoliceThana = repoPoliceThana;
           _repoRangeMetro = repoRangeMetro;
            _repoDivisionDistrict = repoDivisionDistrict;
            _repoZoneCircle = repoZoneCircle;
            _repoThana = repoThana;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new PoliceThanaViewModel
            {
                fLang = _lang.PerseLang("AddressData/PoliceThanaEN.json", "AddressData/PoliceThanaBN.json", Request.Cookies["lang"]),
                policeThanaList =await _addressServices.GetAllPoliceThana(),
                rangeMetroList = _repoRangeMetro.GetAll(),
                divisionDistrictList = _repoDivisionDistrict.GetAll(),
                zoneCircleList = _repoZoneCircle.GetAll(),
                thanaList = _repoThana.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] PoliceThanaViewModel model)
        {
            var Obj = new PoliceThana
            {
                Id = model.PoliceThanaId,
                rangeMetroId = model.rangeMetroId,
                divisionDistrictId = model.divisionDistrictId,
                zoneCircleId = model.zoneCircleId,
                upazillaId = model.upazillaId,
                policeThanaName = model.policeThanaName,
                policeThanaNameBn = model.policeThanaNameBn,
                //isActive = model.isActive,
                //isReportable = model.isReportable,
                //latitude = model.latitude,
                //longitude = model.longitude

            };
            _repoPoliceThana.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeletePoliceThana(int id)
        {
            bool response;

            try
            {
                _repoPoliceThana.Delete(_repoPoliceThana.Get(id));
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