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
    public class UnionWardController : Controller
    {
        private readonly LangGenerate<UnionWardLn> _lang;
        private readonly IRepository<UnionWard> _repoUnionWard;
        private readonly IRepository<Thana> _repoThana;
        private readonly IRepository<District> _repoDistrict;
        private readonly IRepository<Division> _repoDivision;
        private readonly IAddressServices addressServices;
        public UnionWardController(IHostingEnvironment hostingEnvironment, IRepository<UnionWard> repoUnionWard, IRepository<Thana> repoThana, IRepository<District> repoDistrict, IRepository<Division> repoDivision, IAddressServices addressServices)
        {
            _lang = new LangGenerate<UnionWardLn>(hostingEnvironment.ContentRootPath);
            _repoUnionWard = repoUnionWard;
            _repoThana = repoThana;
            _repoDistrict = repoDistrict;
            _repoDivision = repoDivision;
            this.addressServices = addressServices;
        }


        public IActionResult Index()
        {
            var model = new UnionWardViewModel
            {
                fLang = _lang.PerseLang("AddressData/UnionWardEN.json", "AddressData/UnionWardBN.json", Request.Cookies["lang"]),
                divisions = _repoDivision.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index([FromForm] UnionWardViewModel model)
        {
            UnionWard unionWard = new UnionWard
            {
                Id = (int)model.unionWardId,
                districtsId = model.districtId,
                thanaId = (int)model.thanaId,
                unionCode = model.unionCode,
                unionName = model.nameEnglish,
                unionNameBn = model.nameBangla,
                isActive = model.isActive
            };
            if (model.unionWardId > 0)
            {
                _repoUnionWard.Update(unionWard);
            }
            else
            {
                _repoUnionWard.Insert(unionWard);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult DeleteUnionWardById(int Id)
        {
            _repoUnionWard.Delete(_repoUnionWard.Get(Id));
            return Json(true);
        }

        [Route("global/api/GetAllUnionWard")]
        [HttpGet]
        public async Task<IActionResult> GetAllUnionWard()
        {
            return Json(await addressServices.GetAllUnionWard());
        }

        [Route("global/api/GetUpazillaByDistrictId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUpazillaByDistrictId(int Id)
        {
            return Json(await addressServices.GetThanasByDistrictId(Id));
        }

    }
}