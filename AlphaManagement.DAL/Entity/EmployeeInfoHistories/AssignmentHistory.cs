using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
   public class AssignmentHistory:Base
    {
        public int? entryType { get; set; } //1=Admin;2=User

        public String UpdateUserId { get; set; }
        public ApplicationUser UpdateUser { get; set; }

        public int? assignmentId { get; set; }
        public Assignment assignment { get; set; }

        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? assignmentTypeId { get; set; }//1=Assignment,2=Transfer

        //public string assignmentTypeName { get; set; }

        public int? EntryNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int? rankId { get; set; }
        public Rank rank { get; set; }

        public int? designationId { get; set; }
        public Designation designation { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }

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

        public int? statusId { get; set; }
    }
}
