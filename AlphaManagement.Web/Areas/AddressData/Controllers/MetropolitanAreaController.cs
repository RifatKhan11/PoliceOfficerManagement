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
    public class MetropolitanAreaController : Controller
    {
        private readonly LangGenerate<MetropolitanAreaLn> _lang;
        private readonly IRepository<MetropolitanArea> _repoMetropolitanArea;
        private readonly IAddressServices _addressServices;
        public MetropolitanAreaController(IHostingEnvironment hostingEnvironment, IAddressServices addressServices, IRepository<MetropolitanArea> repoMetropolitanArea)
        {
            _lang = new LangGenerate<MetropolitanAreaLn>(hostingEnvironment.ContentRootPath);
            _repoMetropolitanArea = repoMetropolitanArea;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new MetropolitanAreaViewModel
            {
                fLang = _lang.PerseLang("AddressData/MetropolitanAreaEN.json", "AddressData/MetropolitanAreaBN.json", Request.Cookies["lang"]),
                metropolitanAreaList =await _addressServices.GetAllMetroPolitanArea(),
                DistrictList =await _addressServices.GetAllDistrict()

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] MetropolitanAreaViewModel model)
        {
            var Obj = new MetropolitanArea
            {
                Id = model.MetropolitanAreaId,
                areaName = model.areaName,
                areaNameBn = model.areaNameBn,
                districtId = model.districtId,
                shortOrder = model.shortOrder

            };
            if (model.MetropolitanAreaId > 0)
            {
                _repoMetropolitanArea.Update(Obj);
            }
            else
            {
                _repoMetropolitanArea.Insert(Obj);
            }

         

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteMetropolitanArea(int id)
        {
            bool response;

            try
            {
                _repoMetropolitanArea.Delete(_repoMetropolitanArea.Get(id));
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