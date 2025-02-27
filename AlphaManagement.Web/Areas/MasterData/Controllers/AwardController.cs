using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.MasterData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class AwardController : Controller
    {
        private readonly IRepository<Award> _repoAward;
        public AwardController(IRepository<Award> repoAward)
        {

            _repoAward = repoAward;
        }

        [Authorize(Roles = "Super Admin,Admin")]
        public IActionResult Index()
        {
            var model = new AwardViewModel
            {

                awards = _repoAward.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] AwardViewModel model)
        {
            var Obj = new Award
            {
                Id = model.AwardId,
                awardName = model.awardName,
                awardNameBn = model.awardNameBn,
                shortOrder = model.shortOrder,
                awardShortName = model.awardShortName

            };

            if (model.AwardId > 0)
            {

                _repoAward.Update(Obj);
            }
            else
            {
                _repoAward.Insert(Obj);
            }


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteAward(int id)
        {
            bool response;

            try
            {
                _repoAward.Delete(_repoAward.Get(id));
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