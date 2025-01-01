using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Data.Entity
{
    public class EmployeInfo: Base
    {
        [StringLength(400)]
        public string nameEn { get; set; }

        [StringLength(400)]
        public string nameBn { get; set; }

        [StringLength(50)]
        public string employeeCode { get; set; }

        [StringLength(50)]
        public string nidNumber { get; set; }

        public DateTime? joiningDate { get; set; }
        public DateTime? retirementDate { get; set; }
        public int? joiningRankId { get; set; }

        [StringLength(200)]
        public string homeDistrict { get; set; }

        [StringLength(200)]
        public string homeUpazila { get; set; }
       
        public string reMarks { get; set; }

        [StringLength(50)]
        public string personalPhoneNumber { get; set; }

        [StringLength(50)]
        public string officePhoneNumber { get; set; }
    }
}
