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

        public async Task<IActionResult> InstitutionInfoTraining()
        {
            var model = new InstitutionInfoViewModel
            {
                InstitutionInfos = await _masterDataServices.GetInstitutionInfoTraning(),

            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> InstitutionInfo(InstitutionInfoViewModel model)
        {
            var instituteTypeId = 0;
            if(model.instituteType == "Educational")
            {
                instituteTypeId = 1;
            }
            else if(model.instituteType == "Training")
            {
                instituteTypeId = 2;
            }
            InstitutionInfo data = new InstitutionInfo
            {
                Id = model.Id,
                type = model.type,
                nameBn = model.nameBn,
                nameEn = model.nameEn,
                establishYear = model.establishYear,
                placeInfo = model.placeInfo,
                instituteType = instituteTypeId
            };

            var id = await _masterDataServices.SaveInstitutionInfo(data);

             
            if (model.instituteType == "Educational")
            {
                return RedirectToAction("InstitutionInfo");
            }
            else if (model.instituteType == "Training")
            {
                return RedirectToAction("InstitutionInfoTraining");
            }
            return RedirectToAction("AdminDashboard", "Home", new { area = "Auth" });
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

        #region Range Metros
        public async Task<IActionResult> RangeMetros()
        {
            var model = new RangeMetrosViewModel
            {
                Ranges = await _masterDataServices.GetAllRangeMetros(),

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RangeMetros(RangeMetrosViewModel model)
        {
            RangeMetro data = new RangeMetro
            {
                Id = model.Id,
                rangeMetroName = model.rangeMetroName,
                rangeMetroNameBn = model.rangeMetroNameBn,
                latitude = model.latitude,
                longitude = model.longitude,
                pimsRangeId = model.pimsRangeId,
                pimsRangeName = model.pimsRangeName,
                isActive = model.IsActive,
            };

            var id = await _masterDataServices.SaveRangeMetro(data);
            return RedirectToAction("RangeMetros");
            //return Json(id);
        }

        [HttpPost]
        public async Task<IActionResult> InActiveRangeMetros(int Id)
        {
            var data = await _masterDataServices.InActiveRangeMetroById(Id);
            return Json(true);
        }

        #endregion


        #region Division District
        public async Task<IActionResult> GetDivisionDistrictByRangeId(int rangeId)
        {
            var data = await _masterDataServices.GetDivisionDistrictByRangeId(rangeId);
            return Json(data);
        }
        public async Task<IActionResult> DivisionDistrict()
        {
            var model = new DivisionDistrictViewModel
            {
                Ranges = await _masterDataServices.GetAllRangeMetros(),
                divisionDistricts = await _masterDataServices.GetAllDivisionDistrict(),

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DivisionDistrict(DivisionDistrictViewModel model)
        {
            DivisionDistrict data = new DivisionDistrict
            {
                Id = model.Id, 
                rangeMetroId = model.rangeMetroId,
                divisionDistrictName = model.divisionDistrictName,
                divisionDistrictNameBn = model.divisionDistrictNameBn,
                latitude = model.latitude,
                longitude = model.longitude,
                pimsDistrictId = model.pimsDistrictId,
                pimsDistrictName = model.pimsDistrictName
            };

            var id = await _masterDataServices.SaveDivisionDistrict(data);
            return RedirectToAction("DivisionDistrict");
            //return Json(id);
        }

        [HttpPost]
        public async Task<IActionResult> InActiveDivisionDistrict(int Id)
        {
            var data = await _masterDataServices.InActiveDivisionDistrictById(Id);
            return Json(true);
        }

        #endregion
        #region Zone Circle
        public async Task<IActionResult> GetZoneCircleByDivisionDistrictId(int divisionDistrictId)
        {
            var data = await _masterDataServices.GetZoneCircleByDivisionDistrictId(divisionDistrictId);
            return Json(data);
        }

        public async Task<IActionResult> ZoneCircle()
        {
            var model = new ZoneCircleViewModel
            {
                zones = await _masterDataServices.GetAllZoneCircle(),
                divisionDistricts = await _masterDataServices.GetAllDivisionDistrict(),

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ZoneCircle(ZoneCircleViewModel model)
        {
            ZoneCircle data = new ZoneCircle
            {
                Id = model.Id, 
                divisionDistrictId = model.divisionDistrictId,
                zoneName = model.zoneName,
                zoneNameBn = model.zoneNameBn,
                latitude = model.latitude,
                longitude = model.longitude,
                pimsZoneId = model.pimsZoneId,
                pimsZoneName = model.pimsZoneName
            };

            var id = await _masterDataServices.SaveZoneCircle(data);
            return RedirectToAction("ZoneCircle");
            //return Json(id);
        }

        [HttpPost]
        public async Task<IActionResult> InActiveZoneCircle(int Id)
        {
            var data = await _masterDataServices.InActiveZoneCircleById(Id);
            return Json(true);
        }
        #endregion

        #region Police Thana
        public async Task<IActionResult> GetThanasByRangeId(int zoneId)
        {
            var data = await _masterDataServices.GetThanasByRangeId(zoneId);
            return Json(data);
        }

        public async Task<IActionResult> PoliceThana()
        {
            var model = new PoliceThanaViewModel
            {
                Ranges = await _masterDataServices.GetAllRangeMetros(),
                divisionDistricts = await _masterDataServices.GetAllDivisionDistrict(),
                policeThanas = await _masterDataServices.GetAllPoliceThana(),

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PoliceThana(PoliceThanaViewModel model)
        {
            PoliceThana data = new PoliceThana
            {
                Id = model.Id,
                rangeMetroId = model.rangeMetroId,
                divisionDistrictId = model.divisionDistrictId, 
                zoneCircleId = model.zoneCircleId,
                upazillaId = model.upazillaId,
                policeThanaName = model.policeThanaName,
                policeThanaNameBn = model.policeThanaNameBn,
                isReportable = model.isReportable,
                latitude = model.latitude,
                longitude = model.longitude,
                policeThanaId = model.policeThanaId,
                address = model.address,
                fariType   = model.fariType , 
                isChild = model.isChild,
                pimsThanaId = model.pimsThanaId,
                pimsThanaName = model.pimsThanaName
            };

            var id = await _masterDataServices.SavePoliceThana(data);
            return RedirectToAction("PoliceThana");
            //return Json(id);
        }

        [HttpPost]
        public async Task<IActionResult> InActivePoliceThana(int Id)
        {
            var data = await _masterDataServices.InActivePoliceThanaById(Id);
            return Json(true);
        }

        #endregion
    }
}
