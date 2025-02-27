using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Web.Areas.Employee.Models;
using AlphaManagement.Web.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ClosedXML.Excel;
using AlphaManagement.DAL.Models;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class EmployeeGradationController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<BCSBatch> _repoBCSBatch;
        private readonly IRepository<SpecialBranchUnit> _repoSpecialBranchUnit;
        private readonly IRepository<Organization> _repoOrganization;
        private readonly IRepository<Degree> _repoDegree;
        private readonly IRepository<Spouse> _repoSpouse;
        private readonly IRepository<District> _repoDistrict;
        private readonly string rootPath;
        private readonly IConfiguration _configuration;
        private readonly MyPDF myPDF;
        public string FileName;

        public EmployeeGradationController(IHostingEnvironment hostingEnvironment, IRepository<Degree> repoDegree, IRepository<District> repoDistrict, IRepository<Spouse> repoSpouse, IConfiguration _configuration,IConverter converter, IEmployeeService employeeService, IRepository<Rank> repoRank, IRepository<BCSBatch> repoBCSBatch, IRepository<SpecialBranchUnit> repoSpecialBranchUnit, IRepository<Organization> repoOrganization)
        {
            _employeeService = employeeService;
            _repoRank = repoRank;
            _repoBCSBatch = repoBCSBatch;
            _repoSpecialBranchUnit = repoSpecialBranchUnit;
            _repoOrganization = repoOrganization;
            _repoDegree = repoDegree;
            _repoDistrict = repoDistrict;
            _repoSpouse = repoSpouse;
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            this._configuration = _configuration;
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public IActionResult Index()
        {
            EmpViewModel model = new EmpViewModel
            {
                ranks = _repoRank.GetAll().Where(x=>x.rankCode== "Officer").OrderBy(x=>x.shortOrder),
                bCSBatches = _repoBCSBatch.GetAll(),
                specialBranchUnits=_repoSpecialBranchUnit.GetAll(),
                organizations=_repoOrganization.GetAll(),
                degreeList = _repoDegree.GetAll(),
                spouseList=_repoSpouse.GetAll(),
                districtList = _repoDistrict.GetAll()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfosForGradation(int rankId,int batchId,string fromDate,string toDate, string prlFromDate, string prlToDate,int? typeId,string bpNoSearch,string gender)
        {
            string username = User.Identity.Name;
            var result = await _employeeService.GetEmployeeInformationForGradationNew(rankId,batchId,fromDate,toDate,prlFromDate,prlToDate,typeId, username, bpNoSearch, gender);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfosForGradationNew(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string bpNoSearch, string gender)
        {
            string username = User.Identity.Name;
            var result = await _employeeService.GetEmployeeGradationInformationNew(rankId, batchId, fromDate, toDate, prlFromDate, prlToDate, typeId, username, bpNoSearch, gender);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> InactiveEmployeeInfosForGradation(int rankId,int batchId,string fromDate,string toDate, string prlFromDate, string prlToDate,int? typeId,string bpNoSearch)
        {
            string username = User.Identity.Name;
            var Model = new EmpViewModel
            {
                //employeeGradationSPs = await _employeeService.GetAllInActiveEmployeeInfo(rankId, batchId, fromDate, toDate, prlFromDate, prlToDate, typeId, username, bpNoSearch)

            };
            return View(Model);
        }


         [HttpGet]
        public async Task<IActionResult> GetInactiveEmployeeInfosForGradation(int rankId,int batchId,string fromDate,string toDate, string prlFromDate, string prlToDate,int? typeId,string bpNoSearch)
        {
            string username = User.Identity.Name;

            var result = await _employeeService.GetAllInActiveEmployeeInfo(rankId, batchId, fromDate, toDate, prlFromDate, prlToDate, typeId, username, bpNoSearch);

           
            return Json(result);
        }



         [HttpGet]
        public async Task<IActionResult> GetEmployeeInfosForGradationByEmpId(int employeeId)
        {
            string username = User.Identity.Name;
            var result = await _employeeService.GetEmployeeInfogradationById(employeeId);
            return Json(result);
        }


        [AllowAnonymous]
        public async Task<IActionResult> GEmployeeGradationPdfView(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string bpNoSearch, string gender)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/EmployeeGradation/GetEmployeeGradationView?rankId=" + rankId+ "&batchId="+ batchId+ "&fromDate="+ fromDate+ "&toDate="+ toDate + "&prlFromDate=" + prlFromDate + "&prlToDate=" + prlToDate + "&typeId=" + typeId+ "&bpNoSearch=" + bpNoSearch + "&gender=" + gender;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeGradationView(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string bpNoSearch, string gender)
        {
            string username = "";
            var result = await _employeeService.GetEmployeeInformationForGradationNew(rankId, batchId, fromDate, toDate,prlFromDate,prlToDate,typeId, username, bpNoSearch, gender);
            EmpViewModel model = new EmpViewModel
            {
                employeeGradationSPs=result
            };
            ViewBag.typeId = typeId;
            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> GEmployeePreviousPostingInfoPdfView(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string bpNoSearch, string gender)
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/EmployeeGradation/GetEmployeeInformationWithPreviousPosting?rankId=" + rankId + "&batchId=" + batchId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&prlFromDate=" + prlFromDate + "&prlToDate=" + prlToDate + "&typeId=" + typeId + "&bpNoSearch=" + bpNoSearch + "&gender=" + gender;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInformationWithPreviousPosting(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string bpNoSearch, string gender)
        {
            string username = "";
            var result = await _employeeService.GetEmployeeInformationWithPreviousPosting(rankId, batchId, fromDate, toDate,prlFromDate,prlToDate,typeId, username, bpNoSearch, gender);
            EmpViewModel model = new EmpViewModel
            {
                employeePreviousPostingPlaceSPs = result
            };
            ViewBag.typeId = typeId;
            return View(model);
        }

        public async Task<IActionResult> GetEmployeeGradationExcel(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string bpNoSearch, string gender)
        {

            using (var workbook = new XLWorkbook())
            {
                string username = "";
                var result = await _employeeService.GetEmployeeInformationForGradationNew(rankId, batchId, fromDate, toDate, prlFromDate, prlToDate, typeId, username, bpNoSearch, gender);
                var x = 1;
                var worksheet = workbook.Worksheets.Add("Employee Gradation");
                var currentRow = 1;

                #region Header

                worksheet.Cell("A1").Value = "বিসিএস (পুলিশ) ক্যাডারভুক্ত কর্মকর্তাদের খসড়া গ্রেডেশন লিস্ট";
                var titlerangeA1 = worksheet.Range("A1:K1");
                titlerangeA1.Merge().Style.Font.SetBold().Font.FontSize = 14;
                titlerangeA1.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeA1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("A2").Value = "";
                var titlerangeA2 = worksheet.Range("A2:K2");
                titlerangeA2.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeA2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeA2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                
                worksheet.Cell("A3").Value = "ক্রমিক নং";
                var titlerangeA3 = worksheet.Range("A3");
                titlerangeA3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeA3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeA3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("B3").Value = "কর্মকর্তার নাম,জন্ম তারিখ, জন্ম জেলা ও শিক্ষাগত যোগ্যতা";
                var titlerangeB3 = worksheet.Range("B3");
                titlerangeB3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeB3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeB3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("C3").Value = "অভিজ্ঞতা (পদের নাম) নিয়োগের তারিখসহ";
                var titlerangeC3 = worksheet.Range("C3");
                titlerangeC3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeC3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeC3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("D3").Value = "পুলিশ ক্যাডারের প্রারম্ভিক পদে কমিশনের সুপারিশ অনুযায়ী নিয়মিত নিয়োগ (সরাসরি / পদোন্নতি)";
                var titlerangeD3 = worksheet.Range("D3");
                titlerangeD3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeD3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeD3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("E3").Value = "সিনিয়র স্কেলে নিয়মিত পদোন্নতির তারিখ";
                var titlerangeE3 = worksheet.Range("E3");
                titlerangeE3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeE3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeE3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("F3").Value = "সামরিক কর্মকর্তাদের প্রাসঙ্গিক তথ্যঃ - ক) কমিশন প্রাপ্তির তারিখ খ) অবসর গ্রহণের তারিখ";
                var titlerangeF3 = worksheet.Range("F3");
                titlerangeF3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeF3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeF3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("G3").Value = "বর্তমান পদে নিয়মিত নিয়োগের তারিখ";
                var titlerangeG3 = worksheet.Range("G3");
                titlerangeG3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeG3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeG3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("H3").Value = "মন্তব্য";
                var titlerangeH3 = worksheet.Range("H3");
                titlerangeH3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeH3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeH3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("I3").Value = "বিসিএস ব্যাচ";
                var titlerangeI3 = worksheet.Range("I3");
                titlerangeI3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeI3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeI3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("J3").Value = "পুরুষ/মহিলা";
                var titlerangeJ3 = worksheet.Range("J3");
                titlerangeJ3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeJ3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeJ3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("K3").Value = "বর্তমান পদ";
                var titlerangeK3 = worksheet.Range("K3");
                titlerangeK3.Merge().Style.Font.SetBold().Font.FontSize = 11;
                titlerangeK3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titlerangeK3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                
                worksheet.ColumnWidth = 15;
                currentRow++;
                currentRow++;
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "১";
                worksheet.Cell(currentRow, 2).Value = "২";
                worksheet.Cell(currentRow, 3).Value = "৩";
                worksheet.Cell(currentRow, 4).Value = "৪";
                worksheet.Cell(currentRow, 5).Value = "৫";
                worksheet.Cell(currentRow, 6).Value = "৬";
                worksheet.Cell(currentRow, 7).Value = "৭";
                worksheet.Cell(currentRow, 8).Value = "৮";
                worksheet.Cell(currentRow, 9).Value = "৯";
                worksheet.Cell(currentRow, 10).Value = "১০";
                worksheet.Cell(currentRow, 11).Value = "১১";

                worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Cell(currentRow, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                worksheet.Cell(currentRow, 1).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 2).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 3).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 4).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 5).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 6).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 7).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 8).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 9).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 10).Style.Font.SetBold().Font.FontSize = 10;
                worksheet.Cell(currentRow, 11).Style.Font.SetBold().Font.FontSize = 10;

                currentRow++;
                #endregion

                #region Body

                foreach (var data in result)
                {
                    worksheet.Cell(currentRow, 1).Value = data.gradationSerial+ "।";
                    worksheet.Cell(currentRow, 2).Value = "জনাব " + data.nameBangla + ", বিপি-" + data.bpNo + ", জন্ম তারিখ :" + data.dateOfBirth + ", জন্ম জেলা : " + data.homeDistrict + ", শিক্ষাগত যোগ্যতা : " + data.educationQualification + ", মেধাক্রম- " + data.bcsPosition;
                    worksheet.Cell(currentRow, 3).Value = "";
                    worksheet.Cell(currentRow, 4).Value = data.joiningDateGovtService + ", " + data.joiningRank;
                    worksheet.Cell(currentRow, 5).Value = data.firstPromotionDate;
                    worksheet.Cell(currentRow, 6).Value = data.commissionReceiptDate + ", " + data.commissionLPRDate + ", " + data.civilRank + ", " + data.civilReqDate;
                    worksheet.Cell(currentRow, 7).Value = data.rankName + ", " + data.lastPromotionDate;
                    worksheet.Cell(currentRow, 8).Value = data.comments;
                    worksheet.Cell(currentRow, 9).Value = data.batchNameBN;
                    worksheet.Cell(currentRow, 10).Value = data.gender;
                    worksheet.Cell(currentRow, 11).Value = data.rankName;
                    currentRow++;
                    x++;
                }

                #endregion



                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeGradation.xlsx");
                }
            }
        }



        public async Task<IActionResult> GetGradationInfo(int empId)
        {
            var result =await _employeeService.GetEmployeeInformationForGradation(empId);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployeeGradationInfo(GradationViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            model.userName = userName;
            var gradationInfo = await _employeeService.UpdateEmployeeGradation(model);
            return Json(gradationInfo);
        }

        [HttpPost]
        public async Task<IActionResult> IndividualApproveUpdateEmployeeGradationInfo(EmployeeGradationUpdateModel model)
        {
            //return Json(model.gradationId);
            string userName = HttpContext.User.Identity.Name;
            
            var gradationInfo = await _employeeService.UpdateEmployeeDataFromGradationById((int)model.gradationId,userName);
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> AllApproveUpdateEmployeeGradationInfo(EmployeeGradationUpdateModel model)
        {
            try
            {
                //return Json(model.lstGradationId);
                string userName = HttpContext.User.Identity.Name;
                if (model.lstGradationId.Count > 0)
                {
                    foreach (var item in model.lstGradationId)
                    {
                        var gradationInfo = await _employeeService.UpdateEmployeeDataFromGradationById((int)item, userName);
                    }

                }

                return Json(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IActionResult RetiredEmployeeGradation()
        {
            EmpViewModel model = new EmpViewModel
            {
                ranks = _repoRank.GetAll().Where(x => x.rankCode == "Officer").OrderBy(x => x.shortOrder),
                bCSBatches = _repoBCSBatch.GetAll(),
                //specialBranchUnits = _repoSpecialBranchUnit.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> GradationUpgradationPendingList()
        {
            var result = await _employeeService.GetEmployeeInformationForGradationUpdate(2);
            EmpViewModel model = new EmpViewModel
            {
                ranks = _repoRank.GetAll().Where(x => x.rankCode == "Officer").OrderBy(x => x.shortOrder),
                bCSBatches = _repoBCSBatch.GetAll(),
                employeeGradations =result
            };
            return View(model);
        }

    }
}