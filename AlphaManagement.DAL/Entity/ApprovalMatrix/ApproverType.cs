using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.ApprovalMatrix
{
    public class ApproverType:Base
    {
        [Column(TypeName = "nvarchar(150)")]
        public string approverTypeName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string approverTypeNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
