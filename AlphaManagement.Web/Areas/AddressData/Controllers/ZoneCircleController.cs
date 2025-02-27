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
    public class ZoneCircleController : Controller
    {
        private readonly LangGenerate<ZoneCircleLn> _lang;
        private readonly IRepository<ZoneCircle> _repoZoneCircle;
        private readonly IRepository<DivisionDistrict> _repoDivisionDistrict;
        private readonly IAddressServices _addressServices;
        public ZoneCircleController(IHostingEnvironment hostingEnvironment, IAddressServices addressServices, IRepository<ZoneCircle> repoZoneCircle, IRepository<DivisionDistrict> repoDivisionDistrict)
        {
            _lang = new LangGenerate<ZoneCircleLn>(hostingEnvironment.ContentRootPath);
            _repoZoneCircle = repoZoneCircle;
            _repoDivisionDistrict = repoDivisionDistrict;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new ZoneCircleViewModel
            {
                fLang = _lang.PerseLang("AddressData/ZoneCircleEN.json", "AddressData/ZoneCircleBN.json", Request.Cookies["lang"]),
                zoneCircleList =await _addressServices.GetAllZoneCircle(),
                divisionDistrictList = _repoDivisionDistrict.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] ZoneCircleViewModel model)
        {
            var Obj = new ZoneCircle
            {
                Id = model.ZoneCircleId,
                divisionDistrictId= model.divisionDistrictId,
                zoneName= model.zoneName,
                zoneNameBn= model.zoneNameBn,


            };
            if (model.ZoneCircleId>0)
            {
                _repoZoneCircle.Update(Obj);
            }
            else
            {
                _repoZoneCircle.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteZoneCircle(int id)
        {
            bool response;

            try
            {
                _repoZoneCircle.Delete(_repoZoneCircle.Get(id));
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