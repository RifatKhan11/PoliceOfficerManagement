using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Entity.Organogram;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;
using AlphaManagement.Domain.MasterDataServices.Interfaces;
using AlphaManagement.Domain.OgranogramService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.OgranogramService
{
   public class InternalPostingServices: IInternalPostingServices
    {
        private readonly AlphaDbContext _context;

        public InternalPostingServices(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveInternalEnlistMaster(InternalEnlistedAssignmentMaster model)
        {
            if (model.Id > 0)
            {
                _context.InternalEnlistedAssignmentMasters.Update(model);
            }
            else
            {
                await _context.InternalEnlistedAssignmentMasters.AddAsync(model);
            }
            var save = await _context.SaveChangesAsync();

            return model.Id;
        }

        public async Task<int> SaveInternalEnlist(InternalEnlistedAssignment model)
        {
            try
            {
                if (model.Id > 0)
                {
                    _context.InternalEnlistedAssignments.Update(model);
                }
                else
                {
                    await _context.InternalEnlistedAssignments.AddAsync(model);
                }
                var save = await _context.SaveChangesAsync();
                return model.Id;

                }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Department>> GetDepartmentWiseTotalEmployee()
        {
            try
            {
                var result =await (from d in _context.Departments
                              join ee in (from e in _context.EmployeeInfos.Where(x=>x.employeeTypeId==1 && x.branchId==1)
                                          group e by e.departmentId into de
                                          select new { departmentId = de.Key, totalEmployee = de.Count() }) on d.Id equals ee.departmentId into ed
                                          from edd in ed.DefaultIfEmpty()
                              select new Department
                              {
                                  Id = d.Id,
                                  isDelete = edd.totalEmployee,
                                  deptName = d.deptName,
                                  deptNameBn = d.deptNameBn,
                                  deptCode=d.deptCode
                              }).OrderByDescending(x=>x.isDelete).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetSectionRankWiseEmployeeList(int rankId, int unitId, int batchId, int bandId, int servicePeriodId, int isLocked, int isAttached, int isUnMission)
        {
            try
            {
                var result = new List<UnitRankWiseEmployeeViewModel>();
                result = await _context.unitRankWiseEmployeeViewModels.FromSql($"SP_GetInternalSectionWiseEmployeeList {rankId},{unitId},{batchId},{bandId},{servicePeriodId}").ToListAsync();
                if (isLocked > 0)
                {
                    result = result.Where(x => x.lockedEmpId > 0).ToList();
                }
                else if (isAttached > 0)
                {
                    result = result.Where(x => x.attachedUnit != "").ToList();
                }
                else if (isUnMission > 0)
                {
                    result = result.Where(x => x.pHQTRTypeId > 0).ToList();
                }
                else
                {

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetMutipleRankWiseSectionEmployeeList(string rankId, int unitId, int batchId, int bandId, int servicePeriodId, int isLocked, int isAttached, int isUnMission)
        {
            try
            {
                var result = new List<UnitRankWiseEmployeeViewModel>();
                result = await _context.unitRankWiseEmployeeViewModels.FromSql($"SP_GetInternalSectionWiseEmployeeList {rankId},{unitId},{batchId},{bandId},{servicePeriodId}").ToListAsync();
                if (isLocked > 0)
                {
                    result = result.Where(x => x.lockedEmpId > 0).ToList();
                }
                else if (isAttached > 0)
                {
                    result = result.Where(x => x.attachedUnit != "").ToList();
                }
                else if (isUnMission > 0)
                {
                    result = result.Where(x => x.pHQTRTypeId > 0).ToList();
                }
                else
                {

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<InternalUnitRankWiseEmployeeViewModel>> GetInternalMutipleRankWiseSectionEmployeeList(string rankId, int unitId, int batchId, int bandId, int servicePeriodId, int isLocked, int isAttached, int isUnMission)
        {
            try
            {
                var result = new List<InternalUnitRankWiseEmployeeViewModel>();
                result = await _context.internalUnitRankWiseEmployeeViewModels.FromSql($"SP_GetInternalSectionWiseEmployeeList {rankId},{unitId},{batchId},{bandId},{servicePeriodId}").ToListAsync();
                if (isLocked > 0)
                {
                    result = result.Where(x => x.lockedEmpId > 0).ToList();
                }
                else if (isAttached > 0)
                {
                    result = result.Where(x => x.attachedUnit != "").ToList();
                }
                else if (isUnMission > 0)
                {
                    result = result.Where(x => x.pHQTRTypeId > 0).ToList();
                }
                else
                {

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<InternalEnlistedAssignment>> GetEnlistDetailsByMasterId(int id)
        {
            var data=await _context.InternalEnlistedAssignments.Include(x=>x.employee).Include(x=>x.rank)
                .Include(x=>x.enlistedAssignmentMaster).Include(x=>x.section)
                .Where(x => x.enlistedAssignmentMasterId == id)
                .Select(x=>new InternalEnlistedAssignment {
                    Id=x.Id,
                    employeeId=x.employeeId,
                    employee=x.employee,
                    sectionId=x.sectionId,
                    section=x.section,
                    rankId=x.rankId,
                    rank=x.rank,
                    enlistedAssignmentMasterId=x.enlistedAssignmentMasterId,
                    enlistedAssignmentMaster=x.enlistedAssignmentMaster,
                    statusId=x.statusId,
                    typeId=x.typeId,
                    remarks=x.remarks,
                    createdAt=x.createdAt,
                    createdBy=x.createdBy,
                    updatedBy=_context.Photographs.Where(y=>y.employeeId==x.employeeId && y.type== "profile").Select(y=>y.url).FirstOrDefault()
                }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<InternalPostingReportVM>> PendingEnlistMasters(string userId)
        {
            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters.Where(x => x.statusId == 1)
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.ApplicationUserId == (userId == "" ? asignM.ApplicationUserId : userId)
                                select new InternalPostingReportVM
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate =Convert.ToDateTime(asignM.refDate),
                                    description = asignM.remarks,
                                    nameEnglish = emp.nameEnglish,
                                    statusId = asignM.statusId,
                                }).ToListAsync();
            return result;

        }

        public async Task<IEnumerable<InternalPostingReportVM>> AssignmentReturnList(string userId)
        {
            var Ids = await _context.InternalApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 3).Select(x => x.masterId).ToListAsync();

            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where (Ids.Contains(asignM.Id))
                                select new InternalPostingReportVM
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
        

        public async Task<int?> AssignmentReturnListCount(string userId)
        {
            var result = await _context.InternalApprovalLogs.Where(x => x.nextApprovarId == userId && x.isActive == 3).Select(x => x.masterId).CountAsync();
            return result;
        }

        public async Task<int?> AssignmentInternalReturnAndLockCount(int status)
        {
            var result = await _context.InternalEnlistedAssignmentMasters.Where(x => x.statusId == status).CountAsync();
            return result;
        }

        public async Task<IEnumerable<PostingReportView>> PendingEnlistMastersForIgp(string userId)
        {
            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters.Where(x => x.statusId == 3)
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                //where asignM.ApplicationUserId == (userId == "" ? asignM.ApplicationUserId : userId)
                                select new PostingReportView
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

        public async Task<int?> PendingEnlistMastersForIgpCount()
        {
            return await _context.InternalEnlistedAssignmentMasters.Where(x => x.statusId == 3).CountAsync();
        }

        public async Task<IEnumerable<InternalPostingReportVM>> InternalAssignmentMastersList(string userId)
        {
            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters.Where(x => x.statusId == 3)
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                //where asignM.ApplicationUserId == (userId == "" ? asignM.ApplicationUserId : userId)
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

        public async Task<IEnumerable<InternalPostingReportVM>> ApprovedInternalAssignmentMastersList(string userId)
        {
            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.ApplicationUserId == (userId == "" ? asignM.ApplicationUserId : userId)
                                && asignM.statusId==4
                                select new InternalPostingReportVM
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = Convert.ToDateTime(asignM.refDate),
                                    //description = asignM.description,
                                    nameEnglish = emp.nameEnglish,
                                    statusId = asignM.statusId,
                                }).ToListAsync();
            return result;

        }

        public async Task<IEnumerable<InternalPostingReportVM>> ApprovedInternalAssignmentMastersListForIgp(int status)
        {
            var result = await (from asignM in _context.InternalEnlistedAssignmentMasters
                                join emp in _context.EmployeeInfos on asignM.ApplicationUserId equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where asignM.statusId == status
                                select new InternalPostingReportVM
                                {
                                    url = p.url,
                                    assignMasterId = asignM.Id,
                                    refNo = asignM.refNo,
                                    refDate = Convert.ToDateTime(asignM.refDate),
                                    nameEnglish = emp.nameEnglish,
                                    statusId = asignM.statusId,
                                }).ToListAsync();
            return result;

        }

        public async Task<int?> ApprovedInternalAssignmentMastersListCount(string userId)
        {
            var result = await _context.InternalAssignmentMasters.Where(x=>x.applicationUserId == userId).AsNoTracking().CountAsync();
            return result;
        }

        public async Task<InternalAssignmentMaster> InternalAssignmentMasterEnlistId(int id)
        {
            var result = await _context.InternalAssignmentMasters.Where(x=>x.internalEnlistedAssignmentMasterId == id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<InternalAssignmentMaster> InternalAssignmentMaster(string refNo)
        {
            var result = await _context.InternalAssignmentMasters.Where(x => x.refNo == refNo).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<InternalEnlistedAssignment>> GetAssignmentsPriviousLockByMasterId(int id)
        {
            var Ids = await _context.InternalEnlistedAssignments
                .Where(x => x.enlistedAssignmentMasterId == id)
                .Select(x => x.employeeId)
                .ToListAsync();

            var result = await _context.InternalEnlistedAssignments
                .Where(x => x.statusId == 2)
                .Where(x => Ids.Contains(x.employeeId))
                .Include(x => x.enlistedAssignmentMaster)
                .Include(x => x.employee.section.specialBranchUnit.districts)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.bCSBatch)
                .Include(x => x.employee.branch)
                .Include(x => x.employee.Photographs)
                .Include(x => x.section)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<InternalEnlistedAssignment>> GetInternalEnlistedAssignmentsByMasterId(int id)
        {
            var result = await _context.InternalEnlistedAssignments
                .Where(x => x.enlistedAssignmentMasterId == id)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<InternalAssignment>> GetActiveInternalAssignments()
        {
            var result = await _context.InternalAssignments
                .Where(x => x.assignmentTypeId == 2 && x.statusId!=100)
                .Include(x=>x.employee.rank)
                .Include(x=>x.employee.bCSBatch)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<InternalAssignmentDetailsModal>> AssignmentDetailsModalByMasterId(int assignid)
        {
            try
            {
                var result = await _context.InternalEnlistedAssignments
                    .Include(x => x.enlistedAssignmentMaster)
                    .Include(x => x.employee.rank)
                    .Include(x => x.employee.bCSBatch)
                    .Include(x => x.employee.section)
                    .Include(x => x.employee.branch)
                    .Include(x => x.employee)
                    .Include(x => x.employee.Photographs)
                    .Include(x => x.employee.section.specialBranchUnit.districts)
                    .Include(x => x.section)
                    .Include(x => x.employee.department)
                    .Where(x => x.enlistedAssignmentMasterId == assignid).ToListAsync();
                List<InternalAssignmentDetailsModal> data = new List<InternalAssignmentDetailsModal>();
                foreach (var item in result)
                {
                    data.Add(new InternalAssignmentDetailsModal
                    {
                        masterId = item.enlistedAssignmentMasterId,
                        Id = item.Id,
                        employeeId = item.employeeId,
                        rankId = item?.employee?.rankId,
                        creator = item?.enlistedAssignmentMaster?.ApplicationUserId,
                        statusId = item.enlistedAssignmentMaster?.statusId,
                        reqNo = item?.enlistedAssignmentMaster?.refNo,
                        rankName = item?.employee?.branch?.branchUnitName,
                        date = item.enlistedAssignmentMaster.refDate,
                        picture = item.employee?.Photographs.Where(x => x.type == "profile").Select(x => x.url).FirstOrDefault(),
                        Bp = item.employee?.employeeCode,
                        Name = item.employee?.nameEnglish,
                        deptName = item.employee?.department?.deptName,
                        educationalQualification = _context.EducationalQualifications.Where(x => x.employeeId == item.employeeId).Where(x => x.degreeId != 151).OrderByDescending(x => x.passingYear).Select(x => x.degree.degreeName).FirstOrDefault(),
                        homeDistrict = _context.AddressInformation.Where(x => x.employeeInfoId == item.employeeId).Where(x => x.type == "Permanent Address").Select(x => x.district.districtName).FirstOrDefault(),
                        batch = item?.employee?.bCSBatch?.batchName,
                        currentPostringPlace = item?.employee?.section?.Name + ", " + item.employee?.section?.specialBranchUnit?.districts?.districtName,
                        NewPostringPlace = item.section?.deptName,
                        LastPromotionDate = _context.PromotionLogs.Where(x => x.employeeId == item.employeeId).OrderByDescending(x => x.date).Select(x => x.date).FirstOrDefault(),
                        dateOfBirth = item.employee?.dateOfBirth,
                        joiningDateOfPresentunit = item.employee?.joiningDatePresentWorkstation,
                        remarks = item.remarks,
                        type = item.enlistedAssignmentMaster.assignmentTypeId,
                        rank = item.employee.rank.rankName
                    });
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        public async Task<int> UpdateAssignmentInfoForIGP(int id)
        {
            var assign = await _context.InternalEnlistedAssignments.Where(x => x.enlistedAssignmentMasterId == id).ToListAsync();
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


        public async Task<EmployeeInfo> GetAssignmentMasterEmployeeInfoById(int id)
        {
            var result = await (from a in _context.InternalEnlistedAssignmentMasters
                                join u in _context.Users on a.ApplicationUserId equals u.Id
                                join e in _context.EmployeeInfos on u.Id equals e.ApplicationUserId
                                where a.Id == id
                                select e).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<InternalPostingReportVM>> GetApprovalLogFullByMasterId(int masterId)
        {
            var result = await (from AP in _context.InternalApprovalLogs
                                join asignM in _context.Users on AP.userId equals asignM.Id
                                join emp in _context.EmployeeInfos on asignM.Id equals emp.ApplicationUserId
                                join ph in _context.Photographs.Where(x => x.type == "profile") on emp.Id equals ph.employeeId into ps
                                from p in ps.DefaultIfEmpty()
                                where AP.masterId == masterId
                                select new InternalPostingReportVM
                                {
                                    statusId = AP.isActive,
                                    url = p.url,
                                    assignMasterId = AP.Id,
                                    refDate = Convert.ToDateTime(AP.createdAt),
                                    description = AP.notes,
                                    nameEnglish = emp.nameEnglish,
                                    applicationUserId = AP.nextApprovarId,
                                    createApplicationUserId = AP.userId,
                                }).ToListAsync();

            return result;

        }

        public async Task<int?> InternalMinimumRankStatus(int masterId)
        {
            var result = await _context.InternalEnlistedAssignments.Where(x=>x.enlistedAssignmentMasterId==masterId).MinAsync(x=>x.employee.rank.shortOrder);

            return result;
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


        public async Task<InternalEnlistedAssignment> GetAssignmentById(int id)
        {
            var result = await _context.InternalEnlistedAssignments.Where(x => x.Id == id).Include(x => x.employee.rank).Include(x => x.enlistedAssignmentMaster).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public void UpdateApprovalLogBymasterId(int masterId)
        {
            var user = _context.InternalApprovalLogs.Where(x => x.masterId == masterId).ToList();
            foreach (var data in user)
            {
                data.isActive = 0;
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeListBySectionId(int id)
        {
            var data = await _context.EmployeeInfos.Include(x => x.rank).Include(x => x.department)
                .Where(x => x.departmentId == id && x.employeeTypeId==1).Select(x => new EmployeeInfo
                {
                    Id = x.Id,
                    nameEnglish = x.nameEnglish,
                    nameBangla = x.nameBangla,
                    rank = x.rank,
                    department = x.department,
                    employeeCode=x.employeeCode,
                    updatedBy = _context.Photographs.Where(y => y.employeeId == x.Id && y.type== "profile").Select(y => y.url).FirstOrDefault()
                }).ToListAsync();
            return data;
        }

        public async Task<int> SaveInternalAssignmentMaster(InternalAssignmentMaster medicalInfo)
        {
            try
            {
                if (medicalInfo.Id != 0)
                {
                    _context.InternalAssignmentMasters.Update(medicalInfo);
                }
                else
                {
                    _context.InternalAssignmentMasters.Add(medicalInfo);
                }

                await _context.SaveChangesAsync();
                return medicalInfo.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeInfo> GetEmployeeInfoByBpNo(string bp)
        {
            return await _context.EmployeeInfos
                .Include(x=>x.branch)
                .Include(x=>x.rank)
                .Include(x=>x.section)
                .Include(x=>x.department)
                .Include(x=>x.religion)
                .Where(x => x.employeeCode == bp).FirstOrDefaultAsync();
        }

        public async Task<Spouse> GetSpouseByEmpId(int id)
        {
            return await _context.Spouses
                .Where(x => x.spouseRelationId==5 && x.employeeId== id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AssignmentVM>> InternalAssignmentPostedByMasterId(int assignid)
        {
            var query = await (from x in _context.InternalAssignments
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

        public async Task<IEnumerable<InternalAssignment>> InternalAssignmentAll()
        {
            var result = await _context.InternalAssignments
                 .Include(x => x.section.specialBranchUnit.districts)
                 .Include(x => x.department)
                 .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<InternalAssignment>> InternalAssignmentDetailsByMasterId(int id)
        {
            var enlist = await _context.InternalEnlistedAssignmentMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _context.InternalAssignments.Include(x => x.assignmentMaster)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.branch)
                .Include(x => x.assignmentMaster)
                .Include(x => x.department)
                .Include(x => x.section.specialBranchUnit.districts)
                .Where(x => x.assignmentMaster.refNo == enlist.refNo)
                .Select(x=>new InternalAssignment {
                    assignmentMasterId=x.assignmentMasterId,
                    assignmentMaster=x.assignmentMaster,
                    employeeId=x.employeeId,
                    employee=x.employee,
                    assignmentTypeId=x.assignmentTypeId,
                    assignmentType=x.assignmentType,
                    EntryNo=x.EntryNo,
                    StartDate=x.StartDate,
                    EndDate=x.EndDate,
                    rankId=x.rankId,
                    rank=x.rank,
                    departmentId=x.departmentId,
                    department=x.department,
                    supervisorId=x.supervisorId,
                    supervisor=x.supervisor,
                    reasonOfTransfer = x.reasonOfTransfer,
                    ministryRefNo = x.ministryRefNo,
                    receiveRefNo = x.receiveRefNo,
                    sectionId = x.sectionId,
                    section = x.section,
                    sectionName = x.sectionName,
                    servicePeriod = x.servicePeriod,
                    Remarks = x.Remarks,
                    designationName = x.designationName,
                    unitName = x.unitName,
                    statusId = x.statusId,
                    isAdminEntry = x.isAdminEntry,
                    updatedBy= _context.InternalEnlistedAssignments.Include(a => a.employee.department).Where(b=>b.enlistedAssignmentMaster.refNo==enlist.refNo).Select(a=>a.employee.department.deptNameBn).FirstOrDefault(),
                    createdBy= _context.InternalEnlistedAssignments.Include(a => a.employee.branch).Where(b=>b.enlistedAssignmentMaster.refNo==enlist.refNo).Select(a=>a.employee.branch.branchUnitNameBN).FirstOrDefault()
                })
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<InternalAssignment>> InternalAssignmentsDetailsByMasterId(int id)
        {
            var master = await _context.InternalAssignmentMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _context.InternalAssignments.Include(x => x.assignmentMaster)
                .Include(x => x.employee.rank)
                .Include(x => x.employee.branch)
                .Include(x => x.assignmentMaster)
                .Include(x => x.department)
                .Include(x => x.section.specialBranchUnit.districts)
                .Where(x => x.assignmentMaster.Id == id)
                .Select(x => new InternalAssignment
                {
                    assignmentMasterId = x.assignmentMasterId,
                    assignmentMaster = x.assignmentMaster,
                    employeeId = x.employeeId,
                    employee = x.employee,
                    assignmentTypeId = x.assignmentTypeId,
                    assignmentType = x.assignmentType,
                    EntryNo = x.EntryNo,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    rankId = x.rankId,
                    rank = x.rank,
                    departmentId = x.departmentId,
                    department = x.department,
                    supervisorId = x.supervisorId,
                    supervisor = x.supervisor,
                    reasonOfTransfer = x.reasonOfTransfer,
                    ministryRefNo = x.ministryRefNo,
                    receiveRefNo = x.receiveRefNo,
                    sectionId = x.sectionId,
                    section = x.section,
                    sectionName = x.sectionName,
                    servicePeriod = x.servicePeriod,
                    Remarks = x.Remarks,
                    designationName = x.designationName,
                    unitName = x.unitName,
                    statusId = x.statusId,
                    isAdminEntry = x.isAdminEntry,
                    updatedBy = _context.InternalEnlistedAssignments.Include(a => a.employee.department).Where(b => b.enlistedAssignmentMasterId == master.internalEnlistedAssignmentMasterId).Select(a => a.employee.department.deptNameBn).FirstOrDefault(),
                    createdBy = _context.InternalEnlistedAssignments.Include(a => a.employee.branch).Where(b => b.enlistedAssignmentMasterId == master.internalEnlistedAssignmentMasterId).Select(a => a.employee.branch.branchUnitNameBN).FirstOrDefault()
                })
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<InternalAssignment>> InternamAssignments(int masterId)
        {
            return await _context.InternalAssignments.Where(x => x.assignmentMasterId == masterId).ToListAsync();
        }

        public async Task<int> SaveInternamAssignments(InternalAssignment model)
        {
            if (model.Id > 0)
            {
                _context.InternalAssignments.Update(model);
            }
            else
            {
                await _context.InternalAssignments.AddAsync(model);
            }            
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostingReportView>> ApprovedInternalAssignmentMasters(string userId)
        {
            var result = await (from asignM in _context.InternalAssignmentMasters.Where(x => x.statusId == 18)
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

        public async Task<IEnumerable<InternalAssignmentAnulipi>> AssignmentMastersRopo(int id)
        {
            var result = await _context.InternalAssignmentAnulipis
                .Include(x => x.assignmentMaster)
                .Include(x => x.assignmentMaster.internalEnlistedAssignmentMaster)
                .Include(x => x.anulipiList)
                .Where(x => x.assignmentMasterId == id)
                .OrderBy(x => x.shortOrder)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AssignmentAnulipiPreview>> AssignmentMastersRoportPre(int id)
        {
            var result = await _context.AssignmentAnulipiPreviews
                .Include(x => x.assignmentMaster)
                .Include(x => x.internalAssignMaster)
                .Include(x => x.anulipiList)
                .Where(x => x.internalAssignMasterId == id)
                .OrderBy(x => x.shortOrder)
                .ToListAsync();
            return result;
        }

        public void DeleteAssignmentsAunilipiPreviewByMasterId(int id)
        {
            try
            {
                _context.AssignmentAnulipiPreviews.RemoveRange(_context.AssignmentAnulipiPreviews.Where(x => x.internalAssignMasterId == id));
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        #region PHQ DashBoard
        public async Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoList()
        {
            return await _context.EmployeeInfos
          .Include(x => x.rank)
          .Include(x => x.branch)
          .Include(x => x.section)
          .Include(x => x.bCSBatch)
          .Include(x => x.department)
          .Where(x => Convert.ToDateTime(x.joiningDatePresentWorkstation).Date < DateTime.Now.AddYears(-2))
          .Where(x => x.joiningDatePresentWorkstation != null)
          .Where(x => x.branchId == 1)
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
          .Where(x => x.departmentId == (unit == 0 ? x.departmentId : unit) && x.rankId == (rank == 0 ? x.rankId : rank) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch))
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
          .Include(x => x.department)
          .Where(x => Convert.ToDateTime(x.joiningDatePresentWorkstation).Date < DateTime.Now.AddYears(-2))
          .Where(x => x.departmentId == (unit == 0 ? x.departmentId : unit) && x.rankId == (rank == 0 ? x.rankId : rank) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch))
          .Where(x => x.joiningDatePresentWorkstation != null)
          .OrderBy(x => x.joiningDatePresentWorkstation)
          .ToListAsync();
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
                                    nameEnglish = emp.nameEnglish
                                }).ToListAsync();
            return result;

        }
        #endregion
    }
}
