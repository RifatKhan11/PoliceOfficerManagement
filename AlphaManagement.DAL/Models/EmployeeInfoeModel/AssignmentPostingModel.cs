namespace AlphaManagement.DAL.Models.EmployeeInfoeModel
{
    public class AssignmentPostingModel
    {
        public int? unitId { get; set; }
        public int? employeeId { get; set; }
        public int? rankId { get; set; }
        public int? sectionId { get; set; }
        public int? numOfPost { get; set; }
        public int? totalEmployee { get; set; }
        public int? vacantPost { get; set; }
        public int? specialBranchUnitId { get; set; }
        public int? isParent { get; set; }
        public int? lockedEmpId { get; set; }
        public int? pHQTRTypeId { get; set; }
        public string mainUnit { get; set; }
        public string subUnit { get; set; }
        public string name { get; set; }
        public string employeeCode { get; set; }
        public string expertise { get; set; }
        public string colorCode { get; set; }
        public string attachedUnit { get; set; }
        public string sectionName { get; set; }
        public string rankName { get; set; }
    }
}
