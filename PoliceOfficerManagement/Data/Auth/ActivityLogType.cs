using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOfficerManagement.Data.Auth
{
    public class ActivityLogType : Base
    {
        [Column(TypeName = "NVARCHAR(200)")]
        public string typeName { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string tableName { get; set; }
        public int? shortOrder { get; set; }
    }
}
