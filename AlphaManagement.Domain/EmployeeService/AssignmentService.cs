using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.Domain.EmployeeService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmployeeService
{
    public class AssignmentService : IAssignmentService
    {
        private readonly AlphaDbContext _context;

        public AssignmentService(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EducationalQualification>> GetDegreeByUser()
        {
            var result = await _context.EducationalQualifications
                .Include(x => x.employee)
                .Include(x => x.degree)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<SpecialBranchUnit>> GetUnitWiseEmployeeInfo()
        {
            var result = await _context.SpecialBranchUnits.Include(x => x.EmployeeInfos).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Rank>> GetRankWiseEmployeeInfo()
        {
            //var result = await _context.Ranks.Include(x => x.EmployeeInfos).ToListAsync();
            var result = await _context.Ranks.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Rank>> GetRankGreaterASP()
        {
            var result = await _context.Ranks.Where(x => x.Id > 1 && x.Id < 13).ToListAsync();
            return result;
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            var result = await _context.Assignments.Where(x => x.Id == id).Include(x => x.employee.rank).Include(x => x.specialBranchUnit).Include(x => x.assignmentMaster).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EnlistedAssignment>> GetNewEnlistedByRefNo(string refNo)
        {
            var result = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMaster.refNo == refNo && x.statusId==1 && x.typeId==2).Include(x => x.employee.rank).Include(x => x.specialBranchUnit).Include(x=>x.enlistedAssignmentMaster).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentDetailsByMasterId(int id)
        {
            var result = await _context.Assignments.Where(x => x.assignmentMasterId == id).Include(x => x.employee.rank).Include(x => x.specialBranchUnit).Include(x => x.assignmentMaster).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Assignment> GetAssignmentByIdSingle(int id)
        {
            var result = await _context.Assignments.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentWithDuejoining()
        {
            var result = await _context.Assignments
                .Include(x=>x.assignmentMaster)
                .Include(x=>x.employee.section.specialBranchUnit.districts)
                .Include(x=>x.section.specialBranchUnit.districts)
                .Include(x=>x.rank)
                .Include(x=>x.employee.bCSBatch)
                .Where(x=>x.statusId==2)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<InternalEnlistedAssignment> GetInternalEnlistedAssignmentIdSingle(int id)
        {
            var result = await _context.InternalEnlistedAssignments.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeInfo> GetAssignmentMasterEmployeeInfoById(int id)
        {
            var result =await (from a in _context.AssignmentMasters
                         join u in _context.Users on a.applicationUserId equals u.Id
                         join e in _context.EmployeeInfos on u.Id equals e.ApplicationUserId
                         where a.Id==id
                         select e).FirstOrDefaultAsync();

            return result;
        }

        public async Task<AssignmentMaster> GetAssignmentMasterById(int id)
        {
            var result = await _context.AssignmentMasters.Where(x=>x.Id == id).AsNoTracking().FirstOrDefaultAsync();

            return result;
        }

        public async Task<EnlistedAssignmentMaster> GetEnlistedMasterById(int id)
        {
            var result = await _context.EnlistedAssignmentMasters.Where(x=>x.Id == id).FirstOrDefaultAsync();

            return result;
        }

        public async Task<EnlistedAssignmentMaster> GetEnlistedMasterByRefNo(string refNo)
        {
            var result = await _context.EnlistedAssignmentMasters.Where(x => x.refNo == refNo).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<EnlistedAssignment>> GetEnlistedAssignment(string referenceNumber)
        {
            var result = await _context.EnlistedAssignments.Include(x => x.enlistedAssignmentMaster).Where(x => x.enlistedAssignmentMaster.refNo == referenceNumber && x.statusId == 1).Include(x => x.employee.rank).Include(x => x.specialBranchUnit).Include(x => x.employee.designations).Include(x => x.employee.section).Include(x => x.employee.section.specialBranchUnit.districts).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EnlistedAssignment>> GetEnlistedAssignmentOngoing(string referenceNumber)
        {
            var result = await _context.EnlistedAssignments.Include(x => x.enlistedAssignmentMaster).Where(x => x.enlistedAssignmentMaster.refNo == referenceNumber).Include(x => x.employee.rank).Include(x => x.specialBranchUnit).Include(x => x.employee.designations).Include(x => x.employee.section).Include(x => x.employee.section.specialBranchUnit.districts).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<EnlistedAssignment>> GetReturnedEnlistedAssignment(string referenceNumber)
        {
            var result = await _context.EnlistedAssignments.Include(x => x.enlistedAssignmentMaster).Where(x => x.enlistedAssignmentMaster.refNo == referenceNumber).Include(x => x.employee.rank).Include(x => x.specialBranchUnit).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BranchUnitVM>> GetUnitWiseAllEmployeeInfo()
        {
            var result = await (from sb in _context.SpecialBranchUnits.Include(x => x.specialBranchUnit)
                                select new BranchUnitVM
                                {
                                    Id = sb.Id,
                                    branchCode = sb.branchCode,
                                    branchUnitName = sb.branchUnitName,
                                    branchUnitNameBN = sb.branchUnitNameBN,
                                    isParent = sb.isParent,
                                    shortOrder = sb.shortOrder,
                                    isdefault = sb.isdefault,
                                    specialBranchUnit = sb.specialBranchUnit,
                                    EmployeeInfos = _context.EmployeeInfos.Where(x => x.branchId == sb.Id),
                                    totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost),
                                    totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id).Count()
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetUnitWiseAllEmployeeInfos()
        {
            var result = await (from sb in _context.SpecialBranchUnits.Include(x => x.specialBranchUnit)
                                select new SpecialBranchUnit
                                {
                                    Id = sb.Id,
                                    branchCode = sb.branchCode,
                                    branchUnitName = sb.branchUnitName,
                                    branchUnitNameBN = sb.branchUnitNameBN,
                                    isParent = sb.isParent,
                                    shortOrder = sb.shortOrder,
                                    isdefault = sb.isdefault,
                                    specialBranchUnit = sb.specialBranchUnit,
                                    EmployeeInfos = _context.EmployeeInfos.Where(x => x.branchId == sb.Id).ToList(),
                                    totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost),
                                    totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id).Count()
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetPositionVacency(int rankId, int branchId)
        {
            var branch = await _context.SpecialBranchUnits.Where(x => x.Id == branchId).FirstOrDefaultAsync();
            var count = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == branchId).CountAsync();
            var result = new List<SpecialBranchUnit>();
            if (branch == null)
            {
                result = await (from sb in _context.SpecialBranchUnits.Include(x => x.specialBranchUnit)
                                join P in _context.PostInUnits on sb.Id equals P.specialBranchUnitId
                                where sb.specialBranchUnitId == (branchId == 0 ? sb.specialBranchUnitId : branchId)
                                && P.rankId == (rankId == 0 ? P.rankId : rankId)
                                select new SpecialBranchUnit
                                {
                                    Id = sb.Id,
                                    branchCode = _context.Ranks.Where(x => x.Id == P.rankId).Select(x => x.rankName).FirstOrDefault(),
                                    branchUnitName = sb.branchUnitName,
                                    branchUnitNameBN = sb.branchUnitNameBN,
                                    isParent = _context.Ranks.Where(x => x.Id == P.rankId).Select(x => x.Id).FirstOrDefault(),
                                    shortOrder = sb.shortOrder,
                                    isdefault = _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == P.rankId).Count(),
                                    specialBranchUnit = sb.specialBranchUnit,
                                    totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == P.rankId).Sum(x => x.numOfPost),
                                    totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == P.rankId).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == P.rankId).Count()
                                }).ToListAsync();
            }
            else if (count > 1)
            {
                result = await (from sb in _context.SpecialBranchUnits.Include(x => x.specialBranchUnit)
                                join P in _context.PostInUnits on sb.Id equals P.specialBranchUnitId
                                where sb.specialBranchUnitId == (branchId == 0 ? sb.specialBranchUnitId : branchId)
                                && P.rankId == (rankId == 0 ? P.rankId : rankId)
                                select new SpecialBranchUnit
                                {
                                    Id = sb.Id,
                                    branchCode = _context.Ranks.Where(x => x.Id == P.rankId).Select(x => x.rankName).FirstOrDefault(),
                                    branchUnitName = sb.branchUnitName,
                                    branchUnitNameBN = sb.branchUnitNameBN,
                                    isParent = _context.Ranks.Where(x => x.Id == P.rankId).Select(x => x.Id).FirstOrDefault(),
                                    shortOrder = sb.shortOrder,
                                    isdefault = _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == P.rankId).Count(),
                                    specialBranchUnit = sb.specialBranchUnit,
                                    totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == P.rankId).Sum(x => x.numOfPost),
                                    totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == P.rankId).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == P.rankId).Count()
                                }).ToListAsync();
            }
            else
            {
                result = await (from sb in _context.SpecialBranchUnits.Include(x => x.specialBranchUnit)
                                join P in _context.PostInUnits on sb.Id equals P.specialBranchUnitId
                                where sb.Id == (branchId == 0 ? sb.Id : branchId)
                                && P.rankId == (rankId == 0 ? P.rankId : rankId)
                                select new SpecialBranchUnit
                                {
                                    Id = sb.Id,
                                    branchCode = _context.Ranks.Where(x => x.Id == P.rankId).Select(x => x.rankName).FirstOrDefault(),
                                    branchUnitName = sb.branchUnitName,
                                    branchUnitNameBN = sb.branchUnitNameBN,
                                    isParent = _context.Ranks.Where(x => x.Id == P.rankId).Select(x => x.Id).FirstOrDefault(),
                                    shortOrder = sb.shortOrder,
                                    isdefault = _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == P.rankId).Count(),
                                    specialBranchUnit = sb.specialBranchUnit,
                                    totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == P.rankId).Sum(x => x.numOfPost),
                                    totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == P.rankId).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == P.rankId).Count()
                                }).ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> SuggestedSpecialBranchUnit(int empId)
        {
            List<int?> redList = new List<int?>();
            redList.AddRange(await _context.Assignments.Where(x => x.employeeId == empId).Select(x => x.specialBranchUnitId).ToListAsync());

            return await _context.SpecialBranchUnits.Where(x => redList.Contains(x.Id)).AsNoTracking().Take(5).ToListAsync();
        }


        public async Task<List<BranchUnitWiseEmployeesModel>> GetbranchUnitWiseEmployeeInfos()
        {

            List<BranchUnitWiseEmployeesModel> datas = new List<BranchUnitWiseEmployeesModel>();
            var parentBranchUnits = await _context.SpecialBranchUnits.Where(x => x.isParent == 1).ToListAsync();
            foreach (var sb in parentBranchUnits.OrderBy(x => x.Id))
            {
                var unitEmployees = _context.EmployeeInfos.Where(x => x.branchId == sb.Id).ToList();
                var unitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost);
                var unitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id).Count();

                var subUnitList = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == sb.Id).Include(x => x.specialBranchUnit).ToListAsync();
                bool hasSubUnit = false;


                if (subUnitList.Count() > 0)
                {
                    hasSubUnit = true;
                    bool isParent = true;
                    foreach (var subUnit in subUnitList)
                    {
                        BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                        {
                            unitEmployees = unitEmployees,
                            unitTotalPost = unitTotalPost,
                            unitTotalBlankPost = unitTotalBlankPost,
                            Id = sb.Id,
                            parentSpecialBranchUnit = sb,

                            subUnitId = subUnit.Id,
                            isParent = (isParent) ? 1 : 0,
                            hasSubUnit = false,
                            specialBranchUnit = subUnit,
                            subUnitEmployees = _context.EmployeeInfos.Where(x => x.branchId == subUnit.Id).ToList(),
                            subUnitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id).Sum(x => x.numOfPost),
                            subUnitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id).Count()
                        };
                        isParent = false;
                        datas.Add(data);

                    };

                }
                else
                {
                    BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                    {

                        Id = sb.Id,
                        parentSpecialBranchUnit = sb,
                        isParent = sb.isParent,
                        hasSubUnit = hasSubUnit,
                        unitEmployees = _context.EmployeeInfos.Where(x => x.branchId == sb.Id).ToList(),
                        unitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost),
                        unitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id).Count()
                    };
                    datas.Add(data);
                }
            }
            return datas;

            //var result = await (from sb in _context.SpecialBranchUnits.Include(x => x.specialBranchUnit)
            //                    select new SpecialBranchUnit
            //                    {
            //                        Id = sb.Id,
            //                        branchCode = sb.branchCode,
            //                        branchUnitName = sb.branchUnitName,
            //                        isParent = sb.isParent,
            //                        shortOrder = sb.shortOrder,
            //                        isdefault = sb.isdefault,
            //                        specialBranchUnit = sb.specialBranchUnit,
            //                        EmployeeInfos = _context.EmployeeInfos.Where(x => x.branchId == sb.Id).ToList(),
            //                        totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost),
            //                        totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id).Count()
            //                    }).ToListAsync();
            //return result;
        }

        public async Task<List<BranchUnitWiseEmployeesModel>> GetbranchUnitEmployeeInfosByRank(int rankId)
        {

            List<BranchUnitWiseEmployeesModel> datas = new List<BranchUnitWiseEmployeesModel>();
            var parentBranchUnits = await _context.SpecialBranchUnits.Where(x => x.isParent == 1).ToListAsync();
            foreach (var sb in parentBranchUnits.OrderBy(x => x.Id))
            {
                var unitEmployees = _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.rankId == rankId).OrderBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition).ToList();
                var unitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == rankId).Sum(x => x.numOfPost);
                var unitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == rankId).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.rankId == rankId).Count();

                var subUnitList = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == sb.Id).Include(x => x.specialBranchUnit).ToListAsync();
                bool hasSubUnit = false;


                if (subUnitList.Count() > 0)
                {
                    hasSubUnit = true;
                    bool isParent = true;
                    foreach (var subUnit in subUnitList)
                    {
                        BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                        {
                            unitEmployees = unitEmployees,
                            unitTotalPost = unitTotalPost,
                            unitTotalBlankPost = unitTotalBlankPost,
                            Id = sb.Id,
                            parentSpecialBranchUnit = sb,

                            subUnitId = subUnit.Id,
                            isParent = (isParent) ? 1 : 0,
                            hasSubUnit = false,
                            specialBranchUnit = subUnit,
                            subUnitEmployees = _context.EmployeeInfos.Where(x => x.branchId == subUnit.Id && x.rankId == rankId).ToList(),
                            subUnitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == rankId).Sum(x => x.numOfPost),
                            subUnitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == rankId).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.rankId == rankId).Count()
                        };
                        isParent = false;
                        datas.Add(data);

                    };

                }
                else
                {
                    BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                    {

                        Id = sb.Id,
                        parentSpecialBranchUnit = sb,
                        isParent = sb.isParent,
                        hasSubUnit = hasSubUnit,
                        unitEmployees = unitEmployees,
                        unitTotalPost = unitTotalPost,
                        unitTotalBlankPost = unitTotalBlankPost,
                    };
                    datas.Add(data);
                }
            }
            return datas;
        }

        public async Task<List<BranchUnitWiseEmployeesModel>> GetbranchUnitEmployeeInfosByRankUnit(int rankId, int unit, int batch, int servicePeriod, int bandId)
        {

            List<BranchUnitWiseEmployeesModel> datas = new List<BranchUnitWiseEmployeesModel>();
            var parentBranchUnits = await _context.SpecialBranchUnits.Where(x => x.isParent == 1 && x.Id == (unit == 0 ? x.Id : unit)).ToListAsync();

            var maxTime = DateTime.Now.Date;
            var minTime = DateTime.Now.Date.AddYears(-20);
            #region
            if (servicePeriod == 5)
            {
                maxTime = DateTime.Now.Date.AddYears(-3);
                minTime = DateTime.Now.Date.AddYears(-30);
            }
            else if (servicePeriod == 4)
            {
                maxTime = DateTime.Now.Date.AddYears(-2);
                minTime = DateTime.Now.Date.AddYears(-3);
            }
            else if (servicePeriod == 3)
            {
                maxTime = DateTime.Now.Date.AddYears(-1);
                minTime = DateTime.Now.Date.AddYears(-2);
            }
            else if (servicePeriod == 2)
            {
                maxTime = DateTime.Now.Date.AddMonths(-6);
                minTime = DateTime.Now.Date.AddYears(-1);
            }
            else if (servicePeriod == 1)
            {
                maxTime = DateTime.Now.Date;
                minTime = DateTime.Now.Date.AddMonths(-6);
            }
            else
            {
                maxTime = DateTime.Now.Date;
                minTime = DateTime.Now.Date.AddYears(-30);
            }
            #endregion

            int?[] doctor = { 69 };
            int?[] eng = { 59 };
            int?[] llb = { 79 };

            //int? doctorInfo = Model.branchUnitWiseEmployees?.FirstOrDefault().EducationalQualifications.Where(x => doctor.Contains(x.degreeId) && x.employeeId == @sitem.Id).Count();
            //int? engInfo = Model.branchUnitWiseEmployees?.FirstOrDefault().EducationalQualifications.Where(x => eng.Contains(x.degreeId) && x.employeeId == @sitem.Id).Count();
            //int? llbInfo = Model.branchUnitWiseEmployees?.FirstOrDefault().EducationalQualifications.Where(x => llb.Contains(x.degreeId) && x.employeeId == @sitem.Id).Count();

            List<int> employeeIds = new List<int>();
            foreach (var sb in parentBranchUnits.OrderBy(x => x.Id))
            {
               
                var unitEmployees = _context.EmployeeInfos.Include(x => x.rank).Include(x => x.bCSBatch).Include(x => x.section).Where(x => x.branchId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId) && x.branchId == (unit == 0 ? x.branchId : unit) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch) && (Convert.ToDateTime(x.joiningDatePresentWorkstation).Date>minTime && Convert.ToDateTime(x.joiningDatePresentWorkstation).Date<maxTime) && x.isApproved == 4 
                
                ).OrderBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition).ToList();
                if (bandId > 0)
                {


                    switch (bandId)
                    {
                        case 1: //Doctor
                            employeeIds = _context.EducationalQualifications.Where(x => doctor.Contains(x.degreeId)).Select(x => x.employeeId).ToList();
                            unitEmployees = unitEmployees.Where(x =>  employeeIds.Contains((int)x.Id)).ToList();
                            break;
                        case 2:  //Engineer
                            employeeIds = _context.EducationalQualifications.Where(x => eng.Contains(x.degreeId)).Select(x => x.employeeId).ToList();
                            unitEmployees = unitEmployees.Where(x => employeeIds.Contains((int)x.Id)).ToList();
                            break;
                        case 3: //Lawyer
                            employeeIds = _context.EducationalQualifications.Where(x => llb.Contains(x.degreeId)).Select(x => x.employeeId).ToList();
                            unitEmployees = unitEmployees.Where(x => employeeIds.Contains((int)x.Id)).ToList();
                            break;

                    }
                }

                var unitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost);
                var unitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == (rankId == 0 ? x.rankId : rankId)).Count();
                var subUnitList = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == sb.Id).Include(x => x.specialBranchUnit).ToListAsync();
                bool hasSubUnit = false;


                if (subUnitList.Count() > 0)
                {
                    hasSubUnit = true;
                    bool isParent = true;
                    foreach (var subUnit in subUnitList)
                    {
                        var subUnitEmployees = _context.EmployeeInfos.Include(x => x.section).Include(x => x.rank).Where(x => x.branchId == subUnit.Id && x.isApproved == 4 && x.rankId == (rankId == 0 ? x.rankId : rankId) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch) && (Convert.ToDateTime(x.joiningDatePresentWorkstation).Date > minTime && Convert.ToDateTime(x.joiningDatePresentWorkstation).Date < maxTime)).ToList();
                        if (bandId > 0)
                        {
                            switch (bandId)
                            {
                                case 1: //Doctor
                                    employeeIds = _context.EducationalQualifications.Where(x => doctor.Contains(x.degreeId)).Select(x => x.employeeId).ToList();
                                    subUnitEmployees = subUnitEmployees.Where(x => employeeIds.Contains((int)x.Id)).ToList();
                                    break;
                                case 2:  //Engineer
                                    employeeIds = _context.EducationalQualifications.Where(x => eng.Contains(x.degreeId)).Select(x => x.employeeId).ToList();
                                    subUnitEmployees = subUnitEmployees.Where(x => employeeIds.Contains((int)x.Id)).ToList();
                                    break;
                                case 3: //Lawyer
                                    employeeIds = _context.EducationalQualifications.Where(x => llb.Contains(x.degreeId)).Select(x => x.employeeId).ToList();
                                    subUnitEmployees = subUnitEmployees.Where(x => employeeIds.Contains((int)x.Id)).ToList();
                                    break;

                            }
                        }

                        BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                        {
                            unitEmployees = unitEmployees,
                            unitTotalPost = unitTotalPost,
                            unitTotalBlankPost = unitTotalBlankPost,
                            Id = sb.Id,
                            parentSpecialBranchUnit = sb,
                            subUnitId = subUnit.Id,
                            isParent = (isParent) ? 1 : 0,
                            hasSubUnit = false,
                            specialBranchUnit = subUnit,
                            subUnitEmployees = subUnitEmployees,
                            subUnitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost),
                            subUnitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == (rankId == 0 ? x.rankId : rankId)).Count()
                        };
                        isParent = false;
                        datas.Add(data);
                     

                    };

                }
                else
                {
                    BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                    {

                        Id = sb.Id,
                        parentSpecialBranchUnit = sb,
                        isParent = sb.isParent,
                        hasSubUnit = hasSubUnit,
                        unitEmployees = unitEmployees,
                        unitTotalPost = unitTotalPost,
                        unitTotalBlankPost = unitTotalBlankPost,
                    };
                    datas.Add(data);
                }
            }
            return datas;
        }

        public async Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetUnitRankWiseEmployeeList(int rankId, int unitId, int batchId, int bandId, int servicePeriodId,int isLocked,int isAttached,int isUnMission,int isPRL,int skillId)
        {
            try
            {
                var result = new List<UnitRankWiseEmployeeViewModel>();
                result = await _context.unitRankWiseEmployeeViewModels.FromSql($"SP_GetUnitRankWiseEmployeeList {rankId},{unitId},{batchId},{bandId},{servicePeriodId},{isPRL},{skillId}").ToListAsync();
                if (isLocked > 0)
                {
                    result = result.Where(x => x.lockedEmpId > 0).ToList();
                }else if (isAttached > 0)
                {
                    result = result.Where(x => x.attachedUnit!="").ToList();
                }
                else if (isUnMission > 0)
                {
                    result = result.Where(x => x.pHQTRTypeId>0).ToList();
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

        public async Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetUnitRankWiseEmployeeListExportToExcel()
        {
            try
            {
               var result = await _context.unitRankWiseEmployeeViewModels.FromSql($"SP_GetUnitRankAllEmployeeExportToExcel").ToListAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<EducationalQualification>> GetEducationalQualifications()
        {
            return await _context.EducationalQualifications.Include(x => x.degree).Include(x => x.organization).Include(x => x.reldegreesubject).ToListAsync();
        }

        public async Task<List<BranchUnitWiseEmployeesModel>> GetUnitEmployeeByRankUnitWithOutEmp(string refNum, int rankId, int unit, int batch)
        {
            var selectedEmp = await _context.EnlistedAssignments.Include(x => x.enlistedAssignmentMaster).Where(x => x.enlistedAssignmentMaster.refNo == refNum).Select(x => x.employeeId).ToListAsync();
            var selectedEmpList = await _context.EnlistedAssignments.Include(x => x.enlistedAssignmentMaster).Where(x => x.enlistedAssignmentMaster.refNo == refNum).ToListAsync();
            List<BranchUnitWiseEmployeesModel> datas = new List<BranchUnitWiseEmployeesModel>();
            var parentBranchUnits = await _context.SpecialBranchUnits.Where(x => x.isParent == 1 && x.Id == (unit == 0 ? x.Id : unit)).ToListAsync();
            foreach (var sb in parentBranchUnits.OrderBy(x => x.shortOrder.GetValueOrDefault(100)))
            {
                var unitAssignList = await _context.Assignments.Where(x => x.assignmentMaster.refNo == refNum && x.section.specialBranchUnitId == sb.Id && x.statusId != 2).Select(x => x.employeeId).ToListAsync();
                var unitEmployees = _context.EmployeeInfos.Include(x => x.rank).Include(x => x.bCSBatch).Include(x => x.branch).Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == (rankId == 0 ? x.rankId : rankId) && x.branchId == (unit == 0 ? x.branchId : unit) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch) && !selectedEmp.Contains(x.Id)).OrderBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition).ToList();
                var unitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost);
                var unitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == (rankId == 0 ? x.rankId : rankId)).Count();
                var subUnitList = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == sb.Id).Include(x => x.specialBranchUnit).ToListAsync();
                bool hasSubUnit = false;
                if (unitAssignList.Count() > 0)
                {
                    foreach (var item in unitAssignList)
                    {
                        unitEmployees.Add(_context.EmployeeInfos.Where(x => x.Id == item).FirstOrDefault());
                    }
                }


                if (subUnitList.Count() > 0)
                {
                    hasSubUnit = true;
                    bool isParent = true;
                    foreach (var subUnit in subUnitList)
                    {
                        //if (_context.EmployeeInfos.Where(x => x.branchId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch)).Count() > 0)
                        //{
                        var subUnitAssignList = await _context.Assignments.Where(x => x.assignmentMaster.refNo == refNum && x.section.specialBranchUnitId == subUnit.Id && x.statusId != 2).Select(x => x.employeeId).ToListAsync(); ;
                        var subUnitEmp = _context.EmployeeInfos.Include(x => x.section).Include(x => x.rank).Include(x => x.branch).Where(x => x.branchId == subUnit.Id && x.isApproved == 4 && x.rankId == (rankId == 0 ? x.rankId : rankId) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch) && !selectedEmp.Contains(x.Id)).ToList();
                        if (subUnitAssignList.Count() > 0)
                        {
                            foreach (var item in subUnitAssignList)
                            {
                                subUnitEmp.Add(_context.EmployeeInfos.Where(x => x.Id == item).FirstOrDefault());
                            }
                        }
                        BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                        {
                            unitEmployees = unitEmployees,
                            unitTotalPost = unitTotalPost,
                            unitTotalBlankPost = unitTotalBlankPost,
                            Id = sb.Id,
                            parentSpecialBranchUnit = sb,
                            subUnitId = subUnit.Id,
                            isParent = (isParent) ? 1 : 0,
                            hasSubUnit = false,
                            specialBranchUnit = subUnit,
                            subUnitEmployees = subUnitEmp,
                            
                            subUnitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost),
                            subUnitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.isApproved == 4 && x.rankId == (rankId == 0 ? x.rankId : rankId)).Count()
                        };
                        isParent = false;
                        datas.Add(data);
                        //}

                    };

                }
                else
                {
                    BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                    {
                        Id = sb.Id,
                        parentSpecialBranchUnit = sb,
                        isParent = sb.isParent,
                        hasSubUnit = hasSubUnit,
                        unitEmployees = unitEmployees,
                        unitTotalPost = unitTotalPost,
                        unitTotalBlankPost = unitTotalBlankPost,
                    };
                    datas.Add(data);

                }
            }
            return datas;
        }

        public async Task<IEnumerable<AssignmentPostingModel>> GetAssignmentPostingInfo(string refNo,int rankId)
        {
            try
            {
                var result = await _context.assignmentPostingModels.FromSql($"SP_GetEmployeeListForPostingAssign {refNo},{rankId}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<SuggestedBranched>> GetSuggestedPlaceForEmp(int employeeId,int page,int size, string userName)
        {
            try
            {
                var result = await _context.suggestedBrancheds.FromSql($"SP_GetSuggestedUnitByEmployee {employeeId},{page},{size},{userName}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<SuggestedBranched>> GetSuggestedBranched(int employeeId, string userName)
        {
            try
            {
                int pageNum = 1;
                int pageSize = 5;
                var result = await _context.suggestedBrancheds.FromSql($"SP_GetSuggestedUnitByEmployee {employeeId},{pageNum},{pageSize},{userName}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByUnitRank(int unit, int rank)
        {
            var result = await _context.EmployeeInfos
                .Where(x => x.branchId == unit && x.rankId == rank && x.isApproved == 4)
                .Include(x => x.branch)
                .Include(x => x.section)
                .Include(x => x.rank)
                .ToListAsync();
            return result;
        }



        public async Task<List<BranchUnitWiseEmployeesModel>> GetUnitEmployeeByRankUnitSuggested(string refNum, int rankId, int empId)
        {
            var selectedEmp = await _context.EnlistedAssignments.Include(x => x.enlistedAssignmentMaster).Where(x => x.enlistedAssignmentMaster.refNo == refNum).Select(x => x.employeeId).ToListAsync();
            var selectedEmpList = await _context.EnlistedAssignments.Include(x => x.enlistedAssignmentMaster).Where(x => x.enlistedAssignmentMaster.refNo == refNum).ToListAsync();

            var suggested = await _context.suggestedBrancheds.FromSql($"SP_GetSuggestedUnitByEmployee {empId},{""}").ToListAsync();

            List<int?> unitList = suggested.Select(x => x.unitId).ToList();
            List<int?> subUnitIdList = suggested.Select(x => x.specialBranchUnitId).ToList();
            unitList.AddRange(subUnitIdList);

            List<BranchUnitWiseEmployeesModel> datas = new List<BranchUnitWiseEmployeesModel>();
            var parentBranchUnits = await _context.SpecialBranchUnits.Where(x => x.isParent == 1).Where(x => unitList.Contains(x.Id)).ToListAsync();

            foreach (var sb in parentBranchUnits.OrderBy(x => x.Id))
            {
                var unitAssignList = await _context.Assignments.Where(x => x.assignmentMaster.refNo == refNum && x.section.specialBranchUnitId == sb.Id).Select(x => x.employeeId).ToListAsync();
                var unitEmployees = _context.EmployeeInfos.Include(x => x.rank).Include(x => x.bCSBatch).Where(x => x.branchId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId) && !selectedEmp.Contains(x.Id)).OrderBy(x => x.bCSBatchId).ThenBy(x => x.bcsPosition).ToList();
                var unitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost);
                var unitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Count();
                var education = await _context.EducationalQualifications.Include(x => x.degree).Include(x => x.organization).Include(x => x.reldegreesubject).ToListAsync();
                var subUnitList = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == sb.Id).Where(x => subUnitIdList.Contains(x.Id)).Include(x => x.specialBranchUnit).ToListAsync();
                bool hasSubUnit = false;
                if (unitAssignList.Count() > 0)
                {
                    foreach (var item in unitAssignList)
                    {
                        unitEmployees.Add(_context.EmployeeInfos.Where(x => x.Id == item).FirstOrDefault());
                    }
                }


                if (subUnitList.Count() > 0)
                {
                    hasSubUnit = true;
                    bool isParent = true;
                    foreach (var subUnit in subUnitList)
                    {
                        //if (_context.EmployeeInfos.Where(x => x.branchId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId) && x.bCSBatchId == (batch == 0 ? x.bCSBatchId : batch)).Count() > 0)
                        //{
                        var subUnitAssignList = await _context.Assignments.Where(x => x.assignmentMaster.refNo == refNum && x.section.specialBranchUnitId == subUnit.Id).Select(x => x.employeeId).ToListAsync(); ;
                        var subUnitEmp = _context.EmployeeInfos.Include(x => x.section).Include(x => x.rank).Where(x => x.branchId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId) && !selectedEmp.Contains(x.Id)).ToList();
                        if (subUnitAssignList.Count() > 0)
                        {
                            foreach (var item in subUnitAssignList)
                            {
                                subUnitEmp.Add(_context.EmployeeInfos.Where(x => x.Id == item).FirstOrDefault());
                            }
                        }
                        BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                        {
                            unitEmployees = unitEmployees,
                            unitTotalPost = unitTotalPost,
                            unitTotalBlankPost = unitTotalBlankPost,
                            Id = sb.Id,
                            parentSpecialBranchUnit = sb,
                            subUnitId = subUnit.Id,
                            isParent = (isParent) ? 1 : 0,
                            hasSubUnit = false,
                            specialBranchUnit = subUnit,
                            EducationalQualifications = education,
                            subUnitEmployees = subUnitEmp,
                            subUnitTotalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost),
                            subUnitTotalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == subUnit.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == sb.Id && x.rankId == (rankId == 0 ? x.rankId : rankId)).Count()
                        };
                        isParent = false;
                        datas.Add(data);
                        //}

                    };

                }
                else
                {
                    //if (unitEmployees.Count > 0)
                    //{
                    BranchUnitWiseEmployeesModel data = new BranchUnitWiseEmployeesModel
                    {
                        Id = sb.Id,
                        parentSpecialBranchUnit = sb,
                        isParent = sb.isParent,
                        hasSubUnit = hasSubUnit,
                        EducationalQualifications = education,
                        unitEmployees = unitEmployees,
                        unitTotalPost = unitTotalPost,
                        unitTotalBlankPost = unitTotalBlankPost,
                    };
                    datas.Add(data);
                    //}
                    //else
                    //{
                    //    continue;
                    //}

                }
            }

            return datas;
        }




        public async Task<IEnumerable<EmployeeAssignmentModel>> GetUnitWiseEmployeeInfos()
        {
            var result = await _context.SpecialBranchUnits.ToListAsync();
            List<EmployeeAssignmentModel> empBrnchWse = new List<EmployeeAssignmentModel>();

            #region SET Branch Wise Employee

            foreach (var data in result)
            {
                if (data.isParent == 1)
                {
                    var branchList = await _context.SpecialBranchUnits.Where(x => x.specialBranchUnitId == data.Id).ToListAsync();

                    var branchSingle = new SpecialBranchUnit
                    {
                        Id = data.Id,
                        branchCode = data.branchCode,
                        branchUnitName = data.branchUnitName,
                        isParent = data.isParent,
                        shortOrder = data.shortOrder,
                        isdefault = data.isdefault,
                        specialBranchUnit = data.specialBranchUnit,
                        EmployeeInfos = _context.EmployeeInfos.Where(x => x.branchId == data.Id),
                        totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == data.Id).Sum(x => x.numOfPost),
                        totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == data.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == data.Id).Count()
                    };

                    List<SpecialBranchUnit> AddBranchList = new List<SpecialBranchUnit>();
                    foreach (var item in branchList)
                    {
                        var branch = new SpecialBranchUnit
                        {
                            Id = item.Id,
                            branchCode = item.branchCode,
                            branchUnitName = item.branchUnitName,
                            isParent = item.isParent,
                            shortOrder = item.shortOrder,
                            isdefault = item.isdefault,
                            specialBranchUnit = item.specialBranchUnit,
                            EmployeeInfos = _context.EmployeeInfos.Where(x => x.branchId == item.Id),
                            totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == item.Id).Sum(x => x.numOfPost),
                            totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == item.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == item.Id).Count()
                        };
                        AddBranchList.Add(branch);
                    }

                    empBrnchWse.Add(new EmployeeAssignmentModel
                    {
                        specialBranchUnit = branchSingle,
                        specialBranchUnits = AddBranchList,
                        haveChild = 1
                    });

                }
                else
                {
                    var branch = new SpecialBranchUnit
                    {
                        Id = data.Id,
                        branchCode = data.branchCode,
                        branchUnitName = data.branchUnitName,
                        isParent = data.isParent,
                        shortOrder = data.shortOrder,
                        isdefault = data.isdefault,
                        specialBranchUnit = data.specialBranchUnit,
                        EmployeeInfos = _context.EmployeeInfos.Where(x => x.branchId == data.Id),
                        totalPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == data.Id).Sum(x => x.numOfPost),
                        totalBlankPost = _context.PostInUnits.Where(x => x.specialBranchUnitId == data.Id).Sum(x => x.numOfPost) - _context.EmployeeInfos.Where(x => x.branchId == data.Id).Count()
                    };
                    empBrnchWse.Add(new EmployeeAssignmentModel
                    {
                        specialBranchUnit = branch,
                        haveChild = 0
                    });
                }
            }

            #endregion

            return empBrnchWse;
        }

        public async Task<IEnumerable<SuggestedUnitModel>> GetSugesstetUnit(int employeeId, string userName)
        {
            try
            {
                var result = await _context.SuggestedUnits.FromSql($"SP_GetSuggestedUnitByEmployee {employeeId},{userName}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> GetAssignMasterIdByRef(string refNo)
        {
            var result = await _context.AssignmentMasters
                .Where(x => x.refNo == refNo)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> GetSpecialBranchUnitBySectionId(int sectionId)
        {
            var result = await _context.Sections
                .Where(x => x.Id == sectionId)
                .Select(x =>Convert.ToInt32( x.specialBranchUnitId))
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentByRef(string refNo)
        {
            var result = await _context.Assignments
                .Where(x => x.assignmentMaster.refNo == refNo && x.statusId == 1)
                .Include(x => x.employee)
                .Include(x => x.section)
                .ToListAsync();
            return result;
        }

        public async Task<string> GetAssignRefNoIdByMasterId(int id)
        {
            var result = await _context.AssignmentMasters
                .Where(x => x.Id == id)
                .Select(x => x.refNo)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> GetAssignRefNoIdByEnlistId(int id)
        {
            var result = await _context.EnlistedAssignmentMasters
                .Where(x => x.Id == id)
                .Select(x => x.refNo)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<EnlistedAssignment> CheckEnlistAndRemoveAssign(int masterId, int empId)
        {
            var result = await _context.EnlistedAssignments
                .Where(x => x.enlistedAssignmentMasterId == masterId && x.employeeId == empId)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                var refNum = await _context.EnlistedAssignmentMasters.Where(x => x.Id == masterId).Select(x => x.refNo).FirstOrDefaultAsync();
                var id = await _context.Assignments.Where(x => x.assignmentMaster.refNo == refNum && x.employeeId == empId).Select(x => x.Id).FirstOrDefaultAsync();
                if (id > 0)
                {
                    var delete = _context.Assignments.Remove(await _context.Assignments.FindAsync(id));
                    var update = await _context.SaveChangesAsync();
                }
                
            }
            return result;
        }

        public async Task<int> UpdateEnlistStatus(string refNo, int empId)
        {
            var enlist = await _context.EnlistedAssignments.Where(x => x.enlistedAssignmentMaster.refNo == refNo && x.employeeId == empId).FirstOrDefaultAsync();
            var assign = await _context.Assignments.Where(x => x.assignmentMaster.refNo == refNo && x.employeeId == empId).FirstOrDefaultAsync();
            enlist.statusId = 1;
            _context.EnlistedAssignments.Update(enlist);
            var save = await _context.SaveChangesAsync();
            if (assign != null)
            {
                assign.statusId = 2;
                _context.Assignments.Update(assign);
                var save1 = await _context.SaveChangesAsync();
            }
            return save;
        }

        public async Task<IEnumerable<ApprovalLog>> GetApprovalLogByAssignmentMasterId(int id)
        {
            var data = await _context.ApprovalLogs.Include(x => x.nextApprovar).Include(x => x.user).Where(x => x.masterId == id)
                .Select(x => new ApprovalLog {
                    nextApprovar = x.nextApprovar,
                    user = x.user,
                    createdBy = _context.EmployeeInfos.Include(a=>a.rank).Include(b=>b.section).Where(y => y.ApplicationUserId == x.user.Id).Select(c => c.rank.rankNameBN).FirstOrDefault(),
                    updatedBy = _context.Photographs.Where(y => y.employee.ApplicationUserId == x.user.Id && y.type== "signature").Select(y => y.url).FirstOrDefault()
                }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<InternalApprovalLog>> GetInternalApprovalLogByAssignmentMasterId(int id)
        {
            var data = await _context.InternalApprovalLogs.Include(x => x.nextApprovar).Include(x => x.user).Where(x => x.masterId == id)
                .Select(x => new InternalApprovalLog
                {
                    nextApprovar = x.nextApprovar,
                    user = x.user,
                    createdBy = _context.EmployeeInfos.Include(a=>a.rank).Include(b=>b.section).Where(y => y.ApplicationUserId == x.user.Id).Select(c => c.rank.rankNameBN).FirstOrDefault(),
                    updatedBy = _context.Photographs.Where(y => y.employee.ApplicationUserId == x.user.Id && y.type== "signature").Select(y => y.url).FirstOrDefault()
                }).ToListAsync();
            return data;
        }
    }
}
