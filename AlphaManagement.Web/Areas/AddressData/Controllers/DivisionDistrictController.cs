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
    public class DivisionDistrictController : Controller
    {
        private readonly LangGenerate<DivisionDistrictLn> _lang;
        private readonly IRepository<DivisionDistrict> _repoDivisionDistrict;
        private readonly IRepository<RangeMetro> _repoRangeMetro;
        private readonly IAddressServices _addressServices;
        public DivisionDistrictController(IHostingEnvironment hostingEnvironment, IAddressServices addressServices, IRepository<DivisionDistrict> repoDivisionDistrict, IRepository<RangeMetro> repoRangeMetro)
        {
            _lang = new LangGenerate<DivisionDistrictLn>(hostingEnvironment.ContentRootPath);
            _repoDivisionDistrict = repoDivisionDistrict;
            _repoRangeMetro = repoRangeMetro;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new DivisionDistrictViewModel
            {
                fLang = _lang.PerseLang("AddressData/DivisionDistrictEN.json", "AddressData/DivisionDistrictBN.json", Request.Cookies["lang"]),
                divisionDistrictList =await _addressServices.GetAllDivisionDistrict(),
                rangeMetroList = _repoRangeMetro.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] DivisionDistrictViewModel model)
        {
            var Obj = new DivisionDistrict
            {
                Id = model.DivisionDistrictId,
                rangeMetroId=model.rangeMetroId,
                divisionDistrictName =model.divisionDistrictName,
                divisionDistrictNameBn =model.divisionDistrictNameBn,
                //isActive = model.isActive,
                //latitude = model.latitude,
                //longitude = model.longitude,

            };
            _repoDivisionDistrict.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult DeleteDivisionDistrict(int id)
        {
            bool response;

            try
            {
                _repoDivisionDistrict.Delete(_repoDivisionDistrict.Get(id));
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