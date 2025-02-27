using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.Auth;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;
using AlphaManagement.Domain.RepositoryService.Interfaces;
using AlphaManagement.Domain.AuthService.Interfaces;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.EmployeeService.Interfaces;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Web.Api.Models;
using AlphaManagement.Web.Areas.Employee.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AlphaManagement.Web.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AssignmentApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IAssignmentService _assignmentService;
        private readonly IUserInfoes userInfoes;
        private readonly IInternalPostingServices _internalPostingServices;
        private readonly IRepository<Rank> _repoRank;
        private readonly IRepository<SpecialBranchUnit> _specialBranchUnit;
        private readonly IRepository<BCSBatch> _bCSBatch;
        private readonly IRepository<AssignmentMaster> _assignmentMaster;
        private readonly IRepository<ApprovalLog> _approvalLog;
        private readonly IRepository<Assignment> _repoAssignment;
        private readonly IRepository<InternalEnlistedAssignmentMaster> _repoInternalAssignmentMaster;
        private readonly IRepository<InternalApprovalLog> _repoInternalApprovalLog;
        private readonly IRepository<InternalAssignment> _repoInterAssignment;
        private readonly IRepository<InternalEnlistedAssignment> _repoInternalAssignment;
        private readonly IRepository<AssignmentTransectionLog> _AssignmentTransectionLog;
        private IMemoryCache cache;

        public AssignmentApiController(UserManager<ApplicationUser> userManager, IEmployeeService employeeService, IUserInfoes userInfoes, IRepository<InternalEnlistedAssignment> repoInternalAssignment,
            IInternalPostingServices internalPostingServices, IRepository<Rank> repoRank, IRepository<SpecialBranchUnit> specialBranchUnit, IRepository<InternalAssignment> repoInterAssignment
            , IRepository<BCSBatch> bCSBatch, IAssignmentService assignmentService, IRepository<AssignmentMaster> assignmentMaster, IRepository<InternalApprovalLog> repoInternalApprovalLog
            , IRepository<ApprovalLog> approvalLog, IRepository<Assignment> repoAssignment, IRepository<InternalEnlistedAssignmentMaster> repoInternalAssignmentMaster
            , IRepository<AssignmentTransectionLog> AssignmentTransectionLog, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _employeeService = employeeService;
            this.userInfoes = userInfoes;
            _internalPostingServices = internalPostingServices;
            _repoRank = repoRank;
            _specialBranchUnit = specialBranchUnit;
            _bCSBatch = bCSBatch;
            _assignmentService = assignmentService;
            _assignmentMaster = assignmentMaster;
            _approvalLog = approvalLog;
            _repoAssignment = repoAssignment;
            _repoInternalAssignmentMaster = repoInternalAssignmentMaster;
            _repoInternalApprovalLog = repoInternalApprovalLog;
            _repoInterAssignment = repoInterAssignment;
            _repoInternalAssignment = repoInternalAssignment;
            _AssignmentTransectionLog = AssignmentTransectionLog;
            cache = memoryCache;
        }

        //[Authorize(Policy = "AlphaMobile")]
        [HttpGet]
        public async Task<IActionResult> IGPDashboard()
        {
            DashboardModel model = new DashboardModel
            {
                onGoing = await _employeeService.AssignmentPostedMastersForApproveCount(),
                returned = await _employeeService.AssignmentMasterLockedCount(4),
                registration = await _employeeService.AssignmentMasterLockedCount(3),

                internalongoing = await _internalPostingServices.PendingEnlistMastersForIgpCount(),
                internalApproved = await _internalPostingServices.AssignmentInternalReturnAndLockCount(4),
                internalReturn = await _internalPostingServices.AssignmentInternalReturnAndLockCount(5),
            };
            return Ok(model);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> Dashboard(string userName)
        {
            if (userName == "IGP")
            {
                DashboardModel model = new DashboardModel
                {
                    onGoing = await _employeeService.AssignmentPostedMastersForApproveCount(),
                    returned = await _employeeService.AssignmentMasterLockedCount(4),
                    registration = await _employeeService.AssignmentMasterLockedCount(3),

                    internalongoing = await _internalPostingServices.PendingEnlistMastersForIgpCount(),
                    internalApproved = await _internalPostingServices.AssignmentInternalReturnAndLockCount(4),
                    internalReturn = await _internalPostingServices.AssignmentInternalReturnAndLockCount(5),
                };
                return Ok(model);
            }
            else
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(userName);
                var enlistedMasterAssignments = await _employeeService.EnListedMasterDetails(applicationUser.Id);

                var emp = await userInfoes.GetUserInfoByUserName(userName);
                DashboardModel model = new DashboardModel
                {
                    onGoing = await userInfoes.GetRankWiseAssignmentCopyOngoingCount((int)emp.rankId),
                    finalSubmit = await _employeeService.AssignmentPostedMastersApproveCount(applicationUser.Id),
                    returned = await _employeeService.AssignmentReturnListCount(applicationUser.Id),
                    registration = enlistedMasterAssignments.Count(),

                    internalongoing = await userInfoes.GetRankWiseInternalAssignmentCopyOngoingCount((int)emp.rankId),
                    internalApproved = await _internalPostingServices.ApprovedInternalAssignmentMastersListCount(applicationUser.Id),
                    internalReturn = await _internalPostingServices.AssignmentReturnListCount(applicationUser.Id),
                };
                return Ok(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AssignmentMasterPostUpdate(AssignmentPostModel model)
        {

            try
            {
                var currentuser = await _userManager.FindByNameAsync(model.user);
                var data = _assignmentMaster.Get(model.id);
                data.statusId = 3;
                _assignmentMaster.Update(data);
                await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _employeeService.UpdateAssignmentInfoForIGP(data.Id);

                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    isActive = 2,
                    notes = model.comment,
                };
                _approvalLog.Insert(approvalLog);

                return Ok("Success");

            }
            catch (Exception Ex)
            {
                return Ok(Ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentMasterReturn(AssignmentReturnModel model)
        {
            var update = await _employeeService.ReturnAssignmentMaster(model.id);
            var currentuser = await _userManager.FindByNameAsync(model.loggedUser);
            var data = _assignmentMaster.Get(model.id);
            var nextuser = await _userManager.FindByIdAsync(model.returnTo);
            _employeeService.UpdateApprovalLogBymasterId(data.Id);

            ApprovalLog approvalLog = new ApprovalLog
            {
                masterId = data.Id,
                userId = currentuser.Id,
                isActive = 3,
                nextApprovarId = nextuser.Id,
                notes = model.comment,
            };
            _approvalLog.Insert(approvalLog);

            if (update > 0)
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Fail");
            }
        }

        [HttpGet("{id}")]
        public IActionResult AssignmentDeletefromOgoing(int id)
        {
            try
            {
                var update = _repoAssignment.Get(id);
                _repoAssignment.Delete(update);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignmentComment(AssignmentPostModel model)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentById(model.id);
                assignments.Remarks = model.comment;
                _repoAssignment.Update(assignments);
                return Ok("Success");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> ProposalListForBP(string userName)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }

            var data = await _employeeService.AssignmentPostedMastersForApprove(userId);

            return Ok(data);
        }



        [HttpGet("{userName}/{id}")]
        public async Task<IActionResult> BPPostingProposalDetailsById(string userName, int id)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var data = new PostingProposalModel
            {
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                aspNetUsersViewModels = await userInfoes.GetUserInfoForAlpha(),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id)
            };
            data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();

            return Ok(data);
        }

        //api/AssignmentApi/OfficeOrderList/{userName}
        [HttpGet("{userName}")]
        public async Task<IActionResult> OfficeOrderList(string userName)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin" || roles.FirstOrDefault() == "IGP")
            {
                userId = "";
            }

            var data = await _employeeService.ApprovedAssignmentMasters(userId);

            return Ok(data);
        }

        //api/AssignmentApi/OfficeOrderList/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> PostingorderReportView(int id)
        {
            PostingOrderReportModel model = new PostingOrderReportModel
            {
                assignmentAnulipis = await _employeeService.AssignmentMastersRopo(id),
                assignments = await _employeeService.AssignmentPostedByMasterId(id),
            };
            return Ok(model);
        }

        #region PHQ Section

        //api/AssignmentApi/ProposalListForPHQ/igp
        [HttpGet("{userName}")]
        public async Task<IActionResult> ProposalListForPHQ(string userName)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            if (roles.FirstOrDefault() == "Super Admin")
            {
                userId = "";
            }

            var data = await _internalPostingServices.PendingEnlistMastersForIgp(userId);

            return Ok(data);
        }

        //api/AssignmentApi/InternalAssignmentPostingDetails/igp/4
        [HttpGet("{userName}/{id}")]
        public async Task<IActionResult> InternalAssignmentPostingDetails(string userName, int id)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var details = await _internalPostingServices.AssignmentDetailsModalByMasterId(id);
                var empInfo = await _internalPostingServices.GetAssignmentMasterEmployeeInfoById(id);
                var userList = await userInfoes.GetUserInfoForAlpha();
                var reportView = await _internalPostingServices.GetApprovalLogFullByMasterId(id);
                var data = new InternalPostingAPIModel
                {
                    assignmentDetailsModals = details,
                    assignMasterId = id,
                    userInfo = user,
                    employeeInfo = empInfo,
                    aspNetUsersViewModels = userList,
                    postingReportViews = reportView
                };
                data.aspNetUsersViewModels = data.aspNetUsersViewModels.Where(x => x.aspnetId != user.Id).ToList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //POST: api/AssignmentApi/PHQAssignmentMasterReturn
        [HttpPost]
        public async Task<IActionResult> PHQAssignmentMasterReturn(AssignmentReturnModel model)
        {
            var update = await _employeeService.ReturnInternalAssignmentMaster(model.id);
            var currentuser = await _userManager.FindByNameAsync(model.loggedUser);
            var data = _repoInternalAssignmentMaster.Get(model.id);
            var nextuser = await _userManager.FindByIdAsync(model.returnTo);
            _employeeService.UpdateInternalApprovalLogBymasterId(data.Id);

            InternalApprovalLog approvalLog = new InternalApprovalLog
            {
                masterId = data.Id,
                userId = currentuser.Id,
                isActive = 3,
                nextApprovarId = nextuser.Id,
                notes = model.comment,
            };
            _repoInternalApprovalLog.Insert(approvalLog);

            if (update > 0)
            {
                return Ok("update");
            }
            else
            {
                return Ok("Fail");
            }
        }

        //POST: api/AssignmentApi/LockInternalAssignment
        [HttpPost]
        public async Task<IActionResult> LockInternalAssignment(AssignmentPostModel model)
        {
            try
            {
                var currentuser = await _userManager.FindByNameAsync(model.user);
                var data = _repoInternalAssignmentMaster.Get(model.id);
                data.statusId = 4;
                _repoInternalAssignmentMaster.Update(data);
                int type = 1;
                if (data.assignmentTypeId == 2)
                {
                    type = 2;
                }
                var update = await _internalPostingServices.UpdateAssignmentInfoForIGP(data.Id);

                InternalApprovalLog approvalLog = new InternalApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    isActive = 2,
                    notes = model.comment,
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
                int masterid = await _internalPostingServices.SaveInternalAssignmentMaster(internalAssignmentMaster);

                var details = await _internalPostingServices.GetInternalEnlistedAssignmentsByMasterId(model.id);

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
                return Ok("Lock");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        //POST: api/AssignmentApi/PHQAssignmentComment
        [HttpPost]
        public async Task<IActionResult> PHQAssignmentComment(AssignmentPostModel model)
        {
            try
            {
                var assignments = await _internalPostingServices.GetAssignmentById(model.id);
                assignments.remarks = model.comment;
                _repoInternalAssignment.Update(assignments);
                return Ok("save");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        //api/AssignmentApi/InternalAssignmentDeletefromOgoing/{id}
        [HttpGet("{id}")]
        public IActionResult InternalAssignmentDeletefromOgoing(int id)
        {
            try
            {
                var update = _repoInternalAssignment.Get(id);
                _repoInternalAssignment.Delete(update);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //api/AssignmentApi/PHQAssignmentMasterApproveList/{userName}
        [HttpGet("{userName}")]
        public async Task<IActionResult> PHQAssignmentMasterApproveList(string userName)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            string userId = applicationUser.Id;
            var data = await _internalPostingServices.ApprovedInternalAssignmentMastersListForIgp(4);
           // var data = await _internalPostingServices.ApprovedInternalAssignmentMastersList(userId);
            return Ok(data);
        }

        //api/AssignmentApi/PHQAssignmentReturnList/{userName}
        [HttpGet("{userName}")]
        public async Task<IActionResult> PHQAssignmentReturnList(string userName)
        {
            var data = await _internalPostingServices.ApprovedInternalAssignmentMastersListForIgp(5);
            //ApplicationUser applicationUser = await _userManager.FindByNameAsync(userName);
            //var roles = await _userManager.GetRolesAsync(applicationUser);
            //string userId = applicationUser.Id;
            //if (roles.FirstOrDefault() == "Super Admin")
            //{
            //    userId = "";
            //}
            //var data = await _internalPostingServices.AssignmentReturnList(userId);
            return Ok(data);
        }

        //api/AssignmentApi/PHQAssignmentMasterUpdateUndo/
        [HttpPost]
        public async Task<IActionResult> PHQAssignmentMasterUpdateUndo(AssignmentPostModel model)
        {
            try
            {
                var aplog = _repoInternalApprovalLog.Get(model.id);
                var currentuser = await _userManager.FindByNameAsync(model.user);
                var data = _repoInternalAssignmentMaster.Get((int)aplog.masterId);
                var userinfo = await _userManager.FindByIdAsync(aplog.nextApprovarId);
                var Createuserinfo = await _userManager.FindByIdAsync(aplog.userId);
                _internalPostingServices.UpdateApprovalLogBymasterId((int)aplog.masterId);
                if (Createuserinfo.UserName == "IGP")
                {
                    data.statusId = 3;
                }
                else if (Createuserinfo.UserName == "PHQ Approver")
                {
                    data.statusId = 1;
                }
                else
                {
                    data.statusId = 2;
                }
                InternalApprovalLog approvalLog = new InternalApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = currentuser.Id,
                    isActive = 1,
                    notes = model.comment,
                    sequenseNo = 1
                };
                _repoInternalApprovalLog.Insert(approvalLog);
                _repoInternalAssignmentMaster.Update(data);

                return Ok("save");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion
        //api/AssignmentApi/GetUnitRankBatchList
        [HttpGet]
        public IActionResult GetUnitRankBatchList()
        {
            var data = new MasterDataViewModel
            {
                branchUnits=_specialBranchUnit.GetAll().OrderBy(x=>x.Id),
                ranks=_repoRank.GetAll().Where(x=>x.rankCode=="Officer").OrderBy(x=>x.shortOrder),
                bCSBatches=_bCSBatch.GetAll().OrderBy(x=>x.Id)
            };

            return Ok(data);
        }

        [HttpGet("{unitId}/{rankId}")]
        public async Task<IActionResult> GetPositionVacency(int unitId, int rankId)
        {
            return Ok(await _assignmentService.GetPositionVacency(rankId, unitId));
        }

        [AllowAnonymous]
        //api/AssignmentApi/GetEmployeeInfoByBP/'880'
        [HttpGet("{bpNo}")]
        public async Task<IActionResult> GetEmployeeInfoByBP(string bpNo)
        {
            var result = await _employeeService.GetEmployeeInfoByBP(bpNo);
            return Ok(result);
        }

        [HttpGet("{unitId}/{rankId}/{batchId}")]
        public async Task<IActionResult> GetOverDueEmployeeInfoListFilter(int unitId, int rankId, int batchId)
        {
            return Ok(await _employeeService.GetOverDueEmployeeInfoListFilter(rankId, unitId, batchId));
        }

        [HttpGet("{rankId}/{unitId}/{batch}/{servicePeriod}/{bandId}/{isLocked}/{isAttached}/{isUnMission}/{isPRL}/{skillId}")]
        public async Task<IActionResult> GetAllOfficerInfoSearchByFiltering(int rankId, int unitId, int batch, int servicePeriod, int bandId, int isLocked, int isAttached, int isUnMission, int isPRL, int skillId)
        {
            try
            {
                var data = await _assignmentService.GetUnitRankWiseEmployeeList(rankId, unitId, batch, servicePeriod, bandId, isLocked, isAttached, isUnMission, isPRL, skillId);
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //api/AssignmentApi/GetAllOfficerInfoSearchByFiltering/{rankId}/{unitId}/{batchId}
        [HttpGet("{rankId}/{unitId}/{batchId}")]
        public IActionResult GetOfficerSearchInformation(int rankId, int unitId, int batchId)
        {
            try
            {
                var data = _employeeService.GetOfficerSearchInformation(unitId,rankId,batchId,"");

                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //api/AssignmentApi/LockedAssignmentApi 
        [HttpGet]
        public async Task<IActionResult> LockedAssignmentApi()
        {
            var result = await _employeeService.AssignmentMasterLocked(3);
            return Ok(result);
        }

        //api/AssignmentApi/AssignmentDetailsDataPreview/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> AssignmentDetailsDataPreview(int id)
        {
            var data = new PostingProposalModel
            {
                assignmentDetailsModals = await _employeeService.AssignmentDetailsModalByMasterId(id),
                postingReportViews = await _employeeService.GetApprovalLogFullByMasterId(id)
            };
            return Ok(data);
        }

        //api/AssignmentApi/ReturnedAssignmentData
        [HttpGet]
        public async Task<IActionResult> ReturnedAssignmentData()
        {
            var data = await _employeeService.AssignmentMasterLocked(4);
            
            return Ok(data);
        }

        //api/AssignmentApi/LockedAssignmentUndo
        [HttpPost]
        public async Task<IActionResult> LockedAssignmentUndo(AssignmentPostModel model)
        {

            try
            {
                var currentuser = await _userManager.FindByNameAsync(model.user);
                var data = _assignmentMaster.Get(model.id);
                if (data.statusId == 4)
                {
                    _employeeService.UpdateApprovalLogBymasterId(data.Id);
                }
                data.statusId = 2;
                _assignmentMaster.Update(data);
                //await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _employeeService.UpdateAssignmentInfoForIGPUndo(data.Id);

                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = currentuser.Id,
                    isActive = 1,
                    notes = "Undo From IGP sir",
                    sequenseNo = 1
                };
                _approvalLog.Insert(approvalLog);

                //return RedirectToAction("AssignmentPostMaster", "Assignment");
                return Ok("save");

            }
            catch (Exception Ex)
            {
                return Ok(Ex.Message);
            }
        }

        //api/AssignmentApi/ReturnedAssignmentUndo
        [HttpPost]
        public async Task<IActionResult> ReturnedAssignmentUndo(AssignmentPostModel model)
        {

            try
            {
                var currentuser = await _userManager.FindByNameAsync(model.user);
                var data = _assignmentMaster.Get(model.id);
                if (data.statusId == 4)
                {
                    _employeeService.UpdateApprovalLogBymasterId(data.Id);
                }
                data.statusId = 2;
                _assignmentMaster.Update(data);
                //await _employeeService.DeleteAssignmentsPriviousLockByMasterId(data.Id);
                var update = await _employeeService.UpdateAssignmentInfoForIGPUndo(data.Id);

                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = data.Id,
                    userId = currentuser.Id,
                    nextApprovarId = currentuser.Id,
                    isActive = 1,
                    notes = "Undo From IGP sir",
                    sequenseNo = 1
                };
                _approvalLog.Insert(approvalLog);

                //return RedirectToAction("AssignmentPostMaster", "Assignment");
                return Ok("save");

            }
            catch (Exception Ex)
            {
                return Ok(Ex.Message);
            }
        }

        //api/AssignmentApi/GetCentralUnitListForChange/{refNum}/{empId}
        [HttpGet("{refNum}/{empId}")]
        public async Task<IActionResult> GetCentralUnitListForChange(string refNum, int empId)
        {
            var enListed = await _assignmentService.GetEnlistedAssignmentOngoing(refNum);
            var empRnk = _employeeService.GetBasicEmployeeInfoById(empId);

            var data = new List<BranchUnitWiseEmployeesModel>();
            if (!cache.TryGetValue(refNum, out data))
            {
                data = await _assignmentService.GetUnitEmployeeByRankUnitWithOutEmp(refNum, Convert.ToInt32(empRnk.rankId), 0, 0);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(700));

                cache.Set(refNum, data, cacheEntryOptions);
            }

            CentralPostingChangeListModel model = new CentralPostingChangeListModel
            {
                branchUnitWiseEmployees = data,
                enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
                presentUnits = await _employeeService.GetEmployeePresentUnit(empId),
                homeDistUnits = await _employeeService.GetEmployeePermanentAddUnit(empId),
                previousUnits = await _employeeService.GetEmployeePreviousUnit(empId),
                spouseUnits = await _employeeService.GetEmployeeSpouseUnit(empId),
            };
            return Ok(model);
        }

        //api/AssignmentApi/GetCentralUnitListForChangeAutoSuggestionForAndroid/{int:empId}/{int:pageNumber}
        [HttpGet("{empId}/{pageNumber}")]
        public async Task<IActionResult> GetCentralUnitListForChangeAutoSuggestionForAndroid(int empId, int pageNumber)
        {
            var suggestedData = await _assignmentService.GetSuggestedPlaceForEmp(empId, pageNumber, 10, "");
            List<MainSuggestedBranch> mainSuggesteds = new List<MainSuggestedBranch>();
            if (suggestedData.FirstOrDefault().specialBranchUnitId != 0)
            {
                foreach (var data in suggestedData.GroupBy(x => x.specialBranchUnitId))
                {
                    List<SubSuggestedBranch> subSuggesteds = new List<SubSuggestedBranch>();
                    foreach (var item in suggestedData.Where(x => x.specialBranchUnitId == data.Key))
                    {
                        SubSuggestedBranch sub = new SubSuggestedBranch
                        {
                            unitId = item.unitId,
                            subUnit = item.subUnit,
                            numOfPost = item.numOfPost,
                            totalEmployee = item.totalEmployee,
                            vacantPost = item.vacantPost,
                            specialBranchUnitId = item.specialBranchUnitId,
                            isParent = item.isParent
                        };
                        subSuggesteds.Add(sub);
                    }

                    MainSuggestedBranch main = new MainSuggestedBranch
                    {
                        specialBranchUnitId = data.Key,
                        mainUnit = suggestedData.Where(x => x.specialBranchUnitId == data.Key).FirstOrDefault().mainUnit,
                        isParent = suggestedData.Where(x => x.specialBranchUnitId == data.Key).FirstOrDefault().isParent,
                        totalEmployee = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.totalEmployee),
                        vacantPost = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.vacantPost),
                        numOfPost = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.numOfPost),
                        subSuggestedBranches = subSuggesteds
                    };
                    mainSuggesteds.Add(main);
                }
            }
            else
            {
                foreach (var data in suggestedData)
                {
                    MainSuggestedBranch main = new MainSuggestedBranch
                    {
                        specialBranchUnitId = data.unitId,
                        mainUnit = data.mainUnit,
                        isParent = data.isParent,
                        totalEmployee = data.totalEmployee,
                        vacantPost = data.vacantPost,
                        numOfPost = data.numOfPost,
                        subSuggestedBranches = new List<SubSuggestedBranch>()
                    };
                    mainSuggesteds.Add(main);
                }
            }
            
            //var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            CentralPostingAutoSuggestionListModel model = new CentralPostingAutoSuggestionListModel
            {
                mainSuggestedBranches= mainSuggesteds,
                //suggestedBrancheds = await _assignmentService.GetSuggestedPlaceForEmp(empId, pageNumber, 10, ""),
                //enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
            };
            return Ok(model);
        }

        //api/AssignmentApi/GetCentralUnitListForChangeAutoSuggestion/{int:empId}/{int:pageNumber}
        [HttpGet("{empId}/{pageNumber}")]
        public async Task<IActionResult> GetCentralUnitListForChangeAutoSuggestion(int empId, int pageNumber)
        {
            var suggestedData = await _assignmentService.GetSuggestedPlaceForEmp(empId, pageNumber, 10, "");
            List<MainSuggestedBranch> mainSuggesteds = new List<MainSuggestedBranch>();
            if (suggestedData.FirstOrDefault().specialBranchUnitId != 0)
            {
                foreach (var data in suggestedData.GroupBy(x => x.specialBranchUnitId))
                {
                    List<SubSuggestedBranch> subSuggesteds = new List<SubSuggestedBranch>();
                    if (suggestedData.Where(x => x.specialBranchUnitId == data.Key).Count() > 0)
                    {
                        foreach (var item in suggestedData.Where(x => x.specialBranchUnitId == data.Key))
                        {
                            SubSuggestedBranch sub = new SubSuggestedBranch
                            {
                                unitId = item.unitId,
                                subUnit = item.subUnit,
                                numOfPost = item.numOfPost,
                                totalEmployee = item.totalEmployee,
                                vacantPost = item.vacantPost,
                                specialBranchUnitId = item.specialBranchUnitId,
                                isParent = item.isParent
                            };
                            subSuggesteds.Add(sub);
                        }
                    }
                    else
                    {
                        SubSuggestedBranch sub = new SubSuggestedBranch
                        {
                            unitId = suggestedData.Where(x => x.specialBranchUnitId == data.Key).FirstOrDefault().unitId,
                            subUnit = suggestedData.Where(x => x.specialBranchUnitId == data.Key).FirstOrDefault().subUnit,
                            numOfPost = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.numOfPost),
                            totalEmployee = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.totalEmployee),
                            vacantPost = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.vacantPost),
                            specialBranchUnitId = data.Key,
                            isParent = suggestedData.Where(x => x.specialBranchUnitId == data.Key).FirstOrDefault().isParent,
                        };
                        subSuggesteds.Add(sub);
                    }


                    MainSuggestedBranch main = new MainSuggestedBranch
                    {
                        specialBranchUnitId = data.Key,
                        mainUnit = suggestedData.Where(x => x.specialBranchUnitId == data.Key).FirstOrDefault().mainUnit,
                        isParent = suggestedData.Where(x => x.specialBranchUnitId == data.Key).FirstOrDefault().isParent,
                        totalEmployee = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.totalEmployee),
                        vacantPost = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.vacantPost),
                        numOfPost = suggestedData.Where(x => x.specialBranchUnitId == data.Key).Sum(x => x.numOfPost),
                        subSuggestedBranches = subSuggesteds
                    };
                    mainSuggesteds.Add(main);
                }
            }
            else
            {
                foreach (var data in suggestedData)
                {
                    List<SubSuggestedBranch> subSuggesteds = new List<SubSuggestedBranch>();
                    SubSuggestedBranch sub = new SubSuggestedBranch
                    {
                        unitId = data.unitId,
                        specialBranchUnitId = data.unitId,
                        subUnit = data.mainUnit,
                        isParent = data.isParent,
                        totalEmployee = data.totalEmployee,
                        vacantPost = data.vacantPost,
                        numOfPost = data.numOfPost,
                    };
                    subSuggesteds.Add(sub);
                    MainSuggestedBranch main = new MainSuggestedBranch
                    {
                        specialBranchUnitId = data.unitId,
                        mainUnit = data.mainUnit,
                        isParent = data.isParent,
                        totalEmployee = data.totalEmployee,
                        vacantPost = data.vacantPost,
                        numOfPost = data.numOfPost,
                        subSuggestedBranches = subSuggesteds
                    };
                    mainSuggesteds.Add(main);
                }
            }

            //var enListed = await _assignmentService.GetEnlistedAssignment(refNum);
            CentralPostingAutoSuggestionListModel model = new CentralPostingAutoSuggestionListModel
            {
                mainSuggestedBranches = mainSuggesteds,
                //suggestedBrancheds = await _assignmentService.GetSuggestedPlaceForEmp(empId, pageNumber, 10, ""),
                //enlistedAssignments = enListed,
                sections = await _employeeService.GetSectionWiseUnit(),
            };
            return Ok(model);
        }

        //POST: api/AssignmentApi/ChangePostingPlaceFromApprove
        [HttpPost]
        public async Task<IActionResult> ChangePostingPlaceFromApprove(AssignmentChangeModel model)
        {
            var enListed = await _assignmentService.GetAssignmentByIdSingle(model.detailsId);
            var user = await _userManager.FindByNameAsync(model.user);
            if (enListed != null)
            {
                AssignmentTransectionLog data = new AssignmentTransectionLog
                {
                    assignmentId = enListed.Id,
                    oldSectionId = enListed.sectionId,
                    newSectionId = model.sectionId,
                    StartDate = DateTime.Now,
                    UpdateUserId = user.Id
                };
                _AssignmentTransectionLog.Insert(data);

                enListed.sectionId = model.sectionId;
                _repoAssignment.Update(enListed);
            }

            return Ok("success");
        }

        public class AssignmentPostModel
        {
            public int id { get; set; }
            public string comment { get; set; }
            public string user { get; set; }
        }

        public class AssignmentChangeModel
        {
            public int detailsId { get; set; }
            public int sectionId { get; set; }
            public string user { get; set; }
        }

        public class AssignmentReturnModel
        {
            public string loggedUser { get; set; }
            public int id { get; set; }
            public string comment { get; set; }
            public string returnTo { get; set; }
        }

        public class PostingOrderReportModel
        {
            public IEnumerable<AssignmentAnulipi> assignmentAnulipis { get; set; }
            public IEnumerable<Assignment> assignments { get; set; }
        }

        public class CentralPostingChangeListModel
        {
            public IEnumerable<BranchUnitWiseEmployeesModel> branchUnitWiseEmployees { get; set; }
            public IEnumerable<Section> sections { get; set; }
            public IEnumerable<EnlistedAssignment> enlistedAssignments { get; set; }
            public SpecialBranchUnit presentUnits { get; set; }
            public IEnumerable<SpecialBranchUnit> homeDistUnits { get; set; }
            public IEnumerable<SpecialBranchUnit> previousUnits { get; set; }
            public IEnumerable<SpecialBranchUnit> spouseUnits { get; set; }
        }

        public class CentralPostingAutoSuggestionListModel
        {
            public IEnumerable<SuggestedBranched> suggestedBrancheds { get; set; }
            public IEnumerable<MainSuggestedBranch> mainSuggestedBranches { get; set; }
            public IEnumerable<Section> sections { get; set; }
            public IEnumerable<EnlistedAssignment> enlistedAssignments { get; set; }
        }

        public class InternalPostingAPIModel
        {
            public int? assignMasterId { get; set; }
            public ApplicationUser userInfo { get; set; }
            public EmployeeInfo employeeInfo { get; set; }
            public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
            public IEnumerable<InternalPostingReportVM> postingReportViews { get; set; }
            public IEnumerable<InternalAssignmentDetailsModal> assignmentDetailsModals { get; set; }
        }

        public class MainSuggestedBranch
        {
            
            public int? specialBranchUnitId { get; set; }
            public int? isParent { get; set; }
            public int? numOfPost { get; set; }
            public int? vacantPost { get; set; }
            public int? totalEmployee { get; set; }
            public string mainUnit { get; set; }
            public List<SubSuggestedBranch> subSuggestedBranches { get; set; }
        }

        public class SubSuggestedBranch
        {
            public int? unitId { get; set; }
            public int? specialBranchUnitId { get; set; }
            public int? isParent { get; set; }
            public int? numOfPost { get; set; }
            public int? vacantPost { get; set; }
            public int? totalEmployee { get; set; }
            public string subUnit { get; set; }
        }

    }
}