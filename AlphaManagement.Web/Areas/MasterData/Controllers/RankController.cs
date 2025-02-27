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
    public class RankController : Controller
    {
        private readonly LangGenerate<RankLn> _lang;
        private readonly IRepository<Rank> _repoRank;
        public RankController(IHostingEnvironment hostingEnvironment, IRepository<Rank> repoRank)
        {
            _lang = new LangGenerate<RankLn>(hostingEnvironment.ContentRootPath);
            _repoRank = repoRank;
        }

        //[Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new RankViewModel
            {
                fLang = _lang.PerseLang("MasterData/RankEN.json", "MasterData/RankBN.json", Request.Cookies["lang"]),
                Ranks = _repoRank.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] RankViewModel model)
        {
            var Obj = new Rank
            {
                Id=model.RankId,
                rankCode = model.rankCode,
                rankName = model.rankName,
                rankNameBN = model.rankNameBN,
                shortName = model.shortName,
                shortOrder = model.shortOrder
            };
          


            if (model.RankId > 0)
            {

                _repoRank.Update(Obj);
            }
            else
            {
                _repoRank.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteRank(int id)
        //{

        //    _repoRank.Delete(_repoRank.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteRank(int id)
        {
            bool response;

            try
            {
                _repoRank.Delete(_repoRank.Get(id));
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