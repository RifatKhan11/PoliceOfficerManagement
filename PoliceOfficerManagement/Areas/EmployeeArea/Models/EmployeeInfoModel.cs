namespace PoliceOfficerManagement.Areas.EmployeeArea.Models
{
    public class EmployeeInfoModel
    {
        public string employeeCode { get; set; }
        public string empName { get; set; }
        public string empNameBn { get; set; }
        public string joiningRank { get; set; }
        public DateTime? joiningDate { get; set; }
        public DateTime? retirmentDate { get; set; }
        public string homeDistrict { get; set; }
        public string homeUpazila { get; set; }
        public string personalPhoneNumber { get; set; }
        public string officePhoneNumber { get; set; }
        public string currentRank { get; set; }
        public string currentPostingPlace { get; set; }
        public string nidNumber { get; set; }
        public string reMarks { get; set; }

        public IEnumerable<EmployeeInfoModel> Employees { get; set; }
    }
}
