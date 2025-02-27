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
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Domain.MasterDataServices.Interfaces;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class RelDegreeSubjectController : Controller
    {
        private readonly LangGenerate<RelDegreeSubjectLn> _lang;
        private readonly IRepository<RelDegreeSubject> _repoRelDegreeSubject;
        private readonly IRepository<Degree> _repoDegree;
        private readonly IRepository<Subject> _repoSubject;
        private readonly IDegreeService _degreeService;
        public RelDegreeSubjectController(IHostingEnvironment hostingEnvironment, IRepository<RelDegreeSubject> repoRelDegreeSubject, IRepository<Degree> repoDegree, IRepository<Subject> repoSubject, IDegreeService degreeService)
        {
            _lang = new LangGenerate<RelDegreeSubjectLn>(hostingEnvironment.ContentRootPath);
            _repoRelDegreeSubject = repoRelDegreeSubject;
            _repoDegree = repoDegree;
            _repoSubject = repoSubject;
            _degreeService = degreeService;
        }
        public IActionResult Index()
        {
            var model = new RelDegreeSubjectViewModel
            {

                fLang = _lang.PerseLang("MasterData/RelDegreeSubjectEN.json", "MasterData/RelDegreeSubjectBN.json", Request.Cookies["lang"]),
                relDegreeSubjects = _repoRelDegreeSubject.GetAll(),
                subjects= _repoSubject.GetAll(),
                degreeList=_repoDegree.GetAll()

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] RelDegreeSubjectViewModel model)
        {
            var Obj = new RelDegreeSubject
            {
                Id=model.RelDegreeSubjectId,
                degreeId = model.degreeId,
                subjectId = model.subjectId,
            };

            if (model.RelDegreeSubjectId > 0)
            {

                _repoRelDegreeSubject.Update(Obj);
            }
            else
            {
                _repoRelDegreeSubject.Insert(Obj);
            }


            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteRelDegreeSubject(int id)
        //{

        //    _repoRelDegreeSubject.Delete(_repoRelDegreeSubject.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteRelDegreeSubject(int id)
        {
            bool response;

            try
            {
                _repoRelDegreeSubject.Delete(_repoRelDegreeSubject.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }


        public async Task<ActionResult> SaveSubjectGroup([FromForm] GroupSubjectViewModel model)
        {
            try
            {
                Subject subject = new Subject
                {
                    subjectName = model.SName
                };
                int subjectId = await _degreeService.SaveSubjectInfo(subject);

                RelDegreeSubject relDegreeSubject = new RelDegreeSubject
                {
                    degreeId = Convert.ToInt32(model.SdegreeId),
                    subjectId = subjectId
                };
               // _repoRelDegreeSubject.Insert(relDegreeSubject);

                int relDegreeSubjectId = await _degreeService.SaveRelDegreeSubject(relDegreeSubject);
                var data = new { SubjectName = subject.subjectName, Id = relDegreeSubjectId };


                return Json(new { Success = true, data });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message= "Error occurred. Please try again." });
            }
        }

        #region APISetting
        [Route("global/api/relDegreeSubjects/{id}")]
        [HttpGet]
        public IActionResult RelDegreeSubjects(int Id)
        {
           var RelDegreeSubjects = _repoRelDegreeSubject.GetAll();
            return Json(RelDegreeSubjects.Where(X => X.degreeId == Id).ToList());
        }
        #endregion
    }
}