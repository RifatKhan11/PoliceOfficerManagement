using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models.Auth;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Web.Api.Models;
using AlphaManagement.Web.Helpers.Errors;
using AlphaManagement.Web.JWT_Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlphaManagement.Web.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtFactoryService _jwtFactory;
        private readonly IEmployeeService _employeeService;
        private readonly IRepository<MobileDeviceInformation> _deviceInfo;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<SpecialBranchUnit> _specialBranchUnit;
        private readonly IRepository<BCSBatch> _bCSBatch;

        public AuthController(UserManager<ApplicationUser> userManager,  SignInManager<ApplicationUser> signInManager, IJwtFactoryService jwtFactory, IEmployeeService employeeService, IRepository<MobileDeviceInformation> deviceInfo
            , IRepository<Rank> repoRank, IRepository<SpecialBranchUnit> specialBranchUnit, IRepository<BCSBatch> bCSBatch)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtFactory = jwtFactory;
            _employeeService = employeeService;
            _deviceInfo = deviceInfo;
            _repoRank = repoRank;
            _specialBranchUnit = specialBranchUnit;
            _bCSBatch = bCSBatch;
        }

        //POST: api/Auth/LogIn
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] LogInViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.Name);

            if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                var roles = await _userManager.GetRolesAsync(user);
                //var sinRole = roles.FirstOrDefault();
                //var response = new
                //{
                //    auth_token = await _jwtFactory.GenerateToken(user.UserName, user.Id, roles)
                //};

                var jwt = JsonConvert.SerializeObject(await _jwtFactory.GenerateToken(user.UserName, user.Id, roles));
                var employee = await _employeeService.GetPhotographByUserId(user.Id);

                var obj = new ReturnObject
                {
                    jwt = jwt.Replace("\"", ""),
                    userInfo = user,
                    role = roles.FirstOrDefault(),
                    employeeData=employee
                };

                return new OkObjectResult(obj);
            }

            return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
        }

        //POST: api/Auth/UserWiseDeviceInfoAdd
        [HttpPost]
        public async Task<IActionResult> UserWiseDeviceInfoAdd(DeviceInfoMiewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.userName);
                MobileDeviceInformation mobileDevice = new MobileDeviceInformation
                {
                    ApplicationUserId = user.Id,
                    deviceId = model.deviceId
                };
                _deviceInfo.Insert(mobileDevice);
                return Ok("Success");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //api/Auth/GetUnitRankBatchList
        [HttpGet]
        public IActionResult GetUnitRankBatchList()
        {
            var data = new MasterDataViewModel
            {
                branchUnits = _specialBranchUnit.GetAll().OrderBy(x => x.Id),
                ranks = _repoRank.GetAll().OrderBy(x => x.shortOrder),
                bCSBatches = _bCSBatch.GetAll().OrderBy(x => x.Id)
            };

            return Ok(data);
        }

    }
}