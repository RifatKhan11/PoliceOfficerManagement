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
    public class DivisionController : Controller
    {
        private readonly LangGenerate<DivisionLn> _lang;
        private readonly IRepository<Division> _repoDivision;
        private readonly IRepository<Country> _repoCountry;
        private readonly IAddressServices _addressServices;
        public DivisionController(IHostingEnvironment hostingEnvironment, IAddressServices addressServices, IRepository<Division> repoDivision, IRepository<Country> repoCountry)
        {
            _lang = new LangGenerate<DivisionLn>(hostingEnvironment.ContentRootPath);
            _repoDivision = repoDivision;
            _repoCountry = repoCountry;
            _addressServices = addressServices;
        }


        public async Task<IActionResult> Index()
        {
            var model = new DivisionViewModel
            {
                fLang = _lang.PerseLang("AddressData/DivisionEN.json", "AddressData/DivisionBN.json", Request.Cookies["lang"]),
                divisionList =await _addressServices.GetAllDivision(),
                countryList = _repoCountry.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] DivisionViewModel model)
        {
            var Obj = new Division
            {
                Id = model.DivisionId,
                divisionCode = model.divisionCode,
                divisionName = model.divisionName,
                divisionNameBn = model.divisionNameBn,
                shortName = model.shortName,
                countryId = model.countryId
            };
           
            if (model.DivisionId > 0)
            {
                _repoDivision.Update(Obj);
            }
            else
            {
                _repoDivision.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteDivision(int id)
        {
            bool response;

            try
            {
                _repoDivision.Delete(_repoDivision.Get(id));
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