using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.MasterData.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class OrganaizationController : Controller
    {
        private readonly IRepository<Organization> _organization;

        public OrganaizationController(IHostingEnvironment hostingEnvironment, IRepository<Organization> organization)
        {
            _organization = organization;
        }

        public IActionResult Index()
        {
            var model = new OrganizationViewModel
            {
               organizations=_organization.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] OrganizationViewModel model)
        {
            var Obj = new Organization
            {
                Id = model.orgId,
                organizationType = model.organizationType,
                organizationName = model.organizationName,
                organizationNameBn = model.organizationNameBn,
            };

            if (model.orgId > 0)
            {

                _organization.Update(Obj);
            }
            else
            {
                _organization.Insert(Obj);
            }


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteOrganization(int id)
        {
            bool response;

            try
            {
                _organization.Delete(_organization.Get(id));
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