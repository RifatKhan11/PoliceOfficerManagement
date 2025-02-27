using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Web.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlphaManagement.Web.Areas.Employee.Controllers
{
   
    [Area("Employee")]
    public class PMReportController : Controller
    {

        private readonly string rootPath;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService employeeService;
        private readonly MyPDF myPDF;
        public string FileName;


        public PMReportController(
            IHostingEnvironment hostingEnvironment,
            IConfiguration _configuration,
            IConverter converter,
            IEmployeeService employeeService)
        {
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            this._configuration = _configuration;
            this.employeeService = employeeService;
            rootPath = hostingEnvironment.ContentRootPath;        
        }

        [AllowAnonymous]
        public IActionResult Article41()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Article41Pdf()
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/PMReport/Article41";
            string status = myPDF.GeneratePDFLegal(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

           [AllowAnonymous]
        public IActionResult Article41AboveSP()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Article41AboveSPPdf()
        {
            string fileName;
            string host = _configuration["Host:Link"];
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Employee/PMReport/Article41AboveSP";
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