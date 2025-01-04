namespace PoliceOfficerManagement.Areas.Auth.Models
{
    public class AspnetAndEmployeeModel
    {
        public string? UserName { get; set; }
        public string? Id { get; set; }

        public string? Email { get; set; }

        public string? UserTypeName { get; set; }
        public string? FullName { get; set; }
        public string? Citizenship { get; set; }
        public string? NID { get; set; }
        public int? isActive { get; set; }
        public string? roleId { get; set; }
        public string? roleName { get; set; }

        public int? empId { get; set; }
        public string? name { get; set; }
        public DateTime? dateofBirth { get; set; }
        public DateTime? createAt { get; set; }
        public string mobile { get; set; }
        public int? rankId { get; set; }
        public string rankName { get; set; }
        public int? designationId { get; set; }
        public string ApplicationUserId { get; set; }
        public string BPNo { get; set; }
        public int? zoneCircleId { get; set; }
        public string zoneCircleName { get; set; }
        public string zoneCircleNameBN { get; set; }
        public int? divitionDistrictId { get; set; }
        public string divitionDistrictName { get; set; }
        public string divitionDistrictBN { get; set; }
        public string Img { get; set; }
        public string logInPermissionTo { get; set; }
        public int? rangeId { get; set; }
        public int? divId {get; set; }
        public int? zoneId { get; set; }
        public int? thanaId { get; set; }


    }
}
