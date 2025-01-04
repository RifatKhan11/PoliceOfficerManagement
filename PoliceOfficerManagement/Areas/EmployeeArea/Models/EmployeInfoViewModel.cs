using PoliceOfficerManagement.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PoliceOfficerManagement.Areas.EmployeeArea.Models
{
    public class EmployeInfoViewModel
    { 
        public int Id { get; set; }
        public string nameEn { get; set; } 
        public string nameBn { get; set; } 
        public string employeeCode { get; set; }
        public string nidNumber { get; set; }
        public DateTime? joiningDate { get; set; }
        public DateTime? retirementDate { get; set; }
        public int? joiningRankId { get; set; } 
        public string homeDistrict { get; set; } 
        public string homeUpazila { get; set; } 
        public string reMarks { get; set; } 
        public string personalPhoneNumber { get; set; } 
        public string officePhoneNumber { get; set; }

        public IEnumerable<EmployeInfo> employeInfos { get; set; }
        public IEnumerable<InstitutionInfo> institutionInfos { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Rank> Ranks { get; set; }
        public IEnumerable<RangeMetro> RangeMetros { get; set; }
    }
}
