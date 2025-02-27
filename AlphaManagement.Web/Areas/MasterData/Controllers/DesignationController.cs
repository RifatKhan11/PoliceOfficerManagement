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
    public class DesignationController : Controller
    {

        private readonly LangGenerate<DesignationLn> _lang;
        private readonly IRepository<Designation> _repoDesignation;
  
        public DesignationController(IHostingEnvironment hostingEnvironment, IRepository<Designation> repoDesignation )
        {
            _lang = new LangGenerate<DesignationLn>(hostingEnvironment.ContentRootPath);
            _repoDesignation = repoDesignation;
        }
        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new DesignationViewModel
            {
                fLang = _lang.PerseLang("MasterData/DesignationEN.json", "MasterData/DesignationBN.json", Request.Cookies["lang"]),
                designationList = _repoDesignation.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] DesignationViewModel model)
        {
            var Obj = new Designation
            {
                Id=model.DesignationId,
                designationCode = model.designationCode,
                designationName = model.designationName,
                designationNameBN = model.designationNameBN,
                shortName = model.shortName,
                shortOrder = model.shortOrder,

            };

            if (model.DesignationId > 0)
            {

                _repoDesignation.Update(Obj);
            }
            else
            {

                _repoDesignation.Insert(Obj);
            }
           

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteDesignation(int id)
        //{

        //    _repoDesignation.Delete(_repoDesignation.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteDesignation(int id)
        {
            bool response;

            try
            {
                _repoDesignation.Delete(_repoDesignation.Get(id));
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