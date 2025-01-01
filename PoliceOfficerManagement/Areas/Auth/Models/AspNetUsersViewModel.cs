using PoliceOfficerManagement.Data;
using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Areas.Auth.Models
{
    public class AspNetUsersViewModel
    {
        public string? UserName { get; set; }
        public string? ID { get; set; }
        public string? Email { get; set; }
        public string? UserTypeName { get; set; }
        //newly added @28/6/2020
        public string? FullName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Citizenship { get; set; }
        public string? NID { get; set; }
        public int? isActive { get; set; }
        public string? roleId { get; set; }
        public string? RegisterdDate { get; set; }
        public string? PreRegisterdDate { get; set; }
        public string? VerifiedBy { get; set; }
        public EmployeInfo? employeeInfo { get; set; }
        public ApplicationUser? user { get; set; }
    }
}
