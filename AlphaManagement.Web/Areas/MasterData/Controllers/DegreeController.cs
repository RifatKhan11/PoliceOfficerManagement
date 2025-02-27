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
    public class DegreeController : Controller
    {
        private readonly LangGenerate<DegreeLn> _lang;
        private readonly IRepository<Degree> _repoDegree;
        private readonly IRepository<LevelofEducation> _repoLevelofEducation;
        public DegreeController(IHostingEnvironment hostingEnvironment, IRepository<Degree> repoDegree, IRepository<LevelofEducation> repoLevelofEducation)
        {
            _lang = new LangGenerate<DegreeLn>(hostingEnvironment.ContentRootPath);
            _repoDegree = repoDegree;
            _repoLevelofEducation = repoLevelofEducation;
        }

       // [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new DegreeViewModel
            {
                LevelofEducations= _repoLevelofEducation.GetAll(),
                fLang = _lang.PerseLang("MasterData/DegreeEN.json", "MasterData/DegreeBN.json", Request.Cookies["lang"]),
                degreeList = _repoDegree.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] DegreeViewModel model)
        {
            var Obj = new Degree
            {
                Id=model.DegreeId,
                degreeNameBn = model.degreeNameBn,
                degreeName = model.degreeName,
                degreeShortName = model.degreeShortName,
                levelofeducationId = model.levelofeducationId, 
                isDelete=model.shortOrder
            };
            

            if (model.DegreeId > 0)
            {
                _repoDegree.Update(Obj);
            }
            else
            {
                _repoDegree.Insert(Obj);
            }


            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteDegree(int id)
        //{
        //    _repoDegree.Delete(_repoDegree.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        public IActionResult DeleteDegree(int id)
        {
            bool response;

            try
            {
                _repoDegree.Delete(_repoDegree.Get(id));
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