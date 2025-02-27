using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.EmployeeService.Interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ReportController : Controller
    {
      
        private readonly IRepository<AnulipiList> _anulipiList;
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Assignment> _assignment;
        private readonly IRepository<AssignmentMaster> _assignmentMaster;
        private readonly IAssignmentService assignmentService;
        public IRepository<AssignmentAnulipi> _assignmentAnulipi { get; }
        public IRepository<AssignmentAnulipiPreview> _assignmentAnulipiPre { get; }

        private readonly string rootPath;
        private readonly IConfiguration _configuration;
        private readonly MyPDF myPDF;
        public string FileName;

        public ReportController(

            IHostingEnvironment hostingEnvironment, 
            IConfiguration _configuration,
            IConverter converter,
            IEmployeeService employeeService,
            IRepository<AnulipiList> anulipiList,
            IRepository<Assignment> assignment,
            IRepository<AssignmentMaster> assignmentMaster,
            IAssignmentService assignmentService,
            IRepository<AssignmentAnulipi> assignmentAnulipi,
            IRepository<AssignmentAnulipiPreview> assignmentAnulipiPre,
            UserManager<ApplicationUser> userManager
            )

        {
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            this._configuration = _configuration;
            rootPath = hostingEnvironment.ContentRootPath;

            _anulipiList = anulipiList;
            _employeeService = employeeService;
            _userManager = userManager;
            _assignment = assignment;
            _assignmentMaster = assignmentMaster;
            this.assignmentService = assignmentService;
            _assignmentAnulipi = assignmentAnulipi;
            _assignmentAnulipiPre = assignmentAnulipiPre;
        }
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PostingReportEntry(int id)
        {
            ViewBag.Id = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                anulipiLists = _anulipiList.GetAll(),
                assignmentVMs = await _employeeService.AssignmentPostedByMasteId(id),
                assignmentALL = await _employeeService.AssignmentAll(),
                assignments=await _employeeService.AssignmentDetailsByMasterId(id)
            };
            return View(model);
        }

            [HttpGet]
        public async Task<IActionResult> PostingReportEntryNote(int id)
        {
            ViewBag.Id = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                anulipiLists = _anulipiList.GetAll(),
                assignmentVMs = await _employeeService.AssignmentPostedByMasteId(id),
                assignmentALL = await _employeeService.AssignmentAll(),
                assignments=await _employeeService.AssignmentDetailsByMasterId(id)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PostingReportEntryUpdate([FromForm]PostingReportViewModel model)
        {
            try
            {
                for (int i = 0; i < model.anulipiIds.Length; i++)
                {
                    var anu = new AssignmentAnulipi
                    {
                        assignmentMasterId = model.assignMasterId,
                        anulipiListId = model.anulipiIds[i],
                        anulipiText = model.newAnulipiTxt[i],
                        shortOrder = i
                    };
                    if (model.anulipiIds[i] == 0)
                    {
                        anu.anulipiListId = null;
                    }
                    _assignmentAnulipi.Insert(anu);
                }
                //foreach (var item in model.anulipiIds)
                //{
                //    var anu = new AssignmentAnulipi
                //    {
                //        assignmentMasterId = model.assignMasterId,
                //        anulipiListId = item,
                //        anulipiText = model.newAnulipiTxt
                //    };
                //    _assignmentAnulipi.Insert(anu);
                //}

                var data = _assignmentMaster.Get(model.assignMasterId);
                data.memorandumNo = model.refNo;
                data.statusId = 18;
                data.refDate = model.refDate;
                data.title = model.title;
                data.description = model.description;
                _assignmentMaster.Update(data);

                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        public IActionResult PostingReportPreview([FromForm]PostingReportViewModel model)
        {
            try
            {
                if (model?.anulipiIds?.Length > 0)
                {
                    _employeeService.DeleteAssignmentsAunilipiPreviewByMasterId(model.assignMasterId);
                    for (int i = 0; i < model.anulipiIds.Length; i++)
                    {
                        var anu = new AssignmentAnulipiPreview
                        {
                            assignmentMasterId = model.assignMasterId,
                            anulipiListId = model.anulipiIds[i],
                            anulipiText = model.newAnulipiTxt[i],
                            shortOrder = i
                        };
                        if (model.anulipiIds[i] == 0)
                        {
                            anu.anulipiListId = null;
                        }
                        _assignmentAnulipiPre.Insert(anu);
                    }
                    
                }

                var data = _assignmentMaster.Get(model.assignMasterId);
                data.memorandumNo = model.refNo;
                data.refDate = model.refDate;
                data.title = model.title;
                data.description = model.description;
                _assignmentMaster.Update(data);

                return Json("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingConfirm(string refNum)
        {
            var enListed = await assignmentService.GetEnlistedAssignment(refNum);
            PostingReportViewModel model = new PostingReportViewModel
            {
               educations = await assignmentService.GetEducationalQualifications(),
               enlistedAssignments = enListed
            };
            return View(model);
        }
        
        public async Task<IActionResult> EditPostingConfirmDetails(int id)
        {
            ViewBag.AssignmentMasterId = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
               educations = await assignmentService.GetEducationalQualifications(),
               aVM = await _employeeService.GetAssignmentListbyMasterId(id),
               assignments = await _employeeService.AssignmentPostedByMasterId(id),
               approvals = await assignmentService.GetApprovalLogByAssignmentMasterId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingConfirmDetails(int id)
        {
            PostingReportViewModel model = new PostingReportViewModel
            {
               educations = await assignmentService.GetEducationalQualifications(),
               aVM = await _employeeService.GetAssignmentListbyMasterId(id),
               assignments = await _employeeService.AssignmentPostedByMasterId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingConfirmNote(int id)
        {
            ViewBag.AssignmentMasterId = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                educations = await assignmentService.GetEducationalQualifications(),
                aVM = await _employeeService.GetAssignmentListbyMasterId(id),
                assignments = await _employeeService.AssignmentPostedByMasterId(id),
                approvals= await assignmentService.GetApprovalLogByAssignmentMasterId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingConfirmDetailsPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Report/PostingConfirmDetails?id=" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingConfirmNotePdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Report/PostingConfirmNote?id=" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> ReturnedList(int id)
        {
            PostingReportViewModel model = new PostingReportViewModel
            {
                educations = await assignmentService.GetEducationalQualifications(),
                assignments = await _employeeService.AssignmentPostedByMasterId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReturnedListPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Report/ReturnedList?id=" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> PostingReport(int id)
        {
            PostingReportViewModel model = new PostingReportViewModel
            {
                assignmentAnulipis = await _employeeService.AssignmentMastersRopo(id),
                assignments = await _employeeService.AssignmentPostedByMasterId(id),
                assignmentVMs = await _employeeService.AssignmentPostedByMasteId(id),
                assignmentALL = await _employeeService.AssignmentAll()
            };
            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> PostingReportPreview(int id)
        {
            PostingReportViewModel model = new PostingReportViewModel
            {
                assignmentAnulipisPreview = await _employeeService.AssignmentMastersRopoForPreview(id),
                assignments = await _employeeService.AssignmentPostedByMasterId(id),
                assignmentVMs = await _employeeService.AssignmentPostedByMasteId(id),
                assignmentALL = await _employeeService.AssignmentAll()
            };
            return View(model);
        }

        public async Task<IActionResult> PostingCompleteDetails(int id)
        {
            PostingReportViewModel model = new PostingReportViewModel
            {
                assignmentAnulipis = await _employeeService.AssignmentMastersRopo(id),
                assignments = await _employeeService.AssignmentPostedByMasterId(id),
                assignmentALL = await _employeeService.AssignmentAll()
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> WaitAssignList(int id)
        {
            ViewBag.Id = id;
            PostingReportViewModel model = new PostingReportViewModel
            {
                assignments = await _employeeService.AssignmentPostedByMasterId(id),
            };
            return View(model);
        }


        [AllowAnonymous]
        public IActionResult PostingConfrimPdf(string refNum)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Report/PostingConfirm?refNum=" + refNum;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

          [AllowAnonymous]
        public IActionResult WaitAssignListPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Report/WaitAssignList?id=" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public IActionResult PostingReportPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Report/PostingReport/" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }


        [AllowAnonymous]
        public IActionResult PostingReportPreviewPdf(int id)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/Report/PostingReportPreview/" + id;

            string status = myPDF.GeneratePDFLegal(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }


    }
}