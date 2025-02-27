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
    public class TrainingInstituteController : Controller
    {

        private readonly LangGenerate<TrainingInstituteLn> _lang;
        private readonly IRepository<TrainingInstitute> _repoTrainingInstitute;
        public TrainingInstituteController(IHostingEnvironment hostingEnvironment, IRepository<TrainingInstitute> repoTrainingInstitute)
        {
            _lang = new LangGenerate<TrainingInstituteLn>(hostingEnvironment.ContentRootPath);
            _repoTrainingInstitute = repoTrainingInstitute;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new TrainingInstituteViewModel
            {
                fLang = _lang.PerseLang("MasterData/TrainingInstituteEN.json", "MasterData/TrainingInstituteBN.json", Request.Cookies["lang"]),
                trainingInstitutes = _repoTrainingInstitute.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] TrainingInstituteViewModel model)
        {
            var Obj = new TrainingInstitute
            {
                Id=model.TrainingInstituteId,
                trainingInstituteName = model.trainingInstituteName,
                trainingInstituteNameBn = model.trainingInstituteNameBn,
                trainingInstituteShortName = model.trainingInstituteShortName,
                shortOrder = model.shortOrder

            };

            if (model.TrainingInstituteId > 0)
            {

                _repoTrainingInstitute.Update(Obj);
            }
            else
            {
                _repoTrainingInstitute.Insert(Obj);
            }
       

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteTrainingInstitute(int id)
        //{

        //    _repoTrainingInstitute.Delete(_repoTrainingInstitute.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteTrainingInstitute(int id)
        {
            bool response;

            try
            {
                _repoTrainingInstitute.Delete(_repoTrainingInstitute.Get(id));
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