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
    public class DistrictController : Controller
    {
        private readonly LangGenerate<DistrictLn> _lang;
        private readonly IRepository<District> _repoDistrict;
        private readonly IRepository<Division> _repoDivision;
        private readonly IAddressServices _addressServices;
        
        public DistrictController(IHostingEnvironment hostingEnvironment, IAddressServices addressServices, IRepository<District> repoDistrict, IRepository<Division> repoDivision)
        {
            _lang = new LangGenerate<DistrictLn>(hostingEnvironment.ContentRootPath);
            _repoDistrict = repoDistrict;
            _repoDivision = repoDivision;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new DistrictViewModel
            {
                fLang = _lang.PerseLang("AddressData/DistrictEN.json", "AddressData/DistrictBN.json", Request.Cookies["lang"]),
                districtList =await _addressServices.GetAllDistrict(),
                divisionList = _repoDivision.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] DistrictViewModel model)
        {
            var Obj = new District
            {
                Id = model.DistrictId,
                districtCode = model.districtCode,
                districtName = model.districtName,
                districtNameBn = model.districtNameBn,
                shortName = model.shortName,
                divisionId = model.divisionId
            };
           

            if (model.DistrictId > 0)
            {
                _repoDistrict.Update(Obj);
            }
            else
            {
                _repoDistrict.Insert(Obj);
            }


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteDistrict(int id)
        {
            bool response;

            try
            {
                _repoDistrict.Delete(_repoDistrict.Get(id));
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