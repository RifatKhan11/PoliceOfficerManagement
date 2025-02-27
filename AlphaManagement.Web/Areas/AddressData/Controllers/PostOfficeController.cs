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
    public class PostOfficeController : Controller
    {
        private readonly LangGenerate<PostOfficeLn> _lang;
        private readonly IRepository<PostOffice> _repoPostOffice;
        private readonly IRepository<District> _repoDistrict;
        private readonly IAddressServices _addressServices;

        public PostOfficeController(IHostingEnvironment hostingEnvironment,
            IRepository<PostOffice> repoPostOffice,
            IRepository<District> repoDistrict,
            IAddressServices addressServices
            )
        {
            _lang = new LangGenerate<PostOfficeLn>(hostingEnvironment.ContentRootPath);
            _repoPostOffice = repoPostOffice;
            _repoDistrict = repoDistrict;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new PostOfficeViewModel
            {
                fLang = _lang.PerseLang("AddressData/PostOfficeEN.json", "AddressData/PostOfficeBN.json", Request.Cookies["lang"]),
                postOfficeList =await _addressServices.GetDistrictWiseThanaName(),
                districtList = _repoDistrict.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] PostOfficeViewModel model)
        {
            var Obj = new PostOffice
            {
                Id = model.PostOfficeId,
                districtId = model.districtId,
                thanaId = model.thanaId,
                postalCode = model.postalCode,
                postalName = model.postalName,
                postalShortName = model.postalShortName,
                postalNameBn = model.postalNameBn,

            };
            if (model.PostOfficeId>0)
            {
                _repoPostOffice.Update(Obj);
            }
            else
            {
                _repoPostOffice.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult DeletePostOffice(int id)
        {
            bool response;

            try
            {
                _repoPostOffice.Delete(_repoPostOffice.Get(id));
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