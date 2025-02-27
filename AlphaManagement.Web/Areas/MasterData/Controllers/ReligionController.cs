using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;
using AlphaManagement.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using AlphaManagement.Web.Areas.MasterData.Models;
using Microsoft.AspNetCore.Authorization;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class ReligionController : Controller
    {

        private readonly LangGenerate<ReligionLn> _lang;
        private readonly IRepository<Religion> _repoReligion;
        public ReligionController(IHostingEnvironment hostingEnvironment, IRepository<Religion> repoReligion)
        {
            _lang = new LangGenerate<ReligionLn>(hostingEnvironment.ContentRootPath);
            _repoReligion = repoReligion;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new ReligionViewModel
            {
                fLang = _lang.PerseLang("MasterData/ReligionEN.json", "MasterData/ReligionBN.json", Request.Cookies["lang"]),
                religions = _repoReligion.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] ReligionViewModel model)
        {
            var Obj = new Religion
            {
                Id= model.ReligionId,
                name = model.name,
                nameBn = model.nameBn,
                shortName = model.shortName,
          

            };

            if (model.ReligionId > 0)
            {

                _repoReligion.Update(Obj);
            }
            else
            {
                _repoReligion.Insert(Obj);
            }

     

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteReligion(int id)
        //{

        //    _repoReligion.Delete(_repoReligion.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteReligion(int id)
        {
            bool response;

            try
            {
                _repoReligion.Delete(_repoReligion.Get(id));
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