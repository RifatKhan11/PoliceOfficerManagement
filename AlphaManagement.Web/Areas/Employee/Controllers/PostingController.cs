using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.EmployeeService.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class PostingController : Controller
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<BCSBatch> _repoBatches;
        IConverter converter;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public PostingController(IAssignmentService _assignmentService, IRepository<Rank> _repoRank,IRepository<BCSBatch> repoBatches, IEmployeeService _employeeService, IConfiguration _configuration, IHostingEnvironment hostingEnvironment, IConverter converter)
        {
            this._assignmentService = _assignmentService;
            this._employeeService = _employeeService;
            this._configuration = _configuration;
            this._repoRank = _repoRank;
            this._repoBatches = repoBatches;
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        [Authorize(Roles = "Super Admin,Admin,IGP,Sub-Admin")]
        public async Task<IActionResult> Index()
        {
            PostingVacancy model = new PostingVacancy
            {
                specialBranchUnits = await _assignmentService.GetPositionVacency(0, 0),
                ParentBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                ranks = await _assignmentService.GetRankGreaterASP(),
            };
            return View(model);
        }

        public async Task<IActionResult> OverDue()
        {
            PostingVacancy model = new PostingVacancy
            {
                employeeInfos = await _employeeService.GetOverDueEmployeeInfoList(),
                ParentBranchUnits = await _employeeService.GetSpecialBranchUnitParent(),
                ranks = await _assignmentService.GetRankGreaterASP(),
                batches= _repoBatches.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> OverDueJoining()
        {
            PostingVacancy model = new PostingVacancy
            {
                assignments = await _assignmentService.GetAssignmentWithDuejoining()
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetPositionVacencyReport(int Id, int branch)
        {
            PostingVacancy model = new PostingVacancy
            {
                specialBranchUnits = await _assignmentService.GetPositionVacency(Id, branch),
            };
            return View(model);
        }

        public async Task<IActionResult> GetEmpDetails(int Id, int branch)
        {
            return Json(await _assignmentService.GetEmployeeInfoByUnitRank(Id, branch));
        }

        [AllowAnonymous]
        public IActionResult GetPositionVacencyReportPdf(int Id, int branch)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Posting/GetPositionVacencyReport?id=" + Id + "&&branch=" + branch;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public IActionResult GetOverDuePositionReportPdf(int Id, int branch,int batch)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Posting/GetOverDuePositionReport?id=" + Id + "&&branch=" + branch+ "&&batch=" + batch;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> GetOverDuePositionReport(int Id, int branch,int batch)
        {
            PostingVacancy model = new PostingVacancy
            {
                employeeInfos = await _employeeService.GetOverDueEmployeeInfoList(Id, branch,batch),
            };
            return View(model);
        }


        public async Task<IActionResult> GetPositionVacency(int Id, int branch)
        {
            return Json(await _assignmentService.GetPositionVacency(Id, branch));
        }


        public async Task<IActionResult> GetOverDueEmployeeInfoListFilter(int Id, int branch,int batch)
        {
            return Json(await _employeeService.GetOverDueEmployeeInfoListFilter(Id, branch, batch));
        }
    }
}