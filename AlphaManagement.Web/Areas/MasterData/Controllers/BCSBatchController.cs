using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.MasterData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class BCSBatchController : Controller
    {
        private readonly IRepository<BCSBatch> _repoBCSBatch;
        private readonly IRepository<PHQTRType> _repoPHQTRType;
        public BCSBatchController(IRepository<BCSBatch> repoBCSBatch, IRepository<PHQTRType> repoPHQTRType)
        {

            _repoBCSBatch = repoBCSBatch;
            _repoPHQTRType = repoPHQTRType;
        }

        //[Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new BCSBatchViewModel
            {
                bCSBatches = _repoBCSBatch.GetAll()
            };

            return View(model);
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult TrType()
        {
            var model = new BCSBatchViewModel
            {
                pHQTRTypes = _repoPHQTRType.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] BCSBatchViewModel model)
        {
            var Obj = new BCSBatch
            {
                Id = model.BCSBatchId,
                batchName = model.batchName,
                batchNameBn = model.batchNameBn,
                shortOrder = model.shortOrder

            };

            if (model.BCSBatchId > 0)
            {

                _repoBCSBatch.Update(Obj);
            }
            else
            {
                _repoBCSBatch.Insert(Obj);
            }


            return RedirectToAction(nameof(Index));
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TrType([FromForm] BCSBatchViewModel model)
        {
            var Obj = new PHQTRType
            {
                Id = model.BCSBatchId,
                trTypeName = model.batchName,
                trTypeNameBn = model.batchNameBn,
                shortOrder = model.shortOrder

            };

            if (model.BCSBatchId > 0)
            {

                _repoPHQTRType.Update(Obj);
            }
            else
            {
                _repoPHQTRType.Insert(Obj);
            }


            return RedirectToAction(nameof(TrType));
        }


        [HttpPost]
        public IActionResult DeleteBCSBatch(int id)
        {
            bool response;

            try
            {
                _repoBCSBatch.Delete(_repoBCSBatch.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }


        [HttpPost]
        public IActionResult DeleteTrType(int id)
        {
            bool response;

            try
            {
                _repoPHQTRType.Delete(_repoPHQTRType.Get(id));
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