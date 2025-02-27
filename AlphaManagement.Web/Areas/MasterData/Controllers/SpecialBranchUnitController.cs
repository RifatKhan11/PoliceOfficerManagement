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
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Domain.EmployeeService.interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class SpecialBranchUnitController : Controller
    {

        private readonly LangGenerate<SpecialBranchUnitLn> _lang;
        private readonly IRepository<SpecialBranchUnit> _repoSpecialBranchUnit;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<PostInUnit> _postInUnit;
        private readonly IRepository<Section> _section;
        private readonly IRepository<District> _repoDistrict;
        private readonly IAddressServices addressServices;
        private readonly IEmployeeService _employeeService;
        private readonly ISpecialBranchUnitServices specialBranchUnitServices;


        public SpecialBranchUnitController(IHostingEnvironment hostingEnvironment, IRepository<SpecialBranchUnit> repoSpecialBranchUnit, IRepository<District> repoDistrict, IRepository<Rank> repoRank, IAddressServices addressServices, IRepository<PostInUnit> postInUnit, IRepository<Section> section, ISpecialBranchUnitServices specialBranchUnitServices,IEmployeeService _employeeService)
        {
            _lang = new LangGenerate<SpecialBranchUnitLn>(hostingEnvironment.ContentRootPath);
            _repoSpecialBranchUnit = repoSpecialBranchUnit;
            _repoRank = repoRank;
            _postInUnit = postInUnit;
            _section = section;
            _repoDistrict = repoDistrict;
            this.addressServices = addressServices;
            this.specialBranchUnitServices = specialBranchUnitServices;
            this._employeeService = _employeeService;
        }

        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Index()
        {
            var model = new SpecialBranchUnitViewModel
            {
                fLang = _lang.PerseLang("MasterData/SpecialBranchUnitEN.json", "MasterData/SpecialBranchUnitBN.json", Request.Cookies["lang"]),
                //specialBranchUnits = _repoSpecialBranchUnit.GetAll()

               specialBranchUnits = await specialBranchUnitServices.GetAllSpecialBranchUnit(),
                districts = _repoDistrict.GetAll().OrderBy(x=>x.districtName)
            };
            return View(model);
        }

        public async Task<IActionResult> PostInUnit()
        {
            var model = new SpecialBranchUnitViewModel
            {
                fLang = _lang.PerseLang("MasterData/SpecialBranchUnitEN.json", "MasterData/SpecialBranchUnitBN.json", Request.Cookies["lang"]),
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                ranks = _repoRank.GetAll(),
                postInUnits = await addressServices.GetPostInUnit(),
            };
            return View(model);
        }

         public async Task<IActionResult> PostInUnitNameOfPost(int rankId=0,int unitId=0)
        {
            var model = new SpecialBranchUnitViewModel
            {
                fLang = _lang.PerseLang("MasterData/SpecialBranchUnitEN.json", "MasterData/SpecialBranchUnitBN.json", Request.Cookies["lang"]),
                specialBranchUnits = _repoSpecialBranchUnit.GetAll(),
                ranks = _repoRank.GetAll(),
                sections = await addressServices.GetSectionByFilter(rankId,unitId),
            };
            return View(model);
        }

        [Route("global/api/GetPostInUnitByRankAndUnit/{UnitId}/{rankId}")]
        [HttpGet]
        public async Task<IActionResult> GetPostInUnitByRankAndUnit(int UnitId, int rankId)
        {
            return Json(await addressServices.GetPostInUnitByRankAndUnit(UnitId, rankId));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] SpecialBranchUnitViewModel model)
        {
            var Obj = new SpecialBranchUnit
            {
                Id=model.SpecialBranchUnitId,
                branchUnitName = model.branchUnitName,
                branchUnitNameBN = model.branchUnitNameBN,
                branchCode = model.branchCode,
                shortOrder = model.shortOrder,
                isdefault = model.isdefault==null?0:model.isdefault,
                isParent = model.isparent,
                specialBranchUnitId = model.headUnitId,
                districtsId=model.districtId
            };

            if (model.SpecialBranchUnitId > 0)
            {

                _repoSpecialBranchUnit.Update(Obj);
            }
            else
            {
                _repoSpecialBranchUnit.Insert(Obj);
            }
     

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostInUnit([FromForm] SpecialBranchUnitViewModel model)
        {
            var Obj = new PostInUnit
            {
                specialBranchUnitId = model.SpecialBranchUnitId,
                Id =(int) model.postInUnitId,
                rankId = model.rankId,
                numOfPost = model.noOfPost,
            };

            if (model.postInUnitId > 0)
            {
                _postInUnit.Update(Obj);
            }
            else
            {
                _postInUnit.Insert(Obj);
            }
            
            return RedirectToAction(nameof(PostInUnit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Section([FromForm] SpecialBranchUnitViewModel model)
        {
            var Obj = new Section
            {
                specialBranchUnitId = model.SpecialBranchUnitId,
                Id =(int) model.SectionitId,
                rankId = model.rankId,
                Name = model.Name,
                NameBN = model.NameBN,
            };

            if (model.SectionitId > 0)
            {
                _section.Update(Obj);
            }
            else
            {
                _section.Insert(Obj);
            }
            return RedirectToAction("PostInUnitNameOfPost", new {rankId=0,unitId = model.SpecialBranchUnitId });
            //return RedirectToAction(nameof(PostInUnitNameOfPost{ }));
        }




        //public IActionResult DeleteSpecialBranchUnit(int id)
        //{

        //    _repoSpecialBranchUnit.Delete(_repoSpecialBranchUnit.Get(id));
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteSpecialBranchUnit(int id)
        {
            bool response;

            try
            {
                _repoSpecialBranchUnit.Delete(_repoSpecialBranchUnit.Get(id));
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
        public IActionResult DeletePostInUnit(int id)
        {
            bool response;

            try
            {
                _postInUnit.Delete(_postInUnit.Get(id));
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
        public IActionResult Deletesection(int id)
        {
            bool response;

            try
            {
                _section.Delete(_section.Get(id));
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
        public async Task<IActionResult> GetSpecialBranchUnit(int id)
        {
            
                var specialBranchUnit = await specialBranchUnitServices.GetSpecialBranchUnitById(id);
             //   specialBranchUnit.specialBranchUnitId
              //  var data =  specialBranchUnit
                return Json(specialBranchUnit);
            
            
            
            //bool isUnit = true;
            //bool isSubUnit = false;
            //bool isSubSubUnit = false;
            //IEnumerable<SpecialBranchUnit> subUnitList = new List<SpecialBranchUnit>();
            //IEnumerable<SpecialBranchUnit> subsubUnits = new List<SpecialBranchUnit>();
            //if (specialBranchUnit!=null)
            //{
            //    if(specialBranchUnit?.specialBranchUnitId !=null)
            //    {
            //        isUnit = false;
            //        isSubUnit = true;
            //        subUnitList = await _employeeService.GetSpecialBranchUnitChild((int)specialBranchUnit?.specialBranchUnitId);
            //    }
            //}

            //var res = new {
            //   isUnit,isSubUnit,isSubSubUnit,
            //    subUnitList,
            //    subsubUnits
            //};
           // var promotion = await _employeeService.GetSpecialBranchUnitChild(id);

           
        }



    }
}