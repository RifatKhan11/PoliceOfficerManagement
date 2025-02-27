using AlphaManagement.DAL.Entity;
using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Models;
using AlphaManagement.DAL.Models.Auth;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;
using AlphaManagement.Web.Areas.Employee.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class AssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public int masterId { get; set; }
        public int employeeId { get; set; }
        public int empId { get; set; }
        public int? assignmentTypeId { get; set; }//1=Assignment,2=Transfer
        public int? EntryNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? designationId { get; set; }
        public Designation designation { get; set; }
        public int? departmentId { get; set; }
        public Department department { get; set; }
        public int? policeUnitId { get; set; }
        public PoliceUnit policeUnit { get; set; }
        public int? policeSubUnitId { get; set; }
        public int? enlistedId { get; set; }
        public PoliceSubUnit policeSubUnit { get; set; }
        public string Remarks { get; set; }
        public string designationName { get; set; }
        public int? jobRunningCheckBox { get; set; }
        public AssignmentLn fLang { get; set; }
        public Assignment assignment { get; set; }
        public ApplicationUser applicationUser { get; set; }


        public int empJobHistoryId { get; set; }
        public int? secId { get; set; }
        public int? ranksId { get; set; }
        public int? specialBranchUnitId { get; set; }
        public int? jobSpecialBranchUnitId { get; set; }
        public int? assignMasterId { get; set; }
        public string instituteName { get; set; }
        public string reasonofTransfer { get; set; }
        public string servicePeriod { get; set; }
        public string jobsectionName { get; set; }
        public string refNumber { get; set; }
        public string noteSheetTitle { get; set; }
        public string noteComment { get; set; }
        public string roleName { get; set; }
        public int? PoliceId { get; set; }
        public DateTime? AssignjoiningDate { get; set; }
        public DateTime? AssignresignDate { get; set; }


        public IEnumerable<Section> sections { get; set; }
        public IEnumerable<Department> phqSections { get; set; }

        public IEnumerable<PostingReportViewModel> assignmentMasters { get; set; }
        public IEnumerable<BadgeAndActivityModel> badgeAndActivityModels { get; set; }

        public IEnumerable<Assignment> assignments { get; set; }
        public IEnumerable<CancelAssignment> assignmentRevised { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public ApplicationUser userInfo { get; set; }
        public AssignmentMaster assignmentMaster { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<Department> departments { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
        public IEnumerable<SpecialSkillType> specialSkillTypes { get; set; }
        public IEnumerable<BCSBatch> bCSBatches { get; set; }
        public IEnumerable<PostingReportView> assignment2 { get; set; }
        public IEnumerable<InternalPostingReportVM> Internalassignment2 { get; set; }
        public IEnumerable<PostingReportView> Internalassignment1 { get; set; }
        public IEnumerable<PostingReportView> assignmentApprove { get; set; }
        public IEnumerable<AssignmentMaster> assignments1 { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public SpecialBranchUnit presentUnits { get; set; }
        public IEnumerable<SpecialBranchUnit> previousUnits { get; set; }
        public IEnumerable<SpecialBranchUnit> spouseUnits { get; set; }
        public IEnumerable<SpecialBranchUnit> homeDistUnits { get; set; }
        public IEnumerable<BranchUnitVM> branchUnitVMs { get; set; }
        public IEnumerable<EnlistedAssignment> enlistedAssignments { get; set; }
        public IEnumerable<BranchUnitWiseEmployeesModel> branchUnitWiseEmployees { get; set; }
        public IEnumerable<UnitRankWiseEmployeeViewModel> unitRankWiseEmployeeViewModels { get; set; }
        public IEnumerable<AssignmentPostingModel> assignmentPostingModels { get; set; }
        public IEnumerable<SuggestedUnitModel> suggestedUnits { get; set; }
        public IEnumerable<EducationalQualification> educations { get; set; }
        public IEnumerable<SuggestedBranched> suggestedBrancheds { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
        public AspNetUsersViewModel aspNetUserModel { get; set; }
        public IEnumerable<ApprovalLog> approvalLogs { get; set; }
        public IEnumerable<PostingReportView> postingReportViews { get; set; }
        public IEnumerable<AssignmentDetailsModal> assignmentDetailsModals { get; set; }
        public IEnumerable<InternalAssignment> internalAssignments { get; set; }
    }
}
