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
    public class ThanaController : Controller
    {
        private readonly LangGenerate<ThanaLn> _lang;
        private readonly IRepository<Thana> _repoThana;
        private readonly IRepository<District> _repoDistrict;
        private readonly IRepository<RangeMetro> _repoRangeMetro;
        private readonly IAddressServices _addressServices;
        public ThanaController(IHostingEnvironment hostingEnvironment, IAddressServices addressServices, IRepository<Thana> repoThana, IRepository<District> repoDistrict, IRepository<RangeMetro> repoRangeMetro)
        {
            _lang = new LangGenerate<ThanaLn>(hostingEnvironment.ContentRootPath);
            _repoThana = repoThana;
            _repoDistrict = repoDistrict;
            _repoRangeMetro = repoRangeMetro;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new ThanaViewModel
            {
                fLang = _lang.PerseLang("AddressData/ThanaEN.json", "AddressData/ThanaBN.json", Request.Cookies["lang"]),
                thanaList =await _addressServices.GetAllThana(),
                districtList = _repoDistrict.GetAll(),
                rangeMetroList = _repoRangeMetro.GetAll(),
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] ThanaViewModel model)
        {
            var Obj = new Thana
            {
                Id = model.ThanaId,
                thanaCode = model.thanaCode,
                thanaName = model.thanaName,
                thanaNameBn = model.thanaNameBn,
                shortName = model.shortName,
                districtId = model.districtId,
                rangeMetroId=model.rangeMetroId
            };
            if (model.ThanaId>0)
            {
                _repoThana.Update(Obj);
            }
            else
            {
                _repoThana.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteThana(int id)
        {
            bool response;

            try
            {
                _repoThana.Delete(_repoThana.Get(id));
                response = true;
            }
            catch (Exception ex)
            {
                response = false;
                throw;
            }

            return Json(response);
        }
    }
}