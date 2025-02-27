using AlphaManagement.DAL.Entity.AddressData;
using AlphaManagement.DAL.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaManagement.DAL.Entity.EmployeeInfos
{
    public class TraningLog:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? toDate { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public int? trainingCategoryId { get; set; }
        public TrainingCategory trainingCategory { get; set; }

        public int? trainingInstituteId { get; set; }
        public TrainingInstitute trainingInstitute { get; set; }

        public int? specialSkillTypeId { get; set; }
        public SpecialSkillType specialSkillType { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string remarks { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string trainingTitle { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string sponsoringAgency { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string trainingType { get; set; }
        public string referenceNumber { get; set; }

        public int? isAdminEntry { get; set; } //Ensure Admin Entry
        public int? status { get; set; } //Ensure Admin Entry
    }
}
