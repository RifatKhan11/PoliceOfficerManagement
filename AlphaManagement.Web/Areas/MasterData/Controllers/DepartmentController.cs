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
    public class DepartmentController : Controller
    {
        private readonly LangGenerate<DepartmentLn> _lang;
        private readonly IRepository<Department> _repoDepartment;
        private readonly IRepository<Offense> repoOffense;
        private readonly IRepository<NaturalPunishment> repoNPunishment;

        public DepartmentController(IHostingEnvironment hostingEnvironment,
            IRepository<Department> repoDepartment,
            IRepository<Offense> repoOffense,
            IRepository<NaturalPunishment> repoNPunishment
            )
        {
            _lang = new LangGenerate<DepartmentLn>(hostingEnvironment.ContentRootPath);
            _repoDepartment = repoDepartment;
            this.repoOffense = repoOffense;
            this.repoNPunishment = repoNPunishment;
        }

        [Authorize(Roles = "Super Admin")]
        public IActionResult Index()
        {
            var model = new DepartmentViewModel
            {
                fLang = _lang.PerseLang("MasterData/DepartmentEN.json", "MasterData/DepartmentBN.json", Request.Cookies["lang"]),
                departments = _repoDepartment.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] DepartmentViewModel model)
        {
            var Obj = new Department
            {
                Id = model.DepartmentId,
                deptCode = model.deptCode,
                deptName = model.deptName,
                deptNameBn = model.deptNameBn,
                shortName = model.shortName,
                startDate = model.startDate,
            };

            if (model.DepartmentId > 0)
            {

                _repoDepartment.Update(Obj);
            }
            else
            {

                _repoDepartment.Insert(Obj);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteDepartment(int id)
        {
            bool response;

            try
            {
                _repoDepartment.Delete(_repoDepartment.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }

        #region Offence
        [HttpGet]
        public async Task<IActionResult> Offences()
        {
            try
            {
                var model = new DepartmentViewModel
                {
                    fLang = _lang.PerseLang("MasterData/DepartmentEN.json", "MasterData/DepartmentBN.json", Request.Cookies["lang"]),
                    offenses = repoOffense.GetAll(),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<IActionResult> Offences(DepartmentViewModel model)
        {
            try
            {
                Offense offense = new Offense
                {
                    Id = model.offenseId,
                    offense = model.name,
                    description = model.description,
                    shortOrder = model.shortOrder,
                };
                if (model.offenseId > 0)
                {
                    repoOffense.Update(offense);
                }
                else
                {
                    repoOffense.Insert(offense);
                }
                return Json("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetOffenceById(int Id)
        {
            try
            {
                var id = repoOffense.Get(Id);
                return Json(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> DeleteOffenceById(int Id)
        {
            try
            {
                var empData = repoOffense.Get(Id);
                repoOffense.Delete(empData);
                return Json(empData.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region NaturalPunishments
        [HttpGet]
        public async Task<IActionResult> NaturalPunishments()
        {
            try
            {
                var model = new DepartmentViewModel
                {
                    fLang = _lang.PerseLang("MasterData/DepartmentEN.json", "MasterData/DepartmentBN.json", Request.Cookies["lang"]),
                    naturalPunishments = repoNPunishment.GetAll(),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<IActionResult> NaturalPunishments(DepartmentViewModel model)
        {
            try
            {
                NaturalPunishment punishment  = new NaturalPunishment
                {
                    Id = model.naturalPunishmentId,
                    name = model.name,
                    description = model.description,
                    shortOrder = model.shortOrder,
                };
                if (model.naturalPunishmentId > 0)
                {
                    repoNPunishment.Update(punishment);
                }
                else
                {
                    repoNPunishment.Insert(punishment);
                }
                return Json("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetNaturalPunishmentsById(int Id)
        {
            try
            {
                var id = repoNPunishment.Get(Id);
                return Json(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> DeleteNaturalPunishmentsById(int Id)
        {
            try
            {
                var empData = repoNPunishment.Get(Id);
                repoNPunishment.Delete(empData);
                return Json(empData.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}