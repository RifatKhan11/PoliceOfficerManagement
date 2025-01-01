using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Helpers;
using PoliceOfficerManagement.Models;
using System.Diagnostics;


namespace PoliceOfficerManagement.Controllers
{
    [Authorize]
    [Area("Auth")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string? FileName;

        public HomeController(UserManager<ApplicationUser> userManager, IWebHostEnvironment _hostingEnvironment, IConverter converter
            )
        {
            _userManager = userManager;
            this._hostingEnvironment = _hostingEnvironment;
            myPDF = new MyPDF(_hostingEnvironment, converter);
            rootPath = _hostingEnvironment.ContentRootPath;

        }
        //[Authorize(Roles = "Super Admin,Admin,Reader")]
        public async Task<IActionResult> ReaderDashboard()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {



            return View();
        }


        public async Task<IActionResult> Search()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TableTest()
        {
            return View();
        }


        public IActionResult UserFind()
        {
            return View();
        }
    }
}
