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
    public class AddressCategoryController : Controller
    {
        private readonly LangGenerate<AddressCategoryLn> _lang;
        private readonly IRepository<AddressCategory> _repoAddressCategory;
        public AddressCategoryController(IHostingEnvironment hostingEnvironment, IRepository<AddressCategory> repoAddressCategory)
        {
            _lang = new LangGenerate<AddressCategoryLn>(hostingEnvironment.ContentRootPath);
            _repoAddressCategory = repoAddressCategory;
        }


        public IActionResult Index()
        {
            var model = new AddressCategoryViewModel
            {
                fLang = _lang.PerseLang("AddressData/AddressCategoryEN.json", "AddressData/AddressCategoryBN.json", Request.Cookies["lang"]),
                addressCategoryList = _repoAddressCategory.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] AddressCategoryViewModel model)
        {


            var Obj = new AddressCategory
            {
                Id=model.AddressCategoryId,
                name=model.name

            };
            if (model.AddressCategoryId>0)
            {
                _repoAddressCategory.Update(Obj);
            }
            else
            {
                _repoAddressCategory.Insert(Obj);
            }
            

            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public IActionResult DeleteAddressCategory(int id)
        {
            bool response;

            try
            {
                _repoAddressCategory.Delete(_repoAddressCategory.Get(id));
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