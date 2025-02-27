using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Entity.Organogram;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.OgranogramService.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class OrganizationController : Controller
    {
        private readonly IRepository<OrganoOrganization> _repoResult;
        private readonly IRepository<OrganizationType> _repoOrganizationType;
        private readonly IOrganizationPostService organizationPostService;
        private readonly IRepository<Designation> _repoDesignation;
        private int Depth;

        public OrganizationController(IRepository<OrganoOrganization> _repoResult, IRepository<OrganizationType> _repoOrganizationType, IOrganizationPostService organizationPostService, IRepository<Designation> _repoDesignation)
        {  
            this._repoResult = _repoResult;
            this._repoOrganizationType = _repoOrganizationType;
            this.organizationPostService = organizationPostService;
            this._repoDesignation = _repoDesignation;
            Depth = 0;
        }

       
        public async Task<IActionResult> Index()
        {
            OrganoOrganizationViewModel model = new OrganoOrganizationViewModel
            {
                organoOrganizations = await organizationPostService.GetAllOrganization(),
                organizationTypes = _repoOrganizationType.GetAll(),
                organoRoots = await organizationPostService.GetRootOrganizations(),
                designations =  _repoDesignation.GetAll(),
            };
            return View(model);
        }

        // POST: Organization/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OrganoOrganizationViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.organizationTypes = _repoOrganizationType.GetAll();
                return View(model);
            }

            OrganoOrganization data = new OrganoOrganization
            {
                Id = model.organoOrganizationId,
                organizationTypeId = model.organizationTypeId,
                organoOrganizationId = model.organoOrganizationParrentId,
                nameEN = model.nameEN,
                nameBN = model.nameBN,
                remarks = model.remarks,
            };

            //return Json(model);

           await  organizationPostService.SaveOrganization(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobPost([FromForm] OrganoOrganizationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post data = new Post
                {
                    altDesignationId = model.altDesignationId,
                    designationId = model.designationId,
                    numberOfPost = model.numberOfPost,
                    organoOrganizationId = model.organoOrganizationId,
                    IsHead = model.IsHead
                };
                await organizationPostService.SaveOrUpdatePost(data);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizationsJson(string org)
        {
            Depth = 2;
            string s, tm;
            if (org == "ddm")
            {
                tm = await this.GenerateTree(1, "Start", 0);
                if (tm == "")
                    s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", 1, "Opus", "Opus", "null", 1, tm) + "}";
                else s = tm;
            }
            else if (org == "ministry")
            {
                tm = await this.GenerateTree(2, "Start", 0);
                if (tm == "")
                    s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", 2, "Ministry", "মিনিস্ট্রি", "null", 1, tm) + "}";
                else
                    s = tm;
            }
            else
            {
                tm = await this.GenerateTree(3, "Start", 0);
                if (tm == "")
                    s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", 0, "Start", "StartBN", "null", 1, tm) + "}";
                else
                    s = tm;
            }

            dynamic data = new JObject();
            data.menus = s;
            data.depth = Depth;
            return Json(data);
        }

        //Recursion For Retriving Tree 
        private async Task<string> GenerateTree(int parrentid, string parrentName, int level)
        {
            int isHead = 2;
            Depth = Math.Max(level, Depth);
            string data = "";
            IEnumerable<OrganoOrganization> organoOrganizations = await organizationPostService.GetOrganizationByParrentId(parrentid);

            if (organoOrganizations.Count() <= 0) return data;
            int last = organoOrganizations.Last().Id;

            foreach (OrganoOrganization menu in organoOrganizations)
            {
                string child = await GenerateTree(menu.Id, menu.nameEN, level + 1);
                string name = menu.nameEN;
                string Temp = await organizationPostService.GetAllPostString(menu.Id);
                if (Temp != "") name += "|" + Temp;
                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", menu.Id, name, menu.nameEN, parrentid, isHead, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }
                data += S;
            }
            return data;
        }


        public async Task<IActionResult> Delete(int id)
        {
            bool response;

            try
            {
                await organizationPostService.DeleteOgnanoOgnanizationChildById(id);
                _repoResult.Delete(_repoResult.Get(id));
                response = true;
            }
            catch (Exception)
            {
                response = false;
                throw;
            }

            return Json(response);
        }


        #region API Section
        [Route("global/api/organoOrganization/{id}")]
        [HttpGet]
        public async Task<IActionResult> OrganoOrganization(int Id)
        {
            return Json(await organizationPostService.GetOrganizationById(Id));
        }
        #endregion
    }
}