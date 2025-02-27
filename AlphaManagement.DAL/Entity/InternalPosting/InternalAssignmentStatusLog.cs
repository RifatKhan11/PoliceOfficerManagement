using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.InternalPosting
{
    public class InternalAssignmentStatusLog:Base
    {
        public int? assignmentId { get; set; }
        public InternalAssignmentMaster assignment { get; set; }

        public int? enlistedAssignmentId { get; set; }
        public InternalEnlistedAssignmentMaster enlistedAssignment { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public string remarks { get; set; }

        public int? statusInfoId { get; set; }
        public StatusInfo statusInfo { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string empName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string nextEmpName { get; set; }
        
        public string Status { get; set; }
    }
}
