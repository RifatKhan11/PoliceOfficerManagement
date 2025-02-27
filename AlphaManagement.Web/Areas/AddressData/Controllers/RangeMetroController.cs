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
    public class RangeMetroController : Controller
    {
        private readonly LangGenerate<RangeMetroLn> _lang;
        private readonly IRepository<RangeMetro> _repoRangeMetro;
        public RangeMetroController(IHostingEnvironment hostingEnvironment, IRepository<RangeMetro> repoRangeMetro)
        {
            _lang = new LangGenerate<RangeMetroLn>(hostingEnvironment.ContentRootPath);
            _repoRangeMetro = repoRangeMetro;
        }


        public IActionResult Index()
        {
            var model = new RangeMetroViewModel
            {
                fLang = _lang.PerseLang("AddressData/RangeMetroEN.json", "AddressData/RangeMetroBN.json", Request.Cookies["lang"]),
                rangeMetroList = _repoRangeMetro.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] RangeMetroViewModel model)
        {
            var Obj = new RangeMetro
            {
                Id = model.RangeMetroId,
                rangeMetroName = model.rangeMetroName,
                rangeMetroNameBn = model.rangeMetroNameBn,
                //isActive = model.isActive,
                //latitude = model.latitude,
                //longitude = model.longitude,

            };
            if (model.RangeMetroId>0)
            {
                _repoRangeMetro.Update(Obj);
            }
            else
            {
                _repoRangeMetro.Insert(Obj);
            }
            

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteRangeMetro(int id)
        {
            bool response;

            try
            {
                _repoRangeMetro.Delete(_repoRangeMetro.Get(id));
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