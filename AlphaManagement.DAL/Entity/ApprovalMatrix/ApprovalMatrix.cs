using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.ApprovalMatrix
{
    public class ApprovalMatrix:Base
    {
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
    }
}
