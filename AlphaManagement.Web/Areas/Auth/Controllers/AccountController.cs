using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmailService.interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Domain.SMSService.interfaces;
using AlphaManagement.Web.Areas.Auth.Models;
using AlphaManagement.Web.Controllers;
using AlphaManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Auth.Controllers
{

    [Area("Auth")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IAccessLogHistoryService _accessLogHistoryService;
        private readonly ISMSService _sMSService;
        private readonly IRepository<UserInformation> _repoUserInfo;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<AssignmentMaster> _assignmentMaster;
        private readonly IRepository<ApprovalLog> _approvalLog;
        private readonly IUserInfoes userInfoes;
        private readonly IEmployeeService _employeeService;
        private readonly IRepository<InternalEnlistedAssignmentMaster> _repoInternalAssignmentMaster;
        private readonly IRepository<InternalApprovalLog> _repoInternalApprovalLog;
        private readonly IInternalPostingServices _internalServices;
        private readonly IRepository<InternalAssignment> _repoInterAssignment;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IUserInfoes userInfoes, IEmailSenderService emailSenderService, IAccessLogHistoryService accessLogHistoryService, IRepository<UserInformation> repoUserInfo, IRepository<Rank> repoRank, IEmployeeService employeeService, ISMSService sMSService,
            IRepository<AssignmentMaster> assignmentMaster, IRepository<ApprovalLog> approvalLog, IRepository<InternalEnlistedAssignmentMaster> repoInternalAssignmentMaster, IInternalPostingServices internalServices, IRepository<InternalApprovalLog> repoInternalApprovalLog, IRepository<InternalAssignment> repoInterAssignment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            _emailSenderService = emailSenderService;    
            _sMSService = sMSService;
            _accessLogHistoryService = accessLogHistoryService;
            _repoUserInfo = repoUserInfo;
            _repoRank = repoRank;
            _assignmentMaster = assignmentMaster;
            _approvalLog = approvalLog;
            _employeeService = employeeService;
            _repoInternalAssignmentMaster = repoInternalAssignmentMaster;
            _internalServices = internalServices;
            _repoInternalApprovalLog = repoInternalApprovalLog;
            _repoInterAssignment = repoInterAssignment;
        }

        [HttpGet]
        public IActionResult WelcomePage()
        {
            return View();
        }
         [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var userInfos = await userInfoes.GetUserInfoByUser(model.Name);
                if (userInfos != null)
                {
                    var users = await _userManager.FindByIdAsync(userInfos.Id);
                    var roless = await _userManager.GetRolesAsync(users);
                    if (userInfos.isActive == 1 && (roless.Contains("Super Admin") || roless.Contains("Admin") || roless.Contains("PHQ Approver") || roless.Contains("Sub-Admin") || roless.Contains("IGP") || roless.Contains("Disciplinary Action Entry Operator") || roless.Contains("ACR Entry Operator") || roless.Contains("BPA") || roless.Contains("SB") || roless.Contains("Medical") || roless.Contains("Traning1") || roless.Contains("PM1 Gradation")))
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                            var mechineName = Environment.MachineName;
                            
                            var userAgent = Request.Headers["User-Agent"].ToString();

                            UserLogHistory userLog = new UserLogHistory
                            {
                                userId = model.Name,
                                logTime = DateTime.Now,
                                status = 1,
                                ipAddress = ip,
                                pcName = mechineName,
                                browserName = userAgent
                            };

                            await _accessLogHistoryService.SaveUserLogHistory(userLog);
                            var user = await _userManager.FindByIdAsync(userInfos.Id);
                            var roles = await _userManager.GetRolesAsync(user);

                            if (roles.Contains("IGP"))
                            {
                                return RedirectToAction("IGPDashboard", "Home");
                            }
                            else if (roles.Contains("Admin"))
                            {
                                return RedirectToAction("AdminDashboard", "Home");
                            }
                            else if (roles.Contains("Sub-Admin"))
                            {
                                return RedirectToAction("AdminDashboard", "Home");
                            }
                            else if (roles.Contains("PHQ Approver"))
                            {
                                return RedirectToAction("PHQAdminDashBoard", "Home");
                            }
                            else if (roles.Contains("Super Admin"))
                            {
                                return RedirectToAction("Dashboard", "Home");
                            }
                            else if (roles.Contains("General User"))
                            {
                                return RedirectToAction("Index", "Dashboard", new { empIdentityUserSessionToken = model.Name, Area = "Portfolio" });
                            }
                            else if(roles.Contains("ACR Entry Operator"))
                            {
                                return RedirectToAction("ACRDisiplinaryVerify", "Account", new { UserId = user.Id, Area = "Auth" });
                            }
                            else if (roles.Contains("Departmental User"))
                            {
                                return RedirectToAction("DepartmentalDashBoard", "PortfolioDashboard");
                            }
                            else if (roles.Contains("Portfolio Checker"))
                            {
                                return RedirectToAction("PortfolioCheckerDashBoard", "PortfolioDashboard");
                            }
                            else if (roles.Contains("Traning1"))
                            {
                                return RedirectToAction("Index", "TrainingSkill", new { Area = "Portfolio" });
                            }
                            else if (roles.Contains("Disciplinary Action Entry Operator"))
                            {
                                return RedirectToAction("DisciplinaryAndAction", "TrainingSkill", new { Area = "Portfolio" });
                            }
                            else if (roles.Contains("BPA"))
                            {
                                return RedirectToAction("BPAinfoList", "DisciplinaryAction", new { Area = "Employee" });
                            }
                            else if (roles.Contains("SB"))
                            {
                                return RedirectToAction("SBinfoList", "DisciplinaryAction", new { Area = "Employee" });
                            }
                            else if (roles.Contains("Medical"))
                            {
                                return RedirectToAction("EmployeeMedicalInfo", "TrainingSkill", new { Area = "Portfolio" });
                            }
                            else if (roles.Contains("PM1 Gradation"))
                            {
                                return RedirectToAction("Index", "EmployeeGradation", new { Area = "Employee" });
                            }
                            else
                            {
                                return RedirectToLocal(returnUrl);
                            }


                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                            return View(model);
                        }
                    }
                    else if (userInfos.isActive == 1 && roless.Contains("Departmental User"))
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                            var userAgent = Request.Headers["User-Agent"].ToString();
                            var mechineName = Environment.MachineName;

                            UserLogHistory userLog = new UserLogHistory
                            {
                                userId = model.Name,
                                logTime = DateTime.Now,
                                status = 1,
                                ipAddress = ip,
                                pcName = mechineName,
                                browserName = userAgent
                            };

                            await _accessLogHistoryService.SaveUserLogHistory(userLog);
                            var user = await _userManager.FindByIdAsync(userInfos.Id);
                            var roles = await _userManager.GetRolesAsync(user);
                            return RedirectToAction("DepartmentalDashBoard", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public IActionResult UserLogin(string returnUrl = null)
        {
            //LogInViewModel model = new LogInViewModel
            //{

            //};
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(LogInViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var userInfos = await userInfoes.GetUserInfoByUser(model.Name);
                if (userInfos != null)
                {
                    if (userInfos.isActive == 1 && (userInfos.LockoutEnd==null || userInfos.LockoutEnd<DateTime.Now))
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                            var userAgent = Request.Headers["User-Agent"].ToString();
                            var mechineName = Environment.MachineName;

                            UserLogHistory log = await _accessLogHistoryService.GetLastUserLogHistoryByUserId(model.Name);
                            if (log != null)
                            {
                                if (ip != log.ipAddress)
                                {
                                    var userInfo = await userInfoes.GetUserInfoByUser(model.Name);
                                    if (userInfo != null)
                                    {
                                        string newLine = Environment.NewLine;
                                        string mailMessage = @"Dear " + userInfo.bpNo + "," + newLine + "Your Portfolio Account has accessed with (" + ip + ") this IP address at " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "." + newLine +

                                        "Thanks";
                                        if (userInfo.Email != null)
                                        {
                                            //await _emailSenderService.SendEmail(userInfo.Email, "Security alert", mailMessage);
                                        }
                                    }
                                }
                            }

                            UserLogHistory userLog = new UserLogHistory
                            {
                                userId = model.Name,
                                logTime = DateTime.Now,
                                status = 1,
                                ipAddress = ip,
                                pcName = mechineName,
                                browserName = userAgent
                            };
                            await _accessLogHistoryService.SaveUserLogHistory(userLog);
                            var user = await _userManager.FindByIdAsync(userInfos.Id);
                            var roles = await _userManager.GetRolesAsync(user);
                            var empInfo = await _employeeService.GetEmployeeInfoById(userInfos.UserName);
                            
                            if (empInfo == null)
                            {
                                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                                return View(model);
                            }else if (roles.Contains("Departmental User"))
                            {
                                return RedirectToAction("DepartmentalDashBoard", "Home");
                            }else if (roles.Contains("Portfolio Checker"))
                            {
                                return RedirectToAction("PortfolioCheckerDashBoard", "Home");
                            }else if (roles.Contains("Reserve Office"))
                            {  
                                return RedirectToAction("EmployeeListHeadQuaterReserve", "InternalEmployee", new { Area = "InternalPosting" });
                            }
                            else
                            {
                                return RedirectToAction("Index", "Dashboard", new { empIdentityUserSessionToken = model.Name, Area = "Portfolio" });
                            }  
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                            return View(model);
                        }
                    }
                    else if (userInfos.isActive == 1 && userInfos.LockoutEnd > DateTime.Now)
                    {
                        var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        var userAgent = Request.Headers["User-Agent"].ToString();
                        var mechineName = Environment.MachineName;
                        var emp = await userInfoes.GetEmployeeInfoByUserName(model.Name);
                        var userinfo = await userInfoes.GetUserInfoBeforeRegister(model.Name);
                        UserLogHistory userLog = new UserLogHistory
                        {
                            userId = model.Name,
                            logTime = DateTime.Now,
                            status = 1,
                            ipAddress = ip,
                            pcName = mechineName,
                            browserName = userAgent,
                            statusName = "Redirect for verify"
                        };

                        await _accessLogHistoryService.SaveUserLogHistory(userLog);
                        ModelState.AddModelError(string.Empty, "Your account has been temporarily locked due to too many bad login attempts.Please try again in 5 minutes.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //[HttpPost]
        //public IActionResult Login()
        //{
        //    return View();
        //}



        [HttpGet]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Register()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel rolesModel = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(rolesModel);
            }
            ApplicationRoleViewModel model = new ApplicationRoleViewModel
            {
                roleViewModels = lstRole,
                ranks = _repoRank.GetAll()
        };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var userInfo = await _employeeService.GetEmployeeInfoById(model.BPNo);
            var userEmailCheck = await _userManager.FindByEmailAsync(model.Email);
            var userUserNameCheck = await _userManager.FindByNameAsync(model.UserName);
            var userBPCheck = await userInfoes.GetUserInfoByBP(model.BPNo);
            string username = HttpContext.User.Identity.Name;
            if (userEmailCheck != null || userUserNameCheck != null || userBPCheck != null)
            {
                var returnReason = "Failed";
                if (userEmailCheck != null)
                {
                    returnReason = "Email";
                }
                else if (userUserNameCheck!=null)
                {
                    returnReason = "UserName";
                }
                else if (userBPCheck != null)
                {
                    returnReason = "BpNo";
                }
                return Json(returnReason);
            }
            if (userInfo != null)
            {
                var user = new ApplicationUser { UserName = model.UserName, isActive = 1, Email = userInfo.emailAddressPersonal, bpNo = userInfo.employeeCode, PhoneNumber = userInfo.mobileNumberPersonal };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleAssign = await _userManager.AddToRoleAsync(user, model.RoleId);
                }
            }
            else if (userInfo == null)
            {
                var user = new ApplicationUser { UserName = model.UserName, isActive = 1, Email = model.Email, bpNo = model.BPNo, PhoneNumber = model.PhoneNumber };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleAssign= await _userManager.AddToRoleAsync(user, model.RoleId);
                    string otpnumber = RandomString(4);
                    userInfoes.DeleteUserInfoBeforeRegister(model.UserName);
                    var emp = new EmployeeInfo
                    {
                        nameEnglish = model.Name,
                        employeeCode = model.BPNo,
                        emailAddressPersonal = model.Email,
                        mobileNumberPersonal = model.PhoneNumber,
                        rankId=model.rankId,
                        ApplicationUserId= user.Id
                    };
                    var empSave = await _employeeService.SaveEmployeeInformation(emp);
                    var userInf = new UserInformation
                    {
                        userName = model.UserName,
                        bpNo = model.BPNo,
                        email = model.Email,
                        password=model.Password,
                        isVerified=0,
                        phoneNumber = model.PhoneNumber,
                        otpCode= otpnumber,
                        userRole= model.RoleId,
                        statusId=1
                    };
                    var userSave = await userInfoes.SaveUserInfo(userInf);
                    return Json("User Created");
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult UserRegister()
        {
            RegisterViewModel model = new RegisterViewModel
            {

            };
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            RegisterViewModel model = new RegisterViewModel
            {

            };
            return View(model);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                string username = HttpContext.User.Identity.Name;
                //var userinfo = await userInfoes.GetUserInfoByUser(username);
                var userinfo = await userInfoes.GetUserInfoByUser(model.Name);

                if (userinfo == null)
                {
                    string otpnumber = RandomString(4);
                    userInfoes.DeleteUserInfoBeforeRegister(model.Name);
                    UserInformation userInformation = new UserInformation
                    {
                        userName = model.Name,
                        email = model.Email,
                        phoneNumber = model.PhoneNumber,
                        isVerified = 0,
                        statusId = 1,
                        password = model.Password,
                        bpNo = model.Name,
                        userRole = "General User",
                        otpCode = otpnumber,
                    };
                    int UserId = await userInfoes.SaveUserInfo(userInformation);

                    if (userinfo != null)
                    {

                    }
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("UserOTPOption", new { id = UserId, Area = "Auth" });
                }
                else
                {
                    model.errorMsg = "This " + model.Name + " no already exist, Please try with another bp";
                    return View(model);
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserOTPOptionSend(string userId)
        {
            try
            {
                var userInfo = await userInfoes.GetApplicationUserByUserId(userId);

                RegisterViewModel model = new RegisterViewModel
                {
                    Email = userInfo.Email,
                    PhoneNumber = userInfo.PhoneNumber,
                    BPNo = userInfo.UserName
                };
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpGet]
        public async Task<IActionResult> UserOTPOption(int id, string email, string phone)
        {
            try
            {
                var userInfo = await userInfoes.GetUserInfoBeforeRegisterById(id);

                RegisterViewModel model = new RegisterViewModel
                {
                    userInformation = userInfo
                };
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> UserOTPOptionLocOut(int userId, string email, string phone)
        {
            try
            {
                var userInfo = await userInfoes.GetUserInfoBeforeRegisterById(userId);

                RegisterViewModel model = new RegisterViewModel
                {
                    userInformation = userInfo
                };
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> UserEmailVerification(int userId, int smsTypeId)
        {
            try
            {
                var userInfo = await userInfoes.GetUserInfoBeforeRegisterById(userId);
                //string newLine = Environment.NewLine;
                //if (smsTypeId == 1)
                //{
                //    string mailMessage = @"Dear " + userInfo.bpNo + "," + newLine + "Your OTP for BP Portfolio Account verification is " + userInfo.otpCode + "." + newLine + "Do not share it to anyone."+newLine+"" +
                        
                //                    "Thanks";
                //    await _emailSenderService.SendEmail(userInfo.email, "Email Verification for Portfolio", mailMessage);
                //}
                //else
                //{
                //    string smsMessage = string.Format(@"Dear {0}, Your OTP for BP Portfolio Account verification is {1}.Do not share it to anyone. Thanks", userInfo.bpNo, userInfo.otpCode);
                //    string otpNumber = userInfo.phoneNumber;
                //    if (!userInfo.phoneNumber.StartsWith("880"))
                //    {
                //        otpNumber = "88" + userInfo.phoneNumber;
                //    }
                //    else
                //    {
                //        otpNumber = userInfo.phoneNumber;
                //        userInfo.phoneNumber.Substring(0, 2);
                //    }
                    
                //    await _sMSService.SendSMSAsync(otpNumber, smsMessage);
                //}


                RegisterViewModel model = new RegisterViewModel
                {
                    userInformation = userInfo,
                    smsTypeId = smsTypeId
                };
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> ResendOTPCode(int ids, int smsTypeId)
        {
            var userInfo = await userInfoes.GetUserInfoBeforeRegisterById(ids);
            string otpCode = RandomString(4);
            userInfo.otpCode = otpCode;
            userInfo.statusId = 2;
            await userInfoes.SaveUserInfo(userInfo);
            string newLine = Environment.NewLine;
            if (smsTypeId == 1)
            {
                string mailMessage = @"Dear "+userInfo.bpNo+"," + newLine + "Your OTP for BP Portfolio Account verification is " + userInfo.otpCode + "." + newLine + "Do not share it to anyone." +
                    "Thanks";
                await _emailSenderService.SendEmail(userInfo.email, "BP Portfolio Account Verification.", mailMessage);
            }
            else
            {
                string smsMessage = string.Format(@"Dear {0},Your OTP for BP Portfolio Account verification is {1}.Do not share it to anyone. Thanks", userInfo.bpNo, userInfo.otpCode);
                string otpNumber = userInfo.phoneNumber;
                if (!userInfo.phoneNumber.StartsWith("880"))
                {
                    otpNumber = "88" + userInfo.phoneNumber;
                }
                else
                {
                    otpNumber = userInfo.phoneNumber;
                    userInfo.phoneNumber.Substring(0, 2);
                }
                await _sMSService.SendSMSAsync(otpNumber, smsMessage);
            }


            //RegisterViewModel model = new RegisterViewModel
            //{
            //    userInformation = userInfo,
            //    smsTypeId = smsTypeId
            //};
            //string mailMessage = @"Hi," + newLine + "Please use this verification code to complete your sign in: " + userInfo.otpCode  +" ."+newLine+""+
            //                     ". Thanks for helping us keep your account secure.\n" +
            //                     "The PHQ Team";
            //await _emailSenderService.SendEmail(userInfo.email, "Email verification test for development", mailMessage);

            return Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEmailVerification(UserVerificationModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var userInfo = await userInfoes.GetUserInfoBeforeRegisterById((int)model.UserId);
            RegisterViewModel registerView = new RegisterViewModel();
            if (ModelState.IsValid && model.UserOTPCode == userInfo.otpCode)
            {
                string username = HttpContext.User.Identity.Name;
                var user = new ApplicationUser { UserName = userInfo.bpNo, isActive = 1, Email = userInfo.email, bpNo = userInfo.bpNo, PhoneNumber = userInfo.phoneNumber };
                var result = await _userManager.CreateAsync(user, userInfo.password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "General User");
                    IdentityUser temp = await _userManager.FindByNameAsync(userInfo.bpNo);

                    EmployeeInfo employee = new EmployeeInfo
                    {
                        employeeCode = userInfo.bpNo,
                        ApplicationUserId = temp.Id,
                        mobileNumberPersonal = userInfo.phoneNumber,
                        emailAddressPersonal = userInfo.email,
                        employeeTypeId=1,
                        isApproved = 1
                    };
                    var empId= await _employeeService.SaveEmployeeInformation(employee);
                    var resultl = await _signInManager.PasswordSignInAsync(userInfo.bpNo, userInfo.password, false, lockoutOnFailure: true);
                    await _employeeService.SaveEmployeeTransectionHistoryLog(empId, 1, user.Id, "Portfolio", "");
                    return RedirectToAction("WelcomePage", "Account");
                }
                AddErrors(result);
            }
            else
            {
                registerView.userInformation = userInfo;
                registerView.smsTypeId = model.smsTypeId;
                registerView.errorMsg = "Invelid Code";
                
                //model.smsTypeId = model.smsTypeId;
                //model.UserId = model.UserId;
                //model.userInformation = userInfo;
                //model.errorMsg = "Invelid Code";
            }

            // If we got this far, something failed, redisplay form
            return View(registerView);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEmailVerificationforPassword(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var userInfo = await userInfoes.GetUserInfoBeforeRegisterById((int)model.UserId);
            if (ModelState.IsValid && model.UserOTPCode == userInfo.otpCode)
            {
                string username = HttpContext.User.Identity.Name;
                var appId = await _userManager.FindByNameAsync(model.Name);
                return RedirectToAction("ResetPassword", "Account", new { id = appId.Id });
            }
            else
            {
                model.userInformation = userInfo;
                model.verifyMessage = "Invelid Code";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserEmailVerificationLocOut(int userId, int smsTypeId)
        {
            try
            {
                var userInfo = await userInfoes.GetUserInfoBeforeRegisterById(userId);
                string newLine = Environment.NewLine;
                if (smsTypeId == 1)
                {
                    string mailMessage = @"Dear " + userInfo.bpNo + "," + newLine + "Your OTP for BP Portfolio Account verification is " + userInfo.otpCode + "." + newLine + "Do not share it to anyone." + newLine + "" +

                                    "Thanks";
                    await _emailSenderService.SendEmail(userInfo.email, "Email Verification for Portfolio", mailMessage);
                }
                else
                {
                    string smsMessage = string.Format(@"Dear {0}, Your OTP for BP Portfolio Account verification is {1}.Do not share it to anyone. Thanks", userInfo.bpNo, userInfo.otpCode);
                    string otpNumber = userInfo.phoneNumber;
                    if (!userInfo.phoneNumber.StartsWith("880"))
                    {
                        otpNumber = "88" + userInfo.phoneNumber;
                    }
                    else
                    {
                        otpNumber = userInfo.phoneNumber;
                        userInfo.phoneNumber.Substring(0, 2);
                    }

                    await _sMSService.SendSMSAsync(otpNumber, smsMessage);
                }
                RegisterViewModel model = new RegisterViewModel
                {
                    userInformation = userInfo,
                    smsTypeId = smsTypeId
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEmailVerificationLocOut(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var userInfo = await userInfoes.GetUserInfoBeforeRegisterById((int)model.UserId);
            var empInfo = await _employeeService.GetEmployeeInfoById(userInfo?.userName);
            if (ModelState.IsValid && model.UserOTPCode == userInfo.otpCode)
            {
                var removeLocTime = await userInfoes.RemoveUserLocoutTimeByUserName(userInfo?.userName);
                //return RedirectToAction("Index", "EmployeeInfo", new { empIdentityUserSessionToken = empInfo?.Id, Area = "Employee" });
                return RedirectToAction("Index", "Dashboard", new { empIdentityUserSessionToken = model.Name, Area = "Portfolio" });
            }
            else
            {
                model.userInformation = userInfo;
                model.verifyMessage = "Invelid Code";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            string userId = HttpContext.User.Identity.Name;
            await _signInManager.SignOutAsync();
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            var mechineName = Environment.MachineName;
            UserLogHistory userLog = new UserLogHistory
            {
                userId = userId,
                logTime = DateTime.Now,
                status = 1,
                ipAddress = ip,
                pcName = mechineName,
                browserName = userAgent
            };

            await _accessLogHistoryService.SaveUserLogHistory(userLog);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize(Roles = "Super Admin")]
        [HttpGet]
        public async Task<IActionResult> UserRoleCreate()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel model = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(model);
            }
            ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel
            {
                roleViewModels = lstRole
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserIdentityRoleCreate([FromForm] ApplicationRoleViewModel model)
        {
            var user = new ApplicationRole(model.RoleName);
            IdentityResult result = await _roleManager.CreateAsync(user);

            return RedirectToAction(nameof(UserRoleCreate));
        }

        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> AssaignRoleToUser()
        {
            string userName = User.Identity.Name;
            var roles = await _roleManager.Roles.Where(x => x.Name != "Super Admin").ToListAsync();
            var userInfo = await userInfoes.GetAllUserInfo();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel rolesModel = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(rolesModel);
            }

            ApplicationRoleViewModel model = new ApplicationRoleViewModel
            {
                roleViewModels = lstRole,
                userInfos = userInfo
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssaignRoleToUser([FromForm] ApplicationRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //var user = await _userManager.FindByNameAsync(model.userName);
            var user = await _userManager.FindByIdAsync(model.userId);
            await userInfoes.DeleteUserRoleListByUserId(user.Id);
            if (model.roleIdList.Count() > 0)
            {
                for (int i = 0; i < model.roleIdList.Count(); i++)
                {
                    await _userManager.AddToRoleAsync(user, model.roleIdList[i]);
                }
            }

            return RedirectToAction(nameof(AssaignRoleToUser));
        }

        #region ACR/Disiplenary
        [HttpGet]
        public async Task<IActionResult> ACRDisiplinaryVerify(string UserId)
        {
            try
            {
                //var empInfo = _employeeService.GetEmployeeInfoUserId(UserId);
                var user = await _userManager.FindByIdAsync(UserId);
                var userInfo = await userInfoes.GetUserInfoBeforeRegister(user.UserName);
                string newLine = Environment.NewLine;
                string smsMessage = string.Format(@"{0} is Your verivication code.", userInfo.otpCode);
                if (!userInfo.phoneNumber.StartsWith("880"))
                    userInfo.phoneNumber = "88" + userInfo.phoneNumber;
                await _sMSService.SendSMSAsync(userInfo.phoneNumber, smsMessage);


                RegisterViewModel model = new RegisterViewModel
                {
                    userInformation = userInfo,
                    smsTypeId = 2
                };
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ACRDisiplinaryVerify(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var userInfo = await userInfoes.GetUserInfoBeforeRegisterById((int)model.UserId);
            if (ModelState.IsValid && model.UserOTPCode == userInfo.otpCode)
            {                
                string otpnumber = RandomString(4);
                userInfo.otpCode = otpnumber;
                //userInfo.isVerified = 1;
                var userSave = await userInfoes.SaveUserInfo(userInfo);
                return RedirectToAction("Employeelist", "EmployeeProfile",new { area= "Employee" });
            }
            else
            {
                model.userInformation = userInfo;
                model.verifyMessage = "Invelid Code";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                //return Redirect(returnUrl);
                var userId = HttpContext.User.Identity.Name;

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                var userId = HttpContext.User.Identity.Name;
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion

        #region API
        [Route("global/api/getUserInfoList")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            return Json(await userInfoes.GetUserInfo());
        }

        [Route("global/api/GetUserRoleInfoByUserId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserRoleInfoByUserId(string Id)
        {
            return Json(await userInfoes.GetRoleListByUserId(Id));
        }
        

        [Route("global/api/PIMSData/{Id}")]
        [HttpGet]
        public async Task<IActionResult> PIMSData(string Id)
        {
            return Json(await _employeeService.GetPIMSDataModel(Id));
        }
        
        [HttpGet]
        public async Task<JsonResult> GetPIMSDataByBPNo(string bpNo)
        {
            if (bpNo.StartsWith("BP"))
            {
                bpNo = bpNo.Replace(" ","").Trim();
            }
            else
            {
                bpNo = "BP" + bpNo.Replace(" ", "").Trim();
            }
            
            string url = String.Format("https://pims.police.gov.bd:8443/pimslive/webpims/opus/bpdetails/{0}", bpNo);
            
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var smsData = await response.Content.ReadAsStringAsync();
            var pIMSBody = JsonConvert.DeserializeObject<PIMSBody>(await response.Content.ReadAsStringAsync());
            PIMSDataModel pIMS = new PIMSDataModel();
            if (pIMSBody.items.Count() > 0)
            {
                pIMS = pIMSBody.items[0];
            }
            else
            {
                pIMS = new PIMSDataModel();
            }
            
            return Json(pIMS);
        }

        

        #endregion

        #region Password Change/Deactive/Active/MultipleRole

        [HttpGet]
        public async Task<IActionResult> UserEmailVerificationforPassword(int smsTypeId,string UserId)
        {
            try
            {
                var empInfo =await _employeeService.GetEmployeeInfoById(UserId);
                var userInfo = await userInfoes.GetUserInfoBeforeRegister(UserId);
                string newLine = Environment.NewLine;
                if (smsTypeId == 1)
                {
                    string mailMessage = @"Hi," + newLine + "Please use this verification code to complete your sign in: " + userInfo.otpCode + "" + newLine + "" +

                                    ". Thanks for helping us keep your account secure.\n" +
                                    "The PHQ Team";
                    await _emailSenderService.SendEmail(userInfo.email, "Email verification test for development", mailMessage);
                }
                else
                {
                    string smsMessage = string.Format(@"{0} is Your Portfolio verification code.", userInfo.otpCode);
                    if (!userInfo.phoneNumber.StartsWith("880"))
                        userInfo.phoneNumber = "88" + userInfo.phoneNumber;
                    await _sMSService.SendSMSAsync(userInfo.phoneNumber, smsMessage);
                }


                RegisterViewModel model = new RegisterViewModel
                {
                    userInformation = userInfo,
                    smsTypeId = smsTypeId
                };
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        public IActionResult ResetPassword(string id)
        {


            ResetPasswordViewModel model = new ResetPasswordViewModel
            {
                applicationUserId = id
            };
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangePassword(ChangePsswordViewModel model)
        //{
        //    string message = "Fail To Update Password";
        //    if (ModelState.IsValid)
        //    {
        //        var data = await _userManager.ChangePasswordAsync(await _userManager.FindByNameAsync(HttpContext.User.Identity.Name), model.OldPassword, model.Password);
        //        message = data.ToString();
        //    }
        //    return RedirectToAction(nameof(HomeController.Index), "Home", new { Message = message });
        //}

        [HttpPost]
        public async Task<IActionResult> ChangePassword(RegisterViewModel model)
        {

            var result = "error";

            if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.Password))
            {
                var username = User.Identity.Name;

                var user = await _userManager.FindByNameAsync(username);

                await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                result = "success";
            }
            else
            {
                return Json(result);
            }

            return Json(result);
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userName = await userInfoes.GetApplicationUserByUserId(model.applicationUserId);
                    ApplicationUser user = await _userManager.FindByNameAsync(userName.UserName);
                    var result = await _userManager.RemovePasswordAsync(user);
                    var results = await _userManager.AddPasswordAsync(user, model.Password);

                    if (results.Succeeded)
                    {
                        TempData["Success"] = "Password Changed Successfully!";
                        return Json("Success");
                    }
                    else
                    {
                        AddErrors(results);
                    }
                }
                return Json("Failed");
            }
            catch (Exception)
            {
                return Json("Try again");
            }
        }

        public async Task<IActionResult> UserStatusChange(string userId)
        {
            var status = "Failed";
            var user = await userInfoes.GetApplicationUserByUserId(userId);
            if (user.isActive == 1)
            {
                var results = await userInfoes.UserInactiveById(user.Id);
                if (results == 1)
                {
                    status = "Success";
                }
            }
            else
            {
                var results = await userInfoes.UserActiveById(user.Id);
                if (results == 1)
                {
                    status = "Success";
                }
            }
            return Json(status);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AssaignMultipleRoleToUser([FromForm] ApplicationRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var user = await _userManager.FindByIdAsync(model.userId);
                await userInfoes.DeleteUserRoleListByUserId(user.Id);
                if (model.roleIdList.Count() > 0)
                {
                    for (int i = 0; i < model.roleIdList.Count(); i++)
                    {
                        await _userManager.AddToRoleAsync(user, model.roleIdList[i]);
                    }
                }
                return Json("Success");
            }
            catch (Exception)
            {

                return Json("Failed");
            }
        }
        #endregion

        #region Forget Password
        [Route("global/api/GetEmployeeInfoByUserName")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoByUserName(string userName)
        {
            var usernaname = await userInfoes.GetEmployeeInfoByUserName(userName);
            return Json(usernaname);
        }

        #endregion

        #region Lock Confirm
        [HttpGet]
        public async Task<IActionResult> UserEmailVerificationForLock(int smsTypeId, string UserId)
        {
            try
            {
                var empInfo =await _employeeService.GetEmployeeInfoById(UserId);
                var userInfo = await userInfoes.GetUserInfoByUser(User.Identity.Name);
                string otpnumber = RandomString(4);
                userInfo.otpCode = otpnumber;
                var save = await userInfoes.SaveApplicationUserInfo(userInfo);
                string newLine = Environment.NewLine;
                if (smsTypeId == 1)
                {
                    string mailMessage = @"Hi," + newLine + "Please use this verification code to complete lock: " + userInfo.otpCode + "" + newLine + "" +

                                    ". Thanks for helping us keep your account secure.\n" +
                                    "The PHQ Team";
                    await _emailSenderService.SendEmail(userInfo.Email, "OTP Verification", mailMessage);
                }
                else
                {
                    string smsMessage = string.Format(@"{0} is Your verification code.", otpnumber);
                    if (!userInfo.PhoneNumber.StartsWith("880"))
                        userInfo.PhoneNumber = "88" + userInfo.PhoneNumber;
                    await _sMSService.SendSMSAsync(userInfo.PhoneNumber, smsMessage);
                }

                return Json("Send");
            }
            catch (Exception ex)
            {
                return Json("Send");
                //return Json(ex.Message);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> OTPVerificationLock(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var userInfo = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            if (model.UserOTPCode == userInfo.otpCode)
            {
                userInfo.otpCode = null;
                var save = await userInfoes.SaveApplicationUserInfo(userInfo);
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _assignmentMaster.Get(Convert.ToInt32(model.UserId));
                data.statusId = 3;
                _assignmentMaster.Update(data);
                await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _employeeService.UpdateAssignmentInfoForIGP(data.Id);

                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    isActive = 2,
                    notes = model.verifyMessage,
                };
                _approvalLog.Insert(approvalLog);
                return Json("Save");
                //return RedirectToAction("AssignmentMasterPostUpdate", "Assignment", new { area= "Employee", id = model.UserId, comment = model.verifyMessage });
            }

            // If we got this far, something failed, redisplay form
            return Json("Fail");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OTPVerificationLockInternal(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var userInfo = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            if (model.UserOTPCode == userInfo.otpCode)
            {
                userInfo.otpCode = null;
                var currentuser = await _userManager.FindByNameAsync(User.Identity.Name);
                var data = _repoInternalAssignmentMaster.Get(Convert.ToInt32(model.UserId));
                data.statusId = 4;
                _repoInternalAssignmentMaster.Update(data);
                int type = 1;
                if (data.assignmentTypeId == 2)
                {
                    type = 2;
                }
                //await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _internalServices.UpdateAssignmentInfoForIGP(data.Id);

                InternalApprovalLog approvalLog = new InternalApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    isActive = 2,
                    notes = model.verifyMessage,
                };
                _repoInternalApprovalLog.Insert(approvalLog);

                InternalAssignmentMaster internalAssignmentMaster = new InternalAssignmentMaster
                {
                    applicationUserId = data.ApplicationUserId,
                    refNo = data.refNo,
                    refDate = Convert.ToDateTime(data.refDate),
                    statusId = 4,
                    internalEnlistedAssignmentMasterId = data.Id,
                };
                int masterid = await _internalServices.SaveInternalAssignmentMaster(internalAssignmentMaster);
                var save = await userInfoes.SaveApplicationUserInfo(userInfo);

                var details = await _internalServices.GetInternalEnlistedAssignmentsByMasterId(Convert.ToInt32(model.UserId));

                foreach (var item in details)
                {
                    InternalAssignment internalAssignment = new InternalAssignment
                    {
                        assignmentMasterId = masterid,
                        employeeId = item.employeeId,
                        departmentId = item.sectionId,
                        receiveRefNo = item.remarks,
                        assignmentTypeId = type,
                        statusId = 4
                    };
                    _repoInterAssignment.Insert(internalAssignment);
                }
                return Json("Save");
                //return RedirectToAction("AssignmentMasterPostUpdate", "Assignment", new { area= "Employee", id = model.UserId, comment = model.verifyMessage });
            }
            // If we got this far, something failed, redisplay form
            return Json("Fail");
        }
        #endregion
    }
}


