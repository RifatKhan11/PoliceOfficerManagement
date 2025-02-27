using AlphaManagement.DAL;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.Domain.EmployeeService.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmployeeService
{
    public class ReportService: IReportService
    {
        private readonly AlphaDbContext _context;

        public ReportService(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DisciplinaryAction>> GetUnitRankBatchWisePendingDisciplinaryActionList(int unitId, int batchId, int rankId)
        {
            return await _context.DisciplinaryActions.Where(x => x.employee.branchId==(unitId == 0? x.employee.branchId : unitId) && x.employee.rankId==(rankId == 0 ? x.employee.rankId : rankId) && x.employee.bCSBatchId==(batchId == 0 ? x.employee.bCSBatchId : batchId) && (x.status=="1" || x.status=="0"))
                .Include(x => x.employee).Include(x => x.employee.rank).Include(x => x.employee.branch).Include(x => x.employee.bCSBatch).ToListAsync();
        }

        public async Task<IEnumerable<DisciplinaryAction>> GetUnitRankBatchWiseApprovedDisciplinaryActionList(int unitId, int batchId, int rankId)
        {
            return await _context.DisciplinaryActions.Where(x => x.employee.branchId==(unitId == 0? x.employee.branchId : unitId) && x.employee.rankId==(rankId == 0 ? x.employee.rankId : rankId) && x.employee.bCSBatchId==(batchId == 0 ? x.employee.bCSBatchId : batchId) && x.status=="2")
                .Include(x => x.employee).Include(x => x.employee.rank).Include(x => x.employee.branch).Include(x => x.employee.bCSBatch).ToListAsync();
        }

        public async Task<int> ApproveDisciplinary(int id)
        {
            var obj = await _context.DisciplinaryActions.FindAsync(id);
            obj.status = "2";
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TraningLog>> GetUnitRankBatchWisePendingTrainingList(int unitId, int batchId, int rankId)
        {
            return await _context.TraningLogs.Where(x => x.employee.branchId == (unitId == 0 ? x.employee.branchId : unitId) && x.employee.rankId == (rankId == 0 ? x.employee.rankId : rankId) && x.employee.bCSBatchId == (batchId == 0 ? x.employee.bCSBatchId : batchId) && x.status == 1)
                .Include(x => x.employee).Include(x => x.employee.rank).Include(x => x.employee.branch).Include(x => x.employee.bCSBatch).Include(x=>x.trainingInstitute).Include(x=>x.trainingCategory).Include(x=>x.country).ToListAsync();
        }

        public async Task<IEnumerable<TraningLog>> GetUnitRankBatchWiseApprovedTrainingList(int unitId, int batchId, int rankId)
        {
            return await _context.TraningLogs.Where(x => x.employee.branchId == (unitId == 0 ? x.employee.branchId : unitId) && x.employee.rankId == (rankId == 0 ? x.employee.rankId : rankId) && x.employee.bCSBatchId == (batchId == 0 ? x.employee.bCSBatchId : batchId) && x.status == 2)
               .Include(x => x.employee).Include(x => x.employee.rank).Include(x => x.employee.branch).Include(x => x.employee.bCSBatch).Include(x => x.trainingInstitute).Include(x => x.trainingCategory).Include(x => x.country).ToListAsync();
        }

        public async Task<int> ApproveTraningLog(int id)
        {
            var obj = await _context.TraningLogs.FindAsync(id);
            obj.status = 2;
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeMadicalInfo>> GetUnitRankBatchWisePendingMedicalList(int unitId, int batchId, int rankId)
        {
            return await _context.employeeMadicalInfos.Where(x => x.employeeInfo.branchId == (unitId == 0 ? x.employeeInfo.branchId : unitId) && x.employeeInfo.rankId == (rankId == 0 ? x.employeeInfo.rankId : rankId) && x.employeeInfo.bCSBatchId == (batchId == 0 ? x.employeeInfo.bCSBatchId : batchId) && x.status == 1)
                .Include(x => x.employeeInfo).Include(x => x.employeeInfo.rank).Include(x => x.employeeInfo.branch).Include(x => x.employeeInfo.bCSBatch).Include(x => x.medicalSubCategory).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeMadicalInfo>> GetUnitRankBatchWiseApprovedMedicalList(int unitId, int batchId, int rankId)
        {
            return await _context.employeeMadicalInfos.Where(x => x.employeeInfo.branchId == (unitId == 0 ? x.employeeInfo.branchId : unitId) && x.employeeInfo.rankId == (rankId == 0 ? x.employeeInfo.rankId : rankId) && x.employeeInfo.bCSBatchId == (batchId == 0 ? x.employeeInfo.bCSBatchId : batchId) && x.status == 2)
               .Include(x => x.employeeInfo).Include(x => x.employeeInfo.rank).Include(x => x.employeeInfo.branch).Include(x => x.employeeInfo.bCSBatch).Include(x => x.medicalSubCategory).ToListAsync();
        }

        public async Task<int> ApproveMedical(int id)
        {
            var obj = await _context.employeeMadicalInfos.FindAsync(id);
            obj.status = 2;
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWisePendingSBList(int unitId, int batchId, int rankId)
        {
            return await _context.employeeReportInfos.Where(x => x.employeeInfo.branchId == (unitId == 0 ? x.employeeInfo.branchId : unitId) && x.employeeInfo.rankId == (rankId == 0 ? x.employeeInfo.rankId : rankId) && x.employeeInfo.bCSBatchId == (batchId == 0 ? x.employeeInfo.bCSBatchId : batchId) && x.status == 1 && x.type == "BPA")
                .Include(x => x.employeeInfo).Include(x => x.employeeInfo.rank).Include(x => x.employeeInfo.branch).Include(x => x.employeeInfo.bCSBatch).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWiseApprovedSBList(int unitId, int batchId, int rankId)
        {
            return await _context.employeeReportInfos.Where(x => x.employeeInfo.branchId == (unitId == 0 ? x.employeeInfo.branchId : unitId) && x.employeeInfo.rankId == (rankId == 0 ? x.employeeInfo.rankId : rankId) && x.employeeInfo.bCSBatchId == (batchId == 0 ? x.employeeInfo.bCSBatchId : batchId) && x.status == 2 && x.type == "BPA")
               .Include(x => x.employeeInfo).Include(x => x.employeeInfo.rank).Include(x => x.employeeInfo.branch).Include(x => x.employeeInfo.bCSBatch).ToListAsync();
        }

        public async Task<int> ApproveSB(int id)
        {
            var obj = await _context.employeeReportInfos.FindAsync(id);
            obj.status = 2;
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWisePendingBPAList(int unitId, int batchId, int rankId)
        {
            return await _context.employeeReportInfos.Where(x => x.employeeInfo.branchId == (unitId == 0 ? x.employeeInfo.branchId : unitId) && x.employeeInfo.rankId == (rankId == 0 ? x.employeeInfo.rankId : rankId) && x.employeeInfo.bCSBatchId == (batchId == 0 ? x.employeeInfo.bCSBatchId : batchId) && x.status == 1 && x.type == "SB")
                .Include(x => x.employeeInfo).Include(x => x.employeeInfo.rank).Include(x => x.employeeInfo.branch).Include(x => x.employeeInfo.bCSBatch).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWiseApprovedBPAList(int unitId, int batchId, int rankId)
        {
            return await _context.employeeReportInfos.Where(x => x.employeeInfo.branchId == (unitId == 0 ? x.employeeInfo.branchId : unitId) && x.employeeInfo.rankId == (rankId == 0 ? x.employeeInfo.rankId : rankId) && x.employeeInfo.bCSBatchId == (batchId == 0 ? x.employeeInfo.bCSBatchId : batchId) && x.status == 2 && x.type == "SB")
               .Include(x => x.employeeInfo).Include(x => x.employeeInfo.rank).Include(x => x.employeeInfo.branch).Include(x => x.employeeInfo.bCSBatch).ToListAsync();
        }

        public async Task<int> ApproveBPA(int id)
        {
            var obj = await _context.employeeReportInfos.FindAsync(id);
            obj.status = 2;
            return await _context.SaveChangesAsync();
        }
    }
}
