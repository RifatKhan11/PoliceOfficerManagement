using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmployeeService.interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<DisciplinaryAction>> GetUnitRankBatchWisePendingDisciplinaryActionList(int unitId, int batchId, int rankId);
        Task<IEnumerable<DisciplinaryAction>> GetUnitRankBatchWiseApprovedDisciplinaryActionList(int unitId, int batchId, int rankId);
        Task<int> ApproveDisciplinary(int id);
        Task<IEnumerable<TraningLog>> GetUnitRankBatchWisePendingTrainingList(int unitId, int batchId, int rankId);
        Task<IEnumerable<TraningLog>> GetUnitRankBatchWiseApprovedTrainingList(int unitId, int batchId, int rankId);
        Task<int> ApproveTraningLog(int id);
        Task<IEnumerable<EmployeeMadicalInfo>> GetUnitRankBatchWisePendingMedicalList(int unitId, int batchId, int rankId);
        Task<IEnumerable<EmployeeMadicalInfo>> GetUnitRankBatchWiseApprovedMedicalList(int unitId, int batchId, int rankId);
        Task<int> ApproveMedical(int id);
        Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWisePendingBPAList(int unitId, int batchId, int rankId);
        Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWiseApprovedBPAList(int unitId, int batchId, int rankId);
        Task<int> ApproveBPA(int id);
        Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWisePendingSBList(int unitId, int batchId, int rankId);
        Task<IEnumerable<EmployeeReportInfo>> GetUnitRankBatchWiseApprovedSBList(int unitId, int batchId, int rankId);
        Task<int> ApproveSB(int id);
    }
}
