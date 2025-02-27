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
    public class TrainingCategoryController : Controller
    {

        private readonly LangGenerate<TrainingCategoryLn> _lang;
        private readonly IRepository<TrainingCategory> _repoTrainingCategory;
        public TrainingCategoryController(IHostingEnvironment hostingEnvironment, IRepository<TrainingCategory> repoTrainingCategory)
        {
            _lang = new LangGenerate<TrainingCategoryLn>(hostingEnvironment.ContentRootPath);
            _repoTrainingCategory = repoTrainingCategory;
        }
        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new TrainingCategoryViewModel
            {
                fLang = _lang.PerseLang("MasterData/TrainingCategoryEN.json", "MasterData/TrainingCategoryBN.json", Request.Cookies["lang"]),
                trainingCategories = _repoTrainingCategory.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] TrainingCategoryViewModel model)
        {
            var Obj = new TrainingCategory
            {
                Id=model.TrainingCategoryId,
                trainingCategoryName = model.trainingCategoryName,
                trainingCategoryNameBn = model.trainingCategoryNameBn,
                trainingCategoryShortName = model.trainingCategoryShortName,
                shortOrder = model.shortOrder,

            };

            if (model.TrainingCategoryId > 0)
            {

                _repoTrainingCategory.Update(Obj);
            }
            else
            {
                _repoTrainingCategory.Insert(Obj);
            }

          

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteTrainingCategory(int id)
        //{

        //    _repoTrainingCategory.Delete(_repoTrainingCategory.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        public IActionResult DeleteTrainingCategory(int id)
        {
            bool response;

            try
            {
                _repoTrainingCategory.Delete(_repoTrainingCategory.Get(id));
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