using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class PHQTRType:Base
    {
        [Column(TypeName = "NVARCHAR(150)")]
        public string trTypeName { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string trTypeNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}
