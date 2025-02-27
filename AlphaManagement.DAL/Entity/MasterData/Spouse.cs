using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.EmployeeInfos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaManagement.DAL.Entity.MasterData
{
    public class Spouse:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }
        public int? spouseRelationId { get; set; }
        public SpouseRelation spouseRelation { get; set; }
        [MaxLength(250)]
        public string spouseName { get; set; }
        [MaxLength(150)]
        public string email { get; set; }
        [MaxLength(250)]
        public string spouseNameBN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }

        public string occupation { get; set; }
        [MaxLength(20)]
        public string gender { get; set; }
        [MaxLength(250)]
        public string designation { get; set; }
        [MaxLength(450)]
        public string organization { get; set; }
        [MaxLength(100)]
        public string bin { get; set; }
        [MaxLength(100)]
        public string nid { get; set; }
        [MaxLength(30)]
        public string bloodGroup { get; set; }
        [MaxLength(250)]
        public string contact { get; set; }
        [MaxLength(450)]
        public string highestEducation { get; set; }
        [MaxLength(100)]
        public string homeDistrict { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string fatherName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string motherName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string maritalStatus { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string birthCertificate { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string remarks { get; set; }

        public int? districtId { get; set; }
        public District district { get; set; }

        public int? isAdminEntry { get; set; } //Ensure Admin Entry
    }
}
