using System;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Web.Areas.MasterData.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class AnulipiController : Controller
    {
        private readonly IRepository<AnulipiList> _anulipi;
        private readonly IEmployeeService _employeeService;

        public AnulipiController(
            IHostingEnvironment hostingEnvironment, 
            IRepository<AnulipiList> anulipi, 
            IEmployeeService employeeService)
        {
            _anulipi = anulipi;
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var model = new AnulipiViewModel
            {
                anulipiLists = _anulipi.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] AnulipiViewModel model)
        {
            var Obj = new AnulipiList
            {
                Id = model.anulipiId,
                copyName = model.copyName,
                copyNameBn = model.copyNameBn,
            };

            if (model.anulipiId > 0)
            {

                _anulipi.Update(Obj);
            }
            else
            {
                _anulipi.Insert(Obj);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteAnulipi(int id)
        {
            bool response;
            try
            {
                _anulipi.Delete(_anulipi.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> SaveNewAnulipi(string anulipiName)
        {
            var check =await _employeeService.GetAnulipiFromListByName(anulipiName);
            var Obj = new AnulipiList
            {
                Id = 0,
                copyName = anulipiName,
                copyNameBn = anulipiName,
            };
            if (check != null)
            {
                Obj.Id = check.Id;
            }
            return Json(Obj);
        }
    }
}