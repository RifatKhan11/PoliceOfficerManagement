using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.MasterData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class BankController : Controller
    {
        
        private readonly IRepository<Banks> _repoBanks;
        public BankController(IRepository<Banks> repoBanks)
        {

            _repoBanks = repoBanks;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new BanksViewModel
            {

                banks = _repoBanks.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] BanksViewModel model)
        {
            var Obj = new Banks
            {
                Id = model.BanksId,
                bankName = model.bankName,
                bankNameBn = model.bankNameBn,
                shortOrder = model.shortOrder

            };

            if (model.BanksId > 0)
            {

                _repoBanks.Update(Obj);
            }
            else
            {
                _repoBanks.Insert(Obj);
            }


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteBanks(int id)
        {
            bool response;

            try
            {
                _repoBanks.Delete(_repoBanks.Get(id));
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