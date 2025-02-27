using AlphaManagement.DAL.Entity.ApprovalMatrix;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.InternalPosting;
using AlphaManagement.DAL.Entity.MasterData;
using AlphaManagement.DAL.Migrations;
using AlphaManagement.DAL.Models.EmployeeInfoeModel;
using AlphaManagement.DAL.Models.Internal;
using AlphaManagement.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Areas.Employee.Models
{
    public class PostingReportViewModel
    {

        public int assignMasterId { get; set; }

        public string refNo { get; set; }

        public DateTime refDate { get; set; }

        public string applicationUserId { get; set; }

        public int? statusId { get; set; }

        public string title { get; set; }
        public string[] newAnulipiTxt { get; set; }

        public string description { get; set; }
        public int?[] anulipiIds { get; set; }

        public int?[] anuLipiList { get; set; }

        public IEnumerable<AnulipiList> anulipiLists { get; set; }
        public IEnumerable<InternalAnulipiList> internalAnulipiLists { get; set; }
        public IEnumerable<EducationalQualification> educations { get; set; }
        public IEnumerable<Assignment> assignments { get; set; }
        public IEnumerable<InternalAssignment> internalAssignments { get; set; }
        public IEnumerable<InternalAssignmentDetailsModal> internalEnlistAssignments { get; set; }
        public IEnumerable<Assignment> assignmentALL { get; set; }
        public IEnumerable<InternalAssignment> internalAssignmentALL { get; set; }
        public IEnumerable<Assignment> assignmentMas { get; set; }
        public IEnumerable<AssignmentVM> assignmentVMs { get; set; }
        public IEnumerable<AssignmentViewModels> aVM { get; set; }
        public IEnumerable<AssignmentMaster> assignmentMasters { get; set; }
        public IEnumerable<EnlistedAssignment> enlistedAssignments { get; set; }
        public IEnumerable<AssignmentAnulipi> assignmentAnulipis { get; set; }
        public IEnumerable<InternalAssignmentAnulipi> internalAssignmentAnulipis { get; set; }
        public IEnumerable<AssignmentAnulipiPreview> assignmentAnulipisPreview { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<PostingReportView> postingReportViews { get; set; }
        public IEnumerable<PostingReportView> InternalpostingReportViews { get; set; }
        public IEnumerable<InternalPostingReportVM> Internalassignment2 { get; set; }
        public IEnumerable<ApprovalLog> approvals { get; set; }
        public IEnumerable<InternalApprovalLog> internalApprovals { get; set; }
    }
}

