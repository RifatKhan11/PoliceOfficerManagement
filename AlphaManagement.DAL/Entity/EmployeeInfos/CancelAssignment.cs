using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class CancelAssignment : Base
    {
        public int? assignmentMasterId { get; set; }
        public AssignmentMaster assignmentMaster { get; set; }

        public int? assignmentId { get; set; }
        public Assignment assignment { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        
        public int? EntryNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int? rankId { get; set; }
        public Rank rank { get; set; }
        
        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }

        public int? sectionId { get; set; }
        public Section section { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string sectionName { get; set; }
        [Column(TypeName = "NVARCHAR(250)")]
        public string servicePeriod { get; set; }
        [Column(TypeName = "NVARCHAR(550)")]
        public string Remarks { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string designationName { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string unitName { get; set; }

        public int? statusId { get; set; }

        public int? isAdminEntry { get; set; } //Ensure Admin Entry

    }
}
