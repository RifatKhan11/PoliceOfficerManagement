namespace PoliceOfficerManagement.Data.Entity
{
    public class PostingPlace:Base
    {
        public int? rankId { get; set; }
        public DateTime? postingFrom { get; set; }
        public DateTime? postingTo { get; set; }

        public int? thanaId { get; set; }
        public int? zoneId { get; set; }
        public int? districtId { get; set; }
        public int? rangeId { get; set; }
        public string reMarks { get; set; }
        public DateTime? promotionDate { get; set; }


        public int? employeeId { get; set; }
        public EmployeInfo employee { get; set; }
    }
}
