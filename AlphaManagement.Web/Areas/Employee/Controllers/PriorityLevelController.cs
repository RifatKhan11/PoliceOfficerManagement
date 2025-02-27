using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class PriorityLevelController : Controller
    {
        private readonly IRepository<SpecialSkillType> _specialSkillType;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<District> _repoDistrict;
        private readonly IRepository<SpecialBranchUnit> _repoSpecialBranchUnit;
        private readonly IRepository<PriorityLevelType> _repoPriorityLevelType;
        private readonly IRepository<PostingPriorityLevel> _repoPostingPriorityLevel;
        private readonly IRepository<MedicalMainCategory> _RepoMedicalMainCategory;
        private readonly IRepository<MedicalSubCategory> _RepoMedicalSubCategory;
        private readonly IEmployeeService _employeeService;

        public PriorityLevelController(
            IRepository<SpecialSkillType> _specialSkillType,
            IRepository<Rank> _repoRank,
            IRepository<District> _repoDistrict,
            IRepository<SpecialBranchUnit> _repoSpecialBranchUnit,
            IRepository<PriorityLevelType> _repoPriorityLevelType,
            IRepository<PostingPriorityLevel> _repoPostingPriorityLevel,
            IRepository<MedicalMainCategory> _repoMedicalMainCategory,
            IRepository<MedicalSubCategory> _repoMedicalSubCategory,
            IEmployeeService _employeeService
            )
        {
            this._specialSkillType = _specialSkillType;
            this._repoRank = _repoRank;
            this._repoDistrict = _repoDistrict;
            this._repoSpecialBranchUnit = _repoSpecialBranchUnit;
            this._repoPriorityLevelType = _repoPriorityLevelType;
            this._repoPostingPriorityLevel = _repoPostingPriorityLevel;
            _RepoMedicalMainCategory = _repoMedicalMainCategory;
            _RepoMedicalSubCategory = _repoMedicalSubCategory;
            this._employeeService = _employeeService;
        }

        #region PriorityLevel
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            PriorityLevelViewModel model = new PriorityLevelViewModel
            {
                specialSkillTypes = _specialSkillType.GetAll(),
                ranks = _repoRank.GetAll(),
                districts = _repoDistrict.GetAll(),
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                priorityLevelTypes = _repoPriorityLevelType.GetAll(),
                postingPriorityLevels = await _employeeService.GetPostingPriorityLevel()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PriorityLevelViewModel model)
        {
            try
            {
                PostingPriorityLevel level = new PostingPriorityLevel
                {
                    Id = model.prorityLevelId,
                    prorityLevel = model.prorityLevel,
                    rankId = model.rankId,
                    specialBranchUnitId = model.specialBranchUnitId,
                    sortOrder = model.sortOrder,
                    status = 0,
                    districtId = model.districtId,
                    employeeInfoId = model.employeeInfoId,
                    priorityLevelTypeId = model.priorityLevelTypeId,
                    ruleDescription = model.ruleDescription,
                    specialSkillTypeId = model.specialSkillTypeId,
                };
                await _employeeService.SavePostingPriorityLevel(level);
                return Json("success");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeletePriorityLevelbyId(int Id)
        {
            try
            {
                var id = await _employeeService.DeletePostingPriorityLevelById(Id);
                return Json(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region PriorityLevelType
        [HttpGet]
        public async Task<IActionResult> PriorityLevelTypes()
        {
            PriorityLevelViewModel model = new PriorityLevelViewModel
            {
                priorityLevelTypes = _repoPriorityLevelType.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PriorityLevelTypes(PriorityLevelViewModel model)
        {
            try
            {
                PriorityLevelType level = new PriorityLevelType
                {
                    Id = (int)model.priorityLevelTypeId,
                    name = model.name,
                    nameBn = model.nameBn,                  
                    sortOrder = model.sortOrder,
               
                };
                if (model.priorityLevelTypeId > 0)
                {
                    _repoPriorityLevelType.Update(level);
                }
                else
                {
                 _repoPriorityLevelType.Insert(level);
                }              
                return Json("success");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeletePriorityLevelTypebyId(int Id)
         {
            try
            {
                var id = await _employeeService.DeletePriorityLevelTypebyId(Id);
                return Json(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region specialSkillTypes
        [HttpGet]
        public async Task<IActionResult> SpecialSkillTypes()
        {
            PriorityLevelViewModel model = new PriorityLevelViewModel
            {
                specialSkillTypes = _specialSkillType.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SpecialSkillTypes(PriorityLevelViewModel model)
        {
            try
            {
                SpecialSkillType level = new SpecialSkillType
                {
                    Id = (int)model.Id,
                    name = model.name,
                    nameBn = model.nameBn,
                    sortOrder = model.sortOrder,

                };
                if (model.Id > 0)
                {
                    _specialSkillType.Update(level);
                }
                else
                {
                    _specialSkillType.Insert(level);
                }
                return Json("success");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpecialSkillTypebyId(int Id)
        {
            try
            {
                var id = await _employeeService.DeleteSpecialSkillById(Id);
                return Json(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region medicalMainCategories
        [HttpGet]
        public async Task<IActionResult> MedicalMainCategories()
        {
            PriorityLevelViewModel model = new PriorityLevelViewModel
            {
              medicalMainCategories   = _RepoMedicalMainCategory.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MedicalMainCategories(PriorityLevelViewModel model)
        {
            try
            {
                MedicalMainCategory level = new MedicalMainCategory
                {
                    Id = (int)model.Id,
                    name = model.name,
                    nameBn = model.nameBn,
                    sortOrder = model.sortOrder,

                };
                if (model.Id > 0)
                {
                    _RepoMedicalMainCategory.Update(level);
                }
                else
                {
                    _RepoMedicalMainCategory.Insert(level);
                }
                return Json("success");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteMedicalMainCategoriesbyId(int Id)
        {
            try
            {
                var id = await _employeeService.DeleteMedicalMainCategoryById(Id);
                return Json(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region medicalSubCategories
        [HttpGet]
        public async Task<IActionResult> MedicalSubCategories()
        {
            PriorityLevelViewModel model = new PriorityLevelViewModel
            {
                medicalMainCategories = _RepoMedicalMainCategory.GetAll(),
                medicalSubCategories   = await _employeeService.getMedicalSubCategoty()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MedicalSubCategories(PriorityLevelViewModel model)
        {
            try
            {
                MedicalSubCategory level = new MedicalSubCategory
                {
                    Id = (int)model.Id,
                    name = model.name,
                    nameBn = model.nameBn,
                    medicalMainCategoryId = model.medicalMainCategoryId,
                    description = model.description,
                    sortOrder = model.sortOrder                   
                };

                if (model.Id > 0)
                {
                    _RepoMedicalSubCategory.Update(level);
                }
                else
                {
                    _RepoMedicalSubCategory.Insert(level);
                }
                return Json("success");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteMedicalSubCategorybyId(int Id)
        {
            try
            {
                var id = await _employeeService.DeletemedicalSubCategoryById(Id);
                return Json(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region API
        [Route("global/api/getEmployeedataById")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var data = await _employeeService.GetEmployeeInfoSingleById(id);
            return Json(data);
        }

        [Route("global/api/PostingPriorityLevelbyId")]
        [HttpGet]
        public async Task<IActionResult> GetPostingPriorityLevelbyIdById(int Id)
        {
            var data = await _employeeService.GetPostingPriorityLevelById(Id);
            return Json(data);
        }

        [Route("global/api/PriorityLevelTypebyId")]
        [HttpGet]
        public async Task<IActionResult> GetPriorityLevelTypebyIdById(int Id)
        {
            var data =  _repoPriorityLevelType.Get(Id);
            return Json(data);
        }

        [Route("global/api/SpecialSkillsbyId")]
        [HttpGet]
        public async Task<IActionResult> GetSpecialSkillsById(int Id)
        {
            var data =  _specialSkillType.Get(Id);
            return Json(data);
        }
        
        [Route("global/api/MedicalMainCategoriesbyId")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalMainCategoriesbyIdById(int Id)
        {
            var data =  _RepoMedicalMainCategory.Get(Id);
            return Json(data);
        }
        [Route("global/api/MedicalSubCategoriesbyId")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalSubCategoriesbyIdById(int Id)
        {
            var data =  _RepoMedicalSubCategory.Get(Id);
            return Json(data);
        }



        #endregion

    }
}