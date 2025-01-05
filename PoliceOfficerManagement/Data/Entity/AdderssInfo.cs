namespace PoliceOfficerManagement.Data.Entity
{
    public class AdderssInfo:Base
    {
        public int? employeeId { get; set; }
        public EmployeInfo employee { get; set; }

        public int addressType { get; set; }// 1=PRESENT, 2=PERMANENT
        public string roadInfo { get; set; }
        public int? villegeId { get; set; }
        public Village villege { get; set; }
        public int? unionId { get; set; }
        public UnionWard union { get; set; }
        public int? thanaId { get;set; }
        public Thana thana { get; set; }
        public int? districtId { get;set; }
        public District district { get; set; }
        public int? divisionId { get;set; }
        public Division division { get; set; }
    }
}
