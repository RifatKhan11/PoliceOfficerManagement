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
    public class NationalIdentityTypeController : Controller
    {
        private readonly LangGenerate<NationalIdentityTypeLn> _lang;
        private readonly IRepository<NationalIdentityType> _repoNationalIdentityType;
        public NationalIdentityTypeController(IHostingEnvironment hostingEnvironment, IRepository<NationalIdentityType> repoNationalIdentityType)
        {
            _lang = new LangGenerate<NationalIdentityTypeLn>(hostingEnvironment.ContentRootPath);
            _repoNationalIdentityType = repoNationalIdentityType;
        }


        public IActionResult Index()
        {
            var model = new NationalIdentityTypeViewModel
            {
                fLang = _lang.PerseLang("AddressData/NationalIdentityTypeEN.json", "AddressData/NationalIdentityTypeBN.json", Request.Cookies["lang"]),
                nationalIdentityTypeList = _repoNationalIdentityType.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] NationalIdentityTypeViewModel model)
        {
            var Obj = new NationalIdentityType
            {
                Id = model.NationalIdentityTypeId,
                nationalIdentityName = model.nationalIdentityName,
                nationalIdentityNameBn = model.nationalIdentityNameBn,
                //shortOrder = model.shortOrder
            };
            
            if (model.NationalIdentityTypeId > 0)
            {
                _repoNationalIdentityType.Update(Obj);
            }
            else
            {
                _repoNationalIdentityType.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteNationalIdentityType(int id)
        {
            bool response;

            try
            {
                _repoNationalIdentityType.Delete(_repoNationalIdentityType.Get(id));
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