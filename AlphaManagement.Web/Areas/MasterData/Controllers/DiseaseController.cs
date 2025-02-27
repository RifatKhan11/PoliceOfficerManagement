using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Web.Areas.MasterData.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AlphaManagement.Web.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class DiseaseController : Controller
    {
        private readonly IRepository<DiseaseGroup> diseaseGroup;
        private readonly IRepository<Vaccines> repoVaccine;
        private readonly IRepository<Disease> repoDisease;

        public DiseaseController(
            IRepository<DiseaseGroup> diseaseGroup,
            IRepository<Vaccines> repoVaccine,
            IRepository<VaccineGroup> repoVaccineGroup,
            IRepository<Disease> repoDisease
            )
        {
            this.diseaseGroup = diseaseGroup;
            this.repoVaccine = repoVaccine;
            this.repoDisease = repoDisease;
        }

        #region Disease 

        public IActionResult Index()
        {
            DiseaseViewModel model = new DiseaseViewModel
            {
                diseases = repoDisease.GetAll(),
                diseaseGroups = diseaseGroup.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveDisease(DiseaseViewModel model)
        {
            Disease disease = new Disease
            {
                Id = model.diseasId,
            //    diseaseGroupId = model.groupId,
                diseaseName = model.diseaseName,
                diseaseNameBn = model.diseaseNameBn,
                status = 0,
            };
            if (model.diseasId > 0)
            {
                repoDisease.Update(disease);
            }
            else
            {
                repoDisease.Insert(disease);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteDisease(int id)
        {
            bool response;
            try
            {
                repoDisease.Delete(repoDisease.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }
            return Json(response);
        }
        #endregion

        [HttpGet]
        public IActionResult SaveVaccine()
        {
            DiseaseViewModel model  = new DiseaseViewModel
            {
              vaccines = repoVaccine.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveVaccine(DiseaseViewModel model)
        {
            Vaccines vaccines = new Vaccines
            {
                Id = model.vaccineId,
                vaccineName = model.vaccineName,
                vaccineNameBn = model.vaccineNameBn,
                statusId = 0,
            };
            if (model.vaccineId > 0)
            {
                repoVaccine.Update(vaccines);
            }
            else
            {
                repoVaccine.Insert(vaccines);
            }
            return RedirectToAction(nameof(SaveVaccine));
        }

        [HttpPost]
        public IActionResult DeleteVaccine(int id)
        {
            bool response;
            try
            {
                repoVaccine.Delete(repoVaccine.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }
            return Json(response);
        }

        #region Vaccine 


        #endregion


    }
}