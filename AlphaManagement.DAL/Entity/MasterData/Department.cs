using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Department:Base
    {
        public int? sectionId { get; set; }
        public Section section { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string deptCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string deptName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string deptNameBn { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string shortName { get; set; }
        public DateTime? startDate { get; set; }

    }
}
