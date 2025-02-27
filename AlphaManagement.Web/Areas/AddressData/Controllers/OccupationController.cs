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
    public class OccupationController : Controller
    {
        private readonly LangGenerate<OccupationLn> _lang;
        private readonly IRepository<Occupation> _repoOccupation;
        public OccupationController(IHostingEnvironment hostingEnvironment, IRepository<Occupation> repoOccupation)
        {
            _lang = new LangGenerate<OccupationLn>(hostingEnvironment.ContentRootPath);
            _repoOccupation = repoOccupation;
        }


        public IActionResult Index()
        {
            var model = new OccupationViewModel
            {
                fLang = _lang.PerseLang("AddressData/OccupationEN.json", "AddressData/OccupationBN.json", Request.Cookies["lang"]),
                occupationList = _repoOccupation.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] OccupationViewModel model)
        {
            var Obj = new Occupation
            {
                Id = model.OccupationId,
                name = model.name,
                nameBn = model.nameBn,
                //imagePath = model.imagePath
                       
            };
            if (model.OccupationId > 0)
            {
                _repoOccupation.Update(Obj);
            }
            else
            {
                _repoOccupation.Insert(Obj);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteOccupation(int id)
        {
            bool response;

            try
            {
                _repoOccupation.Delete(_repoOccupation.Get(id));
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