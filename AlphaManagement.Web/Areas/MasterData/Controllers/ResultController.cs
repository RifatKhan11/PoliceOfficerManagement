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
    public class ResultController : Controller
    {
        private readonly LangGenerate<ResultLn> _lang;
        private readonly IRepository<Result> _repoResult;

        public ResultController(IHostingEnvironment hostingEnvironment, IRepository<Result> repoResult)
        {
            _lang = new LangGenerate<ResultLn>(hostingEnvironment.ContentRootPath);
            _repoResult = repoResult;
        }

        public IActionResult Index()
        {
            var model = new ResultViewModel
            {
                fLang = _lang.PerseLang("MasterData/ResultEN.json", "MasterData/ResultBN.json", Request.Cookies["lang"]),
                results = _repoResult.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] ResultViewModel model)
        {
            var Obj = new Result
            {
                Id= model.ResultId,
                resultName = model.resultName,
                resultNameBn = model.resultNameBn,
                resultShortName = model.resultShortName,
                resultMaxValue = model.resultMaxValue,
            };

            if (model.ResultId > 0)
            {

                _repoResult.Update(Obj);
            }
            else
            {
                _repoResult.Insert(Obj);
            }
        

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteResult(int id)
        //{

        //    _repoResult.Delete(_repoResult.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteResult(int id)
        {
            bool response;

            try
            {
                _repoResult.Delete(_repoResult.Get(id));
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