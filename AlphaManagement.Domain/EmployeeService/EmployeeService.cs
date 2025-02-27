using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfoHistories;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Domain.EmployeeService.interfaces;
using AlphaManagement.Domain.Helper;
using AlphaManagement.Web.Areas.Employee.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlphaManagement.Domain.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AlphaDbContext _context;
        private IMemoryCache cache;
        public EmployeeService(AlphaDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            this.cache = memoryCache;
        }

        public async Task<IEnumerable<UnitWiseOverduePostingModel>> UnitWiseOverduePosting()
        {
            try
            {
                var data = await _context.unitWiseOverduePostingModels.FromSql($"SP_GetUnitWiseOverduePosting").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<RankWiseOverDueModel>> RankWiseOverDueCount()
        {
            try
            {
                var data = await _context.rankWiseOverDueModels.FromSql($"SP_GetRankWiseOverDue").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<RankWiseVacancyViewModel>> RankWiseVacancyCount()
        {
            try
            {
                var data = await _context.rankWiseVacancyViewModels.FromSql($"SP_RankWiseTotalVacancy").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Rank>> GetRankWiseCount()
        {
            var result = await (from rank in _context.Ranks
                                join emp in _context.EmployeeInfos on rank.Id equals emp.rankId
                                select rank).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeForChartJs()
        {
            var result = await _context.EmployeeInfos
                .Include(x => x.rank)
                .Include(x => x.section.specialBranchUnit)
                ///.Where(x => x.servicePeriod >)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EmployeeReturnReason>> GetEmployeeReturnReasonEmployee(int empId)
        {
            var result = await _context.EmployeeReturnReasons
                .Where(x => x.employeeInfoId == empId && x.status == 2)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AssignmentAnulipi>> AssignmentMastersRopo(int id)
        {
            var result = await _context.AssignmentAnulipis
                .Include(x => x.assignmentMaster)
                .Include(x => x.anulipiList)
                .Where(x => x.assignmentMasterId == id)
                .OrderBy(x => x.shortOrder)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AssignmentAnulipiPreview>> AssignmentMastersRopoForPreview(int id)
        {
            var result = await _context.AssignmentAnulipiPreviews
                .Include(x => x.assignmentMaster)
                .Include(x => x.anulipiList)
                .Where(x => x.assignmentMasterId == id)
                .OrderBy(x => x.shortOrder)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PostingReportView>> AssignmentMasters(string userId)
        {
            var Ids = await _context.ApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 1).Select(x => x.masterId).ToListAsync();

            var result = await (from asignM in _context.AssignmentMasters.Where(x => x.statusId < 3)
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.Id) || asignM.applicationUserId == userId)
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

        public async Task<IEnumerable<PostingReportView>> InternalAssignmentMasters(string userId)
        {
            var Ids = await _context.InternalApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 1).Select(x => x.masterId).ToListAsync();

            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.Id))
                                select new PostingReportView
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = Convert.ToDateTime(asignM.refDate),
                                    //title = asignM.,
                                    //description = asignM.description,
                                    nameEnglish = emp.nameEnglish
                                }).ToListAsync();

            return result;

        }

        public async Task<int?> InternalAssignmentMastersCount(string userId)
        {
            return await _context.InternalApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 1).Select(x => x.masterId).CountAsync();
        }

        public async Task<int?> AssignmentMastersCount(string userId)
        {
            var Ids = await _context.ApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 1).Select(x => x.masterId).ToListAsync();

            var result = await _context.AssignmentMasters.Where(x => Ids.Contains(x.Id) || x.applicationUserId == userId).Where(x => x.statusId < 3).CountAsync();

            return result;

        }

        public async Task<IEnumerable<PostingReportView>> AssignmentMasterLocked(int status)
        {
            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.statusId == status
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

        public async Task<int?> AssignmentMasterLockedCount(int status)
        {
            var result = await _context.AssignmentMasters.Where(x => x.statusId == status).CountAsync();

            return result;
        }

        public async Task<IEnumerable<PostingReportView>> PendingAssignmentMasters(string userId)
        {
            var result1 = await (from asignM in _context.AssignmentMasters.Where(x => x.statusId == 1)
                                 join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                 join apl in _context.ApprovalLogs.Where(x => x.isActive == 1) on asignM.Id equals apl.masterId
                                 join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                 from p in ps.DefaultIfEmpty()
                                 where apl.nextApprovarId == userId
                                 select new PostingReportView
                                 {
                                     url = p.url,
                                     assignMasterId = asignM.Id,
                                     refNo = asignM.refNo,
                                     refDate = asignM.refDate,
                                     title = asignM.title,
                                     description = asignM.description,
                                     nameEnglish = emp.nameEnglish,
                                     statusId = asignM.statusId,
                                 }).ToListAsync();

            var result = await (from asignM in _context.AssignmentMasters.Where(x => x.statusId == 1)
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.applicationUserId == (userId == "" ? asignM.applicationUserId : userId)
                                select new PostingReportView
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = asignM.refDate,
                                    title = asignM.title,
                                    description = asignM.description,
                                    nameEnglish = emp.nameEnglish,
                                    statusId = asignM.statusId,
                                }).Concat(result1).ToListAsync();

            return result;

        }

        public async Task<IEnumerable<PostingReportView>> ApprovedAssignmentMasters(string userId)
        {
            var result = await (from asignM in _context.AssignmentMasters.Where(x => x.statusId == 18)
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.applicationUserId == (userId == "" ? asignM.applicationUserId : userId)
                                select new PostingReportView
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = asignM.refDate,
                                    title = asignM.title,
                                    description = asignM.description,
                                    nameEnglish = emp.nameEnglish,
                                    memorandumNo = asignM.memorandumNo,
                                    statusId = asignM.statusId,
                                    rank = _context.Assignments.Where(x => x.assignmentMasterId == asignM.Id).FirstOrDefault().rank.rankName
                                }).ToListAsync();

            return result;

        }

        public async Task<IEnumerable<PostingReportView>> CompleteAssignmentMasters(string userId)
        {
            var result = await (from asignM in _context.AssignmentMasters.Where(x => x.statusId == 20)
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.applicationUserId == (userId == "" ? asignM.applicationUserId : userId)
                                select new PostingReportView
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = asignM.refDate,
                                    title = asignM.title,
                                    description = asignM.description,
                                    nameEnglish = emp.nameEnglish,
                                    statusId = asignM.statusId,
                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PostingReportView>> AssignmentMastersAll()
        {
            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.statusId == 1
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

        public async Task<IEnumerable<PostingReportView>> AssignmentPostedMasters(string userId)
        {
            var rand = new Random();
            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.applicationUserId == (userId == "" ? asignM.applicationUserId : userId)
                                where asignM.statusId == 2
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


            //var result = await _context.AssignmentMasters
            //    .Include(x => x.applicationUser)
            //    .Where(x => x.applicationUserId == userId)
            //    .Where(x => x.statusId == 2)
            //    .ToListAsync();
            //return result;
        }

        public async Task<IEnumerable<PostingReportView>> AssignmentPostedMastersForApprove(string userId)
        {
            var rand = new Random();
            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.statusId == 2
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

        public async Task<int?> AssignmentPostedMastersForApproveCount()
        {
            var rand = new Random();
            var result = await _context.AssignmentMasters.Where(x => x.statusId == 2).CountAsync();
            return result;
        }

        public void UpdateEmployeeInfoStatusById(int empId, int status)
        {
            var user = _context.EmployeeInfos.Find(empId);
            user.isApproved = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void UpdateApprovalLogBymasterId(int masterId)
        {
            var user = _context.ApprovalLogs.Where(x => x.masterId == masterId).ToList();
            foreach (var data in user)
            {
                data.isActive = 0;
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateInternalApprovalLogBymasterId(int masterId)
        {
            var user = _context.InternalApprovalLogs.Where(x => x.masterId == masterId).ToList();
            foreach (var data in user)
            {
                data.isActive = 0;
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateEmployeeInfoisAdminCheckStatusById(int empId, int status)
        {
            var user = _context.EmployeeInfos.Find(empId);
            user.isAdminCheck = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public async Task<IEnumerable<PostingReportView>> AssignmentReturnList(string userId)
        {
            var Ids = await _context.ApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 3).Select(x => x.masterId).ToListAsync();

            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.Id))
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

        public async Task<int?> AssignmentReturnListCount(string userId)
        {
            var Ids = await _context.ApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 3).Select(x => x.masterId).ToListAsync();

            var result = await _context.AssignmentMasters.Where(x => Ids.Contains(x.Id)).CountAsync();

            return result;
        }

        public async Task<int?> AssignmentDueCount()
        {
            var result = await _context.Assignments.Where(x => x.statusId == 2).CountAsync();
            return result;
        }

        public async Task<IEnumerable<PostingReportView>> AssignmentPostedMastersAll()
        {
            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.statusId == 2
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


        public async Task<IEnumerable<PostingReportView>> AssignmentReturnListAll()
        {
            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.statusId == 4
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

        public async Task<IEnumerable<PostingReportView>> AssignmentPostedMastersApprove(string userId)
        {

            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                    //where asignM.applicationUserId == (userId == "" ? asignM.applicationUserId : userId)
                                where asignM.statusId == 3
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
            //var result = await _context.AssignmentMasters
            //    .Include(x => x.applicationUser)
            //    .Where(x => x.applicationUserId == userId)
            //    .Where(x => x.statusId == 3)
            //    .ToListAsync();
            //return result;
        }

        public async Task<int?> AssignmentPostedMastersApproveCount(string userId)
        {

            var result = await _context.AssignmentMasters.Where(x => x.statusId == 3).CountAsync();

            return result;
        }

        public async Task<IEnumerable<PostingReportView>> AllLockedAssignmentMaster()
        {

            var result = await (from asignM in _context.AssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.applicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.statusId >= 3
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

        public async Task<IEnumerable<Assignment>> AssignmentMastersPosted()
        {
            var result = await _context.Assignments
                 .Include(x => x.employee)
                 .Include(x => x.assignmentMaster)
                 .Include(x => x.employee.rank)
                 .Include(x => x.employee.bCSBatch)
                 .Include(x => x.employee.branch)
                 .Include(x => x.specialBranchUnit)
                 .Include(x => x.employee.section)
                 .Include(x => x.section)
                 .Include(x => x.employee.Photographs)
                 .Include(x => x.specialBranchUnit)
                 .Where(x => x.assignmentMaster.statusId == 2)
                 .ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<Assignment>> AssignmentMastersPostedReport(string id, int assignid)
        //{
        //    var result = await _context.Assignments
        //         .Include(x => x.employee)
        //         .Include(x => x.assignmentMaster)
        //         .Include(x => x.employee.rank)
        //         .Include(x => x.employee.bCSBatch)
        //         .Include(x => x.employee.branch)
        //         .Include(x => x.specialBranchUnit)
        //         .Include(x => x.employee.section)
        //         .Include(x => x.specialBranchUnit)
        //         .Where(x => x.assignmentMasterId == assignid)
        //         .Where(x => x.assignmentMaster.applicationUserId == id)
        //         .ToListAsync();
        //    return result;
        //}

        public async Task<IEnumerable<Assignment>> AssignmentPostedByMasterId(int assignid)
        {
            var result = await _context.Assignments
                 .Include(x => x.employee)
                 .Include(x => x.assignmentMaster)
                 .Include(x => x.employee.rank)
                 .Include(e => e.employee.designations)
                 .Include(x => x.employee.bCSBatch)
                 .Include(x => x.employee.branch)
                 .Include(x => x.specialBranchUnit)
                 .Include(x => x.employee.section)
                 .Include(x => x.employee.section.specialBranchUnit.districts)
                 .Include(x => x.section)
                 .Include(x => x.section.specialBranchUnit.districts)
                 .Include(x => x.specialBranchUnit)
                 .Where(x => x.assignmentMasterId == assignid)
                 .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<InternalAssignment>> InternalAssignmentPostedByMasterId(int assignid)
        {
            var result = await _context.InternalAssignments
                 .Include(x => x.employee)
                 .Include(x => x.assignmentMaster)
                 .Include(x => x.employee.rank)
                 .Include(e => e.employee.designations)
                 .Include(x => x.employee.bCSBatch)
                 .Include(x => x.employee.branch)
                 .Include(x => x.department)
                 .Include(x => x.employee.department)
                 .Where(x => x.assignmentMasterId == assignid)
                 .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AssignmentDetailsModal>> AssignmentDetailsModalByMasterId(int assignid)
        {
            try
            {
                var result = await _context.Assignments
                    .Include(x => x.assignmentMaster)
                    .Include(x => x.employee.rank)
                    .Include(x => x.employee.bCSBatch)
                    .Include(x => x.employee.section)
                    .Include(x => x.employee.Photographs)
                    .Include(x => x.employee.section.specialBranchUnit.districts)
                    .Include(x => x.section)
                    .Include(x => x.section.specialBranchUnit.districts)
                    .Where(x => x.assignmentMasterId == assignid).ToListAsync();
                List<AssignmentDetailsModal> data = new List<AssignmentDetailsModal>();
                foreach (var item in result)
                {
                    data.Add(new AssignmentDetailsModal
                    {
                        masterId = item.assignmentMasterId,
                        Id = item.Id,
                        employeeId = item.employeeId,
                        rankId = item?.employee?.rankId,
                        creator = item?.assignmentMaster?.applicationUserId,
                        statusId = item.assignmentMaster?.statusId,
                        reqNo = item?.assignmentMaster?.refNo,
                        rankName = item?.employee?.rank?.rankName,
                        date = item.assignmentMaster.refDate,
                        picture = item.employee?.Photographs.Where(x => x.type == "profile").Select(x => x.url).FirstOrDefault(),
                        Bp = item.employee?.employeeCode,
                        Name = item.employee?.nameEnglish,
                        educationalQualification = _context.EducationalQualifications.Where(x => x.employeeId == item.employeeId).Where(x => x.degreeId != 151).OrderByDescending(x => x.passingYear).Select(x => x.degree.degreeName).FirstOrDefault(),
                        homeDistrict = _context.AddressInformation.Where(x => x.employeeInfoId == item.employeeId).Where(x => x.type == "Permanent Address").Select(x => x.district.districtName).FirstOrDefault(),
                        batch = item?.employee?.bCSBatch?.batchName,
                        currentPostringPlace = item?.employee?.section?.Name + ", " + item.employee?.section?.specialBranchUnit?.districts?.districtName,
                        NewPostringPlace = item.section?.Name + ", " + item.section?.specialBranchUnit?.districts?.districtName,
                        LastPromotionDate = _context.PromotionLogs.Where(x => x.employeeId == item.employeeId).OrderByDescending(x => x.date).Select(x => x.date).FirstOrDefault(),
                        dateOfBirth = item.employee?.dateOfBirth,
                        joiningDateOfPresentunit = item.employee?.joiningDatePresentWorkstation,
                        remarks = item.Remarks
                    });
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<IEnumerable<AssignmentDetailsModal>> LockedOfficersByMasterId(int masterId, int rankId)
        {
            try
            {
                var Ids = await _context.Assignments.Where(x => x.assignmentMasterId == masterId).Select(x => x.sectionId).ToListAsync();

                var result = await _context.Assignments
                    .Include(x => x.assignmentMaster)
                    .Include(x => x.employee.rank)
                    .Include(x => x.employee.bCSBatch)
                    .Include(x => x.employee.section)
                    .Include(x => x.employee.Photographs)
                    .Include(x => x.employee.section.specialBranchUnit.districts)
                    .Include(x => x.section)
                    .Include(x => x.section.specialBranchUnit.districts)
                    .Where(x => Ids.Contains(x.sectionId) && x.statusId == 2 && x.rankId == rankId).ToListAsync();

                List<AssignmentDetailsModal> data = new List<AssignmentDetailsModal>();
                foreach (var item in result)
                {
                    data.Add(new AssignmentDetailsModal
                    {
                        masterId = item.assignmentMasterId,
                        Id = item.Id,
                        employeeId = item.employeeId,
                        rankId = item?.employee?.rankId,
                        statusId = item.assignmentMaster?.statusId,
                        reqNo = item?.assignmentMaster?.refNo,
                        rankName = item?.employee?.rank?.rankName,
                        date = item.assignmentMaster.refDate,
                        picture = item.employee?.Photographs.Where(x => x.type == "profile").Select(x => x.url).FirstOrDefault(),
                        Bp = item.employee?.employeeCode,
                        Name = item.employee?.nameEnglish,
                        educationalQualification = _context.EducationalQualifications.Where(x => x.employeeId == item.employeeId).Where(x => x.degreeId != 151).OrderByDescending(x => x.passingYear).Select(x => x.degree.degreeName).FirstOrDefault(),
                        homeDistrict = _context.AddressInformation.Where(x => x.employeeInfoId == item.employeeId).Where(x => x.type == "Permanent Address").Select(x => x.district.districtName).FirstOrDefault(),
                        batch = item?.employee?.bCSBatch?.batchName,
                        currentPostringPlace = item?.employee?.section?.Name + ", " + item.employee?.section?.specialBranchUnit?.districts?.districtName,
                        NewPostringPlace = item.section?.Name + ", " + item.section?.specialBranchUnit?.districts?.districtName,
                        LastPromotionDate = _context.PromotionLogs.Where(x => x.employeeId == item.employeeId).OrderByDescending(x => x.date).Select(x => x.date).FirstOrDefault(),
                        dateOfBirth = item.employee?.dateOfBirth,
                        joiningDateOfPresentunit = item.employee?.joiningDatePresentWorkstation,
                    });
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<IEnumerable<AssignmentVM>> AssignmentPostedByMasteId(int assignid)
        {

            var query = await (from x in _context.Assignments
                               join e in _context.EmployeeInfos
                               on x.employeeId equals e.Id
                               where x.assignmentMasterId.Equals(assignid)
                               select new AssignmentVM
                               {
                                   assignmentid = x.Id,
                                   employeeId = x.employeeId,
                                   employeeCode = e.employeeCode,
                                   nameBangla = e.nameBangla,
                                   designation = e.designations.designationNameBN,
                                   drereeid = _context.EducationalQualifications.Where(e => e.employeeId == x.employeeId).Where(e => e.degree.levelofeducationId == 14 || e.degree.levelofeducationId == 15).Select(e => e.degree.levelofeducationId).FirstOrDefault(),
                                   rank = (e.section.specialBranchUnit.isdefault == 1 ? e.rank.shortName : e.rank.rankNameBN),
                                   branch = e.section.specialBranchUnit.branchUnitNameBN,
                                   section = e.section.NameBN
                               }).ToListAsync();

            return query;
        }


        public async Task<IEnumerable<Assignment>> AssignmentAll()
        {
            var result = await _context.Assignments
                 .Include(x => x.section.specialBranchUnit.districts)
                 .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> AssignmentReturnDetailsByMasterId(int assignid)
        {
            var result = await _context.Assignments
                 .Include(x => x.employee)
                 .Include(x => x.assignmentMaster)
                 .Include(x => x.employee.rank)
                 .Include(x => x.employee.bCSBatch)
                 .Include(x => x.employee.branch)
                 .Include(x => x.specialBranchUnit)
                 .Include(x => x.employee.section)
                 .Include(x => x.employee.section.specialBranchUnit.districts)
                 .Include(x => x.section)
                 .Include(x => x.section.specialBranchUnit.districts)
                 .Include(x => x.specialBranchUnit)
                 .Where(x => x.assignmentMasterId == assignid && x.assignmentMaster.statusId == 4)
                 .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> AssignmentMastersEntry(int id)
        {
            var result = await _context.Assignments
                 .Include(x => x.employee)
                 .Include(x => x.assignmentMaster)
                 .Include(x => x.employee.rank)
                 .Include(x => x.employee.bCSBatch)
                 .Include(x => x.employee.branch)
                 .Include(x => x.specialBranchUnit)
                 .Include(x => x.employee.section)
                 .Include(x => x.section)
                 .Include(x => x.employee.Photographs)
                 .Include(x => x.specialBranchUnit)
                 .Where(x => x.Id == id)
                 .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> AssignmentMastersReport(int assignmentId)
        {
            var result = await _context.Assignments
                 .Include(x => x.employee)
                 .Include(x => x.assignmentMaster)
                 .Include(x => x.employee.rank)
                 .Include(x => x.employee.bCSBatch)
                 .Include(x => x.employee.branch)
                 .Include(x => x.specialBranchUnit)
                 .Include(x => x.employee.section)
                 .Include(x => x.section)
                 .Include(x => x.employee.Photographs)
                 .Include(x => x.specialBranchUnit)
                 .Where(x => x.Id == assignmentId)
                 //.Where(x=>x.assignmentMaster.applicationUserId==)
                 .ToListAsync();
            return result;
        }


        public async Task<IEnumerable<Assignment>> GetAssignments()
        {
            var result = await _context.Assignments
                .Include(x => x.employee)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.bCSBatch)
                .Include(x => x.employee.branch)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.employee.section)
                .Where(x => x.assignmentMaster.statusId == 1)
                .ToListAsync();
            return result;
        }

        public async Task<int> GetEnlistedMasterIdByRef(string refNo)
        {
            var result = await _context.EnlistedAssignmentMasters
                .Where(x => x.refNo == refNo)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EnlistedAssignment>> GetEnlistedListByRef(string refNo)
        {
            var result = await _context.EnlistedAssignments
                .Where(x => x.enlistedAssignmentMaster.refNo == refNo)
                .ToListAsync();
            return result;
        }

        public async Task<int> GetAssignmentsMasterIdByDeatilsId(int detailsId)
        {
            var result = await _context.Assignments
                .Where(x => x.Id == detailsId)
                .Select(x => Convert.ToInt32(x.assignmentMasterId)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByMasterId(int id)
        {
            var result = await _context.Assignments
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee.section.specialBranchUnit.districts)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.bCSBatch)
                .Include(x => x.employee.branch)
                .Include(x => x.employee.Photographs)
                .Include(x => x.section.specialBranchUnit.districts)
                .Where(x => x.assignmentMasterId == id)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsPriviousLockByMasterId(int id)
        {
            var Ids = await _context.Assignments
                .Where(x => x.assignmentMasterId == id)
                .Select(x => x.employeeId)
                .ToListAsync();

            var result = await _context.Assignments
                .Where(x => x.statusId == 2)
                .Where(x => Ids.Contains(x.employeeId))
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee.section.specialBranchUnit.districts)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.bCSBatch)
                .Include(x => x.employee.branch)
                .Include(x => x.employee.Photographs)
                .Include(x => x.section.specialBranchUnit.districts)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CancelAssignment>> GetAssignmentsRevisedByMasterId(int id)
        {
            var result = await _context.CancelAssignments
                .Where(x => x.assignmentMasterId == id)
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee.section.specialBranchUnit.districts)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.bCSBatch)
                .Include(x => x.employee.branch)
                .Include(x => x.employee.Photographs)
                .Include(x => x.section.specialBranchUnit.districts)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }


        public async Task<int> DeleteAssignmentsPriviousLockByMasterId(int id)
        {
            var cancelAssignments = await _context.CancelAssignments
                .Where(x => x.assignmentMasterId == id)
                .ToListAsync();

            foreach (var data in cancelAssignments)
            {
                _context.Assignments.Remove(_context.Assignments.Where(x => x.assignmentMasterId == data.EntryNo && x.employeeId == data.employeeId).FirstOrDefault());
            }

            return 1;
        }

        public void DeleteAssignmentsAunilipiPreviewByMasterId(int id)
        {
            try
            {
                _context.AssignmentAnulipiPreviews.RemoveRange(_context.AssignmentAnulipiPreviews.Where(x => x.assignmentMasterId == id));
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        //public async Task<IEnumerable<Assignment>> GetAssignmentPost(int masterId)
        //{
        //    var result = await _context.Assignments
        //        .Include(x=>x.employee)
        //        .Include(x=>x.employee.rank)
        //        .Include(x=>x.employee.bCSBatch)
        //        .Include(x=>x.employee.branch)
        //        .Include(x=>x.specialBranchUnit)
        //        .Include(x=>x.employee.section)
        //        .Where(x=>x.assignmentMasterId==masterId)
        //        .ToListAsync();
        //    return result;
        //}

        public async Task<IEnumerable<ApprovalLog>> GetApprovalLogByMasterId(int masterId)
        {
            var result = await _context.ApprovalLogs
                .Where(x => x.masterId == masterId)
                .Include(x => x.user)
                .ToListAsync();
            return result;

        }

        public async Task<IEnumerable<PostingReportView>> GetApprovalLogFullByMasterId(int masterId)
        {
            var result = await (from AP in _context.ApprovalLogs
                                join asignM in _context.Users on AP.userId equals asignM.Id
                                join UR in _context.UserRoles on AP.userId equals UR.UserId
                                join R in _context.Roles on UR.RoleId equals R.Id
                                join emp in _context.EmployeeInfos.Include(x => x.rank) on asignM.Id equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where AP.masterId == masterId && emp.rank.shortOrder < 14
                                select new PostingReportView
                                {
                                    statusId = AP.isActive,
                                    url = p.url,
                                    assignMasterId = AP.Id,
                                    refDate = Convert.ToDateTime(AP.createdAt),
                                    description = AP.notes,
                                    nameEnglish = emp.nameEnglish,
                                    applicationUserId = AP.nextApprovarId,
                                    createApplicationUserId = AP.userId,
                                    userRole = R.Name,
                                    departmentId = emp.departmentId
                                }).ToListAsync();

            return result;

        }

        public async Task<IEnumerable<Disease>> GetDiseases()
        {
            var result = await _context.Diseases
                .Include(x => x.diseaseGroup)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Vaccines>> GetVaccines()
        {
            var result = await _context.Vaccines
                .Include(x => x.vaccineGroup)
                .ToListAsync();
            return result;
        }


        public Task<Photograph> GetPhotographByEmpId(int empId)
        {
            return _context.Photographs.Where(x => x.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<EmployeeInfoVM> GetPhotographByUserId(string userId)
        {
            var result = await (from e in _context.EmployeeInfos
                                join r in _context.Ranks on e.rankId equals r.Id
                                join p in _context.Photographs.Where(x => x.type == "profile") on e.Id equals p.employeeId
                                where e.ApplicationUserId == userId
                                select new EmployeeInfoVM
                                {
                                    Id = e.Id,
                                    empName = e.nameEnglish,
                                    rankName = r.rankName,
                                    empCode = e.employeeCode,
                                    imageUrl = p.url
                                }).FirstOrDefaultAsync();
            return result;
        }

        public IQueryable<EmployeeDetailsViewModel> GetOfficerSearchInformation(int unitId, int rankId, int batchId, string bpNo)
        {
            var result = (from e in _context.EmployeeInfos
                          join r in _context.Ranks on e.rankId equals r.Id
                          join u in _context.SpecialBranchUnits on e.branchId equals u.Id
                          join d in _context.Districts on u.districtsId equals d.Id
                          join s in _context.Sections on e.sectionId equals s.Id into ss
                          from sec in ss.DefaultIfEmpty()
                          join bb in _context.BCSBatches on e.bCSBatchId equals bb.Id into bs
                          from bcs in bs.DefaultIfEmpty()
                          join peradd in _context.AddressInformation.Include(x => x.district).Where(x => x.type == "Permanent Address") on e.Id equals peradd.employeeInfoId into pai
                          from pa in pai.DefaultIfEmpty()
                          join preadd in _context.AddressInformation.Include(x => x.district).Where(x => x.type == "Present Address") on e.Id equals preadd.employeeInfoId into preai
                          from pra in preai.DefaultIfEmpty()
                          join spoadd in _context.AddressInformation.Include(x => x.district).Where(x => x.type == "Spouse Address") on e.Id equals spoadd.employeeInfoId into sai
                          from sa in sai.DefaultIfEmpty()
                          join spoinfo in _context.Spouses.Where(x => x.spouseRelationId == 5) on e.Id equals spoinfo.employeeId into spi
                          from spoi in spi.DefaultIfEmpty()
                          join p in _context.Photographs.Where(x => x.type == "profile") on e.Id equals p.employeeId
                          //join ed in (from dd in _context.EducationalQualifications.OrderByDescending(x=>x.passingYear)
                          //            join relds in _context.RelDegreeSubjects on dd.reldegreesubjectId equals relds.Id
                          //            join dg in _context.Degrees on relds.degreeId equals dg.Id
                          //            join sub in _context.Subjects on relds.subjectId equals sub.Id
                          //            where dd.degreeId!= 151
                          //            group new { dd,dg,sub} by new { dd.employeeId,dg.degreeName,sub.subjectName} into edc
                          //            select new { employeeId=edc.Key.employeeId,edc.Key.degreeName,edc.Key.subjectName}) on e.Id equals ed.employeeId into edd
                          //from edu in edd.DefaultIfEmpty()
                          where e.employeeCode == (bpNo == "" ? e.employeeCode : bpNo) && e.branchId == (unitId == 0 ? e.branchId : unitId)
                          && e.rankId == (rankId == 0 ? e.rankId : rankId) && e.bCSBatchId == (batchId == 0 ? e.bCSBatchId : batchId)
                          && e.employeeTypeId == 1

                          select new EmployeeDetailsViewModel
                          {
                              empId = e.Id,
                              empName = e.nameEnglish,
                              empNameBn = e.nameBangla,
                              rankName = r.rankName,
                              bpNo = e.employeeCode,
                              unitName = u.branchUnitName,
                              sectionName = sec.Name + ", " + d.districtName,
                              bcsBatch = bcs.batchName,
                              bcsPosition = e.bcsPosition,
                              spouseName = spoi.spouseName,
                              presentAddress = pra.district.districtName,
                              permanentAddress = pa.district.districtName,
                              spouseAddress = sa.district.districtName,
                              //educationQualification=edu.degreeName+" in "+edu.subjectName,
                              imageUrl = p.url,
                              joiningDate = e.joiningDateGovtService,
                              joiningDateOfPresentUnit = e.joiningDatePresentWorkstation,
                              lastPromotionDate = e.promotionDate,
                              LPRDate = e.LPRDate,
                              fatherName = e.fatherNameEnglish,
                              rankId = r.shortOrder,
                              batchId = bcs.Id
                          }).OrderBy(x => x.rankId).ThenBy(x => x.batchId).ThenBy(x => x.bpNo).AsQueryable();
            return result;
        }

        public async Task<int> SaveEmployeeInformation(EmployeeInfo employeeInfo)
        {
            try
            {
                if (employeeInfo.Id != 0)
                {
                    _context.EmployeeInfos.Update(employeeInfo);
                }
                else
                {
                    _context.EmployeeInfos.Add(employeeInfo);
                }

                await _context.SaveChangesAsync();
                return employeeInfo.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveEmployeeGradationInformation(EmployeeGradation employeeInfo)
        {
            try
            {
                if (employeeInfo.Id != 0)
                {
                    _context.EmployeeGradations.Update(employeeInfo);
                }
                else
                {
                    _context.EmployeeGradations.Add(employeeInfo);
                }

                await _context.SaveChangesAsync();
                return employeeInfo.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> SaveEmployeeEmployeeInfoHistory(EmployeeInfoHistory employeeInfo)
        {
            try
            {
                if (employeeInfo.Id != 0)
                {
                    _context.EmployeeInfoHistories.Update(employeeInfo);
                }
                else
                {
                    _context.EmployeeInfoHistories.Add(employeeInfo);
                }

                await _context.SaveChangesAsync();
                return employeeInfo.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeInfo> GetEmployeeInfoById(string empCode)
        {
            var result = await _context.EmployeeInfos
                .Include(x => x.rank)
                .Include(x => x.designations)
                .Include(x => x.branch)
                .Include(x => x.section)
                .Include(x => x.religion)
                .Include(x => x.rank)
                .Include(x => x.bCSBatch)
                 .Include(x => x.AddressInformation)
                 .Include(x => x.banks)
                 .Include(x => x.otherBanks)
                 .Include(x => x.pHQTRType)
                 .Include(x => x.country)
                 .Include(x => x.attachmentBranch)
                .Where(x => x.employeeCode == empCode).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeInfo> GetEmployeeInfoSingleById(string empCode)
        {
            var result = await _context.EmployeeInfos
                .Where(x => x.employeeCode == empCode).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeInfoHistory> GetEmployeeInfoHistoryById(string empCode)
        {
            var result = await _context.EmployeeInfoHistories
                .Include(x => x.rank)
                .Include(x => x.designations)
                .Include(x => x.branch)
                .Include(x => x.section)
                .Include(x => x.religion)
                .Include(x => x.rank)
                .Include(x => x.bCSBatch)
                 .Include(x => x.AddressInformation)
                 .Include(x => x.banks)
                 .Include(x => x.otherBanks)
                .Where(x => x.employeeCode == empCode).FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeInfo> GetEmployeeProfileInfoById(int id)
        {
            var result = await _context.EmployeeInfos
                .Include(x => x.rank)
                .Include(x => x.designations)
                .Include(x => x.Photographs)
                .Include(x => x.branch)
                .Include(x => x.section)
                .Include(x => x.religion)
                .Include(x => x.rank)
                .Include(x => x.bCSBatch)
                .Include(x => x.AddressInformation)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public EmployeeInfo GetBasicEmployeeInfoById(int id)
        {
            var result = _context.EmployeeInfos.Find(id);
            return result;
        }

        public async Task<IEnumerable<ACRInformation>> GetACRInfoByEmpId(int id)
        {
            var result = await _context.ACRInformation.Where(x => x.employeeId == id).Include(x => x.employee).ToListAsync();
            return result;
        }

        public async Task<EmployeeInfo> GetEmployeeInfosByEmpId(int empId)
        {
            var result = await _context.EmployeeInfos
                .Include(x => x.rank)
                .Include(x => x.Photographs)
                .Include(x => x.designations)
                .Include(x => x.department)
                .Include(x => x.branch)
                .Include(x => x.section)
                .Include(x => x.religion)
                .Include(x => x.rank)
                .Include(x => x.bCSBatch)
                .Include(x => x.AddressInformation)
                .Where(x => x.Id == empId)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfos()
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
                .Include(x => x.AddressInformation)
                .ToListAsync();

            return result;
        }

        public async Task<EmployeeAPIModel> GetEmployeeInfoByBP(string BP)
        {
            var result = await _context.EmployeeInfos
                .Include(x => x.rank).Include(x => x.bCSBatch)
                .Include(x => x.section.specialBranchUnit.districts)
                .Where(x => x.employeeCode.EndsWith(BP))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return new EmployeeAPIModel();
            }
            else
            {
                EmployeeAPIModel data = new EmployeeAPIModel
                {
                    Name = result.nameEnglish,
                    NameBangla = result.nameBangla,
                    BP = result.employeeCode,
                    rank = result?.rank?.rankNameBN,
                    rankId = result?.rankId,
                    branchId = result?.branchId,
                    bcsBatchId = result?.bCSBatchId,
                    bcsPosition = result?.bcsPosition,
                    bcsBatchName=result?.bCSBatch?.batchName,
                    CurrentPostingPlace = result?.section?.NameBN + "," + result?.section?.specialBranchUnit?.districts?.districtNameBn,
                    HomeDistrict = await _context.AddressInformation.Where(x => x.employeeInfoId == result.Id && x.type == "Permanent Address").Select(x => x.district.districtNameBn).FirstOrDefaultAsync(),
                    joiningDate = result.joiningDateGovtService?.ToString("dd/MM/yyyy"),
                    dateOfBirth = result.dateOfBirth?.ToString("dd/MM/yyyy"),
                    photo = "http://10.201.30.22/" + await _context.Photographs.Where(x => x.type == "profile").Where(x => x.employeeId == result.Id).AsNoTracking().Select(x => x.url).FirstOrDefaultAsync()
                };
                return data;
            }
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfosByType(int typeId)
        {
            var result = await _context.EmployeeInfos
                .Include(x => x.employeeType)
                .Where(x => x.employeeTypeId == typeId && !_context.Photographs.Any(y => y.employeeId == x.Id))
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoList(int statusId)
        {
            if (statusId == 0) // All Employee
            {
                return await _context.EmployeeInfos
                .Include(x => x.rank)
                .Include(x => x.designations)
                .Include(x => x.department)
                .Include(x => x.branch)
                .Include(x => x.section)
                .Include(x => x.religion)
                .Include(x => x.rank)
                .Include(x => x.bCSBatch)
                .Include(x => x.AddressInformation)
                .ToListAsync();
            }
            else if (statusId == 1)
            {
                return await _context.EmployeeInfos
              .Include(x => x.rank)
              .Include(x => x.designations)
              .Include(x => x.department)
              .Include(x => x.branch)
              .Include(x => x.section)
              .Include(x => x.religion)
              .Include(x => x.rank)
              .Include(x => x.bCSBatch)
              .Include(x => x.AddressInformation)
              .Where(x => x.isApproved == 1 || x.isApproved == 2)
              .ToListAsync();
            }
            else
            {
                return await _context.EmployeeInfos
              .Include(x => x.rank)
              .Include(x => x.designations)
              .Include(x => x.department)
              .Include(x => x.branch)
              .Include(x => x.section)
              .Include(x => x.religion)
              .Include(x => x.rank)
              .Include(x => x.bCSBatch)
              .Include(x => x.AddressInformation)
              .Where(x => x.isApproved == statusId)
              .ToListAsync();
            }



        }

        public async Task<IEnumerable<EmployeeInfo>> GetCheckedEmployeeInfoList()
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
          .Include(x => x.Photographs)
          .Where(x => x.isAdminCheck == 1)
          .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetCheckedUpdatedEmployeeInfoList()
        {
            return await _context.EmployeeInfos
          .Include(x => x.rank)
          .Include(x => x.designations)
          .Include(x => x.department)
          .Include(x => x.branch)
          .Include(x => x.section)
          .Include(x => x.religion)
          .Include(x => x.rank)
          .Include(x => x.bCSBatch)
          .Include(x => x.Photographs)
          .Where(x => x.isAdminCheck == 2 || x.isApproved == 8)
          .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> LoadCheckedUpdatedEmployeeInfoList(int rankId, int unitId, int batchId)
        {
            return await _context.EmployeeInfos.Where(x => x.rankId == (rankId == 0 ? x.rankId : rankId) && x.branchId == (unitId == 0 ? x.branchId : unitId) && x.bCSBatchId == (batchId == 0 ? x.bCSBatchId : batchId))
          .Include(x => x.rank)
          .Include(x => x.designations)
          .Include(x => x.department)
          .Include(x => x.branch)
          .Include(x => x.section)
          .Include(x => x.religion)
          .Include(x => x.rank)
          .Include(x => x.bCSBatch)
          .Include(x => x.Photographs)
          .Where(x => x.isApproved == 8)
          .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetCheckedUpdatedEmployeeInfoListUpdate()
        {
            return await _context.EmployeeInfos
          .Include(x => x.rank)
          .Include(x => x.designations)
          .Include(x => x.department)
          .Include(x => x.branch)
          .Include(x => x.section)
          .Include(x => x.religion)
          .Include(x => x.rank)
          .Include(x => x.bCSBatch)
          .Include(x => x.Photographs)
          .Where(x => x.isAdminCheck == 3)
          .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoList()
        {
            return await _context.EmployeeInfos
          .Include(x => x.rank)
          .Include(x => x.branch)
          .Include(x => x.section)
          .Include(x => x.bCSBatch)
          .Where(x => Convert.ToDateTime(x.joiningDatePresentWorkstation).Date < DateTime.Now.AddYears(-2))
          .Where(x => x.joiningDatePresentWorkstation != null)
          .Where(x => x.employeeTypeId == 1)
          .OrderBy(x => x.joiningDatePresentWorkstation)
          .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoListFilter(int rank, int unit, int batch)
        {
            return await _context.EmployeeInfos
          .Include(x => x.rank)
          .Include(x => x.branch)
          .Include(x => x.section)
          .Include(x => x.bCSBatch)
          .Where(x => Convert.ToDateTime(x.joiningDatePresentWorkstation).Date < DateTime.Now.AddYears(-2))
          .Where(x => x.branchId == (unit == 0 ? x.branchId : unit) && x.rankId == (rank == 0 ? x.rankId : rank) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch))
          .Where(x => x.joiningDatePresentWorkstation != null)
          .OrderBy(x => x.joiningDatePresentWorkstation)
          .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoList(int rank, int unit, int batch)
        {
            return await _context.EmployeeInfos
          .Include(x => x.rank)
          .Include(x => x.branch)
          .Include(x => x.section)
          .Include(x => x.bCSBatch)
          .Where(x => Convert.ToDateTime(x.joiningDatePresentWorkstation).Date < DateTime.Now.AddYears(-2))
          .Where(x => x.branchId == (unit == 0 ? x.branchId : unit) && x.rankId == (rank == 0 ? x.rankId : rank) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch))
          .Where(x => x.joiningDatePresentWorkstation != null)
          .OrderBy(x => x.joiningDatePresentWorkstation)
          .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfosViewModelFor_SP>> GetEmployeeInfoListForSp(string userName, int unitId, int rankId, int batchId, int statusId, string periodType)
        {
            try
            {
                return await _context.employeeInfos_Sp.FromSql($"SP_GetProtfolioListWithStatus {userName},{unitId},{rankId},{batchId},{statusId},{periodType}").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<UnitWiseEmployeeInfosViewModelFor_SP>> GetEmployeePercentPrograssList(int? statusId)
        {
            try
            {
                return await _context.unitWiseEmployeeInfos_Sp.FromSql($"SP_Employee_Percent_Prograss {statusId}").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BadgeAndActivityModel>> GetBadgeAndActivityModelList()
        {
            try
            {
                return await _context.badgeAndActivityModels.FromSql($"SP_GETAllEmployeeIcons").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<AssignmentViewModels>> GetAssignmentListbyMasterId(int masterid)
        {
            try
            {
                var list = await _context.assignmentViewModels.FromSql($"SP_GetEmployeeInfoForNote {masterid}").AsNoTracking().ToListAsync();
                return list;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<AssignmentViewModels>> GetInternalAssignmentListbyMasterId(int masterid)
        {
            try
            {
                var list = await _context.assignmentViewModels.FromSql($"SP_GetInternalEmployeeInfoForNote {masterid}").AsNoTracking().ToListAsync();
                return list;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IEnumerable<UnitWiseEmployeeInfosViewModelFor_SP>> GetUnitWiseEmployeeInfoListForSp(string userName, int unitId, int rankId, int batchId, int divisionId)
        {
            try
            {
                return await _context.unitWiseEmployeeInfos_Sp.FromSql($"SP_GetUnitWiseProtfolioListWithStatus {userName},{unitId},{rankId},{batchId},{divisionId}").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IEnumerable<EmployeeListVM>> GetEmployeeListById()
        {
            return await (from e in _context.EmployeeInfos
                          select new EmployeeListVM
                          {
                              employeeCode = e.employeeCode,
                              name = e.nameEnglish,
                              rank = e.rank.rankName,
                              designation = e.designations.designationName,
                              email = e.emailAddress,
                              Id = e.Id,
                              facebook = e.facebookId,
                              linktin = e.linkdInId,
                              twitter = e.skypeId,
                              unit = e.branch.branchUnitName,
                              phone = e.mobileNumberPersonal,
                              url = _context.Photographs.Where(c => c.employeeId == e.Id).Select(x => x.url).FirstOrDefault()
                          }).ToListAsync();
        }

        public async Task<IEnumerable<GetCheckedEmployeeList_SP>> GetCheckedEmployeeListBySp(int unitId, int rankId, int batchId)
        {
            try
            {
                return await _context.SP_CheckedEmployeeInfoList.FromSql($"SP_CheckedEmployeeInfoList {unitId},{rankId},{batchId}").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<GetCheckedEmployeeList_SP>> GetDiciplinaryEmployeeListBySp(int unitId, int rankId, int batchId)
        {
            try
            {
                return await _context.SP_CheckedEmployeeInfoList.FromSql($"SP_EmployeeInfoListWithDisciplinaryActions {unitId},{rankId},{batchId}").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<GetCheckedEmployeeList_SP>> GetMedicalInfoEmployeeListBySp(int unitId, int rankId, int batchId)
        {
            try
            {
                return await _context.SP_CheckedEmployeeInfoList.FromSql($"[SP_EmployeeInfoListWithMedicalInfo] {unitId},{rankId},{batchId}").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<GetEmployeeListWithTrainingSkill>> GetEmployeeListWithTrainingSkills(int unitId, int rankId, int batchId)
        {
            try
            {
                return await _context.getEmployeeListWithTrainingSkills.FromSql($"SP_EmployeeInfoListWithTrainingSkill {unitId},{rankId},{batchId}").AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<EmployeeListVM>> GetEmployeeListForAlphaPersonalProfile()
        {
            return await (from e in _context.EmployeeInfos.Where(x => x.isApproved == 4)
                          select new EmployeeListVM
                          {
                              employeeCode = e.employeeCode,
                              name = e.nameEnglish,
                              rank = e.rank.rankName,
                              designation = e.designations.designationName,
                              email = e.emailAddress,
                              Id = e.Id,
                              facebook = e.facebookId,
                              linktin = e.linkdInId,
                              twitter = e.skypeId,
                              unit = e.branch.branchUnitName,
                              phone = e.mobileNumberPersonal,
                              url = _context.Photographs.Where(c => c.employeeId == e.Id).Select(x => x.url).FirstOrDefault()
                          }).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeListVM>> GetPRLEmployeeListForAlphaProfile(int? rankId, int? unitId, int? bcsBatchId)
        {
            var result = await (from e in _context.EmployeeInfos.Where(x => x.isApproved == 4 && x.pabx == "PRL" && x.rankId == (rankId == 0 ? x.rankId : rankId) && x.branchId == (unitId == 0 ? x.branchId : unitId) && x.bCSBatchId == (bcsBatchId == 0 ? x.bCSBatchId : bcsBatchId) && Convert.ToDateTime(x.LPRDate) <= DateTime.Now.Date)
                                select new EmployeeListVM
                                {
                                    employeeCode = e.employeeCode,
                                    name = e.nameEnglish,
                                    rank = e.rank.rankName,
                                    designation = e.designations.designationName,
                                    email = e.emailAddress,
                                    Id = e.Id,
                                    facebook = e.facebookId,
                                    homeDistrict = e.homeDistrict,
                                    linktin = e.linkdInId,
                                    twitter = e.skypeId,
                                    unit = e.branch.branchUnitName,
                                    phone = e.mobileNumberPersonal,
                                    joiningDate = e.joiningDateGovtService,
                                    PRLDate = Convert.ToDateTime(e.LPRDate).AddYears(-1),
                                    url = _context.Photographs.Where(c => c.employeeId == e.Id && c.type == "profile").Select(x => x.url).FirstOrDefault()
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ForeignTravel>> GetForeignTravelsById(int id)
        {
            var result = await _context.ForeignTravels
                .Include(x => x.country)
                .Include(x => x.employee)
                .Where(X => X.employeeId == id)
                .OrderByDescending(x => x.travelDate)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ForeignTravel>> GetForeignTravelsForFATById(int id)
        {
            var result = await _context.ForeignTravels
                .Include(x => x.country)
                .Include(x => x.employee)
                .Where(X => X.employeeId == id && X.travelPurpose == "FAT")
                .OrderByDescending(x => x.travelDate)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ForeignTravelHistory>> GetForeignTravelHistoryById(int id)
        {
            var result = await _context.ForeignTravelHistories
                .Include(x => x.country)
                .Include(x => x.employee)
                .Where(X => X.employeeId == id)
                .OrderByDescending(x => x.travelDate)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ACRInformation>> GetEmployeeACRInfo(int id)
        {
            var result = await _context.ACRInformation
                .Include(x => x.employee)
                .Where(X => X.employeeId == id)
                .OrderByDescending(x => x.year)
                .Take(3)
                .ToListAsync();
            return result;
        }

        public async Task<Photograph> GetEmployeePhotographByEmpId(int empId)
        {
            var result = await _context.Photographs.Where(x => x.employeeId == empId).Include(e => e.employee)
                .Where(x => x.type == "profile")
                .LastOrDefaultAsync();
            return result;
        }

        public async Task<Photograph> GetEmployeeSignatureByEmpId(int empId)
        {
            return await _context.Photographs
                .Where(x => x.employeeId == empId).Include(e => e.employee)
                .Where(x => x.type == "signature")
                .LastOrDefaultAsync();
        }

        public async Task<Photograph> GetEmployeeSignatureByBp(string bp)
        {
            return await _context.Photographs.Include(x => x.employee)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.branch)
                .Where(x => x.employee.employeeCode == bp)
                .Where(x => x.type == "signature")
                .Select(x => new Photograph
                {
                    employeeId = x.employeeId,
                    url = x.url,
                    type = x.type,
                    remarks = x.employee.nameEnglish + " (" + x.employee.rank.rankName + ") "
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveFamilyInformation(Spouse spouse)
        {
            try
            {
                if (spouse.Id != 0)
                {
                    _context.Spouses.Update(spouse);
                }
                else
                {
                    _context.Spouses.Add(spouse);
                }

                await _context.SaveChangesAsync();
                return spouse.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveSpouseHistory(SpouseHistory spouse)
        {
            try
            {
                if (spouse.Id != 0)
                {
                    _context.SpouseHistories.Update(spouse);
                }
                else
                {
                    _context.SpouseHistories.Add(spouse);
                }

                await _context.SaveChangesAsync();
                return spouse.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Spouse> GeSpouseById(int Id)
        {
            var result = await _context.Spouses
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeInfo> GetBankByEmpId(int bankId)
        {
            var result = await _context.EmployeeInfos
                .Where(x => x.banksId == bankId)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Spouse>> GetSpouseInfoByEmpId(int empId)
        {
            var result = await _context.Spouses.Include(x => x.spouseRelation).Include(x => x.district).Where(x => x.employeeId == empId)
                .AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpouseHistory>> GetSpouseHistoryByEmpId(int empId)
        {
            var result = await _context.SpouseHistories.Include(x => x.spouseRelation).Include(x => x.district).Where(x => x.employeeId == empId)
                .AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Spouse> GetSpouseInfoByEmpIdRelId(int empId)
        {
            var result = await _context.Spouses.Include(x => x.spouseRelation).Include(x => x.district).Where(x => x.employeeId == empId && x.spouseRelationId == 5).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EmployeeMedicalDisease>> GetDiseasesInfoByMedicalId(int medId)
        {
            var result = await _context.EmployeeMedicalDiseases.Where(x => x.medicalId == medId)
                .AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Disease> GetDiseasesInfoById(int id)
        {
            var result = await _context.Diseases.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveAssignmentInformation(Assignment assignment)
        {
            try
            {
                if (assignment.Id != 0)
                {
                    _context.Assignments.Update(assignment);
                }
                else
                {
                    _context.Assignments.Add(assignment);
                }

                await _context.SaveChangesAsync();
                return assignment.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveAssignmentAssignmentHistory(AssignmentHistory assignment)
        {
            try
            {
                if (assignment.Id != 0)
                {
                    _context.AssignmentHistories.Update(assignment);
                }
                else
                {
                    _context.AssignmentHistories.Add(assignment);
                }

                await _context.SaveChangesAsync();
                return assignment.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveEnlistedAssignment(EnlistedAssignment assignment)
        {
            try
            {
                if (assignment.Id != 0)
                {
                    _context.EnlistedAssignments.Update(assignment);
                }
                else
                {
                    _context.EnlistedAssignments.Add(assignment);
                }

                await _context.SaveChangesAsync();
                return assignment.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveEnlistedMasterAssignment(EnlistedAssignmentMaster assignment)
        {
            try
            {
                if (assignment.Id != 0)
                {
                    _context.EnlistedAssignmentMasters.Update(assignment);
                }
                else
                {
                    _context.EnlistedAssignmentMasters.Add(assignment);
                }

                await _context.SaveChangesAsync();
                return assignment.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteEnlistedAssignment(int id)
        {
            _context.EnlistedAssignments.Remove(await _context.EnlistedAssignments.FindAsync(id));
            var del = await _context.SaveChangesAsync();
            return del;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentInfoByEmpId(int empId)
        {
            var result = await _context.Assignments
                .Include(x => x.rank)
                .Include(X => X.employee)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.designation)
                .Where(x => x.employeeId == empId)
                .OrderBy(x => x.StartDate)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AssignmentHistory>> GetAssignmentHistoryByEmpId(int empId)
        {
            var result = await _context.AssignmentHistories
                .Include(x => x.rank)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.designation)
                .Where(x => x.employeeId == empId)
                .OrderBy(x => x.StartDate)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<Assignment> GetAssignmentInfoById(int Id)
        {
            var result = await _context.Assignments
                .Include(x => x.assignmentMaster)
                .Include(x => x.employee)
                .Include(x => x.employee.rank)
                .Include(x => x.supervisor.rank)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.specialBranchUnit.specialBranchUnit)
                .Include(x => x.rank)
                .Include(x => x.section)
                .Include(x => x.department)
                .Include(x => x.designation)
                .Include(x => x.supervisor)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EmployeeReturnReason>> GetEmployeeReturnReasonByEmpId(int empId)
        {
            var result = await _context.EmployeeReturnReasons
                .Where(x => x.employeeInfoId == empId)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<Assignment> GetAssignmentByRefNoEmpId(string refNo, int empId)
        {
            var result = await _context.Assignments
                .Include(x => x.rank)
                .Include(x => x.specialBranchUnit)
                .Include(x => x.designation)
                .Where(x => x.assignmentMaster.refNo == refNo && x.employeeId == empId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SavePromotionInfo(PromotionLog promotionLog)
        {
            try
            {
                if (promotionLog.Id != 0)
                {
                    _context.PromotionLogs.Update(promotionLog);
                }
                else
                {
                    _context.PromotionLogs.Add(promotionLog);
                }

                await _context.SaveChangesAsync();
                return promotionLog.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SavePromotionLogHistory(PromotionLogHistory promotionLog)
        {
            try
            {
                if (promotionLog.Id != 0)
                {
                    _context.PromotionLogHistories.Update(promotionLog);
                }
                else
                {
                    _context.PromotionLogHistories.Add(promotionLog);
                }

                await _context.SaveChangesAsync();
                return promotionLog.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PromotionLog> GetPromotionInfoById(int Id)
        {
            var result = await _context.PromotionLogs.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<PromotionLog>> GetPromotionInfoByEmpId(int empId)
        {
            var result = await _context.PromotionLogs.Include(x => x.rank).Include(x => x.designationNew).Include(x => x.rankOld).Include(x => x.designationOld).Where(x => x.employeeId == empId).OrderBy(x => x.date).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PromotionLogHistory>> GetPromotionLogHistoryByEmpId(int empId)
        {
            var result = await _context.PromotionLogHistories.Include(x => x.rank).Include(x => x.designationNew).Include(x => x.rankOld).Include(x => x.designationOld).Where(x => x.employeeId == empId).OrderBy(x => x.date).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<int> SaveEducationInformation(EducationalQualification educationalQualification)
        {
            try
            {
                if (educationalQualification.Id != 0)
                {
                    _context.EducationalQualifications.Update(educationalQualification);
                }
                else
                {
                    _context.EducationalQualifications.Add(educationalQualification);
                }

                await _context.SaveChangesAsync();
                return educationalQualification.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveEducationalQualificationHistory(EducationalQualificationHistory educationalQualification)
        {
            try
            {
                if (educationalQualification.Id != 0)
                {
                    _context.EducationalQualificationHistories.Update(educationalQualification);
                }
                else
                {
                    _context.EducationalQualificationHistories.Add(educationalQualification);
                }

                await _context.SaveChangesAsync();
                return educationalQualification.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EducationalQualification> GetEducationalQualificationInfoById(int Id)
        {
            var result = await _context.EducationalQualifications
                .Where(x => x.Id == Id)
                .OrderBy(x => x.passingYear)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EducationalQualification>> GetEducationalQualificationInfoByEmpId(int empId)
        {
            var result = await _context.EducationalQualifications
                .Include(x => x.result)
                .Include(x => x.degree)
                .Include(x => x.degree.levelofeducation)
                .Include(x => x.reldegreesubject.subject)
                .Include(x => x.reldegreesubject.degree)
                .Include(x => x.organization)
                .Where(x => x.employeeId == empId)
                .OrderBy(x => x.passingYear)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EducationalQualificationHistory>> GetEducationalQualificationHistoryByEmpId(int empId)
        {
            var result = await _context.EducationalQualificationHistories
                .Include(x => x.result)
                .Include(x => x.degree)
                .Include(x => x.degree.levelofeducation)
                .Include(x => x.reldegreesubject.subject)
                .Include(x => x.reldegreesubject.degree)
                .Include(x => x.organization)
                .Where(x => x.employeeId == empId)
                .OrderBy(x => x.passingYear)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<int> SaveTrainingInformation(TraningLog level)
        {
            try
            {
                if (level.Id != 0)
                {
                    _context.TraningLogs.Update(level);
                }
                else
                {
                    _context.TraningLogs.Add(level);
                }

                await _context.SaveChangesAsync();
                return level.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveTraningLogHistory(TraningLogHistory level)
        {
            try
            {
                if (level.Id != 0)
                {
                    _context.TraningLogHistories.Update(level);
                }
                else
                {
                    _context.TraningLogHistories.Add(level);
                }

                await _context.SaveChangesAsync();
                return level.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TraningLog> GetTraningLogInfoById(int Id)
        {
            var result = await _context.TraningLogs.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<TraningLog>> GetTraningLogInfoByEmpId(int empId)
        {
            var result = await _context.TraningLogs.Include(x => x.trainingCategory).Include(x => x.country).Include(x => x.trainingInstitute).Include(x => x.specialSkillType).Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<EmployeeReportInfo>> GetEmployeeReportInfoByEmpIdandType(int empId, string type)
        {
            var result = await _context.employeeReportInfos.Include(x => x.employeeInfo).Where(x => x.employeeInfoId == empId && x.type == type).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeReportInfoByType(string type, int branchId, int rankId, int bcs)
        {
            var Ids = await _context.employeeReportInfos.Include(x => x.employeeInfo).Where(x => x.type == type).Select(x => x.employeeInfoId).Distinct().ToListAsync();
            var result = await _context.EmployeeInfos
                .Include(x => x.rank)
                .Include(x => x.section.specialBranchUnit.districts)
                .Include(x => x.bCSBatch)
                .Where(x => x.branchId == (branchId == 0 ? x.branchId : branchId))
                .Where(x => x.rankId == (rankId == 0 ? x.rankId : rankId))
                .Where(x => x.bCSBatchId == (bcs == 0 ? x.bCSBatchId : bcs))
                .Where(x => Ids.Contains(x.Id)).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EmployeeMadicalInfo>> GetEmployeeMadicalInfoByEmpIde(int empId)
        {
            var result = await _context.employeeMadicalInfos.Include(x => x.employeeInfo).Include(x => x.medicalSubCategory.medicalMainCategory).Where(x => x.employeeInfoId == empId).AsNoTracking().ToListAsync();
            return result;
        }


        public async Task<IEnumerable<PostingPriorityLevel>> GetPostingPriorityLevel()
        {
            var result = await _context.postingPriorityLevels
                .Include(x => x.priorityLevelType)
                .Include(x => x.district)
                .Include(x => x.employeeInfo.rank)
                .Include(x => x.specialSkillType)
                .Include(x => x.rank)
                .Include(x => x.specialBranchUnit).ToListAsync();
            return result;
        }
        public async Task<int> SavePostingPriorityLevel(PostingPriorityLevel level)
        {
            try
            {
                if (level.Id != 0)
                {
                    _context.postingPriorityLevels.Update(level);
                }
                else
                {
                    _context.postingPriorityLevels.Add(level);
                }

                await _context.SaveChangesAsync();
                return level.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PostingPriorityLevel> GetPostingPriorityLevelById(int id)
        {
            var data = await _context.postingPriorityLevels.
                Include(e => e.specialBranchUnit).
                Include(e => e.specialSkillType).
                Include(e => e.employeeInfo).
                Include(e => e.district).
                Include(e => e.rank).
                Include(e => e.priorityLevelType).
                Where(e => e.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<int> DeletePriorityLevelTypebyId(int id)
        {
            _context.priorityLevelTypes.Remove(await _context.priorityLevelTypes.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeletePostingPriorityLevelById(int id)
        {
            _context.postingPriorityLevels.Remove(await _context.postingPriorityLevels.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public async Task<int> DeleteSpecialSkillById(int id)
        {
            _context.specialSkillTypes.Remove(await _context.specialSkillTypes.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeleteMedicalMainCategoryById(int id)
        {
            _context.medicalMainCategories.Remove(await _context.medicalMainCategories.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> DeletemedicalSubCategoryById(int id)
        {
            _context.medicalSubCategories.Remove(await _context.medicalSubCategories.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<MedicalSubCategory>> getMedicalSubCategoty()
        {
            var result = await _context.medicalSubCategories.Include(x => x.medicalMainCategory).AsNoTracking().ToListAsync();
            return result;
        }



        public async Task<IEnumerable<TraningLogHistory>> GetTraningLogHistoryByEmpId(int empId)
        {
            var result = await _context.TraningLogHistories.Include(x => x.trainingCategory).Include(x => x.country).Include(x => x.trainingInstitute).Where(x => x.employeeId == empId).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<EmployeeListVM>> GetEmployeeSearchByUnitId(int unitId)
        {

            var result = await (from e in _context.EmployeeInfos.Where(e => e.branchId == unitId)
                                select new EmployeeListVM
                                {
                                    employeeCode = e.employeeCode,
                                    name = e.nameEnglish,
                                    designation = e.designations.designationName,
                                    email = e.emailAddress,
                                    Id = e.Id,
                                    facebook = e.facebookId,
                                    linktin = e.linkdInId,
                                    twitter = e.skypeId,
                                    unit = e.branch.branchUnitName,
                                    phone = e.mobileNumberPersonal,
                                    url = _context.Photographs.Where(c => c.employeeId == e.Id).Select(x => x.url).FirstOrDefault()
                                }).ToListAsync();
            return result;
        }
        public async Task<EmployeeBasicInfoForPosting> GetEmployeeBasicInfoForPostingByempId(int Id)
        {

            var result = await (from e in _context.EmployeeInfos.Include(x => x.rank).Where(e => e.Id == Id)
                                select new EmployeeBasicInfoForPosting
                                {
                                    bp = e.employeeCode,
                                    name = e.nameEnglish,
                                    rank = e.rank.rankName,
                                    picture = _context.Photographs.Where(x => x.type == "profile" && x.employeeId == Id).Select(x => x.url).FirstOrDefault(),
                                    priviousPosting = _context.Assignments.Where(x => x.employeeId == Id && x.statusId == 3 && x.isDelete == 1).OrderByDescending(x => Convert.ToDateTime(x.StartDate)).Select(x => x.specialBranchUnit.branchUnitName).FirstOrDefault(),
                                    currentPosting = _context.Assignments.Where(x => x.employeeId == Id && x.statusId == 3 && x.isDelete != 1).OrderByDescending(x => Convert.ToDateTime(x.StartDate)).Select(x => x.specialBranchUnit.branchUnitName).FirstOrDefault(),
                                    HomeDistrict = _context.AddressInformation.Where(x => x.employeeInfoId == Id).Where(x => x.type == "Permanent Address").Select(x => x.district.districtName).FirstOrDefault(),
                                    SpouseHomeDistrict = _context.Spouses.Where(x => x.employeeId == Id).Where(x => x.spouseRelationId == 5).Select(x => x.district.districtName).FirstOrDefault(),
                                    Education = _context.EducationalQualifications.Where(x => x.employeeId == Id).Where(x => x.degreeId != 151).OrderByDescending(x => x.passingYear).Select(x => x.degree.degreeName).FirstOrDefault(),
                                    lastPromotionDate = _context.PromotionLogs.Where(x => x.employeeId == Id).OrderByDescending(x => x.date).Select(x => x.date).FirstOrDefault().ToString("dd-MMM-yyyy"),
                                    assignments = _context.Assignments.Where(x => x.employeeId == Id && x.statusId == 3 && x.isDelete.GetValueOrDefault(0) != 1).OrderByDescending(x => Convert.ToDateTime(x.StartDate)).Select(x => x.specialBranchUnit.branchUnitName.ToString() + ", " + x.section.Name.ToString() + ", (" + Convert.ToDateTime(x.StartDate).ToString("dd-MM-yyyy") + " to " + Convert.ToDateTime(x.EndDate).ToString("dd-MM-yyyy") + ")").ToList(),
                                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EmployeeListVM>> GetEmployeeSearchByRankId(int rankId)
        {

            var result = await (from e in _context.EmployeeInfos.Where(e => e.rankId == rankId)
                                select new EmployeeListVM
                                {
                                    employeeCode = e.employeeCode,
                                    name = e.nameEnglish,
                                    designation = e.designations.designationName,
                                    email = e.emailAddress,
                                    Id = e.Id,
                                    facebook = e.facebookId,
                                    linktin = e.linkdInId,
                                    twitter = e.skypeId,
                                    unit = e.branch.branchUnitName,
                                    phone = e.mobileNumberPersonal,
                                    url = _context.Photographs.Where(c => c.employeeId == e.Id).Select(x => x.url).FirstOrDefault()
                                }).ToListAsync();
            return result;

        }

        public async Task<IEnumerable<EmployeeListVM>> GetEmployeeSearchByDistrictId(int districtId)
        {

            var result = await (from e in _context.EmployeeInfos.Include(x => x.Photographs).Where(x => x.AddressInformation.FirstOrDefault().districtId == districtId)
                                select new EmployeeListVM
                                {
                                    employeeCode = e.employeeCode,
                                    name = e.nameEnglish,
                                    designation = e.designations.designationName,
                                    email = e.emailAddress,
                                    Id = e.Id,
                                    facebook = e.facebookId,
                                    linktin = e.linkdInId,
                                    twitter = e.skypeId,
                                    unit = e.branch.branchUnitName,
                                    phone = e.mobileNumberPersonal,
                                    url = _context.Photographs.Where(c => c.employeeId == e.Id).Select(x => x.url).FirstOrDefault()
                                }).ToListAsync();

            return result;
        }

        public async Task<EmployeeInfo> EmployeeInfoDetails(int id)
        {
            var result = await _context.EmployeeInfos
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveAwardInformation(AwardEntry awardEntry)
        {
            try
            {
                if (awardEntry.Id != 0)
                {
                    _context.AwardEntries.Update(awardEntry);
                }
                else
                {
                    _context.AwardEntries.Add(awardEntry);
                }

                await _context.SaveChangesAsync();
                return awardEntry.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveAwardEntryHistory(AwardEntryHistory awardEntry)
        {
            try
            {
                if (awardEntry.Id != 0)
                {
                    _context.AwardEntryHistories.Update(awardEntry);
                }
                else
                {
                    _context.AwardEntryHistories.Add(awardEntry);
                }

                await _context.SaveChangesAsync();
                return awardEntry.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AwardEntry> GetAwardInfoById(int Id)
        {
            var result = await _context.AwardEntries.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<AwardEntry>> GetAwardInfoByEmpId(int empId)
        {
            var result = await _context.AwardEntries.Where(x => x.employeeId == empId).Include(x => x.award).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AwardEntryHistory>> GetAwardEntryHistoryByEmpId(int empId)
        {
            var result = await _context.AwardEntryHistories.Where(x => x.employeeId == empId).Include(x => x.award).ToListAsync();
            return result;
        }

        public async Task<int> SaveDisciplinaryInformation(DisciplinaryAction disciplinaryAction)
        {
            try
            {
                if (disciplinaryAction.Id != 0)
                {
                    _context.DisciplinaryActions.Update(disciplinaryAction);
                }
                else
                {
                    _context.DisciplinaryActions.Add(disciplinaryAction);
                }

                await _context.SaveChangesAsync();
                return disciplinaryAction.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveForeignTravel(ForeignTravel foreignTravel)
        {
            try
            {
                if (foreignTravel.Id != 0)
                {
                    _context.ForeignTravels.Update(foreignTravel);
                }
                else
                {
                    _context.ForeignTravels.Add(foreignTravel);
                }

                await _context.SaveChangesAsync();
                return foreignTravel.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ForeignTravel> GetForeignTravelById(int Id)
        {
            var result = await _context.ForeignTravels.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveForeignTravelHistory(ForeignTravelHistory foreignTravel)
        {
            try
            {
                if (foreignTravel.Id != 0)
                {
                    _context.ForeignTravelHistories.Update(foreignTravel);
                }
                else
                {
                    _context.ForeignTravelHistories.Add(foreignTravel);
                }

                await _context.SaveChangesAsync();
                return foreignTravel.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteForeignTravel(int id)
        {
            _context.ForeignTravels.Remove(await _context.ForeignTravels.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<DisciplinaryAction>> GetDisciplinaryByEmpId(int empId)
        {
            var result = await _context.DisciplinaryActions.Include(x => x.Offense).Include(x => x.naturalPunishment).Where(x => x.employeeId == empId).ToListAsync();
            return result;
        }


        public async Task<int> GetinternalAssignmentMasterIdByEnlishmentId(int id)
        {
            var result = await _context.InternalAssignmentMasters.Where(x => x.internalEnlistedAssignmentMasterId == id).Select(x => x.Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> DeleteFamilyInfoById(int id)
        {
            _context.Spouses.Remove(await _context.Spouses.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteReturnResaonById(int id)
        {
            _context.EmployeeReturnReasons.Remove(await _context.EmployeeReturnReasons.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteAssignmentInfoById(int id)
        {
            _context.Assignments.Remove(await _context.Assignments.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeletePresentAddressById(int id)
        {
            try
            {
                _context.AddressInformation.Remove(await _context.AddressInformation.FindAsync(id));
                var save = await _context.SaveChangesAsync();
                if (save > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteEducationalQualificationInfoById(int id)
        {
            _context.EducationalQualifications.Remove(await _context.EducationalQualifications.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteDisciplinaryById(int id)
        {
            _context.DisciplinaryActions.Remove(await _context.DisciplinaryActions.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteAwardById(int id)
        {
            _context.AwardEntries.Remove(await _context.AwardEntries.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteTrainingInfoById(int id)
        {
            _context.TraningLogs.Remove(await _context.TraningLogs.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteEnListedList(string refNo)
        {
            var master = await _context.EnlistedAssignmentMasters.Where(x => x.refNo == refNo).FirstOrDefaultAsync();
            if (master != null)
            {
                var ellistList = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMasterId == master.Id).ToListAsync();
                _context.EnlistedAssignments.RemoveRange(ellistList);
                var delete = await _context.SaveChangesAsync();
                _context.EnlistedAssignmentMasters.Remove(master);
                var save = await _context.SaveChangesAsync();
                if (save > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        public async Task<int> UpdateEnListedEmplyeeStatus(string refNo)
        {
            try
            {
                var master = await _context.EnlistedAssignmentMasters.Where(x => x.refNo == refNo).FirstOrDefaultAsync();
                if (master != null)
                {
                    var enlistList = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMasterId == master.Id && x.statusId == 1).ToListAsync();
                    if (enlistList.Count() > 0)
                    {
                        foreach (var item in enlistList)
                        {
                            item.statusId = 2;
                            _context.EnlistedAssignments.Update(item);
                            _context.SaveChanges();
                        }
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<CheckBranchViewModel>> CheckDistrictTypeBySecIdAndEmpId(int branchId, int employeeid)
        {
            //var sections = new List<int>();
            //var message = "ok";

            var Address = await (from e in _context.EmployeeInfos
                                 join a in _context.AddressInformation on e.Id equals a.employeeInfoId
                                 join sb in _context.SpecialBranchUnits on a.districtId equals sb.districtsId
                                 where e.Id == employeeid && sb.Id == branchId && sb.districtsId != 66 && a.type == "Permanent Address"
                                 select new CheckBranchViewModel
                                 {
                                     SBUId = sb.Id,
                                     branchId = sb.Id,
                                     isDefault = sb.isdefault,
                                     reason = a.type
                                 }).Distinct().ToListAsync();

            var spouseAddress = await (from s in _context.Spouses
                                       join sb in _context.SpecialBranchUnits on s.districtId equals sb.districtsId
                                       where s.employeeId == employeeid && sb.Id == branchId && sb.districtsId != 66 && s.spouseRelationId == 5
                                       select new CheckBranchViewModel
                                       {
                                           SBUId = sb.Id,
                                           branchId = sb.Id,
                                           isDefault = sb.isdefault,
                                           reason = "Spouse Address"
                                       }).Distinct().ToListAsync();
            var Joining = await _context.Assignments.Where(x => x.employeeId == employeeid).OrderByDescending(x => x.StartDate).Take(3)
                .Select(x => new CheckBranchViewModel { SBUId = branchId, branchId = x.specialBranchUnitId, reason = "Previous Work Place" }).ToListAsync();
            var joinQ = Address.Concat(Joining.Where(x => x.branchId == branchId)).Concat(spouseAddress);
            return joinQ;
        }

        #region Address
        public async Task<int> SaveAddressInformation(AddressInformation addressInformation)
        {
            try
            {
                if (addressInformation.Id != 0)
                {
                    _context.AddressInformation.Update(addressInformation);
                }
                else
                {
                    _context.AddressInformation.Add(addressInformation);
                }

                await _context.SaveChangesAsync();
                return addressInformation.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveAddressInformationHistory(AddressInformationHistory addressInformation)
        {
            try
            {
                if (addressInformation.Id != 0)
                {
                    _context.AddressInformationHistories.Update(addressInformation);
                }
                else
                {
                    _context.AddressInformationHistories.Add(addressInformation);
                }

                await _context.SaveChangesAsync();
                return addressInformation.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAddressInfoByEmpIdType(int empId, string type)
        {
            try
            {
                var address = await _context.AddressInformation.Where(x => x.employeeInfoId == empId && x.type == type).FirstOrDefaultAsync();
                if (address != null)
                {
                    _context.AddressInformation.Remove(address);
                    int save = await _context.SaveChangesAsync();
                    if (save > 0)
                    {
                        return 1;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AddressInformation> GetAddressById(int Id)
        {
            var result = await _context.AddressInformation.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<AddressInformation>> GetAddressInformationByEmpId(int empId)
        {
            var result = await _context.AddressInformation
                .Include(x => x.country)
                .Include(x => x.division)
                .Include(x => x.district)
                .Include(x => x.thana)
                .Include(x => x.unionWard)
                .Include(x => x.village)
                .Where(x => x.employeeInfoId == empId)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AddressInformationHistory>> GetAddressInformationHistoryByEmpId(int empId)
        {
            var result = await _context.AddressInformationHistories
                .Include(x => x.country)
                .Include(x => x.division)
                .Include(x => x.district)
                .Include(x => x.thana)
                .Include(x => x.unionWard)
                .Include(x => x.village)
                .Where(x => x.employeeInfoId == empId)
                .ToListAsync();
            return result;
        }

        public async Task<AddressInformation> GetAddressByEmpIdType(int empId, string type)
        {
            var result = await _context.AddressInformation.Include(x => x.country).Include(x => x.division).Include(x => x.district).Include(x => x.thana).Include(x => x.unionWard).Include(x => x.village).Where(x => x.employeeInfoId == empId && x.type == type).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<TraningLog>> GetTraningLogInfoByEmpIdType(int empId, string type)
        {
            var result = await _context.TraningLogs.Include(x => x.trainingCategory).Include(x => x.country).Include(x => x.trainingInstitute).Where(x => x.employeeId == empId && x.trainingType == type).ToListAsync();
            return result;
        }

        #endregion

        #region Assign
        public async Task<int> SaveAssignMaster(AssignmentMaster assignment)
        {
            try
            {
                if (assignment.Id > 0)
                {
                    _context.AssignmentMasters.Update(assignment);
                }
                else
                {
                    await _context.AssignmentMasters.AddAsync(assignment);
                }
                await _context.SaveChangesAsync();
                return assignment.Id;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> SaveAssignDetails(Assignment assignment)
        {
            try
            {
                if (assignment.Id > 0)
                {
                    _context.Assignments.Update(assignment);
                }
                else
                {
                    await _context.Assignments.AddAsync(assignment);
                }
                await _context.SaveChangesAsync();
                return assignment.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> GetTotalPHQEmployee()
        {
            return _context.EmployeeInfos.Where(x => x.branchId == 1).Count();
        }

        public async Task<IEnumerable<EnlistedViewModel>> EnListedMasterDetails(string userId)
        {
            var data = new List<EnlistedViewModel>();
            var enList = (from m in _context.EnlistedAssignmentMasters.Where(x => x.ApplicationUserId == userId && x.statusId == 1 && x.typeId == 1)
                          join e in (from ea in _context.EnlistedAssignments.Where(x => x.statusId == 1)
                                     group ea by new { ea.statusId, ea.enlistedAssignmentMasterId } into ee
                                     select new { enlistedAssignmentMasterId = ee.Key.enlistedAssignmentMasterId }) on m.Id equals e.enlistedAssignmentMasterId
                          select m).ToList();
            //var enList = await _context.EnlistedAssignmentMasters.Where(x => x.ApplicationUserId == userId && x.statusId == 1 && x.typeId == 1).ToListAsync();
            foreach (var item in enList)
            {
                var newData = new EnlistedViewModel();
                var enlistedEmployee = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMasterId == item.Id).Include(x => x.employee).Include(x => x.employee.rank).ToListAsync();
                if (enlistedEmployee.Count() > 0)
                {
                    newData.enlistedEmployee = enlistedEmployee;
                }
                else
                {
                    newData.enlistedEmployee = new List<EnlistedAssignment>();
                }
                newData.enlistedAssignmentMasters = item;
                newData.employeeInfo = await _context.EmployeeInfos.Where(x => x.employeeCode == item.ApplicationUser.UserName).FirstOrDefaultAsync();
                data.Add(newData);
            };
            return data;
        }

        public async Task<IEnumerable<Assignment>> AssignmentDetailsByMasterId(int id)
        {
            var result = await _context.Assignments.Include(x => x.assignmentMaster)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.section.specialBranchUnit.districts)
                .Include(x => x.assignmentMaster)
                .Include(x => x.section.specialBranchUnit.districts)
                .Where(x => x.assignmentMasterId == id)
                .ToListAsync();
            return result;
        }

        public async Task<int> GetEnlistMasterId(string refNo)
        {
            var result = _context.EnlistedAssignmentMasters
                .Where(x => x.refNo == refNo).Select(x => x.Id).FirstOrDefault();
            return result;
        }

        public async Task<int> UpdateAssignmentInfoForIGP(int id)
        {
            var assign = await _context.Assignments.Where(x => x.assignmentMasterId == id).ToListAsync();
            if (assign.Count() > 0)
            {
                foreach (var item in assign)
                {
                    item.statusId = 2;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
            }
            return 1;
        }

        public async Task<int> UpdateAssignmentInfoForIGPUndo(int id)
        {
            var assign = await _context.Assignments.Where(x => x.assignmentMasterId == id).ToListAsync();
            if (assign.Count() > 0)
            {
                foreach (var item in assign)
                {
                    item.statusId = 1;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
            }
            return 1;
        }

        public async Task<int> UpdateEmployeeInfo(int id)
        {
            var assign = await _context.Assignments.Where(x => x.assignmentMasterId == id).ToListAsync();
            if (assign.Count() > 0)
            {
                foreach (var item in assign)
                {
                    var emp = await _context.EmployeeInfos.FindAsync(item.employeeId);
                    var unit = await _context.Sections.Where(x => x.Id == item.sectionId).FirstOrDefaultAsync();
                    emp.sectionId = item.sectionId;
                    emp.branchId = unit.specialBranchUnitId;
                    emp.joiningDatePresentWorkstation = DateTime.Now.Date;
                    _context.Update(emp);
                    await _context.SaveChangesAsync();
                }
            }
            return 1;
        }

        public async Task<int> UpdateEmployeeInfoAfterArticle47(int id)
        {
            var assign = await _context.Assignments.FindAsync(id);
            var emp = await _context.EmployeeInfos.FindAsync(assign.employeeId);
            //var unit = await _context.Sections.Where(x => x.Id == assign.sectionId).FirstOrDefaultAsync();
            emp.sectionId = assign.sectionId;
            emp.branchId = assign.specialBranchUnitId;
            emp.joiningDatePresentWorkstation = assign.StartDate;
            _context.Update(emp);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<IEnumerable<EnlistedViewModel>> FrezzMasterDetails(string userId)
        {
            var data = new List<EnlistedViewModel>();
            var enList = await _context.EnlistedAssignmentMasters.Where(x => x.ApplicationUserId == userId && x.statusId == 1 && x.typeId == 2).ToListAsync();
            foreach (var item in enList)
            {
                var newData = new EnlistedViewModel();
                var enlistedEmployee = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMasterId == item.Id).Include(x => x.employee).Include(x => x.employee.rank).ToListAsync();
                if (enlistedEmployee.Count() > 0)
                {
                    newData.enlistedEmployee = enlistedEmployee;
                }
                else
                {
                    newData.enlistedEmployee = new List<EnlistedAssignment>();
                }
                newData.enlistedAssignmentMasters = item;
                newData.employeeInfo = await _context.EmployeeInfos.Where(x => x.employeeCode == item.ApplicationUser.UserName).FirstOrDefaultAsync();
                data.Add(newData);
            };
            return data;
        }

        public async Task<IEnumerable<EnlistedAssignment>> EnListedDetails(string userId)
        {
            var data = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMaster.ApplicationUserId == userId && x.typeId == 1).ToListAsync(); ;
            return data;
        }

        public async Task<IEnumerable<EnlistedAssignment>> FrezzDetails(string userId)
        {
            var data = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMaster.ApplicationUserId == userId && x.typeId == 2).ToListAsync(); ;
            return data;
        }

        public async Task<int> ReturnAssignmentMaster(int id)
        {
            var data = await _context.AssignmentMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
            data.statusId = 4;
            _context.AssignmentMasters.Update(data);
            var save = await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> ReturnInternalAssignmentMaster(int id)
        {
            var data = await _context.InternalEnlistedAssignmentMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
            data.statusId = 5;
            _context.InternalEnlistedAssignmentMasters.Update(data);
            var save = await _context.SaveChangesAsync();
            return 1;
        }
        #endregion


        #region Medical

        public async Task<int> SaveMedicalInfo(MedicalInfo medicalInfo)
        {
            try
            {
                if (medicalInfo.Id != 0)
                {
                    _context.MedicalInfos.Update(medicalInfo);
                }
                else
                {
                    _context.MedicalInfos.Add(medicalInfo);
                }

                await _context.SaveChangesAsync();
                return medicalInfo.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveMedicalDiseaseInfo(EmployeeMedicalDisease medicalInfo)
        {
            try
            {
                if (medicalInfo.Id != 0)
                {
                    _context.EmployeeMedicalDiseases.Update(medicalInfo);
                }
                else
                {
                    _context.EmployeeMedicalDiseases.Add(medicalInfo);
                }

                await _context.SaveChangesAsync();
                return medicalInfo.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveMedicalVaccineInfo(EmployeeMedicalVaccine medicalInfo)
        {
            try
            {
                if (medicalInfo.Id != 0)
                {
                    _context.EmployeeMedicalVaccines.Update(medicalInfo);
                }
                else
                {
                    _context.EmployeeMedicalVaccines.Add(medicalInfo);
                }

                await _context.SaveChangesAsync();
                return medicalInfo.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<MedicalInfo>> GetMedicalInfoByEmpIdType(int empId)
        {
            var result = await _context.MedicalInfos.Where(x => x.employeeInfoId == empId).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnitParent()
        {
            var data = new List<SpecialBranchUnit>();
            if (!cache.TryGetValue("SBU", out data))
            {
                data = await _context.SpecialBranchUnits.Where(x => x.isParent == 1).OrderBy(x => x.branchUnitName).ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(700));

                cache.Set("SBU", data, cacheEntryOptions);
            }

            return data;
            //var result = await _context.SpecialBranchUnits.Where(x => x.isParent == 1).OrderBy(x => x.branchUnitName).ToListAsync();
            //return result;
        }

        public async Task<IEnumerable<Section>> GetSectionWiseUnit()
        {
            var data = new List<Section>();
            if (!cache.TryGetValue("Sections", out data))
            {
                data = await _context.Sections
                .Include(x => x.specialBranchUnit.districts)
                .ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(700));

                cache.Set("Sections", data, cacheEntryOptions);
            }

            return data;
            //var result = await _context.Sections
            //    .Include(x => x.specialBranchUnit)
            //    .ToListAsync();
            //return result;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetEmployeePermanentAddUnit(int empId)
        {
            var empDist = await _context.AddressInformation.Where(x => x.employeeInfoId == empId && x.type == "Permanent Address").Select(x => x.districtId).FirstOrDefaultAsync();
            var result = await _context.SpecialBranchUnits.Where(x => x.districtsId == empDist).ToListAsync();
            return result;
        }

        public async Task<SpecialBranchUnit> GetEmployeePresentUnit(int empId)
        {
            var empDist = await _context.AddressInformation.Where(x => x.employeeInfoId == empId && x.type == "Permanent Address").Select(x => x.districtId).FirstOrDefaultAsync();
            var presentUnit = await _context.Assignments.Include(x => x.specialBranchUnit).Where(x => x.employeeId == empId && x.isDelete == 1).Select(x =>
                                  new SpecialBranchUnit
                                  {
                                      Id = Convert.ToInt16(x.specialBranchUnitId),
                                      branchUnitName = x.specialBranchUnit.branchUnitName,
                                      districtsId = x.specialBranchUnit.districtsId
                                  }).FirstOrDefaultAsync();
            return presentUnit;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetEmployeePreviousUnit(int empId)
        {
            var previousUnit = await _context.Assignments
                                    .Include(x => x.specialBranchUnit)
                                    .OrderBy(x => x.StartDate)
                                    .Where(x => x.employeeId == empId && (x.isDelete != 1 || x.isDelete == null))
                                    .Take(3)
                                    .Select(x =>
                                      new SpecialBranchUnit
                                      {
                                          Id = Convert.ToInt16(x.specialBranchUnitId),
                                          branchUnitName = x.specialBranchUnit.branchUnitName,
                                          districtsId = x.specialBranchUnit.districtsId
                                      }).ToListAsync();
            return previousUnit;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetEmployeeSpouseUnit(int empId)
        {
            var spouseUnit = await (from s in _context.Spouses
                                    join sb in _context.SpecialBranchUnits on s.districtId equals sb.districtsId
                                    where s.employeeId == empId && s.spouseRelationId == 5
                                    select new SpecialBranchUnit
                                    {
                                        Id = Convert.ToInt16(sb.Id),
                                        branchUnitName = sb.branchUnitName,
                                        districtsId = sb.districtsId
                                    }).ToListAsync();
            return spouseUnit;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnitChild(int id)
        {
            var result = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == id).OrderBy(x => x.branchUnitName).ToListAsync();
            return result;
        }

        public async Task<MedicalInfo> GetMedicalInfoByEmpIdMedId(int empId, int medId)
        {
            var result = await _context.MedicalInfos.Where(x => x.employeeInfoId == empId && x.Id == medId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<MedicalInfoViewModel>> GetMedicalInfoByEmpId(int empId)
        {
            var result = await (from m in _context.MedicalInfos
                                where m.employeeInfoId == empId
                                select new MedicalInfoViewModel
                                {
                                    Id = m.Id,
                                    isHospitalise = m.isHospitalise,
                                    year = m.year,
                                    remarks = m.remarks,
                                    satatus = m.satatus,
                                    date = m.date,
                                    isDelete = m.isDelete,
                                    lastCheckupHistory = m.lastCheckupHistory,
                                    medicalDisease = _context.EmployeeMedicalDiseases.Include(x => x.disease).Where(x => x.medicalId == m.Id).Select(x => new EmployeeMedicalDisease { disease = x.disease }).FirstOrDefault(),

                                }).ToListAsync();


            return result;
        }

        public async Task<MedicalInfo> GetMedicalInfoByMedId(int medicalId)
        {
            var result = await (from m in _context.MedicalInfos
                                where m.Id == medicalId
                                select new MedicalInfo
                                {
                                    Id = m.Id,
                                    isHospitalise = m.isHospitalise,
                                    year = m.year,
                                    remarks = m.remarks,
                                    satatus = m.satatus,
                                    date = m.date,
                                    isDelete = m.isDelete,
                                    lastCheckupHistory = m.lastCheckupHistory,
                                    medicalDiseases = _context.EmployeeMedicalDiseases.Include(x => x.disease).Where(x => x.medicalId == m.Id).Select(x => new EmployeeMedicalDisease { disease = x.disease }).ToList(),
                                    //medicalVaccines = _context.EmployeeMedicalVaccines.Include(x => x.vaccines).Where(x => x.medicalId == m.Id),
                                }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<int> DeleteMedicalInfoById(int id)
        {
            _context.MedicalInfos.Remove(await _context.MedicalInfos.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteMedicalDiseaseInfoByMedicalId(int id)
        {
            _context.EmployeeMedicalDiseases.RemoveRange(_context.EmployeeMedicalDiseases.Where(x => x.medicalId == id).ToList());
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeletePromotionInfoById(int id)
        {
            _context.PromotionLogs.Remove(await _context.PromotionLogs.FindAsync(id));
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<DisciplinaryAction>> GetDisciplinaryActions(int empId)
        {
            return await _context.DisciplinaryActions
                .Include(x => x.employee)
                .Include(x => x.Offense)
                .Include(x => x.naturalPunishment)
                .Where(x => x.employeeId == empId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Section>> GetPostingPlaceByBranchId(int id)
        {
            var data = await _context.Sections.Where(x => x.specialBranchUnitId == id).Include(x => x.specialBranchUnit.districts).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<MedicalSubCategory>> GetMedicalSubCatbyMainId(int id)
        {
            var data = await _context.medicalSubCategories.Where(x => x.medicalMainCategoryId == id).AsNoTracking().ToListAsync();
            return data;
        }

        public async Task<Section> GetSectionInfoForEmployee(int id)
        {
            var data = await _context.Sections.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<PromotionLog>> GetPromotionLogs(int empId)
        {
            return await _context.PromotionLogs
                .Include(x => x.employee)
                .Include(x => x.designationNew)
                .Include(x => x.designationOld)
                .Where(x => x.employeeId == empId)
                .AsNoTracking()
                .ToListAsync();
        }

        #endregion
        public async Task<int> GetStatusWiseEmployeeCount(int statusid)
        {
            int totalEmployees = 0;
            if (statusid == 1)
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isApproved == 1 || x.isApproved == 2).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            if (statusid == 2)
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isApproved == 1 || x.isApproved == 2).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            else if (statusid == 3)
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isApproved >= statusid).Where(x => x.employeeTypeId == 1).CountAsync();
            }
            else
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isApproved == statusid).Where(x => x.employeeTypeId == 1).CountAsync();
            }
            //  int totalEmployees =await _context.EmployeeInfos.Where(x => x.isApproved == statusid).CountAsync();
            return totalEmployees;
        }
        public async Task<int> GetCheckedEmployeeCount(int statusid)
        {
            int totalEmployees = 0;
            if (statusid == 1)
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isAdminCheck == 1).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            else if (statusid == 2)
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isApproved == 8).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            else if (statusid == 0)
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => Convert.ToDateTime(x.updatedAt).Date == DateTime.Now.Date).Where(x => x.isAdminCheck == 2 || x.isApproved == 8).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            else
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isAdminCheck == null).Where(x => x.employeeTypeId == 1).CountAsync();
            }
            //  int totalEmployees =await _context.EmployeeInfos.Where(x => x.isApproved == statusid).CountAsync();
            return totalEmployees;
        }

        public async Task<int> GetCheckedEmployeeCountByChecker(int statusid, string userName)
        {
            int totalEmployees = 0;
            if (statusid == 1)
            {
                //totalEmployees = await (from e in _context.EmployeeInfos.Where(x => x.isAdminCheck == 1)
                //                        join te in (from et in _context.EmployeeTransectionLogs.Where(x => x.ApplicationUser.UserName == userName && x.statusInfoId == 6) group et by et.employeeInfoId into ett select new { employeeInfoId = ett.Key }) on e.Id equals te.employeeInfoId
                //                        select e).CountAsync();
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isAdminCheck == 1 && x.updatedBy == userName).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            else if (statusid == 2)
            {
                //totalEmployees = await (from e in _context.EmployeeInfos.Where(x => x.isAdminCheck == 2 || x.isApproved == 8)
                //                        join te in (from et in _context.EmployeeTransectionLogs.Where(x => x.ApplicationUser.UserName == userName && x.statusInfoId == 6) group et by et.employeeInfoId into ett from tee in ett.DefaultIfEmpty() select new { employeeInfoId = tee.employeeInfoId }) on e.Id equals te.employeeInfoId
                //                        select e).CountAsync();
                totalEmployees = await _context.EmployeeInfos.Where(x => (x.isAdminCheck == 2 || x.isApproved == 8) && x.updatedBy == userName).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            else if (statusid == 0)
            {
                //totalEmployees = await (from e in _context.EmployeeInfos.Where(x => Convert.ToDateTime(x.updatedAt).Date == DateTime.Now.Date).Where(x => x.isAdminCheck == 2 || x.isApproved == 8)
                //                     join te in(from et in _context.EmployeeTransectionLogs.Where(x => x.ApplicationUser.UserName == userName && x.statusInfoId == 6) group et by et.employeeInfoId into ett from tee in ett.DefaultIfEmpty() select new { employeeInfoId=tee.employeeInfoId}) on e.Id equals te.employeeInfoId
                //                     select e).CountAsync();



                totalEmployees = await _context.EmployeeInfos.Where(x => Convert.ToDateTime(x.updatedAt).Date == DateTime.Now.Date).Where(x => (x.isAdminCheck == 2 || x.isApproved == 8) && x.updatedBy == userName).Where(x => x.employeeTypeId == 1).CountAsync();

            }
            else
            {
                totalEmployees = await _context.EmployeeInfos.Where(x => x.isAdminCheck == null && x.updatedBy == userName).Where(x => x.employeeTypeId == 1).CountAsync();
            }
            //  int totalEmployees =await _context.EmployeeInfos.Where(x => x.isApproved == statusid).CountAsync();
            return totalEmployees;
        }

        public async Task<List<UnitWiseEmployeeCountModel>> GetUnitWiseEmployeeCount()
        {
            List<UnitWiseEmployeeCountModel> unit = new List<UnitWiseEmployeeCountModel>();
            try
            {
                var employeeInfos = await _context.EmployeeInfos.Where(x => x.branchId != null).Include(x => x.branch).Where(x => x.employeeTypeId == 1).AsNoTracking().ToListAsync();
                var specialBranchUnits = await _context.SpecialBranchUnits.AsNoTracking().ToListAsync();
                foreach (var branch in specialBranchUnits)
                {
                    unit.Add(new UnitWiseEmployeeCountModel
                    {
                        unitId = branch.Id,
                        unitName = branch.branchUnitName,
                        count = employeeInfos.Where(x => x.branchId == branch.Id).Count(),
                    });
                }
                return unit.OrderByDescending(x => x.count).ToList();

            }
            catch (Exception ex)
            {

                return unit;
            }
        }

        public async Task<RankWiseEmployeeCountModel> GetRankWiseEmployeeCount()
        {
            RankWiseEmployeeCountModel rankModel = new RankWiseEmployeeCountModel();
            try
            {
                int[] rankIds = { 2, 4, 6, 8, 10, 12, 13, 14 };
                List<RankWiseEmployeeCountModel> ranklist = new List<RankWiseEmployeeCountModel>();
                foreach (var rank in _context.Ranks.Where(x => rankIds.Contains(x.Id)).OrderByDescending(x => x.shortOrder))
                {
                    ranklist.Add(new RankWiseEmployeeCountModel
                    {
                        rankId = rank.Id,
                        rankName = rank.rankName,
                        count = await _context.EmployeeInfos.Where(x => x.rankId == rank.Id).CountAsync(),
                    });
                }
                rankModel.allRanks = ranklist.Select(x => x.rankName).ToList();
                rankModel.noOfEmployees = ranklist.Select(x => (int)x.count).ToList();
                rankModel.rankIds = ranklist.Select(x => (int)x.rankId).ToList();

                return rankModel;

            }
            catch (Exception ex)
            {

                return rankModel;
            }

        }


        public async Task<RankWiseEmployeeCountModel> GetRankWiseEmployeeCountChecked()
        {
            RankWiseEmployeeCountModel rankModel = new RankWiseEmployeeCountModel();
            try
            {
                int[] rankIds = { 2, 4, 6, 8, 10, 12, 13, 14 };
                var employeeInfos = await _context.EmployeeInfos.Where(x => x.rankId != null && rankIds.Contains((int)x.rankId)).Where(x => x.isAdminCheck == 1).Include(x => x.rank).Where(x => x.employeeTypeId == 1).AsNoTracking().ToListAsync();
                List<RankWiseEmployeeCountModel> ranklist = new List<RankWiseEmployeeCountModel>();
                foreach (var employeeInfo in employeeInfos.OrderByDescending(x => x.rank.shortOrder).GroupBy(x => x.rankId))
                {
                    ranklist.Add(new RankWiseEmployeeCountModel
                    {
                        rankId = employeeInfo.Key,
                        rankName = employeeInfo.Select(x => (x.rank.rankName != null) ? x.rank.rankName : "").FirstOrDefault(),
                        // shortOrder = employeeInfo.Select(x => x.rank.shortOrder==null)).FirstOrDefault(),
                        count = employeeInfo.Count(),
                    });
                }
                //  var ranks = ranklist.OrderBy(x => x.shortOrder).ToList();
                rankModel.allRanks = ranklist.Select(x => x.rankName).ToList();
                rankModel.noOfEmployees = ranklist.Select(x => (int)x.count).ToList();
                rankModel.rankIds = ranklist.Select(x => (int)x.rankId).ToList();

                return rankModel;

            }
            catch (Exception ex)
            {

                return rankModel;
            }

        }

        public async Task<List<DivisionWiseEmployeeCountModel>> GetDivisionWiseEmployeeCount()
        {
            List<DivisionWiseEmployeeCountModel> divlist = new List<DivisionWiseEmployeeCountModel>();
            try
            {

                //var employeeInfos = await _context.EmployeeInfos.Where(x => x.branchId != null).Include(x => x.branch.districts.division).AsNoTracking().ToListAsync();
                var divisionList = await _context.Divisions.AsNoTracking().ToListAsync();

                foreach (var div in divisionList)
                {

                    divlist.Add(new DivisionWiseEmployeeCountModel
                    {
                        label = div.divisionName,
                        divId = div.Id,
                        //value = employeeInfos.Where(x => x.branch.districts.divisionId == div.Id).Count()
                        value = await _context.EmployeeInfos.Where(x => x.branch.districts.divisionId == div.Id).Where(x => x.employeeTypeId == 1).CountAsync()
                    });
                }

                return divlist;

            }
            catch (Exception ex)
            {

                return divlist;
            }


        }

        public async Task<BatchWiseEmployeeCountModel> GetBatchWiseEmployeeCount()
        {
            BatchWiseEmployeeCountModel batchModel = new BatchWiseEmployeeCountModel();
            try
            {
                //var employeeInfos = await _context.EmployeeInfos.Where(x => x.bCSBatchId != null).Include(x => x.bCSBatch).Where(x=>x.employeeTypeId==1).AsNoTracking().ToListAsync();
                List<BatchWiseEmployeeCountModel> batchlist = new List<BatchWiseEmployeeCountModel>();
                foreach (var batch in _context.BCSBatches.OrderByDescending(x => x.Id))
                {
                    batchlist.Add(new BatchWiseEmployeeCountModel
                    {
                        batchId = batch.Id,
                        batchName = batch.batchName,
                        count = await _context.EmployeeInfos.Where(x => x.bCSBatchId == batch.Id).CountAsync(),
                    });
                }
                //  var ranks = ranklist.OrderBy(x => x.shortOrder).ToList();
                batchModel.allBatches = batchlist.Select(x => x.batchName).ToList();
                batchModel.noOfEmployees = batchlist.Select(x => (int)x.count).ToList();
                batchModel.batchIds = batchlist.Select(x => (int)x.batchId).ToList();

                return batchModel;

            }
            catch (Exception ex)
            {

                return batchModel;
            }
        }

        public async Task<int> GetTodaysEmployeesStatusWise(int statusid)
        {
            int totalEmployees = 0;
            if (statusid == 1)
            {
                totalEmployees = _context.EmployeeInfos.Where(x => (x.isApproved == 1 || x.isApproved == 2) && Convert.ToDateTime(x.createdAt).Date == DateTime.Now.Date).Where(x => x.employeeTypeId == 1).Count();

            }
            if (statusid == 2)
            {
                totalEmployees = _context.EmployeeInfos.Where(x => (x.isApproved == 1 || x.isApproved == 2) && Convert.ToDateTime(x.createdAt).Date == DateTime.Now.Date).Where(x => x.employeeTypeId == 1).Count();

            }
            else
            {
                totalEmployees = _context.EmployeeInfos.Where(x => x.isApproved == statusid && Convert.ToDateTime(x.createdAt).Date == DateTime.Now.Date).Where(x => x.employeeTypeId == 1).Count();
            }

            return totalEmployees;
            //_context.EmployeeInfos.Where(x => x.isApproved == statusid && Convert.ToDateTime(x.createdAt).Date == DateTime.Now.Date).Count();
        }

        public async Task<IEnumerable<GlobalSearchModel>> GetGlobalSearchInfo(string userName, string filter)
        {
            var data = await _context.globalSearchModels.FromSql($"SP_GetGlobalSearch {userName},{filter}").ToListAsync();
            return data;
        }

        public async Task<IEnumerable<GetPortfolioTransectionHistoryLog>> GetPortfolioTransectionHistoryLog(int empId)
        {
            var data = await _context.transectionHistoryLogs.FromSql($"SP_GetPortfolioTransectionHistoryLog {empId}").ToListAsync();
            return data;
        }

        public async Task<PIMSDataModel> GetPIMSDataModel(string bp)
        {
            var data = await _context.pIMSDataModels.FromSql($"SP_GettblPIMSEmployeeUpdated {bp}").FirstOrDefaultAsync();
            return data;
        }

        public async Task<LPRDateModel> GetLPRDateByBirthDate(string birthDate)
        {
            var data = await _context.lPRDates.FromSql($"SP_GET_DateOfRetirement {birthDate}").FirstOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<OfficerImageModel>> GetOfficerImages()
        {
            try
            {
                var data = await _context.OfficerImageModels.FromSql($"SP_Get_OfficerInfoForOneTouch").ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<EmployeePercentWisePrograssModel>> GetPercentWisePrograss()
        {
            var data = await _context.employeePercentWisePrograsses.FromSql($"SP_Percent_Wise_Prograss").ToListAsync();
            return data;
        }

        public async Task<List<EmployeeReport>> GetEmployeeInfos(string queryString)
        {
            try
            {
                int[] userIds = { 4869 };
                //IQueryable<EmployeeInfo> queryData = _context.EmployeeInfos.Where(x => x.bCSBatchId != null).Include(x => x.branch).Include(x => x.rank).Include(x => x.religion).Include(x => x.bCSBatch);
                IQueryable<EmployeeInfo> queryData = (from e in _context.EmployeeInfos
                                                         join rr in _context.Ranks on e.rankId equals rr.Id into rrr
                                                         from r in rrr.DefaultIfEmpty()
                                                         join bcsb in _context.BCSBatches on e.bCSBatchId equals bcsb.Id into bsb
                                                         from bcs in bsb.DefaultIfEmpty()
                                                         join br in _context.SpecialBranchUnits on e.branchId equals br.Id into brr
                                                         from b in brr.DefaultIfEmpty()
                                                          join rll in _context.Religions on e.branchId equals rll.Id into rrl
                                                          from rl in rrl.DefaultIfEmpty()
                                                          join eg in _context.EmployeeGradations on e.Id equals eg.employeeId
                                                         where eg.isDelete.GetValueOrDefault(0) == 0 && !userIds.Contains(e.Id)
                                                      select new EmployeeInfo
                                                         {
                                                             Id = e.Id,
                                                             activityStatus = e.activityStatus,
                                                             religion=rl,
                                                             ApplicationUserId = e.ApplicationUserId,
                                                             attachmentBranchId = e.attachmentBranchId,
                                                             bankAccount = e.bankAccount,
                                                             banksId = e.banksId,
                                                             bCSBatchId = e.bCSBatchId,
                                                             bCSBatch = bcs,
                                                             bcsPosition = e.bcsPosition,
                                                             birthIdentificationNo = e.birthIdentificationNo,
                                                             birthPlace = e.birthPlace,
                                                             bloodGroup = e.bloodGroup,
                                                             branchId = e.branchId,
                                                             branch = b,
                                                             countryId = e.countryId,
                                                             emailAddressPersonal = e.emailAddressPersonal,
                                                             dateOfBirth = e.dateOfBirth,
                                                             emailAddress = e.emailAddress,
                                                             employeeCode = e.employeeCode,
                                                             employeeTypeId = e.employeeTypeId,
                                                             extraActivity = e.extraActivity,
                                                             extraActivitys = e.extraActivitys,
                                                             extraSkill = e.extraSkill,
                                                             designationsId = e.designationsId,
                                                             dateOfPermanent = e.dateOfPermanent,
                                                             dateofregularity = e.dateofregularity,
                                                             disability = e.disability,
                                                             departmentalPromotionYear = e.departmentalPromotionYear,
                                                             drivingLicense = e.drivingLicense,
                                                             facebookId = e.facebookId,
                                                             fatherNameBangla = e.fatherNameBangla,
                                                             fatherNameEnglish = e.fatherNameEnglish,
                                                             freedomFighter = e.freedomFighter,
                                                             freedomFighterNo = e.freedomFighterNo,
                                                             gender = e.gender,
                                                             govtID = e.govtID,
                                                             gpfAcNo = e.gpfAcNo,
                                                             gradationSerial = eg.gradationSerial,
                                                             height = e.height,
                                                             homeDistrict = e.homeDistrict,
                                                             identificationSign = e.identificationSign,
                                                             isApproved = e.isApproved,
                                                             isDelete = e.isDelete,
                                                             joiningDateGovtService = e.joiningDateGovtService,
                                                             joiningDatePresentWorkstation = e.joiningDatePresentWorkstation,
                                                             joiningDesignation = e.joiningDesignation,
                                                             linkdInId = e.linkdInId,
                                                             LPRDate = e.LPRDate,
                                                             maritalStatus = e.maritalStatus,
                                                             mobileNumberOffice = e.mobileNumberOffice,
                                                             mobileNumberPersonal = e.mobileNumberPersonal,
                                                             motherNameBangla = e.motherNameBangla,
                                                             motherNameEnglish = e.motherNameEnglish,
                                                             nameBangla = e.nameBangla,
                                                             nameEnglish = e.nameEnglish,
                                                             nationalID = e.nationalID,
                                                             nationality = e.nationality,
                                                             otherBankAccountNo = e.otherBankAccountNo,
                                                             otherBanksId = e.otherBanksId,
                                                             pabx = e.pabx,
                                                             passportNo = e.passportNo,
                                                             pHQTRTypeId = e.pHQTRTypeId,
                                                             PRLEndDate = e.PRLEndDate,
                                                             PRLStartDate = e.PRLStartDate,
                                                             promotionDate = e.promotionDate,
                                                             rank = r,
                                                             rankId = e.rankId,
                                                             religionId = e.religionId,
                                                             salaryAccountNo = e.salaryAccountNo,
                                                             rationId = e.rationId,
                                                             sectionId = e.sectionId,
                                                             seniorityNumber = e.seniorityNumber,
                                                             departmentId = e.departmentId,
                                                             servicePeriod = e.servicePeriod,
                                                             weight = e.weight,
                                                             skypeId = e.skypeId,
                                                             specialSkill = e.specialSkill
                                                         }).AsQueryable();

                #region Filtering ...
                string[] Tokens = queryString.Split("|");
                List<string> lstBP = new List<string>();
                List<int?> lstUnitId = new List<int?>();
                List<int?> lstRankId = new List<int?>();
                List<int?> lstBCSBatchId = new List<int?>();
                string length = string.Empty;

                foreach (string token in Tokens)
                {
                    string[] SepToken = token.Split("=");
                    if (SepToken.Length > 1)
                    {
                        if (SepToken[0] == "Gender")
                        {
                            queryData = queryData.Where(x => x.gender == SepToken[1]);
                        }
                        else if (SepToken[0] == "unit")
                        {
                            lstUnitId.Add(Convert.ToInt32(SepToken[1]));
                            int notUnit = Tokens.Where(x => !x.Contains(SepToken[0])).Count();
                            if (lstUnitId.Count == Math.Abs(Tokens.Where(x => x.Contains(SepToken[0])).Count() - notUnit + 1))
                                queryData = queryData.Where(x => lstUnitId.Contains(x.branchId));
                            //queryData = queryData.Where(x => x.branchId == Convert.ToInt32(SepToken[1]));
                        }
                        else if (SepToken[0] == "Name")
                        {
                            queryData = queryData.Where(x => x.nameEnglish.Contains(SepToken[1]));
                        }
                        else if (SepToken[0] == "Bp")
                        {
                            lstBP.Add(SepToken[1]);
                            int notBp = Tokens.Where(x => !x.Contains(SepToken[0])).Count();
                            if (lstBP.Count == Math.Abs(Tokens.Where(x => x.Contains(SepToken[0])).Count() - notBp))
                            {
                                queryData = queryData.Where(x => lstBP.Contains(x.employeeCode));
                            }
                            else
                            {
                                queryData = queryData.Where(x => lstBP.Contains(x.employeeCode));
                            }

                        }
                        else if (SepToken[0] == "Status")
                        {
                            if (Convert.ToInt32(SepToken[1]) == 0)
                            {
                                queryData = queryData.Where(x => x.isApproved == 3 || x.isApproved == 8);
                            }
                            else
                            {
                                queryData = queryData.Where(x => x.isApproved == Convert.ToInt32(SepToken[1]));
                            }
                        }
                        else if (SepToken[0] == "PostingStatus")
                        {
                            if (Convert.ToInt32(SepToken[1]) == 1)
                            {
                                List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                                queryData = queryData.Where(x => Ids.Contains(x.Id));
                            }
                            else if (Convert.ToInt32(SepToken[1]) == 2)
                            {
                                List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Spouse Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                                queryData = queryData.Where(x => Ids.Contains(x.Id));
                            }
                            else
                            {
                                List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Maternal Family Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                                queryData = queryData.Where(x => Ids.Contains(x.Id));
                            }
                        }
                        else if (SepToken[0] == "rank")
                        {
                            lstRankId.Add(Convert.ToInt32(SepToken[1]));
                            int notRank = Tokens.Where(x => !x.Contains(SepToken[0])).Count();
                            if (lstRankId.Count == Math.Abs(Tokens.Where(x => x.Contains(SepToken[0])).Count() - notRank + 1))
                                queryData = queryData.Where(x => lstRankId.Contains(x.rankId));
                            //queryData = queryData.Where(x => x.rankId == Convert.ToInt32(SepToken[1]));
                        }
                        else if (SepToken[0] == "batch")
                        {
                            lstBCSBatchId.Add(Convert.ToInt32(SepToken[1]));
                            int notBCS = Tokens.Where(x => !x.Contains(SepToken[0])).Count();
                            if (lstBCSBatchId.Count == Math.Abs(Tokens.Where(x => x.Contains(SepToken[0])).Count() - notBCS + 1))
                                queryData = queryData.Where(x => lstBCSBatchId.Contains(x.bCSBatchId));
                            //queryData = queryData.Where(x => x.bCSBatchId == Convert.ToInt32(SepToken[1]));
                        }
                        else if (SepToken[0] == "Disability") queryData = queryData.Where(x => x.disability == SepToken[1]);
                        else if (SepToken[0] == "MaritalStatus") queryData = queryData.Where(x => x.maritalStatus == SepToken[1]);
                        else if (SepToken[0] == "Religion") queryData = queryData.Where(x => x.religionId == Int32.Parse(SepToken[1]));
                        else if (SepToken[0] == "BloodGroup")
                        {
                            //if (!SepToken[1].Contains("-"))
                            //{
                            //    SepToken[1] = SepToken[1] + "+";
                            //}
                            queryData = queryData.Where(x => x.bloodGroup.Replace("+", " ") == SepToken[1]);

                        }
                        else if (SepToken[0] == "EmployeePosition") queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                        else if (SepToken[0] == "FreedomFighter") queryData = queryData.Where(x => x.freedomFighter == (SepToken[1] == "Yes" ? true : false));
                        else if (SepToken[0] == "NatureRecrutement") queryData = queryData.Where(x => x.natureOfRequitment == SepToken[1]);
                        else if (SepToken[0] == "joiningDesignation") queryData = queryData.Where(x => x.joiningDesignation == SepToken[1]);
                        else if (SepToken[0] == "CurrentDesignation") queryData = queryData.Where(x => x.designation == SepToken[1]);
                        else if (SepToken[0] == "Division")
                        {
                            //List<int> branchsIds = await _context.SpecialBranchUnits.Where(x => x.divisionId == Int32.Parse(SepToken[1])).AsNoTracking().Select(x => (int)x.Id).ToListAsync();
                            //List<int> Ids = await _context.transferLogs.Where(x => branchsIds.Contains((int)x.workStationId) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                            //queryData = queryData.Where(x => Ids.Contains(x.Id));

                        }
                        else if (SepToken[0] == "District")
                        {
                            //List<int> branchsIds = await _context.branchOfficeUnits.Where(x => x.districtId == Int32.Parse(SepToken[1])).AsNoTracking().Select(x => (int)x.Id).ToListAsync();
                            //List<int> Ids = await _context.transferLogs.Where(x => branchsIds.Contains((int)x.workStationId) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                            //queryData = queryData.Where(x => Ids.Contains(x.Id));

                        }
                        else if (SepToken[0] == "Thana")
                        {
                            //List<int> branchsIds = await _context.branchOfficeUnits.Where(x => x.thanaId == Int32.Parse(SepToken[1])).AsNoTracking().Select(x => (int)x.Id).ToListAsync();
                            //List<int> Ids = await _context.transferLogs.Where(x => branchsIds.Contains((int)x.workStationId) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                            //queryData = queryData.Where(x => Ids.Contains(x.Id));

                        }
                        else if (SepToken[0] == "Degree")
                        {
                            List<int> Ids = await _context.EducationalQualifications.Where(x => x.degreeId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else if (SepToken[0] == "Expertise")
                        {
                            var exprt = await _context.badgeAndActivityModels.FromSql($"SP_GETAllEmployeeIcons").AsNoTracking().ToListAsync();
                            if (SepToken[1] == "Locked")
                            {
                                List<int?> Ids = exprt.Where(x => x.lockedEmpId > 0).Select(x => x.employeeId).ToList();
                                queryData = queryData.Where(x => Ids.Contains(x.Id));

                            }
                            else if (SepToken[1] == "UN Mission")
                            {
                                List<int?> Ids = exprt.Where(x => x.pHQTRTypeId > 0).Select(x => x.employeeId).ToList();
                                queryData = queryData.Where(x => Ids.Contains(x.Id));
                            }
                            else
                            {
                                List<int?> Ids = exprt.Where(x => x.expertise == SepToken[1]).Select(x => x.employeeId).ToList();
                                queryData = queryData.Where(x => Ids.Contains(x.Id));
                            }
                        }
                        else if (SepToken[0] == "Group")
                        {
                            //List<int> Ids = await _context.educationalQualifications.Where(x => x.reldegreesubjectId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                            //queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else if (SepToken[0] == "University")
                        {
                            List<int> Ids = await _context.EducationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else if (SepToken[0] == "SpouseHomeDistrict")
                        {
                            List<int> Ids = await _context.Spouses.Where(x => x.spouseRelationId == 5).Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else if (SepToken[0] == "HomeDistrict")
                        {
                            List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address").Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeInfoId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else if (SepToken[0] == "AdverseComment")
                        {
                            //List<int> Ids = await _context.acrInfos.Where(x => x.advanceComment == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                            //queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else if (SepToken[0] == "WorkStation")
                        {
                            //List<int> Ids = await _context.transferLogs.Where(x => x.workStationId == Int32.Parse(SepToken[1]) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                            //queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "dateOfBirth") queryData = queryData.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2])));

                        else if (SepToken[0] == "joiningDatePresentWorkstation") queryData = queryData.Where(x => (x.joiningDatePresentWorkstation >= DateTime.Parse(SepToken[1]) && x.joiningDatePresentWorkstation <= DateTime.Parse(SepToken[2])));

                        else if (SepToken[0] == "LPRDate") queryData = queryData.Where(x => (x.LPRDate >= DateTime.Parse(SepToken[1]) && x.LPRDate <= DateTime.Parse(SepToken[2])));

                        else if (SepToken[0] == "dateOfPermanent") queryData = queryData.Where(x => (x.joiningDateGovtService >= DateTime.Parse(SepToken[1]) && x.joiningDateGovtService <= DateTime.Parse(SepToken[2])));

                        else if (SepToken[0] == "ServiceFromDate")
                        {
                            List<int> Ids = await _context.Assignments.Where(x => (x.StartDate >= DateTime.Parse(SepToken[1]) && x.StartDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "ServiceToDate")
                        {
                            List<int> Ids = await _context.Assignments.Where(x => (x.EndDate >= DateTime.Parse(SepToken[1]) && x.EndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "TrainingFromDate")
                        {
                            List<int> Ids = await _context.TraningLogs.Where(x => (x.fromDate >= DateTime.Parse(SepToken[1]) && x.fromDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "TrainingToDate")
                        {
                            List<int> Ids = await _context.TraningLogs.Where(x => (x.toDate >= DateTime.Parse(SepToken[1]) && x.toDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "dateOfPromotion")
                        {
                            List<int> Ids = await _context.PromotionLogs.Where(x => (x.date >= DateTime.Parse(SepToken[1]) && x.date <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "UNStart")
                        {
                            List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose == "UN Mission").Where(x => (x.travelDate >= DateTime.Parse(SepToken[1]) && x.travelDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "UNEnd")
                        {
                            List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose == "UN Mission").Where(x => (x.travelEndDate >= DateTime.Parse(SepToken[1]) && x.travelEndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "ForignStart")
                        {
                            List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose != "UN Mission").Where(x => (x.travelDate >= DateTime.Parse(SepToken[1]) && x.travelDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "ForignEnd")
                        {
                            List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose != "UN Mission").Where(x => (x.travelEndDate >= DateTime.Parse(SepToken[1]) && x.travelEndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }

                        else if (SepToken[0] == "awardDate")
                        {
                            List<int> Ids = await _context.AwardEntries.Where(x => (x.awardDate >= DateTime.Parse(SepToken[1]) && x.awardDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                    }
                }
                #endregion

                #region Result Process
                List<EmployeeInfo> data = await queryData.ToListAsync();
                List<EmployeeReport> filteredData = new List<EmployeeReport>();

                foreach (EmployeeInfo employeeInfo in data.OrderBy(x=>x.gradationSerial).ThenBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition.GetValueOrDefault(1000)))
                {

                    var photographs = _context.Photographs.Where(x => x.employeeId == employeeInfo.Id && x.type == "profile").FirstOrDefault();
                    string homeDistrict = _context.AddressInformation.Include(x => x.district).Where(x => x.employeeInfoId == employeeInfo.Id && x.type == "Permanent Address").FirstOrDefault().district?.districtName;
                    //var loan = _context.loanInformations.Where(x => x.employeeId == employeeInfo.Id).FirstOrDefault();
                    //var gPF = _context.gPFFundWithdraws.Where(x => x.employeeId == employeeInfo.Id).FirstOrDefault();

                    filteredData.Add(new EmployeeReport
                    {
                        employeeId = employeeInfo.Id,
                        employeeCode = (employeeInfo.employeeCode == null) ? "" : employeeInfo.employeeCode,
                        imageUrl = photographs?.url,
                        nameEnglish = (employeeInfo.nameEnglish == null) ? "" : employeeInfo.nameEnglish,
                        unit = (employeeInfo.branch?.branchUnitName == null) ? "" : employeeInfo.branch?.branchUnitName,
                        rank = (employeeInfo.rank?.rankName == null) ? "" : employeeInfo.rank?.rankName,
                        bcsBatch = (employeeInfo.bCSBatch?.batchName == null) ? "" : employeeInfo.bCSBatch?.batchName,
                        gender = employeeInfo.gender == null ? "" : employeeInfo.gender,
                        religion = employeeInfo.religion?.name == null ? "" : employeeInfo.religion?.name,
                        meritePosition = employeeInfo.bcsPosition.ToString() == null ? "" : employeeInfo.bcsPosition.ToString(),

                        homeDistrict = homeDistrict,
                        emailOffice = employeeInfo.emailAddress,
                        fatherName = employeeInfo.fatherNameEnglish,
                        motherName = employeeInfo.motherNameEnglish,
                        officeMobile = employeeInfo.mobileNumberOffice,
                        personalMobile = employeeInfo.mobileNumberPersonal,
                        emailPersona = employeeInfo.emailAddressPersonal,
                        nid = employeeInfo.nationalID,
                        gradationNo=employeeInfo.gradationSerial,
                        dateOfbirth = employeeInfo.dateOfBirth?.ToString("dd-MMM-yyyy")
                    });
                }
                #endregion

                return filteredData;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<EmployeeReport>> GetEmployeeInformationByQueryList(EmployeeReport model)
        {
            int? empId = 0;
            try
            {
                //List<EmployeeInfo> queryData = new List<EmployeeInfo>();
                //List<EmployeeInfo> newQueryData = _context.EmployeeInfos.Include(x => x.rank).Include(x => x.bCSBatch).Include(x => x.branch).ToList();
                int[] userIds = { 4869 };
                List<EmployeeInfo> newQueryData = await (from e in _context.EmployeeInfos
                                    join rr in _context.Ranks on e.rankId equals rr.Id into rrr
                                    from r in rrr.DefaultIfEmpty()
                                    join bcsb in _context.BCSBatches on e.bCSBatchId equals bcsb.Id into bsb
                                    from bcs in bsb.DefaultIfEmpty()
                                    join br in _context.SpecialBranchUnits on e.branchId equals br.Id into brr
                                    from b in brr.DefaultIfEmpty()
                                    join eg in _context.EmployeeGradations on e.Id equals eg.employeeId
                                    where eg.isDelete.GetValueOrDefault(0) == 0 && !userIds.Contains(e.Id)
                                    select new EmployeeInfo
                                    {
                                        Id=e.Id,
                                        activityStatus=e.activityStatus,ApplicationUserId=e.ApplicationUserId,attachmentBranchId=e.attachmentBranchId,bankAccount=e.bankAccount,banksId=e.banksId,bCSBatchId=e.bCSBatchId,bCSBatch=bcs,bcsPosition=e.bcsPosition,
                                        birthIdentificationNo=e.birthIdentificationNo,birthPlace=e.birthPlace,bloodGroup=e.bloodGroup,branchId=e.branchId,branch=b,countryId=e.countryId,
                                        emailAddressPersonal=e.emailAddressPersonal,dateOfBirth=e.dateOfBirth,emailAddress=e.emailAddress,employeeCode=e.employeeCode,employeeTypeId=e.employeeTypeId,extraActivity=e.extraActivity,
                                        extraActivitys=e.extraActivitys,extraSkill=e.extraSkill,designationsId=e.designationsId,dateOfPermanent=e.dateOfPermanent,dateofregularity=e.dateofregularity,disability=e.disability,departmentalPromotionYear=e.departmentalPromotionYear,
                                        drivingLicense=e.drivingLicense,facebookId=e.facebookId,fatherNameBangla=e.fatherNameBangla,fatherNameEnglish=e.fatherNameEnglish,freedomFighter=e.freedomFighter,freedomFighterNo=e.freedomFighterNo,gender=e.gender,govtID=e.govtID,
                                        gpfAcNo=e.gpfAcNo,gradationSerial=eg.gradationSerial,height=e.height,homeDistrict=e.homeDistrict,identificationSign=e.identificationSign,isApproved=e.isApproved,isDelete=e.isDelete,joiningDateGovtService=e.joiningDateGovtService,
                                        joiningDatePresentWorkstation=e.joiningDatePresentWorkstation,joiningDesignation=e.joiningDesignation,linkdInId=e.linkdInId,LPRDate=e.LPRDate,maritalStatus=e.maritalStatus,mobileNumberOffice=e.mobileNumberOffice,mobileNumberPersonal=e.mobileNumberPersonal,
                                        motherNameBangla=e.motherNameBangla,motherNameEnglish=e.motherNameEnglish,nameBangla=e.nameBangla,nameEnglish=e.nameEnglish,nationalID=e.nationalID,nationality=e.nationality,otherBankAccountNo=e.otherBankAccountNo,otherBanksId=e.otherBanksId,
                                        pabx=e.pabx,passportNo=e.passportNo,pHQTRTypeId=e.pHQTRTypeId,PRLEndDate=e.PRLEndDate,PRLStartDate=e.PRLStartDate,promotionDate=e.promotionDate,rank=r,rankId=e.rankId,religionId=e.religionId,salaryAccountNo=e.salaryAccountNo,
                                        rationId=e.rationId,sectionId=e.sectionId,seniorityNumber=e.seniorityNumber,departmentId=e.departmentId,servicePeriod=e.servicePeriod,weight=e.weight,skypeId=e.skypeId,specialSkill=e.specialSkill
                                    }).ToListAsync();


                if (model.itemDetails.Where(x => x.colName == "Bp").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Bp").Select(n => n.colValue)).Contains(x.employeeCode)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Name").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Name").Select(n => n.colValue)).Contains(x.nameEnglish)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "batch").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "batch").Select(n => Convert.ToInt32(n.colValue))).Contains(Convert.ToInt32(x.bCSBatchId.GetValueOrDefault(0)))).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "rank").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "rank").Select(n => Convert.ToInt32(n.colValue))).Contains(Convert.ToInt32(x.rankId.GetValueOrDefault(0)))).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "unit").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "unit").Select(n => Convert.ToInt32(n.colValue))).Contains(Convert.ToInt32(x.branchId.GetValueOrDefault(0)))).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Gender").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Gender").Select(n => n.colValue)).Contains(x.gender)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Status").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Status").Select(n => n.colValue)).Contains(x.isApproved.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "PostingStatus").Count() > 0)
                {
                    if (model.itemDetails.Where(x => x.colName == "PostingStatus" && x.colValue == "1").Count() > 0)
                    {
                        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                        newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                    }
                    if (model.itemDetails.Where(x => x.colName == "PostingStatus" && x.colValue == "2").Count() > 0)
                    {
                        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Spouse Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                        newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                    }
                    if (model.itemDetails.Where(x => x.colName == "PostingStatus" && x.colValue == "3").Count() > 0)
                    {
                        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Maternal Family Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                        newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                    }
                }
                if (model.itemDetails.Where(x => x.colName == "MaritalStatus").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "MaritalStatus").Select(n => n.colValue)).Contains(x.maritalStatus.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Religion").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Religion").Select(n => n.colValue)).Contains(x.religionId.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "BloodGroup").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "BloodGroup").Select(n => n.colValue)).Contains(x.bloodGroup.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Degree").Count() > 0)
                {
                    List<int> Ids = await _context.EducationalQualifications.Where(e => (model.itemDetails.Where(m => m.colName == "Degree").Select(n => n.colValue)).Contains(e.degreeId.ToString())).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "University").Count() > 0)
                {
                    List<int> Ids = await _context.EducationalQualifications.Where(e => (model.itemDetails.Where(m => m.colName == "University").Select(n => n.colValue)).Contains(e.organizationId.ToString())).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "SpouseHomeDistrict").Count() > 0)
                {
                    List<int> Ids = await _context.Spouses.Where(e => e.spouseRelationId == 5 && (model.itemDetails.Where(m => m.colName == "SpouseHomeDistrict").Select(n => n.colValue)).Contains(e.districtId.ToString())).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "HomeDistrict").Count() > 0)
                {
                    List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address" && (model.itemDetails.Where(m => m.colName == "HomeDistrict").Select(n => n.colValue)).Contains(e.districtId.ToString())).Select(x => x.employeeInfoId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "dateOfBirth").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfBirth").Select(n => n.fromDate))) >= x.dateOfBirth
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfBirth").Select(n => n.toDate))) <= x.dateOfBirth).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "joiningDatePresentWorkstation").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "joiningDatePresentWorkstation").Select(n => n.fromDate))) >= x.joiningDatePresentWorkstation
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "joiningDatePresentWorkstation").Select(n => n.toDate))) <= x.joiningDatePresentWorkstation).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "LPRDate").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "LPRDate").Select(n => n.fromDate))) >= x.LPRDate
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "LPRDate").Select(n => n.toDate))) <= x.LPRDate).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "dateOfPermanent").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfPermanent").Select(n => n.fromDate))) >= x.joiningDateGovtService
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfPermanent").Select(n => n.toDate))) <= x.joiningDateGovtService).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ServiceFromDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ServiceFromDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ServiceFromDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.Assignments.Where(e => Convert.ToDateTime(fromDate) >= e.StartDate
                    && (Convert.ToDateTime(toDate) <= e.StartDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ServiceToDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ServiceToDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ServiceToDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.Assignments.Where(e => Convert.ToDateTime(fromDate) >= e.EndDate
                    && (Convert.ToDateTime(toDate) <= e.EndDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "TrainingFromDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "TrainingFromDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "TrainingFromDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.TraningLogs.Where(e => Convert.ToDateTime(fromDate) >= e.fromDate
                    && (Convert.ToDateTime(toDate) <= e.fromDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "TrainingToDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "TrainingToDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "TrainingToDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.TraningLogs.Where(e => Convert.ToDateTime(fromDate) >= e.toDate
                    && (Convert.ToDateTime(toDate) <= e.toDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "UNStart").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "UNStart").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "UNStart").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose == "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelDate
                    && (Convert.ToDateTime(toDate) <= e.travelDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "UNEnd").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "UNEnd").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "UNEnd").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose == "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelEndDate
                    && (Convert.ToDateTime(toDate) <= e.travelEndDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ForignStart").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ForignStart").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ForignStart").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose != "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelDate
                    && (Convert.ToDateTime(toDate) <= e.travelDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ForignEnd").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose != "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelEndDate
                    && (Convert.ToDateTime(toDate) <= e.travelEndDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "awardDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.AwardEntries.Where(e => Convert.ToDateTime(fromDate) >= e.awardDate
                    && (Convert.ToDateTime(toDate) <= e.awardDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }

                //foreach (var item in model.itemDetails)
                //{
                //    if (item.colName == "Bp")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.employeeCode==item.colValue)).OrderBy(x => x.employeeCode).ToList();
                //    }
                //    else if(item.colName=="Name")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.nameEnglish == item.colValue)).OrderBy(x => x.employeeCode).ToList();
                //    }

                //    else if (item.colName == "batch")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.bCSBatchId== Convert.ToInt32(item.colValue))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }

                //    else if (item.colName == "rank")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.rankId==Convert.ToInt32(item.colValue))).OrderBy(x => x.bcsPosition).ThenBy(x=>x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "unit")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.branchId==Convert.ToInt32(item.colValue))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "Gender")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.gender == item.colValue)).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "Status")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.isApproved == (item.colValue=="0" || item.colValue=="8"?3:Convert.ToInt32(item.colValue)))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if(item.colName== "PostingStatus")
                //    {
                //        if (item.colValue == "1")
                //        {
                //            List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                //            queryData = queryData.Concat(newQueryData.Where(x =>Ids.Contains(x.Id) )).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //        }
                //        else if (item.colValue == "2")
                //        {
                //            List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Spouse Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                //            queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //        }
                //        else
                //        {
                //            List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Maternal Family Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                //            queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //        }

                //    }

                //    else if (item.colName == "Disability")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.disability == item.colValue)).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "MaritalStatus")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.maritalStatus == item.colValue)).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "Religion")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.religionId == Convert.ToInt32(item.colValue))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "BloodGroup")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.bloodGroup == item.colValue)).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "Degree")
                //    {
                //        List<int> Ids = await _context.EducationalQualifications.Where(e => e.degreeId == Convert.ToInt32(item.colValue)).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "University")
                //    {
                //        List<int> Ids = await _context.EducationalQualifications.Where(e => e.organizationId == Convert.ToInt32(item.colValue)).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "SpouseHomeDistrict")
                //    {
                //        List<int> Ids = await _context.Spouses.Where(e => e.spouseRelationId == 5 && e.districtId== Convert.ToInt32(item.colValue)).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "HomeDistrict")
                //    {
                //        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address" && e.districtId == Convert.ToInt32(item.colValue)).Select(x => x.employeeInfoId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "dateOfBirth")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x =>x.dateOfBirth>= DateTime.Parse(item.fromDate) && x.dateOfBirth <= DateTime.Parse(item.toDate))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "joiningDatePresentWorkstation")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.joiningDatePresentWorkstation >= DateTime.Parse(item.fromDate) && x.joiningDatePresentWorkstation <= DateTime.Parse(item.toDate))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "LPRDate")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.LPRDate >= DateTime.Parse(item.fromDate) && x.LPRDate <= DateTime.Parse(item.toDate))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "dateOfPermanent")
                //    {
                //        queryData = queryData.Concat(newQueryData.Where(x => x.joiningDateGovtService >= DateTime.Parse(item.fromDate) && x.joiningDateGovtService <= DateTime.Parse(item.toDate))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "ServiceFromDate")
                //    {
                //        List<int> Ids = await _context.Assignments.Where(x => (x.StartDate >= DateTime.Parse(item.fromDate) && x.StartDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "ServiceToDate")
                //    {
                //        List<int> Ids = await _context.Assignments.Where(x => (x.EndDate >= DateTime.Parse(item.fromDate) && x.EndDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "TrainingFromDate")
                //    {
                //        List<int> Ids = await _context.TraningLogs.Where(x => (x.fromDate >= DateTime.Parse(item.fromDate) && x.fromDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "TrainingToDate")
                //    {
                //        List<int> Ids = await _context.TraningLogs.Where(x => (x.toDate >= DateTime.Parse(item.fromDate) && x.toDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "UNStart")
                //    {
                //        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose == "UN Mission").Where(x => (x.travelDate >= DateTime.Parse(item.fromDate) && x.travelDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "UNEnd")
                //    {
                //        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose == "UN Mission").Where(x => (x.travelEndDate >= DateTime.Parse(item.fromDate) && x.travelEndDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "ForignStart")
                //    {
                //        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose != "UN Mission").Where(x => (x.travelDate >= DateTime.Parse(item.fromDate) && x.travelDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "ForignEnd")
                //    {
                //        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose != "UN Mission").Where(x => (x.travelEndDate >= DateTime.Parse(item.fromDate) && x.travelEndDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }
                //    else if (item.colName == "awardDate")
                //    {
                //        List<int> Ids = await _context.AwardEntries.Where(x => (x.awardDate >= DateTime.Parse(item.fromDate) && x.awardDate <= DateTime.Parse(item.toDate))).Select(x => x.employeeId).ToListAsync();
                //        queryData = queryData.Concat(newQueryData.Where(x => Ids.Contains(x.Id))).OrderBy(x => x.bcsPosition).ThenBy(x => x.employeeCode).ToList();
                //    }

                //}

                List<EmployeeReport> employeeReports = new List<EmployeeReport>();
                foreach (var item in newQueryData.Where(x => x.isDelete.GetValueOrDefault(0) == 0).OrderBy(x=>x.gradationSerial).ThenBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition.GetValueOrDefault(1000)).ThenBy(x => x.employeeCode))
                {
                    empId = empId + 1;
                    var image = _context.Photographs.Where(y => y.employeeId == item.Id && y.type == "profile").FirstOrDefault();
                    var homeDistrict = _context.AddressInformation.Include(y => y.district).Where(y => y.employeeInfoId == item.Id && y.type == "Permanent Address").FirstOrDefault();
                    EmployeeReport employee = new EmployeeReport
                    {
                        employeeId = item.Id,
                        employeeCode = item.employeeCode,
                        imageUrl = image?.url,
                        nameEnglish = item.nameEnglish,
                        unit = item?.branch?.branchUnitName,
                        rank = item.rank?.rankName,
                        bcsBatch = item.bCSBatch?.batchName,
                        gender = item.gender,
                        religion = item.religion?.name,
                        meritePosition = item.bcsPosition.ToString(),
                        homeDistrict = homeDistrict?.district?.districtName,
                        emailOffice = item.emailAddress,
                        fatherName = item.fatherNameEnglish,
                        motherName = item.motherNameEnglish,
                        officeMobile = item.mobileNumberOffice,
                        personalMobile = item.mobileNumberPersonal,
                        emailPersona = item.emailAddressPersonal,
                        nid = item.nationalID,
                        dateOfbirth = item.dateOfBirth?.ToString("dd-MMM-yyyy"),
                        gradationNo=item.gradationSerial
                    };
                    employeeReports.Add(employee);
                }
                return employeeReports;

                //var data = newQueryData.Select(x => new EmployeeReport
                //{
                //    employeeId = x.Id,
                //    employeeCode = (x.employeeCode == null) ? "" : x.employeeCode,
                //    imageUrl = _context.Photographs.Where(y => y.employeeId == x.Id && y.type == "profile").FirstOrDefault().url,
                //    nameEnglish = (x.nameEnglish == null) ? "" : x.nameEnglish,
                //    unit = (x.branch?.branchUnitName == null) ? "" : x.branch?.branchUnitName,
                //    rank = (x.rank?.rankName == null) ? "" : x.rank?.rankName,
                //    bcsBatch = (x.bCSBatch?.batchName == null) ? "" : x.bCSBatch?.batchName,
                //    gender = x.gender == null ? "" : x.gender,
                //    religion = x.religion?.name == null ? "" : x.religion?.name,
                //    meritePosition = x.bcsPosition.ToString() == null ? "" : x.bcsPosition.ToString(),
                //    homeDistrict = _context.AddressInformation.Include(y => y.district).Where(y => y.employeeInfoId == x.Id && y.type == "Permanent Address").FirstOrDefault().district?.districtName,
                //    emailOffice = x.emailAddress,
                //    fatherName = x.fatherNameEnglish,
                //    motherName = x.motherNameEnglish,
                //    officeMobile = x.mobileNumberOffice,
                //    personalMobile = x.mobileNumberPersonal,
                //    emailPersona = x.emailAddressPersonal,
                //    nid = x.nationalID,
                //    dateOfbirth = x.dateOfBirth?.ToString("dd-MMM-yyyy")
                //}).OrderBy(x => x.meritePosition.ToString()).ThenBy(x => x.employeeCode).ToList();
                //return data;

            }
            catch (Exception ex)
            {
                empId = (int)empId;
                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeeGradationSPModel>> GetEmployeeInformationForGradation(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender)
        {
            return await _context.employeeGradationSPs.FromSql($"SP_GetEmployeeGradationList {rankId},{batchId},{fromDate},{toDate},{prlFromDate},{prlToDate},{typeId},{userName}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeeGradationSPModel>> GetEmployeeInformationForGradationNew(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender)
        {
            try
            {
                var data = await _context.employeeGradationSPs.FromSql($"SP_GetEmployeeGradationList {rankId},{batchId},{fromDate},{toDate},{prlFromDate},{prlToDate},{typeId},{userName},{bpNoSearch},{gender}").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeePreviousPostingPlaceSPModel>> GetEmployeeInformationWithPreviousPosting(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender)
        {
            try
            {
                var data = await _context.employeePreviousPostingPlaceSPs.FromSql($"SP_GetEmployeeListWithPreviousPostingPlace {rankId},{batchId},{fromDate},{toDate},{prlFromDate},{prlToDate},{typeId},{userName},{bpNoSearch},{gender}").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeeGradationInfoSPModel>> GetEmployeeGradationInformationNew(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch, string gender)
        {
            try
            {
                var data = await _context.employeeGradationInfo.FromSql($"SP_GetEmployeeGradationListNew {rankId},{batchId},{fromDate},{toDate},{prlFromDate},{prlToDate},{typeId},{userName},{bpNoSearch},{gender}").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task<EmployeeGradationSPModel> GetEmployeeInfogradationById(int employeeId)
        {
            try
            {
                var data= await _context.employeeGradationSPs.FromSql($"SP_GetEmployeeGradationByEmpId {employeeId}").FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<EmployeeGradationSPModel>> GetAllInActiveEmployeeInfo(int rankId, int batchId, string fromDate, string toDate, string prlFromDate, string prlToDate, int? typeId, string userName, string bpNoSearch)
        {
            var data = await _context.employeeGradationSPs.FromSql($"SP_GetAllInactiveEmployeeGradationList {rankId},{batchId},{fromDate},{toDate},{prlFromDate},{prlToDate},{typeId},{userName},{bpNoSearch}").AsNoTracking().ToListAsync();
            return data;
        }


        public async Task<EmployeeGradation> GetEmployeeInformationForGradation(int empId)
        {           

            var data= await _context.EmployeeGradations.Include(x => x.employee).Include(x => x.rank).Include(x => x.bCSBatch).Include(x => x.organization).Include(x => x.branch).Include(x => x.SpDistrict).Where(x => x.employeeId == empId).FirstOrDefaultAsync();
            return data;

        }

        public async Task<EmployeeGradation> GetEmployeeInfogradationById1(int empId)
        {
            return await _context.EmployeeGradations.Include(x => x.rank).Include(x => x.degree).Include(x => x.bCSBatch).Include(x => x.organization).Include(x => x.branch).Where(x => x.employeeId == empId).FirstOrDefaultAsync();
        }

        

        public async Task<bool> UpdateEmployeeGradation(GradationViewModel model)
        {
            try
            {
                
                var gradationInfo = await _context.EmployeeGradations.FindAsync(model.Id);
                var empInfo = await _context.EmployeeInfos.FindAsync(model.employeeId);
                gradationInfo.gradationSerial = Convert.ToInt32(model.gradationSerial);
                if (model.rankId.GetValueOrDefault(0) > 0)
                {
                    if (model.rankId.GetValueOrDefault(0) != empInfo.rankId)
                    {
                        gradationInfo.rankId = model.rankId;
                        gradationInfo.statusId = 2;
                    }

                }
                if (model.branchId.GetValueOrDefault(0) > 0)
                {
                    if (model.branchId.GetValueOrDefault(0) != empInfo.branchId)
                    {
                        gradationInfo.branchId = model.branchId;
                        gradationInfo.statusId = 2;
                    }

                }
                if (model.sectionId.GetValueOrDefault(0) > 0)
                {
                    if (model.sectionId.GetValueOrDefault(0) != empInfo.sectionId)
                    {
                        gradationInfo.sectionId = model.sectionId;
                        gradationInfo.statusId = 2;
                    }

                }

                if (model.batchId.GetValueOrDefault(0) > 0)
                {
                    if (model.batchId.GetValueOrDefault(0) != empInfo.bCSBatchId)
                    {
                        gradationInfo.bCSBatchId = model.batchId;
                        gradationInfo.statusId = 2;
                    }

                }
                if (model.organizationId.GetValueOrDefault(0) > 0)
                {
                    gradationInfo.organizationId = model.organizationId;
                }

                if (model.degreeId.GetValueOrDefault(0) > 0)
                {
                    gradationInfo.degreeId = model.degreeId;
                }

                if (model.bcsPosition.GetValueOrDefault(0) > 0)
                {
                    if (model.bcsPosition.GetValueOrDefault(0) != empInfo.bcsPosition)
                    {
                        gradationInfo.bcsPosition = model.bcsPosition;
                        gradationInfo.statusId = 2;
                    }

                }

                else
                {
                    gradationInfo.bcsPosition = model.bcsPosition;
                }
                if (model.lastPromotionDate == "" || model.lastPromotionDate == null)
                {

                }
                else
                {
                    gradationInfo.lastPromotionDate = Convert.ToDateTime(model.lastPromotionDate);
                    gradationInfo.statusId = 2;
                }

                if (model.joiningDatePresentWorkstation == "" || model.joiningDatePresentWorkstation == null)
                {

                }
                else
                {
                    gradationInfo.joiningDatePresentWorkstation = Convert.ToDateTime(model.joiningDatePresentWorkstation);
                    gradationInfo.statusId = 2;
                }

                if (model.firstPromotionDate == "" || model.firstPromotionDate == null)
                {
                    gradationInfo.firstPromotionDate = null;
                }
                else
                {
                    gradationInfo.firstPromotionDate = Convert.ToDateTime(model.firstPromotionDate);
                 
                }

                if (model.joiningDateGovtService == "" || model.joiningDateGovtService == null)
                {

                }
                else
                {
                    gradationInfo.joiningDateGovtService = Convert.ToDateTime(model.joiningDateGovtService);
                 
                }
                gradationInfo.experienceName = model.experienceName;
                if (model.expReqDate == "" || model.expReqDate == null)
                {

                }
                else
                {
                    gradationInfo.expReqDate = Convert.ToDateTime(model.expReqDate);
                }
                if (model.dateOfBirth == "" || model.dateOfBirth == null)
                {

                }
                else
                {
                    gradationInfo.dateOfBirth = Convert.ToDateTime(model.dateOfBirth);
                }
                if (model.homeDistrict == "" || model.homeDistrict == null)
                {

                }
                else
                {
                    gradationInfo.homeDistrict = model.homeDistrict;
                }
                if (model.gender == "" || model.gender == null)
                {

                }
                else
                {
                    gradationInfo.gender = model.gender;
                }
                if(model.nameBangla==null || model.nameBangla==string.Empty)
                {
                    gradationInfo.nameBangla = gradationInfo.nameBangla;
                }
                else
                {
                    gradationInfo.nameBangla = model.nameBangla;
                }
                
                gradationInfo.firstAdhoc = model.firstAdhoc;
                gradationInfo.secondAdhoc = model.secondAdhoc;
                gradationInfo.thirdAdhoc = model.thirdAdhoc;
                gradationInfo.forthAdhoc = model.forthAdhoc;
                gradationInfo.comments = model.comments;
                //gradationInfo.SpouseAddress = model.SpouseAddress;
                //gradationInfo.SpDivisionId = model.SpDivisionId;
                //gradationInfo.SpThanaId = model.SpThanaId;
                gradationInfo.SpDistrictId = model.SpDistrictId;
              
                gradationInfo.regularJoin = model.regularJoin;
                if (model.comments == "" || model.comments == null)
                {

                }
                else
                {
                    gradationInfo.commentDate = DateTime.Now;
                }
                int? beforeStatus = gradationInfo.isDelete;
                gradationInfo.isDelete = model.status;
                gradationInfo.educationQualification = model.educationQualification;

                _context.EmployeeGradations.Update(gradationInfo);
                var result= 1 == await _context.SaveChangesAsync();
                if((int)beforeStatus != (int)model.status)
                {
                    UpdateEmployeeGredation(gradationInfo.Id, (int)model.status, model.userName);
                }
                
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<int> UpdateEmployeeDataFromGradationById(int gradationId, string userName)
        {
            try
            {
                //var userInfo = await _context.Users.Where(x => x.UserName == userName).AsNoTracking().FirstOrDefaultAsync();
                //var gradationInfo = await _context.EmployeeGradations.Include(x => x.section).Where(x => x.Id == gradationId).AsNoTracking().AsQueryable().FirstOrDefaultAsync();
                //var empInfo = await _context.EmployeeInfos.Include(x => x.rank).Where(x => x.Id == gradationInfo.employeeId).AsNoTracking().AsQueryable().FirstOrDefaultAsync();
                //var educationInfo = await _context.EducationalQualifications.Include(x => x.degree).Where(x => x.employeeId == gradationInfo.employeeId && x.degreeId != 151).AsNoTracking().AsQueryable().OrderByDescending(x => x.passingYear).FirstOrDefaultAsync();
                //var addressInfo = await _context.AddressInformation.Include(x=>x.district).Where(x => x.employeeInfoId == gradationInfo.employeeId).AsNoTracking().ToListAsync();

                var userInfo = await _context.Users.Where(x => x.UserName == userName).AsNoTracking().FirstOrDefaultAsync();
                var gradationInfo = await _context.EmployeeGradations.Where(x => x.Id == gradationId).AsNoTracking().FirstOrDefaultAsync();
                var empInfo = await _context.EmployeeInfos.Where(x => x.Id == gradationInfo.employeeId).AsNoTracking().FirstOrDefaultAsync();
                var educationInfo = await _context.EducationalQualifications.Where(x => x.employeeId == gradationInfo.employeeId && x.degreeId != 151).AsNoTracking().OrderByDescending(x => x.passingYear).FirstOrDefaultAsync();
                var addressInfo = await _context.AddressInformation.Where(x => x.employeeInfoId == gradationInfo.employeeId).AsNoTracking().ToListAsync();
                if (addressInfo == null)
                    addressInfo = new List<AddressInformation>();

                var gradRank = await _context.Ranks.FindAsync(gradationInfo.rankId);
                var currentRank = await _context.Ranks.FindAsync(empInfo.rankId);
                var permanentAddress = addressInfo.Where(x => x.type == "Permanent Address").FirstOrDefault();
                var spouseAddress = addressInfo.Where(x => x.type == "Spouse Address").FirstOrDefault();
                //Branch
                int? gradBranchId = gradationInfo.branchId;
                int? currentBranchId = empInfo.branchId;
                //Rank
                int? gradRankId = gradationInfo.rankId; int? gradRankSortOrderId = gradRank?.shortOrder;
                int? currentRankId = empInfo.rankId; int? currRankSortOrderId = currentRank?.shortOrder;

                //BCS Batch
                int? gradBatchId = gradationInfo.bCSBatchId;
                int? currentBatchId = empInfo.bCSBatchId;


                if (gradBranchId != null && gradBranchId != currentBranchId)
                {
                    var prev = new Assignment();
                    prev = await _context.Assignments.Where(x => x.employeeId == gradationInfo.employeeId).OrderByDescending(x => x.Id).AsNoTracking().FirstOrDefaultAsync();
                    if (prev == null)
                        prev = new Assignment();

                    if (prev.employeeId > 0)
                    {
                        try
                        {
                            var AssignmentHistory = new AssignmentHistory
                            {
                                employeeId = prev.employeeId,
                                sectionId = prev.sectionId,
                                specialBranchUnitId = prev.specialBranchUnitId,
                                rankId = prev.rankId,
                                sectionName = prev.sectionName,
                                Remarks = prev.Remarks,
                                StartDate = prev.StartDate,
                                EndDate = prev.EndDate,
                                servicePeriod = prev.servicePeriod,
                                isDelete = prev.isDelete,
                                entryType = 1,
                                statusId = 3,
                                assignmentId = prev.Id,
                                UpdateUserId = userInfo.Id
                            };
                            _context.AssignmentHistories.Add(AssignmentHistory);

                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }

                        prev.EndDate = gradationInfo.joiningDatePresentWorkstation;
                        prev.isDelete = null;
                        _context.Assignments.Update(prev);
                        await _context.SaveChangesAsync();
                    }
                    
                    
                    try
                    {
                        var Assignment = new Assignment
                        {
                            employeeId = gradationInfo.employeeId,
                            sectionId = gradationInfo.sectionId,
                            specialBranchUnitId = gradationInfo.branchId,
                            rankId = gradationInfo.rankId,
                            sectionName = gradationInfo.section?.Name,
                            Remarks = "Regular",
                            StartDate = gradationInfo.joiningDatePresentWorkstation,
                            EndDate = null,
                            servicePeriod = prev.servicePeriod,
                            isDelete = 1,

                            statusId = 3,

                        };
                        _context.Assignments.Add(Assignment);

                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        int? gradationIds = gradationId;
                        throw ex;
                    }
                    

                    EmployeeTransectionLog emplog = new EmployeeTransectionLog();
                    emplog.employeeInfoId = gradationInfo.employeeId;
                    emplog.statusInfoId = 11;
                    emplog.empName = empInfo.nameEnglish;
                    emplog.ApplicationUserId = userInfo.Id;
                    emplog.remarks = "Gradation";
                    emplog.Status = empInfo.nameEnglish + "'s portfolio partially edited by " + userInfo.UserName + ", For Assignment";
                    _context.EmployeeTransectionLogs.Add(emplog);
                    await _context.SaveChangesAsync();
                }
                else
                {

                }

                if (gradRankId != null && (gradRankId != currentRankId && gradRankSortOrderId < currRankSortOrderId))
                {
                    try
                    {
                        var promotion = new PromotionLog
                        {
                            employeeId = gradationInfo.employeeId,
                            rankId = gradationInfo.rankId,
                            date = Convert.ToDateTime(gradationInfo.lastPromotionDate),
                        };
                        _context.PromotionLogs.Add(promotion);

                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        int? gradationIds = gradationId;
                        throw ex;
                    }
                    

                    EmployeeTransectionLog elog = new EmployeeTransectionLog();
                    elog.employeeInfoId = gradationInfo.employeeId;
                    elog.statusInfoId = 11;
                    elog.empName = empInfo.nameEnglish;
                    elog.ApplicationUserId = userInfo.Id;
                    elog.remarks = "Gradation";
                    elog.Status = empInfo.nameEnglish + "'s portfolio partially edited by " + userInfo.UserName + ", For Promotion";
                    _context.EmployeeTransectionLogs.Add(elog);
                    await _context.SaveChangesAsync();
                }
                else
                {

                }

                if (gradationInfo.SpDistrictId != null && (gradationInfo.SpDistrictId != spouseAddress?.districtId ))
                {
                    if (spouseAddress != null)
                    {
                        var prev = spouseAddress;
                        try
                        {
                            var AddressInformationHistory = new AddressInformationHistory
                            {
                                employeeInfoId = prev.employeeInfoId,
                                type = prev.type,
                                divisionId = prev.divisionId,
                                districtId = prev.districtId,
                                thanaId = prev.thanaId,
                                unionWardId = prev.unionWardId,
                                postCode = prev.postCode,
                                addressDetails = prev.addressDetails,
                                addressInformationId = prev.Id,
                                UpdateUserId = userInfo.Id,
                                entryType = 1,
                            };
                            _context.AddressInformationHistories.Add(AddressInformationHistory);

                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }

                        try
                        {
                            spouseAddress.divisionId = gradationInfo.SpDivisionId == null ? spouseAddress.divisionId : gradationInfo.SpDivisionId;
                            spouseAddress.districtId = gradationInfo.SpDistrictId == null ? spouseAddress.districtId : gradationInfo.SpDistrictId;
                            spouseAddress.thanaId = gradationInfo.SpThanaId == null ? spouseAddress.thanaId : gradationInfo.SpThanaId;

                            _context.AddressInformation.Update(spouseAddress);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }
                        
                    }
                    else
                    {
                        try
                        {
                            var addressInformation = new AddressInformation
                            {
                                employeeInfoId = gradationInfo.employeeId,
                                type = "Spouse Address",
                                divisionId = gradationInfo.SpDivisionId,
                                districtId = gradationInfo.SpDistrictId,
                                thanaId = gradationInfo.SpThanaId,
                                addressDetails = gradationInfo.SpouseAddress
                            };
                            _context.AddressInformation.Add(addressInformation);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }
                        
                    }
                    
                }
                else
                {

                }

                if (gradationInfo.homeDistrict != null && (gradationInfo.homeDistrict != permanentAddress?.district?.districtNameBn))
                {
                    var districtInfo = await _context.Districts.Where(x => x.districtNameBn == gradationInfo.homeDistrict).AsNoTracking().FirstOrDefaultAsync();
                    if (permanentAddress != null)
                    {
                        var prev = permanentAddress;
                        try
                        {
                            var AddressInformationHistory = new AddressInformationHistory
                            {
                                employeeInfoId = prev.employeeInfoId,
                                type = prev.type,
                                divisionId = prev.divisionId,
                                districtId = prev.districtId,
                                thanaId = prev.thanaId,
                                unionWardId = prev.unionWardId,
                                postCode = prev.postCode,
                                addressDetails = prev.addressDetails,
                                addressInformationId = prev.Id,
                                UpdateUserId = userInfo.Id,
                                entryType = 1,
                            };
                            _context.AddressInformationHistories.Add(AddressInformationHistory);

                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }

                        try
                        {
                            permanentAddress.divisionId = districtInfo?.divisionId;
                            permanentAddress.districtId = districtInfo?.Id;
                            //permanentAddress.thanaId = gradationInfo.SpThanaId == null ? spouseAddress.thanaId : gradationInfo.SpThanaId;

                            _context.AddressInformation.Update(permanentAddress);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }

                    }
                    else
                    {
                        try
                        {
                            var addressInformation = new AddressInformation
                            {
                                employeeInfoId = gradationInfo.employeeId,
                                type = "Permanent Address",
                                divisionId = districtInfo?.divisionId,
                                districtId = districtInfo?.Id,
                                thanaId = null,
                                addressDetails = null
                            };
                            _context.AddressInformation.Add(addressInformation);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }

                    }

                }
                else
                {

                }

                if (gradationInfo.educationQualification != null && (gradationInfo.educationQualification != educationInfo?.degree?.degreeName))
                {
                    if (educationInfo == null)
                    {
                        var model = educationInfo;
                        try
                        {
                            var educationalQualification = new EducationalQualification
                            {
                                employeeId = gradationInfo.employeeId,
                                degreeId = gradationInfo.degreeId == null ? educationInfo.degreeId : gradationInfo.degreeId,
                                reldegreesubjectId = _context.RelDegreeSubjects.Where(x => x.degreeId == (gradationInfo.degreeId == null ? educationInfo.degreeId : gradationInfo.degreeId)).FirstOrDefault().Id,
                                organizationId = gradationInfo.organizationId == null ? educationInfo.organizationId : gradationInfo.organizationId,
                                passingYear = Convert.ToDateTime(gradationInfo.joiningDateGovtService).Year - 2
                            };
                            _context.EducationalQualifications.Add(educationalQualification);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            int? gradationIds = gradationId;
                            throw ex;
                        }
                        
                    }
                    else
                    {
                        var model = educationInfo;
                        try
                        {
                            var educationalQualification = new EducationalQualification
                            {
                                employeeId = gradationInfo.employeeId,
                                degreeId = gradationInfo.degreeId==null?educationInfo.degreeId: gradationInfo.degreeId,
                                reldegreesubjectId = _context.RelDegreeSubjects.Where(x => x.degreeId == (gradationInfo.degreeId==null? educationInfo.degreeId : gradationInfo.degreeId)).FirstOrDefault().Id,
                                organizationId = gradationInfo.organizationId==null?educationInfo.organizationId:gradationInfo.organizationId,
                                passingYear = model.passingYear + 1
                            };
                            _context.EducationalQualifications.Add(educationalQualification);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            int? gradationIds = gradationId;
                            throw ex;
                        }
                        
                    }
                    EmployeeTransectionLog etlog = new EmployeeTransectionLog();
                    etlog.employeeInfoId = gradationInfo.employeeId;
                    etlog.statusInfoId = 11;
                    etlog.empName = empInfo.nameEnglish;
                    etlog.ApplicationUserId = userInfo.Id;
                    etlog.remarks = "Gradation";
                    etlog.Status = empInfo.nameEnglish + "'s portfolio partially edited by " + userInfo.UserName + ", For Education Qualification";
                    _context.EmployeeTransectionLogs.Add(etlog);
                    await _context.SaveChangesAsync();
                }

                if (gradationInfo.bCSBatchId != empInfo.bCSBatchId || gradationInfo.bcsPosition != empInfo.bcsPosition || gradationInfo.branchId != empInfo.branchId ||
                    gradationInfo.rankId != empInfo.rankId || gradationInfo.nameBangla != empInfo.nameBangla || gradationInfo?.joiningDatePresentWorkstation != empInfo.joiningDatePresentWorkstation
                    || gradationInfo.dateOfBirth != empInfo.dateOfBirth || gradationInfo.homeDistrict != permanentAddress?.district?.districtNameBn || gradationInfo.gender!=empInfo.gender)
                {
                    //var employee = empInfo;
                    try
                    {
                        var EmployeeInfoHistory = new EmployeeInfoHistory
                        {
                            Id = 0,
                            nameBangla = empInfo.nameBangla,
                            nameEnglish = empInfo.nameEnglish,
                            employeeCode = empInfo.employeeCode,
                            rankId = empInfo.rankId,
                            designation = empInfo.designation,
                            designationsId = empInfo.designationsId,
                            joiningDatePresentWorkstation = empInfo.joiningDatePresentWorkstation,
                            fatherNameEnglish = empInfo.fatherNameEnglish,
                            motherNameEnglish = empInfo.motherNameEnglish,
                            gender = empInfo.gender,
                            sectionName = empInfo.sectionName,
                            sectionId = empInfo.sectionId,
                            nationalID = empInfo.nationalID,
                            homeDistrict = empInfo.homeDistrict,
                            dateOfBirth = empInfo.dateOfBirth,
                            joiningDateGovtService = empInfo.joiningDateGovtService,
                            bCSBatchId = empInfo.bCSBatchId,
                            bcsPosition = empInfo.bcsPosition,
                            servicePeriod = empInfo.servicePeriod,
                            joiningDesignation = empInfo.joiningDesignation,
                            bloodGroup = empInfo.bloodGroup,
                            height = empInfo.height,
                            weight = empInfo.weight,
                            identificationSign = empInfo.identificationSign,
                            religionId = empInfo.religionId,
                            maritalStatus = empInfo.maritalStatus,
                            tribal = empInfo.tribal,
                            mobileNumberOffice = empInfo.mobileNumberOffice,
                            mobileNumberPersonal = empInfo.mobileNumberPersonal,
                            emailAddress = empInfo.emailAddress,
                            emailAddressPersonal = empInfo.emailAddressPersonal,
                            promotionDate = empInfo.promotionDate,
                            pabx = empInfo.pabx,
                            passportNo = empInfo.passportNo,
                            telephoneOffice = empInfo.telephoneOffice,
                            banksId = empInfo.banksId,
                            salaryAccountNo = empInfo.salaryAccountNo,
                            otherBanksId = empInfo.otherBanksId,
                            otherBankAccountNo = empInfo.otherBankAccountNo,
                            rationId = empInfo.rationId,
                            drivingLicense = empInfo.drivingLicense,
                            linkdInId = empInfo.linkdInId,
                            facebookId = empInfo.facebookId,
                            skypeId = empInfo.skypeId,
                            skill = empInfo.skill,
                            extraActivity = empInfo.extraActivity,
                            branchId = empInfo.branchId,
                            LPRDate = empInfo.LPRDate,
                            extraSkill = empInfo.extraSkill,
                            extraActivitys = empInfo.extraActivitys,
                            departmentalPromotionYear = empInfo.departmentalPromotionYear,
                            entryType = 1,
                            UpdateUserId = userInfo.Id,
                            pHQTRTypeId = empInfo.pHQTRTypeId,
                            countryId = empInfo.countryId,
                            attachmentBranchId = empInfo.attachmentBranchId,
                            
                            //isApproved = 2,
                        };

                        _context.EmployeeInfoHistories.Add(EmployeeInfoHistory);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        int? gradationIds = gradationId;
                        throw ex;
                    }

                }
                    try
                    {
                        var model = gradationInfo;
                    //empInfo.Id = empInfo.Id;
                        empInfo.nameBangla = model.nameBangla == null ? empInfo.nameBangla : model.nameBangla == string.Empty ? empInfo.nameBangla : model.nameBangla;
                        empInfo.nameEnglish = model.nameEnglish == null ? empInfo.nameEnglish : model.nameEnglish == string.Empty ? empInfo.nameEnglish : model.nameEnglish;
                   
                    empInfo.rankId = model.rankId == null ? empInfo.rankId : model.rankId;
                    empInfo.designationsId = model.designationsId == null ? empInfo.designationsId : model.designationsId;
                        empInfo.joiningDatePresentWorkstation = model.joiningDatePresentWorkstation == null ? empInfo.joiningDatePresentWorkstation : model.joiningDatePresentWorkstation;
                        empInfo.sectionId = model.sectionId == null ? empInfo.sectionId : model.sectionId;
                        empInfo.homeDistrict = model.homeDistrict == null ? permanentAddress?.district?.districtNameBn : model.homeDistrict;
                        empInfo.dateOfBirth = model.dateOfBirth == null ? empInfo.dateOfBirth : model.dateOfBirth;
                        empInfo.seniorityNumber = gradationInfo.gradationSerial.ToString();
                    empInfo.gender = model.gender; 
                        if (model.dateOfBirth != null && model.dateOfBirth != empInfo.dateOfBirth)
                        {
                            empInfo.LPRDate = Convert.ToDateTime(model.dateOfBirth).AddYears(59);
                        }
                        empInfo.bCSBatchId = model.bCSBatchId == null ? empInfo.bCSBatchId : model.bCSBatchId;
                        empInfo.bcsPosition = model.bcsPosition == null ? empInfo.bcsPosition : model.bcsPosition;
                    empInfo.promotionDate = model.lastPromotionDate == null ? empInfo.promotionDate : model.lastPromotionDate;

                        _context.EmployeeInfos.Update(empInfo);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        int? gradationIds = gradationId;
                        throw ex;
                    }

                try
                {
                    EmployeeTransectionLog log = new EmployeeTransectionLog();
                    log.employeeInfoId = gradationInfo.employeeId;
                    log.statusInfoId = 11;
                    log.empName = empInfo.nameEnglish;
                    log.ApplicationUserId = userInfo.Id;
                    log.remarks = "Gradation";
                    log.Status = empInfo.nameEnglish + "'s portfolio partially edited by " + userInfo.UserName + ", For Employee Info";
                    _context.EmployeeTransectionLogs.Add(log);
                    await _context.SaveChangesAsync();

                    gradationInfo.statusId = 3;
                    gradationInfo.updatedAt = DateTime.Now;
                    gradationInfo.updatedBy = userName;
                    _context.EmployeeGradations.Update(gradationInfo);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    int gradationIds = gradationId;
                    throw ex;
                }
                    
                
                gradationInfo = new EmployeeGradation();
                empInfo = new EmployeeInfo() ;
                educationInfo = new EducationalQualification();
                addressInfo = new List<AddressInformation>();
                
                return 1;
            }
            catch (Exception ex)
            {
                int? gradationIds = gradationId;
                throw ex;
            }
            
        }

        private void UpdateEmployeeGredation(int gradationId, int status, string userName)
        {
            _context.updateEmployeeGredations.FromSql($"SP_Update_All_GradationList {gradationId},{status},{userName}").AsNoTracking().FirstOrDefault();
        }

        public async Task<IEnumerable<EmployeeGradation>> GetEmployeeInformationForGradationUpdate(int status)
        {
            return await _context.EmployeeGradations.Include(x => x.rank).Include(x => x.bCSBatch).Include(x => x.organization).Include(x => x.branch)
                .Include(x => x.employee.rank).Include(x => x.employee.branch).Include(x => x.employee.bCSBatch)
                .Where(x => x.statusId == status && x.isDelete.GetValueOrDefault(0)==0).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeReport>> GetEmployeeInformationByQueryListForGradation(EmployeeReport model)
        {
            int? empId = 0;
            try
            {
                List<EmployeeInfo> queryData = new List<EmployeeInfo>();
                List<EmployeeInfo> newQueryData = _context.EmployeeInfos.Include(x => x.rank).Include(x => x.bCSBatch).Include(x => x.branch).ToList();

                if (model.itemDetails.Where(x => x.colName == "Bp").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Bp").Select(n => n.colValue)).Contains(x.employeeCode)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Name").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Name").Select(n => n.colValue)).Contains(x.nameEnglish)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "batch").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "batch").Select(n => Convert.ToInt32(n.colValue))).Contains(Convert.ToInt32(x.bCSBatchId.GetValueOrDefault(0)))).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "rank").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "rank").Select(n => Convert.ToInt32(n.colValue))).Contains(Convert.ToInt32(x.rankId.GetValueOrDefault(0)))).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "unit").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "unit").Select(n => Convert.ToInt32(n.colValue))).Contains(Convert.ToInt32(x.branchId.GetValueOrDefault(0)))).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Gender").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Gender").Select(n => n.colValue)).Contains(x.gender)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Status").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Status").Select(n => n.colValue)).Contains(x.isApproved.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "PostingStatus").Count() > 0)
                {
                    if (model.itemDetails.Where(x => x.colName == "PostingStatus" && x.colValue == "1").Count() > 0)
                    {
                        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                        newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                    }
                    if (model.itemDetails.Where(x => x.colName == "PostingStatus" && x.colValue == "2").Count() > 0)
                    {
                        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Spouse Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                        newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                    }
                    if (model.itemDetails.Where(x => x.colName == "PostingStatus" && x.colValue == "3").Count() > 0)
                    {
                        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Maternal Family Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                        newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                    }
                }
                if (model.itemDetails.Where(x => x.colName == "MaritalStatus").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "MaritalStatus").Select(n => n.colValue)).Contains(x.maritalStatus.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Religion").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "Religion").Select(n => n.colValue)).Contains(x.religionId.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "BloodGroup").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (model.itemDetails.Where(m => m.colName == "BloodGroup").Select(n => n.colValue)).Contains(x.bloodGroup.ToString())).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "Degree").Count() > 0)
                {
                    List<int> Ids = await _context.EducationalQualifications.Where(e => (model.itemDetails.Where(m => m.colName == "Degree").Select(n => n.colValue)).Contains(e.degreeId.ToString())).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "University").Count() > 0)
                {
                    List<int> Ids = await _context.EducationalQualifications.Where(e => (model.itemDetails.Where(m => m.colName == "University").Select(n => n.colValue)).Contains(e.organizationId.ToString())).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "SpouseHomeDistrict").Count() > 0)
                {
                    List<int> Ids = await _context.Spouses.Where(e => e.spouseRelationId == 5 && (model.itemDetails.Where(m => m.colName == "SpouseHomeDistrict").Select(n => n.colValue)).Contains(e.districtId.ToString())).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "HomeDistrict").Count() > 0)
                {
                    List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address" && (model.itemDetails.Where(m => m.colName == "HomeDistrict").Select(n => n.colValue)).Contains(e.districtId.ToString())).Select(x => x.employeeInfoId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "dateOfBirth").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfBirth").Select(n => n.fromDate))) >= x.dateOfBirth
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfBirth").Select(n => n.toDate))) <= x.dateOfBirth).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "joiningDatePresentWorkstation").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "joiningDatePresentWorkstation").Select(n => n.fromDate))) >= x.joiningDatePresentWorkstation
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "joiningDatePresentWorkstation").Select(n => n.toDate))) <= x.joiningDatePresentWorkstation).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "LPRDate").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "LPRDate").Select(n => n.fromDate))) >= x.LPRDate
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "LPRDate").Select(n => n.toDate))) <= x.LPRDate).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "dateOfPermanent").Count() > 0)
                {
                    newQueryData = newQueryData.Where(x => (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfPermanent").Select(n => n.fromDate))) >= x.joiningDateGovtService
                    && (Convert.ToDateTime(model.itemDetails.Where(m => m.colName == "dateOfPermanent").Select(n => n.toDate))) <= x.joiningDateGovtService).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ServiceFromDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ServiceFromDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ServiceFromDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.Assignments.Where(e => Convert.ToDateTime(fromDate) >= e.StartDate
                    && (Convert.ToDateTime(toDate) <= e.StartDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ServiceToDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ServiceToDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ServiceToDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.Assignments.Where(e => Convert.ToDateTime(fromDate) >= e.EndDate
                    && (Convert.ToDateTime(toDate) <= e.EndDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "TrainingFromDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "TrainingFromDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "TrainingFromDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.TraningLogs.Where(e => Convert.ToDateTime(fromDate) >= e.fromDate
                    && (Convert.ToDateTime(toDate) <= e.fromDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "TrainingToDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "TrainingToDate").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "TrainingToDate").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.TraningLogs.Where(e => Convert.ToDateTime(fromDate) >= e.toDate
                    && (Convert.ToDateTime(toDate) <= e.toDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "UNStart").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "UNStart").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "UNStart").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose == "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelDate
                    && (Convert.ToDateTime(toDate) <= e.travelDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "UNEnd").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "UNEnd").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "UNEnd").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose == "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelEndDate
                    && (Convert.ToDateTime(toDate) <= e.travelEndDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ForignStart").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ForignStart").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ForignStart").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose != "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelDate
                    && (Convert.ToDateTime(toDate) <= e.travelDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "ForignEnd").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.toDate).ToList();
                    List<int?> Ids = await _context.ForeignTravels.Where(e => e.travelPurpose != "UN Mission" && Convert.ToDateTime(fromDate) >= e.travelEndDate
                    && (Convert.ToDateTime(toDate) <= e.travelEndDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }
                if (model.itemDetails.Where(x => x.colName == "awardDate").Count() > 0)
                {
                    List<string> fromDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.fromDate).ToList();
                    List<string> toDate = model.itemDetails.Where(m => m.colName == "ForignEnd").Select(n => n.toDate).ToList();
                    List<int> Ids = await _context.AwardEntries.Where(e => Convert.ToDateTime(fromDate) >= e.awardDate
                    && (Convert.ToDateTime(toDate) <= e.awardDate)).Select(x => x.employeeId).ToListAsync();
                    newQueryData = newQueryData.Where(x => Ids.Contains(x.Id)).ToList();
                }

                List<EmployeeReport> employeeReports = new List<EmployeeReport>();
                foreach (var item in newQueryData.Where(x => x.isDelete.GetValueOrDefault(0) == 0).OrderBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition.GetValueOrDefault(1000)).ThenBy(x => x.employeeCode))
                {
                    empId = empId + 1;
                    var image = _context.Photographs.Where(y => y.employeeId == item.Id && y.type == "profile").FirstOrDefault();
                    var homeDistrict = _context.AddressInformation.Include(y => y.district).Where(y => y.employeeInfoId == item.Id && y.type == "Permanent Address").FirstOrDefault();

                    var promotinoInfo = _context.PromotionLogs.Include(x => x.rank).Where(x => x.employeeId == item.Id);
                    var educationInfo = _context.EducationalQualifications.Include(x => x.degree).Where(x => x.employeeId == item.Id && x.degreeId != 151).LastOrDefault();
                    string joiningRank = item.rank?.rankNameBN;
                    string seniorRank = string.Empty;
                    string seniorRankDate = string.Empty;
                    string lastPromotionDate = string.Empty;

                    string educationQualification = string.Empty;
                    if (educationInfo != null)
                    {
                        educationQualification = educationInfo.degree?.degreeNameBn;
                    }
                    if (promotinoInfo.Count() == 0)
                    {
                        joiningRank = item.rank?.rankNameBN;
                    }
                    if (promotinoInfo.Count() >= 1)
                    {
                        joiningRank = "এএসপি";
                        seniorRank = promotinoInfo.FirstOrDefault().rank.rankNameBN;
                        seniorRankDate = promotinoInfo.FirstOrDefault().date.ToString("yyyy-MM-dd");
                        lastPromotionDate = promotinoInfo.LastOrDefault().date.ToString("yyyy-MM-dd");
                    }

                    string dateOfbirth = EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(item?.dateOfBirth).Day.ToString()) + "-" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(item?.dateOfBirth).Month.ToString()) + "-" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(item?.dateOfBirth).Year.ToString());
                    string joiningDate = EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(item?.joiningDateGovtService).Day.ToString()) + "/" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(item?.joiningDateGovtService).Month.ToString()) + "/" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(item?.joiningDateGovtService).Year.ToString());
                    string lastProDate = lastPromotionDate == string.Empty ? "" : EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(lastPromotionDate).Day.ToString()) + "/" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(lastPromotionDate).Month.ToString()) + "/" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(lastPromotionDate).Year.ToString());
                    string srRankDate = seniorRankDate == string.Empty ? "" : EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(seniorRankDate).Day.ToString()) + "/" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(seniorRankDate).Month.ToString()) + "/" + EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(Convert.ToDateTime(seniorRankDate).Year.ToString());

                    EmployeeReport employee = new EmployeeReport
                    {
                        employeeId = item.Id,
                        employeeCode = EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(item.employeeCode),
                        imageUrl = image?.url,
                        nameEnglish = item.nameBangla,
                        unit = item?.branch?.branchUnitNameBN,
                        rank = item.rank?.rankNameBN,
                        bcsBatch = item.bCSBatch?.batchNameBn,
                        gender = item.gender == "Female" ? "মহিলা" : "পুরুষ",
                        religion = item.religion?.nameBn,
                        meritePosition = EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(item.bcsPosition.ToString()),
                        homeDistrict = homeDistrict?.district?.districtNameBn,
                        emailOffice = item.emailAddress,
                        fatherName = item.fatherNameBangla,
                        motherName = item.motherNameBangla,
                        officeMobile = EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(item.mobileNumberOffice),
                        personalMobile = EnglishToBanglaNumber.ConvertEnglishNumToBanglaNum(item.mobileNumberPersonal),
                        emailPersona = item.emailAddressPersonal,
                        nid = item.nationalID,
                        joiningRank = joiningRank,
                        educationQualification = educationQualification,
                        dateOfbirth = dateOfbirth,
                        joiningDate = joiningDate,
                        lastPromotionDate = lastProDate,
                        seniorRankDate = srRankDate
                    };
                    employeeReports.Add(employee);
                }
                return employeeReports;
            }
            catch (Exception ex)
            {
                empId = (int)empId;
                throw ex;
            }
        }
        
        

        public async Task<List<EmployeeSearchReport>> GetEmployeeInfosBySearch(string queryString)
        {

            IQueryable<EmployeeInfo> queryData = _context.EmployeeInfos.Where(x => x.bCSBatchId != null).Include(x => x.branch).Include(x => x.rank).Include(x => x.religion).Include(x => x.bCSBatch);

            #region Filtering ...
            string[] Tokens = queryString.Split("|");

            string length = string.Empty;

            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "Gender")
                    {
                        queryData = queryData.Where(x => x.gender == SepToken[1]);
                    }
                    else if (SepToken[0] == "unit")
                    {
                        queryData = queryData.Where(x => x.branchId == Convert.ToInt32(SepToken[1]));
                    }
                    else if (SepToken[0] == "Name")
                    {
                        queryData = queryData.Where(x => x.nameEnglish.Contains(SepToken[1]));
                    }
                    else if (SepToken[0] == "Bp")
                    {
                        queryData = queryData.Where(x => x.employeeCode.Contains(SepToken[1]));
                    }
                    else if (SepToken[0] == "Status")
                    {
                        if (Convert.ToInt32(SepToken[1]) == 0)
                        {
                            queryData = queryData.Where(x => x.isApproved == 3 || x.isApproved == 8);
                        }
                        else
                        {
                            queryData = queryData.Where(x => x.isApproved == Convert.ToInt32(SepToken[1]));
                        }
                    }
                    else if (SepToken[0] == "PostingStatus")
                    {
                        if (Convert.ToInt32(SepToken[1]) == 1)
                        {
                            List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else if (Convert.ToInt32(SepToken[1]) == 2)
                        {
                            List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Spouse Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else
                        {
                            List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Maternal Family Address").Where(x => x.districtId == x.employeeInfo.branch.districtsId).Select(x => x.employeeInfoId).ToListAsync();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                    }
                    else if (SepToken[0] == "rank")
                    {
                        queryData = queryData.Where(x => x.rankId == Convert.ToInt32(SepToken[1]));
                    }
                    else if (SepToken[0] == "batch")
                    {
                        queryData = queryData.Where(x => x.bCSBatchId == Convert.ToInt32(SepToken[1]));
                    }
                    else if (SepToken[0] == "Disability") queryData = queryData.Where(x => x.disability == SepToken[1]);
                    else if (SepToken[0] == "MaritalStatus") queryData = queryData.Where(x => x.maritalStatus == SepToken[1]);
                    else if (SepToken[0] == "Religion") queryData = queryData.Where(x => x.religionId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "BloodGroup")
                    {
                        //if (!SepToken[1].Contains("-"))
                        //{
                        //    SepToken[1] = SepToken[1] + "+";
                        //}
                        queryData = queryData.Where(x => x.bloodGroup.Replace("+", " ") == SepToken[1]);

                    }
                    else if (SepToken[0] == "EmployeePosition") queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "FreedomFighter") queryData = queryData.Where(x => x.freedomFighter == (SepToken[1] == "Yes" ? true : false));
                    else if (SepToken[0] == "NatureRecrutement") queryData = queryData.Where(x => x.natureOfRequitment == SepToken[1]);
                    else if (SepToken[0] == "joiningDesignation") queryData = queryData.Where(x => x.joiningDesignation == SepToken[1]);
                    else if (SepToken[0] == "CurrentDesignation") queryData = queryData.Where(x => x.designation == SepToken[1]);
                    else if (SepToken[0] == "Division")
                    {
                        //List<int> branchsIds = await _context.SpecialBranchUnits.Where(x => x.divisionId == Int32.Parse(SepToken[1])).AsNoTracking().Select(x => (int)x.Id).ToListAsync();
                        //List<int> Ids = await _context.transferLogs.Where(x => branchsIds.Contains((int)x.workStationId) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                        //queryData = queryData.Where(x => Ids.Contains(x.Id));

                    }
                    else if (SepToken[0] == "District")
                    {
                        //List<int> branchsIds = await _context.branchOfficeUnits.Where(x => x.districtId == Int32.Parse(SepToken[1])).AsNoTracking().Select(x => (int)x.Id).ToListAsync();
                        //List<int> Ids = await _context.transferLogs.Where(x => branchsIds.Contains((int)x.workStationId) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                        //queryData = queryData.Where(x => Ids.Contains(x.Id));

                    }
                    else if (SepToken[0] == "Thana")
                    {
                        //List<int> branchsIds = await _context.branchOfficeUnits.Where(x => x.thanaId == Int32.Parse(SepToken[1])).AsNoTracking().Select(x => (int)x.Id).ToListAsync();
                        //List<int> Ids = await _context.transferLogs.Where(x => branchsIds.Contains((int)x.workStationId) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                        //queryData = queryData.Where(x => Ids.Contains(x.Id));

                    }
                    else if (SepToken[0] == "Degree")
                    {
                        List<int> Ids = await _context.EducationalQualifications.Where(x => x.degreeId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Expertise")
                    {
                        var exprt = await _context.badgeAndActivityModels.FromSql($"SP_GETAllEmployeeIcons").AsNoTracking().ToListAsync();
                        if (SepToken[1] == "Locked")
                        {
                            List<int?> Ids = exprt.Where(x => x.lockedEmpId > 0).Select(x => x.employeeId).ToList();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));

                        }
                        else if (SepToken[1] == "UN Mission")
                        {
                            List<int?> Ids = exprt.Where(x => x.pHQTRTypeId > 0).Select(x => x.employeeId).ToList();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                        else
                        {
                            List<int?> Ids = exprt.Where(x => x.expertise == SepToken[1]).Select(x => x.employeeId).ToList();
                            queryData = queryData.Where(x => Ids.Contains(x.Id));
                        }
                    }
                    else if (SepToken[0] == "Group")
                    {
                        //List<int> Ids = await _context.educationalQualifications.Where(x => x.reldegreesubjectId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        //queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "University")
                    {
                        List<int> Ids = await _context.EducationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "SpouseHomeDistrict")
                    {
                        List<int> Ids = await _context.Spouses.Where(x => x.spouseRelationId == 5).Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "HomeDistrict")
                    {
                        List<int?> Ids = await _context.AddressInformation.Where(e => e.type == "Permanent Address").Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeInfoId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "AdverseComment")
                    {
                        //List<int> Ids = await _context.acrInfos.Where(x => x.advanceComment == SepToken[1]).Select(x => x.employeeId).ToListAsync();
                        //queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "WorkStation")
                    {
                        //List<int> Ids = await _context.transferLogs.Where(x => x.workStationId == Int32.Parse(SepToken[1]) && x.isActive == 1).Select(x => (int)x.employeeId).ToListAsync();
                        //queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "dateOfBirth") queryData = queryData.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "joiningDatePresentWorkstation") queryData = queryData.Where(x => (x.joiningDatePresentWorkstation >= DateTime.Parse(SepToken[1]) && x.joiningDatePresentWorkstation <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "LPRDate") queryData = queryData.Where(x => (x.LPRDate >= DateTime.Parse(SepToken[1]) && x.LPRDate <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "dateOfPermanent") queryData = queryData.Where(x => (x.joiningDateGovtService >= DateTime.Parse(SepToken[1]) && x.joiningDateGovtService <= DateTime.Parse(SepToken[2])));

                    else if (SepToken[0] == "ServiceFromDate")
                    {
                        List<int> Ids = await _context.Assignments.Where(x => (x.StartDate >= DateTime.Parse(SepToken[1]) && x.StartDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ServiceToDate")
                    {
                        List<int> Ids = await _context.Assignments.Where(x => (x.EndDate >= DateTime.Parse(SepToken[1]) && x.EndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TrainingFromDate")
                    {
                        List<int> Ids = await _context.TraningLogs.Where(x => (x.fromDate >= DateTime.Parse(SepToken[1]) && x.fromDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "TrainingToDate")
                    {
                        List<int> Ids = await _context.TraningLogs.Where(x => (x.toDate >= DateTime.Parse(SepToken[1]) && x.toDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "dateOfPromotion")
                    {
                        List<int> Ids = await _context.PromotionLogs.Where(x => (x.date >= DateTime.Parse(SepToken[1]) && x.date <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "UNStart")
                    {
                        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose == "UN Mission").Where(x => (x.travelDate >= DateTime.Parse(SepToken[1]) && x.travelDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "UNEnd")
                    {
                        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose == "UN Mission").Where(x => (x.travelEndDate >= DateTime.Parse(SepToken[1]) && x.travelEndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ForignStart")
                    {
                        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose != "UN Mission").Where(x => (x.travelDate >= DateTime.Parse(SepToken[1]) && x.travelDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "ForignEnd")
                    {
                        List<int?> Ids = await _context.ForeignTravels.Where(x => x.travelPurpose != "UN Mission").Where(x => (x.travelEndDate >= DateTime.Parse(SepToken[1]) && x.travelEndDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "awardDate")
                    {
                        List<int> Ids = await _context.AwardEntries.Where(x => (x.awardDate >= DateTime.Parse(SepToken[1]) && x.awardDate <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                }
            }
            #endregion

            #region Result Process
            List<EmployeeInfo> data = await queryData.ToListAsync();
            List<EmployeeSearchReport> filteredData = new List<EmployeeSearchReport>();

            foreach (EmployeeInfo employeeInfo in data.OrderBy(x => x.rankId).ThenBy(x => x.bcsPosition))
            {

                var photographs = _context.Photographs.Where(x => x.employeeId == employeeInfo.Id && x.type == "profile").FirstOrDefault();
                //var loan = _context.loanInformations.Where(x => x.employeeId == employeeInfo.Id).FirstOrDefault();
                //var gPF = _context.gPFFundWithdraws.Where(x => x.employeeId == employeeInfo.Id).FirstOrDefault();

                filteredData.Add(new EmployeeSearchReport
                {
                    employeeId = employeeInfo.Id,
                    employeeCode = (employeeInfo.employeeCode == null) ? "" : employeeInfo.employeeCode,
                    BPNumberBn = (employeeInfo.employeeCode == null) ? "" : employeeInfo.employeeCode,
                    imageUrl = photographs?.url,
                    nameEnglish = (employeeInfo.nameEnglish == null) ? "" : employeeInfo.nameEnglish,
                    nameBangla = employeeInfo.nameBangla == null ? "" : employeeInfo.nameBangla,
                    unit = (employeeInfo.branch?.branchUnitName == null) ? "" : employeeInfo.branch?.branchUnitName,
                    rank = (employeeInfo.rank?.rankName == null) ? "" : employeeInfo.rank?.rankName,
                    bcsBatch = (employeeInfo.bCSBatch?.batchName == null) ? "" : employeeInfo.bCSBatch?.batchName,
                    gender = employeeInfo.gender == null ? "" : employeeInfo.gender,
                    religion = employeeInfo.religion?.name == null ? "" : employeeInfo.religion?.name,
                    meritePosition = employeeInfo.bcsPosition.ToString() == null ? "" : employeeInfo.bcsPosition.ToString(),
                    emailOffice = employeeInfo.emailAddress,
                    fatherName = employeeInfo.fatherNameEnglish,
                    motherName = employeeInfo.motherNameEnglish,
                    officeMobile = employeeInfo.mobileNumberOffice,
                    personalMobile = employeeInfo.mobileNumberPersonal,
                    emailPersona = employeeInfo.emailAddressPersonal,
                    nid = employeeInfo.nationalID,
                    dateOfbirth = employeeInfo.dateOfBirth?.ToString("dd/MMM/yyyy"),


                    joiningDateGovtService = employeeInfo.joiningDateGovtService?.ToString("dd/MMM/yyyy"),

                    homeDistrict = (_context.AddressInformation.Where(x => x.employeeInfoId == employeeInfo.Id && x.type == "Permanent Address").Select(x => x.district.districtNameBn).FirstOrDefault() == null) ? "" : _context.AddressInformation.Where(x => x.employeeInfoId == employeeInfo.Id && x.type == "Permanent Address").Select(x => x.district.districtNameBn).FirstOrDefault(),

                    joiningDesignation = (employeeInfo.joiningDesignation == null) ? "" : employeeInfo.joiningDesignation,

                    currentPositionJoiningDate = (_context.PromotionLogs.Where(x => x.employeeId == employeeInfo.Id).OrderBy(x => x.date).Select(x => x.date.ToString("dd/MMM/yyyy")).LastOrDefault() == null) ? "" : _context.PromotionLogs.Where(x => x.employeeId == employeeInfo.Id).OrderBy(x => x.date).Select(x => x.date.ToString("dd/MMM/yyyy")).LastOrDefault(),

                    seniorScalePromotionDate = (_context.PromotionLogs.Where(x => x.employeeId == employeeInfo.Id).OrderBy(x => x.date).Select(x => x.date.ToString("dd/MMM/yyyy")).LastOrDefault() == null) ? "" : _context.PromotionLogs.Where(x => x.employeeId == employeeInfo.Id).OrderBy(x => x.date).Select(x => x.date.ToString("dd/MMM/yyyy")).FirstOrDefault(),

                    currentRankNameBn = (employeeInfo?.rank?.rankNameBN == null) ? "" : employeeInfo?.rank?.rankNameBN,

                    birthPlace = (_context.AddressInformation.Where(x => x.employeeInfoId == employeeInfo.Id && x.type == "Permanent Address").Select(x => x.district.districtNameBn).FirstOrDefault() == null) ? "" : _context.AddressInformation.Where(x => x.employeeInfoId == employeeInfo.Id && x.type == "Permanent Address").Select(x => x.district.districtNameBn).FirstOrDefault(),

                    educationalQualifaction = (await _context.EducationalQualifications.Where(x => x.employeeId == employeeInfo.Id).OrderBy(x => x.passingYear).Select(x => x.degree.degreeNameBn).LastOrDefaultAsync() == null) ? "" : await _context.EducationalQualifications.Where(x => x.employeeId == employeeInfo.Id).OrderBy(x => x.passingYear).Select(x => x.degree.degreeNameBn).LastOrDefaultAsync()
                });
            }
            #endregion

            return filteredData;
        }

        public async Task<bool> SaveEmployeeTransectionHistoryLog(int empId, int statusId, string userId, string actionType, string remarks)
        {
            try
            {
                var roleList = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
                var userRole = _context.Roles.Where(x => roleList.Contains(x.Id));
                var userInfo = await _context.EmployeeInfos.Where(x => x.ApplicationUserId == userId).AsNoTracking().FirstOrDefaultAsync();
                var empInfo = await _context.EmployeeInfos.FindAsync(empId);
                if (actionType == "Portfolio")
                {
                    EmployeeTransectionLog log = new EmployeeTransectionLog();
                    if (statusId == 1)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.employeeCode;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = "Employee Successfully Registerd.";
                    }
                    else if (statusId == 2)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + " updated his portfolio.";
                    }
                    else if (statusId == 3)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + " finally submitted his portfolio.";
                    }
                    else if (statusId == 4)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio verified by " + userInfo.nameEnglish;
                    }
                    else if (statusId == 5)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio returned by " + userInfo.nameEnglish;
                    }
                    else if (statusId == 6)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio checked by " + userInfo.nameEnglish + ", " + remarks;
                    }
                    else if (statusId == 7)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio edit done by " + userInfo.nameEnglish;
                    }
                    else if (statusId == 8)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio verify by " + userInfo.nameEnglish;
                    }
                    else if (statusId == 11)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio partially edited by " + userInfo.nameEnglish + ", " + remarks;
                    }
                    else if (statusId == 12)
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio Undo by " + userInfo.nameEnglish + ", " + remarks;
                    }
                    else
                    {
                        log.employeeInfoId = empId;
                        log.statusInfoId = statusId;
                        log.empName = empInfo.nameEnglish;
                        log.ApplicationUserId = userId;
                        log.remarks = "Portfolio";
                        log.Status = empInfo.nameEnglish + "'s portfolio verify by " + userInfo.nameEnglish;
                    }

                    await _context.EmployeeTransectionLogs.AddAsync(log);
                    await _context.SaveChangesAsync();
                }
                else if (actionType == "Alpha")
                {
                    AssignmentStatusLog statusLog = new AssignmentStatusLog();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> SaveAlphaTransectionHistoryLog(int statusId, string userId, int actionId, string actionType, string remarks)
        {
            try
            {
                var roleList = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
                var userRole = _context.Roles.Where(x => roleList.Contains(x.Id));
                var userInfo = await _context.EmployeeInfos.Where(x => x.ApplicationUserId == userId).AsNoTracking().FirstOrDefaultAsync();
                if (actionType == "Alpha")
                {
                    if (statusId == 12)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.enlistedAssignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = userInfo.nameEnglish + " Create EnList For Posting";
                        }
                        else
                        {
                            statusLog.Status = userInfo.nameEnglish + " " + remarks + " EnList For Posting";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 13)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = userInfo.nameEnglish + " Create Posting List";
                        }
                        else
                        {
                            statusLog.Status = userInfo.nameEnglish + " " + remarks + " PostingList";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 14)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = userInfo.nameEnglish + " Submit Posting List For Approve";
                        }
                        else
                        {
                            statusLog.Status = userInfo.nameEnglish + " " + remarks + " Posting List For Approve";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 15)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = userInfo.nameEnglish + " Approve Posting List.";
                        }
                        else
                        {
                            statusLog.Status = userInfo.nameEnglish + " " + remarks + " Approve Posting.";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 16)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = "IGP Locked Posting List.";
                        }
                        else
                        {
                            statusLog.Status = "IGP " + remarks + " Posting.";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 17)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = "IGP Teturned Posting List.";
                        }
                        else
                        {
                            statusLog.Status = "IGP " + remarks + " Posting.";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 18)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = userInfo.nameEnglish + " Created Posting Letter.";
                        }
                        else
                        {
                            statusLog.Status = userInfo.nameEnglish + remarks + " Posting Letter.";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 19)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = userInfo.nameEnglish + "A Draft Letter Draft By Admin.";
                        }
                        else
                        {
                            statusLog.Status = userInfo.nameEnglish + remarks + " By Admin.";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else if (statusId == 20)
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.empName = userInfo.nameEnglish;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        if (remarks == null)
                        {
                            statusLog.Status = userInfo.nameEnglish + " Complete Officer's Posting By Admin.";
                        }
                        else
                        {
                            statusLog.Status = userInfo.nameEnglish + remarks + " By Admin.";
                        }

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        AssignmentStatusLog statusLog = new AssignmentStatusLog();
                        statusLog.employeeId = userInfo.Id;
                        statusLog.statusInfoId = statusId;
                        statusLog.applicationUserId = userId;
                        statusLog.assignmentId = actionId;
                        statusLog.remarks = "Alpha";
                        statusLog.Status = userInfo.nameEnglish + " Performed A Alpha Action";

                        await _context.AssignmentStatusLogs.AddAsync(statusLog);
                        await _context.SaveChangesAsync();
                    }
                }
                else if (actionType == "Portfolio")
                {

                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<AnulipiList> GetAnulipiFromListByName(string anulipi)
        {
            var data = await _context.AnulipiLists.Where(x => x.copyName == anulipi).FirstOrDefaultAsync(); ;
            return data;
        }

        public async Task<int> UpdateTypeEnlistedAssignment(int empId, string refNo)
        {
            var enlist = await _context.EnlistedAssignments.Where(x => x.employeeId == empId && x.enlistedAssignmentMaster.refNo == refNo).FirstOrDefaultAsync();
            enlist.typeId = 1;
            _context.EnlistedAssignments.Update(enlist);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<DisciplinaryAction>> GetDiciplinaryActionByEmpId(int empId)
        {
            return await _context.DisciplinaryActions.Include(x => x.employee).Where(x => x.employeeId == empId).ToListAsync();
        }
        public async Task<IEnumerable<EmployeeMadicalInfo>> GetMedicalInfosByEmpId(int empId)
        {
            return await _context.employeeMadicalInfos.Include(x => x.employeeInfo).Include(x => x.medicalSubCategory).Where(x => x.employeeInfoId == empId).ToListAsync();
        }

        public async Task<IEnumerable<GetEmployeeListWithTrainingSkill>> GetAllEmployeeList(int unitId, int rankId, int batchId, string userName)
        {
            return await _context.getEmployeeListWithTrainingSkills.FromSql($"SP_EmployeeInfoListForSkillEntry {unitId},{rankId},{batchId},{userName}").AsNoTracking().ToListAsync();
        }

        public async Task<string> GetUserRole(string userId)
        {
            var data = "";
            data = await (from user in _context.Users
                          join userRole in _context.UserRoles
                          on user.Id equals userRole.UserId
                          join role in _context.Roles
                          on userRole.RoleId equals role.Id
                          where user.Id == userId
                          select role.Name).FirstOrDefaultAsync();
            return data;
        }
        #region Employee Search By BCS Batch
        public async Task<IEnumerable<EmpolyeeDetailsByBCS>> GetEmployeeInfoByBCSBatch(int bcsBatchId)
        {
            var data = new List<EmpolyeeDetailsByBCS>();
            var employeeList = await _context.EmployeeInfos.Where(x => x.bCSBatchId == bcsBatchId && x.isDelete != 1).Include(x => x.section.specialBranchUnit.districts).Include(x => x.bCSBatch).Include(x => x.rank).OrderBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition).AsNoTracking().ToListAsync();
            foreach (var item in employeeList)
            {
                var parmanentAdd = await _context.AddressInformation.Where(x => x.employeeInfoId == item.Id && x.type == "Permanent Address").Include(x => x.thana).Include(x => x.unionWard).Include(x => x.district).Include(x => x.division).FirstOrDefaultAsync();
                var viewModel = new EmpolyeeDetailsByBCS
                {
                    id = item.Id,
                    bcsBatch = item.bCSBatch.batchName,
                    bcsPosition = item.bcsPosition,
                    name = item.nameBangla,
                    bp = item.employeeCode,
                    rank = item.rank.rankNameBN,
                    fatherName = item.fatherNameBangla,
                    motherName = item.motherNameBangla,
                    workingPlace = item?.section?.NameBN + ", " + item?.section?.specialBranchUnit?.districts?.districtNameBn,
                    address = parmanentAdd?.addressDetails + ", ওয়ার্ডঃ" + parmanentAdd?.unionWard?.unionNameBn + ", থানাঃ" + parmanentAdd?.thana?.thanaNameBn + ", জেলাঃ" + parmanentAdd?.district?.districtNameBn + ", বিভাগঃ" + parmanentAdd?.division?.divisionNameBn
                };
                data.Add(viewModel);
            }
            return data;
        }

        public async Task<IEnumerable<PMCorrectionDataModel>> GetCorrectionEmployeeInfoByBCSBatch(int batchId)
        {
            try
            {
                var result = await _context.pMCorrectionDataModels.FromSql($"SP_Get_PM_CorrectionData {batchId}").AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
