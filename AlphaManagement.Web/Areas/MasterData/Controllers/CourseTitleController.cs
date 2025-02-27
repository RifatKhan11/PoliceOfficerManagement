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

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class CourseTitleController : Controller
    {
        private readonly LangGenerate<CourseTitleLn> _lang;
        private readonly IRepository<CourseTitle> _repoCourseTitle;
        public CourseTitleController(IHostingEnvironment hostingEnvironment, IRepository<CourseTitle> repoCourseTitle)
        {
            _lang = new LangGenerate<CourseTitleLn>(hostingEnvironment.ContentRootPath);
            _repoCourseTitle = repoCourseTitle;
        }
        public IActionResult Index()
        {
            var model = new CourseTitleViewModel
            {
                fLang = _lang.PerseLang("MasterData/CourseTitleEN.json", "MasterData/CourseTitleBN.json", Request.Cookies["lang"]),
                courseTitles = _repoCourseTitle.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] CourseTitleViewModel model)
        {
            var Obj = new CourseTitle
            {
                Id=model.CourseTitleId,
                nameEN = model.nameEN,
                nameBN = model.nameBN,
                remarks = model.remarks,
            };
     
            if (model.CourseTitleId > 0)
            {
                _repoCourseTitle.Update(Obj);
            }
            else
            {
                _repoCourseTitle.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteCourseTitle(int id)
        //{
        //    _repoCourseTitle.Delete(_repoCourseTitle.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteCourseTitle(int id)
        {
            bool response;

            try
            {
                _repoCourseTitle.Delete(_repoCourseTitle.Get(id));
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