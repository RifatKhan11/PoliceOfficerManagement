using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using AlphaManagement.Web.Areas.MasterData.Models.Lang;
using AlphaManagement.Web.Areas.MasterData.Models;
using AlphaManagement.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class ActivityStatusController : Controller
    {
        private readonly LangGenerate<ActivityStatusLn> _lang;
        private readonly IRepository<ActivityStatus> _repoActivityStatus;
        public ActivityStatusController(IHostingEnvironment hostingEnvironment, IRepository<ActivityStatus> repoActivityStatus)
        {
            _lang = new LangGenerate<ActivityStatusLn>(hostingEnvironment.ContentRootPath);
            _repoActivityStatus = repoActivityStatus;
        }
        public IActionResult Index()
        {
            var model = new ActivityStatusViewModel
            {
                fLang = _lang.PerseLang("MasterData/ActivityStatusEN.json", "MasterData/ActivityStatusBN.json", Request.Cookies["lang"]),
                activityStatusList = _repoActivityStatus.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] ActivityStatusViewModel model)
        {
            var Obj = new ActivityStatus
            {
               Id= model.ActivityStatusId,
               shortName=model.shortName,
               statusNameBn=model.statusNameBn,
               statusName=model.statusName

            };
            if (model.ActivityStatusId>0)
            {
                _repoActivityStatus.Update(Obj);
            }
            else
            {
                _repoActivityStatus.Insert(Obj);
            }
            


            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteActivityStatus(int id)
        //{
        //    _repoActivityStatus.Delete(_repoActivityStatus.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        public IActionResult DeleteActivityStatus(int id)
        {
            bool response;

            try
            {
                _repoActivityStatus.Delete(_repoActivityStatus.Get(id));
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