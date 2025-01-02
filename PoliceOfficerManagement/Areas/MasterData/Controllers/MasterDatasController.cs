using Microsoft.AspNetCore.Mvc;
using PoliceOfficerManagement.Areas.EmployeeArea.Models;
using PoliceOfficerManagement.Areas.MasterData.Models;
using PoliceOfficerManagement.Data.Entity;
using PoliceOfficerManagement.Services.AuthServices.Interfaces;
 using PoliceOfficerManagement.Services.MasterData.Interfaces;

namespace PoliceOfficerManagement.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class MasterDatasController : Controller
    {
        private readonly IMasterDataServices _masterDataServices;
        private readonly IUserInfoes userInfoes;
        private readonly string rootPath;
        public string FileName;

        public MasterDatasController(IWebHostEnvironment hostingEnvironment, IUserInfoes userInfoes, IMasterDataServices masterDataServices)
        {


            this.userInfoes = userInfoes;
            this._masterDataServices = masterDataServices;
            rootPath = hostingEnvironment.ContentRootPath;
        }


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
    }
}
