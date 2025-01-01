using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Data.Entity
{
    public class EducationalInfo:Base
    {
        public int? instituteId { get; set; }

        [StringLength(100)]
        public string passingYear { get; set; }

        [StringLength(100)]
        public string batchNo { get; set; }

        [StringLength(100)]
        public string grade { get; set; }

        [StringLength(420)]
        public string degreeName { get; set; }

        [StringLength(420)]
        public string achievement { get; set; }

        [StringLength(200)]
        public string courseDuration { get; set; }

        public int? employeeId { get; set; }
        public EmployeInfo employee { get; set; }
    }
}
