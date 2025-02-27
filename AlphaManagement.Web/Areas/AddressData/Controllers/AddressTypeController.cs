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
    public class AddressTypeController : Controller
    {
        private readonly LangGenerate<AddressTypeLn> _lang;
        private readonly IRepository<AddressType> _repoAddressType;
        public AddressTypeController(IHostingEnvironment hostingEnvironment, IRepository<AddressType> repoAddressType)
        {
            _lang = new LangGenerate<AddressTypeLn>(hostingEnvironment.ContentRootPath);
            _repoAddressType = repoAddressType;
        }


        public IActionResult Index()
        {
            var model = new AddressTypeViewModel
            {
                fLang = _lang.PerseLang("AddressData/AddressTypeEN.json", "AddressData/AddressTypeBN.json", Request.Cookies["lang"]),
                addressTypeList = _repoAddressType.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] AddressTypeViewModel model)
        {
            var Obj = new AddressType
            {
                Id = model.AddressTypeId,
                typeName=model.typeName
            };
            if (model.AddressTypeId > 0)
            {
                _repoAddressType.Update(Obj);
            }
            else
            {
                _repoAddressType.Insert(Obj);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteAddressType(int id)
        {
            bool response;

            try
            {
                _repoAddressType.Delete(_repoAddressType.Get(id));
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