using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.ApprovalMatrix
{
    public class ApprovalLog:Base
    {
        public int? masterId { get; set; }
        public AssignmentMaster master { get; set; }
        public int MyProperty { get; set; }
        public int? matrixTypeId { get; set; }
        public MatrixType matrixType { get; set; }
        public string userId { get; set; }
        public ApplicationUser user { get; set; }
        public string nextApprovarId { get; set; }
        public ApplicationUser nextApprovar { get; set; }
        public int? approverTypeId { get; set; }
        public ApproverType approverType { get; set; }
        public int? isActive { get; set; }
        public int? sequenseNo { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string notes { get; set; }
    }
}
