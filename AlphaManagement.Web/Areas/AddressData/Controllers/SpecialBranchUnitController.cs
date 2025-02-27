using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.AddressData.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.AddressData.Controllers
{
    [Area("AddressData")]
    public class SpecialBranchUnitController : Controller
    {
        private readonly IRepository<SpecialBranchUnit> _specialBranch;

        public SpecialBranchUnitController(IHostingEnvironment hostingEnvironment, IRepository<SpecialBranchUnit> specialBranch)
        {

            _specialBranch = specialBranch;
        }


        public IActionResult Index()
        {
            var model = new SpecialBranchUnitViewModel
            {
                specialBranchUnits = _specialBranch.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] SpecialBranchUnitViewModel model)
        {
            var Obj = new SpecialBranchUnit
            {
                Id = model.branchId,
                branchUnitName=model.branchUnitName,
                branchUnitNameBN=model.branchUnitNameBN,
                branchCode=model.branchCode,
               
            };

            if (model.branchId > 0)
            {
                _specialBranch.Update(Obj);
            }
            else
            {
                _specialBranch.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteBranch(int id)
        {
            bool response;

            try
            {
                _specialBranch.Delete(_specialBranch.Get(id));
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