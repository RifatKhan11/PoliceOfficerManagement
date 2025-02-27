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
    public class RelationController : Controller
    {
        private readonly LangGenerate<RelationLn> _lang;
        private readonly IRepository<Relation> _repoRelation;
        public RelationController(IHostingEnvironment hostingEnvironment, IRepository<Relation> repoRelation)
        {
            _lang = new LangGenerate<RelationLn>(hostingEnvironment.ContentRootPath);
            _repoRelation = repoRelation;
        }
        //[Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new RelationViewModel
            {
                fLang = _lang.PerseLang("MasterData/RelationEN.json", "MasterData/RelationBN.json", Request.Cookies["lang"]),
                relations = _repoRelation.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] RelationViewModel model)
        {
            var Obj = new Relation
            {
                Id=model.RelationId,
                relationName = model.relationName,
                relationNameBn = model.relationNameBn,
                relationShortName = model.relationShortName
            };

            if (model.RelationId > 0)
            {

                _repoRelation.Update(Obj);
            }
            else
            {
                _repoRelation.Insert(Obj);
            }
         

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteRelation(int id)
        //{

        //    _repoRelation.Delete(_repoRelation.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteRelation(int id)
        {
            bool response;

            try
            {
                _repoRelation.Delete(_repoRelation.Get(id));
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