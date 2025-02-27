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
    public class LanguageController : Controller
    {

        private readonly LangGenerate<LanguageLn> _lang;
        private readonly IRepository<Language> _repoLanguage;
        public LanguageController(IHostingEnvironment hostingEnvironment, IRepository<Language> repoLanguage)
        {
            _lang = new LangGenerate<LanguageLn>(hostingEnvironment.ContentRootPath);
            _repoLanguage = repoLanguage;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new LanguageViewModel
            {
                
                fLang = _lang.PerseLang("MasterData/LanguageEN.json", "MasterData/LanguageBN.json", Request.Cookies["lang"]),
                LanguageList = _repoLanguage.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] LanguageViewModel model)
        {
            var Obj = new Language
            {
                Id=model.LanguageId,
                languageName = model.languageName,
                languageNameBn = model.languageNameBn,
                languageShortName = model.languageShortName,
                shortOrder = model.shortOrder


            };


            if (model.LanguageId > 0)
            {

                _repoLanguage.Update(Obj);
            }
            else
            {

                _repoLanguage.Insert(Obj);
            }
            

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteLanguage(int id)
        //{

        //    _repoLanguage.Delete(_repoLanguage.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteLanguage(int id)
        {
            bool response;

            try
            {
                _repoLanguage.Delete(_repoLanguage.Get(id));
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