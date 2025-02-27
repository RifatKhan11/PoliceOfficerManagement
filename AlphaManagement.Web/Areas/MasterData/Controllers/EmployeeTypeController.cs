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
    public class EmployeeTypeController : Controller
    {
        private readonly LangGenerate<EmployeeTypeLn> _lang;
        private readonly IRepository<EmployeeType> _repoEmployeeType;
        public EmployeeTypeController(IHostingEnvironment hostingEnvironment, IRepository<EmployeeType> repoEmployeeType)
        {
            _lang = new LangGenerate<EmployeeTypeLn>(hostingEnvironment.ContentRootPath);
            _repoEmployeeType = repoEmployeeType;
        }
        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new EmployeeTypeViewModel
            {
                fLang = _lang.PerseLang("MasterData/EmployeeTypeEN.json", "MasterData/EmployeeTypeBN.json", Request.Cookies["lang"]),
                employeeTypes = _repoEmployeeType.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] EmployeeTypeViewModel model)
        {
            var Obj = new EmployeeType
            {
                Id=model.EmployeeTypeId,
                empType = model.empType,
                empTypeBn = model.empTypeBn,
                shortName = model.shortName
            };


            if (model.EmployeeTypeId > 0)
            {

                _repoEmployeeType.Update(Obj);
            }
            else
            {

                _repoEmployeeType.Insert(Obj);
            }
            

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteEmployeeType(int id)
        //{

        //    _repoEmployeeType.Delete(_repoEmployeeType.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteEmployeeType(int id)
        {
            bool response;

            try
            {
                _repoEmployeeType.Delete(_repoEmployeeType.Get(id));
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