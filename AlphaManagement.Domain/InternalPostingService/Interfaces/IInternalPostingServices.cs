using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaManagement.Domain.MasterDataServices.Interfaces
{
    public interface IInternalPostingServices
    {
        Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetSectionRankWiseEmployeeList(int rankId, int unitId, int batchId, int bandId, int servicePeriodId, int isLocked, int isAttached, int isUnMission);
        Task<IEnumerable<UnitRankWiseEmployeeViewModel>> GetMutipleRankWiseSectionEmployeeList(string rankId, int unitId, int batchId, int bandId, int servicePeriodId, int isLocked, int isAttached, int isUnMission);
        Task<IEnumerable<InternalUnitRankWiseEmployeeViewModel>> GetInternalMutipleRankWiseSectionEmployeeList(string rankId, int unitId, int batchId, int bandId, int servicePeriodId, int isLocked, int isAttached, int isUnMission);
        Task<int> SaveInternalEnlistMaster(InternalEnlistedAssignmentMaster model);
        Task<int> SaveInternalEnlist(InternalEnlistedAssignment model);
        Task<IEnumerable<InternalEnlistedAssignment>> GetEnlistDetailsByMasterId(int id);
        Task<IEnumerable<Department>> GetDepartmentWiseTotalEmployee();
        Task<IEnumerable<InternalPostingReportVM>> PendingEnlistMasters(string userId);
        Task<IEnumerable<PostingReportView>> PendingEnlistMastersForIgp(string userId);
        Task<IEnumerable<InternalEnlistedAssignment>> GetAssignmentsPriviousLockByMasterId(int id);
        Task<IEnumerable<InternalAssignmentDetailsModal>> AssignmentDetailsModalByMasterId(int assignid);
        Task<EmployeeInfo> GetAssignmentMasterEmployeeInfoById(int id);
        Task<EmployeeInfo> GetEmployeeInfoByBpNo(string bp);
        Task<IEnumerable<InternalPostingReportVM>> GetApprovalLogFullByMasterId(int masterId);
        Task<IEnumerable<BadgeAndActivityModel>> GetBadgeAndActivityModelList();
        void UpdateApprovalLogBymasterId(int masterId);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeListBySectionId(int id);
        Task<InternalEnlistedAssignment> GetAssignmentById(int id);
        Task<int> UpdateAssignmentInfoForIGP(int id);
        Task<IEnumerable<InternalEnlistedAssignment>> GetInternalEnlistedAssignmentsByMasterId(int id);
        Task<int> SaveInternalAssignmentMaster(InternalAssignmentMaster medicalInfo);
        Task<IEnumerable<InternalPostingReportVM>> InternalAssignmentMastersList(string userId);
        Task<IEnumerable<InternalPostingReportVM>> AssignmentReturnList(string userId);
        Task<IEnumerable<InternalPostingReportVM>> ApprovedInternalAssignmentMastersList(string userId);
        Task<int?> ApprovedInternalAssignmentMastersListCount(string userId);
        Task<int?> AssignmentReturnListCount(string userId);
        Task<int?> PendingEnlistMastersForIgpCount();
        Task<int?> AssignmentInternalReturnAndLockCount(int status);
        Task<IEnumerable<InternalPostingReportVM>> ApprovedInternalAssignmentMastersListForIgp(int status);
        Task<int?> InternalMinimumRankStatus(int masterId);
        Task<IEnumerable<AssignmentVM>> InternalAssignmentPostedByMasterId(int assignid);
        Task<IEnumerable<InternalAssignment>> InternalAssignmentAll();
        Task<IEnumerable<InternalAssignment>> GetActiveInternalAssignments();
        Task<IEnumerable<InternalAssignment>> InternalAssignmentDetailsByMasterId(int id);
        Task<IEnumerable<InternalAssignment>> InternalAssignmentsDetailsByMasterId(int id);
        Task<IEnumerable<InternalAssignment>> InternamAssignments(int masterId);
        Task<InternalAssignmentMaster> InternalAssignmentMaster(string refNo);
        Task<InternalAssignmentMaster> InternalAssignmentMasterEnlistId(int id);
        Task<IEnumerable<PostingReportView>> ApprovedInternalAssignmentMasters(string userId);
        Task<IEnumerable<InternalAssignmentAnulipi>> AssignmentMastersRopo(int id);
        Task<int> SaveInternamAssignments(InternalAssignment model);
        Task<IEnumerable<AssignmentAnulipiPreview>> AssignmentMastersRoportPre(int id);
        void DeleteAssignmentsAunilipiPreviewByMasterId(int id);
        Task<Spouse> GetSpouseByEmpId(int id);
        #region PHQ Dashboard
        Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoList();
        Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoListFilter(int rank, int unit, int batch);
        Task<IEnumerable<EmployeeInfo>> GetOverDueEmployeeInfoList(int rank, int unit, int batch); Task<IEnumerable<PostingReportView>> InternalAssignmentMasters(string userId);
        #endregion
    }
}
