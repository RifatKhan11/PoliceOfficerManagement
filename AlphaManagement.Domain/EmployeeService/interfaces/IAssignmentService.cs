using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.EmployeeService.Interfaces
{
    public interface IAssignmentService
    {
        Task<IEnumerable<SpecialBranchUnit>> GetUnitWiseEmployeeInfo();
        Task<IEnumerable<Rank>> GetRankWiseEmployeeInfo();
        Task<IEnumerable<BranchUnitVM>> GetUnitWiseAllEmployeeInfo();
        Task<IEnumerable<SpecialBranchUnit>> GetUnitWiseAllEmployeeInfos();
        Task<IEnumerable<EnlistedAssignment>> GetEnlistedAssignment(string referenceNumber);
        Task<IEnumerable<EnlistedAssignment>> GetReturnedEnlistedAssignment(string referenceNumber);
        Task<IEnumerable<EmployeeAssignmentModel>> GetUnitWiseEmployeeInfos();
        Task<List<BranchUnitWiseEmployeesModel>> GetbranchUnitWiseEmployeeInfos();
        Task<List<BranchUnitWiseEmployeesModel>> GetbranchUnitEmployeeInfosByRank(int rankId);
        Task<EmployeeInfo> GetAssignmentMasterEmployeeInfoById(int id);
        Task<List<BranchUnitWiseEmployeesModel>> GetbranchUnitEmployeeInfosByRankUnit(int rankId, int unit, int batch,int servicePeriod, int bandId);
        Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetUnitRankWiseEmployeeList(int rankId, int unitId, int batchId, int bandId, int servicePeriodId, int isLocked, int isAttached, int isUnMission, int isPRL,int skillId);
        Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetUnitRankWiseEmployeeListExportToExcel();
        Task<List<BranchUnitWiseEmployeesModel>> GetUnitEmployeeByRankUnitWithOutEmp(string refNum, int rankId, int unit, int batch);
        Task<Assignment> GetAssignmentById(int id);
        Task<Assignment> GetAssignmentByIdSingle(int id);
        Task<IEnumerable<SpecialBranchUnit>> SuggestedSpecialBranchUnit(int empId);
        Task<IEnumerable<SuggestedUnitModel>> GetSugesstetUnit(int employeeId, string userName);
        Task<int> GetAssignMasterIdByRef(string refNo);
        Task<string> GetAssignRefNoIdByEnlistId(int id);
        Task<string> GetAssignRefNoIdByMasterId(int id);
        Task<List<BranchUnitWiseEmployeesModel>> GetUnitEmployeeByRankUnitSuggested(string refNum, int rankId, int empId);
        Task<EnlistedAssignment> CheckEnlistAndRemoveAssign(int masterId, int empId);
        Task<IEnumerable<Assignment>> GetAssignmentByRef(string refNo);
        Task<IEnumerable<SuggestedBranched>> GetSuggestedBranched(int employeeId, string userName);
        Task<int> UpdateEnlistStatus(string refNo, int empId);
        Task<IEnumerable<EducationalQualification>> GetEducationalQualifications();
        Task<IEnumerable<Assignment>> GetAssignmentDetailsByMasterId(int id);
        Task<AssignmentMaster> GetAssignmentMasterById(int id);
        Task<IEnumerable<AssignmentPostingModel>> GetAssignmentPostingInfo(string refNo,int rankId);

        Task<IEnumerable<SpecialBranchUnit>> GetPositionVacency(int rankId, int branchId);
        Task<IEnumerable<Rank>> GetRankGreaterASP();
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByUnitRank(int unit, int rank);
        Task<IEnumerable<EnlistedAssignment>> GetEnlistedAssignmentOngoing(string referenceNumber);
        Task<IEnumerable<EnlistedAssignment>> GetNewEnlistedByRefNo(string refNo);
        Task<EnlistedAssignmentMaster> GetEnlistedMasterById(int id);
        Task<EnlistedAssignmentMaster> GetEnlistedMasterByRefNo(string refNo);
        Task<int> GetSpecialBranchUnitBySectionId(int sectionId);
        Task<InternalEnlistedAssignment> GetInternalEnlistedAssignmentIdSingle(int id);
        Task<IEnumerable<ApprovalLog>> GetApprovalLogByAssignmentMasterId(int id);
        Task<IEnumerable<InternalApprovalLog>> GetInternalApprovalLogByAssignmentMasterId(int id);
        Task<IEnumerable<SuggestedBranched>> GetSuggestedPlaceForEmp(int employeeId, int page, int size, string userName);
        Task<IEnumerable<Assignment>> GetAssignmentWithDuejoining();
    }
}
