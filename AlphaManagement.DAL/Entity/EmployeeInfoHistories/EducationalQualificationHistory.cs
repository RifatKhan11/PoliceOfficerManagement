using AlphaManagement.DAL.Entity.EmployeeInfos;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.EmployeeInfoHistories
{
   public class EducationalQualificationHistory:Base
    {
        public int? entryType { get; set; } //1=Admin;2=User

        public String UpdateUserId { get; set; }
        public ApplicationUser UpdateUser { get; set; }

        public int? educationalQualificationId { get; set; }
        public EducationalQualification educationalQualification { get; set; }
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string institution { get; set; }

        public int? resultId { get; set; }
        public Result result { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string majorGroup { get; set; }
        [Column(TypeName = "NVARCHAR(150)")]
        public string grade { get; set; }

        public int? passingYear { get; set; }

        public int? degreeId { get; set; }
        public Degree degree { get; set; }

        public int? organizationId { get; set; }
        public Organization organization { get; set; }

        public int? reldegreesubjectId { get; set; }
        public RelDegreeSubject reldegreesubject { get; set; }
    }
}
