using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoliceOfficerManagement.Areas.Auth.Models;
using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Data.Auth;
using PoliceOfficerManagement.Helpers;
using PoliceOfficerManagement.Services.AuthServices.Interfaces;
using PoliceOfficerManagement.Services.Dapper.IInterfaces;
using PoliceOfficerManagement.Services.jwt.Interfaces;

namespace PoliceOfficerManagement.Areas.Auth.Controllers
{
    //[Authorize]
    [Area("Auth")]
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUserInfoes _iUserInfoes;
        //private readonly IMasterDataService _iMasterDataService;

        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        private readonly string rootPath;
        private readonly MyPDF myPDF;


        public string FileName;
        private readonly IDapper _dapper;
        private readonly IJwtFactoryService _jwtFactory;
        public AccountController(IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConverter converter, IDapper dapper, IJwtFactoryService jwtFactory, IConfiguration configuration, IUserInfoes iUserInfoes/*, IMasterDataService iMasterDataService*/)
        {
            this._dapper = dapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _iUserInfoes = iUserInfoes;
            //_iMasterDataService = iMasterDataService;
            this._hostingEnvironment = hostingEnvironment;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
            _jwtFactory = jwtFactory;

            _env = hostingEnvironment;
            _configuration = configuration;
            _iUserInfoes = iUserInfoes;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region Register Login LogOut
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(/*string returnUrl = null*/)
        {

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.Name);
                var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    if ((user.isVarified == 1 || user.userTypeId == 2) && user.isActive == 1)
                    {
                        #region Log
                        var remote = HttpContext.Connection.RemoteIpAddress;
                        var local = HttpContext.Connection.LocalIpAddress;
                        string userip = remote.ToString();
                        string useripLocal = local.ToString();
                        #endregion
                        var userInfo = await _iUserInfoes.GetUserInfoByUserName(user.UserName);

                        if (userInfo != null)
                        {

                            try
                            {
                                ActivityHistoryLog activityHistoryLog = new ActivityHistoryLog
                                {
                                    actionName = "Login",
                                    ApplicationUserId = user.Id,
                                    logTypeId = 1,
                                    description = "সর্বশেষ লগ-ইন করা হয়েছে " + user.FullName + " দ্বারা |",
                                    statusId = 1,
                                    date = DateTime.Now,
                                    ipAddress = userip
                                };
                                //await _iUserInfoes.SaveActivityLog(activityHistoryLog);

                            }
                            catch (Exception)
                            {

                            }

                            var userInfos = await _iUserInfoes.GetUserInfoByUserName(user.UserName);
                            
                            return RedirectToAction("GeneralUserDashboard", "FileUpload", new { area = "UploadDocument" });
                            
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return View(model);
                        }


                    }
                    else if (user.isActive == 0)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt. User Not Active");
                        return View(model);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Password Incorrect");
                    return View(model);
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string userName = null, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (userName == null)
            {
                userName = "";
            }

            var model = new RegisterViewModel
            {
                users = await _userManager.FindByNameAsync(userName),
                identityRoles = await _iUserInfoes.GetAllRoles()
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        isActive = 1,
                        userTypeId = 1,
                        isVarified = 1,
                        createdAt = DateTime.Now,
                        updatedAt = DateTime.Now
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, model.UserRole);
                        ApplicationUser userInfo = await _userManager.FindByNameAsync(model.UserName);
                        //var employee = await _iMasterDataService.GetEmployeeInfoByEmployeeCode(model.UserName);

                        //employee.ApplicationUserId = userInfo.Id;
                        //await _iMasterDataService.SaveEmployeeInfo(employee);

                        return Ok(new { status = true, message = "User Create Successfully!" });
                    }
                    else
                    {
                        return Ok(new { status = false, message = "Failed, Something went wrong!" });
                    }

                }
                else
                {
                    return Ok(new { status = false, message = "Failed, Something went wrong!" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = ex.Message + "!" });
            }

        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogInBuffet(string returnUrl = null)
        {

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> AllUserList()
        //{
        //    var model = new EmployeeViewModel
        //    {
        //        employeeInfos = await _iMasterDataService.GetAllEmployeeInfo()
        //    };
        //    return View(model);
        //}


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ClientRegister(string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            string username = HttpContext.User.Identity.Name;
            ApplicationUser user = await _userManager.FindByNameAsync(username);
            //_logger.LogInformation("User logged out.");
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            #endregion

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Account", new { area = "Auth" });
            //}


        }



        [AllowAnonymous]
        [Route("api/Account/AllGDByFiltering/{range}/{dis}/{zone}/{thana}")]
        [HttpGet]
        public async Task<IActionResult> AllGDByFiltering(int range, int dis, int zone, int thana)
        {
            return Json("");
        }

        [AllowAnonymous]
        [Route("api/Account/GetPublicUserLogHistoryLog/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPublicUserLogHistoryLog(string id)
        {
            //var result = await _iUserInfoes.GetPublicUserLogHistory(id);
            return Json("");
        }


        public ActionResult AccessDenied()
        {
            return View();
        }


    }
}