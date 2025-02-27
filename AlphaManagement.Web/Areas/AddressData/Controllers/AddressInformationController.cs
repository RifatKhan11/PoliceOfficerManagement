using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.AddressData.Models;
using AlphaManagement.Web.Areas.AddressData.Models.Lang;
using AlphaManagement.Web.Helpers;
using Microsoft.AspNetCore.Hosting;


namespace AlphaManagement.Web.Areas.AddressData.Controllers
{
    [Area("AddressData")]
    public class AddressInformationController : Controller
    {
        private readonly LangGenerate<AddressInformationLn> _lang;
        private readonly IRepository<AddressInformation> _repoAddressInformation;
        public AddressInformationController(IHostingEnvironment hostingEnvironment, IRepository<AddressInformation> repoAddressInformation)
        {
            _lang = new LangGenerate<AddressInformationLn>(hostingEnvironment.ContentRootPath);
            _repoAddressInformation = repoAddressInformation;
        }


        public IActionResult Index()
        {
            var model = new AddressInformationViewModel
            {
                fLang = _lang.PerseLang("AddressData/AddressInformationEN.json", "AddressData/AddressInformationBN.json", Request.Cookies["lang"]),
                addressInformationList = _repoAddressInformation.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] AddressInformationViewModel model)
        {
            var Obj = new AddressInformation
            {
                Id = model.AddressInformationId,
                employeeInfoId = model.employeeInfoId,
                spouseId = model.spouseId,
                countryId = model.countryId,
                divisionId = model.divisionId,
                districtId = model.districtId,
                thanaId = model.thanaId,
                unionWardId = model.unionWardId,
                villageId = model.villageId,
                union = model.union,
                postOffice = model.postOffice,
                postCode = model.postCode,
                blockSector = model.blockSector,
                houseVillage = model.houseVillage,
                roadNumber = model.roadNumber,
                addressDetails = model.addressDetails,
                oneLineAddress = model.oneLineAddress,
                type = model.type

            };
            _repoAddressInformation.Insert(Obj);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult DeleteAddressInformation(int id)
        {
            bool response;

            try
            {
                _repoAddressInformation.Delete(_repoAddressInformation.Get(id));
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