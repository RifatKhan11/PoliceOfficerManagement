using PoliceOfficerManagement.Data.Entity;

namespace PoliceOfficerManagement.Areas.MasterData.Models
{
    public class EmployeeViewModel
    {
        public int employeeId { get; set; }
        public int? rangeMetroId { get; set; }
        public int? divisionDistrictId { get; set; }
        public int? zoneCircleId { get; set; }
        public int? policeThanaId { get; set; }
        public int? rankId { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string nameBangla { get; set; }
        public string emailAddress { get; set; }
        public string mobileNumberOffice { get; set; }
        public string mobileNumberPersonal { get; set; }
        public int? designationInfoId { get; set; }
        public int? statusId { get; set; }
        public string ApplicationUserId { get; set; }

        public IEnumerable<EmployeInfo> employeeInfos { get; set; }
        public EmployeInfo employeeInfo { get; set; }
        public IEnumerable<RangeMetro> rangeMetro { get; set; }
        public IEnumerable<Rank> ranks { get; set; }
    }
}
