using AlphaManagement.DAL.Entity.EmployeeInfos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class EmployeeType : Base
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string empType { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string empTypeBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string shortName { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfos { get; set; }

    }
}
