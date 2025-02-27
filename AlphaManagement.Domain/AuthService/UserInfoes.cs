using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.Auth;
using AlphaManagement.DAL.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaManagement.Domain.AuthService.Interfaces;
using static System.Collections.Specialized.BitVector32;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;

namespace AlphaManagement.Domain.AuthService
{
    public class UserInfoes : IUserInfoes
    {
        private readonly AlphaDbContext _context;
        public UserInfoes(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllUserInfo()
        {
            return await _context.EmployeeInfos.Where(x => x.ApplicationUserId != null).Include(x => x.ApplicationUser)
                .Include(x => x.rank).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserInfoByUser(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<EmployeeInfo> GetEmployeeInfoByUserName(string userName)
        {
            return await _context.EmployeeInfos.Where(x => x.ApplicationUser.UserName == userName).Include(x => x.ApplicationUser).Include(x => x.rank).FirstOrDefaultAsync();
        }

        public async Task<EmployeeInfo> GetUserInfoByUserId(string userId)
        {
            return await _context.EmployeeInfos.Where(x => x.ApplicationUserId == userId).Include(x => x.ApplicationUser)
                .Include(x => x.religion).Include(x => x.employeeType).Include(x => x.branch).Include(x => x.attachmentBranch)
                .Include(x => x.banks).Include(x => x.rank).Include(x => x.designations).Include(x => x.rank).Include(x => x.section).Include(x => x.section).Include(x => x.bCSBatch)
                .FirstOrDefaultAsync();
        }

        public async Task<EmployeeInfo> GetUserInfoByEmpId(int userId)
        {
            return await _context.EmployeeInfos.Where(x => x.Id == userId).Include(x => x.ApplicationUser)
                .Include(x => x.religion).Include(x => x.employeeType).Include(x => x.branch).Include(x => x.attachmentBranch)
                .Include(x => x.banks).Include(x => x.rank).Include(x => x.designations).Include(x => x.section).Include(x => x.bCSBatch)
                .FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetUserInfoByBP(string BPNo)
        {
            return await _context.Users.Where(x => x.bpNo == BPNo).FirstOrDefaultAsync();
        }

        public async Task<AspNetUsersViewModel> GetUserInfoByUserName(string userName)
        {
            try
            {
                var result = await (from U in _context.Users
                                    join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId into EE
                                    from emp in EE.DefaultIfEmpty()
                                    join pl in _context.Sections on emp.sectionId equals pl.Id into pp
                                    from PCL in pp.DefaultIfEmpty()
                                    join sbu in _context.SpecialBranchUnits on emp.branchId equals sbu.Id into ssb
                                    from sb in ssb.DefaultIfEmpty()
                                    join R in _context.Ranks on emp.rankId equals R.Id into DD
                                    from dpt in DD.DefaultIfEmpty()
                                    join PH in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals PH.employeeId into PP
                                    from P in PP.DefaultIfEmpty()
                                    join ur in _context.UserRoles on U.Id equals ur.UserId
                                    join ar in _context.Roles on ur.RoleId equals ar.Id
                                    join bh in _context.BCSBatches on emp.bCSBatchId equals bh.Id into bbc
                                    from bb in bbc.DefaultIfEmpty()
                                    where U.UserName == userName

                                    select new AspNetUsersViewModel
                                    {
                                        aspnetId = U.Id,
                                        userName = U.UserName,
                                        email = U.Email,
                                        empCode = emp.employeeCode,
                                        isActive = (U.isActive == null) ? 0 : U.isActive,
                                        empName = emp.nameEnglish,
                                        employeeId = emp.Id,
                                        sectionName = PCL.Name,
                                        rankName = dpt.rankName,
                                        rankId = emp.rankId,
                                        unitId = emp.branchId,
                                        unitName = sb.branchUnitName,
                                        roleId = ar.Id,
                                        roleName = ar.Name,
                                        batchName = bb.batchName,
                                        joiningDate = emp.joiningDateGovtService,
                                        joiningDatePresentWorkStation = emp.joiningDatePresentWorkstation,
                                        mobileNo = emp.mobileNumberPersonal,
                                        status = emp.isApproved,
                                        imageUrl = P.url,
                                        departmentId=emp.departmentId
                                    }).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoList()
        {
            var result = await (from a in _context.Users
                                select new AspNetUsersViewModel
                                {
                                    userName = a.UserName,
                                    email = a.Email
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AlphaModule>> GetAllAlphaModule()
        {
            return await _context.AlphaModules.AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteUserRoleListByUserId(string Id)
        {
            _context.UserRoles.RemoveRange(_context.UserRoles.Where(x => x.UserId == Id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoleById(string Id)
        {
            _context.Roles.Remove(_context.Roles.Where(x => x.Id == Id).First());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> SaveUserInfo(UserInformation userInformation)
        {
            try
            {
                if (userInformation.Id != 0)
                {
                    _context.UserInformation.Update(userInformation);
                }
                else
                {
                    _context.UserInformation.Add(userInformation);
                }

                await _context.SaveChangesAsync();
                return userInformation.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveApplicationUserInfo(ApplicationUser user)
        {
            try
            {
                if (user.Id != null)
                {
                    _context.Users.Update(user);
                }
                else
                {
                    _context.Users.Add(user);
                }
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserInformation> GetUserInfoBeforeRegister(string userName)
        {
            return await _context.UserInformation.Where(x => x.userName == userName).FirstOrDefaultAsync();
        }

        public async Task<UserInformation> GetUserInfoBeforeRegisterById(int id)
        {
            return await _context.UserInformation.FindAsync(id);
        }

        public async Task<UserInformation> GetUserInfoBeforeRegisterByBpNumber(string bp)
        {
            return await _context.UserInformation.Where(x => x.bpNo == bp).LastOrDefaultAsync();
        }

        public async Task<string> RemoveUserLocoutTimeByUserName(string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            user.LockoutEnd = null;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return "update";
        }

        public void DeleteUserInfoBeforeRegister(string userName)
        {
            _context.RemoveRange(_context.UserInformation.Where(x => x.userName == userName));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<AspNetUsersViewModel>> GetUserInfo()
        {
            var result = (from U in _context.Users
                          join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                          select new AspNetUsersViewModel
                          {
                              aspnetId = U.Id,
                              userName = U.UserName,
                              email = U.Email,
                              empCode = E.employeeCode,
                              isActive = U.isActive,
                              empName = E.nameEnglish + " - " + E.employeeCode,
                              employeeId = E.Id,
                              //DivisionName = EEE.department.deptName

                          }).ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoForAlpha()
        {
            var result = (from U in _context.Users
                          join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                          join R in _context.UserRoles on U.Id equals R.UserId
                          join RL in _context.Roles on R.RoleId equals RL.Id
                          where (RL.Name == "Admin" || RL.Name == "IGP")
                          select new AspNetUsersViewModel
                          {
                              aspnetId = U.Id,
                              userName = U.UserName,
                              email = U.Email,
                              empCode = E.employeeCode,
                              isActive = U.isActive,
                              empName = E.nameEnglish,
                              employeeId = E.Id,
                              roleName = RL.Name,
                              //DivisionName = EEE.department.deptName

                          }).ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<PostingReportView>> GetRankWiseAssignmentCopy(int rank)
        {
            int? Sortorder = await _context.Ranks.Where(x => x.Id == rank).Select(x => x.shortOrder).FirstOrDefaultAsync();
            var Ids = await (from U in _context.Users
                             join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                             join Rn in _context.Ranks on E.rankId equals Rn.Id
                             join R in _context.UserRoles on U.Id equals R.UserId
                             join RL in _context.Roles on R.RoleId equals RL.Id
                             where (RL.Name == "Admin" && Rn.shortOrder >= Sortorder)
                             select (U.Id)).ToListAsync();

            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.applicationUserId))
                                select new PostingReportView
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = asignM.refDate,
                                    title = asignM.title,
                                    description = asignM.description,
                                    nameEnglish = emp.nameEnglish
                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PostingReportView>> GetRankWiseAssignmentCopyOngoing(int rank)
        {
            int? Sortorder = await _context.Ranks.Where(x => x.Id == rank).Select(x => x.shortOrder).FirstOrDefaultAsync();
            var Ids = await (from U in _context.Users
                             join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                             join Rn in _context.Ranks on E.rankId equals Rn.Id
                             join R in _context.UserRoles on U.Id equals R.UserId
                             join RL in _context.Roles on R.RoleId equals RL.Id
                             where (RL.Name == "Admin" && Rn.shortOrder >= Sortorder) || RL.Name=="Super Admin"
                             select (U.Id)).ToListAsync();

            var result = await (from asignM in _context.AssignmentMasters.Where(x => x.statusId < 3)
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.applicationUserId))
                                select new PostingReportView
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = asignM.refDate,
                                    title = asignM.title,
                                    description = asignM.description,
                                    nameEnglish = emp.nameEnglish
                                }).ToListAsync();

            return result;
        }

        public async Task<int?> GetRankWiseAssignmentCopyOngoingCount(int rank)
        {
            int? Sortorder = await _context.Ranks.Where(x => x.Id == rank).Select(x => x.shortOrder).FirstOrDefaultAsync();
            var Ids = await (from U in _context.Users
                             join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                             join Rn in _context.Ranks on E.rankId equals Rn.Id
                             join R in _context.UserRoles on U.Id equals R.UserId
                             join RL in _context.Roles on R.RoleId equals RL.Id
                             where (RL.Name == "Admin" && Rn.shortOrder >= Sortorder)
                             select (U.Id)).ToListAsync();

            var result = await (from asignM in _context.AssignmentMasters.Where(x => x.statusId < 3)
                                where (Ids.Contains(asignM.applicationUserId))
                                select new PostingReportView
                                {
                                    assignMasterId = asignM.Id,
                                }).CountAsync();

            return result;
        }



        public async Task<IEnumerable<PostingReportView>> GetRankWiseInternalAssignmentCopyOngoing(int rank)
        {
            String[] rolestring = { "Admin", "PHQ Approver"};
            int? Sortorder = await _context.Ranks.Where(x => x.Id == rank).Select(x => x.shortOrder).FirstOrDefaultAsync();
            var Ids = await (from U in _context.Users
                             join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                             join Rn in _context.Ranks on E.rankId equals Rn.Id
                             join R in _context.UserRoles on U.Id equals R.UserId
                             join RL in _context.Roles on R.RoleId equals RL.Id
                             where (rolestring.Contains(RL.Name) && Rn.shortOrder >= Sortorder)
                             select (U.Id)).ToListAsync();

            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters.Where(x => x.statusId < 4)
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.ApplicationUserId))
                                select new PostingReportView
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate =Convert.ToDateTime(asignM.refDate),
                                    //title = asignM.title,
                                    //description = asignM.description,
                                    nameEnglish = emp.nameEnglish
                                }).ToListAsync();

            return result;
        }

        public async Task<int?> GetRankWiseInternalAssignmentCopyOngoingCount(int rank)
        {
            String[] rolestring = { "Admin", "PHQ Approver" };
            int? Sortorder = await _context.Ranks.Where(x => x.Id == rank).Select(x => x.shortOrder).FirstOrDefaultAsync();
            var Ids = await (from U in _context.Users
                             join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                             join Rn in _context.Ranks on E.rankId equals Rn.Id
                             join R in _context.UserRoles on U.Id equals R.UserId
                             join RL in _context.Roles on R.RoleId equals RL.Id
                             where (rolestring.Contains(RL.Name) && Rn.shortOrder >= Sortorder)
                             select (U.Id)).ToListAsync();

            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters.Where(x => x.statusId < 4)
                                where (Ids.Contains(asignM.ApplicationUserId))
                                select new PostingReportView
                                {
                                    assignMasterId = asignM.Id,
                                }).CountAsync();

            return result;
        }

        public async Task<IEnumerable<InternalPostingReportVM>> GetRankWiseInternalAssignmentCopy(int rank)
        {
            int? Sortorder = await _context.Ranks.Where(x => x.Id == rank).Select(x => x.shortOrder).FirstOrDefaultAsync();
            var Ids = await (from U in _context.Users
                             join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                             join Rn in _context.Ranks on E.rankId equals Rn.Id
                             join R in _context.UserRoles on U.Id equals R.UserId
                             join RL in _context.Roles on R.RoleId equals RL.Id
                             where (RL.Name == "PHQ Approver" && Rn.shortOrder >= Sortorder)
                             select (U.Id)).ToListAsync();

            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.ApplicationUserId))
                                select new InternalPostingReportVM
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = Convert.ToDateTime(asignM.refDate),
                                    description = asignM.remarks,
                                    nameEnglish = emp.nameEnglish,
                                    statusId = asignM.statusId,
                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<string>> GetRoleListByUserId(string Id)
        {
            return await _context.UserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetUserInfosFromEmployee()
        {
            var result = await _context.EmployeeInfos
                .Include(x => x.rank)
                .Include(x => x.designations)
                .Include(x => x.department)
                .Include(x => x.branch)
                .Include(x => x.section)
                .Include(x => x.religion)
                .Include(x => x.rank)
                .Include(x => x.bCSBatch)
                .Include(x => x.ApplicationUser)
                .Where(x => x.ApplicationUserId != null)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<AspnetAndEmployeeModel>> GetUserInfoListByFilteringForPoliceUser(string id)
        {
            List<AspnetAndEmployeeModel> result = await (from U in _context.Users
                                                         join ur in _context.UserRoles on U.Id equals ur.UserId
                                                         join R in _context.Roles on ur.RoleId equals R.Id
                                                         join e in _context.EmployeeInfos on U.Id equals e.ApplicationUserId
                                                         where e.ApplicationUserId != null && ur.RoleId == id
                                                         select new AspnetAndEmployeeModel
                                                         {
                                                             ID = U.Id,
                                                             UserName = U.UserName,
                                                             Email = U.Email,
                                                             mobile = e.mobileNumberPersonal,
                                                             Photo = _context.Photographs.Where(c => c.employeeId == e.Id).Select(x => x.url).FirstOrDefault(),
                                                             Designation = e.designations.designationName,
                                                             unit = e.branch.branchUnitName,
                                                             NID = e.nationalID,
                                                             isActive = U.isActive,
                                                             empId = e.Id,
                                                             name = e.nameEnglish,
                                                             rankId = e.rankId,
                                                             designationId = e.designationsId,
                                                         }).ToListAsync();

            var aspnetolelist = _context.UserRoles.ToList();
            var aspnetrolenamelist = _context.Roles.ToList();
            List<AspnetAndEmployeeModel> aspNetUsersViewModels = new List<AspnetAndEmployeeModel>();
            foreach (AspnetAndEmployeeModel data in result)
            {
                var roleId = aspnetolelist.Where(x => x.UserId == data.ID).ToList();
                List<string> role = new List<string>();
                foreach (var UserRole in roleId)
                {
                    string rnam = aspnetrolenamelist.Where(x => x.Id == UserRole.RoleId).Select(x => x.Name).First();
                    role.Add(rnam);
                }
                aspNetUsersViewModels.Add(new AspnetAndEmployeeModel
                {
                    ID = data.ID,
                    UserName = data.UserName,
                    Email = data.Email,
                    Photo = data.Photo,
                    unit = data.unit,
                    Designation = data.Designation,
                    NID = data.NID,
                    isActive = data.isActive,
                    roleId = string.Join(",", role),
                    empId = data.empId,
                    name = data.name,
                    mobile = data.mobile,
                    rankId = data.rankId,
                    designationId = data.designationId,
                });

            }
            return aspNetUsersViewModels;
        }





        //  public async Task<IEnumerable<EmployeeListVM>> GetEmployeeUserForAlpha(string roleid)
        //{
        //    var list =  await (from e in _context.EmployeeInfos
        //                  join r in _context.Users  on  e.ApplicationUserId equals r.Id
        //                  join er in _context.UserRoles on r.Id equals er.UserId
        //                  where er.RoleId == roleid
        //                  select new EmployeeListVM
        //                  {
        //                      employeeCode = e.employeeCode,
        //                      name = e.nameEnglish,
        //                      rank = e.rank.rankName,
        //                      designation = e.designations.designationName,
        //                      email = e.emailAddress,
        //                      Id = e.Id,
        //                      facebook = e.facebookId,
        //                      linktin = e.linkdInId,
        //                      twitter = e.skypeId,
        //                      unit = e.branch.branchUnitName,
        //                      phone = e.mobileNumberPersonal,
        //                      url = _context.Photographs.Where(c => c.employeeId == e.Id).Select(x => x.url).FirstOrDefault()
        //                  }).ToListAsync();
        //    return list;
        //}
        public async Task<ApplicationUser> GetApplicationUserByUserId(string userId)
        {
            return await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<int> UserInactiveById(string userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.isActive = 0;
                _context.Users.Update(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> UserActiveById(string userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.isActive = 1;
                _context.Users.Update(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Navbar>> GetAllPage()
        {
            return await _context.Navbars.Include(x => x.module).ToListAsync();
        }

        public async Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoListForProxyAdmin(string userRoleId, string userName)
        {
            var aspnetroles = await _context.UserRoles.Where(x => x.RoleId == (userRoleId == "NA" ? x.RoleId : userRoleId)).Select(x => x.UserId).ToListAsync();
            List<AspNetUsersViewModel> result = (from U in _context.Users.Where(x => aspnetroles.Contains(x.Id))
                                                 join E in _context.EmployeeInfos on U.Id equals E.ApplicationUserId
                                                 into EE
                                                 from m in EE.DefaultIfEmpty()
                                                 join PH in _context.Photographs on m.employeeCode equals PH.employee.employeeCode
                                                 into PP
                                                 from P in PP.DefaultIfEmpty()
                                                 join SS in _context.Sections on m.sectionId equals SS.Id
                                                 into SSS
                                                 from SC in SSS.DefaultIfEmpty()
                                                 where U.UserName == (userName == "NA" ? U.UserName : userName) && U.UserName != "rakib.uddin"
                                                 select new AspNetUsersViewModel
                                                 {
                                                     aspnetId = U.Id,
                                                     userName = U.UserName,
                                                     imageUrl = P.url,
                                                     empCode = m.employeeCode,
                                                     isActive = U.isActive,
                                                     joiningDate = m.joiningDateGovtService,
                                                     mobileNo = m.mobileNumberPersonal,
                                                     email = m.emailAddress,
                                                     status = m.activityStatus,
                                                     empName = m.nameEnglish,
                                                     employeeId = (m.Id == null) ? 0 : m.Id
                                                 }).ToList();

            var aspnetolelist = _context.UserRoles.Where(x => x.RoleId == (userRoleId == "NA" ? x.RoleId : userRoleId)).ToList();
            var aspnetrolenamelist = _context.Roles.ToList();
            List<AspNetUsersViewModel> aspNetUsersViewModels = new List<AspNetUsersViewModel>();
            foreach (AspNetUsersViewModel data in result)
            {
                var roleId = aspnetolelist.Where(x => x.UserId == data.aspnetId).ToList();
                List<string> role = new List<string>();
                foreach (var UserRole in roleId)
                {
                    string rnam = aspnetrolenamelist.Where(x => x.Id == UserRole.RoleId).Select(x => x.Name).First();
                    role.Add(rnam);
                }
                aspNetUsersViewModels.Add(new AspNetUsersViewModel
                {
                    aspnetId = data.aspnetId,
                    userName = data.userName,
                    email = data.email,
                    empCode = data.empCode,
                    isActive = data.isActive,
                    joiningDate = data.joiningDate,
                    mobileNo = data.mobileNo,
                    status = data.status,
                    employeeId = data.employeeId,
                    roleId = string.Join(",", role),
                    empName = data.empName,
                    imageUrl = await _context.Photographs.Where(x => x.employeeId == data.employeeId).Select(x => x.url).FirstOrDefaultAsync()
                });

            }
            return aspNetUsersViewModels;
        }
    }
}
