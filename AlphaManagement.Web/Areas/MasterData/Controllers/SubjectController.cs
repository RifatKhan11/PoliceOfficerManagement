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
    public class SubjectController : Controller
    {


        private readonly LangGenerate<SubjectLn> _lang;
        private readonly IRepository<Subject> _repoSubject;
        public SubjectController(IHostingEnvironment hostingEnvironment, IRepository<Subject> repoSubject)
        {
            _lang = new LangGenerate<SubjectLn>(hostingEnvironment.ContentRootPath);
            _repoSubject = repoSubject;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index(int id)
        {
            var model = new SubjectViewModel
            {
                subject = _repoSubject.Get(id),
                fLang = _lang.PerseLang("MasterData/SubjectEN.json", "MasterData/SubjectBN.json", Request.Cookies["lang"]),
                subjects = _repoSubject.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] SubjectViewModel model)
        {
            var Obj = new Subject
            {
                Id=model.SubjectId,
                subjectName = model.subjectName,
                subjectNameBn = model.subjectNameBn,
                subjectShortName = model.subjectShortName,
                shortOrder = model.shortOrder,

            };

            if (model.SubjectId > 0)
            {

                _repoSubject.Update(Obj);
            }
            else
            {
                _repoSubject.Insert(Obj);
            }
           

            return RedirectToAction(nameof(Index));
        }
               
        //public IActionResult DeleteSubject(int id)
        //{

        //    _repoSubject.Delete(_repoSubject.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteSubject(int id)
        {
            bool response;

            try
            {
                _repoSubject.Delete(_repoSubject.Get(id));
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