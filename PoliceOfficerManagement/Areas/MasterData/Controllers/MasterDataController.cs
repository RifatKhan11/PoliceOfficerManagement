using Microsoft.AspNetCore.Mvc;
using PoliceOfficerManagement.Areas.EmployeeArea.Models;
using PoliceOfficerManagement.Areas.MasterData.Models;
using PoliceOfficerManagement.Data.Entity;
using PoliceOfficerManagement.Services.AuthServices.Interfaces;
 using PoliceOfficerManagement.Services.MasterData.Interfaces;

namespace PoliceOfficerManagement.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class MasterDataController : Controller
    {
        private readonly IMasterDataServices _masterDataServices;
        private readonly IUserInfoes userInfoes;
        private readonly string rootPath;
        public string FileName;

        public MasterDataController(IWebHostEnvironment hostingEnvironment, IUserInfoes userInfoes, IMasterDataServices masterDataServices)
        {


            this.userInfoes = userInfoes;
            this._masterDataServices = masterDataServices;
            rootPath = hostingEnvironment.ContentRootPath;
        }



        #region Institution Info
        public async Task<IActionResult> InstitutionInfo()
        {
            var model = new InstitutionInfoViewModel
            {
                InstitutionInfos = await _masterDataServices.GetInstitutionInfo(),

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> InstitutionInfo(InstitutionInfoViewModel model)
        {
            InstitutionInfo data = new InstitutionInfo
            {
                Id = model.Id,
                type = model.type,
                nameBn = model.nameBn,
                nameEn = model.nameEn,
                establishYear = model.establishYear,
                placeInfo = model.placeInfo,
                instituteType = model.instituteType
            };

            var id = await _masterDataServices.SaveInstitutionInfo(data);
            return RedirectToAction("InstitutionInfo");
        }

        [HttpPost]
        public async Task<IActionResult> InActiveInstitutionInfo(int Id)
        {
            var data = await _masterDataServices.InActiveInstitutionInfoById(Id);
            return Json(true);
        }

        #endregion



        #region Rank
        public async Task<IActionResult> Rank()
        {
            var model = new RankViewModel
            {
                Ranks = await _masterDataServices.GetRank(),

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rank(RankViewModel model)
        {
            Rank data = new Rank
            {
                Id = model.Id,
                rankCode   = model.rankCode,  
                rankName   = model.rankName,  
                rankNameBN = model.rankNameBN, 
                shortName  = model.shortName,  
                shortOrder = model.shortOrder,
                forceCatId = model.forceCatId  
            };

            var id = await _masterDataServices.SaveRank(data);
            return Json(id);
        }

        [HttpPost]
        public async Task<IActionResult> InActiveRank(int Id)
        {
            var data = await _masterDataServices.InActiveRankById(Id);
            return Json(true);
        }

        #endregion


        #region Divisions
        public async Task<IActionResult> getAllDivisions()
        {
            var data = await _masterDataServices.GetAllDivision();
            return Json(data);
        }
        #endregion
        #region Districts
        public async Task<IActionResult> GetDistrictsByDivisionId(int divisionId)
        {
            var data = await _masterDataServices.GetDistrictsByDivisionId(divisionId);
            return Json(data);
        }
        #endregion
        #region Thanas
        public async Task<IActionResult> GetThanasByDistrictId(int districtId)
        {
            var data = await _masterDataServices.GetThanasByDistrictId(districtId);
            return Json(data);
        }
        public async Task<IActionResult> GetThanasByRangeId(int zoneId)
        {
            var data = await _masterDataServices.GetThanasByRangeId(zoneId);
            return Json(data);
        }
        #endregion
        #region UnionWard
        public async Task<IActionResult> GetUnionWardsByThanaId(int thanaId)
        {
            var data = await _masterDataServices.GetUnionWardsByThanaId(thanaId);
            return Json(data);
        }
        #endregion
        #region Village
        public async Task<IActionResult> GetUnionWardsByUnionWardId(int unionId)
        {
            var data = await _masterDataServices.GetUnionWardsByUnionWardId(unionId);
            return Json(data);
        }
        #endregion
        #region Division District
        public async Task<IActionResult> GetDivisionDistrictByRangeId(int rangeId)
        {
            var data = await _masterDataServices.GetDivisionDistrictByRangeId(rangeId);
            return Json(data);
        }
        #endregion
        #region Zone Circle
        public async Task<IActionResult> GetZoneCircleByDivisionDistrictId(int divisionDistrictId)
        {
            var data = await _masterDataServices.GetZoneCircleByDivisionDistrictId(divisionDistrictId);
            return Json(data);
        }
        #endregion

    }
}
