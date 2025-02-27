using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Entity.Organogram;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")] 
    public class OrganizationTypeController : Controller
    {
        private readonly IRepository<OrganizationType> _repoResult;

        public OrganizationTypeController(IRepository<OrganizationType> _repoResult)
        {
            this._repoResult = _repoResult;
        }

        public IActionResult Index()
        {
            OrganizationTypeViewModel model = new OrganizationTypeViewModel
            {
                organizationTypes = _repoResult.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] OrganizationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.organizationTypes = _repoResult.GetAll();
                return View(model);
            }

            OrganizationType data = new OrganizationType
            {
                Id = model.organizationTypeId,
                nameEN = model.organizationTypeName,
                nameBN = model.organizationTypeNameBN,
                remarks = model.remarks,
            };

            if (model.organizationTypeId > 0)
            {

                _repoResult.Update(data);
            }
            else
            {
                _repoResult.Insert(data);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult Delete(int id)
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