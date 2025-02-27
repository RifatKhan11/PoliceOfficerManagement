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
    public class VillageController : Controller
    {
        private readonly LangGenerate<VillageLn> _lang;
        private readonly IRepository<Village> _repoVillage;
        private readonly IRepository<Thana> _repoThana;
        private readonly IRepository<District> _repoDistrict;
        private readonly IRepository<UnionWard> _repoUnionWard;
        public VillageController(IHostingEnvironment hostingEnvironment, IRepository<Village> repoVillage,IRepository<Thana> repoThana, IRepository<District> repoDistrict, IRepository<UnionWard> repoUnionWard)
        {
            _lang = new LangGenerate<VillageLn>(hostingEnvironment.ContentRootPath);
            _repoVillage = repoVillage;
            _repoThana = repoThana;
            _repoDistrict = repoDistrict;
            _repoUnionWard = repoUnionWard;
        }


        public IActionResult Index()
        {
            var model = new VillageViewModel
            {
                fLang = _lang.PerseLang("AddressData/VillageEN.json", "AddressData/VillageBN.json", Request.Cookies["lang"]),
                villageList = _repoVillage.GetAll(),
                thanaList = _repoThana.GetAll(),
                districtList = _repoDistrict.GetAll(),
                unionWardList= _repoUnionWard.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] VillageViewModel model)
        {
            var Obj = new Village
            {
                Id = model.VillageId,
                unionWardId = model.unionWardId,
                thanaId = model.thanaId,
                districtsId = model.districtsId,
                villageCode = model.villageCode,
                villageName = model.villageName,
                villageNameBn = model.villageNameBn,
                shortName = model.shortName,
                isActive = model.isActive,
                latitude = model.latitude,
                longitude = model.longitude,

            };
            _repoVillage.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult DeleteVillage(int id)
        {
            bool response;

            try
            {
                _repoVillage.Delete(_repoVillage.Get(id));
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