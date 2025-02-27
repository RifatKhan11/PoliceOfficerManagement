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
    public class CountryController : Controller
    {
        private readonly LangGenerate<CountryLn> _lang;
        private readonly IRepository<Country> _repoCountry;
        public CountryController(IHostingEnvironment hostingEnvironment, IRepository<Country> repoCountry)
        {
            _lang = new LangGenerate<CountryLn>(hostingEnvironment.ContentRootPath);
            _repoCountry = repoCountry;
        }


        public IActionResult Index()
        {
            var model = new CountryViewModel
            {
                fLang = _lang.PerseLang("AddressData/CountryEN.json", "AddressData/CountryBN.json", Request.Cookies["lang"]),
                countryList = _repoCountry.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] CountryViewModel model)
        {
            var Obj = new Country
            {
                Id = model.CountryId,
                countryCode = model.countryCode,
                countryName = model.countryName,
                countryNameBn = model.countryNameBn,
                shortName = model.shortName,

            };
            if (model.CountryId> 0)
            {
                _repoCountry.Update(Obj);
            }
            else
            {
                _repoCountry.Insert(Obj);
            }
          

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteCountry(int id)
        {
            bool response;

            try
            {
                _repoCountry.Delete(_repoCountry.Get(id));
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