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
    public class LevelofEducationController : Controller
    {
        private readonly LangGenerate<LevelofEducationLn> _lang;
        private readonly IRepository<LevelofEducation> _repoLevelofEducation;
        public LevelofEducationController(IHostingEnvironment hostingEnvironment, IRepository<LevelofEducation> repoLevelofEducation)
        {
            _lang = new LangGenerate<LevelofEducationLn>(hostingEnvironment.ContentRootPath);
            _repoLevelofEducation = repoLevelofEducation;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new LevelofEducationViewModel
            {
                fLang = _lang.PerseLang("MasterData/LevelofEducationEN.json", "MasterData/LevelofEducationBN.json", Request.Cookies["lang"]),
                levelofEducations = _repoLevelofEducation.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] LevelofEducationViewModel model)
        {
            var Obj = new LevelofEducation
            {
                Id = model.LevelofEducationId,
                levelofeducationName =model.levelofeducationName,
                levelofeducationNameBn = model.levelofeducationNameBn,
                shortOrder = model.shortOrder,
            };

            if (model.LevelofEducationId > 0)
            {

                _repoLevelofEducation.Update(Obj);
            }
            else
            {
                _repoLevelofEducation.Insert(Obj);
            }
     

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteLevelofEducation(int id)
        //{
        //    _repoLevelofEducation.Delete(_repoLevelofEducation.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteLevelofEducation(int id)
        {
            bool response;

            try
            {
                _repoLevelofEducation.Delete(_repoLevelofEducation.Get(id));
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