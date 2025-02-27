using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.ApprovalMatrix
{
    public class MatrixType:Base
    {
        [Column(TypeName = "nvarchar(150)")]
        public string matrixTypeName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string matrixTypeNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
